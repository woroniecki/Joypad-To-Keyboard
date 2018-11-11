using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;

public class SimulatorManager : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public List<Button> buttons;
        public List<DualAxis> axes;
    }

    public Data data;
    public List<Button> buttons { get { return data.buttons; } }
    public List<DualAxis> axes { get { return data.axes; } }
    public JoystickController joyController;

    void Awake()
    {
        data = new Data();
        CreateJoystickParts<Button.Type, Button>(out data.buttons, true);
        CreateJoystickParts<DualAxis.Type, DualAxis>(out data.axes, false);
    }

    private void FixedUpdate()
    {
        joyController.UpdateState();
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Execute();
        for (int i = 0; i < axes.Count; i++)
            axes[i].Execute();
    }

    private static void CreateJoystickParts<T1, T2>(out List<T2> list, bool btns) where T2 : JoystickPart
    {
        T1[] values = (T1[])Enum.GetValues(typeof(T1));
        list = new List<T2>(values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            if (btns)
            {
                Button btn = new Button();
                btn.type = (Button.Type)(object)values[i];
                list.Add((T2)(object)btn);
            }
            else
            {
                DualAxis axis = new DualAxis();
                axis.type = (DualAxis.Type)(object)values[i];
                list.Add((T2)(object)axis);
            }
            
        }
    }

    public UnityEngine.UI.Image im;

    public void Deb()
    {
        im.alphaHitTestMinimumThreshold = 1f;
        Debug.Log(Time.time);
    }

}
