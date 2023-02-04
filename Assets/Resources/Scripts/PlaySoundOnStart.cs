using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip Transition;
    [SerializeField] private AudioClip currentWorld;
    
    private void OnEnable()
    {
        GameManager.changeWorldsEvent += SwitchWorlds;
    }
    public void OnDisable()
    {
        GameManager.changeWorldsEvent -= SwitchWorlds;
    }

    void SwitchWorlds(World world)
    {

        SoundManager.instance.PlaySFX(Transition);
       
    }
}
