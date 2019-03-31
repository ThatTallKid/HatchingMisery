using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class BabyChickV2 : MonoBehaviour
{
    public float FollowSpeed;
    public float FollowDistance;
    public float wandertime;
    private bool startedfollowing = false;

    public enum ChickState
    {
        Wandering,
        Feeding,
        Following
    }
    public ChickState CurrentState;    // Change back to private after
    private GameObject MotherHen;
    private NavMeshAgent Nav;
    private GameObject CurrentFood;
    
    // Start is called before the first frame update
    void Start()
    {
        Nav = gameObject.GetComponent<NavMeshAgent>();
        CurrentState = ChickState.Following;
        MotherHen = FindObjectOfType<Chicken>().gameObject;
        Nav.speed = FollowSpeed;
        //PlayerPrefs.SetInt("feedtotal", FeedTotal);
        //PlayerPrefs.SetInt("chicksleft", PlayerPrefs.GetInt("chicksleft")+1);

    }

    // Update is called once per frame
    void Update()
    {
        Nav.isStopped = false;
        switch (CurrentState)
        {
            case ChickState.Following:
                Nav.destination = MotherHen.transform.position;
                if (!startedfollowing)
                {
                    Nav.stoppingDistance = FollowDistance;
                    startedfollowing = true;
                }
                //Vector3 ToChicken = MotherHen.transform.position - transform.position;
                //transform.Translate(ToChicken * FollowSpeed * Time.deltaTime);
                break;
            case ChickState.Feeding:
                if (startedfollowing)
                {
                    startedfollowing = false;
                    Nav.stoppingDistance = 0.1f;
                }

                if (!CurrentFood)
                {
                    CurrentState = ChickState.Following;
                }
                break;
            case ChickState.Wandering:
                if (startedfollowing)
                {
                    startedfollowing = false;
                    Nav.stoppingDistance = 0.1f;
                }
                wandertime += Time.deltaTime;
                if (wandertime > 6)
                {
                    Nav.destination = transform.position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0f,2f));
                    wandertime = 0;
                }
                //transform.Translate(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)) * FollowSpeed * Time.deltaTime);
                break;
        }
        Nav.speed = FollowSpeed;
    }

    public void TriggeredFeed(Vector3 position, float radius, GameObject item)
    {
        CurrentFood = item;
        if (CurrentState != ChickState.Feeding)
        {
            CurrentState = ChickState.Feeding;
            Nav.destination = position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0,radius));
        }
    }
}
