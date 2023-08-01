using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathPuzzle : MonoBehaviour
{
    public Transform respawnPoint;

    public Joystick js;

    public GameObject joystickPanel;
    public GameObject player;
    public GameObject blackScreenHolder;
    
    public Image blackScreen;

    public bool wrongStep;
    public bool reset;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = blackScreenHolder.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wrongStep)
        {
            StartCoroutine(IncorrectCo());
            wrongStep = false;
        }
    }


    IEnumerator IncorrectCo()
    {
        js.PointerUp();
        joystickPanel.SetActive(false);
        blackScreenHolder.SetActive(true);
        blackScreen.CrossFadeAlpha(0f, 0f, false);
        blackScreen.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1f);
        reset = true;
        player.transform.position = respawnPoint.position;
        yield return new WaitForSeconds(0.5f);
        joystickPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackScreen.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
        reset = false;
        blackScreenHolder.SetActive(false);
    }

}
