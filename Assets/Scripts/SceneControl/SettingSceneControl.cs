using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneControl : MonoBehaviour
{
    public Slider musicSlider; // 音乐滑块
    public Text musicNumber; // 音乐数值显示
    public Slider lightSlider; // 亮度滑块
    public Text lightNumber; // 亮度数值显示

    private Renderer[] renderers; // 渲染器数组，用于调整亮度
    private Light[] lights; // 光源数组，用于调整光源强度

    void Start()
    {
        // 获取所有子对象的 Renderer 组件
        renderers = GetComponentsInChildren<Renderer>();

        // 获取场景中的所有光源
        lights = FindObjectsOfType<Light>();

        // 初始化滑块值
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 50f);
        lightSlider.value = PlayerPrefs.GetFloat("Brightness", 50f);

        // 更新数值显示
        UpdateNumbers();

        // 设置初始音量
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.ChangeVolume(musicSlider.value / 100f);
        }

        // 防止这个 GameObject 在场景切换时被销毁
        DontDestroyOnLoad(gameObject);
    }

    void UpdateNumbers()
    {
        musicNumber.text = musicSlider.value.ToString("0");
        lightNumber.text = lightSlider.value.ToString("0");
    }

    public void OnMusicSliderValueChanged(float value)
    {
        if (BackgroundMusicManager.Instance != null) // 确保音频源存在
        {
            BackgroundMusicManager.Instance.ChangeVolume(value / 100f); // 更新音频源音量
            PlayerPrefs.SetFloat("MusicVolume", value); // 保存到玩家偏好
        }
        UpdateNumbers(); // 更新数值显示
    }

    public void OnLightSliderValueChanged(float value)
    {
        float brightness = value / 100f; // 将滑块值转换为0到1之间的浮点数

        // 调整渲染器颜色
        foreach (Renderer r in renderers)
        {
            if (r != null && r.material != null)
            {
                Color currentColor = r.material.color;
                Color newColor = new Color(
                    Mathf.Lerp(0, currentColor.r, brightness),
                    Mathf.Lerp(0, currentColor.g, brightness),
                    Mathf.Lerp(0, currentColor.b, brightness),
                    currentColor.a // 保持透明度不变
                );
                r.material.color = newColor;
            }
        }

        // 调整光源强度
        foreach (Light light in lights)
        {
            if (light != null)
            {
                float originalIntensity = light.intensity;
                light.intensity = Mathf.Lerp(0, originalIntensity, brightness);
            }
        }

        PlayerPrefs.SetFloat("Brightness", value); // 保存到玩家偏好
        UpdateNumbers(); // 更新数值显示
    }

    // 返回主页面
    public void ReturnStartScene()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}