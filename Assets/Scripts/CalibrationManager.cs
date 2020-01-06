using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationManager : MonoBehaviour
{
    public GameObject uiController;
    public Text instructions;
    public GameObject recalibrate;
    public GameObject locationSpawner;
    public GameObject rHand;
    public GameObject lHand;
    public GameObject headset;
    public GameObject astronaut;
    public GameObject speechBubble;
    public GameObject snapzone;

    private Vector3 snapzoneStartPosition;
    private Coroutine coroutine;

    void Start()
    {
        locationSpawner.GetComponent<LocationSpawner>().setHeights(headset.transform.position.y);    
        if (coroutine != null){
            StopCoroutine(coroutine);
        }    
        coroutine = StartCoroutine(calibrate());
        snapzoneStartPosition = snapzone.transform.position;
    }

    IEnumerator calibrate(){
        yield return new WaitForSeconds(0.2f);
        snapzone.transform.position = snapzoneStartPosition;
        snapzone.transform.Translate(Vector3.up * (headset.transform.position.y * Constants.RING));
        astronaut.SetActive(true);
        speechBubble.SetActive(true);
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Stand like me &\nsqueeze the trigger";
        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger));
        locationSpawner.GetComponent<LocationSpawner>().CalcMagnitude(rHand.transform.position);
        
        
        yield return new WaitForSeconds(1.0f);
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.REACHING_INSTRUCTIONS; 
        uiController.GetComponent<UIController>().MenuOn();        
    }

    public void Recalibrate(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        uiController.GetComponent<UIController>().MenuOff();
        coroutine = StartCoroutine(calibrate());
    }

}
