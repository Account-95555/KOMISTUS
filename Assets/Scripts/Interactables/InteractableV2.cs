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

    public bool isItem;
    public bool isSource;
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
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.isActive)
        {
            spezionParticles.SetActive(true);
        }
        else
        {
            spezionParticles.SetActive(false);
        }
        //onClick
        if (ib.clickRegistered && inRange)
        {
            if (isItem)
            {
                iv2.isHolding = true;
                iv2.attachedObject = gameObject;
                iv2.itemImage.sprite = sr.sprite;
                iv2.storedItem = itemName;
                gameObject.SetActive(false);
            }
            else if (isSource)
            {
                StartCoroutine(SealBreak());
            }
            ib.clickRegistered = false;
        }
        if (sr.isVisible && isAdded == false)
        {
            sp.interactiblesOnScreen += 1;
            isAdded = true;
        }
        else if (!sr.isVisible && isAdded == true)
        {
            sp.interactiblesOnScreen -= 1;
            isAdded = false;
        }
    }

    IEnumerator SealBreak()
    {
        controls.SetActive(false);
        cc.target = originEntity;
        deathParticles.transform.position = new Vector3(originEntity.transform.position.x, originEntity.transform.position.y,deathParticles.transform.position.z);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2.1f);
        originEntity.SetActive(false);
        deathParticles.SetActive(true);
        yield return new WaitForSecondsRealtime(2.1f);
        originEntityHolder.SetActive(false);
        controls.SetActive(true);
        js.PointerUp();
        cc.target = player;
        Time.timeScale = 1f;
    }

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
