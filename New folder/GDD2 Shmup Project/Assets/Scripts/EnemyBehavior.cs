using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public int health = 1;
    public Material red; // colors
    public Material green;
    public Material blue;
    public Material yellow;
    private float randomColorNum; // float for RNG part of color
    private int colorNum; // number to use to pick a color on instantiation

    // rotation vector
    private Vector3 spawnRot;

    // get the enemy manager
    public GameObject enemyMngr;
    private EnemySpawn eSpawn;


    // Use this for initialization
    void Start()
    {
        // position and rotation for spawn
        spawnRot = new Vector3(0.0f,0.0f,-90.0f);
        gameObject.transform.Rotate(spawnRot);

        // reference to the spawn manager
        eSpawn = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawn>();

        // generate a random number from 1 to 4
        //randomColorNum = Random.Range(1.0f, 5.0f);
        randomColorNum = Random.Range(0.0f, Mathf.Min(eSpawn.gameMngr.level, 3.0f));

        // truncate the float into an int so that we can use it in the switch
        colorNum = (int)randomColorNum;

        //Debug.Log("Level " + eSpawn.gameMngr.level);
        Debug.Log("Color Num " + colorNum);
        //Debug.Log("randColorNum " + randomColorNum);
        // switch for colors

        switch (colorNum)
        {
            case (int)GameColor.red:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                //this.gameObject.GetComponent<Renderer>().material = red;
                break;

            case (int)GameColor.yellow:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                //this.gameObject.GetComponent<Renderer>().material = yellow;
                break;

            case (int)GameColor.green:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                //this.gameObject.GetComponent<Renderer>().material = green;
                break;

            case (int)GameColor.blue:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                //this.gameObject.GetComponent<Renderer>().material = blue;
                break;

            default:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                //this.gameObject.GetComponent<Renderer>().material = red;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // check for destruction
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // keep the y value in a place where the unit can be shot
        else if(gameObject.transform.position.y > 0.01 || gameObject.transform.position.y < -0.01)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z);
        }
	}
}
