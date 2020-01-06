using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepUIController : MonoBehaviour
{

public Text scoreCounter;
public Text timerText;
public Text locAlert;
public GameObject playBut;
public GameObject calibrateBut;
public GameObject menuBut;
public GameObject settingsControl;
public GameObject magUI;
public Text ClockRangeLabelL;
public Text ClockRangeLabelR;

public Image clockSliderBGL;
public Image clockSliderFillL;
public Image clockSliderBGR;
public Image clockSliderFillR;

public bool invertedClockRange;

public GameObject astronaut;
public GameObject speechBubble;


public void StartGame(){
    MenuOff();
    scoreCounter.text = "Score: 0";
}

public void RoundOver(){
    timerText.text = null;
    MenuOn();
    playBut.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Replay";
    locAlert.text = "";
    StartCoroutine(congratsMessage());
}

IEnumerator congratsMessage(){
    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Round over\nWell done!";
    yield return new WaitForSeconds(5.0f);
    speechBubble.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Constants.STEPPER_INSTRUCTIONS;
}

public void MenuOff(){
    playBut.SetActive(false);
    settingsControl.SetActive(false);
    calibrateBut.SetActive(false);
    menuBut.SetActive(false);
    astronaut.SetActive(false);
    speechBubble.SetActive(false);
}

public void MenuOn(){
    playBut.SetActive(true);
    settingsControl.SetActive(true);
    calibrateBut.SetActive(true);
    menuBut.SetActive(true);
    astronaut.SetActive(true);
    speechBubble.SetActive(true);
}

public void MagUIOn(){
    magUI.SetActive(true);
}

public void UpdateClockRangeL(float val){
    ClockRangeLabelL.text = ((int)val).ToString();
}

public void UpdateClockRangeR(float val){
    ClockRangeLabelR.text = ((int)val).ToString();
}

public void invertClockRange(){
    clockSliderFillL.color = clockSliderFillR.color;
    clockSliderBGL.color = clockSliderBGR.color;
    clockSliderFillR.color = clockSliderBGL.color;
    clockSliderBGR.color = clockSliderFillL.color;
}

}


