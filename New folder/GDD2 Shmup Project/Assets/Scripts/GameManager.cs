using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Manages functions of gameplay
/// </summary>
public class GameManager : MonoBehaviour {

    /// <summary>
    /// Represents the state of the gameplay
    /// </summary>
    public enum GameState {Start, Play, GameOver};

    // INSTANCES
    #region INSTANCES
    private GameState gameState = GameState.Start;
    
    /// <summary>An empty Transform that has children which represent elements of the start window</summary>
    public GameObject startScreen;
    /// <summary>An empty Transform that has children which represent elements of the play window</summary>
    public GameObject playScreen;
    /// <summary>An empty Transform that has children which represent elements of the game over window</summary>
    public GameObject gameOverScreen;
    /// <summary>The text for the playtime score</summary>
    public Text playScore;
    /// <summary>The text for the game over score</summary>
    public Text gameOverScore;

    /// <summary>TEMPORARY VARIABLE: Only enable enemies during gameplay</summary>
    public GameObject enemies;


    // PLAYER COMPONENTS
    public GameObject mainCamera;   // The main camera of the scene
    public GameObject player;       // The player themself

    // BACKGROUNDS
    public GameObject startingBackground;       // The background that the game starts with
    public GameObject backgroundPrefab;         // Prefabs for background objects
    public Material[] levelBackgrounds;         // Backgrounds used for levels
    //public Material[] transitionBackgrounds;    // T
    public Material memeBackground;             // Background for the secret meme level

    private GameObject backgrounds;             // Empty game object containing the backgrounds in the scene
    private List<GameObject> activeBackgrounds; // The backgrounds currently existing in the scene
    private Vector3 centerLocation;             // The location around which backgrounds will be made
    private Quaternion spawnRotation;           // The rotation to spawn a background with
    private Vector3 backgroundSpawnDisplace;    // The distance between where new background objects will spawn
    private Vector3 backgroundDespawnLocation;  // The location where background objects will despawn
    
    // LEVELS
    public int level = 0;           // The level the player is currently on
    public float levelSpeed = 1f;   // The speed of the scrolling background in units/sec
    #endregion


    // PROPERTIES
    /// <summary>
    /// Get / set the state of the screen
    /// </summary>
    public GameState GameplayState
    {
        get { return gameState; }
        set { SetGameState(value); }
    }


