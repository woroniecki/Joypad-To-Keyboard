using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIKeyboardButtonPresser : MonoBehaviour
    {
        public UnityEngine.UI.Button addBtn;
        public Transform List;

        KeyboardButtonPresser current;

        public void Load(KeyboardButtonPresser data)
        {
            current = data;
            addBtn.onClick.RemoveAllListeners();
            addBtn.onClick.AddListener(delegate {
                current.Add();
                UpdateView();
            });
            UpdateView();
        }

        public void UpdateView()
        {
            int careEndlessLoop = 10;
            while (List.childCount > 0 && careEndlessLoop > 0)
            {
                DestroyImmediate (List.GetChild(0).gameObject);
                careEndlessLoop--;
            }

            for (int i = 0; i < current.keyCode.Count; i++)
            {
                GameObject rowObj = Instantiate(UIController.instance.keyboardRowPrefab, List);
                rowObj.transform.localPosition = new Vector3(0, i * -30);

                UIKeyboardButtonRow row = rowObj.GetComponent<UIKeyboardButtonRow>();
                int id = i;
                row.remove.onClick.RemoveAllListeners();
                row.remove.onClick.AddListener(delegate {
                    current.Remove(id);
                    UpdateView();
                });

                row.code.onValueChanged.RemoveAllListeners();
                row.code.onValueChanged.AddListener(delegate {
                    current.SetCode (row.code.GetCode(), id);
                });
                row.code.SetCode(current.keyCode[id]);

                row.type.onValueChanged.RemoveAllListeners();
                row.type.onValueChanged.AddListener(delegate {
                    current.SetType ((KeyboardButtonPresser.KeyType)row.type.value, id);
                });
                row.type.value = (int)current.keyType[id];
            }
        }
    }
}
