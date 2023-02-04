using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePlanter : MonoBehaviour
{
    public float treeSafeRadius = 2;

    public float timeForRollingOnGround = 1;
    public float timeUntilTimeoutBecauseNoPlant = 15;

    public bool canBePlanted = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeForRollingOnGround <= 0 && canBePlanted)
        {
            // Check if near tree or sapling
            Collider[] nearbys = Physics.OverlapSphere(transform.position, treeSafeRadius);
            foreach (Collider nearby in nearbys)
            {
                if(nearby.tag == "Tree")
                {
                    Destroy(transform.gameObject); // Too close to other tree or treeparent, destroying apple.
                    return;
                }
            }

            // Create new sapling
            Vector3 spawnLocation = new Vector3(transform.position.x, 0, transform.position.z);
            Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            Instantiate(Resources.Load<GameObject>("prefabs/TreeParent"),spawnLocation, spawnRotation, GameObject.Find("Under World").transform);
            Destroy(transform.gameObject);
        }
        timeUntilTimeoutBecauseNoPlant = timeUntilTimeoutBecauseNoPlant - Time.deltaTime;
        if(timeUntilTimeoutBecauseNoPlant <= 0)
        {
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
