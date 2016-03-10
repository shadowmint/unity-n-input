using UnityEngine;
using N;
using N.Package.Input.Draggable;

public class DummyReceiveScale : MonoBehaviour
{

    bool scale = false;

    public bool accept;

    public void CheckSource(DraggableEvent query)
    {
        query.accept = this.accept;
    }

    /// Default leave event
    public void EnterHandler(DraggableEvent ep)
    {
        if (ep.accept && !scale)
        {
            this.gameObject.transform.localScale = new Vector3(2, 1, 2);
            scale = true;
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 0.5f);
            scale = true;
        }
    }

    /// Default leave event
    public void LeaveHandler(DraggableEvent target)
    {
        ResetScale();
    }

    /// Default receiver for a drop/click event
    public void AcceptHandler(DraggableEvent target)
    {
        ResetScale();
    }

    private void ResetScale()
    {
        if (scale)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            this.scale = false;
        }
    }
}
