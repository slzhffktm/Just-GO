using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    public float jumpPower;
    public float movementSpeed;
    public float gravity;

    public float swordAttackRate;
    public float swordAttackDuration;
    public GameObject swordAttack;
    public Transform swordAttackSpawn;
    private float nextSwordAttack;

    private Rigidbody2D playerRigidBody;
    private int jumpCount = 0;
    private Animator playerAnimator;
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;

    private bool life = true;
    private bool attack = false;

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
        if (controller.isGrounded)
        {
            jumpCount = 0;
        }
        MovePlayer();
        playerCamera.transform.position = new Vector3(transform.position.x + 4, transform.position.y, playerCamera.transform.position.z);
    }

    private void MovePlayer()
    {
        attack = false;
        moveDirection.x = movementSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            moveDirection.y = jumpPower;
            jumpCount++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextSwordAttack)
        {
            nextSwordAttack = Time.time + swordAttackRate;
            Attack();
        }

        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        GameObject clone = Instantiate(swordAttack, swordAttackSpawn.position, swordAttackSpawn.rotation);
        //Destroy(clone, swordAttackDuration);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Enemy")
        {
            print("touched something OTHER than the ground");
            playerAnimator.SetTrigger("Die");
            movementSpeed = 0;
        }
        
        //// NOTE: bitwise NOT is the tild character: ~
        //CollisionFlags ignoreGround = ~CollisionFlags.Below;

        //CollisionFlags newCollisionFlags = controller.collisionFlags & ignoreGround;

        //if (newCollisionFlags != 0)
        //{
        //    print("touched something OTHER than the ground");
        //    playerAnimator.SetTrigger("Die");
        //    movementSpeed = 0;
        //}
    }
}
