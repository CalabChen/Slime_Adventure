using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControl : MonoBehaviour
{
    public AudioSource music;

    // ��ת���ؿ�ѡ�����
    public void GotoStagePickScene()
    {
        SceneManager.LoadScene("ChapterPickScene", LoadSceneMode.Single);
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

    // ��ת���ؿ�ѡ�����
    public void GotoTestScene()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }
}
