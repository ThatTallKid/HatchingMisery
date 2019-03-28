using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class Sun : MonoBehaviour
{
    // Setting up fields...
    /*#region Rotation
    public float AfternoonAngle;
    public float EveningAngle;
    #endregion*/

    #region SunColours
    public Color MorningColor;
    public Color AfternoonColor;
    public Color EveningColor;
    #endregion

    public float MorningAngle;
    public float RotationSpeed;

    private Vector3 SunsetPos;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up the initial sun
        GetComponent<Transform>().transform.rotation = Quaternion.Euler(MorningAngle, 0, 0);
        GetComponent<Light>().color = MorningColor;

        FindObjectOfType<GameTime>().SunChange += SunRotate;
        //SunsetPos = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Transform>().transform.rotation.
        //transform.rotation = Quaternion.AngleAxis(30, Vector3.up);

        // Rotating slowly throughout the day
        transform.Rotate(Time.deltaTime*RotationSpeed, 0, 0);
    }

    // Function that changes the colour of the sun based off the game timer
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

