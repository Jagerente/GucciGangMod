//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

    [DebuggerHidden]
    public IEnumerator<T> GetEnumerator()
    {
        return new GetEnumeratorcIterator9<T> { fthis = this };
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
            buffer[size] = default(T);
            return local;
        }
        return default(T);
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
                    buffer[i] = default(T);
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
            buffer[index] = default(T);
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

    [CompilerGenerated]
    private sealed class GetEnumeratorcIterator9<T> : IEnumerator, IDisposable, IEnumerator<T>
    {
        internal T Scurrent;
        internal int SPC;
        internal BetterList<T> fthis;
        internal int i0;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    if (fthis.buffer == null)
                    {
                        goto Label_0086;
                    }
                    i0 = 0;
                    break;

                case 1:
                    i0++;
                    break;

                default:
                    goto Label_008D;
            }
            if (i0 < fthis.size)
            {
                Scurrent = fthis.buffer[i0];
                SPC = 1;
                return true;
            }
        Label_0086:
            SPC = -1;
        Label_008D:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        T IEnumerator<T>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }
}

