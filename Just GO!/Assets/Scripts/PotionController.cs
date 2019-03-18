using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        moveDirection.y -= 50 * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Fireball")
        {
            Physics.IgnoreCollision(controller, hit.collider);
        }
    }
}
