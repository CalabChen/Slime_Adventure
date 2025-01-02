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
        // ��ʼ��MoreMenuΪ����״̬
        moreMenu.SetActive(false);

        // ���û���ĳ�ʼֵ
        musicSlider.value = bgm.volume * 100;
        musicNumber.text = ((int)musicSlider.value).ToString();

        // ��ӻ���ֵ�ı��¼�
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    public void OnSettingControlClicked()
    {
        // ��ʾMoreMenu
        moreMenu.SetActive(true);
    }

    private void OnMusicSliderChanged(float value)
    {
        // ����BGM������
        bgm.volume = value / 100;

        // ��������������ʾ
        musicNumber.text = ((int)value).ToString();
    }

    public void OnShutDownClicked()
    {
        // ��ʾMoreMenu
        moreMenu.SetActive(false);
    }
}