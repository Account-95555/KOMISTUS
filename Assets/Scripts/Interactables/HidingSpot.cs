using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Sprite openSprite;
    public Sprite closedSprite;

    public GameObject interactButtonObj;
    public GameObject player;
    public AudioSource audioSource;

    public AudioClip open;
    public AudioClip close;

    private Color playerColor;
    private SpriteRenderer srHideObj;
    private SpriteRenderer srPlayer;
    private InteractButton ib;
    private PlayerController pc;
    private bool inRange = false;
    private bool isHiding = false;

    // Start is called before the first frame update
    void Start()
    {
        srHideObj = GetComponent<SpriteRenderer>();
        srPlayer = player.GetComponent<SpriteRenderer>();
        pc = player.GetComponent<PlayerController>();
        ib = interactButtonObj.GetComponent<InteractButton>();
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
                    playerColor.a = 0f;
                    srPlayer.color = playerColor;
                    //StartCoroutine(HideRoutine);
                    isHiding = true;
                    pc.isHiding = isHiding;
                    audioSource.PlayOneShot(close);
                    pc.canMove = false;
                }
                else if (isHiding == true)
                {
                    playerColor.a = 1f;
                    srPlayer.color = playerColor;
                    //StartCoroutine(HideRoutine);
                    isHiding = false;
                    pc.isHiding = isHiding;
                    audioSource.PlayOneShot(open);
                    pc.canMove = true;
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
