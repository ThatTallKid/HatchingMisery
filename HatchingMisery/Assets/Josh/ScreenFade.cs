using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public List<Material> dithermat;
    public float timer = 7;
    public float amount = 1;

    private void Awake()
    {
        var temp = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer item in temp)
        {
            foreach (Material mat in item.materials)
            {
                if(mat.shader.name=="Custom/Dither")dithermat.Add(mat);
            }
        }
        
    }

    private void Update()
    {
        if (timer <= 2)
        {
            timer += Time.deltaTime;
            foreach (Material mat in dithermat)
            {
                ditherer(mat,1/(amount+2),0.1f);
            }
        }
        else if (timer <= 3)
        {
            timer += Time.deltaTime;
            foreach (Material mat in dithermat)
            {
                ditherer(mat,1f,(timer-2f));
            }
        }
    }

    private void ditherer(Material a, float b,float c)
    {
        a.SetFloat("_Transparency", Mathf.Lerp(a.GetFloat("_Transparency"), b, c));
    }
}
