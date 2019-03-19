using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastMover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Transform player;

    private Vector3 movementVector = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        movementVector = (new Vector3(player.position.x+8, player.position.y, player.position.z) - transform.position).normalized * speed;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y - 8, 0), step);
        //transform.position += movementVector * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Ultimate" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
