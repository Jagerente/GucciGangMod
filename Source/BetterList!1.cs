using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterList<T>
{
    public T[] buffer;
    public int size;

    public void Add(T item)
    {
        if (buffer == null || size == buffer.Length)
        {
            AllocateMore();
        }
        buffer[size++] = item;
    }

    private void AllocateMore()
    {
        var array = buffer == null ? new T[32] : new T[Mathf.Max(buffer.Length << 1, 32)];
        if (buffer != null && size > 0)
        {
            buffer.CopyTo(array, 0);
        }
        buffer = array;
    }

    public void Clear()
    {
        size = 0;
    }

    public bool Contains(T item)
    {
        if (buffer != null)
        {
            for (var i = 0; i < size; i++)
            {
                if (buffer[i].Equals(item))
                {
                    return true;
                }
            }
        }
        return false;
    }

    
    public IEnumerator<T> GetEnumerator()
    {
        return new GetEnumeratorc__Iterator9 {f__this = this };
    }

    public void Insert(int index, T item)
    {
        if (buffer == null || size == buffer.Length)
        {
            AllocateMore();
        }
        if (index < size)
        {
            for (var i = size; i > index; i--)
            {
                buffer[i] = buffer[i - 1];
            }
            buffer[index] = item;
            size++;
        }
        else
        {
            Add(item);
        }
    }

    public T Pop()
    {
        if (buffer != null && size != 0)
        {
            var local = buffer[--size];
            buffer[size] = default;
            return local;
        }
        return default;
    }

    public void Release()
    {
        size = 0;
        buffer = null;
    }

    public bool Remove(T item)
    {
        if (buffer != null)
        {
            var comparer = EqualityComparer<T>.Default;
            for (var i = 0; i < size; i++)
            {
                if (comparer.Equals(buffer[i], item))
                {
                    size--;
                    buffer[i] = default;
                    for (var j = i; j < size; j++)
                    {
                        buffer[j] = buffer[j + 1];
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveAt(int index)
    {
        if (buffer != null && index < size)
        {
            size--;
            buffer[index] = default;
            for (var i = index; i < size; i++)
            {
                buffer[i] = buffer[i + 1];
            }
        }
    }

    public void Sort(Comparison<T> comparer)
    {
        var flag = true;
        while (flag)
        {
            flag = false;
            for (var i = 1; i < size; i++)
            {
                if (comparer(buffer[i - 1], buffer[i]) > 0)
                {
                    var local = buffer[i];
                    buffer[i] = buffer[i - 1];
                    buffer[i - 1] = local;
                    flag = true;
                }
            }
        }
    }

    public T[] ToArray()
    {
        Trim();
        return buffer;
    }

    private void Trim()
    {
        if (size > 0)
        {
            if (size < buffer.Length)
            {
                var localArray = new T[size];
                for (var i = 0; i < size; i++)
                {
                    localArray[i] = buffer[i];
                }
                buffer = localArray;
            }
        }
        else
        {
            buffer = null;
        }
    }

    public T this[int i]
    {
        get
        {
            return buffer[i];
        }
        set
        {
            buffer[i] = value;
        }
    }

    
    private sealed class GetEnumeratorc__Iterator9 : IEnumerator, IDisposable, IEnumerator<T>
    {
        internal T current;
        internal int PC;
        internal BetterList<T> f__this;
        internal int __0;

        
        public void Dispose()
        {
            PC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) PC;
            PC = -1;
            switch (num)
            {
                case 0:
                    if (f__this.buffer == null)
                    {
                        goto Label_0086;
                    }
                    __0 = 0;
                    break;

                case 1:
                    __0++;
                    break;

                default:
                    goto Label_008D;
            }
            if (__0 < f__this.size)
            {
                current = f__this.buffer[__0];
                PC = 1;
                return true;
            }
        Label_0086:
            PC = -1;
        Label_008D:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        T IEnumerator<T>.Current
        {
            
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return current;
            }
        }
    }
}

