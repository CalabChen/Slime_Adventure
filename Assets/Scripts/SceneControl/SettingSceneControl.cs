using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneControl : MonoBehaviour
{
    public Slider musicSlider; // ���ֻ���
    public Text musicNumber; // ������ֵ��ʾ
    public Slider lightSlider; // ���Ȼ���
    public Text lightNumber; // ������ֵ��ʾ

    private Renderer[] renderers; // ��Ⱦ�����飬���ڵ�������
    private Light[] lights; // ��Դ���飬���ڵ�����Դǿ��

    void Start()
    {
        // ��ȡ�����Ӷ���� Renderer ���
        renderers = GetComponentsInChildren<Renderer>();

        // ��ȡ�����е����й�Դ
        lights = FindObjectsOfType<Light>();

        // ��ʼ������ֵ
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 50f);
        lightSlider.value = PlayerPrefs.GetFloat("Brightness", 50f);

        // ������ֵ��ʾ
        UpdateNumbers();

        // ���ó�ʼ����
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.ChangeVolume(musicSlider.value / 100f);
        }

        // ��ֹ��� GameObject �ڳ����л�ʱ������
        DontDestroyOnLoad(gameObject);
    }

    void UpdateNumbers()
    {
        musicNumber.text = musicSlider.value.ToString("0");
        lightNumber.text = lightSlider.value.ToString("0");
    }

    public void OnMusicSliderValueChanged(float value)
    {
        if (BackgroundMusicManager.Instance != null) // ȷ����ƵԴ����
        {
            BackgroundMusicManager.Instance.ChangeVolume(value / 100f); // ������ƵԴ����
            PlayerPrefs.SetFloat("MusicVolume", value); // ���浽���ƫ��
        }
        UpdateNumbers(); // ������ֵ��ʾ
    }

    public void OnLightSliderValueChanged(float value)
    {
        float brightness = value / 100f; // ������ֵת��Ϊ0��1֮��ĸ�����

        // ������Ⱦ����ɫ
        foreach (Renderer r in renderers)
        {
            if (r != null && r.material != null)
            {
                Color currentColor = r.material.color;
                Color newColor = new Color(
                    Mathf.Lerp(0, currentColor.r, brightness),
                    Mathf.Lerp(0, currentColor.g, brightness),
                    Mathf.Lerp(0, currentColor.b, brightness),
                    currentColor.a // ����͸���Ȳ���
                );
                r.material.color = newColor;
            }
        }

        // ������Դǿ��
        foreach (Light light in lights)
        {
            if (light != null)
            {
                float originalIntensity = light.intensity;
                light.intensity = Mathf.Lerp(0, originalIntensity, brightness);
            }
        }

        PlayerPrefs.SetFloat("Brightness", value); // ���浽���ƫ��
        UpdateNumbers(); // ������ֵ��ʾ
    }

    // ������ҳ��
    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}