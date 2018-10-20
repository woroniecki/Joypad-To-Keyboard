using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMouse_MoveAxisView : MonoBehaviour
    {
        public UIKeyboardButtonPresser UIkeyboardPressers;
        public InputField inputField;

        [System.NonSerialized]
        DualAxis.Mouse_MoveData current;

        public void Load(DualAxis.Mouse_MoveData data)
        {
            current = data;

            UIkeyboardPressers.Load(current.keyButtons);
            SetInputField(inputField, data.value,
                delegate { data.value = float.Parse(inputField.text); });
        }

        protected void SetInputField(InputField field, float currentValue, DelegateMethod listenerMethod)
        {
            field.characterValidation = InputField.CharacterValidation.Decimal;
            field.text = currentValue.ToString();
            field.onValueChanged.RemoveAllListeners();
            field.onValueChanged.AddListener(delegate
            {
                listenerMethod();
            });
        }
    }
}
