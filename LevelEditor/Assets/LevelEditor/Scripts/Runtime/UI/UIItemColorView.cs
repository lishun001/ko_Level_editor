using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UIItemColorView : MonoBehaviour
    {
        public Image img_color;
        public InputField input_count;
        public Button btn_del;

        private ColorElement _colorElement;

        private void Awake()
        {
            btn_del.onClick.AddListener(OnClickDel);
        }

        private void OnClickDel()
        {
            LevelEditorMgr.Instance.RemoveSelectColor(_colorElement);
        }

        public void SetData(ColorElement colorElement, int count)
        {
            _colorElement = colorElement;
            img_color.color = colorElement.Color;
            input_count.text = count.ToString();
            
            input_count.onValueChanged.RemoveAllListeners();
            input_count.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            if (int.TryParse(value, out int count))
            {
                LevelEditorMgr.Instance.ModSelectColor(_colorElement, count);
            }
        }

        private void OnDestroy()
        {
            input_count.onValueChanged.RemoveAllListeners();
        }
    }
}