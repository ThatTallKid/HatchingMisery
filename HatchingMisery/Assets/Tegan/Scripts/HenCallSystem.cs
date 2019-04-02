using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class HenCallSystem : MonoBehaviour
{
    public Button follow;
    public Button stopFollow;

    public BabyChickV2.ChickState currentState;

    // Start is called before the first frame update
    void Start()
    {
        follow.onClick.AddListener(Follow);
        stopFollow.onClick.AddListener(StopFollow);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Follow()
    {
        currentState = BabyChickV2.ChickState.Following;
        Debug.Log("Chicks are following");
    }

    void StopFollow()
    {
        currentState = BabyChickV2.ChickState.Wandering;
        Debug.Log("Chicks have stopped following");
    }
}
