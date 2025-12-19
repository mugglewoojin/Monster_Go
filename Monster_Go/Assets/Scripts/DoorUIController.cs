using UnityEngine;
using UnityEngine.UI;

public class DoorUIController : MonoBehaviour
{
    public Game_Data gameData;
    public ViewController view;

    [Header("Images")]
    public Image doorImage;
    public Image leverImage;

    [Header("Sprites")]
    public Sprite doorOpen;
    public Sprite doorClosed;

    public Sprite leverUp;    // 문 열림
    public Sprite leverDown;  // 문 닫힘

    public void UpdateUI()
    {
        bool closed = gameData.IsDoorClosed(view.viewDir);

        doorImage.sprite = closed ? doorClosed : doorOpen;
        leverImage.sprite = closed ? leverDown : leverUp;
    }

    public void OnLeverButton()
    {
        gameData.ToggleDoor(view.viewDir);
        UpdateUI();
    }
}
