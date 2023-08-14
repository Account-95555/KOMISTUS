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
    public AudioClip locked;

    private Color playerColor;
    private SpriteRenderer srHideObj;
    private SpriteRenderer srPlayer;
    private InteractButton ib;
    private PlayerController pc;
    private Joystick js;
    public bool isLocked;
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
        //audioSource = GetComponent<AudioSource>();
        playerColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            if (isHiding)
            {
                srHideObj.sprite = closedSprite;

            }
            else
            {

                srHideObj.sprite = openSprite;

            }
        }
        
        
        if (inRange)
        {
            if (ib.clickRegistered)
            {
                ib.clickRegistered = false;
                if (isLocked)
                {
                    audioSource.PlayOneShot(locked); //audio feedback for locked door
                }
                else
                {
                    if (isHiding == false)
                    {
                        feet.SetActive(false); //set feet collider inactive so it does not trigger entity while hiding
                        playerColor.a = 0f; //set the player transparent as if inside hiding spot
                        srPlayer.color = playerColor; //store the player's color to prevent him from coming out black
                        //StartCoroutine(HideRoutine);
                        isHiding = true;
                        pc.isHiding = isHiding;
                        audioSource.PlayOneShot(close); //audio feedback
                        pc.canMove = false; //set player can move to false so they don't keep moving when they come out if they came in moving.
                        js.PointerUp(); //set joystick to its default position
                        joystick.SetActive(false); //disable it so player can't move
                    }
                    else if (isHiding == true)
                    {
                        feet.SetActive(true);
                        playerColor.a = 1f; //make the player reappear/opauge
                        srPlayer.color = playerColor; //reapply the player's colors
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            pc.noDrop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            pc.noDrop = false;
        }
    }

    /*IEnumerator HideRoutine()
    {

    }*/
}
