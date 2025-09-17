using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LevelEditor
{
    public class UIItemCell : MonoBehaviour, ISelectable
    {
        public Button btn_select;
        public Transform trans_root;
        public Transform wall_node;
        public Transform pipeline_node;
        
        public int Row { set; get; }
        public int Col { set; get; }

        private void Awake()
        {
            btn_select.onClick.AddListener(OnClickSelect);
        }

        public void SetData(int row, int col, CellData cellData)
        {
            Row = row;
            Col = col;
            gameObject.name = $"Cell_{row}_{col}";

            wall_node.gameObject.SetActive(false);
            pipeline_node.gameObject.SetActive(false);
            if (cellData.type == CellType.Wall)
            {
                GameObject obj = null;
                if (wall_node.transform.Find("wall") == null)
                {
                    GameObject prefab =
                        AssetDatabase.LoadAssetAtPath<GameObject>("Assets/LevelEditor/Editor/Res/prefab_wall.prefab");
                    obj = Object.Instantiate(prefab, wall_node);
                }
                else
                {
                    obj = wall_node.transform.Find("wall").gameObject;
                }
                
                obj.name = "wall";
                wall_node.gameObject.SetActive(true);
            }
            else if (cellData.type == CellType.PipelineMec)
            {
                GameObject obj = null;
                if (pipeline_node.transform.Find("pipeline") == null)
                {
                    GameObject prefab =
                        AssetDatabase.LoadAssetAtPath<GameObject>("Assets/LevelEditor/Editor/Res/prefab_pipeline.prefab");
                    obj = Object.Instantiate(prefab, pipeline_node);
                }
                else
                {
                    obj = pipeline_node.transform.Find("pipeline").gameObject;
                }
                
                obj.name = "pipeline";
                pipeline_node.gameObject.SetActive(true);
            }
        }

        private void OnClickSelect()
        {
            ((ISelectable) this).Select();
        }
    }
}