using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudio;

    public AudioClip objectClickSound;
    public AudioClip objectFinishSound;
    public AudioClip objectGenerateSound;
    public AudioClip buttonSound;

    void Awake()
    {
        if(SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
    }
    
    public void PlayObjectClick()
    {
        myAudio.PlayOneShot(objectClickSound);
    }

    public void PlayObjectFinish()
    {
        myAudio.PlayOneShot(objectFinishSound);
    }

    public void PlayObjectGenerate()
    {
        myAudio.PlayOneShot(objectGenerateSound);
    }

    public void PlayButton()
    {
        myAudio.PlayOneShot(buttonSound);
    }
}
