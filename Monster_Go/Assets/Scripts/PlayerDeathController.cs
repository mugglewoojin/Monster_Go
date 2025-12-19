using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathController : MonoBehaviour
{
    [Header("References")]
    public Game_Data gameData;
    public Image deathImage;  // 사망 시 나타낼 이미지

    [Header("Settings")]
    public float delayBeforeScene = 3f;
    public string deathSceneName = "DeathMessage";

    public AudioSource audioSource;
    public AudioClip clip;

    void Awake()
    {
        // 처음에는 숨기기
        if (deathImage != null)
            deathImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // 여기서는 Game_Data에서 플레이어 사망 체크 후 호출 가능
        // 예: Game_Data.PlayerDie()에서 이 스크립트 호출
    }

    // 게임 데이터에서 PlayerDie() 안에 호출
    public void OnPlayerDie()
    {
        if (deathImage != null)
            deathImage.gameObject.SetActive(true);

        StartCoroutine(GoToDeathScene());
    }

    IEnumerator GoToDeathScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(delayBeforeScene);
        SceneManager.LoadScene(deathSceneName);
    }
}
