using System;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

namespace UI
{
    public class UIDropDownCustom : UIDropDown
    {
        public string enumTypeName;

        public override string[] GetOptions()
        {
            Type t = Utility.StringToType(enumTypeName);
            return Enum.GetNames(t);
        }

        public override void SetValue(int value)
        {
            dropDown.value = value;
        }

    }
}