using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;

    public LayerMask groundPlayer;
    public bool onTheGround;
    public int jumpHeight;

    public Animator hurting;
    public bool hurtAnim;
    public Animator running;
    public bool right;

    public int healthCount;
    public GameObject gameOverScene;
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public bool gameIsOver;

    public GameObject gameWinScene;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthCount = 3;
    }

    void Update()
    {
        Vector2 direction = Vector2.down;
        Vector2 position = transform.position;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundPlayer);
        onTheGround = hit.collider != null;

        if (Input.GetKey(KeyCode.RightArrow) && !hurtAnim && !gameIsOver)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            running.Play("running");
            right = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !hurtAnim && !gameIsOver)
        {
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
            running.Play("running");
            right = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && onTheGround && !gameIsOver)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }

        h1.SetActive(healthCount >= 1);
        h2.SetActive(healthCount >= 2);
        h3.SetActive(healthCount >= 3);

        if (healthCount <= 0)
        {
            gameIsOver = true;
            gameOverScene.SetActive(true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            healthCount--;
            hurting.Play("hurt");

            if (right)
            {
                transform.position += Vector3.left * 100 * Time.deltaTime;
                transform.position += Vector3.up * 100 * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * 100 * Time.deltaTime;
                transform.position += Vector3.up * 100 * Time.deltaTime;
            }
        }

        if (collision.gameObject.CompareTag("winL1"))
        {
            gameIsOver = true;
            gameWinScene.SetActive(true);

            if (PlayerPrefs.GetInt("levelsWon") == 0)
            {
                PlayerPrefs.SetInt("levelsWon", 1);
                PlayerPrefs.Save();
            }
        }

        if (collision.gameObject.CompareTag("winL2"))
        {
            gameIsOver = true;
            gameWinScene.SetActive(true);

            if (PlayerPrefs.GetInt("levelsWon") == 1)
            {
                PlayerPrefs.SetInt("levelsWon", 2);
                PlayerPrefs.Save(); 
            }
        }

        if (collision.gameObject.CompareTag("instantGameOver"))
        {
            healthCount = 0;
        }

        if (collision.gameObject.CompareTag("heart") && healthCount < 3)
        {
            healthCount++;
        }
    }
}
