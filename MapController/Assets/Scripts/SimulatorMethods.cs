using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System;

public class SimulatorMethods : MonoBehaviour {

    public static SimulatorMethods instance;

    public static InputSimulator sim;


    Vector2 screenSize;
    /// <summary>
    /// InputSimulator use diffrent scale for cursor positon
    /// </summary>
    double scaledSimSize = 65535;
    double xOnePixelSimSize;
    double yOnePixelSimSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (sim == null)
            sim = new InputSimulator();
        screenSize = new Vector2 (UnityEngine.Screen.currentResolution.width, UnityEngine.Screen.currentResolution.height);
        xOnePixelSimSize = scaledSimSize / screenSize.x;
        yOnePixelSimSize = scaledSimSize / screenSize.y;
    }

    public static void KeyDown(VirtualKeyCode code)
    {
        switch (code)
        {
            case VirtualKeyCode.LBUTTON:
                sim.Mouse.LeftButtonDown();
                return;
            case VirtualKeyCode.RBUTTON:
                sim.Mouse.RightButtonDown();
                return;
            default:
                sim.Keyboard.KeyDown(code);
                return;
        }
    }

    public static void KeyPress(VirtualKeyCode code)
    {
        switch (code)
        {
            case VirtualKeyCode.LBUTTON:
                sim.Mouse.LeftButtonDown();
                return;
            case VirtualKeyCode.RBUTTON:
                sim.Mouse.RightButtonDown();
                return;
            default:
                sim.Keyboard.KeyPress(code);
                return;
        }
    }

    public static void KeyUp(VirtualKeyCode code)
    {
        switch (code)
        {
            case VirtualKeyCode.LBUTTON:
                sim.Mouse.LeftButtonUp();
                return;
            case VirtualKeyCode.RBUTTON:
                sim.Mouse.RightButtonUp();
                return;
            default:
                sim.Keyboard.KeyUp(code);
                return;
        }
    }

    public void MouseMoveBy(int x, int y)
    {
        sim.Mouse.MoveMouseBy(x, y);
    }

    public void MouseSetPos(int x, int y)
    {
        sim.Mouse.MoveMouseTo(
            Convert.ToDouble(x * xOnePixelSimSize),
            Convert.ToDouble(y * yOnePixelSimSize)
            );
    }

    Point MouseGetPosition()
    {
        return GetPosMethods.GetCursorPosition();
    }
}

public static class GetPosMethods
{
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);

    public static Point GetCursorPosition()
    {
        Point lpPoint;
        GetCursorPos(out lpPoint);
        return lpPoint;
    }
}
