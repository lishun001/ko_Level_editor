using System;
using UnityEngine;

namespace LevelEditor
{
    public abstract class EditorInspectorBase : MonoBehaviour
    {
        public abstract Type SelectType { get; }
        public abstract EOPState OPState { get; }
        public abstract void Show(object selectItem);
        public abstract void Hide();
    }
}