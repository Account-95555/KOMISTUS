using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableV2 : MonoBehaviour
{
    public GameObject player;
    public GameObject spezionParticles;
    public GameObject originEntityHolder;
    public GameObject originEntity;
    public GameObject deathParticles;
    public GameObject controls;
    public GameObject sigil;
    public AudioSource bgm;
    public AudioClip entityDeathSound;

    public bool isItem;
    public bool isSource;
    public bool sourceDestroyed;
    public string itemName;
    private bool inRange;
    private bool isAdded = false;
    //Classes
    public CameraController cc;
    public Spezion sp;
    public InteractButton ib;
    public InventoryV2 iv2;
    public Joystick js;
    private SpriteRenderer sr;
    public LevelEnd le;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.isActive) //If the spezion is pressed, do accordingly
        {
            spezionParticles.SetActive(true);
        }
        else
        {
            spezionParticles.SetActive(false);
        }
        //onClick
        if (ib.clickRegistered && inRange) //General Item interaction code
        {
            if (isItem) //Regular item
            {
                iv2.isHolding = true; //Player is holding in inventory
                iv2.attachedObject = gameObject; //item becomes linked to the inventory script
                iv2.itemImage.sprite = sr.sprite; //inventory button sprite is equal to the item.
                iv2.storedItem = itemName; //item name is stored in the inventory for unlockables
                gameObject.SetActive(false);
            }
            else if (isSource && !sourceDestroyed) //If the origin source has not been destroyed yet, destroy.
            {
                StartCoroutine(SealBreak());
            }
            ib.clickRegistered = false; //Set to false since the click activity has been done.
        }

        //For the spezion enabling only if there is an interactible on the screen.
        if (sr.isVisible && isAdded == false) //If there is an interactible on the screen, increment
        {
            sp.interactiblesOnScreen += 1;
            isAdded = true;
        }
        else if (!sr.isVisible && isAdded == true) //If the interactible disappears from screen, decrement
        {
            sp.interactiblesOnScreen -= 1;
            isAdded = false;
        }
    }

    IEnumerator SealBreak()
    {
        sourceDestroyed = true;
        controls.SetActive(false);
        cc.target = originEntity;
        deathParticles.transform.position = new Vector3(originEntity.transform.position.x, originEntity.transform.position.y,deathParticles.transform.position.z);
        sigil.transform.position = new Vector3(originEntity.transform.position.x, originEntity.transform.position.y, deathParticles.transform.position.z); 
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.1f);
        sigil.SetActive(true);
        originEntity.SetActive(false);
        yield return new WaitForSecondsRealtime(1.1f);
        sigil.SetActive(false);
        bgm.PlayOneShot(entityDeathSound);
        deathParticles.SetActive(true);
        yield return new WaitForSecondsRealtime(2.1f);
        originEntityHolder.SetActive(false);
        controls.SetActive(true);
        js.PointerUp();
        cc.target = player;
        Time.timeScale = 1f;
        le.originDestroyed = true;
    }

    //Range codes if the player is within the item's range
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
