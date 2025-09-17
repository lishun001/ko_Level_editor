using System;

namespace LevelEditor
{
    public static class LevelEditorCallback
    {
        // 选择颜色列表变化
        public static Action OnSelectColorChanged;
        public static Action<EOPState> OnChangeState;

        // 刷新格子视图
        public static Action OnRefreshGridView;
        
        public static Action<ISelectable> OnSelectChanged;
    }
}