using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepReachCalibrator : MonoBehaviour
{
    public GameObject uiController;
    public GameObject locationSpawner;
    public GameObject rHand;
    public GameObject lHand;
    public GameObject player;
    public GameObject astronaut;
    public GameObject speechBubble;
    public GameObject snapzone;
    public GameObject skipCalibrate;

    private Coroutine coroutine;
    private Vector3 snapzoneStartPosition;
    private LocationSpawner locSpawnerScript;
    private List<float> calibrationValues = new List<float>();  

    void Start()
    {
        locationSpawner.GetComponent<LocationSpawner>().setHeights(player.transform.position.y);
        if (coroutine != null){
            StopCoroutine(coroutine);
        }    
        coroutine = StartCoroutine(calibrate());
        snapzoneStartPosition = snapzone.transform.position;
    }

    IEnumerator calibrate(){
        yield return new WaitForSeconds(0.2f);
        snapzone.transform.position = snapzoneStartPosition;
        snapzone.transform.Translate(Vector3.up * (player.transform.position.y * Constants.RING));
        astronaut.SetActive(true);
        speechBubble.SetActive(true);
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Stand like me &\nsqueeze the trigger";
        yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger));
        locationSpawner.GetComponent<LocationSpawner>().CalcMagnitude(rHand.transform.position);
        skipCalibrate.SetActive(true);

        yield return new WaitForSeconds(1.0f);    
        float distance = 0.0f;
        foreach (Direction direction in System.Enum.GetValues(typeof(Direction))){
            while(calibrationValues.Count < 2){
                Vector3 startPosition = player.transform.position; 
                if (calibrationValues.Count < 1){
                    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Take a big step " + direction.ToString();
                }
                else {
                    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Take another big\nstep " + direction.ToString();
                }
                yield return new WaitForSeconds(3);
                distance = measureStep(direction, startPosition);

                if (distance > 0.5){
                    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Thank you";
                    calibrationValues.Add(distance);
                    yield return new WaitForSeconds(1.0f);
                }
                else {
                    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Try Again";
                    yield return new WaitForSeconds(1.5f);
                }
            }
            float stepMag = avgStepLength(calibrationValues);
            locationSpawner.GetComponent<LocationSpawner>().setMagnitude(avgStepLength(calibrationValues) + Constants.STEPPER_MAGNITUDE_OFFSET);
            calibrationValues.Clear();
        }
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Return to the red circle";       
        yield return new WaitForSeconds(3.0f);

        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.STEP_REACH_INSTRUCTIONS;
        uiController.GetComponent<UIController>().MenuOn();    
    }

    public void Recalibrate(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        uiController.GetComponent<UIController>().MenuOff();
        coroutine = StartCoroutine(calibrate());
    }

    public void InterruptCalibraton(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.STEPPER_INSTRUCTIONS;
        locationSpawner.GetComponent<LocationSpawner>().setMagnitude(1.5f);
        locationSpawner.GetComponent<LocationSpawner>().SetStepReachMagnitude(0.9f);
    }

    private float avgStepLength(List<float> steps){
        float sum = 0.0f;
        if (steps.Count == 0){
            return sum;
        }
        for (int i = 0; i < steps.Count; i++){
            sum += steps[i];
        }
        return sum / steps.Count;
    }
    
     public float measureStep(Direction dir, Vector3 startPos){
        float distance = 0.0f;
        switch (dir){
            case Direction.forward:
                distance = player.transform.position.z - startPos.z;
                break;
            case Direction.right:
                distance = player.transform.position.x - startPos.x;
                break;
            case Direction.backward:
                distance = startPos.z - player.transform.position.z;
                break;
            case Direction.left:
                distance = startPos.x - player.transform.position.x;
                break;
            default:
                Debug.Log("Direction not found");
                break;
        }
        return distance;
     }

}
