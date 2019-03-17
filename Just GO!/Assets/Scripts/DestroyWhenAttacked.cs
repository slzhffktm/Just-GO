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
            print("Enemy attacked");
            Destroy(gameObject);
        }
    }
}
