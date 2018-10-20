using System;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

public delegate void DelegateMethod();

public enum JoyButtonState
{
    None, Pressed, Hold, Realesed
}

public static class Utility {

    static string[] _keysStringList;
    public static string[] keysStringList
    {
        get
        {
            if (_keysStringList == null)
                _keysStringList = Enum.GetNames(typeof(VirtualKeyCode));
            return _keysStringList;
        }
    }

    static VirtualKeyCode[] _keysValueList;
    public static VirtualKeyCode[] keysValueList
    {
        get
        {
            if (_keysValueList == null)
                _keysValueList = (VirtualKeyCode[])Enum.GetValues(typeof(VirtualKeyCode));
            return _keysValueList;
        }
    }

    public static void SetCode(this Dropdown drop, VirtualKeyCode code)
    {
        for (int i = 0; i < keysValueList.Length; i++)
        {
            if (code == keysValueList[i])
            {
                drop.value = i;
                return;
            }
        }
    }

    public static VirtualKeyCode GetCode(this Dropdown drop)
    {
        return keysValueList[drop.value];
    }

    public static Type StringToType(string typeAsString)
    {
        Type typeAsType = Type.GetType(typeAsString);
        return typeAsType;
    }
}
