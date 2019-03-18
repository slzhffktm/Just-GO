using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Vector2 lastPosition;
    public Quaternion lastRotation;
    public Vector2 lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        lastPosition = transform.position;
        lastVelocity = rb.velocity;
        lastRotation = transform.rotation;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("kena sesuatu");
        if (hit.collider.gameObject.tag == "FireballPotion" || hit.collider.gameObject.tag == "Ultimate")
        {
            print("kena potion");
            Physics.IgnoreCollision(GetComponent<Collider>(), hit.collider);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("kena sesuatu");
        if (collision.gameObject.tag == "FireballPotion")
        {
            print("kena potion");
            transform.position = lastPosition;
            transform.rotation = lastRotation;
            rb.velocity = lastVelocity;
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        } else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
}

