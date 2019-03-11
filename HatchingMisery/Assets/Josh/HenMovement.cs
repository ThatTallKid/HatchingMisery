using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenMovement : MonoBehaviour
{
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        // only get the gameobject once rather than once per frame
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // move in direction indicated by controller regardless of what the controller is
        body.AddForce(Input.GetAxis("Vertical")/5,0,-Input.GetAxis("Horizontal")/5,ForceMode.VelocityChange);
    }
}
