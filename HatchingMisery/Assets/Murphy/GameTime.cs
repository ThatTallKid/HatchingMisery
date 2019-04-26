using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;   // This is for the events

public class GameTime : MonoBehaviour
{
    public bool tutorial = false;
    public bool finallevel = false;
    public int ChicksLeft;
    public float gametime;
    public GameObject UniqueChick;// for later
    public List<GameObject> Chicks;
    public int chickstartgameamount = 20;
    public GameObject[] ChickUI;

    // V's code
    public float AfternoonTime;
    public float EveningTime;
    public event Action<string> SunChange;

    private string TimeOfDay;
    // End V's code

    // Start is called before the first frame update
    void Start()
    {
        /*
       Chicks = GameObject.FindGameObjectsWithTag("Chick").ToList();
       ChicksLeft = PlayerPrefs.GetInt("chicksleft");

       for (int i = 0; i < ChicksLeft; i++)
       {
           GameObject temp = Chicks.Last();
           Chicks.Remove(temp);
           Destroy(temp);
       }
        */
        PlayerPrefs.SetInt("currentfeed", 0);
        
    }

    public static void checklevel(int a)
    {
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            PlayerPrefs.SetInt("feedtotal", 0);
            PlayerPrefs.SetInt("chicksleft", 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("number bitch" + PlayerPrefs.GetInt("Chicksleft")); 
        ChickUI[PlayerPrefs.GetInt("Chicksleft")].SetActive(true);
        // todo step by step logic will be needed in the tutorial to teach each part of the game in turn
        if (!tutorial&&!finallevel)
        {
                        
            gametime = gametime + Time.deltaTime * 1;

            if (gametime > 60)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // V's code
            if ((int) gametime == AfternoonTime)
            {
                TimeOfDay = "Afternoon";
                SunChange(TimeOfDay);
            }

            if ((int) gametime == EveningTime)
            {
                TimeOfDay = "Evening";
                SunChange(TimeOfDay);
            }

            // End V's code
        }
    }
}