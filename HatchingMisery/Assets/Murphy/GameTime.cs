using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject ScoreScreen;
    public GameObject Hen;
    public GameObject Hawk;
    public Image Score;


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

        Score.CrossFadeAlpha(0.1f, 4f, false);

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
        Debug.Log("number bitch" + PlayerPrefs.GetInt("chicksleft"));
        foreach (GameObject obj in ChickUI)
        {
            obj.SetActive(false);
        }
        ChickUI[PlayerPrefs.GetInt("chicksleft")].SetActive(true);


        // todo step by step logic will be needed in the tutorial to teach each part of the game in turn
        if (!tutorial && !finallevel)
        {

            gametime = gametime + Time.deltaTime * 1;
            if (PlayerPrefs.GetInt("chicksleft") < 1)
            {
                ScoreScreen.SetActive(true);
                Hawk.SetActive(false);
                Hen.SetActive(false);

            }
            if (gametime > 178)
            {
                Score.CrossFadeAlpha(4f, 4f, false);
            }
            if (gametime > 180)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                ScoreScreen.SetActive(true);
                Hawk.SetActive(false);
                Hen.SetActive(false);
            }


            // V's code
            if ((int)gametime == AfternoonTime)
            {
                TimeOfDay = "Afternoon";
                SunChange(TimeOfDay);
            }

            if ((int)gametime == EveningTime)
            {
                TimeOfDay = "Evening";
                SunChange(TimeOfDay);
            }

            // End V's code
        }
    }
}