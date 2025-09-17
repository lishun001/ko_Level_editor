using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(fileName = "LevelEditorSettings", menuName = "LevelEditor/LevelEditorSettings")]
    public class LevelEditorSettings : ScriptableObject
    {
        public List<ColorElement> Colors = new List<ColorElement>();


        private static LevelEditorSettings _settings;
        public static LevelEditorSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Resources.Load<LevelEditorSettings>("LevelEditorSettings");
                    if (_settings == null)
                    {
                        Debug.LogError("LevelEditorSettings asset not found in Resources folder.");
                    }
                }
                return _settings;
            }
        }
    }
}