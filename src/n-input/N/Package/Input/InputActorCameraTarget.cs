using UnityEngine;

namespace N.Package.Input.Example
{
    public class InputActorCameraTarget : MonoBehaviour
    {
        public GameObject target;

        public void Update()
        {
            transform.position = target.transform.position;
        }
    }
}