using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpossumController : MonoBehaviour
{
    public Transform player;
    private Animator enemyAnimator;
    private CharacterController controller;
    private Rigidbody2D enemyRigidBody;

    public float gravity;
    public float movementSpeed;
    public int maxDist;
    public int minDist;

    private Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = transform.GetComponent<Rigidbody2D>();
        enemyAnimator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        moveDirection.x = -movementSpeed;
        //transform.LookAt(player);
        //if (Vector3.Distance(transform.position, player.position) >= minDist)
        //{
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;

        //    if (Vector3.Distance(transform.position, player.position) <= maxDist)
        //    {
        //        // Shoot or something
        //    }
        //}
        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
}
