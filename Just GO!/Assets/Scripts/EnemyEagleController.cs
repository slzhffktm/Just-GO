using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagleController : MonoBehaviour
{
    private CharacterController controller;
    public float gravity;
    public float movementSpeed;
    public int maxDist;
    public int minDist;
    private Transform player;
    private Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEagle();
    }

    private void MoveEagle()
    {
        moveDirection.x = -movementSpeed;
        if (Vector3.Distance(transform.position, player.position) < minDist)
        {
            moveDirection.y -= gravity * Time.smoothDeltaTime;
        }
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
}
