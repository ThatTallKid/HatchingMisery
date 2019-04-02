using UnityEngine;
using UnityEngine.UI;


public class HenCallSystem : MonoBehaviour
{
    public Button follow;
    public Button stopFollow;

    private BabyChickV2[] chicks;

    void Start()
    {
        // When the follow button is clicked, listen to the Follow function
        follow.onClick.AddListener(Follow);
        // When the stopFollow button is clicked, listen to the StopFollow function
        stopFollow.onClick.AddListener(StopFollow);
        
        // Initializing the chicks array
        chicks = FindObjectsOfType<BabyChickV2>();
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
        // Foreach chick, change their state to 'wandering' (temporary)
        foreach (BabyChickV2 chick in chicks)
        {
            chick.CurrentState = BabyChickV2.ChickState.Wandering;
        }
    }
}
