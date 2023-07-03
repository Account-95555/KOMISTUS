using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
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
            nlm.ExitScene(levelToEnter);
        }
    }
}
