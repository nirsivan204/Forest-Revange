using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimatorScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool booli;
 
    public void Upgrade()
    {
        animator.SetTrigger("Upgrade");
    }

    public void Update()
    {
        if (booli)
        {
            booli = !booli;
            Upgrade();
        }
    }

}
