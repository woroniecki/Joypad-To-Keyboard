using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {

        static public UIController instance;

        public GameObject buttonPrefab;
        public GameObject keyboardRowPrefab;
        public SimulatorManager simManager;

        [Space(5)]
        public Transform buttonsParent;
        public UiButtonView buttonView;
        public UIDualAxisView dualAxisView;

        [Space(5)]
        public Text nameText;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            int i = 0;
            for (i = 0; i < simManager.buttons.Count; i++)
            {
                GameObject obj = Instantiate(buttonPrefab, buttonsParent);
                obj.GetComponentInChildren<Text>().text = simManager.buttons[i].ToString();
                obj.name = simManager.buttons[i].ToString();
                RectTransform rect = obj.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(rect.localPosition.x, -i * 30, 0);
                int id = i;
                rect.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { ShowButtonData(id); });
            }

            for (int j = 0; j < simManager.axes.Count; j++)
            {
                GameObject obj = Instantiate(buttonPrefab, buttonsParent);
                obj.GetComponentInChildren<Text>().text = simManager.axes[j].ToString();
                obj.name = simManager.axes[j].ToString();
                RectTransform rect = obj.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(rect.localPosition.x, -(i + j + 1) * 30, 0);
                int id = j;
                rect.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { ShowDualAxisData(id); });
            }
        }

        void ShowButtonData(int id)
        {
            dualAxisView.gameObject.SetActive(false);
            buttonView.gameObject.SetActive(true);
            buttonView.Load(simManager.buttons[id]);
        }

        void ShowDualAxisData(int id)
        {
            buttonView.gameObject.SetActive(false);
            dualAxisView.gameObject.SetActive(true);
            dualAxisView.Load(simManager.axes[id]);
        }

        public void DisableAllViews()
        {
            buttonView.gameObject.SetActive(false);
            dualAxisView.gameObject.SetActive(false);
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }
    }
}
