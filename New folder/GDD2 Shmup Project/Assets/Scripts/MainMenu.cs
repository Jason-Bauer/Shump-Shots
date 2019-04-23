using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Behaviors for the main menu (Start the game)
/// </summary>
public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
