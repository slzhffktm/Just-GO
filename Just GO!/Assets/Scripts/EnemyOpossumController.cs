using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpossumController : MonoBehaviour
{
    public Transform player;
    private Animator enemyAnimator;
    private CharacterController controller;
    private Rigidbody2D enemyRigidBody;
    public int moveSpeed;
    public float movementSpeed;
    public int maxDist;
    public int minDist;

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
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= minDist)
        {
            controller.position += controller.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= maxDist)
            {
                // Shoot or something
            }
        }
    }

    private void MoveEnemy()
    {

    }
}
