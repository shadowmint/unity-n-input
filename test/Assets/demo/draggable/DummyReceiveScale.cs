using UnityEngine;
using N;
using N.Package.Input.Draggable;

public class DummyReceiveScale : MonoBehaviour
{

    bool scale = false;

    public bool accept;

    /// Default receiver for an over/out event
    public void ReceiverHandler(ReceiveTarget target)
    {
        target.accept = accept;
        if (accept && !scale)
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
    public void LeaveHandler(DraggableSource target)
    {
        ResetScale();
    }

    /// Default receiver for a drop/click event
    public void AcceptHandler(DraggableSource target)
    {
        target.gameObject.Move(target.origin);
        ResetScale();
        _.Log("accepted target dropped");
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
