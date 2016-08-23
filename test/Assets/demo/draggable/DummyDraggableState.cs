using UnityEngine;
using N;
using N.Package.Core;
using N.Package.Input.Draggable;

public class DummyDraggableState : MonoBehaviour
{
  public Color good;
  public Color bad;
  public Color idle;
  public bool dragReady = true;

  public void IsDragReady(DraggableEvent target)
  {
    target.accept = dragReady;
  }

  public void OnStart(DraggableEvent ep)
  {
    var origin = ep.source.GameObject.AddComponent<Origin>();
    origin.Bind();
  }

  public void OnStop(DraggableEvent ep)
  {
    var origin = ep.source.GameObject.GetComponent<Origin>();
    ep.source.GameObject.Move(origin.position);
    ep.source.GameObject.GetComponent<Renderer>().material.color = idle;
    if (ep.source.DragCursor != null)
    {
      ep.source.DragCursor.GetComponent<Renderer>().material.color = idle;
    }
  }

  public void EnterTarget(DraggableEvent ep)
  {
    if (ep.accept)
    {
      ep.source.GameObject.GetComponent<Renderer>().material.color = good;
      if (ep.source.DragCursor != null)
      {
        ep.source.DragCursor.GetComponent<Renderer>().material.color = good;
      }
    }
    else
    {
      ep.source.GameObject.GetComponent<Renderer>().material.color = bad;
      if (ep.source.DragCursor != null)
      {
        ep.source.DragCursor.GetComponent<Renderer>().material.color = bad;
      }
    }
  }

  public void LeaveTarget(DraggableEvent ep)
  {
    ep.source.GameObject.GetComponent<Renderer>().material.color = idle;
    if (ep.source.DragCursor != null)
    {
      ep.source.DragCursor.GetComponent<Renderer>().material.color = idle;
    }
  }

  public void OnReceive(DraggableEvent rp)
  {
    GameObject.Destroy(rp.source.GameObject);
  }
}