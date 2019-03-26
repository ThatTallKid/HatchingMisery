using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class Sun : MonoBehaviour
{
    /*public float MorningAngle;
    public float AfternoonAngle;
    public float EveningAngle;*/

    public Color MorningColor;
    public Color AfternoonColor;
    public Color EveningColor;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Transform>().transform.rotation = Quaternion.Euler(MorningAngle, 0, 0);
        GetComponent<Light>().color = MorningColor;

        FindObjectOfType<GameTime>().SunChange += SunRotate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SunRotate(string DayTime)
    {
        //Debug.Log(DayTime);
        switch (DayTime)
        {
            case "Afternoon":
                GetComponent<Light>().color = AfternoonColor;
                //GetComponent<Transform>().transform.rotation = Quaternion.Euler(AfternoonAngle, 0, 0);
                break;
            case "Evening":
                GetComponent<Light>().color = EveningColor;
                //GetComponent<Transform>().transform.rotation = Quaternion.Euler(EveningAngle, 0, 0);
                break;
        }
    }
}

