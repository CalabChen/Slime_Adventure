using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuXin;
    public bool isStop = true;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        menuXin.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuXin.SetActive(true);
                isStop = false;
                Time.timeScale=(0);// game stop
                music.Pause();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuXin.SetActive(false);
            isStop = true;
            Time.timeScale=(1);// game stop
            music.Play();
        }
    }

    public void Resume()
    {
        menuXin.SetActive(false);
        isStop = true;
        Time.timeScale = 1;
        music.Play();
    }

    public void RestartTest()
    {
        SceneManager.LoadScene("TestScene");
        Time.timeScale = 1;
    }

    public void RestartForest()
    {
        SceneManager.LoadScene("ForestScene");
        Time.timeScale = 1;
    }

    public void RestartSnow()
    {
        SceneManager.LoadScene("SnowScene");
        Time.timeScale = 1;
    }

    public void RestartDesert()
    {
        SceneManager.LoadScene("DesertScene");
        Time.timeScale = 1;
    }

    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene("ChapterPickScene",LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
