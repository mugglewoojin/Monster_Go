using UnityEngine;
using UnityEngine.UI;

public class DirectionUIController : MonoBehaviour
{
    public ViewController view;
    public Text directionText;   // TextMeshPro 쓰면 TMP_Text로 변경

    void Update()
    {
        UpdateDirectionText();
    }

    void UpdateDirectionText()
    {
        switch (view.viewDir)
        {
            case 0:
                directionText.text = "Direction: N";
                break;
            case 1:
                directionText.text = "Direction: E";
                break;
            case 2:
                directionText.text = "Direction: S";
                break;
            case 3:
                directionText.text = "Direction: W";
                break;
        }
    }
}
