using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeLife : MonoBehaviour
{
    [Header("���")]
    public Rigidbody2D playerRB; // ��Ҹ������
    public Animator playerAnim;  // ��Ҷ���������
    public AudioSource deathSoundEffect;
   
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            Die();
        }
    }

    private void Die()
    {
        playerRB.bodyType = RigidbodyType2D.Static;
        playerAnim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
