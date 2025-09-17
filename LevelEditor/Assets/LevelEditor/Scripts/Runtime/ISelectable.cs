using UnityEngine;

namespace LevelEditor
{
    public interface ISelectable
    {
        public virtual void Select()
        {
            OnSelect();
        }

        public virtual void OnSelect()
        {
            var old = LevelEditorMgr.Instance.SelectItem;
            if(old != null && old != this)
            {
                ((MonoBehaviour) old).transform.Find("obj_select").gameObject.SetActive(false);
            }
            LevelEditorMgr.Instance.SelectItem = this;
            ((MonoBehaviour) this).transform.Find("obj_select").gameObject.SetActive(true);
        }
    }
}