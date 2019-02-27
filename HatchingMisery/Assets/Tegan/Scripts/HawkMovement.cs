using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkMovement : HawkBase
{
    public Rigidbody rb;

    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;

    public Vector3 startPos;
    
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    public override void Execute()
    {
        base.Execute();
        
        startPos.x += horizontalSpeed;
        startPos.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;

        transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
