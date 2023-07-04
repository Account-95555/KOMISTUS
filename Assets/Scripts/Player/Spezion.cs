using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spezion : MonoBehaviour
{
    public int spezionAmount;
    public TextMeshProUGUI spezionLeft;
    public GameObject noSpezion;
    public Image noSpezionImage;
    public bool isActive = false;
    public int interactiblesOnScreen = 0;

    // Start is called before the first frame update
    void Start()
    {
        spezionLeft.text = spezionAmount.ToString();
        noSpezionImage = noSpezion.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactiblesOnScreen > 0)
        {
            noSpezion.SetActive(false);
        }
        else
        {
            noSpezion.SetActive(true);
        }
    }

    public void SpezionUseUp()
    {
        if (spezionAmount > 0 && !isActive)
        {
            spezionAmount -= 1;
            spezionLeft.text = spezionAmount.ToString();
            StartCoroutine(SpezionCo());
           
        }
        
    }

    IEnumerator SpezionCo()
    {
        isActive = true;
        yield return new WaitForSeconds(6f);
        isActive = false;
    }
}
