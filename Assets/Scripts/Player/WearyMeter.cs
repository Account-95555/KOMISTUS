using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearyMeter : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip screech;
    public Slider wearyMeter;
    public float wearyVal = 0f;
    public float mobileMult = 1f;
    public GameObject player;
    public GameObject wearyHolder;
    public AudioSource chaseSource;
    public bool canBeChased = false;
    private PlayerController pc;
    private bool coroutineActive = false;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("wearyStatus", "on") == "off")
        {
            wearyHolder.SetActive(false);
        }
        pc = player.GetComponent<PlayerController>();
        wearyMeter.value = 0f;
        

        // weary meter is slower on mobile than on pc
        if (Application.isMobilePlatform)
        {
            mobileMult = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        wearyVal = Mathf.Clamp(wearyVal, 0f, 100f);
        wearyMeter.value = wearyVal;
        /*if (wearyVal < 0)
        {
            wearyVal = 0;
        }
        else if (wearyVal > 123f)
        {
            wearyVal = 123f;
        }*/
        if (pc.isRunning == true) //increase the weary meter on run
        {
            wearyVal += 20f * Time.deltaTime;
        }
        else //decrease when not running
        {
            wearyVal -= 10f * Time.deltaTime;
        }
        if (wearyMeter.value >= 100f)//if full, chase player
        {
            if (canBeChased == false)
            {
                canBeChased = true;
                bgm.PlayOneShot(screech);
            }
            
            if (!chaseSource.isPlaying)
            {
                chaseSource.Play();
            }
            
            //pc.isDead = true;
            //PlayerPrefs.SetString("CauseOfDeath", "WearyMeter");
        }
        else if (wearyMeter.value < 40)
        {
            if (!coroutineActive)
            {
                StartCoroutine(ChaseEnd());
            }
            
        }

        IEnumerator ChaseEnd()
        {
            coroutineActive = true;
            yield return new WaitForSeconds(4.2f);
            if (wearyMeter.value < 40)
            {
                canBeChased = false;
                chaseSource.Stop();
            }
            coroutineActive = false;
        }
        
    }
}
