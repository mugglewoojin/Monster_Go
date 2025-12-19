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

    public AudioClip clip;
    void Start()
    {
        UpdateUI();
        AudioSource audio = gameObject.AddComponent<AudioSource>();
    }

    // =========================
    // UI 갱신
    // =========================
    public void UpdateUI()
    {
        bool closed = gameData.IsDoorClosed(view.viewDir);

        doorImage.sprite = closed ? doorClosed : doorOpen;
        leverImage.sprite = closed ? leverDown : leverUp;
    }

    // =========================
    // 레버 클릭 → 문 토글
    // =========================
    public void OnLeverButton()
    {
        gameData.ToggleDoor(view.viewDir);
        GetComponent<AudioSource>().PlayOneShot(clip);
        UpdateUI();
    }

    // =========================
    // 문 클릭 → 이동 시도
    // =========================
    public void OnDoorButton()
    {
        bool moved = gameData.TryMovePlayer(view.viewDir);

        if (moved)
        {
            Debug.Log("플레이어 이동 성공");
            UpdateUI();
        }
        else
        {
            Debug.Log("이동 불가");
        }
    }
}
