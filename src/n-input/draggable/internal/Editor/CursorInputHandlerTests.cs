#if N_INPUT_TESTS
using UnityEngine;
using N;
using N.Package.Input;
using N.Package.Events;
using System.Collections.Generic;
using NUnit.Framework;
using N.Package.Input.Draggable.Internal;
using N.Package.Input.Draggable;

public class CursorInputHandlerTests : N.Tests.Test
{
    private class FakeSource : DraggableBase, IDraggableSource
    {
        public IDraggableSource Source { get { return this; } }

        public GameObject DragCursor { get { return null; } }

        public GameObject GameObject { get { return this.gameObject; } }

        public bool DragObject { get { return false; } }

        public bool CanDragStart() { return true; }

        public void OnDragStart() {}
        public void OnReceivedBy(IDraggableReceiver receiver) {}
        public void OverValidTarget(IDraggableReceiver target) {}
        public void OverInvalidTarget(IDraggableReceiver target) {}
    }

    private class FakeReceiver : DraggableBase, IDraggableReceiver
    {
        public IDraggableReceiver Receiver { get { return this; } }

        public GameObject GameObject { get { return this.gameObject; } }

        public bool IsValidDraggable(IDraggableSource draggable) { return true; }

        public void DraggableEntered(IDraggableSource draggable, bool isValid) {}

        public void DraggableLeft(IDraggableSource draggable) {}

        public void OnReceiveDraggable(IDraggableSource draggable) {}
    }

    [Test]
    public void test_create_instance()
    {
        var dummy = this.SpawnBlank();
        var instance = new CursorInputHandler(dummy);
        Assert(instance != null);
    }

    [Test]
    public void test_start_dragging()
    {
        var dummy = this.SpawnBlank();
        var instance = new CursorInputHandler(dummy);
        Assert(instance != null);
    }
}
#endif
