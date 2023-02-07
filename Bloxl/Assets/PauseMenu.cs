using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private Inputs inputController;

    private bool isPaused = false;

    private void Start()
    {
        inputController = FindObjectOfType<GameController>().inputControlls;

        inputController.PauseMenu.Enable();

        inputController.PauseMenu.Pause.started += PauseAction;
    }

    private void PauseAction(CallbackContext context)
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            inputController.PlayerBasics.Disable();

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;

            inputController.PlayerBasics.Enable();
        }
    }

    public void Resume() => PauseAction(default);

    public void QuitGame() => Application.Quit();

}
