using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public bool originDestroyed = false;
    public bool requiresItem;
    public bool spezionRequirement;
    public bool isTutorial;
    public string requiredItem;
    public string levelToEnter;
    public GameObject levelManager;
    public NormalLevelManager nlm;
    public Spezion sp;
    public InventoryV2 iv2;
    // Start is called before the first frame update
    void Start()
    {
        nlm = levelManager.GetComponent<NormalLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (originDestroyed)
            {
                if (isTutorial)
                {
                    PlayerPrefs.SetString("tutorialComplete", "true");
                }
                if (requiresItem)
                {
                    if (iv2.storedItem != requiredItem)
                    {
                        if (textbox.text == "")
                        {
                            textbox.text = "You need to bring the item from the safe back to HQ";
                        }
                        
                    }
                    else
                    {
                        nlm.ExitScene(levelToEnter);
                    }
                }
                else if (spezionRequirement)
                {
                    if (sp.spezionAmount >= 3)
                    {
                        nlm.ExitScene(levelToEnter);
                    }
                    else
                    {
                        nlm.ExitScene("IncompleteEnding");
                    }
                    
                }
                else
                {
                    nlm.ExitScene(levelToEnter);
                }
                
            }
            else
            {
                if (textbox.text == "")
                {
                    textbox.text = "The origin source is still active, use the Spezion if you are having trouble finding it!";
                }
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        textbox.text = "";
    }
}
