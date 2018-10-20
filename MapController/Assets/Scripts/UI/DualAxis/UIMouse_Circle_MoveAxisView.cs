using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMouse_Circle_MoveAxisView : UIMouse_MoveAxisView
    {
        public InputField inputFieldX;
        public InputField inputFieldY;

        [System.NonSerialized]
        DualAxis.Mouse_Circle_MoveData current;

        public void Load(DualAxis.Mouse_Circle_MoveData data)
        {
            base.Load(data);
            current = data;
            SetInputField(inputFieldX, current.centerX,
                delegate { current.centerX = int.Parse(inputFieldX.text); });
            SetInputField(inputFieldY, current.centerY,
                delegate { current.centerY = int.Parse(inputFieldY.text); });
        }

        public void SetMidPosition()
        {
            inputFieldX.text = (UnityEngine.Screen.currentResolution.width / 2f).ToString();
            inputFieldY.text = (UnityEngine.Screen.currentResolution.height / 2f).ToString();
        }

        public void SetCursorToPosition()
        {
            SimulatorMethods.instance.MouseSetPos(current.centerX, current.centerY);
        }
    }
}
