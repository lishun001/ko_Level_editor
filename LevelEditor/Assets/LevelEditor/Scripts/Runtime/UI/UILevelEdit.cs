using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UILevelEdit : MonoBehaviour
    {
        // ========== Head ==========
        public Text txt_level_id;
        public Button btn_back;
        public Button btn_save;


        private void Awake()
        {
            AddEvent();
        }
        
        private void AddEvent()
        {
            btn_back.onClick.AddListener(OnClickBack);
            btn_save.onClick.AddListener(OnClickSave);   
        }

        private void OnClickSave()
        {
            
        }

        private void OnClickBack()
        {
            Debug.Log(">>>>>");
            UITips.ShowTips("关卡数据保存了吗，确认返回？", UILevelEditor.ShowNewView, UITips.CloseTips);
        }
    }
}