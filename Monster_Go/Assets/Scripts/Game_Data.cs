using UnityEngine;
using System.Collections;

public class Game_Data : MonoBehaviour
{
    public Room[,] room;
    public int[,] monster;

    public int room_Scale = 5;
    public int monster_num = 3;

    public int playerX;
    public int playerY;

    public bool playerHasTorch = false;


    void Start()
    {
        // 방 생성
        room = new Room[room_Scale, room_Scale];
        for (int x = 0; x < room_Scale; x++)
        {
            for (int y = 0; y < room_Scale; y++)
            {
                room[x, y] = new Room();
            }
        }

        // 플레이어 중앙 배치
        playerX = room_Scale / 2;
        playerY = room_Scale / 2;
        room[playerX, playerY].isplayer = 1;

        // 몬스터 배열
        monster = new int[monster_num, 2];

        for (int i = 0; i < monster_num; i++)
        {
            monster[i, 0] = -1;
            monster[i, 1] = -1;
        }

        SpawnMonsters();

        StartCoroutine(activation());
    }

    // =========================
    // 몬스터 스폰
    // =========================
    void SpawnMonsters()
    {
        for (int i = 0; i < monster_num; i++)
        {
            while (true)
            {
                int x = Random.Range(0, room_Scale);
                int y = Random.Range(0, room_Scale);

                // 플레이어 + 주변 제외 (맨해튼 거리)
                int dist = Mathf.Abs(x - playerX) + Mathf.Abs(y - playerY);
                if (dist <= 1) continue;

                // 중복 방지
                bool overlap = false;
                for (int j = 0; j < i; j++)
                {
                    if (monster[j, 0] == x && monster[j, 1] == y)
                    {
                        overlap = true;
                        break;
                    }
                }
                if (overlap) continue;

                monster[i, 0] = x;
                monster[i, 1] = y;
                break;
            }
        }
    }

    // =========================
    // 이동 가능 여부 + del_level
    // =========================
    public bool canmove(int dir, int x, int y, out int del)
    {
        del = int.MinValue;

        switch (dir)
        {
            case 0: // up
                if (y + 1 >= room_Scale) return false;

                // 현재 방 up 문 + 위 방 down 문
                if (room[x, y].door[0]) return false;
                if (room[x, y + 1].door[3]) return false;

                del = room[x, y + 1].DelLevelCalculator();
                return true;

            case 1: // right
                if (x + 1 >= room_Scale) return false;

                // 현재 방 right 문 + 오른쪽 방 left 문
                if (room[x, y].door[1]) return false;
                if (room[x + 1, y].door[2]) return false;

                del = room[x + 1, y].DelLevelCalculator();
                return true;

            case 2: // left
                if (x - 1 < 0) return false;

                // 현재 방 left 문 + 왼쪽 방 right 문
                if (room[x, y].door[2]) return false;
                if (room[x - 1, y].door[1]) return false;

                del = room[x - 1, y].DelLevelCalculator();
                return true;

            case 3: // down
                if (y - 1 < 0) return false;

                // 현재 방 down 문 + 아래 방 up 문
                if (room[x, y].door[3]) return false;
                if (room[x, y - 1].door[0]) return false;

                del = room[x, y - 1].DelLevelCalculator();
                return true;
        }

        return false;
    }


    // =========================
    // 몬스터 이동 방향 결정
    // =========================
    int Mob_Move(int x, int y)
    {
        int bestDel = int.MinValue;
        int[] candidates = new int[4];
        int count = 0;

        for (int dir = 0; dir < 4; dir++)
        {
            int del;
            if (canmove(dir, x, y, out del))
            {
                if (del > bestDel)
                {
                    bestDel = del;
                    count = 0;
                    candidates[count++] = dir;
                }
                else if (del == bestDel)
                {
                    candidates[count++] = dir;
                }
            }
        }

        if (count == 0) return -1;

        // 동점이면 랜덤
        return candidates[Random.Range(0, count)];
    }

