using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[System.Serializable]
public class Button : JoystickPart
{
    public enum Type
    {
        A, B, X, Y, Right_Bumper, Left_Bumper, Right_Stick_Press, Left_Stick_Press, Back, Start, Left_Trigger, Right_Trigger
    }

    public Type type;
    public KeyboardButtonPresser keyButtons;

    ButtonState currentState { get; set; }
    ButtonState lastState { get { return keyButtons.lastState; } }

    public Button()
    {
        keyButtons = new KeyboardButtonPresser();
    }

    public override void Execute()
    {
        base.Execute();
        keyButtons.Execute(GetState(), currentState);
    }

    private JoyButtonState GetState()
    {
        currentState = GetValue();
        return KeyboardButtonPresser.GetJoyState(currentState == ButtonState.Pressed, lastState == ButtonState.Pressed);
    }

    private ButtonState GetValue()
    {
        switch (type)
        {
            case Type.A:
                return JoystickController.padState.Buttons.A;
            case Type.B:
                return JoystickController.padState.Buttons.B;
            case Type.X:
                return JoystickController.padState.Buttons.X;
            case Type.Y:
                return JoystickController.padState.Buttons.Y;
            case Type.Right_Bumper:
                return JoystickController.padState.Buttons.RightShoulder;
            case Type.Left_Bumper:
                return JoystickController.padState.Buttons.LeftShoulder;
            case Type.Right_Stick_Press:
                return JoystickController.padState.Buttons.RightStick;
            case Type.Left_Stick_Press:
                return JoystickController.padState.Buttons.LeftStick;
            case Type.Back:
                return JoystickController.padState.Buttons.Back;
            case Type.Start:
                return JoystickController.padState.Buttons.Start;
            case Type.Left_Trigger:
                return JoystickController.GetButtonStateByValue(JoystickController.padState.Triggers.Left);
            case Type.Right_Trigger:
                return JoystickController.GetButtonStateByValue(JoystickController.padState.Triggers.Right);
        }
        return ButtonState.Released;
    }

    public override string ToString()
    {
        return type.ToString();
    }
}
