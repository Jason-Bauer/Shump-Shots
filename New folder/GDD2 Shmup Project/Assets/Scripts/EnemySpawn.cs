using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    private Vector3 spawnPos; // position that the enemy will spawn at
    private float xSpawnPos;
    private float ySpawnPos;
    private float zSpawnPos;

    public GameObject enemy; // enemy prefab to instantiate
    public bool spawn; // whether or not to spawn an enemy

    private float timerReset = 3.0f; // number to reset to
    public float minTimerReset = .8f;
    private float timer = 3.0f; // timer for spawn cooldowns
    private int enemyCount; // number of enemys to spawn in a wave before increasing spawn rate
    private float timeStep = 0.2f; // amount the spawn timer decreases by after each wave

    public GameManager gameMngr;

    private bool firstFrame; // first frame of the loop

    // Use this for initialization
    void Start ()
    {
        gameMngr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(gameMngr.GameplayState == GameManager.GameState.Play)
        {
            if(spawn)
            {
                timer -= Time.deltaTime; // update the timer

                // if it is past the time, spawn an an enemy and set the timer again, slightly increasing the spawn rate
                if (timer <= 0)
                {
                    timer = timerReset;
                    // increase the spawn rate after every ten enemies
                    if(enemyCount > 10)
                    {
                        firstFrame = true;
                        // only on the first frame
                        if(firstFrame)
                        {
                            // increase the level
                            gameMngr.level++;

                            // Increase level speed until it reaches 10 units/second
                            if (gameMngr.levelSpeed <= 10)
                            {
                                gameMngr.levelSpeed += .5f;
                            }
                            Debug.Log("Level in ESpawn: " + gameMngr.level);

                            // level can't go past level 4
                            if (gameMngr.level >= 3)
                            {
                                gameMngr.level = 3;
                            }

                            // reset the timer and decrease it by the timeStep to effectively increase spawn rate
                            timerReset = timerReset - timeStep;
                            timer = timer - timeStep;
                            enemyCount = 0;

                            // cap the minimum time in between spawns so that it doesn't flood the player
                            if (timerReset <= minTimerReset)
                            {
                                timer = minTimerReset;
                                timerReset = minTimerReset;
                            }
                            firstFrame = false;
                        }
                    }
                    // spawn an enemy and increment the enemy count
                    SpawnEnemy();
                    enemyCount++;
                }
            }
        }
    }

    // method that spawns an enemy
    void SpawnEnemy()
    {
        // build a spawn location for the enemy
        xSpawnPos = 10.0f;
        ySpawnPos = 0.0f;
        zSpawnPos = Random.Range(ScreenManager.Left + .2f, ScreenManager.Right - .2f);
        spawnPos = new Vector3(xSpawnPos, ySpawnPos, zSpawnPos);

        // Instantiate the enemy unit
        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}
