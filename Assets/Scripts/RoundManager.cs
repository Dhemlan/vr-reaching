using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK.Prefabs.Interactions.Interactors;
using VRTK.Prefabs.Interactions.Interactables;


public class RoundManager : MonoBehaviour{

    System.Random rand = new System.Random();
    List<Vector3> clockLocs = new List<Vector3>();
    List<string> clockLocsLabel = new List<string>();
    private List<float> calibrationValues = new List<float>();  
    
    public GameObject stepLoc;
    public GameObject grabbable;
    public GameObject homePlate;
    public Text locAlert;
    public Text scoreCounter;
    public Text timerText;
    public Text magnitudeText;
    public GameObject UIController;

    public GameObject lHand;
    public GameObject rHand;

    private int score;
    private float magnitude;
    public float timer;
    private bool pauseTimer = false;

    void Start(){
        pauseTimer = true;
    }

    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0.0f) {
            if (!pauseTimer){
                endRound();
            }
        }
        else timerText.text = "Time: " + ((int)timer).ToString();
    }
    
    public void TargetHit(){
        if(grabbable == null){
            score++;
            scoreCounter.text = "Score: " + score.ToString();
            locAlert.text = "Return";
        }       
    }

    public void TargetMissed(){
        locAlert.text = "Bigger step\n" + locAlert.text; 
    }

    public void GrabScore(){
        score++;
        scoreCounter.text = "Score: " + score.ToString();
    }

    void endRound(){   
        if (stepLoc != null){
            if (grabbable ==  null){
                UIController.GetComponent<StepUIController>().RoundOver();
            }
            homePlate.GetComponent<Collider>().enabled = false;
            stepLoc.SetActive(false);
        }
        if (grabbable != null){
            // rHand.GetComponent<InteractorFacade>().Ungrab();
            //grabbable.GetComponent<InteractableFacade>().Ungrab(rHand);
            
            UIController.GetComponent<UIController>().RoundOver();
            grabbable.SetActive(false);
           
        }
        pauseTimer = true;
    }

    public void replay(){
        homePlate.GetComponent<Collider>().enabled = true;
        if (stepLoc != null){
            stepLoc.SetActive(true);
        }
        if (grabbable != null){
            grabbable.SetActive(true);
        }
        score = 0;
        pauseTimer = false;
        timer = 20.0f;
    }
    /*
    public void setMagnitude(float newMag){
        this.magnitude = newMag;
        magnitudeText.text = magnitude.ToString("n2");
    }

    public void DecreaseMagnitude(){
        magnitude *= 0.97f;
        magnitudeText.text = magnitude.ToString("n2");
    }

    public void IncreaseMagnitude(){
        magnitude *= 1.03f;
        magnitudeText.text = magnitude.ToString("n2");        
    } */

    public void ReturnToMenu(){
        SceneManager.LoadScene("NewMenu");
    }

}


