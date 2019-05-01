using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autotrigger : MonoBehaviour
{
    public GameObject triggerprefab;

    public GameObject endpath;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("chicksleft"); i++)
        {
            Vector3 temp = Vector3.Lerp(gameObject.transform.position, endpath.transform.position,
                ((float) i / (PlayerPrefs.GetInt("chicksleft"))));
            Instantiate(triggerprefab, temp,Quaternion.identity);
        }
    }
}
