using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    public int ChicksLeft;
    public float gametime;
    public GameObject chick1;
    public GameObject chick2;
    public GameObject chick3;
    public GameObject chick4;
    public GameObject chick5;
    public GameObject chick6;
    public GameObject chick7;
    public GameObject chick8;
    public GameObject chick9;
    public GameObject chick10;
    // Start is called before the first frame update
    void Start()
    {
       ChicksLeft = PlayerPrefs.GetInt("chicksleft"); 

        if(ChicksLeft < 10)
        {
            Destroy(chick1);
        }
        if (ChicksLeft < 9)
        {
            Destroy(chick2);
        }
        if (ChicksLeft < 8)
        {
            Destroy(chick3);
        }
        if (ChicksLeft < 7)
        {
            Destroy(chick4);
        }
        if (ChicksLeft < 6)
        {
            Destroy(chick5);
        }
        if (ChicksLeft < 5)
        {
            Destroy(chick6);
        }
        if (ChicksLeft < 4)
        {
            Destroy(chick7);
        }
        if (ChicksLeft < 3)
        {
            Destroy(chick8);
        }
        if (ChicksLeft < 2)
        {
            Destroy(chick9);
        }
        if (ChicksLeft < 1)
        {
            Destroy(chick10);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        gametime = gametime + Time.deltaTime * 1;

        if( gametime > 60)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
