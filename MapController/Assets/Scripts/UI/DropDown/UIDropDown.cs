using System;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

namespace UI
{
    [RequireComponent(typeof(Dropdown))]
    public abstract class UIDropDown : MonoBehaviour, IUIDropDown
    {
        protected Dropdown dropDown;

        void Awake()
        {
            dropDown = GetComponent<Dropdown>();
            dropDown.ClearOptions();

            for (int i = 0; i < GetOptions().Length; i++)
            {
                Dropdown.OptionData rowData = new Dropdown.OptionData();
                rowData.text = GetOptions()[i];
                dropDown.options.Add(rowData);
            }
        }

        public virtual string[] GetOptions()
        {
            throw new NotImplementedException();
        }

        public virtual void SetValue(int value)
        {
            throw new NotImplementedException();
        }
    }
}