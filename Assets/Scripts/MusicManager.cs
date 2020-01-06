using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static MusicManager music;

    void Awake()
    {
        if (!music){
            music = this;
        }
        else 
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
