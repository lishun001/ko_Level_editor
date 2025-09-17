using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UIItemColorPre : MonoBehaviour
    {
        public Text txt_color;
        public Toggle tog_select;
        public Image img_color;

        private ColorElement _colorElement;
        
        public void SetData(ColorElement colorElement)
        {
            _colorElement = colorElement;
            img_color.color = colorElement.Color;
            txt_color.text = colorElement.ColorType.ToString();
            tog_select.isOn = LevelEditorMgr.Instance.HasColor(colorElement);
            
            tog_select.onValueChanged.RemoveAllListeners();
            tog_select.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            if (isOn)
            {
                LevelEditorMgr.Instance.AddSelectColor(_colorElement);
            }
            else
            {
                LevelEditorMgr.Instance.RemoveSelectColor(_colorElement);
            }
        }

        private void OnDestroy()
        {
            tog_select.onValueChanged.RemoveAllListeners();
        }
    }
}