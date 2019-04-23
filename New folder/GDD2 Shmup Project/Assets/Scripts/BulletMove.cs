using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMove : MonoBehaviour {

    // TEMPORARY VARIABLES
    public static int score = 0;

    public GameObject playButton;
    public GameObject player;
    public GameManager gameManager;

    private GameColor bulletColor;
    public float bulletLifetime = 1.5f;
    public float bulletSpeed = 15f;

    private bool firstFrame;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // Set the bullet lifetime based on its color
        switch(bulletColor)
        {
            case GameColor.red:
                break;

            case GameColor.yellow:
                break;

            case GameColor.green:
                break;

            case GameColor.blue:
                break;

            default:
                bulletLifetime = .5f;
                break;
        }
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.x > ScreenManager.Top + .2f)
        {
            Destroy(gameObject);
        }

        firstFrame = true;
        if (gameObject.tag == "Boolet")
        {
            transform.position += new Vector3(bulletSpeed, 0, 0);
            Debug.Log(bulletSpeed);
            if (transform.position.y > 0.01 || transform.position.y < -.01)
            {
                transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            }
        }
        if (gameObject.tag == "Bullet2")
        {
            transform.position += new Vector3(bulletSpeed, 0, 0);
        }
        if (gameObject.tag == "Bullet2left")
        {
            transform.position += new Vector3(bulletSpeed, 0, .1f);
        }
        if (gameObject.tag == "Bullet2right")
        {
            transform.position += new Vector3(bulletSpeed, 0, -0.1f);
        }
        if (gameObject.tag == "Bullet3left")
        {
            transform.position += new Vector3(0, 0, .05f);
            transform.Rotate(Vector3.right * Time.deltaTime);
        }
        if (gameObject.tag == "Bullet3right")
        {
            transform.position += new Vector3(0, 0, -0.05f);
            transform.Rotate(Vector3.left * Time.deltaTime);
        }
        if (gameObject.tag == "Shield")
        {
            transform.position = (GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.2f, 0, 0));
        }
        if (gameObject.tag == "Explosion")
        {
            transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
        }
    }

    // collisions
    /*void OnCollisionEnter()
    {
        this.GetComponent<Collider>().
        Debug.Log("Hit " + gameObject.tag);
        Destroy(this.gameObject);
    }*/

    void OnCollisionEnter(Collision collision)
    {
        // If the bullet enters the play button from the start screen
        if (collision.gameObject.tag == "PlayButton")
        {
            //Debug.Log("Collision with PlayButton");
            //screenMngr.GetComponent<ScreenChange>().toPlay = 1;

            // Disable the play button & set the screen state to play mode
            gameManager.GameplayState = GameManager.GameState.Play;

            // Delete the bullet that collided with the button
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ENEMY")
        {
            // only destroy the enemy if the bullet is the same color
            if(gameObject.GetComponent<Renderer>().material.color == collision.gameObject.GetComponent<Renderer>().material.color)
            {
                Destroy(collision.gameObject);
                if (gameObject.tag != "Shield" && gameObject.tag != "Explosion")
                {
                    Destroy(gameObject);
                    if (firstFrame)
                    {
                        firstFrame = false;
                        score++;
                        gameManager.playScore.text = "Score: " + score;
                    }
                }                
                //Debug.Log("SCORE: " + score);
            }
        }
    }
}
