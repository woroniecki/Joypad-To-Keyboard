using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using XInputDotNetPure;

[System.Serializable]
public class KeyboardButtonPresser
{
    [System.NonSerialized]
    int maxAmount = 4;

    public enum KeyType
    {
        Click, Hold, Release_On_Second_Click
    }

    public List<VirtualKeyCode> keyCode;
    public List<KeyType> keyType;

    [System.NonSerialized]
    public ButtonState lastState = ButtonState.Released;
    [System.NonSerialized]
    public bool isPressed = false;

    public KeyboardButtonPresser()
    {
        keyCode = new List<VirtualKeyCode>();
        keyType = new List<KeyType>();
    }

    public void Execute(JoyButtonState state, ButtonState btnState = ButtonState.Released)
    {
        lastState = btnState;
        for (int i = 0; i < keyType.Count; i++)
        {
            //if (i == 0)
            ExecuteByType(state, i);
            //else
            //{
            //    int index = i;
            //    CoroutineMaster.ExecuteAfterNFrames(i, delegate { ExecuteByType(state, index); });
            //}
        }
    }

    private void ExecuteByType(JoyButtonState state, int i)
    {
        switch (keyType[i])
        {
            case KeyType.Click:
                ExecuteClick(state, i);
                break;
            case KeyType.Hold:
                ExecuteHold(state, i);
                break;
            case KeyType.Release_On_Second_Click:
                ExecuteReleaseOnSecondClick(state, i);
                break;
        }
    }

    public void Add()
    {
        if (keyCode.Count >= maxAmount)
            return;
        keyCode.Add(VirtualKeyCode.NONAME);
        keyType.Add(KeyType.Click);
    }

    public void SetCode(VirtualKeyCode code, int id)
    {
        keyCode[id] = code;
    }

    public void SetType(KeyType type, int id)
    {
        keyType[id] = type;
    }

    public void Remove(int id)
    {
        keyCode.RemoveAt(id);
        keyType.RemoveAt(id);
    }

    private void ExecuteClick(JoyButtonState state, int i)
    {
        switch (state)
        {
            case JoyButtonState.Pressed:
                SimulatorMethods.KeyDown(keyCode[i]);
                CoroutineMaster.ExecuteAfterNFrames(
                    1, delegate { SimulatorMethods.KeyUp(keyCode[i]); }
                    );
                break;
        }
    }

    private void ExecuteHold(JoyButtonState state, int i)
    {
        switch (state)
        {
            case JoyButtonState.Pressed:
                SimulatorMethods.KeyDown(keyCode[i]);
                break;
            case JoyButtonState.Hold:
                SimulatorMethods.KeyDown(keyCode[i]);
                break;
            case JoyButtonState.Realesed:
                SimulatorMethods.KeyUp(keyCode[i]);
                break;
            case JoyButtonState.None:
                break;
        }
    }

    private void ExecuteReleaseOnSecondClick(JoyButtonState state, int i)
    {
        switch (state)
        {
            case JoyButtonState.Pressed:
                isPressed = !isPressed;
                if (isPressed)
                {
                    SimulatorMethods.KeyDown(keyCode[i]);
                }
                else
                {
                    SimulatorMethods.KeyUp(keyCode[i]);
                }
                break;
            default:
                if (isPressed)
                    SimulatorMethods.KeyDown(keyCode[i]);
                break;
        }
    }

    public static JoyButtonState GetJoyState(bool currentState, bool lastState)
    {
        if (!lastState && !currentState)
            return JoyButtonState.None;
        if (!lastState && currentState)
            return JoyButtonState.Pressed;
        if (lastState && currentState)
            return JoyButtonState.Hold;
        if (lastState && !currentState)
            return JoyButtonState.Realesed;
        return JoyButtonState.None;
    }
}
