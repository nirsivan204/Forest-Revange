using UnityEngine;

public class AppleDestruction : MonoBehaviour
{
    public ResourceTypes resourceType;

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
        switch (resourceType) {
            case ResourceTypes.Gas:
                return "gas";
            case ResourceTypes.Sewage:
                return "sewage";
            case ResourceTypes.Electricity:
                return "gas";
            case ResourceTypes.Water:
                return "water";
        }
        return null;
    }
}
