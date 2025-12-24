using UnityEngine;

public class EdgeLookCollider : MonoBehaviour
{
    public ViewController view;
    public bool isLeft; // true = 왼쪽, false = 오른쪽

    void Update()
    {
        if(isLeft && Input.GetKeyDown(KeyCode.LeftArrow)){
            view.RotateLeft();
        }
        else if(!isLeft && Input.GetKeyDown(KeyCode.RightArrow)){
            view.RotateRight();
        }
        
    }
}
