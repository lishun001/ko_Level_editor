namespace LevelEditor
{
    public partial class LevelEditorMgr
    {
        public EOPState State { set; get; }
        
        public void ChangeState(EOPState state)
        {
            if (State == state)
            {
                return;
            }

            State = state;
            switch (State)
            {
                case EOPState.Main:
                    LevelEditorMgr.Instance.SelectItem = null;
                    break;
                case EOPState.GridEdit:
                    break;
            }
            
            LevelEditorCallback.OnChangeState?.Invoke(State);
        }
    }
    
    public enum EOPState
    {
        None,
        Main,
        GridEdit,
    }
}