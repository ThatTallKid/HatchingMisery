using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chick"))
        {
            other.GetComponent<BabyChickV2>().Inmud++;
        }
        if(other.gameObject.name =="Hen")
        {
            Debug.Log("test2");
            other.GetComponent<HenMovement>().Inmud++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chick"))
        {
            other.GetComponent<BabyChickV2>().Inmud--;
        }
        if(other.gameObject.name =="Hen")
        {
            Debug.Log("test");
            other.GetComponent<HenMovement>().Inmud--;
        }
    }
}
