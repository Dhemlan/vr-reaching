using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationSpawner : MonoBehaviour
{
System.Random rand = new System.Random();
List<Location> clockLocsDefault = new List<Location>();
List<Location> clockLocs = new List<Location>();
List<float> targetHeights = new List<float>();
List<float> targetHeightValues = new List<float>();

public Text locAlert;
public Text magnitudeText;
public float magnitude;
public float stepReachMagnitude;
public GameObject stepLoc;
public GameObject grabbable;
public GameObject headset;

int minHeight = Constants.MINHEIGHT;
int maxHeight = Constants.MAXHEIGHT;
int minClockRangeL = Constants.DEFAULTCLOCKRANGEL;
int maxClockRangeL = Constants.MAXCLOCKRANGEL;
int minClockRangeR = Constants.MINCLOCKRANGER;
int maxClockRangeR = Constants.DEFAULTCLOCKRANGER;
bool invertedClockRange = false;

void Start(){
    setLocs();
    if (grabbable == null){
        minClockRangeL = Constants.MINCLOCKRANGEL;
        maxClockRangeR = Constants.MAXCLOCKRANGER;
    }
}

public void spawnLoc(){
    int nextLoc = rand.Next(0,clockLocs.Count);
    if (stepLoc != null){
        stepLoc.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        stepLoc.transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = true;
        PositionBeam(stepLoc, nextLoc);
    }
    if (grabbable != null){  
        if (stepLoc != null){
            PositionOrb(grabbable, nextLoc, this.stepReachMagnitude); 
        }
        else {
            PositionOrb(grabbable, nextLoc, this.magnitude); 
        }
        int nextHeight = rand.Next(0, targetHeights.Count);
        float height = targetHeights[nextHeight];
        //grabbable.transform.position = new Vector3(grabbable.transform.position.x, 0, grabbable.transform.position);
        grabbable.transform.Translate(Vector3.up * height);
    }
    locAlert.text = clockLocs[nextLoc].GetName() + " o'Clock";   
}

public void PositionBeam(GameObject target, int nextLoc){
    Vector3 nextPosition = clockLocs[nextLoc].GetPosition();
    target.transform.position = nextPosition * magnitude;
    target.transform.LookAt(Vector3.zero);
}

public void PositionOrb(GameObject target, int nextLoc, float distance){
    Vector3 nextPosition = clockLocs[nextLoc].GetPosition();
    target.transform.position = nextPosition * distance;
}

public void setMagnitude(float newMag){
    this.magnitude = newMag;
    magnitudeText.text = magnitude.ToString("n2");
}

public void SetStepReachMagnitude(float stepMag){
    this.stepReachMagnitude = magnitude; //this.magnitude + stepMag;
    setMagnitude(stepMag);
}

public void CalcMagnitude(Vector3 handPosition){
    setMagnitude(Vector3.Distance(handPosition, headset.transform.position) * Constants.REACH_MULTIPLIER);
}

public void GenerateHeightSettings(){
    targetHeights.Clear();
    if (minHeight > maxHeight){
        int temp = maxHeight;
        maxHeight = minHeight;
        minHeight = temp;    
    }
    for (int i = minHeight; i <= maxHeight; i++){
        targetHeights.Add(targetHeightValues[i - 1]);
    }
}

public void GenerateClockSettings(){   
    clockLocs.Clear();
    for (int i = minClockRangeL; i <= maxClockRangeL; i++){
        clockLocs.Add(clockLocsDefault[i - 1]);
    }
    for (int i = minClockRangeR; i <= maxClockRangeR; i++){
        clockLocs.Add(clockLocsDefault[i - 1]);
    }
}

public void setLocs(){   
    clockLocsDefault.Add(new Location(1, 0.5f, 0, 0.87f));
    clockLocsDefault.Add(new Location(2, 0.87f, 0, 0.5f));
    clockLocsDefault.Add(new Location(3, 1, 0, 0));

    clockLocsDefault.Add(new Location(4, 0.87f, 0,-0.5f));
    clockLocsDefault.Add(new Location(5, 0.5f, 0, -0.87f));
    clockLocsDefault.Add(new Location(6, 0, 0, -1));

    clockLocsDefault.Add(new Location(7, -0.5f ,0, -0.87f));
    clockLocsDefault.Add(new Location(8, -0.87f, 0, -0.5f));
    clockLocsDefault.Add(new Location(9, -1, 0, 0));

    clockLocsDefault.Add(new Location(10, -0.87f, 0, 0.5f));
    clockLocsDefault.Add(new Location(11, -0.5f, 0, 0.87f));
    clockLocsDefault.Add(new Location(12, 0, 0, 1));
}

public void setHeights(float playerHeight){
    targetHeightValues.Add(Constants.FLOOR);
    targetHeightValues.Add(playerHeight * Constants.KNEE_PERCENTAGE);
    targetHeightValues.Add(playerHeight * Constants.WAIST_PERCENTAGE);
    targetHeightValues.Add(playerHeight * Constants.SHOULDER_PERCENTAGE);
    targetHeightValues.Add(playerHeight * Constants.HEAD_PERCENTAGE);
}

public void InvertClockRange(){
    invertedClockRange = !invertedClockRange;
    if(invertedClockRange){
        SetInvertValues();
    }
    else{
        RevertInvertValues();
    }
}

public void SetInvertValues(){
    maxClockRangeL = minClockRangeL;
    minClockRangeL = Constants.MINCLOCKRANGEL;
    minClockRangeR = maxClockRangeR;
    maxClockRangeR = Constants.MAXCLOCKRANGER;
}

public void RevertInvertValues(){
    minClockRangeL = maxClockRangeL;
    maxClockRangeL = Constants.MAXCLOCKRANGEL;
    maxClockRangeR = minClockRangeR;
    minClockRangeR = Constants.MINCLOCKRANGER;
}

public void DecreaseMagnitude(){
    magnitude *= 0.97f;
    magnitudeText.text = magnitude.ToString("n2");
}

public void IncreaseMagnitude(){
    magnitude *= 1.03f;
    magnitudeText.text = magnitude.ToString("n2");        
}

public void UpdateMinHeight(float value){
    minHeight = (int)value;
}

public void UpdateMaxHeight(float value){
    maxHeight = (int)value;
}

public void UpdateMinClockRangeL(float value){
    minClockRangeL = (int)value;
}

public void UpdateMaxClockRangeL(float value){
    maxClockRangeL = (int)value;
}

public void UpdateMinClockRangeR(float value){
    minClockRangeR = (int)value;
}

public void UpdateMaxClockRangeR(float value){
    maxClockRangeR = (int)value;
}

}
