using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UITips : MonoBehaviour
    {
        public Text txt_msg;
        public Button btn_yes;
        public Button btn_no;
        
        private Action _onYes;
        private Action _onNo;
        
        
        private static UITips _uiTips;
        
        public static void ShowTips(string content, Action onYes = null, Action onNo = null)
        {
            _uiTips.Show(content, onYes, onNo);   
        }
        
        public static void CloseTips()
        {
            _uiTips.transform.Find("panel").gameObject.SetActive(false);
        }
        
        private void Awake()
        {
            _uiTips = this;
            btn_yes.onClick.AddListener(OnClickYes);
            btn_no.onClick.AddListener(OnClickNo);
        }

        private void Show(string content, Action onYes, Action onNo)
        {
            btn_yes.gameObject.SetActive(onYes != null);
            btn_no.gameObject.SetActive(onNo != null);
            transform.Find("panel").gameObject.SetActive(true);
            txt_msg.text = content;
            _onYes = onYes;
            _onNo = onNo;
        }
        
        private void OnClickNo()
        {
            _onNo?.Invoke();
        }

        private void OnClickYes()
        {
            _onYes?.Invoke();
        }
    }
}