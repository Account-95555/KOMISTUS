using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathSceneManager : SceneManagerCommon
{
    public Image guidingLight;
    public GameObject returnToMenuButton;
    public TextMeshProUGUI youDiedText;
    public string youDied;
    public TextMeshProUGUI deathText;
    private string causeOfDeath;
    // Start is called before the first frame update
    void Start()
    {
        causeOfDeath = PlayerPrefs.GetString("CauseOfDeath");
        blackScreenImage = blackScreenHolder.GetComponent<Image>();
        BlackScreenFadeIn();
        BGMFadeIn();
        StartCoroutine(DeathScreenCo());
    }

    // Update is called once per frame
    void Update()
    {
        if (causeOfDeath == "PatrolEnemy")
        {
            deathText.text = ("You were caught...");
        }
        else if (causeOfDeath == "WearyMeter")
        {
            deathText.text = ("You attracted too much attention to yourself...");
        }
        else
        {
            deathText.text = ("Cause of death - Unknown...");
        }
    }

    IEnumerator TextScroll()
    {
        foreach (char c in youDied)
        {
            youDiedText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator DeathScreenCo()
    {
        StartCoroutine(TextScroll());
        //youDiedText.CrossFadeAlpha(0f, 0f, false);
        deathText.CrossFadeAlpha(0f, 0f, false);
        guidingLight.CrossFadeAlpha(0f, 0f, false);
        //youDiedText.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(2f);
        deathText.CrossFadeAlpha(1f, 1.5f, false);
        guidingLight.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(2f);
        returnToMenuButton.SetActive(true);

    }

    public override IEnumerator ExitSceneCo(string sceneToLoad)
    {
        BGMFadeOut();
        BlackScreenFadeOut();
        yield return new WaitForSeconds(fadeOutTime);
        guidingLight.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
        LoadScene(sceneToLoad);
    }
}
