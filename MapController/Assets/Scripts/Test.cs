using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using WindowsInput.Native;
using WindowsInput;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Windows.Forms;
using System;
using System.Drawing;

public class Test : MonoBehaviour
{

    public Text text;

    static InputSimulator sim;
    // Use this for initialization
    void Start()
    {

        if (sim == null)
            sim = new InputSimulator();
        /*
         MainManager

         
         */


    }

    float timeToReach = 0f;
    int amount;

    float X;
    float Y;

    double a = 65535 / 2f;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            WindowsInput.MouseSimulator m = null;
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            
            if (testState.IsConnected)
            {
                X = testState.ThumbSticks.Left.X;
                Y = testState.ThumbSticks.Left.Y;
                if (Mathf.Abs(X) > 0.25f || Mathf.Abs(Y) > 0.25f)
                {
                    //text.text = (amount++).ToString();
                    //sim.Keyboard.KeyPress(VirtualKeyCode.VK_A);

                    Vector2 dir = new Vector2(X, Y).normalized;

                    //sim.Mouse.MoveMouseTo(a, a);
                    //sim.Mouse.MoveMouseBy((int)(testState.ThumbSticks.Left.X * 20), (int)(testState.ThumbSticks.Left.Y * -20));

                    sim.Mouse.MoveMouseTo(
                        Convert.ToDouble(a + (int)(dir.x * 3000000 / UnityEngine.Screen.currentResolution.width)),
                        Convert.ToDouble(a + (int)(dir.y * -3000000 / UnityEngine.Screen.currentResolution.height))
                        );


                    sim.Mouse.LeftButtonDown();

                    //sim.Mouse.MoveMouseToPositionOnVirtualDesktop(a, a);

                    text.text = GetPosMethods.GetCursorPosition().X + " " + GetPosMethods.GetCursorPosition().Y;

                    timeToReach = Time.time + 2f;
                    sim.Mouse.LeftButtonUp();
                }
                
            }
        }
        //if (Input.anyKey)
        //{
        //    Debug.Log("?");
        //}
    }
}
