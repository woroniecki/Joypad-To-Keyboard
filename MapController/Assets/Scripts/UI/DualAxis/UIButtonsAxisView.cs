using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIButtonsAxisView : MonoBehaviour
    {

        [Header("Should have 4 elements")]
        public UIKeyboardButtonPresser[] UIkeyboardPressers;

        public void Load(DualAxis.ButtonsData data)
        {
            for (int i = 0; i < UIkeyboardPressers.Length; i++)
                UIkeyboardPressers[i].Load(data.keyButtons[i]);
        }
    }
}