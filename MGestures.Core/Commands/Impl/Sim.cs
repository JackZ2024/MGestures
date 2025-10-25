using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput;
using WindowsInput.Native;
using WindowsInput.Events;

namespace MGestures.Core.Commands.Impl
{
    static internal class Sim
    {
        //public static InputSimulator Simulator = new InputSimulator() { ExtraInfo = new IntPtr(19900620) };

        public static void KeyDown(KeyCode k)
        {
            //Simulator.Keyboard.KeyDown(k);
            Simulate.Events().Hold(k).Wait(100).Invoke();
        }

        public static void KeyUp(KeyCode k)
        {
            //Simulator.Keyboard.KeyUp(k);
            Simulate.Events().Release(k).Wait(100).Invoke();
        }

        public static void KeyPress(KeyCode k)
        {
            //Simulator.Keyboard.KeyPress(k);
            Simulate.Events().Click(k).Wait(1000).Invoke();
        }

        public static void TextEntry(string s)
        {
            //Simulator.Keyboard.TextEntry(s);
            Simulate.Events().Click(s).Invoke();
        }
    }
}
