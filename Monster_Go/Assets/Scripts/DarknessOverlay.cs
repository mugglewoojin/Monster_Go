using UnityEngine;
using UnityEngine.UI;

public class DarknessOverlay : MonoBehaviour
{
    [Header("References")]
    public Game_Data gameData;
    public Image overlayImage;

    [Header("Light Settings")]
    [Tooltip("light = 0 일 때 최대 어두움")]
    public float maxAlpha = 0.75f;

    [Tooltip("light = MAX_LIGHT 일 때 최소 어두움")]
    public float minAlpha = 0.0f;

    void Update()
    {
        UpdateDarkness();
    }

    void UpdateDarkness()
    {
        Room curRoom = gameData.room[gameData.playerX, gameData.playerY];

        // ✅ 조건 기반 빛 계산
        int light = curRoom.GetLightLevel(gameData.playerHasTorch);
        int maxLight = Room.MAX_LIGHT;

        // 0~1 정규화
        float t = Mathf.Clamp01((float)light / maxLight);

        // 밝을수록 투명
        float alpha = Mathf.Lerp(maxAlpha, minAlpha, t);

        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }
}
