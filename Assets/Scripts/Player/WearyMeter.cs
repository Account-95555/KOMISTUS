using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearyMeter : MonoBehaviour
{
    public Slider wearyMeter;
    public float wearyVal = 0f;
    public GameObject player;
    public GameObject jumpscare;
    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        wearyMeter.value = 0f;
        wearyVal = Mathf.Clamp(wearyVal, 0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        wearyMeter.value = wearyVal;
        if (wearyVal < 0)
        {
            wearyVal = 0;
        }
        if (pc.isRunning == true) //increase the weary meter on run
        {
            wearyVal += 0.1f;
        }
        else //decrease when not running
        {
            wearyVal -= 0.05f;
        }
        if (wearyMeter.value >= 100) //if full, game over
        {
            pc.isDead = true;
            PlayerPrefs.SetString("CauseOfDeath", "WearyMeter");
        }
    }
}
