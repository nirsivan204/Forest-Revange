using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceEntity : MonoBehaviour
{
    public string unitType = "";
    public string resourceName = "";
    public float unitValency = 1;
    public float amount = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scaleAccordingToAmount();
    }
    void scaleAccordingToAmount()
    {
        Vector3 newScale = new Vector3(amount, amount, amount);
        transform.localScale = newScale;
    }
}

