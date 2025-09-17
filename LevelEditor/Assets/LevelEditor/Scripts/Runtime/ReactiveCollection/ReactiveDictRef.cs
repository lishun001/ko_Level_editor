using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveDictRef<TKey, TValue> : IDictionary<TKey, TValue> where TValue : PropertyChangedBase
{
    IDictionary<TKey, TValue> m_Dict;

    public event Action OnChanged;
    
    private Dictionary<TValue, int> _subscriptionCounts = new Dictionary<TValue, int>();

    public TValue this[TKey key]
    {
        get { return m_Dict[key]; }
        set
        {
            bool hasOldValue = m_Dict.TryGetValue(key, out TValue oldVal);
            
            if (hasOldValue && oldVal != null)
            {
                UnsubscribeFromItem(oldVal);
            }
            
            m_Dict[key] = value;
            
            if (value != null)
            {
                SubscribeToItem(value);
            }
            
            if (!hasOldValue || !ReferenceEquals(oldVal, value))
            {
                OnChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// 订阅元素的属性变化事件
    /// </summary>
    private void SubscribeToItem(TValue item)
    {
        if (item != null)
        {
            if(_subscriptionCounts.TryGetValue(item, out int count) && count > 0)
            {
                _subscriptionCounts[item] = count + 1;
            }
            else
            {
                _subscriptionCounts[item] = 1;
                item.OnChanged += OnItemPropertyChanged;
            }
        }
    }

    /// <summary>
    /// 取消订阅元素的属性变化事件
    /// </summary>
    private void UnsubscribeFromItem(TValue item)
    {
        if (item != null)
        {
            if (_subscriptionCounts.TryGetValue(item, out int count))
            {
                if (count == 1)
                {
                    _subscriptionCounts[item] = 0;
                    item.OnChanged -= OnItemPropertyChanged;
                }
                else
                {
                    _subscriptionCounts[item] = count - 1;
                }
            }
        }
    }

    /// <summary>
    /// 处理元素属性变化事件
    /// </summary>
    private void OnItemPropertyChanged()
    {
        OnChanged?.Invoke();
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

    public ReactiveDictRef()
    {
        m_Dict = new Dictionary<TKey, TValue>();
    }

    public ReactiveDictRef(int capacity)
    {
        m_Dict = new Dictionary<TKey, TValue>(capacity);
    }

    public ReactiveDictRef(IDictionary<TKey, TValue> dictionary)
    {
        m_Dict = new Dictionary<TKey, TValue>(dictionary);
        // 订阅初始字典中所有元素的属性变化事件
        foreach (var kvp in m_Dict)
        {
            SubscribeToItem(kvp.Value);
        }
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
        SubscribeToItem(value);
        OnChanged?.Invoke();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public bool Remove(TKey key)
    {
        if (m_Dict.TryGetValue(key, out TValue value))
        {
            bool ret = m_Dict.Remove(key);
            if (ret)
            {
                UnsubscribeFromItem(value);
                OnChanged?.Invoke();
            }
            return ret;
        }
        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        bool ret = m_Dict.Remove(item);
        if (ret)
        {
            UnsubscribeFromItem(item.Value);
            OnChanged?.Invoke();
        }
        return ret;
    }

    public void Clear()
    {
        // 取消订阅所有元素的属性变化事件
        foreach (var kvp in m_Dict)
        {
            UnsubscribeFromItem(kvp.Value);
        }
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