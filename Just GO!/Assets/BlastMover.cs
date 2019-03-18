using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastMover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        //if (player.position.x < transform.position.x)
        //{
        //    rb.velocity = -transform.right * speed;
        //}
        //else
        //{
        //    rb.velocity = transform.right * speed;
        //}

    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y - 2, 0), step);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("here");
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
