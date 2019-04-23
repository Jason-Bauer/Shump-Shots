using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

// Starts the game when something collides with it
public class PlayButton : MonoBehaviour {

    // The trigger zone for the play button
    //private BoxCollider buttonCollider;
    private GameManager manager;

	// Use this for initialization
	void Start () {
        //buttonCollider = GetComponent<BoxCollider>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<BulletMove>() != null)
        {
            manager.GameplayState = GameManager.GameState.Play;
            Destroy(other.gameObject);
        }
    }
}
