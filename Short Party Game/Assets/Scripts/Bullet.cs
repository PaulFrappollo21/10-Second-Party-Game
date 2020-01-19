using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 8f;
    public Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            Destroy(gameObject);
        }

        else if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameBorder")
        {
            Destroy(gameObject);
        }
    }
}