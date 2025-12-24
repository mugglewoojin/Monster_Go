using UnityEngine;

public class EdgeLookCollider : MonoBehaviour
{
    public ViewController view;
    public bool isLeft; // true = 왼쪽, false = 오른쪽

    void OnMouseEnter()
    {
        if (isLeft)
            view.RotateLeft();
        else
            view.RotateRight();
    }

    void Update()
    {
        if(isLeft && Input.GetKeyDown(KeyCode.LeftArrow)){
            view.RotateRight();
        }
        else if(!isLeft && Input.GetKeyDown(KeyCode.RightArrow)){
            view.RotateLeft();
        }
        
    }
}
