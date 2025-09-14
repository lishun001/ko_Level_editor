using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UILevelEditor : MonoBehaviour
    {
        public GameObject obj_new_level;
        public GameObject obj_edit_level;
        public InputField input_level_id;
        public Button btn_create_level;
        public Button btn_mod_level;
        
        private static UILevelEditor _ui;
        
        public static void ShowEditView()
        {
            _ui.obj_new_level.SetActive(false);
            _ui.obj_edit_level.SetActive(true);
        }
        
        public static void ShowNewView()
        {
            _ui.obj_new_level.SetActive(true);
            _ui.obj_edit_level.SetActive(false);
        }

        private void Awake()
        {
            _ui = this;
            obj_new_level.SetActive(true);
            obj_edit_level.SetActive(false);
            
            AddEvent();
        }

        private void AddEvent()
        {
            btn_create_level.onClick.AddListener(OnClickCreateLevel);
            btn_mod_level.onClick.AddListener(OnClickModifyLevel);
        }

        private void OnClickModifyLevel()
        {
            if(!CheckValid())
            {
                UIToast.ShowToast("LevelID 不合法");
                return;
            }
            
            obj_new_level.SetActive(false);
            obj_edit_level.SetActive(true);
        }

        private void OnClickCreateLevel()
        {
            if(!CheckValid())
            {
                UIToast.ShowToast("LevelID 不合法");
                return;
            }
            
            LevelEditorMgr.Instance.LevelId = input_level_id.text;
            obj_new_level.SetActive(false);
            obj_edit_level.SetActive(true);
        }
        
        private bool CheckValid()
        {
            bool isValid = !string.IsNullOrEmpty(input_level_id.text);
            return isValid;
        }
    }
}