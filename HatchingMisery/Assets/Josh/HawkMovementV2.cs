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
                    anims.SetBool("Grab", false);

                }
                if (value == states.Swooping)
                {
                    hawkCall.PlayOneShot(hawkCall.clip);
                }
            }
            currentstate = value;
        }
    }

    public Projector shadow;
    public Renderer show;
    public Animator anims;
    public AudioSource hawkCall;
    public AudioSource chickScream;

    // start off screen a distance, above the camera height
    public float startswoopheight;
    public float idletime = 7;
    public float warningtime = 7;
    public float totalswooptime = 5;
    public int maxchicksperswoop = 2;

    private Vector3 target = Vector3.zero;
    private float swoopvalue = 0;
    private Vector3 startswooppoint = Vector3.zero;
    private Vector3 endswooppoint = Vector3.zero;
    private float searchtimer = 0;
    private int heldchicks = 0;

    private GameTime chicks;

    // Start is called before the first frame update
    void Awake()
    {
        chicks = GameObject.FindObjectOfType<GameTime>();
        anims.SetBool("GLIDING", true);
        anims.SetBool("Grab", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // no need to draw object
        show.enabled = (Currentstate == states.Swooping);
        shadow.enabled = (Currentstate == states.Warning);
        if (Currentstate == states.Elsewhere)
        {
            searchtimer += Time.deltaTime;
            if (searchtimer >= idletime)
            {
                searchtimer = 0;
                Currentstate = states.Surveying;
            }
        }

        if (Currentstate == states.Surveying)
        {
            Vector3 temptarget = Vector3.zero;
            foreach (GameObject item in chicks.Chicks)
            {
                temptarget += item.transform.position;
            }

            temptarget /= chicks.Chicks.Count;
            temptarget.y = 0.5f;
            target = temptarget;
            RaycastHit hit;
            //todo uses height of collider to tell if it is shelter, change to tag later
            if (Physics.Raycast(target + (5 * Vector3.up), Vector3.down, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("HawkCol"))
                {
                    Currentstate = states.Elsewhere;
                }
                else
                {
                    // choose the start and end points and report
                    float temp = choosedir(target);
                    Debug.Log(temp);
                    target.y = 1;
                    Currentstate = states.Warning;
                }
            }
        }
        // set state to warning then swap to swooping and reverse along the path
        // don't do this all the time, have it cut off when the material color is within a limit of the target
        if (Currentstate == states.Warning)
        {
            swoopvalue += Time.fixedDeltaTime;
            //todo when we have the projector material installed re-enable the projector
            //shadow.farClipPlane = Mathf.Lerp(startswoopheight, startswoopheight + 4,Mathf.InverseLerp(0,Vector3.Distance(startswooppoint,endswooppoint),Vector3.Distance(transform.position,startswooppoint)));
            //shadow.material.color = new Color(shadow.material.color.r,shadow.material.color.g,shadow.material.color.b,Mathf.Lerp(0,1,Mathf.InverseLerp(0,Vector3.Distance(startswooppoint,endswooppoint),Vector3.Distance(transform.position,startswooppoint))));
            gameObject.transform.position = Vector3.Lerp(endswooppoint, startswooppoint, swoopvalue / warningtime);
            gameObject.transform.rotation = Quaternion.LookRotation((startswooppoint - endswooppoint).normalized, Vector3.up);
            if (swoopvalue >= warningtime)
            {
                Currentstate = states.Swooping;
                swoopvalue = 0;
            }
        }
        else if (Currentstate == states.Swooping)
        {
            swoopvalue += Time.fixedDeltaTime;
            Vector3 gradient = Vector3.zero;
            float startdist = Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z),
                new Vector2(target.x, target.z));
            float dist = startdist / Vector2.Distance(new Vector2(startswooppoint.x, startswooppoint.z), new Vector2(endswooppoint.x, endswooppoint.z));
            dist *= totalswooptime;

            // setup waypoints to have the Hawk smoothly follow
            Vector3 templerpstart = Vector3.Lerp(Vector3.Lerp(startswooppoint, new Vector3(startswooppoint.x, target.y, startswooppoint.z), swoopvalue / dist),
                Vector3.Lerp(new Vector3(startswooppoint.x, target.y, startswooppoint.z), target, swoopvalue / dist),
                swoopvalue / dist);
            Vector3 templerpend = Vector3.Lerp(Vector3.Lerp(target, new Vector3(endswooppoint.x, target.y, endswooppoint.z), ((swoopvalue - dist) / (totalswooptime - dist))),
                Vector3.Lerp(new Vector3(endswooppoint.x, target.y, endswooppoint.z), endswooppoint, ((swoopvalue - dist) / (totalswooptime - dist))),
                ((swoopvalue - dist) / (totalswooptime - dist)));
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
            if (swoopvalue >= totalswooptime)
            {
                Currentstate = states.Elsewhere;
                heldchicks = 0;
            }

            transform.LookAt(gameObject.transform.position + gradient);
        }
    }

    public RaycastHit checktarget(Vector3 target, Vector3 direction)
    {
        RaycastHit hit;
        // get layer mask working correctly, use for hidden collisions of objects player can go under
        string[] tempnames = { "Default", "HawkCol" };
        LayerMask temp = LayerMask.GetMask(tempnames);
        Physics.Raycast(target + new Vector3(0, 0, 0), direction, out hit, 10000, temp);

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
            Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
            RaycastHit forward = checktarget(target, direction);
            RaycastHit backward = checktarget(target, -direction);
            if (Vector3.Distance(forward.point, backward.point) > largestdist)
            {
                largestdist = Vector3.Distance(forward.point, backward.point);
                largestdir = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                tempforward = forward;
                tempbackward = backward;
            }
        }

        // use the abs of the hit colliders to get the height of the end points
        if (tempforward.collider)
        {
            if (tempforward.collider.bounds.max.y > startswoopheight)
            {
                startswooppoint = new Vector3(tempforward.point.x, tempforward.collider.bounds.max.y, tempforward.point.z);
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
                endswooppoint = new Vector3(tempbackward.point.x, tempbackward.collider.bounds.max.y, tempbackward.point.z);
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
            if (heldchicks == 0)
            {
                anims.SetBool("Grab", true);
                chickScream.Play();
            }
            if (heldchicks < maxchicksperswoop)
            {
                other.gameObject.GetComponent<BabyChickV2>().enabled = false;
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                //todo maybe later move the chicks to a hold position and deleted them off screen? doesn't really matter is
                //other.transform.SetParent(transform);
                chicks.Chicks.Remove(other.gameObject);
                PlayerPrefs.SetInt("chicksleft", PlayerPrefs.GetInt("chicksleft") - 1);
                Destroy(other.gameObject);
                heldchicks++;
            }
        }
    }
}
