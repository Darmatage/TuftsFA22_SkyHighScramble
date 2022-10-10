using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
       public GameObject textGameObject;
       private int score;

       void Start () {
            score = 3;
            UpdateScore();
      }

       public void AddScore (int newScoreValue) {
             score = score + newScoreValue;
             UpdateScore ();
      }

       void UpdateScore () {
             Text scoreTextB = textGameObject.GetComponent<Text>();
              scoreTextB.text = "Score: " + score;
      }

      

   
}

