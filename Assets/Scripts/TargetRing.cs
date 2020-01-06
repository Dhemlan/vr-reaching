using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRing : MonoBehaviour
{
    public GameObject redRing;
    public GameObject greenRing;
    private bool roundActive = false;

    public void score(){
        StartCoroutine(scoreLogic());
    }

    IEnumerator scoreLogic(){ 
        redRing.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        greenRing.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        if (roundActive){
            redRing.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            greenRing.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void roundActiveFlip(){
        roundActive = !roundActive;
    }
}
