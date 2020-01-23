using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Floater : MonoBehaviour
    {
        public List<Rigidbody> targets;
        
        public Vector3 power = Physics.gravity;

        public void OnTriggerEnter(Collider other)
        {
            var target = other.attachedRigidbody;
            targets.Add(target);
        }

        public void OnTriggerExit(Collider other)
        {
            var target = other.attachedRigidbody;
            targets.Remove(target);
        }

        public void Update()
        {
            targets.ForEach(i => i.AddForce(power * i.mass));
        }
    }
}