using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScoreScreen : MonoBehaviour
{
    public int deadchicks;
    public int feedscore;
    public string strdeadchicks;
    public Text txtdeadchicks;
    public Text txtchicksLeft;
    public int intchicksleft;
    public string strchicksleft;
    public GameObject Loose;

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
            deadchicks = Mathf.FloorToInt(Mathf.Min((float)intchicksleft ,  (float)feedscore/10)); 
        }
        
      
        
        txtdeadchicks.text = deadchicks.ToString();

        intchicksleft = 10 - deadchicks;
     
        txtchicksLeft.text = intchicksleft.ToString();
        if (deadchicks < 1)
        {
            Loose.SetActive (true);
        }

        PlayerPrefs.SetInt("chicksleft", intchicksleft);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
