using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text text;

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        //loadScreen.setActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync("WorldMap");

        operation.allowSceneActivation = false;
         while (!operation.isDone)
        {
            slider.value = operation.progress;
            //Output the current progress
            text.text = "Loading progress: " + (operation.progress * 100) + "%";

            // Check if the load has finished
            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                //Change the Text to show the Scene is ready
                text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
