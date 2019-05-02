using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public RawImage Tutorial1;
    public RawImage Tutorial2;
    public float Gametime;
    public bool tutting1;
    public bool tutting2;
    // Start is called before the first frame update
    void Start()
    {
        Tutorial1.CrossFadeAlpha(0f, 0f, false);
        Tutorial2.CrossFadeAlpha(0f, 0f, false);
        tutting1 = false;
        tutting2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Gametime = Gametime + Time.deltaTime * 1;
        if(Gametime > 2 && tutting1 == false)
        {
            Tutorial1.CrossFadeAlpha(4f, 4f, false);
            tutting1 = true;
        }

        if(Gametime > 12)
        {
            Tutorial1.CrossFadeAlpha(0f, 1f, false);
        }

        if (Gametime > 14 && tutting2 == false)
        {
            Tutorial2.CrossFadeAlpha(4f, 4f, false);
            tutting2 = true;
        }

        if (Gametime > 24)
        {
            Tutorial2.CrossFadeAlpha(0f, 1f, false);
        }
    }
}
