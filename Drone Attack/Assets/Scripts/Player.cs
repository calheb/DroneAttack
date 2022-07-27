using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    public GameObject deathMenuUI;
    public LayerMask enemyLayer;
    private Animator player_Animator;
    public Rigidbody2D player_RB2D;
    private GroundChecker groundSensor;
    private bool isGrounded = false;
    private bool playerIdle = false;
    public Animator DroneAnimator;
    public Rigidbody2D droneRB2D;
    public Collider2D droneCollider;
    public AudioSource audioSource;
    public bool isDead = false;
    public Collider2D playerCollider;

    
    [SerializeField]
    TextMeshProUGUI scoreText;

    public AudioSource gemAudio;
    public Collider2D gemCollider;
    public Rigidbody2D gemRB2D;
    public GameObject gemClone;

    // Use this for initialization
    void Start()
    {
        player_Animator = GetComponent<Animator>();
        player_RB2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        groundSensor = transform.Find("GroundSensor").GetComponent<GroundChecker>();
        scoreText.text = "Score: " + Scoring.CurrentScore;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "Enemy")
        {
            Debug.Log("WE LOST");
            Scoring.CurrentScore = 0;
            player_RB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            droneRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
            player_Animator.SetBool("isDead", true);
            player_Animator.SetInteger("AnimState", 4);
            player_Animator.Play("player_death");
            DroneAnimator.Play("drone_death");
            audioSource.Play();
            StartCoroutine(waiter());
        }

        //else if (col.collider.tag == "Gem")
        //{
        //    gemAudio.Play();
        //    Scoring.CurrentScore += 1;
        //    scoreText.text = "Score: " + Scoring.CurrentScore;
        //    gemCollider.enabled = false;
        //    gemClone.SetActive(false);
        //    gemClone.GetComponent<Renderer>().enabled = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            gemAudio.Play();
            Scoring.CurrentScore += 1;
            scoreText.text = "Score: " + Scoring.CurrentScore;

        }
    }
    void Update()
    {
        //Check if character just landed on the ground
        if (!isGrounded && groundSensor.State())
        {
            isGrounded = true;
            player_Animator.SetBool("Grounded", isGrounded);
        }

        //Check if character just started falling
        if (isGrounded && !groundSensor.State())
        {
            isGrounded = false;
            player_Animator.SetBool("Grounded", isGrounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        player_RB2D.velocity = new Vector2(inputX * m_speed, player_RB2D.velocity.y);

        //Set AirSpeed in animator
        player_Animator.SetFloat("AirSpeed", player_RB2D.velocity.y);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player_Animator.SetInteger("AnimState", 5);
            player_Animator.SetBool("teleporting", true);
        }

        //Jump
        if (Input.GetKeyDown("space") && isGrounded)
        {
            player_Animator.SetInteger("AnimState", 3);
            isGrounded = false;
            player_Animator.SetBool("Grounded", isGrounded);
            player_RB2D.velocity = new Vector2(player_RB2D.velocity.x, m_jumpForce);
            groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            player_Animator.SetInteger("AnimState", 2);


        else if (playerIdle)
            player_Animator.SetInteger("AnimState", 1);

        //Idle
        else if (Mathf.Abs(inputX) <= Mathf.Epsilon)
            player_Animator.SetInteger("AnimState", 0);

        if (player_RB2D.position.y < -1.5 || player_RB2D.position.y > 5)
        {
            deathMenuUI.SetActive(true);
        }
    }
    IEnumerator waiter()
    {
        //Debug.Log("Waiting!!!");
        yield return new WaitForSeconds(0.50f);
        droneCollider.enabled = false; //disable hit box
        playerCollider.enabled = false;
        if ((player_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) && DroneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            Debug.Log("not playing");
        else
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            deathMenuUI.SetActive(true);

    }

    
}
