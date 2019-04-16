using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicTrack1;
    public AudioSource musicTrack2;
    public AudioSource musicTrack3;
    public int chicksLeft;
    public int chickCountTrack2;
    public int chickCountTrack3;


    private bool _hasFadeStarted = false;

    [Range(.01f, .5f)] public float fadeoutRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        chicksLeft = PlayerPrefs.GetInt("chicksleft");

        if (chicksLeft <= chickCountTrack2 && !_hasFadeStarted)
        {
            _hasFadeStarted = true;
            StartCoroutine(FadeOut(musicTrack1));

            StartCoroutine(FadeIn(musicTrack2));
        }

        if (chicksLeft <= chickCountTrack3 && !_hasFadeStarted)
        {
            _hasFadeStarted = true;
            StartCoroutine(FadeOut(musicTrack2));

            StartCoroutine(FadeIn(musicTrack3));
        }
    }

    IEnumerator FadeOut(AudioSource _track)
    {
        while (_track.volume > 0f)
        {
            _track.volume -= fadeoutRate;
            Debug.Log("volume is " + _track.volume);
            yield return new WaitForSeconds(.05f);
        }
        _hasFadeStarted = false;
    }


    IEnumerator FadeIn(AudioSource _track)
    {
        while (_track.volume < 1f)
        {

            _track.volume += fadeoutRate;
            yield return new WaitForSeconds(.05f);

        }

        _hasFadeStarted = false;
    }
}
