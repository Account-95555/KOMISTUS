using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Sprite openSprite;
    public Sprite closedSprite;

    public GameObject joystick;
    public GameObject interactButtonObj;
    public GameObject player;
    public GameObject feet;
    public AudioSource audioSource;

    public AudioClip open;
    public AudioClip close;

    private Color playerColor;
    private SpriteRenderer srHideObj;
    private SpriteRenderer srPlayer;
    private InteractButton ib;
    private PlayerController pc;
    private Joystick js;
    private bool inRange = false;
    private bool isHiding = false;

    // Start is called before the first frame update
    void Start()
    {
        srHideObj = GetComponent<SpriteRenderer>();
        srPlayer = player.GetComponent<SpriteRenderer>();
        pc = player.GetComponent<PlayerController>();
        ib = interactButtonObj.GetComponent<InteractButton>();
        js = joystick.GetComponent<Joystick>();
        audioSource = GetComponent<AudioSource>();
        playerColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding)
        {
            srHideObj.sprite = closedSprite;
            
        }
        else
        {
            srHideObj.sprite = openSprite;
            
        }
        
        if (inRange)
        {
            if (ib.clickRegistered)
            {
                ib.clickRegistered = false;
                if (isHiding == false)
                {
                    feet.SetActive(false);
                    playerColor.a = 0f;
                    srPlayer.color = playerColor;
                    //StartCoroutine(HideRoutine);
                    isHiding = true;
                    pc.isHiding = isHiding;
                    audioSource.PlayOneShot(close);
                    pc.canMove = false;
                    js.PointerUp();
                    joystick.SetActive(false);
                }
                else if (isHiding == true)
                {
                    feet.SetActive(true);
                    playerColor.a = 1f;
                    srPlayer.color = playerColor;
                    //StartCoroutine(HideRoutine);
                    isHiding = false;
                    pc.isHiding = isHiding;
                    audioSource.PlayOneShot(open);
                    pc.canMove = true;
                    joystick.SetActive(true);
                    
                }
            }
      
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    /*IEnumerator HideRoutine()
    {

    }*/
}
