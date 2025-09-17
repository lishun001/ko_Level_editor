using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveDictStruct<TKey, TValue> : IDictionary<TKey, TValue> where TValue : struct
{
    IDictionary<TKey, TValue> m_Dict;

    public event Action OnChanged;

    public TValue this[TKey key]
    {
        get { return m_Dict[key]; }
        set
        {
            bool hasOldValue = m_Dict.TryGetValue(key, out TValue oldVal);
            m_Dict[key] = value;
            
            if (!hasOldValue || !oldVal.Equals(value))
            {
                OnChanged?.Invoke();
            }
        }
    }

    public ICollection<TKey> Keys
    {
        get { return m_Dict.Keys; }
    }

    public ICollection<TValue> Values
    {
        get { return m_Dict.Values; }
    }

    public int Count
    {
        get { return m_Dict.Count; }
    }

    public bool IsReadOnly
    {
        get => false;
    }

    public ReactiveDictStruct()
    {
        m_Dict = new Dictionary<TKey, TValue>();
    }

    public ReactiveDictStruct(int capacity)
    {
        m_Dict = new Dictionary<TKey, TValue>(capacity);
    }

    public bool ContainsKey(TKey key)
    {
        return m_Dict.ContainsKey(key);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return m_Dict.Contains(item);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return m_Dict.TryGetValue(key, out value);
    }

    public void Add(TKey key, TValue value)
    {
        m_Dict.Add(key, value);
        OnChanged?.Invoke();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public bool Remove(TKey key)
    {
        bool ret = m_Dict.Remove(key);
        if (ret)
        {
            OnChanged?.Invoke();
        }
        return ret;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        bool ret = m_Dict.Remove(item);
        if (ret)
        {
            OnChanged?.Invoke();
        }
        return ret;
    }

    public void Clear()
    {
        m_Dict.Clear();
        OnChanged?.Invoke();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        m_Dict.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return m_Dict.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}