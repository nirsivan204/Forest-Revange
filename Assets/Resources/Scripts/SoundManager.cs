using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource upperWorld, underWorld, sfxSource;
    public static SoundManager instance;
    bool isUpper = true;
    [SerializeField] float timeToFade = 0.25f;

    [Header("SFX")]
    [SerializeField] private AudioClip Transition;
    [SerializeField] private AudioClip upperToUnder;
    [SerializeField] private AudioClip underToUpper;

    private void OnEnable()
    {
        GameManager.changeWorldsEvent += SwitchWorlds;
    }
    public void OnDisable()
    {
        GameManager.changeWorldsEvent -= SwitchWorlds;
    }
    void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    private void Start()
    {
        isUpper = true;
        upperWorld.Play();
        underWorld.Play();
        underWorld.volume = 0;

    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    void SwitchWorlds(World world)
    {
        Transition = world == World.Upper ? upperToUnder : underToUpper;
        PlaySFX(Transition);
        isUpper = !isUpper;
        StartCoroutine(FadeTrack());
    }
    private IEnumerator FadeTrack()
    {
        float timeElapsed = 0;
        if (isUpper)
        {
            while(timeElapsed < timeToFade)
            {
                upperWorld.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                underWorld.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timeElapsed < timeToFade)
            {
                upperWorld.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                underWorld.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;

            }
        }
    }
}
