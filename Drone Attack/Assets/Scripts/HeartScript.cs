using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HeartScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    public float heartMinSpeed = 2f;
    public float heartMaxSpeed = 4f;
    public float heartSpeed;

    public Collider2D heartCollider;
    public GameObject heartClone;
    public Rigidbody2D heartRB2D;
    public Rigidbody2D playerRB2D;

    public GameObject deathMenuUI;

    private void Start()
    {
        heartRB2D = GetComponent<Rigidbody2D>();
        heartSpeed = Random.Range(heartMinSpeed, heartMaxSpeed);
    }

    void FixedUpdate()
    {
        heartRB2D.MovePosition(heartRB2D.position + Vector2.down * Time.fixedDeltaTime * heartSpeed);

        //if (heartRB2D.position.y <= 1.95)
        //{
        //    heartRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        //    StartCoroutine(waiter());
        //}

        //IEnumerator waiter()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    heartRB2D.constraints = RigidbodyConstraints2D.None;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !deathMenuUI.activeSelf)
        {
            heartRB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

}