using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class HawkMovementV2 : MonoBehaviour
{
    public enum states
    {
        Elsewhere,
        Surveying,
        Warning,
        Swooping,
    }

    public states currentstate = states.Surveying;

    public states Currentstate
    {
        get => currentstate;
        set
        {
            if (currentstate != value)
            {
                if (value != states.Swooping)
                {
                    // reset swooping value
                    swoopvalue = 0;
                }
            }
            currentstate = value;
        } 
    }

    public Projector shadow;
    public Renderer show;

    // start off screen a distance, above the camera height
    public float startswoopheight;

    public Vector3 target;

    public float minimundist = 5;
    public float maxdist = 20;
    public float minheight = 0.5f;

    public float swoopvalue = 0;
    public Vector3 startswooppoint = Vector3.zero;
    public Vector3 endswooppoint = Vector3.zero;

    public GameTime chicks;

    public float searchtimer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        chicks = GameObject.FindObjectOfType<GameTime>();
    }

    // Update is called once per frame
    void Update()
    {
        // no need to draw object
        show.enabled = (Currentstate==states.Swooping);
        if (currentstate == states.Elsewhere)
        {
            searchtimer += Time.deltaTime;
            if (searchtimer >= 7)
            {
                searchtimer = 0;
                Currentstate = states.Surveying;
            }
        }

        if (currentstate == states.Surveying)
        {
            Vector3 temptarget = Vector3.zero;
            foreach (GameObject item in chicks.Chicks)
            {
                temptarget += item.transform.position;
            }

            temptarget /= chicks.Chicks.Count;
            temptarget.y = minheight;
            target = temptarget;
            RaycastHit hit;
            if (Physics.Raycast(target + (5 * Vector3.up), Vector3.down, out hit))
            {
                if (hit.collider.bounds.max.y>target.y+0.5f)
                {
                    Currentstate = states.Elsewhere;
                }
                else
                {
                    Debug.Log(choosedir(target));
                    currentstate = states.Warning;
                }
            }
        }
        // set state to warning then swap to swooping and reverse along the path
        // don't do this all the time, have it cut off when the material color is within a limit of the target
        if (currentstate == states.Warning)
        {
            swoopvalue += Time.deltaTime;
            //shadow.material.color = new Color(shadow.material.color.r,shadow.material.color.g,shadow.material.color.b,Mathf.Lerp(0,1,Vector3.Distance(transform.position,startswooppoint)));
            gameObject.transform.position = Vector3.Lerp(endswooppoint, startswooppoint,swoopvalue/6);
            gameObject.transform.rotation = Quaternion.LookRotation((startswooppoint-endswooppoint).normalized,Vector3.up);
            if (swoopvalue >= 6)
            {
                currentstate = states.Swooping;
                swoopvalue = 0;
            }
        }
        else if(currentstate == states.Swooping)
        {
            swoopvalue += Time.deltaTime;
            Vector3 gradient = Vector3.zero;
            float timevalue = 5;
            float startdist = Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z),
                new Vector2(target.x, target.z));
            float dist = startdist/ Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z),new Vector2(endswooppoint.x, endswooppoint.z));
            dist *= timevalue;
            
            // setup waypoints to have the Hawk smoothly follow
            Vector3 templerpstart = Vector3.Lerp(Vector3.Lerp(startswooppoint,new Vector3(startswooppoint.x,target.y,startswooppoint.z),swoopvalue/dist),
                Vector3.Lerp(new Vector3(startswooppoint.x,target.y,startswooppoint.z),target,swoopvalue/dist),
                swoopvalue/dist);
            Vector3 templerpend = Vector3.Lerp(Vector3.Lerp(target,new Vector3(endswooppoint.x,target.y,endswooppoint.z),((swoopvalue-dist)/(timevalue - dist))),
                Vector3.Lerp(new Vector3(endswooppoint.x,target.y,endswooppoint.z),endswooppoint,((swoopvalue-dist)/(timevalue-dist))),
                ((swoopvalue-dist)/(timevalue-dist)));
            if (swoopvalue <= dist)
            {
                //gameObject.GetComponent<Rigidbody>().MovePosition(templerpstart);
                gradient = templerpstart - gameObject.transform.position;
                //gameObject.GetComponent<Rigidbody>().AddForce(gradient,ForceMode.VelocityChange);
                gameObject.transform.position = templerpstart;
            }
            else
            {
                //gameObject.GetComponent<Rigidbody>().MovePosition(templerpend);
                gradient = templerpend - gameObject.transform.position;
                //gameObject.GetComponent<Rigidbody>().AddForce(gradient,ForceMode.VelocityChange);
                gameObject.transform.position = templerpend;
            }
            //gameObject.transform.position = Vector3.Lerp(templerpstart, templerpend,swoopvalue/dist);
            if (swoopvalue >= timevalue)
            {
                Currentstate = states.Elsewhere;
            }

            transform.LookAt(gameObject.transform.position+gradient);
        }
    }

    public RaycastHit checktarget(Vector3 target, Vector3 direction)
    {
        RaycastHit hit;
        // get layer mask working correctly, use for hidden collisions of objects player can go under
        Physics.Raycast(target+new Vector3(0,0,0), direction, out hit,10000,1);
        
        return hit;
    }

    // call this function every so often when we want to warn and swoop
    // will need changes for cutting off at a certain distance path for short and quick swoops later
    public float choosedir(Vector3 target)
    {
        float largestdist = 0;
        Vector3 largestdir = Vector3.zero;

        RaycastHit tempforward = new RaycastHit();
        RaycastHit tempbackward = new RaycastHit();
        
        for (float i = 0; i < 180; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            Vector3 direction = new Vector3(Mathf.Sin(rad),0,Mathf.Cos(rad));
            RaycastHit forward = checktarget(target,direction);
            RaycastHit backward = checktarget(target,-direction);
            if (Vector3.Distance(forward.point, backward.point) > largestdist)
            {
                largestdist = Vector3.Distance(forward.point, backward.point);
                largestdir = new Vector3(Mathf.Sin(rad),0,Mathf.Cos(rad));
                tempforward = forward;
                tempbackward = backward;
            }
        }

        // use the abs of the hit colliders to get the height of the end points
        if (tempforward.collider)
        {
            if (tempforward.collider.bounds.max.y > startswoopheight)
            {
                startswooppoint = new Vector3(tempforward.point.x, tempforward.collider.bounds.max.y,tempforward.point.z);
            }
            else
            {
                startswooppoint = new Vector3(tempforward.point.x, startswoopheight, tempforward.point.z);
            }
        }
        if (tempbackward.collider)
        {
            if (tempbackward.collider.bounds.max.y > startswoopheight)
            {
                endswooppoint = new Vector3(tempbackward.point.x, tempbackward.collider.bounds.max.y,tempbackward.point.z);
            }
            else
            {
                endswooppoint = new Vector3(tempbackward.point.x, startswoopheight, tempbackward.point.z);
            }
        }
        // have a max/min angle to cast from the end points onto the path
        // when swooping, have a trigger around the hawk that picks up chicks/kills them, perhaps wait until off screen
        return largestdist;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chick"))
        {
            other.gameObject.GetComponent<BabyChickV2>().enabled = false;
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            //other.transform.SetParent(transform);
            chicks.Chicks.Remove(other.gameObject);
            PlayerPrefs.SetInt("chicksleft", PlayerPrefs.GetInt("chicksleft")-1);
            Destroy(other.gameObject);
        }
    }
}
