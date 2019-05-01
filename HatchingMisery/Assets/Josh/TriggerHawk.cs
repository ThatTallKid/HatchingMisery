using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TriggerHawk : MonoBehaviour
{
    public bool Swooping
    {
        get => swooping;
        set
        {
            if (value == true)
            {
                hawkCall.PlayOneShot(hawkCall.clip);
            }
            swooping = value;
        }
    }

    public bool swooping = false;

    public float speed = 1;
    private float swoopvalue = 0;
    public float sideswoopdist = 18;
    private Vector3 target;

    public bool haschick = false;
    public Animator anims;
    public AudioSource hawkCall;
    public AudioSource chickScream;
    private GameTime chicks;

    private Vector3 startswooppoint;

    private Vector3 endswooppoint;

    public Renderer show;
    // Start is called before the first frame update
    void Awake()
    {
        chicks = GameObject.FindObjectOfType<GameTime>();
        anims.SetBool("GLIDING", true);
        anims.SetBool("Grab", false);
        target = transform.parent.position;
        if (Random.value > 0.5f)
        {
            startswooppoint = new Vector3(target.x,target.y+10,target.z-sideswoopdist);
            endswooppoint = new Vector3(target.x,target.y+10,target.z+sideswoopdist);
        }
        else
        {
            startswooppoint = new Vector3(target.x,target.y+10,target.z+sideswoopdist);
            endswooppoint = new Vector3(target.x,target.y+10,target.z-sideswoopdist);
        }
        gameObject.transform.SetParent(null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Swooping)
        {
            show.enabled = true;
            swoopvalue += Time.fixedDeltaTime;
            Vector3 gradient = Vector3.zero;
            float startdist = Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z),
                new Vector2(target.x, target.z));
            float dist = startdist / Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z), new Vector2(endswooppoint.x, endswooppoint.z));
            dist *= speed;

            // setup waypoints to have the Hawk smoothly follow
            Vector3 templerpstart = Vector3.Lerp(Vector3.Lerp(startswooppoint, new Vector3(startswooppoint.x, target.y, startswooppoint.z), swoopvalue / dist),
                Vector3.Lerp(new Vector3(startswooppoint.x, target.y, startswooppoint.z), target, swoopvalue / dist),
                swoopvalue / dist);
            Vector3 templerpend = Vector3.Lerp(Vector3.Lerp(target, new Vector3(endswooppoint.x, target.y, endswooppoint.z), ((swoopvalue - dist) / (speed - dist))),
                Vector3.Lerp(new Vector3(endswooppoint.x, target.y, endswooppoint.z), endswooppoint, ((swoopvalue - dist) / (speed - dist))),
                ((swoopvalue - dist) / (speed - dist)));
            if (swoopvalue <= dist)
            {
                gradient = templerpstart - gameObject.transform.position;
                gameObject.transform.position = templerpstart;
            }
            else
            {
                gradient = templerpend - gameObject.transform.position;
                gameObject.transform.position = templerpend;
            }
            if (swoopvalue >= speed)
            {
                show.enabled = false;
                Swooping = false;
            }

            transform.LookAt(gameObject.transform.position + gradient);
        }
    }

    public void triggered()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chick"))
        {
            GameObject temp = chicks.Chicks[0];
            if (temp&&!haschick)
            {
                haschick = true;
                anims.SetBool("Grab", true);
                chickScream.Play();
                temp.gameObject.GetComponent<BabyChickV2>().enabled = false;
                temp.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                //todo maybe later move the chicks to a hold position and deleted them off screen? doesn't really matter is
                //other.transform.SetParent(transform);
                chicks.Chicks.Remove(temp.gameObject);
                PlayerPrefs.SetInt("chicksleft", PlayerPrefs.GetInt("chicksleft") - 1);
                Destroy(temp.gameObject);
            }
        }
    }
}
