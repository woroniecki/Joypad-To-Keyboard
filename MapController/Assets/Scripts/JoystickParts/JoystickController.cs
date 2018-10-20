using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class JoystickController : MonoBehaviour {

    public static GamePadState padState;

    int currentPadIndex;
    float timeToCheck = 0f;
    float delay = 2f;

    public void UpdateState()
    {
        if(currentPadIndex > -1)
        {
            padState = GamePad.GetState((PlayerIndex)currentPadIndex);
        }
    }

    private void Update()
    {
        if(timeToCheck < Time.time)
        {
            int padIndexToSubscribe = -1;
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);

                if (testState.IsConnected)
                {
                    padIndexToSubscribe = i;
                    break;
                }
            }
            currentPadIndex = padIndexToSubscribe;
            timeToCheck += delay;
        }
    }

    public static XInputDotNetPure.ButtonState GetButtonStateByValue(float value, float deadzone = 0.3f, bool positive = true)
    {
        if (positive)
        {
            if (value > deadzone)
                return ButtonState.Pressed;
            else
                return ButtonState.Released;
        }
        else
        {
            if (value < -deadzone)
                return ButtonState.Pressed;
            else
                return ButtonState.Released;
        }
    }

    public static float GetValueByButtonState (XInputDotNetPure.ButtonState state)
    {
        if (state == XInputDotNetPure.ButtonState.Pressed)
            return 1;
        return 0;
    }

}
