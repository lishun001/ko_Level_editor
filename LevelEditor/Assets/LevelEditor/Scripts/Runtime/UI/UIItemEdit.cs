using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UIItemEdit : MonoBehaviour
    {
        // ========== Head ==========
        public Text txt_level_id;
        public Button btn_back;
        public Button btn_save;
        
        // ========== 网格生成 ==========
        public InputField input_row;
        public InputField input_col;
        public Button btn_generate_grid;
        public Button btn_grid_edit_mode;


        private void Awake()
        {
            AddEvent();
            
            RefreshView();
        }
        
        private void RefreshView()
        {
            txt_level_id.text = $"关卡ID: {LevelEditorMgr.Instance.LevelId}";
        }
        
        private void AddEvent()
        {
            btn_back.onClick.AddListener(OnClickBack);
            btn_save.onClick.AddListener(OnClickSave);   
            
            btn_generate_grid.onClick.AddListener(OnClickGenerateGrid);
            btn_grid_edit_mode.onClick.AddListener(OnClickGridEditMode);
        }

        private void OnClickGridEditMode()
        {
            
        }

        private void OnClickGenerateGrid()
        {
            
        }
        
        private void OnClickSave()
        {
            
        }

        private void OnClickBack()
        {
            UITips.ShowTips("关卡数据保存了吗，确认返回？", () =>
            {
                UILevelEditor.ShowNewView();
                UITips.CloseTips();
            }, UITips.CloseTips);
        }
    }
}