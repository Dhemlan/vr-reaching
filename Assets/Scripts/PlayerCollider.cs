using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject locSpawner;
    public GameObject roundManager;
    public GameObject beam;

    void OnTriggerEnter(Collider other) {
        //other.transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
        print(other);
        if(other.gameObject.name == "HitBox"){
            beam.GetComponent<Renderer>().enabled = false;
            roundManager.GetComponent<RoundManager>().TargetHit();
        } 

        if(other.gameObject.name == "HomePlate"){
            if (!beam.GetComponent<Renderer>().enabled){
                locSpawner.GetComponent<LocationSpawner>().spawnLoc();  
            }
        }    
    }
}
