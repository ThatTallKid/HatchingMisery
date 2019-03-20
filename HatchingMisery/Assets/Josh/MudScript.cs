using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudScript : MonoBehaviour
{
    public float slowspeed = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chick"))
        {
            other.GetComponent<BabyChickV2>().FollowSpeed *= slowspeed;
        }
        if(other.gameObject.layer.Equals(8))
        {
            other.GetComponent<HenMovement>().Nav.speed *= slowspeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chick"))
        {
            other.GetComponent<BabyChickV2>().FollowSpeed /= slowspeed;
        }
        if(other.gameObject.layer.Equals(8))
        {
            other.GetComponent<HenMovement>().Nav.speed /= slowspeed;
        }
    }
}
