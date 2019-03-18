using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkMovement : HawkBase
{
    private Rigidbody hawkRigidBody;
    
    // Determines the hawk's forward moving (Z) speed
    public float speed;
    // Determines the hawk's Y rotation speed
    public float turnSpeed;
    
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;

    public override void Enter()
    {
        base.Enter();
        
        hawkRigidBody = GetComponent<Rigidbody>();
    }
    
    public override void Execute()
    {
       base.Execute();
       
       // Casts a ray from the transform of the hawk downwards
       Ray ray = new Ray(transform.position, -transform.up);
       
       RaycastHit hit;
       
       // If the hawk is within hover height from the ground, this will push the hawk away from the ground by applying some physics forces
       if (Physics.Raycast(ray, out hit, hoverHeight))
       {    
           // Push with a different amount of force depending on the height that that hawk is at (basically used to create smoother hovering)
           // Gives a value that will be used to represent the hawk's height (to modify force based on distance from ground)
           float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
           
           // Scale hawk's hover force based on the proportional height
           Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
           
           // Apply the hawk's hover force to the hawk's rigidbody, ignoring its mass
           hawkRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
       }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
