﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public Vector3 Angle;

    private void Awake()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        // to modify the camera positions until they are right these are both in update
        // set the camera rotation to the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Angle),Time.deltaTime);
        //transform.rotation.eulerAngles.Set(Angle.x,Angle.y,Angle.z);
        // set the camera position to the player position + an offset where the player is assumed to be the parent
        
        transform.position = Vector3.Lerp(transform.position,target.transform.position + offset,Time.deltaTime);
    }
}
