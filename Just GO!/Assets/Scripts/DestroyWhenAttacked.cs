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
        if (hit.collider.gameObject.tag == "Attack")
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Attack")
            {
                Destroy(gameObject);
                Destroy(hit.gameObject);
            }
            else if (hit.collider.gameObject.tag == "Ultimate")
            {
                Destroy(gameObject);
            }
        }
    }
}
