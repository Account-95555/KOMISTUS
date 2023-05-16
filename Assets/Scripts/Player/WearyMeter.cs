using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearyMeter : MonoBehaviour
{
    public Slider wearyMeter;
    public float wearyVal = 0f;
    public float mobileMult = 1f;
    public GameObject player;
    public bool canBeChased = false;
    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        wearyMeter.value = 0f;
        wearyVal = Mathf.Clamp(wearyVal, 0f, 100f);

        // weary meter is slower on mobile than on pc
        if (Application.isMobilePlatform)
        {
            mobileMult = 10f;
        }
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
            wearyVal += 10f * Time.deltaTime;
        }
        else //decrease when not running
        {
            wearyVal -= 5f * Time.deltaTime;
        }
        if (wearyMeter.value >= 90) //if full, chase player
        {
            canBeChased = true;
            //pc.isDead = true;
            //PlayerPrefs.SetString("CauseOfDeath", "WearyMeter");
        }
        else if (wearyMeter.value < 50)
        {
            canBeChased = false;
        }
    }
}
