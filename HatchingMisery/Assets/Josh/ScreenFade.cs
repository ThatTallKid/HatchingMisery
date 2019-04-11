using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public Renderer[] dithermat;
    public float timer = 7;
    public float amount = 1;

    private void Awake()
    {
        dithermat = gameObject.GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (timer <= 2)
        {
            timer += Time.deltaTime;
            foreach (var item in dithermat)
            {
                if(item.material.shader.name == "Dither")item.material.SetFloat("_Transparency",Mathf.Lerp(item.material.GetFloat("_Transparency"),1/(amount+2),0.1f));
            }
        }
        else if (timer <= 3)
        {
            timer += Time.deltaTime;
            foreach (var item in dithermat)
            {
                if(item.material.shader.name == "Dither")item.material.SetFloat("_Transparency",Mathf.Lerp(item.material.GetFloat("_Transparency"),1f,(timer-2f)));
            }
        }
    }
}
