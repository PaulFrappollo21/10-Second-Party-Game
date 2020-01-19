using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;



    public float speed = 3;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bumper")
        {
            transform.Rotate(0f, 180f, 0f);
            speed = -3;
        }
        else if (collision.gameObject.tag == "Bumper1")
        {
            transform.Rotate(0f, 180f, 0f);
            speed = 3;
        }
    }
}