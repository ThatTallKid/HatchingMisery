using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FeedV2 : MonoBehaviour   // Venus
{
    public int GrainAmount;
    public float feedtime;
    public bool feeding;
    public float Radius;
    public AudioSource eating;

    void Update()
    {
        if (feeding == true)
        {
            feedtime = feedtime + Time.deltaTime;
        }

        if (feedtime > 4)
        {
            PlayerPrefs.SetInt("currentfeed", PlayerPrefs.GetInt("currentfeed")+GrainAmount);
            Debug.Log("saving score as " + PlayerPrefs.GetInt("currentfeed"));
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Chick"))
        {
            //Debug.Log("test");
            other.gameObject.GetComponent<BabyChickV2>().TriggeredFeed(transform.position,Radius,gameObject);
            feeding = true;
            eating.Play();
        }
    }
}