using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hawk
{
    public class HawkBase : MonoBehaviour
    {
        public Rigidbody hawkRigidBody;
        
        public virtual void Enter()
        {
            hawkRigidBody = GetComponent<Rigidbody>();
        }

        public virtual void Execute()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
