using UnityEngine;
using System.Collections.Generic;
using N.Package.Input.Next;

public class BoxActor : Actor
{
    public bool left;
    public bool right;
    public bool forward;

    public float speed;
    public float maxSpeed;

    public float spin;

    public float distance;
    public GameObject compass;

    /// Execute an action on this actor
    public override void Trigger<TAction>(TAction action)
    {
        switch ((MyEventType)(object)action)
        {
            case MyEventType.START_FORWARDS:
                forward = true;
                break;
            case MyEventType.STOP_FORWARDS:
                forward = false;
                break;
            case MyEventType.START_TURN_LEFT:
                left = true;
                break;
            case MyEventType.STOP_TURN_LEFT:
                left = false;
                break;
            case MyEventType.START_TURN_RIGHT:
                right = true;
                break;
            case MyEventType.STOP_TURN_RIGHT:
                right = false;
                break;
        }
    }

    public void Update()
    {
        // Trigger events
        this.TriggerPending<MyEventType>();

        // Update compass marker position
        var direction = (compass.transform.position - transform.position).normalized;
        var position = transform.position + direction * distance;
        position[1] = transform.position[1]; // Always match y axis
        compass.transform.position = position;

        // Update motion
        var rb = GetComponent<Rigidbody>();
        if (forward)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (direction.magnitude > 0)
                {
                    rb.AddForce(direction * speed * Time.deltaTime);
                }
            }
        }
        if (left)
        {
            compass.transform.RotateAround(transform.position, Vector3.up, -spin * Time.deltaTime);
        }
        if (right)
        {
            compass.transform.RotateAround(transform.position, Vector3.up, spin * Time.deltaTime);
        }
    }
}
