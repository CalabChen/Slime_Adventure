using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������ҳ��
    public void ReturnPreviousScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}