using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = System.Object;
    

public class HenCallSystem : MonoBehaviour
{
    public Button follow;
    public Button stopFollow;

    public BabyChickV2[] chicks;
  
   // Start is called before the first frame update
    void Start()
    {
        follow.onClick.AddListener(Follow);
        stopFollow.onClick.AddListener(StopFollow);
        
        chicks = FindObjectsOfType<BabyChickV2>();
     
        if (chicks.Equals(0))
        {
            Debug.Log("No chicks were found");
        }            
    }

    void Follow()
    {
        foreach (BabyChickV2 chick in chicks)
        {
            BabyChickV2.CurrentState = BabyChickV2.ChickState.Following;
        }
    }
   
    void StopFollow()
    {
        foreach (BabyChickV2 chick in chicks)
        {
            BabyChickV2.CurrentState = BabyChickV2.ChickState.Wandering;
        }
    }
}
