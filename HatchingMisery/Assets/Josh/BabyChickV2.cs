using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class BabyChickV2 : MonoBehaviour
{
    public float FollowSpeed;
    public float FollowDistance;
    public float slowspeed = 0.5f;
    private float wandertime = 0;
    private float currentrand = 0;
    public float boredtime = 4;
    public float boredrange = 4;
    private bool startedfollowing = false;
    private int inmud = 0;
    public int Inmud
    {
        get => inmud;
        set
        {
            if (inmud != 0)
            {
                if(value == 0)FollowSpeed /= slowspeed;
            }
            else
            {
                if(value != 0)FollowSpeed *= slowspeed;
            }
            inmud = value;
        }
    }

    public enum ChickState
    {
        Wandering,
        Feeding,
        Following,
        Stopping
    }

    private ChickState currentState = ChickState.Following;

    public ChickState CurrentState
    {
        get { return currentState; }
        set
        {
            currentrand = Random.Range(0, boredrange);
            wandertime = 0;
            if (value != currentState)
            {
            }
            currentState = value;
        }
    }
// Change back to private after

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
    void FixedUpdate()
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
                
                if (wandertime < boredtime + currentrand)
                {
                    wandertime += Time.deltaTime;
                }
                else
                {
                    Nav.destination = transform.position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0f,2f));
                    CurrentState = ChickState.Wandering;
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
                    Nav.destination = transform.position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0f,2f));
                    CurrentState = ChickState.Wandering;
                }
                break;
            case ChickState.Wandering:
                if (startedfollowing)
                {
                    startedfollowing = false;
                    Nav.stoppingDistance = 0.1f;
                }
                if (wandertime < boredtime)
                {
                    wandertime += Time.deltaTime;
                }
                else
                {
                    Nav.destination = transform.position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0f,2f));
                    wandertime = 0;
                }
                //transform.Translate(new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2)) * FollowSpeed * Time.deltaTime);
                break;
            case ChickState.Stopping:
            {
                Nav.destination = transform.position;
                
                if (wandertime < boredtime + currentrand)
                {
                    wandertime += Time.deltaTime;
                }
                else
                {
                    Nav.destination = transform.position + (new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized * Random.Range(0f,2f));
                    CurrentState = ChickState.Wandering;
                }
            }
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
