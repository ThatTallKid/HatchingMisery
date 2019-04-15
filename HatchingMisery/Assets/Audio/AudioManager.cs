using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicTrackArray;
    public AudioSource currentMusicTrack;
    public bool playMusicTrack2;
    public bool playMusicTrack3;
    public int chicksLeft;
    public int chicCountTrack2;
    public int chicCountTrack3;


    void Awake()
    {
        currentMusicTrack = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        chicksLeft = PlayerPrefs.GetInt("chicksleft");

        currentMusicTrack.clip = musicTrackArray[(0)];
        currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (chicksLeft <= chicCountTrack2)
        {
            currentMusicTrack.Stop();

            currentMusicTrack.clip = musicTrackArray[(1)];
            currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
            currentMusicTrack.Play();
        }

        if (chicksLeft <= chicCountTrack3)
        {
            currentMusicTrack.Stop();

            currentMusicTrack.clip = musicTrackArray[(2)];
            currentMusicTrack.PlayOneShot(currentMusicTrack.clip);
            currentMusicTrack.Play();
        }
    }
}
