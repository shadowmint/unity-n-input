using UnityEngine;
using N;
using N.Package.Input.Draggable;

public class DummyDraggableState : MonoBehaviour
{

    public Color good;
    public Color bad;
    public Color idle;
    public bool savedIdle;

    private void Color(DraggableSource target, Color c)
    {
        if (target != null)
        {
            var gp = target.GetCursor() != null ? target.GetCursor() : target.gameObject;
            var renderer = gp.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = c;
            }
        }
    }

    public void OnIdle(DraggableSource target)
    {
        if (!savedIdle)
        {
            idle = target.gameObject.GetComponent<MeshRenderer>().material.color;
            savedIdle = true;
        }
        Color(target, idle);
    }

    public void OnGood(DraggableSource target)
    {
        Color(target, good);
    }

    public void OnBad(DraggableSource target)
    {
        Color(target, bad);
    }

    public void DropHandler(DraggableSource target)
    {
        target.gameObject.Move(target.origin);
        Color(target, idle);
    }
}
