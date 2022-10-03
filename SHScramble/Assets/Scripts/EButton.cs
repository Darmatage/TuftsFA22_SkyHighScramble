using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EButton : MonoBehaviour
{
    private Image bg;
    public Image ebut;
    public Text text;
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup trans = ebut.GetComponent<CanvasGroup>();
        trans.alpha = 0.5f;
        bar.GetComponent<Animator>().Play("Done");
    }

    // Update is called once per frame
    public void Near(string s)
    {
        text.text = s;

        bg = this.GetComponent<Image>();
        CanvasGroup trans = ebut.GetComponent<CanvasGroup>();
        trans.alpha = 1.0f;
        bg.GetComponent<Animator>().Play("EButton");
        bar.GetComponent<Animator>().Play("TextBar");
    }

    // public void Click()
    // {
    //     bg = this.GetComponent<Image>();
    // }

    public void Away()
    {
        bg = this.GetComponent<Image>();
        CanvasGroup trans = ebut.GetComponent<CanvasGroup>();
        trans.alpha = 0.5f;
        bg.GetComponent<Animator>().Play("New State");
        bar.GetComponent<Animator>().Play("Done");
    }
}
