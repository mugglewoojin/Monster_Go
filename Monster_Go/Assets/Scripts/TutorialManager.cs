using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public Image image;                  // 등장시키고 싶은 이미지
        public Text textObject;              // 등장시키고 싶은 텍스트 UI
        public Button button;                // 일반 버튼
        public bool requireSpecificButton = false; // 특정 버튼만 눌러야 진행
        public Button specificButton;        // 특정 버튼
        [TextArea] public string descriptionText; // 하단 설명 텍스트
    }

    public TutorialStep[] steps;
    public Text descriptionBox; // 하단 설명 텍스트 박스
    private int currentStep = -1;

    void Start()
    {
        // 처음에는 모든 UI 요소 숨기기
        foreach (var step in steps)
        {
            if (step.image != null) step.image.gameObject.SetActive(false);
            if (step.textObject != null) step.textObject.gameObject.SetActive(false);
            if (step.button != null) step.button.gameObject.SetActive(false);
            if (step.specificButton != null) step.specificButton.gameObject.SetActive(false);
        }

        descriptionBox.text = "";
        NextStep();
    }

    void Update()
    {
        if (currentStep < steps.Length)
        {
            var step = steps[currentStep];

            // 특정 버튼 단계가 아니면
            // - 아무 키 입력으로 다음 단계 가능
            // - 버튼이 나타나도 다른 키 입력으로 넘어갈 수 있음
            if (!step.requireSpecificButton)
            {
                if (Input.anyKeyDown)
                    NextStep();
            }
        }
    }

    public void NextStep()
    {
        currentStep++;
        if (currentStep >= steps.Length)
        {
            Debug.Log("튜토리얼 종료");
            SceneManager.LoadScene("StartScene");
            return;
        }

        var step = steps[currentStep];

        // UI 요소 활성화 (사라지지 않음)
        if (step.image != null) step.image.gameObject.SetActive(true);
        if (step.textObject != null) step.textObject.gameObject.SetActive(true);
        if (step.button != null) step.button.gameObject.SetActive(true);

        // 버튼 이벤트 등록
        if (step.button != null && !step.requireSpecificButton)
        {
            step.button.onClick.RemoveAllListeners();
            step.button.onClick.AddListener(NextStep); // 버튼 클릭으로도 진행 가능
        }

        if (step.specificButton != null && step.requireSpecificButton)
        {
            step.specificButton.onClick.RemoveAllListeners();
            step.specificButton.onClick.AddListener(NextStep); // 반드시 클릭해야 진행
        }

        // 하단 설명 박스 업데이트 (덮어쓰기)
        descriptionBox.text = step.descriptionText;
    }
}
