using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ·µ»ØÖ÷Ò³Ãæ
    public void ReturnPreviousScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}
