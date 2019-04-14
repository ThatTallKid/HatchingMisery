using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip[] sfxSoundsArray;
    public AudioSource sfxSound;
    public int randomSFX;
    public float timeCounter;
    public float randomTime;
    public float rtMin;
    public float rtMax;

    void Awake()
    {
        sfxSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter > randomTime)
        {
            randomTime = Random.Range(rtMin, rtMax);
            timeCounter = 0f;
            sfxSound.Stop();
            ChooseMusic();
            sfxSound.Play();
        }
        timeCounter += Time.deltaTime;
    }

    public void ChooseMusic()
    {
        randomSFX = Random.Range(0, 2);

        switch (randomSFX)
        {
            case 0:
                sfxSound.clip = sfxSoundsArray[(0)];
                sfxSound.PlayOneShot(sfxSound.clip);
                break;
            case 1:
                sfxSound.clip = sfxSoundsArray[(1)];
                sfxSound.PlayOneShot(sfxSound.clip);
                break;
        }
    }
}
