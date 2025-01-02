using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterPickSceneControl : MonoBehaviour
{
    // �ֵ����ڴ洢ÿ���½ڰ�ť�Ͷ�Ӧ�ĳ�������
    private Dictionary<string, string> chapterScenes = new Dictionary<string, string>
    {
        { "Chapter1", "ForestScene" },
        { "Chapter2", "SnowScene" },
        { "Chapter3", "DesertScene" }
        // ������������Ӹ�����½�
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

    // ��ת��������ҳ��
    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    // �����½ڰ�ť�����Ƽ��ض�Ӧ�ĳ���
    public void LoadChapter(GameObject chapterButton)
    {
        // ��ȡ��ť������
        string buttonName = chapterButton.name;

        // ����ֵ����Ƿ���ڶ�Ӧ�ĳ�������
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