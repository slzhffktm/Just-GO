﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    public float jumpPower;
    public float movementSpeed;
    public float gravity;

    public float swordAttackRate;
    public float swordAttackDuration;
    public GameObject swordAttack;
    public Transform swordAttackSpawn;
    private float nextSwordAttack;

    public float FireballRate;
    public float FireballDuration;
    public GameObject Fireball;
    public Transform FireballSpawn;
    private float nextFireball;

    public float UltimateRate;
    public float UltimateDuration;
    public GameObject Ultimate;
    public Transform UltimateSpawn;
    private float nextUltimate;

    private Rigidbody2D playerRigidBody;
    private int jumpCount = 0;
    private Animator playerAnimator;
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;

    private bool hasFireball;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        hasFireball = false;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
        }
        MovePlayer();
        playerCamera.transform.position = new Vector3(transform.position.x + 4, transform.position.y, playerCamera.transform.position.z);
    }

    private void MovePlayer()
    {
        moveDirection.x = movementSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            moveDirection.y = jumpPower;
            jumpCount++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextSwordAttack)
        {
            nextSwordAttack = Time.time + swordAttackRate;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > nextFireball)
        {
            nextFireball = Time.time + FireballRate;
            FireballAttack();
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextUltimate)
        {
            moveDirection.y -= gravity * Time.smoothDeltaTime;
            nextUltimate = Time.time + UltimateRate;
            UltimateAttack();
        }

        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        GameObject clone = Instantiate(swordAttack, swordAttackSpawn.position, swordAttackSpawn.rotation);
        Destroy(clone, swordAttackDuration);
    }

    private void FireballAttack()
    {
        playerAnimator.SetTrigger("Fireball");
        GameObject clone = Instantiate(Fireball, FireballSpawn.position, FireballSpawn.rotation);
        Destroy(clone, FireballDuration);
    }

    private void UltimateAttack()
    {
        playerAnimator.SetTrigger("Ultimate");
        GameObject clone = Instantiate(Ultimate, UltimateSpawn.position, UltimateSpawn.rotation);
        Destroy(clone, UltimateDuration);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "FireballPotion")
        {
            hasFireball = true;
        }
    }
}
