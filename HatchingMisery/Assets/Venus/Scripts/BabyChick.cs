using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyChick : MonoBehaviour
{
    public float FollowSpeed;
    public float feedtime;
    public int FeedTotal;
   

    public enum ChickState
    {
        Wandering,
        Feeding,
        Following
    }
    public ChickState CurrentState;    // Change back to private after
    private GameObject MotherHen;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = ChickState.Following;
        MotherHen = FindObjectOfType<Chicken>().gameObject;
        PlayerPrefs.SetInt("feedtotal", FeedTotal);
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case ChickState.Following:
                Vector3 ToChicken = MotherHen.transform.position - transform.position;
                transform.Translate(ToChicken * FollowSpeed * Time.deltaTime);
                break;
            case ChickState.Feeding:
                feedtime = feedtime + Time.deltaTime * 2;
                break;
            case ChickState.Wandering:
                transform.Translate(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)) * FollowSpeed * Time.deltaTime);
                break;
        }

        if (feedtime > 4)
        {
            CurrentState = ChickState.Following;

            FeedTotal = FeedTotal + 10;
            feedtime = 0;
            PlayerPrefs.Save();
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Feed>())
        {
            CurrentState = ChickState.Feeding;
        }
    }
}
