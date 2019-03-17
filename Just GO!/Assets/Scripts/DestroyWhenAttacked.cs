using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAttacked : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
        } else
        {
            Destroy(collision.gameObject);
        }
    }
}
