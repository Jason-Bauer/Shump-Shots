using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Return to main menu
/// </summary>
public class GameOver : MonoBehaviour {

	public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
