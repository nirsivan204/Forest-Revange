using UnityEngine;

public class AppleDestruction : MonoBehaviour
{
    public PowerType power;

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
        string powerString = powerToString();
        if(collision.gameObject.name.IndexOf(powerString) != -1)
        {
            // TODO: destruct the collision object
        }
    }

    private string powerToString() {
        switch (power) {
            case PowerType.Gas:
                return "gas";
            case PowerType.Sewerage:
                return "sewerage";
            case PowerType.Electric:
                return "gas";
            default:
                return "nothing";
        }
    }
}
