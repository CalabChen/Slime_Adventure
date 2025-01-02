using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherryCount = 0;
    private int atlasCount = 0;
    private int bananaCount = 0;
    [Header("×é¼þ")]
    public AudioSource collectSoundEffect;
    public Text CherryText;
    public Text AtlasText;
    public Text BananaText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherryCount++;
            if (CherryText != null)
            {
                CherryText.text = "Cherries:" + cherryCount;
            }
            int max_c = PlayerPrefs.GetInt("MaxCherries", 0);
            if(max_c < cherryCount)
            {
                PlayerPrefs.SetInt("MaxCherries", cherryCount);
            }
        }
        else if (collision.gameObject.CompareTag("Atlas"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            atlasCount++;
            if (AtlasText != null)
            {
                AtlasText.text = "Atlas:" + atlasCount;
            }
            int max_c = PlayerPrefs.GetInt("MaxAtlas", 0);
            if (max_c < atlasCount)
            {
                PlayerPrefs.SetInt("MaxAtlas", atlasCount);
            }
        }
        else if (collision.gameObject.CompareTag("Banana"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            bananaCount++;
            if (BananaText != null)
            {
                BananaText.text = "Bananas:" + bananaCount;
            }
            int max_c = PlayerPrefs.GetInt("MaxBananas", 0);
            if (max_c < bananaCount)
            {
                PlayerPrefs.SetInt("MaxBananas", bananaCount);
            }
        }
    }
}
