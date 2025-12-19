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
                directionText.text = "N";
                break;
            case 1:
                directionText.text = "E";
                break;
            case 2:
                directionText.text = "S";
                break;
            case 3:
                directionText.text = "W";
                break;
        }
    }
}
