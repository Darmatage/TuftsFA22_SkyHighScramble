using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    // public GameObject loadScreen;
    // public Slider slider;
    // public Text text;
    public LevelLoaderMAIN ll;

    public void LoadLevel() {
        ll.scene = "Clouds";
        ll.LoadNextLevel();
    }

}
