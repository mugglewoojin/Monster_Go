using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider sliderS;
    public Slider sliderM;

    public TextMeshProUGUI textS;
    public TextMeshProUGUI textM;

    public static int scale;
    int minScale = 5;
    int maxScale = 9;
    public static int num;
    int minNum = 1;
    int maxNum = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderS.minValue = minScale;
        sliderS.maxValue = maxScale;
        sliderM.minValue = minNum;
        sliderM.maxValue = maxNum;
    }

    // Update is called once per frame
    void Update()
    {
        scale = (int)sliderS.value;
        num = (int)sliderM.value;

        textS.text = scale.ToString();
        textM.text = num.ToString();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("StartScene");
    }
}
