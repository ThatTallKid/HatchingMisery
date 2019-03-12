using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Feed : MonoBehaviour   // Venus
{
    public int GrainAmount;
    public float feedtime;
    public bool feeding;

    void Update()
    {
        if (feeding == true)
        {
            feedtime = feedtime + Time.deltaTime;
        }

        if (feedtime > 4)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Chick"))
        {
            feeding = true;
        }
    }
}