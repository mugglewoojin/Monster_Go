using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [Header("Image Settings")]
    public Transform[] images;   // 4개 이미지 (순서대로 1~4)
    public float imageWidth = 10f;
    public float viewWidth = 8f;

    [Header("Scroll Settings")]
    public float scrollSpeed = 5f;
    public float mouseDeadZone = 0.1f; // 중앙 무시 영역 (0~1)

    private Camera cam;
    private int currentIndex = 1; // 현재 중앙 이미지 (0~3)

    void Start()
    {
        cam = Camera.main;

        // 초기 배치 (1 2 3 4)
        for (int i = 0; i < images.Length; i++)
        {
            images[i].position = new Vector3(i * imageWidth, 0, 0);
        }

        cam.transform.position = new Vector3(imageWidth, 0, -10);
    }

    void Update()
    {
        HandleMouseScroll();
        HandleImageLoop();
    }

    void HandleMouseScroll()
    {
        float mouseX = Input.mousePosition.x / Screen.width; // 0~1
        float offset = mouseX - 0.5f;

        if (Mathf.Abs(offset) < mouseDeadZone)
            return;

        float move = offset * scrollSpeed * Time.deltaTime;
        cam.transform.position += new Vector3(move, 0, 0);
    }

    void HandleImageLoop()
    {
        float camX = cam.transform.position.x;
        float centerX = images[currentIndex].position.x;

        // 오른쪽으로 넘어감
        if (camX > centerX + imageWidth / 2f)
        {
            MoveRight();
        }
        // 왼쪽으로 넘어감
        else if (camX < centerX - imageWidth / 2f)
        {
            MoveLeft();
        }
    }

    void MoveRight()
    {
        int leftMost = (currentIndex + images.Length - 1) % images.Length;

        images[leftMost].position =
            images[currentIndex].position + Vector3.right * imageWidth;

        currentIndex = (currentIndex + 1) % images.Length;
    }

    void MoveLeft()
    {
        int rightMost = (currentIndex + 1) % images.Length;

        images[rightMost].position =
            images[currentIndex].position + Vector3.left * imageWidth;

        currentIndex = (currentIndex + images.Length - 1) % images.Length;
    }
}
