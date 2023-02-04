using System.Collections;
using UnityEngine;

public class AppleDestruction : MonoBehaviour
{
    public ResourceTypes resourceType;
    //public ApplePlanter plant;
    GameObject hitParticle = null; 

    private void OnCollisionEnter(Collision collision)
    {
        string powerString = powerToString();
        if (collision.gameObject.name.IndexOf(powerString) != -1)
        {
            //plant.canBePlanted = false;
            Destroy(collision.gameObject);
            StartCoroutine(PlayEffect());
        }
    }

    IEnumerator PlayEffect()
    {
        switch (resourceType)
        {
            case ResourceTypes.Water:
                break;
            case ResourceTypes.Gas:
                hitParticle = (GameObject)Resources.Load("Particle Effects/BigExplosion.prefab");
                break;
            case ResourceTypes.Sewage:
                hitParticle = (GameObject)Resources.Load("Particle Effects/SwampBall.prefab");
                break;
            case ResourceTypes.Electricity:
                break;

        }
        if (hitParticle != null)
        {
            Instantiate(hitParticle, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(hitParticle.GetComponent<ParticleSystem>().main.duration);
        }
        Destroy(this.gameObject);


        //make effect

    }

    private string powerToString() {
        switch (resourceType) {
            case ResourceTypes.Gas:
                return "gas";
            case ResourceTypes.Sewage:
                return "acid";
            case ResourceTypes.Electricity:
                return "electricity";
            case ResourceTypes.Water:
                return "water";
        }
        return null;
    }
}
