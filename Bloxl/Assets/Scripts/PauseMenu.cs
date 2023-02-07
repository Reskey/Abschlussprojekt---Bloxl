using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvasObject;

    private Inputs inputController;

    private bool isPaused = false;

    private void Awake()
    {
        inputController = FindObjectOfType<GameController>().inputControlls;

        inputController.PauseMenu.Pause.started += PauseAction;
    }

    private void Start()
    {
        inputController.PauseMenu.Enable();
    }

    private void PauseAction(CallbackContext context)
    {
        isPaused = !isPaused;

        pauseMenuCanvasObject.SetActive(isPaused);

        Time.timeScale = Convert.ToSingle(isPaused);

        if (isPaused)
        {
            inputController.PlayerBasics.Disable();

            return;
        }

        inputController.PlayerBasics.Enable();
    }

    public void Resume() => PauseAction(default);

    public void QuitGame() => Application.Quit();

}
