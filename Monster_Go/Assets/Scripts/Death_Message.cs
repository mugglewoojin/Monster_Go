using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death_Message : MonoBehaviour
{
    public float fadeInDuration = 2.0f;   // 페이드 인 시간
    public float visibleTime = 2.0f;      // 완전히 보이는 시간
    public float fadeOutDuration = 2.0f;  // 페이드 아웃 시간

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f; // 시작은 투명
    }

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // 페이드 인
        yield return StartCoroutine(Fade(0f, 1f, fadeInDuration));

        // 2초 유지
        yield return new WaitForSeconds(visibleTime);

        // 페이드 아웃
        yield return StartCoroutine(Fade(1f, 0f, fadeOutDuration));

        SceneManager.LoadScene("StartScene");
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        
    }
}
