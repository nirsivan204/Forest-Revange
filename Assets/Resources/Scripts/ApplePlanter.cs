using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePlanter : MonoBehaviour
{

    private int timesCollided = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        timesCollided++;
        Debug.Log(timesCollided);
    }

}
