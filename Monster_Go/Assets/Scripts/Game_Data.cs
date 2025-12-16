using UnityEngine;

public class Game_Data : MonoBehaviour
{
    public Room[,] room;
    public int[,] monster;

    public int room_Scale = 5;
    public int monster_num = 3;

    int playerX;
    int playerY;

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

        SpawnMonsters();
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
                del = room[x, y + 1].DelLevelCalculator();
                return !room[x, y + 1].door[3];

            case 1: // right
                if (x + 1 >= room_Scale) return false;
                del = room[x + 1, y].DelLevelCalculator();
                return !room[x + 1, y].door[2];

            case 2: // left
                if (x - 1 < 0) return false;
                del = room[x - 1, y].DelLevelCalculator();
                return !room[x - 1, y].door[1];

            case 3: // down
                if (y - 1 < 0) return false;
                del = room[x, y - 1].DelLevelCalculator();
                return !room[x, y - 1].door[0];
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

    // =========================
    // 실제 몬스터 이동
    // =========================
    void FixedUpdate()
    {
        for (int i = 0; i < monster_num; i++)
        {
            int x = monster[i, 0];
            int y = monster[i, 1];

            int dir = Mob_Move(x, y);
            if (dir == -1) continue;

            switch (dir)
            {
                case 0: monster[i, 1]++; break;
                case 1: monster[i, 0]++; break;
                case 2: monster[i, 0]--; break;
                case 3: monster[i, 1]--; break;
            }
        }
        
    }
}


public class Room
{
    // 0: up, 1: right, 2: left, 3: down
    public bool[] door = new bool[4];

    public int light = 1;
    public int isplayer = 0;

    public int DelLevelCalculator()
    {
        return 2 - light + isplayer;
    }
}

