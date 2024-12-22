using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StagePickSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 跳转到开发者页面
    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    // 退出游戏
    public void Quit()
    {
        Application.Quit();
    }
}
