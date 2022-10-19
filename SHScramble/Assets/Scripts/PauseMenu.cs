using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject[] stars;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void exit() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("WorldMap");
    }

    public void restart() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Clouds");
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win(int happy) {
        Pause();
        int j = 0;
        if (happy >= 75)
            j = 3;
        else if (happy >= 50)
            j = 2;
        else if (happy >= 25)
            j = 1;
        else
            j = 0;
        
        
        for (int i = 0; i < j; i++)
            stars[i].SetActive(true);
    }

    public void Lose(){
        Pause();
    }
}
