using UnityEngine;

public class ViewController : MonoBehaviour
{
    // 0: up, 1: left, 2: down, 3: right
    public int viewDir = 0;

    public DoorUIController ui;

    void Start()
    {
        ui.UpdateUI();
    }

    // 왼쪽 끝 콜라이더 → 반시계 회전
    public void RotateLeft()
    {
        viewDir = (viewDir + 1) % 4;
        ui.UpdateUI();
    }

    // 오른쪽 끝 콜라이더 → 시계 회전
    public void RotateRight()
    {
        viewDir = (viewDir + 3) % 4; // -1 효과
        ui.UpdateUI();
    }
}
