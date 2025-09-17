using UnityEngine;

namespace LevelEditor
{
    public partial class LevelEditorMgr
    {
        private ISelectable _selectItem;

        public ISelectable SelectItem
        {
            set
            {
                if (_selectItem == value)
                {
                    return;
                }

                if (value == null && _selectItem != null)
                {
                    ((MonoBehaviour) _selectItem).transform.Find("obj_select").gameObject.SetActive(false);
                }
                _selectItem = value;
                LevelEditorCallback.OnSelectChanged?.Invoke(_selectItem);
            }

            get
            {
                return _selectItem;
            }
        }
    }
}