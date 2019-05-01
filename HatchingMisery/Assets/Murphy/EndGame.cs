using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float GameTime;
    public bool trig;
    public Image panskils;
    // Start is called before the first frame update
    void Start()
    {
        panskils.CrossFadeAlpha(0f, 4f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (trig == true)
        {
            panskils.CrossFadeAlpha(4f, 4f, false);
            GameTime = GameTime + Time.deltaTime * 1;
        }

        if (GameTime > 4)
        {
            SceneManager.LoadScene(9);
        }
    }

    void OnTriggerEnter()
    {

        trig = true;
    }


}
