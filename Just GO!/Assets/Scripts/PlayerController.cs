using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool hasFireball;
    public float FireballRate;
    public float FireballDuration;
    public GameObject Fireball;
    public Transform FireballSpawn;
    private float nextFireball;

    public float UltimateRate;
    public float UltimateDuration;
    public GameObject Ultimate;
    public Transform UltimateSpawn;
    public Image imageCooldown;
    private bool isUltimateCooldown;
    private float endOfUltimate;

    private Rigidbody2D playerRigidBody;
    private int jumpCount = 0;
    private Animator playerAnimator;
    private CharacterController controller;
    private Vector2 moveDirection = Vector2.zero;
    
    public Transform sword;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
        hasFireball = false;
        isUltimateCooldown = true;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
        }
        if (isUltimateCooldown)
        {
            imageCooldown.fillAmount -= 1 / UltimateRate * Time.deltaTime;
            if (imageCooldown.fillAmount <= 0)
            {
                isUltimateCooldown = false;
            }
        }
        MovePlayer();
        playerCamera.transform.position = new Vector3(transform.position.x + 4, transform.position.y, playerCamera.transform.position.z);
    }

    private void MovePlayer()
    {
        if (Time.time > endOfUltimate)
        {
            movementSpeed = 2;
        }
        moveDirection.x = movementSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            moveDirection.y = jumpPower;
            jumpCount++;
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextSwordAttack)
        {
            nextSwordAttack = Time.time + swordAttackRate;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q) && hasFireball && Time.time > nextFireball)
        {
            nextFireball = Time.time + FireballRate;
            FireballAttack();
        }
        if (Input.GetKeyDown(KeyCode.E) && !isUltimateCooldown)
        {
            movementSpeed = 0;
            moveDirection.y -= gravity * Time.smoothDeltaTime;
            isUltimateCooldown = true;
            imageCooldown.fillAmount = 1;
            UltimateAttack();
            endOfUltimate = Time.time + UltimateDuration;
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

    void FireballAttack()
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
        if (hit.collider.tag == "Enemy")
        {
            print("touched something OTHER than the ground");
            playerAnimator.SetTrigger("Die");
            movementSpeed = 0;
        }

        if (hit.collider.gameObject.tag == "FireballPotion")
        {
            hasFireball = true;
            Destroy(hit.gameObject);
        }
    }
}
