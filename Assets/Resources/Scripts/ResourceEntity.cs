using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    Water,
}

public class ResourceEntity : MonoBehaviour
{
    public ResourceType type;
    public string resourceName = "";
    public float unitValency = 1;
    public float amount = 1;
    public bool isCollected = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.tag = "Resource";
    }

    // Update is called once per frame
    void Update()
    {
      //  scaleAccordingToAmount();
    }
    void scaleAccordingToAmount()
    {
        Vector3 newScale = new Vector3(amount, amount, amount);
        transform.localScale = newScale;
    }
}

