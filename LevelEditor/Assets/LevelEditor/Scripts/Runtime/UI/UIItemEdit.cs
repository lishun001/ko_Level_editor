using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class UIItemEdit : MonoBehaviour
    {
        public GameObject obj_mask;
        // ========== Head ==========
        public Text txt_level_id;
        public Button btn_back;
        public Button btn_save;
        
        // ========== 网格生成 ==========
        public InputField input_row;
        public InputField input_col;
        public Button btn_generate_grid;
        public Button btn_grid_edit_mode;
        
        // ========== 线圈生成 ==========
        public Button btn_add_color;
        public Button btn_generate_coil;
        public Button btn_coil_edit_mode;
        public UIItemColorPreNode uiItemColorPreNode;
        public GameObject obj_color_node;
        
        
        
        // ========== 预览区域 ==========
        public GridLayoutGroup layout_grid;


        private void Awake()
        {
            AddEvent();
            
            RefreshView();
        }
        
        private void RefreshView()
        {
            txt_level_id.text = $"关卡ID: {LevelEditorMgr.Instance.LevelId}";
            RefreshColorNode();
        }

        public void RefreshGrid()
        {
            var cellDatas = LevelEditorMgr.Instance.LevelDB.CellDatas;
            int row = cellDatas.Count;
            int col = cellDatas[0].Count;
            
            float width = col * EditorDefine.WIDTH;
            float height = row * EditorDefine.HEIGHT;
            
            layout_grid.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            int count = row * col;
            int childCount = layout_grid.transform.childCount;
            GameObject prefab = layout_grid.transform.GetChild(0).gameObject;
            
            for (int i = 0; i < childCount; i++)
            {
                layout_grid.transform.GetChild(i).gameObject.SetActive(false);
            }
            
            for (int i = 0; i < count; i++)
            {
                GameObject obj;
                if (i < childCount)
                {
                    obj = layout_grid.transform.GetChild(i).gameObject;
                }
                else
                {
                    obj = Instantiate(prefab, layout_grid.transform);
                }
                obj.SetActive(true);
                obj.GetComponent<UIItemCell>().SetData(i/col, i%col, cellDatas[i/col][i%col]);
            }
        }

        private void RefreshColorNode()
        {
            int childCount = obj_color_node.transform.childCount;
            GameObject prefab = obj_color_node.transform.GetChild(0).gameObject;
            int count = LevelEditorMgr.Instance.SelectColors.Count;

            for (int i = 0; i < childCount; i++)
            {
                obj_color_node.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < count; i++)
            {
                var kv = LevelEditorMgr.Instance.SelectColors[i];
                GameObject obj;
                if (i < childCount)
                {
                    obj = obj_color_node.transform.GetChild(i).gameObject;
                }
                else
                {
                    obj = Instantiate(prefab, obj_color_node.transform);
                }
                obj.SetActive(true);
                UIItemColorView uiItemColorView = obj.GetComponent<UIItemColorView>();
                uiItemColorView.SetData(kv.Key, kv.Value);
            }
        }
        
        private void AddEvent()
        {
            btn_back.onClick.AddListener(OnClickBack);
            btn_save.onClick.AddListener(OnClickSave);   
            
            btn_generate_grid.onClick.AddListener(OnClickGenerateGrid);
            btn_grid_edit_mode.onClick.AddListener(OnClickGridEditMode);
            
            btn_add_color.onClick.AddListener(OnClickAddColor);
            btn_generate_coil.onClick.AddListener(OnClickGenerateCoil);
            btn_coil_edit_mode.onClick.AddListener(OnClickCoilEditMode);

            LevelEditorCallback.OnSelectColorChanged += OnSelectColorChanged;
            LevelEditorCallback.OnRefreshGridView += RefreshGrid;
            LevelEditorCallback.OnChangeState += OnChangeState;
        }

        private void OnChangeState(EOPState state)
        {
            obj_mask.SetActive(state != EOPState.Main);
        }

        private void OnSelectColorChanged()
        {
            RefreshColorNode();
        }

        private void OnClickAddColor()
        {
            uiItemColorPreNode.Show();
        }

        private void OnClickCoilEditMode()
        {
            
        }

        private void OnClickGenerateCoil()
        {
            
        }

        private void OnClickGridEditMode()
        {
            LevelEditorMgr.Instance.ChangeState(EOPState.GridEdit);
        }

        private void OnClickGenerateGrid()
        {
            if (!CheckRowColValid())
            {
                UITips.ShowTips("row最大值10，col最大值8", UITips.CloseTips, null);
                return;
            }
            LevelEditorMgr.Instance.GenerateGrid(int.Parse(input_row.text), int.Parse(input_col.text));
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

        private bool CheckRowColValid()
        {
            int row = int.Parse(input_row.text);
            int col = int.Parse(input_col.text);

            return row > 0 && row <= 10 && col > 0 && col <= 8;
        }

        private void OnDestroy()
        {
            LevelEditorCallback.OnChangeState -= OnChangeState;
            LevelEditorCallback.OnRefreshGridView -= RefreshGrid;
            LevelEditorCallback.OnSelectColorChanged -= OnSelectColorChanged;
        }
    }
}