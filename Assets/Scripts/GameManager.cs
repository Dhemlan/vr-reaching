using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject earth; 
    public GameObject music;

    private void Start() {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(music);  
    }

    public void PlayStepper(){
        SceneManager.LoadScene("BasicStepper");    
    }

    public void PlayReachStanding(){
        SceneManager.LoadScene("StandingReach");
    }

    public void PlayStepAndReach(){
        SceneManager.LoadScene("StepAndReach");
    }
}
