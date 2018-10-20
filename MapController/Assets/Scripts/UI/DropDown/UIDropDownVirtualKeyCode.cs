using System;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

namespace UI
{
    public class UIDropDownVirtualKeyCode : UIDropDown
    {

        public override string[] GetOptions()
        {
            return Utility.keysStringList;
        }

        public override void SetValue(int code)
        {
            dropDown.SetCode((VirtualKeyCode)code);
        }

    }
}
