using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method loads the next scene in the build order
    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Loads the next scene
    }

    // This method quits the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops playing in the editor
#else
        Application.Quit(); // Quits the game in a standalone build
#endif
    }

    // Trigger event for collision detection with player
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (assuming it has a "Player" tag)
        if (other.CompareTag("Player"))
        {
            StartGame(); // Calls StartGame if player collides
        }
    }
}
