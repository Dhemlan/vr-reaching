using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSetUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float radius = 1f;
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI*2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle)*radius, 1, Mathf.Sin(angle)*radius);
            GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), newPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
