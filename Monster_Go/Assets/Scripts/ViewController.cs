using UnityEngine;

public class ViewController : MonoBehaviour
{
    // 0: up, 1: right, 2: down, 3: left
    public int viewDir = 0;

    public DoorUIController ui;

    void Start()
    {
        ui.UpdateUI();
    }

    // 왼쪽 끝 콜라이더
    public void RotateLeft()
    {
        viewDir = (viewDir + 3) % 4; // -1 효과
        ui.UpdateUI();
    }

    // 오른쪽 끝 콜라이더
    public void RotateRight()
    {
        viewDir = (viewDir + 1) % 4;
        ui.UpdateUI();
    }
}
