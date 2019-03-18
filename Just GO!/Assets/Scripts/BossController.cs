using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private CharacterController controller;
    public float gravity;
    public float movementSpeed;
    private Vector2 moveDirection = Vector2.zero;

    public float attackWait;
    public float blastWait;

    private int life = 100;

    public float minDist;
    public float touched;
    private Transform player;
    static private float originDistance;
    private bool fallBack = false;

    public GameObject Blast;
    public Transform BlastSpawn;
    private float nextBlast = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
        originDistance = Vector3.Distance(transform.position, player.position);
    }

    // Update is called once per frame
    void Update()
    {
        print(life);
        if (life > 0)
        {
            StartCoroutine(AttackBeak());
        } else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AttackBeak()
    {
        MoveBoss();
        yield return new WaitForSeconds(attackWait);

        if (!fallBack)
        {
            if (transform.position.x > player.position.x)
            {
                moveDirection.x = -movementSpeed*1.4f;
            } else
            {
                moveDirection.x = movementSpeed;
            }

            if (transform.position.y > player.position.y)
            {
                moveDirection.y -= 1.25f *gravity * Time.smoothDeltaTime;
            }
            controller.Move(moveDirection * Time.smoothDeltaTime);
        } else
        {
            MoveToOrigin();
            yield return new WaitForSeconds(4);
            if (Time.time > nextBlast)
            {
                AttackBeam();
                nextBlast = Time.time + blastWait;
            }
            fallBack = false;
        }
        
        //Debug.Log(Vector3.Distance(transform.position, player.position));
    }

    void MoveBoss()
    {
        moveDirection.x = movementSpeed;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    void MoveToOrigin()
    {
        moveDirection.x = movementSpeed * 1.4f;
        if (transform.position.y <= 3)
        {
            moveDirection.y +=  1.3f * gravity * Time.smoothDeltaTime;
        }
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }

    void AttackBeam()
    {
        GameObject clone = Instantiate(Blast, BlastSpawn.position, BlastSpawn.rotation);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.gameObject.tag);
        if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Sword" || hit.collider.gameObject.tag == "Fireball" || hit.collider.gameObject.tag == "Ultimate")
        {
            fallBack = true;
            life -= AttackedScore(hit);
        } else
        {

        }
    }

    private int AttackedScore(ControllerColliderHit hit)
    {
        int minusHP = 0;
        if (hit.collider.gameObject.tag == "Sword")
        {
            minusHP = 10;
        } else if (hit.collider.gameObject.tag == "Fireball")
        {
            Destroy(hit.collider.gameObject);
            minusHP = 5;
        } else if (hit.collider.gameObject.tag == "Ultimate")
        {
            minusHP = 20;
        }

        return minusHP;
    }
}
