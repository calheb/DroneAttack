using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GemScript : MonoBehaviour
{
    public Animator gem_animator;
    public Rigidbody2D gem_rb2d;

    public float gemMinSpeed = 2f;
    public float gemMaxSpeed = 4f;
    public float gemSpeed;

    public AudioSource gemAudio;
    public Collider2D gemCollider;
    public Rigidbody2D gemRB2D;
    public GameObject gemClone;

    [SerializeField]
    TextMeshProUGUI scoreText;


    private void Start()
    {
        gem_animator = GetComponent<Animator>();
        gem_rb2d = GetComponent<Rigidbody2D>();
        gemSpeed = Random.Range(gemMinSpeed, gemMaxSpeed);
    }
    void FixedUpdate()
    {
        gem_rb2d.MovePosition(gem_rb2d.position + Vector2.down * Time.fixedDeltaTime * gemSpeed);

        if (gem_rb2d.position.y <= 2.10)
        {
            gem_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(waiter());
        }


        IEnumerator waiter()
        {
            yield return new WaitForSeconds(0.5f);
            gem_rb2d.constraints = RigidbodyConstraints2D.None;
            //GetComponent<Collider2D>().enabled = false; //disable hit box
        }
    }

    //private void OnCollisionStay2D(Collision2D col)
    //{
    //    if (col.collider.tag == "Player")
    //    {
    //        gemAudio.Play();
    //        Scoring.CurrentScore += 1;
    //        scoreText.text = "Score: " + Scoring.CurrentScore;
    //        gemCollider.enabled = false;
    //        //Destroy(gemClone);
    //    }
    //}

}