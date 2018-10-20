using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIDualAxisView : MonoBehaviour
    {

        public Dropdown dropDownType;

        public UIButtonsAxisView buttonsPanel;
        public UIMouse_MoveAxisView mouse_MovePanel;
        public UIMouse_Circle_MoveAxisView mouse_Circle_MovePanel;

        DualAxis current;

        public void Load(DualAxis data)
        {
            current = data;

            SubscribeExecutionTypeSet();
            SetExecutionTypeView();
        }

        private void SubscribeExecutionTypeSet()
        {
            dropDownType.onValueChanged.RemoveAllListeners();
            dropDownType.onValueChanged.AddListener(delegate
            {
                current.executionType = (DualAxis.ExecutionType)dropDownType.value;
                SetExecutionTypeView();
            });
            dropDownType.value = (int)current.executionType;
        }

        private void SetExecutionTypeView()
        {
            buttonsPanel.gameObject.SetActive(false);
            mouse_MovePanel.gameObject.SetActive(false);
            mouse_Circle_MovePanel.gameObject.SetActive(false);
            switch (current.executionType)
            {
                case DualAxis.ExecutionType.Buttons:
                    buttonsPanel.gameObject.SetActive(true);
                    buttonsPanel.Load(current.buttonsData);
                    break;
                case DualAxis.ExecutionType.Mouse_Move:
                    mouse_MovePanel.gameObject.SetActive(true);
                    mouse_MovePanel.Load(current.mouseMoveData);
                    break;
                case DualAxis.ExecutionType.Mouse_Circle_Move:
                    mouse_Circle_MovePanel.gameObject.SetActive(true);
                    mouse_Circle_MovePanel.Load(current.mouseCircleMoveData);
                    break;
            }
        }
    }
}
