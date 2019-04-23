using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HenMovement : MonoBehaviour
{
    public Rigidbody body;
    public int chickamount = 10;
    public GameObject ChickPrefab;
    public Animator anims;
    public float slowspeed = 0.5f;
    public int inmud = 0;
    public AudioSource walking;
    public AudioSource walkingMud;

    int runHash = Animator.StringToHash("IsRunning");
    int mudHash = Animator.StringToHash("InMud");
    public bool finallevel = false;
    public int Inmud
    {
        get => inmud;
        set
        {
            
            if (inmud != 0)
            {
                if (value == 0)
                {
                    Nav.speed /= slowspeed;
                    anims.SetBool(mudHash,false);
                }
            }
            else
            {
                if (value != 0)
                {
                    Nav.speed *= slowspeed;
                    anims.SetBool(mudHash,true);

                    walkingMud.PlayOneShot(walkingMud.clip);
                }
            }
            inmud = value;
        }
    }

    public NavMeshAgent Nav;
    // Start is called before the first frame update
    void Start()
    {
        Nav = gameObject.GetComponent<NavMeshAgent>();
        Nav.speed = 5;
        // only get the gameobject once rather than once per frame
        body = gameObject.GetComponent<Rigidbody>();
        GameTime tempgame = FindObjectOfType<GameTime>();
        if (tempgame)
        {
            if (chickamount == 10)
            {
                GameTime.checklevel(tempgame.chickstartgameamount);
                chickamount = PlayerPrefs.GetInt("chicksleft");
            }
        }
        else
        {
            chickamount = 10;
        }
        // TODO manage the living chicks for first level somehow, ignore player perfs for living chicks in tutorial
        
        for (int i = 0; i < chickamount; i++)
        {
            GameObject temp = Instantiate(ChickPrefab,transform.position + new Vector3(Random.Range(-2,2),0,Random.Range(-2,2)),Quaternion.identity);
            if (tempgame)
            {
                tempgame.Chicks.Add(temp);
                finallevel = tempgame.finallevel;
                temp.GetComponent<BabyChickV2>().final = finallevel;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anims.SetBool(runHash,(Nav.velocity.magnitude > 0.1f));
        //anims.speed = Nav.velocity.magnitude;
        Nav.isStopped = false;

        if (finallevel)
        {
            Nav.destination = transform.position + new Vector3(1,0,-Input.GetAxis("Horizontal")).normalized;
        }
        else
        {
            Nav.destination = transform.position + new Vector3(Input.GetAxis("Vertical"),0,-Input.GetAxis("Horizontal")).normalized;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            walking.PlayOneShot(walking.clip);
        }
        else
        {
            if (Input.GetButtonUp("Vertical"))
            {
                walking.Stop();
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            walking.PlayOneShot(walking.clip);
        }
        else
        {
            if (Input.GetButtonUp("Horizontal"))
            {
                walking.Stop();
            }
        }
        // move in direction indicated by controller regardless of what the controller is
        //body.AddForce(Input.GetAxis("Vertical")/5,0,-Input.GetAxis("Horizontal")/5,ForceMode.VelocityChange);
    }
}
