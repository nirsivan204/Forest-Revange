using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AppleDestruction : MonoBehaviour
{
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        string resultString = Regex.Match(collision.gameObject.name, @"\d+").Value;
        int collisionLevel;
        bool success = int.TryParse(resultString, out collisionLevel);

        if(collisionLevel >= level)
        {
            // TODO: destruct the collision object
        }
    }
}
