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

    // ��ת��������ҳ��
    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    // �˳���Ϸ
    public void Quit()
    {
        Application.Quit();
    }
}
