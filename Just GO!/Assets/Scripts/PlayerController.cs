using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    public float jumpPower;
    public float movementSpeed;
    public float gravity;

    private Rigidbody2D playerRigidBody;
    private int jumpCount = 0;
    private Animator playerAnimator;
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;

    private bool life = true;

    Collider mCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (true)
        {
            if (controller.isGrounded)
            {
                jumpCount = 0;
            }
            MovePlayer();
            playerCamera.transform.position = new Vector3(transform.position.x + 4, transform.position.y, playerCamera.transform.position.z);
        }
    }

    private void MovePlayer()
    {
        moveDirection.x = movementSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            moveDirection.y = jumpPower;
            jumpCount++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // NOTE: bitwise NOT is the tild character: ~
        CollisionFlags ignoreGround = ~CollisionFlags.Below;

        CollisionFlags newCollisionFlags = controller.collisionFlags & ignoreGround;

        if (newCollisionFlags != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(hit.collider.gameObject);
            }
            Destroy(gameObject);
            print("touched something OTHER than the ground");
        }
    }
}
