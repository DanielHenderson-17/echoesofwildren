using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Animator menuAnimator;
    private bool isMenuOpen = false;
    private bool isAnimating = false;
    

    void Start()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log(isAnimating) ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        mainMenuPanel.SetActive(true);
        menuAnimator.Play("MenuSlideIn");
        isMenuOpen = true;
        isAnimating = true;
        Invoke("PauseGame", menuAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        menuAnimator.SetBool("isClosing", true);
        menuAnimator.Play("MenuSlideOut");
        isMenuOpen = false;
        isAnimating = true;
        Invoke("EndAnimation", menuAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    void EndAnimation()
    {
        isAnimating = false;
        menuAnimator.SetBool("isClosing", false);
        if (!isMenuOpen) mainMenuPanel.SetActive(false);
    }

    public void QuitToStartScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
