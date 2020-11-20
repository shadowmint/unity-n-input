using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace N.Package.Input.Tooling
{
    public class InputGroundTrackerVolume : MonoBehaviour
    {
        public bool active;

        public int connected;

        public int layerMask;

        public List<GameObject> connectedObjects = new List<GameObject>();

        public void Update()
        {
            connectedObjects.RemoveAll(i => !i.gameObject.activeInHierarchy);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!active) return;
            if ((other.gameObject.layer & layerMask) != other.gameObject.layer) return;
            if (other.isTrigger) return;
            if (connectedObjects.Contains(other.gameObject)) return;
            connectedObjects.Add(other.gameObject);
            connected = connectedObjects.Count;
        }

        public void OnTriggerExit(Collider other)
        {
            if (!active) return;
            if (other.isTrigger) return;
            if (!connectedObjects.Contains(other.gameObject)) return;
            connectedObjects.Remove(other.gameObject);
            connected = connectedObjects.Count;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!active) return;
            if ((other.gameObject.layer & layerMask) == 0) return;
            if (other.isTrigger) return;
            if (connectedObjects.Contains(other.gameObject)) return;
            connectedObjects.Add(other.gameObject);
            connected = connectedObjects.Count;
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!active) return;
            if (other.isTrigger) return;
            if (!connectedObjects.Contains(other.gameObject)) return;
            connectedObjects.Remove(other.gameObject);
            connected = connectedObjects.Count;
        }

        /// <summary>
        /// Explicitly remove a tracked object; eg. if it is changed into a trigger.
        /// </summary>
        public void ExplicitRemove(GameObject externalObject)
        {
            if (connectedObjects.Contains(externalObject))
            {
                connectedObjects.Remove(externalObject);
                connected = connectedObjects.Count;
            }
        }
    }
}