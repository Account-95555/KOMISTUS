using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spezion : MonoBehaviour
{
    public int spezionAmount;
    public TextMeshProUGUI spezionLeft;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spezionLeft.text = spezionAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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
