using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // 确保只有一个实例存在，并且它不会在加载新场景时被销毁
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 销毁重复的实例
        }

        // 设置音频源（如果需要）
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            // 配置 AudioSource 参数，例如音量、循环等
            //audioSource.loop = true;
            //audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 100f);
        }
    }
}
