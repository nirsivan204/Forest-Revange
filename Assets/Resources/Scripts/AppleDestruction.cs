using UnityEngine;

public class AppleDestruction : MonoBehaviour
{
    public ResourceTypes resourceType;

    private void OnCollisionEnter(Collision collision)
    {
        string powerString = powerToString();
        if (collision.gameObject.name.IndexOf(powerString) != -1)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private string powerToString() {
        switch (resourceType) {
            case ResourceTypes.Gas:
                return "gas";
            case ResourceTypes.Sewage:
                return "sewage";
            case ResourceTypes.Electricity:
                return "electricity";
            case ResourceTypes.Water:
                return "water";
        }
        return null;
    }
}
