using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterPickSceneControl : MonoBehaviour
{
    // 字典用于存储每个章节按钮和对应的场景名称
    private Dictionary<string, string> chapterScenes = new Dictionary<string, string>
    {
        { "Chapter1", "ForestScene" },
        { "Chapter2", "SnowScene" },
        { "Chapter3", "DesertScene" }
        // 可以在这里添加更多的章节
    };
    public Text CherriesTXT;

    // Start is called before the first frame update
    void Start()
    {
        CherriesTXT.text = PlayerPrefs.GetInt("MaxCherries", 0) + "/6";
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

    // 根据章节按钮的名称加载对应的场景
    public void LoadChapter(GameObject chapterButton)
    {
        // 获取按钮的名称
        string buttonName = chapterButton.name;

        // 检查字典中是否存在对应的场景名称
        if (chapterScenes.TryGetValue(buttonName, out string sceneName))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning($"Invalid chapter button: {buttonName}");
        }
    }
}