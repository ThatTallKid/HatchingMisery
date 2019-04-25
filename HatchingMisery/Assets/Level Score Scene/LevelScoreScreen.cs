using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScoreScreen : MonoBehaviour
{
    public int FedChicks;
    public int feedscore;
    public string strdeadchicks;
    public Text txtdeadchicks;
    public Text txtchicksLeft;
    public int intchicksleft;
    public string strchicksleft;
    public GameObject[] ChickCounter;

    void Start()
    {
        CalcScore();
    }

    public void CalcScore()
    {
        Debug.Log("i found a number " + PlayerPrefs.GetInt("feedtotal"));
        PlayerPrefs.SetInt("feedtotal",PlayerPrefs.GetInt("feedtotal")+PlayerPrefs.GetInt("currentfeed"));
        feedscore = PlayerPrefs.GetInt("currentfeed");
        intchicksleft = PlayerPrefs.GetInt("chicksleft");
        

        // this was sometimes returning negative numbers thanks to the number of feeding zones being variable
        if (intchicksleft * 10 > feedscore)
        {
            FedChicks = Mathf.FloorToInt(Mathf.Min((float)intchicksleft ,  (float)feedscore/10)); 
        }

       
        //txtdeadchicks.text = (intchicksleft - FedChicks).ToString();

        intchicksleft = FedChicks;
        Debug.Log("number here = " + intchicksleft);
        ChickCounter[intchicksleft].SetActive(true);
        //txtchicksLeft.text = intchicksleft.ToString();
        if (FedChicks < 1)
        {
            
        }

        Debug.Log(PlayerPrefs.GetInt("chicksleft"));
        PlayerPrefs.SetInt("chicksleft", intchicksleft);
        Debug.Log(PlayerPrefs.GetInt("chicksleft"));
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
