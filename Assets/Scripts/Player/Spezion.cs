using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spezion : MonoBehaviour
{
    public int spezionAmount;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpezionUseUp()
    {
        if (spezionAmount > 0)
        {
            spezionAmount -= 1;
            if (!isActive)
            {
                StartCoroutine(SpezionCo());
            }
        }
        
    }

    IEnumerator SpezionCo()
    {
        isActive = true;
        yield return new WaitForSeconds(6f);
        isActive = false;
    }
}
