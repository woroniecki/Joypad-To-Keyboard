using UnityEngine;
using XInputDotNetPure;

[System.Serializable]
public class DualAxis : JoystickPart
{
    const float deadZone = 0.2f;

    public enum Type
    {
        Left_Stick, Right_Stick, Cross
    }

    public enum ExecutionType
    {
        Buttons, Mouse_Move, Mouse_Circle_Move
    }

    public Type type;
    public ExecutionType executionType;
    public ButtonsData buttonsData;
    public Mouse_MoveData mouseMoveData;
    public Mouse_Circle_MoveData mouseCircleMoveData;

    [System.NonSerialized]
    private bool lastCanExecute = false;

    [System.Serializable]
    public class ButtonsData
    {
        public KeyboardButtonPresser[] keyButtons;
        public ButtonsData()
        {
            keyButtons = new KeyboardButtonPresser[4];
            for (int i = 0; i < keyButtons.Length; i++)
                keyButtons[i] = new KeyboardButtonPresser();
        }
    }

    [System.Serializable]
    public class Mouse_MoveData
    {
        public float value;
        public KeyboardButtonPresser keyButtons;
        public Mouse_MoveData()
        {
            keyButtons = new KeyboardButtonPresser();
        }
    }

    [System.Serializable]
    public class Mouse_Circle_MoveData : Mouse_MoveData
    {
        public int centerX;
        public int centerY;
    }

    public DualAxis()
    {
        buttonsData = new ButtonsData();
        mouseMoveData = new Mouse_MoveData();
        mouseCircleMoveData = new Mouse_Circle_MoveData();
    }

    public override void Execute()
    {
        base.Execute();
        Vector2 value = new Vector2(GetValueX(), GetValueY());
        bool curCanExecute = Mathf.Abs(value.x) > deadZone || Mathf.Abs(value.y) > deadZone;
        switch (executionType)
        {
            case ExecutionType.Buttons:
                ExecuteClick(value);
                break;
            case ExecutionType.Mouse_Move:
                ExecuteMouseMove(value, curCanExecute);
                break;
            case ExecutionType.Mouse_Circle_Move:
                ExecuteMouseCircleMove(value, curCanExecute);
                break;
        }
        lastCanExecute = curCanExecute;
    }

    private void ExecuteClick(Vector2 value)
    {
        ExecuteButton(0, JoystickController.GetButtonStateByValue(value.x, deadZone, false));//-X
        ExecuteButton(1, JoystickController.GetButtonStateByValue(value.y, deadZone, true));//+Y
        ExecuteButton(2, JoystickController.GetButtonStateByValue(value.x, deadZone, true));//+X
        ExecuteButton(3, JoystickController.GetButtonStateByValue(value.y, deadZone, false));//-Y
    }

    private void ExecuteButton(int index, ButtonState curState)
    {
        buttonsData.keyButtons[index].Execute(
                    KeyboardButtonPresser.GetJoyState(
                        curState == ButtonState.Pressed,
                        buttonsData.keyButtons[index].lastState == ButtonState.Pressed),
                    curState);
    }

    private void ExecuteMouseMove(Vector2 value, bool canExecute)
    {
        mouseMoveData.keyButtons.Execute(KeyboardButtonPresser.GetJoyState(canExecute, lastCanExecute));
        if (canExecute)
            SimulatorMethods.instance.MouseMoveBy(
                                (int)(mouseMoveData.value * value.x * Time.deltaTime),
                                (int)(mouseMoveData.value * value.y * -Time.deltaTime)
                                );
    }

    private void ExecuteMouseCircleMove(Vector2 value, bool canExecute)
    {
        mouseCircleMoveData.keyButtons.Execute(KeyboardButtonPresser.GetJoyState(canExecute, lastCanExecute));
        if (canExecute)
            SimulatorMethods.instance.MouseSetPos(
                                (int)(mouseCircleMoveData.centerX + value.x * mouseCircleMoveData.value),
                                (int)(mouseCircleMoveData.centerY + value.y * -mouseCircleMoveData.value)
                                );
    }

    private float GetValueX()
    {
        switch (type)
        {
            case Type.Cross:
                return -JoystickController.GetValueByButtonState(JoystickController.padState.DPad.Left)
                    + JoystickController.GetValueByButtonState(JoystickController.padState.DPad.Right);
            case Type.Left_Stick:
                return JoystickController.padState.ThumbSticks.Left.X;
            case Type.Right_Stick:
                return JoystickController.padState.ThumbSticks.Right.X;
        }
        return 0f;
    }

    private float GetValueY()
    {
        switch (type)
        {
            case Type.Cross:
                return -JoystickController.GetValueByButtonState(JoystickController.padState.DPad.Down)
                    + JoystickController.GetValueByButtonState(JoystickController.padState.DPad.Up);
            case Type.Left_Stick:
                return JoystickController.padState.ThumbSticks.Left.Y;
            case Type.Right_Stick:
                return JoystickController.padState.ThumbSticks.Right.Y;
        }
        return 0f;
    }

    public override string ToString()
    {
        return type.ToString();
    }
}
