using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float playerMoveSpeed = 10f;
    public Rigidbody2D playerRB;
    public Collider2D playerColl;
    public Animator playerAnim;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerColl = GetComponent<Collider2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
    }

   void PlayerMove()
    {
        float horizontalNum = Input.GetAxis("Horizontal");
        float faceNum = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalNum, playerRB.velocity.y);
        playerAnim.SetFloat("move", Mathf.Abs(playerMoveSpeed * horizontalNum));

        if (faceNum != 0)
        {
            transform.localScale = new Vector3(faceNum * 6, transform.localScale.y, transform.localScale.z);
        }
    }
}