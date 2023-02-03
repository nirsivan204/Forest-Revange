using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleNoClip : MonoBehaviour
{
    public float noCollisionTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<Collider>().enabled == false)
        {
            noCollisionTimer = noCollisionTimer - Time.deltaTime;
            if(noCollisionTimer <= 0)
            {
                transform.GetComponent<Collider>().enabled = true;
            }
        }
    }
}