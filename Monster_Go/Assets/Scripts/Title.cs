using UnityEngine;
using UnityEngine.SceneManagement;

public class TItle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ToSetting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnExitButton(){
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdApbYlidTkfkx-0gaMjIKm_ONNO11xq2j8ZnFZM_DbAILUWw/viewform?usp=publish-editor");
        Application.Quit();
    }

    public void OnTutorialButton(){
        SceneManager.LoadScene("Tutorial");
    }
}
