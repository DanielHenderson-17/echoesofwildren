using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel; // Reference to the Main Menu Panel
    public Animator menuAnimator; // Reference to the Animator for the menu
    private bool isMenuOpen = false; // Track whether the menu is open or closed
    private bool isAnimating = false; // Track if the menu is in the middle of an animation

    void Start()
    {
        // Ensure the menu is closed at the start of the game
        mainMenuPanel.SetActive(false); // Ensure the panel is inactive at game start
        Time.timeScale = 1f; // Make sure time is running normally at the start
    }

    void Update()

    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Debug.Log("Escape pressed");

            // Check if the menu is open or closed
            if (isMenuOpen)
            {
                // Debug.Log("Menu is open, closing it...");
                CloseMenu(); // Close the menu if it's open
            }
            else
            {
                // Debug.Log("Menu is closed, opening it...");
                OpenMenu(); // Open the menu if it's closed
            }
        }
    }

    // Show the menu by playing the slide-in animation
    public void OpenMenu()
    {
        // Debug.Log("Opening menu");
        mainMenuPanel.SetActive(true); // Ensure the panel is active
        menuAnimator.Play("MenuSlideIn"); // Directly play the slide-in animation by name
        isMenuOpen = true;
        isAnimating = true; // Set animating to true

        // Delay setting Time.timeScale to avoid freezing animation
        Invoke("PauseGame", menuAnimator.GetCurrentAnimatorStateInfo(0).length); // Pause the game after animation duration
    }

    // Pauses the game after the animation plays
    void PauseGame()
    {
        Time.timeScale = 0f; // Now pause the game
    }

    // Hide the menu by playing the slide-out animation
    public void CloseMenu()
    {
        // Debug.Log("CloseMenu called"); // Log when CloseMenu is triggered

        Time.timeScale = 1f; // Resume the game before playing the close animation
        menuAnimator.SetBool("isClosing", true); // Trigger the transition to MenuSlideOut
        menuAnimator.Play("MenuSlideOut"); // Play the slide-out animation by name

        // Debug.Log("Set isClosing to true and triggered MenuSlideOut"); // Log the progress
        isMenuOpen = false;
        isAnimating = true;
        Invoke("EndAnimation", menuAnimator.GetCurrentAnimatorStateInfo(0).length); // End animation after duration
    }




    // Stop animation flag after the animation completes
    void EndAnimation()
    {
        // Debug.Log("Animation ended");
        isAnimating = false; // Stop the animation flag
        menuAnimator.SetBool("isClosing", false); // Reset the closing animation trigger
        if (!isMenuOpen) mainMenuPanel.SetActive(false); // Deactivate the panel if the menu is closed
    }

    // Quit to the StartScreen when the Quit button is pressed
    public void QuitToStartScreen()
    {
        Time.timeScale = 1f; // Reset the time scale to normal before switching scenes
        SceneManager.LoadScene("StartScreen"); // Load the StartScreen scene
    }
}
