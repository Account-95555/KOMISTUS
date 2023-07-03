using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public bool originDestroyed = false;
    public string levelToEnter;
    public GameObject levelManager;
    public NormalLevelManager nlm;
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
                nlm.ExitScene(levelToEnter);
            }
            else
            {
                textbox.text = "The origin source is still active, use the Spezion if you are having trouble finding it!";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        textbox.text = "";
    }
}
