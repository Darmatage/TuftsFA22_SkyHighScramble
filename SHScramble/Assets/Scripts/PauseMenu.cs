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
    public GameObject Darken;
    public GameObject tutorialUI;

    public GameHandler gameHandler;

    public float waitTime = .5f;
    public float tutorialIndex = 0;
    public bool taskFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
        // if(LevelHandler.playlev == 0) {
        //     playTutorial();
        // }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused && !tutorialUI.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(GameIsPaused && tutorialUI.activeSelf) {
                Resume();
                tutorialUI.SetActive(false);
            }
        }
        if(LevelHandler.playlev == 0) {
            if(tutorialIndex == 0) {
                Tutorial("Welcome to Sky High Scramble!");
                tutorialIndex++;
            } else if(tutorialIndex == 1) {
                if(waitTime > 0) {
                    waitTime -= Time.deltaTime;
                } else {
                    Tutorial("Use [WASD] to walk and shift to run");
                    tutorialIndex++;
                    waitTime = 3f;
                }
            } else if(tutorialIndex == 2) {
                if(Input.GetKeyDown("a") || Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) {
                    taskFinished = true;
                }
                if(taskFinished) {
                    if(waitTime > 0) {
                        waitTime -= Time.deltaTime;
                    } else {
                        Tutorial("The front of the plane has item buttons. Press E to get an item and E to drop items!");
                        tutorialIndex++;
                        taskFinished = false;
                        waitTime = 3f;
                    }
                }      
            } else if(tutorialIndex == 3) {
                if(PlayerPickupDrop.grabbing) {
                    taskFinished = true;
                }
                if(taskFinished) {
                    if(waitTime > 0) {
                        waitTime -= Time.deltaTime;
                    } else {
                        Tutorial("Talk to an customer that has an eclamation point by pressing E. Bring the customer the order it asks for to get a point!");
                        tutorialIndex++;
                        taskFinished = false;
                        waitTime = 3f;
                    }
                }
            } else if(tutorialIndex == 4) {
                if(gameHandler.doneOrders > 0) {
                    taskFinished = true;
                }
                if(taskFinished) {
                    if(waitTime > 0) {
                        waitTime -= Time.deltaTime;
                    } else {
                        Tutorial("Good Job! Now complete orders until the flight time ends and try to get all 3 stars!");
                        tutorialIndex++;
                        taskFinished = false;
                        waitTime = 3f;
                    }
                }
            }
        }
    }


    public void exit() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerPickupDrop.grabbing = false;
        SceneManager.LoadScene("End");
    }

    public void restart() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerPickupDrop.grabbing = false;
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
        Darken.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Darken.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        int j = 0;
        int happy = gameHandler.doneOrders;
        int goal = gameHandler.ordersNeeded[LevelHandler.playlev];
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
        if(LevelHandler.curstars[LevelHandler.playlev] < j) {
            LevelHandler.curstars[LevelHandler.playlev] = j;
        }
        if(j >= 1)
        {
            LevelHandler.levUnlocked[LevelHandler.playlev + 1] = true;
            LevelHandler.curlev += 1;
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
