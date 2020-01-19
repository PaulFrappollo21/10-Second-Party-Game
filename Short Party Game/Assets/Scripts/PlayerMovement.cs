using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float timeLeft = 12.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 


    public float runSpeed = 40f;
    public Text countText;
    public Text livesText;
    public Text winText;
    public Text loseText;
    public AudioSource coin;
    public AudioSource jumping;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource hit;
    public AudioSource timeisup;
    public int restartlevel;
    private bool restart;
    private bool gameOver;
    public Text restartText;
    Rigidbody2D m_rigidbody;
    public BoxCollider2D collider;
    


    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private int count;
    private int lives;




    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        lives = 1;
        SetCountText();
        SetLivesText();
        gameOver = false;
        restart = false;
        restartText.text = "";
        m_rigidbody = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            jumping.Play();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(restartlevel);
            }
        }
        if (gameOver)
        {
            restartText.text = "Press 'R' to Restart";
            restart = true;
        }
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft <= 0)
        {
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            loseText.text = "You Lose! Game created by Paul Frappollo";
            timeLeft = 0;
            restart = true;
            gameOver = true;
            collider.enabled = false;
            timeisup.Play();
        }

        else if (count >= 5)
        {
            timeLeft = 10;
        }

       



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
            hit.Play();
        }

    }

    void SetCountText()
    {
      

        countText.text = "Score: " + count.ToString();
        if (count >= 5)
        {
            winText.text = "You Win! Game created by Paul Frappollo";
            win.Play();
            startText.text = "10";
            restart = true;
            gameOver = true;
            collider.enabled = false;
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;


        }
        


    }


    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            collision.gameObject.SetActive(false);
            coin.Play();
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            loseText.text = "You Lose! Game created by Paul Frappollo";
            lose.Play();
            restart = true;
            gameOver = true;
            collider.enabled = false;
            timeLeft = 0;
        }

    }

    public void OnLanding()

    {
        animator.SetBool("IsJumping", false);
        jump = false;

    }

    public void OnCrouching(bool isCrouching)

    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }
}
