using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicTrackArray;
    public AudioSource currentMusicTrack;
    public bool playMusicTrack2;
    public bool playMusicTrack3;
    public HenMovement henMovementScript;
    public int chicksLeft;
    

    void Awake()
    {
        currentMusicTrack = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //henMovementScript = GetComponents<HenMovement>(chickamount);
        //chickamount = chicksLeft;

        currentMusicTrack.clip = musicTrackArray[(0)];
        currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (playMusicTrack2 == true)
        {
            currentMusicTrack.Stop();

            currentMusicTrack.clip = musicTrackArray[(1)];
            currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
            currentMusicTrack.Play();
        }

        if (playMusicTrack3 == true)
        {
            currentMusicTrack.Stop();

            currentMusicTrack.clip = musicTrackArray[(2)];
            currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
            currentMusicTrack.Play();
        }
    }

    public void PlayMusicTrack2()
    {
       if (chicksLeft <= 12)
        {
            playMusicTrack2 = true;
        }
        else
        {
            playMusicTrack2 = false;
        }
        
    }

    public void PlayMusicTrack3()
    {
        if (chicksLeft <= 5)
        {
            playMusicTrack3 = true;
        }
        else
        {
            playMusicTrack3 = false;
        }
        
    }
}