    public void TorchButton()
    {
        Room cur = room[playerX, playerY];

        if (playerHasTorch) return;   // 이미 들고 있음
        if (!cur.hasTorch) return;    // 방에 횃불 없음

        playerHasTorch = true;
        cur.hasTorch = false;
    }

    public void DropTorchButton()
    {
        Room cur = room[playerX, playerY];

        if (!playerHasTorch) return;  // 들고 있지 않음
        if (cur.hasTorch) return;     // 이미 방에 횃불 있음

        playerHasTorch = false;
        cur.hasTorch = true;
    }

    public void ToggleDoorButton(int dir)
    {
        bool current = room[playerX, playerY].door[dir];
        SetDoor(playerX, playerY, dir, !current);
    }

    public void SetDoor(int x, int y, int dir, bool closed)
    {
        room[x, y].door[dir] = closed;

        int nx = x, ny = y, ndir = 0;

        switch (dir)
        {
            case 0: ny++; ndir = 3; break;
            case 1: nx++; ndir = 2; break;
            case 2: nx--; ndir = 1; break;
            case 3: ny--; ndir = 0; break;
        }

        if (nx >= 0 && nx < room_Scale &&
            ny >= 0 && ny < room_Scale)
        {
            room[nx, ny].door[ndir] = closed;
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Dead");
    }


    void CheckPlayerDeath()
    {
        for (int i = 0; i < monster_num; i++)
        {
            int mx = monster[i, 0];
            int my = monster[i, 1];

            // 죽은 몬스터 무시
            if (mx == -1) continue;

            if (mx == playerX && my == playerY)
            {
                PlayerDie();
                return;
            }
        }
    }

    void CheckWarning()
    {
        for (int i = 0; i < monster_num; i++)
        {
            int mx = monster[i, 0];
            int my = monster[i, 1];

            if (mx == -1) continue; // 죽은 몬스터 무시

            // 상
            if (mx == playerX && my == playerY + 1)
            {
                if (!room[playerX, playerY].door[0])
                    ShowWarning();
            }
            // 우
            else if (mx == playerX + 1 && my == playerY)
            {
                if (!room[playerX, playerY].door[1])
                    ShowWarning();
            }
            // 좌
            else if (mx == playerX - 1 && my == playerY)
            {
                if (!room[playerX, playerY].door[2])
                    ShowWarning();
            }
            // 하
            else if (mx == playerX && my == playerY - 1)
            {
                if (!room[playerX, playerY].door[3])
                    ShowWarning();
            }
        }
    }

    void ShowWarning()
    {
        Debug.Log("⚠️ 몬스터가 근처에 있다!");
        
        // 여기에 나중에
        // - 화면 흔들림
        // - 심장 소리
        // - 붉은 비네트
        // - UI 아이콘
    }








    // =========================
    // 실제 몬스터 이동
    // =========================
    IEnumerator activation()
    {
        for (int i = 0; i < monster_num; i++)
        {
            int x = monster[i, 0];
            int y = monster[i, 1];

            // 이미 죽은 몬스터
            if (x == -1) continue;

            int dir = Mob_Move(x, y);

            // 🔥 이동 불가 → 사망
            if (dir == -1)
            {
                monster[i, 0] = -1;
                monster[i, 1] = -1;
                continue;
            }

            switch (dir)
            {
                case 0: monster[i, 1]++; break;
                case 1: monster[i, 0]++; break;
                case 2: monster[i, 0]--; break;
                case 3: monster[i, 1]--; break;
            }
        }

        CheckWarning();

        CheckPlayerDeath();

        yield return new WaitForSeconds(5f);
        StartCoroutine(activation());
    }

}


public class Room
{
    // 0: up, 1: right, 2: left, 3: down
    public bool[] door = new bool[4];

    public int light = 1;
    public const int MAX_LIGHT = 2;   // ⭐ 최대 밝기

    public bool hasTorch = true;
    public int isplayer = 0;

    public int DelLevelCalculator()
    {
        return 2 - light + isplayer;
    }
}


