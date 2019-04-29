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
    public int previouschicks;
    public float gabetime;

    void Start()
    {
        CalcScore();
        gabetime = 0;
    }

    void Update()
    {
        gabetime = gabetime + Time.deltaTime * 1;
    }
    public void CalcScore()
    {
        Debug.Log("i found a number " + PlayerPrefs.GetInt("feedtotal"));
        feedscore = PlayerPrefs.GetInt("currentfeed");
        intchicksleft = PlayerPrefs.GetInt("chicksleft");

        
        previouschicks = intchicksleft;

        // this was sometimes returning negative numbers thanks to the number of feeding zones being variable
        if (intchicksleft * 10 > feedscore)
        {
            FedChicks = Mathf.FloorToInt(Mathf.Min((float)intchicksleft, (float)feedscore / 10));
            intchicksleft = FedChicks;
        }

        FedChicks = intchicksleft;
        //txtdeadchicks.text = (intchicksleft - FedChicks).ToString();


        Debug.Log("number here = " + intchicksleft);

        if (FedChicks >= 1)
        {
            StartCoroutine(End());
        }

        //txtchicksLeft.text = intchicksleft.ToString();
        if (FedChicks < 1)
        {
            SceneManager.LoadScene(8);

        }

        Debug.Log(PlayerPrefs.GetInt("chicksleft"));
        PlayerPrefs.SetInt("chicksleft", intchicksleft);
        Debug.Log(PlayerPrefs.GetInt("chicksleft"));
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    IEnumerator End()
    {
        for (int i = previouschicks; i >= intchicksleft; i--)
        {
            foreach (GameObject obj in ChickCounter)
            {
                obj.SetActive(false);
            }
            ChickCounter[i].SetActive(true);
            yield return new WaitForSeconds(1);
            Debug.Log("hi number : " + i);
            Debug.Log("hi bumbler : " + intchicksleft);
        }
        ChickCounter[intchicksleft].SetActive(true);
        yield return new WaitForSeconds(3);
        NextLevel();
    }


}
