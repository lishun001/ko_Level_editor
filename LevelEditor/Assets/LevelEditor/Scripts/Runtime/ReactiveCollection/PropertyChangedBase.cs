using System;
using System.Collections.Generic;

public class PropertyChangedBase
{
    public event Action OnChanged;
    
    protected bool SetField<T>(ref T field, T value)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }
        field = value;
        OnChanged?.Invoke();
        return true;
    }
}