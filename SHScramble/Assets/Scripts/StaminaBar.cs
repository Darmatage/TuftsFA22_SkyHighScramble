using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina;
    float maxStamina;

    public Slider staminaBar;
    public Image fill;
    public float dValue;

    public Image back;
    private float initialStam;
    private bool decrease;
    private bool filling;

    public PlayerMovement pm;


    public bool tired;
    public float lerpSpeed;

    private Color pink = new Color(0.7f, 0.3f, 0.0f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
        initialStam = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina >= maxStamina)
            tired = false;
        if (stamina < 1)
            tired = true;

        if (tired)
        {
            float lerpy = Mathf.PingPong(Time.time * lerpSpeed, 1.0f);
            fill.color = Color.Lerp(pink, Color.red, lerpy);
        }
        else
            fill.color = Color.green;



        if (Input.GetKey(KeyCode.LeftShift) && (stamina > 0) && !tired && pm.grounded)
            DecreaseEnergy();
        else if(initialStam > stamina)
        {
            initialStam -= dValue * 1.7f * Time.deltaTime;
        }
        else if (stamina < maxStamina)
            IncreaseEnergy();
        
        staminaBar.value = stamina;
        back.fillAmount = initialStam/30;
        
    }

    private void DecreaseEnergy()
    {
        if (stamina >= 0)
        {
            stamina -= dValue * Time.deltaTime;
            initialStam -= dValue/2 * Time.deltaTime;
        }

    }

    private void IncreaseEnergy()
    {
        if (!tired)
        {
            stamina += dValue * Time.deltaTime;
            initialStam += dValue * Time.deltaTime;
        }
        else
        {
            stamina += dValue/1.5f * Time.deltaTime;
            initialStam += dValue/1.5f * Time.deltaTime;
        }
    
    }
}
