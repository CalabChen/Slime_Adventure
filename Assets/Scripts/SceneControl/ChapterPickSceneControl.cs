using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterPickSceneControl : MonoBehaviour
{
    // �ֵ����ڴ洢ÿ���½ڰ�ť�Ͷ�Ӧ�ĳ�������
    private Dictionary<string, string> chapterScenes = new Dictionary<string, string>
    {
        { "Chapter1", "ForestScene" },
        { "Chapter2", "Chapter2Scene" },
        { "Chapter3", "Chapter3Scene" }
        // ������������Ӹ�����½�
    };

    // Start is called before the first frame update
    void Start()
    {
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