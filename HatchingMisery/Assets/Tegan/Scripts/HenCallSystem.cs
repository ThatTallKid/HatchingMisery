using UnityEngine;
using UnityEngine.UI;


public class HenCallSystem : MonoBehaviour
{
    private BabyChickV2[] chicks;

    void Start()
    {
        // Initializing the chicks array
        chicks = FindObjectsOfType<BabyChickV2>();
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
        foreach (BabyChickV2 chick in chicks)
        {
            chick.CurrentState = BabyChickV2.ChickState.Following;
        }
    }

    void StopFollow()
    {
        // Foreach chick, change their state to 'stopping'
        foreach (BabyChickV2 chick in chicks)
        {
            chick.CurrentState = BabyChickV2.ChickState.Stopping;
        }
    }
}
