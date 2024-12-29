using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 跳转到关卡选择界面
    public void GotoStagePickScene()
    {
        SceneManager.LoadScene("ChapterPickScene", LoadSceneMode.Single);
    }

    // 跳转到设置页面
    public void GotoSettingScene()
    {
        SceneManager.LoadScene("SettingScene", LoadSceneMode.Single);
    }

    // 跳转到开发者页面
    public void GotoDeveloperScene()
    {
        SceneManager.LoadScene("DeveloperScene", LoadSceneMode.Single);
    }

    // 退出游戏
    public void Quit()
    {
        Application.Quit();
    }
}
