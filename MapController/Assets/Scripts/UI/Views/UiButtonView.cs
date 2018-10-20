using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UiButtonView : MonoBehaviour
    {

        public UIKeyboardButtonPresser UIkeyboardPresser;

        public void Load(Button data)
        {
            UIkeyboardPresser.Load(data.keyButtons);
        }

    }
}
