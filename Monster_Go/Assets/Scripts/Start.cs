using UnityEngine;

public class Starter : MonoBehaviour
{
    void Start()
    {
        bool done = PlayerPrefs.GetInt("done", 0) == 1;

        if (!done)
        {
            PlayerPrefs.SetInt("scale", 5);
            PlayerPrefs.SetInt("num", 1);
            PlayerPrefs.SetInt("done", 1);

            PlayerPrefs.Save(); // 권장
        }
    }
}
