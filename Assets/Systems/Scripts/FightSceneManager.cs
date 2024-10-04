using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro namespace

public class FightSceneManager : MonoBehaviour
{
    public Button runButton; // Reference to the run button
    public TextMeshProUGUI resultText; // Reference to the TextMeshProUGUI text

    private void Start()
    {
        runButton.onClick.AddListener(OnRunButtonClick); // Add listener to the button
        resultText.text = ""; // Clear the result text initially
    }

    private void OnRunButtonClick()
    {
        int roll = Random.Range(1, 11); // Generate a number between 1 and 10
        if (roll >= 5)
        {
            resultText.text = "You got away safely";
            Invoke("ReturnToGameStart", Random.Range(2f, 3f)); // Wait 2-3 seconds before returning
        }
        else
        {
            resultText.text = "You were unable to escape";
        }
    }

    private void ReturnToGameStart()
    {
        SceneManager.LoadScene("GameStart"); // Return to the GameStart scene
    }
}
