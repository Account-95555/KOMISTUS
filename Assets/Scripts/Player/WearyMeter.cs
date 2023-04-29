using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearyMeter : MonoBehaviour
{
    public Slider wearyMeter;
    public GameObject player;
    public GameObject jumpscare;
    private Player pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<Player>();
        wearyMeter.value = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isRunning == true)
        {
            wearyMeter.value += 0.1f;
        }
        else
        {
            wearyMeter.value -= 0.05f;
        }
        if (wearyMeter.value >= 100)
        {
            jumpscare.SetActive(true);
        }
    }
}
