using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public Camera mainCamera;
    private float horizontalExtent; // width of screen / 2.0f
    //private float verticalExtent; // height of screen / 2.0f

    // Use this for initialization
    void Start () {
        horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameplayState = GameManager.GameState.GameOver;

            //Debug.Log("GAME OVER");
        }
    }

    // Update is called once per frame
    void Update () {
        if (!GameObject.FindGameObjectWithTag("PlayButton"))
        {
            transform.position += new Vector3(-0.025f, 0, 0);
        }
        if (transform.position.x <= -1.0f)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameplayState = GameManager.GameState.GameOver;
            /*
            if (transform.position.x <= -3.0f)
            {
                Destroy(gameObject);
            }*/
            
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        /*
        if (transform.position.z > horizontalExtent)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, horizontalExtent);
        }
        if (transform.position.z < -horizontalExtent)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -horizontalExtent);
        }
        */
    }
}
