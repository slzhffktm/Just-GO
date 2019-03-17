using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAttacked : MonoBehaviour
{
    public EnemyOpossumController enemy;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // NOTE: bitwise NOT is the tild character: ~
        CollisionFlags ignoreGround = ~CollisionFlags.Below;

        CollisionFlags newCollisionFlags = controller.collisionFlags & ignoreGround;

        if (newCollisionFlags != 0)
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Attack")
            {
                print("Enemy attacked");
                Destroy(gameObject);
            }
            else
            {
                enemy.movementSpeed = 0;
            }
        }
    }
}
