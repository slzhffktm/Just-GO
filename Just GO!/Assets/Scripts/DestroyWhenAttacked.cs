using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAttacked : MonoBehaviour
{
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.collider.gameObject.tag == "Sword")
        {
            print(hit.collider.gameObject.tag);
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "Fireball")
        {
            print(hit.collider.gameObject.tag);
            Destroy(gameObject);
            Destroy(hit.gameObject);
        }
        else if (hit.collider.gameObject.tag == "Ultimate")
        {
            print(hit.collider.gameObject.tag);
            Destroy(gameObject);
        }
        else if (hit.collider.gameObject.tag == "FireballPotion" )
        {
            Physics.IgnoreCollision(controller, hit.collider);
        }
    }
}
