using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{

    //Prepare yourself for variable wall...
    public TextMeshProUGUI textbox;
    
    public GameObject joystick;
    public GameObject interactButtonObj;
    public GameObject inventoryButton;
    public GameObject attachedItem;
    public GameObject inventory;
    public GameObject spezionButton;
    public GameObject spezionParticles;
    public GameObject player;
    public GameObject nun;
    public GameObject symbol;
    public GameObject sealParticles;

    public AudioSource deathSound;
    public AudioSource bgm;

    public AudioClip entityDeath;

    public string endText;
    public string textPointText;
    public string[] lines;
    public string itemName;
    
    public float textSpeed;
    
    public bool isLockedCloset;
    //public bool isItemStore;
    public bool isItem;
    public bool isFinalLevelItem;
    public bool isDialoguePoint;
    public bool isOrigin;
    public bool sealBreakCo = false;
    
    public Sprite itemSprite;
    public Sprite replaceSprite;
    public Sprite symbolSprite;
    
    public Image itemImage;

    public LevelEnd le;
    private Spezion sp;
    private Inventory inv;
    private InteractButton ib;
    private PlayerController pc;
    private Joystick js;
    private SpriteRenderer sr;

    private Color symbolColor;

    private int index = 0;
    private bool inRange;
    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    //private bool checkForText = false;
    // Start is called before the first frame update

    public Sprite spriteChange;
    void Start()
    {
        symbolColor = Color.white;
        symbolColor.a = 0f;
        textbox.text = "";
        inventoryButton.SetActive(false);
        inv = inventory.GetComponent<Inventory>();
        ib = interactButtonObj.GetComponent<InteractButton>();
        sp = spezionButton.GetComponent<Spezion>();
        pc = player.GetComponent<PlayerController>();
        js = joystick.GetComponent<Joystick>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sealBreakCo)
        {
            symbol.GetComponent<SpriteRenderer>().color = symbolColor;
        }
        if (inRange)
        {
            if (ib.clickRegistered)
            {
                ib.clickRegistered = false;
                StartCoroutine(onClick());
            }
        }


        if (spezionParticles != null) //Activates spezion particles if there and spezion is pressed
        {
            if (sp.isActive)
            {
                spezionParticles.SetActive(true);
            }
            else
            {
                spezionParticles.SetActive(false);
            }
        }

        //Textbox skip
        if (dialogueStarted && !dialogueFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textbox.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textbox.text = lines[index];
                }
            }
        }

        
    }

    IEnumerator onClick()
    {
        // coroutine is here to make the text blank after 5 seconds
        if (isLockedCloset)
        {
            textbox.text = ("The closet is locked...");
        }
        //if (isItemStore)
        //{
        if (isItem)
        {
            if (!isFinalLevelItem)
            {
                inv.itemName = itemName;
                inv.isHolding = true;
                inventoryButton.SetActive(true);
                itemImage.sprite = itemSprite;
                inv.attachedItem = attachedItem;
                gameObject.SetActive(false);
            }
            else
            {
                textbox.text = "Wonder what that item is, I think it may be useful later...";
                yield return new WaitForSeconds(3f);
                textbox.text = "";
            }
        }

        if (isOrigin)
        {
            le.originDestroyed = true;
            StartCoroutine(SealBroken());
        }
        //else
        //{
            //textbox.text = ("Nothing in here...");
        //}
        //}
        yield return new WaitForSeconds(3f);
        textbox.text = "";
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the player is colliding with the object
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDialoguePoint && !dialogueStarted)
            {
                StartDialogue();
            }
            else
            {
                textbox.text = endText;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
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
            if (isDialoguePoint)
            {
                StartCoroutine(ClearText());
            }
        }
    }

    //IEnumerator ItemFall()
    //{
        //yield return new WaitForSeconds(1f);
    //}
    
    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(1.5f);
        textbox.text = "";
    }
    void StartDialogue()
    {
        pc.canMove = false;
        js.PointerUp();
        dialogueStarted = true;
        joystick.SetActive(false);
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator SealBroken()
    {
        bgm.PlayOneShot(entityDeath);
        sealBreakCo = true;
        symbol.SetActive(true);
        symbol.transform.position = new Vector3(nun.transform.position.x, nun.transform.position.y, nun.transform.position.z) ;
        symbolColor.a = Mathf.MoveTowards(symbolColor.a, 1f, 2f * Time.deltaTime);
        nun.SetActive(false);
        yield return new WaitForSeconds(2f);
        Instantiate(sealParticles, symbol.transform.position, Quaternion.identity);
        symbol.SetActive(false);
        gameObject.SetActive(false);
        sealBreakCo = false;
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textbox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textbox.text = string.Empty; //reset string to empty for next line
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            deathSound.Play();
            dialogueFinished = true;
            joystick.SetActive(true);
            pc.canMove = true;
            textbox.text = endText;
            if (replaceSprite != null)
            {
                sr.sprite = replaceSprite;
            }
            inv.isHolding = true;
            inventoryButton.SetActive(true);
            //textbox.text = (itemName + " has been picked up.");
            itemImage.sprite = itemSprite;
            inv.attachedItem = attachedItem;
            attachedItem.SetActive(false);
        }
        
    }
}
