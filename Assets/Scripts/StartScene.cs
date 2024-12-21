using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��ת������ҳ��
    public void GotoSettingScene()
    {
        SceneManager.LoadScene("SettingScene", LoadSceneMode.Single);
    }

    // ��ת��������ҳ��
    public void GotoDeveloperScene()
    {
        SceneManager.LoadScene("DeveloperScene", LoadSceneMode.Single);
    }

    // �˳���Ϸ
    public void Quit()
    {
        Application.Quit();
    }
}