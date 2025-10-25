using System;
using System.Drawing;

namespace MGestures.Core
{
    public abstract class GestureContext// : MarshalByRefObject
    {
        public Point StartPoint;
        public Point EndPoint;

        public uint ProcId;
        public IntPtr WinId;

        public GestureTriggerButton GestureButton;

        public abstract void ActivateTargetWindow();

    }
}