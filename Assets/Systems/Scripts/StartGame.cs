using UnityEngine;
using UnityEngine.SceneManagement;  // This is needed to load scenes.

public class StartGame : MonoBehaviour
{
    // Function to be called when the start button is pressed.
    public void OnStartButtonPressed()
    {
        // Load your game scene, replace "GameScene" with the actual name of your game scene.
        SceneManager.LoadScene("GameStart");
    }
}
