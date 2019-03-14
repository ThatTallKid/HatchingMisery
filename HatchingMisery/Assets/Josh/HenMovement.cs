using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenMovement : MonoBehaviour
{
    public Rigidbody body;
    public int chickamount;
    public GameObject ChickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // only get the gameobject once rather than once per frame
        body = gameObject.GetComponent<Rigidbody>();
        GameTime tempgame = FindObjectOfType<GameTime>();
        if (tempgame)
        {
            GameTime.checklevel();
            chickamount = PlayerPrefs.GetInt("chicksleft");
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move in direction indicated by controller regardless of what the controller is
        body.AddForce(Input.GetAxis("Vertical")/5,0,-Input.GetAxis("Horizontal")/5,ForceMode.VelocityChange);
    }
}
