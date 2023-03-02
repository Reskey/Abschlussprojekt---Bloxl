using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        // Funktioniert nur im fertigen Build, nicht im Unity Editor
        Application.Quit();
    }

    public void BackToMenu()
    {
        FindObjectOfType<GameController>().inputControlls.Disable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
