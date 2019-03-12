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

    void Update()
    {
        Debug.Log("i found a number " + PlayerPrefs.GetInt("feedtotal"));
        feedscore = PlayerPrefs.GetInt("feedtotal");
        intchicksleft = PlayerPrefs.GetInt("chicksleft");
        

        deadchicks = ((intchicksleft * 10) - feedscore) / 10; 
        
      
        
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
