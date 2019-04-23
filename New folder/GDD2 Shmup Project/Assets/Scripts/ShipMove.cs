using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

    private BoxCollider playerCollider;

	// Use this for initialization
	void Start () {
        playerCollider = gameObject.GetComponent<BoxCollider>();
	}

    // Update is called once per frame
    void Update () {
        Vector3 position = transform.position;
        /*float x = -Input.GetAxis("X Axis");
        position.z += x;*/
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            position.z -= 4f * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            position.z += 4f * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            position.x += 6.5f * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            position.x -= 6.5f * Time.deltaTime;
            transform.position = position;
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);


    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ENEMY")
        {
            Debug.Log("COLLISION");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameplayState = GameManager.GameState.GameOver;

            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }
    }
}