    // METHODS
	// Use this for initialization
	void Start () {
        // Disable the cursor on start
        Cursor.visible = false;
        
        // Setup default instances depending on game state
        if (gameState != GameState.Play)
        {
            //mainCamera.GetComponent<CameraMove>().enabled = false;
            playScreen.SetActive(false);
            level = 0;
        }

        if (gameState != GameState.GameOver)
        {
            gameOverScreen.SetActive(false);
        }

        //else
        //{
            //mainCamera.GetComponent<CameraMove>().enabled = true;
        //}


        #region BACKGROUND INSTANTIATION
        // Assign the center location around which the game will take place
        centerLocation = startingBackground.transform.position;
        spawnRotation = startingBackground.transform.rotation;

        backgroundSpawnDisplace = new Vector3(startingBackground.transform.localScale.y, 0f, 0f);
        backgroundDespawnLocation = centerLocation
            - new Vector3(startingBackground.transform.localScale.y, 0f, 0f);


        // Add the starting background to the list of active backgrounds & create a new background
        backgrounds = new GameObject();
        backgrounds.name = "Backgrounds";

        activeBackgrounds = new List<GameObject>();
        activeBackgrounds.Add(startingBackground);
        activeBackgrounds.Add(
            Instantiate(
                startingBackground,
                centerLocation + backgroundSpawnDisplace,
                spawnRotation
            ));
        activeBackgrounds[activeBackgrounds.Count].GetComponent<Renderer>().material = levelBackgrounds[0];

        for (int i = 0; i < activeBackgrounds.Count; i++)
        {
            activeBackgrounds[i].transform.SetParent(backgrounds.transform);
        }
        #endregion
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameState == GameState.Play)
        {
            UpdateBackground();
        }
    }


    /// <summary>
    /// Helper method to determine what to do when switching between game states
    /// </summary>
    /// <param name="state">The new state of the screen</param>
    private void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                // If switching from Game Over to Start...
                if (gameState == GameState.GameOver)
                {
                    // Hide everything from the Game Over screen
                    gameOverScreen.SetActive(false);
                }

                if (gameState == GameState.Play)
                {
                    // Hide the Play screen
                    playScreen.SetActive(false);
                }

                // Don't allow the camera to move
                //mainCamera.GetComponent<CameraMove>().enabled = false;

                // Enable player movement and shooting and collisions
                player.GetComponent<ShipMove>().enabled = true;
                player.GetComponent<ShipShoot>().enabled = true;
                player.GetComponent<ShipColor>().enabled = true;
                player.GetComponent<Collider>().enabled = true;

                // Enable the components of the start screen
                startScreen.SetActive(true);
                break;

                
            case GameState.Play:
                // If switching from Start state to Play state...
                if (gameState == GameState.Start)
                {
                    // Hide everything from the Start screen
                    startScreen.SetActive(false);
                }

                // If switching from Game Over to Play (which should not happen)...
                if (gameState == GameState.GameOver)
                {
                    gameOverScreen.SetActive(false);
                }

                // Enable the play screen
                playScreen.SetActive(true);

                // Enter the main game state & enable camera movement
                gameState = GameState.Play;
                //mainCamera.GetComponent<CameraMove>().enabled = true;

                // Enable player collisions
                player.GetComponent<Collider>().enabled = true;

                // TEMPORARY - enable enemies
                GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawn>().spawn = true;
                enemies.SetActive(true);
                break;
            
                
            case GameState.GameOver:
                // The game should not transition from start to game over, but should it ever...
                if (gameState == GameState.Start)
                {
                    // Disable components of the Start screen
                    startScreen.SetActive(false);
                }

                // If entering Game Over from gameplay
                if (gameState == GameState.Play)
                {
                    // Disable camera scrolling
                    //mainCamera.GetComponent<CameraMove>().enabled = false;
                    playScreen.SetActive(false);
                }
                

                // disable enemies
                GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawn>().spawn = false;

                // Show the Game Over screen
                gameOverScreen.SetActive(true);

                // Show the final score
                gameOverScore.text = "Final Score: " + BulletMove.score;
                
                // Disable the ship during GameOver screen
                player.GetComponent<ShipMove>().enabled = false;
                player.GetComponent<ShipShoot>().enabled = false;
                player.GetComponent<ShipColor>().enabled = false;
                player.GetComponent<Collider>().enabled = false;

                // Enable the cursor so the button can be selected
                Cursor.visible = true;
                break;
        }
    }


    /// <summary>
    /// Scroll the background based upon the current level
    /// </summary>
    void UpdateBackground()
    {
        // Move each active background object backwards to simulate screen movement
        for (int i = 0; i < activeBackgrounds.Count; i++)
        {
            activeBackgrounds[i].transform.position -= Vector3.right * levelSpeed * Time.deltaTime;
        }

        // If the first background is in the center of the screen...
        if (activeBackgrounds[0].transform.position.x < backgroundDespawnLocation.x)
        {
            // ...despawn the first background
            Destroy(activeBackgrounds[0]);
            activeBackgrounds.RemoveAt(0);

            // add a new background onto the end
            activeBackgrounds.Add(
                Instantiate(
                    backgroundPrefab,
                    activeBackgrounds[0].transform.position + backgroundSpawnDisplace,
                    spawnRotation
                ));

            // Change the image of the last background object to match what should be with the current level
            if (level < levelBackgrounds.Length)
            {
                activeBackgrounds[activeBackgrounds.Count - 1].GetComponent<Renderer>().material
                    = levelBackgrounds[level];
            }
            else
            {
                activeBackgrounds[activeBackgrounds.Count - 1].GetComponent<Renderer>().material
                    = levelBackgrounds[levelBackgrounds.Length - 1];
            }
            
            // make the new background a child of the Backgrounds object
            activeBackgrounds[activeBackgrounds.Count - 1]
                .transform.SetParent(backgrounds.transform);
        }
    }
}
