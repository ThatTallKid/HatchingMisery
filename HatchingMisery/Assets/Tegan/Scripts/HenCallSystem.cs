﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HenCallSystem : MonoBehaviour
{
    private GameTime chicks;
    public float radius = 3;

    void Start()
    {
        chicks = FindObjectOfType<GameTime>();
    }

    private void Update()
    {
        if (HenCallSystemControls.AButton())
        {
            Follow();
        }

        if (HenCallSystemControls.BButton())
        {
            StopFollow();
        }
    }

    void Follow()
    {
        // Foreach chick, change their state to 'following'
        foreach (GameObject chick in chicks.Chicks)
        {
            BabyChickV2 temp = chick.GetComponent<BabyChickV2>();
            
            if (temp.CurrentState != BabyChickV2.ChickState.Feeding)
            {
                if (Vector3.Distance(temp.transform.position, transform.position) <= radius)
                {
                    chick.GetComponent<BabyChickV2>().CurrentState = BabyChickV2.ChickState.Following;
                }
            }
        }
    }

    void StopFollow()
    {
        // Foreach chick, change their state to 'stopping'
        foreach (GameObject chick in chicks.Chicks)
        {
            BabyChickV2 temp = chick.GetComponent<BabyChickV2>();
            if (temp.CurrentState != BabyChickV2.ChickState.Feeding&&temp.CurrentState == BabyChickV2.ChickState.Following)
            {
                if (Vector3.Distance(temp.transform.position, transform.position) <= radius)
                {
                    chick.GetComponent<BabyChickV2>().CurrentState = BabyChickV2.ChickState.Stopping;
                }
            }
        }
    }
}
