using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UIItemColorPreNode : MonoBehaviour
    {
        public Transform trans_color_node;
        public Button btn_close;

        private void Awake()
        {
            btn_close.onClick.AddListener(OnClickClose);
        }

        private void OnClickClose()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            int count = LevelEditorSettings.Settings.Colors.Count;
            int childCount = trans_color_node.childCount;
            GameObject prefab = trans_color_node.GetChild(0).gameObject;

            for (int i = 0; i < childCount; i++)
            {
                trans_color_node.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < count; i++)
            {
                GameObject obj = null;
                if (i < childCount)
                {
                    obj = trans_color_node.GetChild(i).gameObject;
                }
                else
                {
                    obj = Instantiate(prefab, prefab.transform.parent);
                }
                
                obj.SetActive(true);
                UIItemColorPre uiItemColorPre = obj.GetComponent<UIItemColorPre>();
                ColorElement colorElement = LevelEditorSettings.Settings.Colors[i];
                uiItemColorPre.SetData(colorElement);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}