using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniMapController : MonoBehaviour
{
    [Header("Map")]
    public Image[,] cells = new Image[5, 5];

    [Header("Colors")]
    public Color baseColor = Color.black;
    public Color playerColor = Color.green;
    public Color enemyColor = Color.red;
    
    public Game_Data gameData;

    [Header("Skill")]
    public float revealTime = 3f;
    public float cooldown = 10f;

    private bool canUse = true;

    void Awake()
    {
        int index = 0;
        for (int y = 4; y >= 0; y--)
        {
            for (int x = 0; x < 5; x++)
            {
                cells[x, y] = flatCells[index++];
            }
        }
    }

    void Start()
    {
        ClearMap();
    }

    public Image[] flatCells; // 25개

    


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canUse)
        {
            StartCoroutine(RevealEnemies());
        }
    }

    IEnumerator RevealEnemies()
    {
        canUse = false;

        DrawMap(true);
        yield return new WaitForSeconds(revealTime);

        ClearMap();

        yield return new WaitForSeconds(cooldown);
        canUse = true;
    }

    void ClearMap()
    {
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 5; y++)
                cells[x, y].color = baseColor;

        // 플레이어는 항상 표시
        cells[2, 2].color = playerColor;
    }

    void DrawMap(bool showEnemies)
    {
        ClearMap();

        if (!showEnemies) return;

        for (int i = 0; i < gameData.monster_num; i++)
        {
            int mx = gameData.monster[i, 0];
            int my = gameData.monster[i, 1];

            if (mx == -1) continue;

            int dx = mx - gameData.playerX;
            int dy = my - gameData.playerY;

            if (Mathf.Abs(dx) > 2 || Mathf.Abs(dy) > 2)
                continue;

            int mapX = dx + 2;
            int mapY = dy + 2;

            cells[mapX, mapY].color = enemyColor;
        }
    }

}
