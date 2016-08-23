using N;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace N.Package.Input.Draggable
{
    /// Various utility functions
    public class Draggable
    {
        /// Return the instance of the draggable manager on the scene or error.
        public static DraggableManager RequireManager()
        {
            Draggable.RequireRawInput();
            var instance = Scene.FindComponent<DraggableManager>();
            if (instance == null)
            {
                throw new Exception("A DraggableManager must be present on the scene to use Draggable");
            }
            return instance;
        }

        /// Error if the scene does not have a RawInputListener
        public static void RequireRawInput()
        {
            var instance = Scene.FindComponent<RawInputListener>();
            if (instance == null)
            {
                throw new Exception("A RawInputListener must be present on the scene to use Draggable");
            }
        }
    }
}