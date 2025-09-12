using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorMgr : MonoBehaviour
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

        public string LevelId;
    }
}