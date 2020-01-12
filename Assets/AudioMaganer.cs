using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaganer : MonoBehaviour
{
    public static AudioClip voices, jack, background, lick, hit, bell, step;
    static AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        voices = Resources.Load<AudioClip>("voices");
        jack = Resources.Load<AudioClip>("jack");
        background = Resources.Load<AudioClip>("background");
        lick = Resources.Load<AudioClip>("lick");
        hit = Resources.Load<AudioClip>("hit");
        bell = Resources.Load<AudioClip>("bell");
        step = Resources.Load<AudioClip>("step");

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "step":
                audio.PlayOneShot(step);
                break;
            case "jack":
                audio.PlayOneShot(jack);
                break;
            case "voices":
                audio.PlayOneShot(voices);
                break;
        }
             
        
    }
}
