using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePlanter : MonoBehaviour
{

    public float timeForRollingOnGround = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeForRollingOnGround <= 0)
        {
            Vector3 spawnLocation = new Vector3(transform.position.x, 0, transform.position.z);
            Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            Instantiate(Resources.Load<GameObject>("prefabs/TreeParent"),spawnLocation, spawnRotation, GameObject.Find("Under World").transform);
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            timeForRollingOnGround = timeForRollingOnGround - Time.fixedDeltaTime;
        }

    }

}
