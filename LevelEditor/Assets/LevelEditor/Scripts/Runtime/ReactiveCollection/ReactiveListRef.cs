using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveListRef<T> : IList<T> where T : PropertyChangedBase
{
    List<T> m_List;

    public event Action OnChanged;
    
    private Dictionary<T, int> _subscriptionCounts = new Dictionary<T, int>();

    public T this[int index]
    {
        get { return m_List[index]; }
        set
        {
            T oldVal = m_List[index];
            if (oldVal != null)
            {
                UnsubscribeFromItem(oldVal);
            }
            
            m_List[index] = value;
            
            if (value != null)
            {
                SubscribeToItem(value);
            }
            
            if (!ReferenceEquals(oldVal, value))
            {
                OnChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// 订阅元素的属性变化事件
    /// </summary>
    private void SubscribeToItem(T item)
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
    private void UnsubscribeFromItem(T item)
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

    public int Count
    {
        get { return m_List.Count; }
    }

    public bool IsReadOnly
    {
        get => false;
    }

    public ReactiveListRef()
        : this(0)
    {
    }

    public ReactiveListRef(int capacity)
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
        SubscribeToItem(item);
        OnChanged?.Invoke();
    }

    public void Insert(int index, T item)
    {
        m_List.Insert(index, item);
        SubscribeToItem(item);
        OnChanged?.Invoke();
    }

    public bool Remove(T item)
    {
        bool ret = m_List.Remove(item);
        if (ret)
        {
            UnsubscribeFromItem(item);
            OnChanged?.Invoke();
        }

        return ret;
    }
    
    public void RemoveAt(int index)
    {
        var item = m_List[index];
        m_List.RemoveAt(index);
        UnsubscribeFromItem(item);
        OnChanged?.Invoke();
    }

    public void Clear()
    {
        // 取消订阅所有元素的属性变化事件
        foreach (var item in m_List)
        {
            UnsubscribeFromItem(item);
        }
        m_List.Clear();
        OnChanged?.Invoke();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        m_List.CopyTo(array, arrayIndex);
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

    public IEnumerator<T> GetEnumerator()
    {
        return m_List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}