using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction{forward, backward, right, left};

public class StepCalibrator : MonoBehaviour{
    public GameObject uiController;
    public GameObject player;
    public GameObject astronaut;
    public GameObject speechBubble;
    public GameObject locationSpawner;
    public GameObject skipCalibrate;

    private Coroutine coroutine;
    private List<float> calibrationValues = new List<float>();  

    void Start(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(calibrate()); 
    }

    IEnumerator calibrate(){
        yield return new WaitForSeconds(0.2f);
        astronaut.SetActive(true);
        speechBubble.SetActive(true);
        
        float distance = 0.0f;
        foreach (Direction direction in System.Enum.GetValues(typeof(Direction))){
            int i = 0;
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
                    i++;
                    yield return new WaitForSeconds(1.0f);
                }
            }
            locationSpawner.GetComponent<LocationSpawner>().setMagnitude(avgStepLength(calibrationValues) + Constants.STEPPER_MAGNITUDE_OFFSET);
            calibrationValues.Clear();
        }
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Return to the red circle";       
        yield return new WaitForSeconds(3.0f);

        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.STEPPER_INSTRUCTIONS;
        uiController.GetComponent<StepUIController>().MenuOn();
    }

    public void Recalibrate(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        uiController.GetComponent<StepUIController>().MenuOff();
        coroutine = StartCoroutine(calibrate());
        skipCalibrate.SetActive(true);
    }

    public void InterruptCalibraton(){
        if (coroutine != null){
            StopCoroutine(coroutine);
        }
        speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.STEPPER_INSTRUCTIONS;
        locationSpawner.GetComponent<LocationSpawner>().setMagnitude(1.5f);
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

    /*
    IEnumerator calibrateMagnitude(){

         /*
        locAlert.text = "Starting in\n3..";
        WaitForSeconds one = new WaitForSeconds(1.0f);
        yield return one;
        locAlert.text += "2..";
        yield return one;
        locAlert.text += "1..";
        yield return one;
        pauseTimer = false;
        replay();
       
    public void calibrate(){
        pauseTimer = true;
        stepLoc.SetActive(false);
        playBut.SetActive(false);
        calibrateBut.SetActive(false);
        locAlert.rectTransform.anchoredPosition = Vector3.zero;  
        
        StartCoroutine(calibrateMagnitude());        
    } */