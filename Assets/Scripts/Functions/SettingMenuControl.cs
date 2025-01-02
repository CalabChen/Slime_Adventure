using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenuControl : MonoBehaviour
{
    public GameObject moreMenu;
    public Slider musicSlider;
    public Text musicNumber;
    public AudioSource bgm;

    void Start()
    {
        // 初始化MoreMenu为隐藏状态
        moreMenu.SetActive(false);

        // 设置滑块的初始值
        musicSlider.value = bgm.volume * 100;
        musicNumber.text = ((int)musicSlider.value).ToString();

        // 添加滑块值改变事件
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    public void OnSettingControlClicked()
    {
        // 显示MoreMenu
        moreMenu.SetActive(true);
    }

    private void OnMusicSliderChanged(float value)
    {
        // 调整BGM的音量
        bgm.volume = value / 100;

        // 更新音量数字显示
        musicNumber.text = ((int)value).ToString();
    }

    public void OnShutDownClicked()
    {
        // 显示MoreMenu
        moreMenu.SetActive(false);
    }
}