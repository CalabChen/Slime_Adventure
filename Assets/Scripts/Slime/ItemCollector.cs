using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllControl;

public class ItemCollector : MonoBehaviour
{
    private int cherryCount = 0;
    [Header("×é¼þ")]
    public AudioSource collectSoundEffect;
    public Text CherryText;


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
            if(GameManager.Instance.cherries < cherryCount)
            GameManager.Instance.cherries = cherryCount;
        }
    }
}
