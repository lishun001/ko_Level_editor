using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveListStruct<T> : IList<T> where T : struct
{
    List<T> m_List;

    public event Action OnChanged;

    public T this[int index]
    {
        get { return m_List[index]; }
        set
        {
            T oldVal = m_List[index];
            m_List[index] = value;
            if (!oldVal.Equals(value))
            {
                OnChanged?.Invoke();
            }
        }
    }

    public int Count
    {
        get { return m_List.Count; }
    }

    public bool IsReadOnly
    {
        get => false;
    }

    public ReactiveListStruct()
        : this(0)
    {
    }

    public ReactiveListStruct(int capacity)
    {
        m_List = new List<T>(capacity);
    }

    public bool Contains(T item)
    {
        return m_List.Contains(item);
    }

    public int IndexOf(T item)
    {
        return m_List.IndexOf(item);
    }

    public void Add(T item)
    {
        m_List.Add(item);
        OnChanged?.Invoke();
    }

    public void Insert(int index, T item)
    {
        m_List.Insert(index, item);
        OnChanged?.Invoke();
    }

    public bool Remove(T item)
    {
        bool ret = m_List.Remove(item);
        if (ret)
        {
            OnChanged?.Invoke();
        }

        return ret;
    }

    public void RemoveAt(int index)
    {
        m_List.RemoveAt(index);
        OnChanged?.Invoke();
    }

    public void Sort()
    {
        m_List.Sort();
    }

    public void Sort(System.Collections.Generic.IComparer<T> comparer)
    {
        m_List.Sort(comparer);
    }

    public void Sort(System.Comparison<T> comparison)
    {
        m_List.Sort(comparison);
    }

    public void Sort(int index, int count, System.Collections.Generic.IComparer<T> comparer)
    {
        m_List.Sort(index, count, comparer);
    }

    public void Clear()
    {
        m_List.Clear();
        OnChanged?.Invoke();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        m_List.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return m_List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}