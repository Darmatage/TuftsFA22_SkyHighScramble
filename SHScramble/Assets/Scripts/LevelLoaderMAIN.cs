using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelLoaderMAIN : MonoBehaviour
{
    public VideoPlayer[] vp;
    public GameObject canva;
    public float transitionTime = 1f;
    public string scene;
    public bool top;
    // Update is called once per frame
    void Start()
    {
        canva.SetActive(false);
        if (!top)
            IntoNextLevel();
    }

    public void LoadNextLevel()
    {
        canva.SetActive(true);
        StartCoroutine(LoadLevel(scene, 1, true));
    }

    public void IntoNextLevel()
    {
        canva.SetActive(true);
        StartCoroutine(LoadLevel(scene, 0, false));
    }

    IEnumerator LoadLevel(string index, int i, bool load)
    {
        vp[i].Play();
        yield return new WaitForSeconds(transitionTime);
        if(load)
            SceneManager.LoadScene(index);
        yield return new WaitForSeconds(0.5f);
        canva.SetActive(false);
    }
}
