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

    void Start()
    {
        feedscore = PlayerPrefs.GetInt("feedtotal");
        if(feedscore > 90)
        {
            deadchicks = 0;
        }
        if (feedscore < 80)
        {
            deadchicks = 1;
        }
        if (feedscore < 70)
        {
            deadchicks = 2;
        }
        if (feedscore < 60)
        {
            deadchicks = 3;
        }
        if (feedscore < 50)
        {
            deadchicks = 4;
        }
        if (feedscore < 40)
        {
            deadchicks = 5;
        }
        if (feedscore < 30)
        {
            deadchicks = 6;
        }
        if (feedscore < 20)
        {
            deadchicks = 7;
        }
        if (feedscore < 10)
        {
            deadchicks = 8;
        }
        deadchicks.ToString(strdeadchicks);
        txtdeadchicks.text = strdeadchicks;

        intchicksleft = 10 - deadchicks;
        intchicksleft.ToString(strchicksleft);
        txtchicksLeft.text = strchicksleft;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
