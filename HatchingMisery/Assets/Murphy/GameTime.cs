using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    public int ChicksLeft;
    public float gametime;
    public GameObject UniqueChick;// for later
    public List<GameObject> Chicks;

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
        PlayerPrefs.SetInt("currentfeed",0);
    }

    public static void checklevel()
    {
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            PlayerPrefs.SetInt("feedtotal",0);
            PlayerPrefs.SetInt("chicksleft",10);
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
