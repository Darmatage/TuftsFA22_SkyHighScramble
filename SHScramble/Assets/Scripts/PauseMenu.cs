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
    public GameObject tutorialUI;

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
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(GameIsPaused) {
                Resume();
                tutorialUI.SetActive(false);
            }
            else {
                Tutorial("Use [WASD] to walk");
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

    public void Tutorial(string str)
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
        tutorialUI.transform.GetChild(4).GetComponent<Text>().text = str;
        tutorialUI.SetActive(true);
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void Win(int happy, int goal) {
        Pause();
        int j = 0;
        if (happy >= goal)
            j = 3;
        else if (happy >= goal/2)
            j = 2;
        else if (happy >= goal/4)
            j = 1;
        else
            j = 0;
        
        for (int i = 0; i < j; i++)
        {
            stars[i].SetActive(true);
            stars[i].GetComponent<Animator>().Play("Star");
        }
        //StartCoroutine(waiter(j));
    }

    IEnumerator waiter(int j)
    {
        for (int i = 0; i <= j; i++)
        {
            stars[i].SetActive(true);
            stars[i].GetComponent<Animator>().Play("Star");
            yield return new WaitForSeconds(1);
        }
    }

    public void Lose(){
        Pause();
    }
}
