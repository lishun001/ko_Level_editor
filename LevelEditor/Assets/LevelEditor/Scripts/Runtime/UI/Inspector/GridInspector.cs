using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class GridInspector : EditorInspectorBase
    {
        public Button btn_add_wall;
        public Button btn_add_pipeline;
        public Button btn_rotate_pipeline;
        public Button btn_close;

        public override Type SelectType => typeof(UIItemCell);
        public override EOPState OPState => EOPState.GridEdit;

        private void Awake()
        {
            btn_add_wall.onClick.AddListener(OnClickAddWall);
            btn_add_pipeline.onClick.AddListener(OnClickAddPipeline);
            btn_rotate_pipeline.onClick.AddListener(OnClickRotatePipeline);
            btn_close.onClick.AddListener(OnClickClose);
        }

        private void OnClickClose()
        {
            LevelEditorMgr.Instance.ChangeState(EOPState.Main);
        }

        private void OnClickAddPipeline()
        {
            if (LevelEditorMgr.Instance.SelectItem == null)
            {
                return;
            }

            if (LevelEditorMgr.Instance.SelectItem is UIItemCell uiItemCell)
            {
                LevelEditorMgr.Instance.ModLevelDB(uiItemCell.Row, uiItemCell.Col, CellType.PipelineMec);
            }
        }
        
        private void OnClickRotatePipeline()
        {
            
        }

        private void OnClickAddWall()
        {
            if (LevelEditorMgr.Instance.SelectItem == null)
            {
                return;
            }

            if (LevelEditorMgr.Instance.SelectItem is UIItemCell uiItemCell)
            {
                LevelEditorMgr.Instance.ModLevelDB(uiItemCell.Row, uiItemCell.Col, CellType.Wall);
            }
        }

        public override void Show(object selectItem)
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}