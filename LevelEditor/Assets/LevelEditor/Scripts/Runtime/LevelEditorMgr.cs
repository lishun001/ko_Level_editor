using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Rendering;

namespace LevelEditor
{
    public partial class LevelEditorMgr : MonoBehaviour
    {
        private static LevelEditorMgr _instance;
        public static LevelEditorMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("LevelEditorMgr");
                    _instance = obj.AddComponent<LevelEditorMgr>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }

        public string LevelId { set; get; }

        public LevelDB LevelDB { set; get; }

        

        public List<KeyValuePair<ColorElement, int>> SelectColors { set; get; } =
            new List<KeyValuePair<ColorElement, int>>();

        private UIItemEdit _uiItemEdit;
        
        public void Init(UIItemEdit uiItemEdit)
        {
            _uiItemEdit = uiItemEdit;
            ChangeState(EOPState.Main);
        }
        public void CreateLevelDB()
        {
            LevelDB = new LevelDB();
        }

        public void GenerateGrid(int row, int col)
        {
            LevelDB.GenerateGrid(row, col);
            LevelEditorCallback.OnRefreshGridView?.Invoke();
        }
        
        /// <summary>
        /// 是否已经存在该颜色
        /// </summary>
        /// <param name="colorElement"></param>
        /// <returns></returns>
        public bool HasColor(ColorElement colorElement)
        {
            foreach (var item in SelectColors)
            {
                if (item.Key == colorElement)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSelectColor(ColorElement colorElement)
        {
            if (HasColor(colorElement))
            {
                Debug.LogError($"Color already selected: {colorElement.ColorType}");
                return;
            }
            
            SelectColors.Add(new KeyValuePair<ColorElement, int>(colorElement, 0));
            SelectColors.Sort((a, b) => LevelEditorSettings.Settings.Colors.IndexOf(a.Key)
                .CompareTo(LevelEditorSettings.Settings.Colors.IndexOf(b.Key)));
            LevelEditorCallback.OnSelectColorChanged?.Invoke();
        }
        
        public void RemoveSelectColor(ColorElement colorElement)
        {
            for (int i = SelectColors.Count - 1; i >= 0; i--)
            {
                if (SelectColors[i].Key == colorElement)
                {
                    SelectColors.RemoveAt(i);
                    SelectColors.Sort((a, b) => LevelEditorSettings.Settings.Colors.IndexOf(a.Key)
                        .CompareTo(LevelEditorSettings.Settings.Colors.IndexOf(b.Key)));
                    LevelEditorCallback.OnSelectColorChanged?.Invoke();
                    return;
                }
            }
            
            Debug.LogError($"Color not found in selection: {colorElement.ColorType}");
        }

        public void ModSelectColor(ColorElement colorElement, int count)
        {
            if (!HasColor(colorElement))
            {
                Debug.LogError($"Color not found in selection: {colorElement.ColorType}");
                return;
            }
            
            for (int i = 0; i < SelectColors.Count; i++)
            {
                if (SelectColors[i].Key == colorElement)
                {
                    SelectColors[i] = new KeyValuePair<ColorElement, int>(colorElement, count);
                    LevelEditorCallback.OnSelectColorChanged?.Invoke();
                    return;
                }
            }
        }
        
        public void ModLevelDB(int row, int col, CellType type, EColor color = EColor.Red)
        {
            if (LevelDB == null || LevelDB.CellDatas == null)
            {
                Debug.LogError("LevelDB or CellDatas is null");
                return;
            }
            
            if (row < 0 || row >= LevelDB.CellDatas.Count || col < 0 || col >= LevelDB.CellDatas[0].Count)
            {
                Debug.LogError($"Invalid row or col: {row}, {col}");
                return;
            }

            LevelDB.CellDatas[row][col].type = type;
            LevelDB.CellDatas[row][col].color = color;
            LevelEditorCallback.OnRefreshGridView?.Invoke();
        }
    }
}