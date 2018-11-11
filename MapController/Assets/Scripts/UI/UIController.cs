using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {

        static public UIController instance;

        public GameObject keyboardRowPrefab;
        public SimulatorManager simManager;

        [Space(5)]
        public Transform buttonsParent;
        public UiButtonView buttonView;
        public UIDualAxisView dualAxisView;

        [Space(5)]
        public Text nameText;
        public Image padImage;
        public UnityEngine.UI.Button[] buttons;

        UnityEngine.UI.Image[] images;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            images = new Image[buttons.Length];
            for (int x = 0; x < buttons.Length; x++)
                images[x] = buttons[x].GetComponent<UnityEngine.UI.Image> ();

            int i = 0;
            for (i = 0; i < simManager.buttons.Count; i++)
            {
                int id = i;
                buttons[i].onClick.AddListener(delegate {
                    ShowButtonData(id);
                    EnableButtonView(id);
                });
            }

            for (int j = 0; j < simManager.axes.Count; j++)
            {
                int id = j;
                int imageButtonID = simManager.buttons.Count + j;
                buttons[simManager.buttons.Count + j].onClick.AddListener(delegate {
                    ShowDualAxisData(id);
                    EnableButtonView(imageButtonID);
                });
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
            ResetButtonColors();
            buttonView.gameObject.SetActive(false);
            dualAxisView.gameObject.SetActive(false);
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void EnableButtonView(int index)
        {
            ResetButtonColors();
            images[index].color = Color.gray;
        }

        public void ResetButtonColors()
        {
            for (int i = 0; i < images.Length; i++)
                images[i].color = Color.white;
        }
    }
}
