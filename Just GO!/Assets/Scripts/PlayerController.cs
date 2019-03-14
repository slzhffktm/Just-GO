using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;

    private Rigidbody2D playerRigidBody;
    private int jumpCount = 0;
    private bool isGrounded;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 1)
        {
            playerRigidBody.AddForce(Vector3.up * (jumpPower * playerRigidBody.mass * playerRigidBody.gravityScale * 20.0f));
            jumpCount++;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 0;
            Debug.Log(isGrounded + " " + jumpCount);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
