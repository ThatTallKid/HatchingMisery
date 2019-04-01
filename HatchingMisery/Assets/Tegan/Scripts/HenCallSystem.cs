using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

    

public class HenCallSystem : MonoBehaviour
{
    public Button follow;
    public Button stopFollow;

    public BabyChickV2[] chicks;
  
    void Start()
    {
        //When the follow button is clicked, listen to the Follow function
        follow.onClick.AddListener(Follow);
        //When the stopFollow button is clicked, listen to the StopFollow function
        stopFollow.onClick.AddListener(StopFollow);
        
        //Initializing the chicks array
        chicks = FindObjectsOfType<BabyChickV2>();
     
        //Log a message if no chicks were found
        if (chicks.Equals(0))
        {
            Debug.Log("No chicks were found");
        }            
    }

    void Follow()
    {
        //Foreach chick, change their state to following
        foreach (BabyChickV2 chick in chicks)
        {
            chick.currentState = BabyChickV2.ChickState.Following;
        }
    }
   
    void StopFollow()
    {
        //Foreach chick, change their state to wandering
        foreach (BabyChickV2 chick in chicks)
        {
            chick.currentState = BabyChickV2.ChickState.Wandering;
        }
    }
}
