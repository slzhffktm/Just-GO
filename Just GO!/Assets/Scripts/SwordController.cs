using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    CharacterController controller;
    public float movementSpeed;
    private Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //controller.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = movementSpeed;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
}
