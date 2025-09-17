using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class InspectorNode : MonoBehaviour
    {
        public GameObject obj_empty;
        public List<EditorInspectorBase> inspectors; 
        
        private void Awake()
        {
            LevelEditorCallback.OnSelectChanged += OnSelectChanged;
            LevelEditorCallback.OnChangeState += OnChangeState;
        }

        private void OnChangeState(EOPState state)
        {
            obj_empty.SetActive(state == EOPState.Main || state == EOPState.None);
            foreach (EditorInspectorBase inspector in inspectors)
            {
                if (inspector.OPState == state)
                {
                    inspector.Show(null);
                }
                else
                {
                    inspector.Hide();
                }
            }
        }

        private void OnSelectChanged(ISelectable selectable)
        {
            if (LevelEditorMgr.Instance.State == EOPState.Main)
            {
                return;
            }
            foreach (EditorInspectorBase inspector in inspectors)
            {
                if (inspector.SelectType.IsInstanceOfType(selectable))
                {
                    inspector.Show(selectable);
                }
                else
                {
                    inspector.Hide();
                }
            }
            
        }
        

        private void OnDestroy()
        {
            LevelEditorCallback.OnChangeState -= OnChangeState;
            LevelEditorCallback.OnSelectChanged -= OnSelectChanged;
        }
    }
}