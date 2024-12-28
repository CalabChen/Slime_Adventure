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
        // ȷ��ֻ��һ��ʵ�����ڣ������������ڼ����³���ʱ������
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �����ظ���ʵ��
        }

        // ������ƵԴ�������Ҫ��
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            // ���� AudioSource ����������������ѭ����
            //audioSource.loop = true;
            //audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 100f);
        }
    }
}
