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

[AddComponentMenu("NGUI/Internal/Update Manager"), ExecuteInEditMode]
public class UpdateManager : MonoBehaviour
{
    private BetterList<DestroyEntry> mDest = new BetterList<DestroyEntry>();
    private static UpdateManager mInst;
    private List<UpdateEntry> mOnCoro = new List<UpdateEntry>();
    private List<UpdateEntry> mOnLate = new List<UpdateEntry>();
    private List<UpdateEntry> mOnUpdate = new List<UpdateEntry>();
    private float mTime;

    private void Add(MonoBehaviour mb, int updateOrder, OnUpdate func, List<UpdateEntry> list)
    {
        var num = 0;
        var count = list.Count;
        while (num < count)
        {
            var entry = list[num];
            if (entry.func == func)
            {
                return;
            }
            num++;
        }
        var item = new UpdateEntry {
            index = updateOrder,
            func = func,
            mb = mb,
            isMonoBehaviour = mb != null
        };
        list.Add(item);
        if (updateOrder != 0)
        {
            list.Sort(new Comparison<UpdateEntry>(Compare));
        }
    }

    public static void AddCoroutine(MonoBehaviour mb, int updateOrder, OnUpdate func)
    {
        CreateInstance();
        mInst.Add(mb, updateOrder, func, mInst.mOnCoro);
    }

    public static void AddDestroy(UnityEngine.Object obj, float delay)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
            {
                if (delay > 0f)
                {
                    CreateInstance();
                    var item = new DestroyEntry {
                        obj = obj,
                        time = Time.realtimeSinceStartup + delay
                    };
                    mInst.mDest.Add(item);
                }
                else
                {
                    Destroy(obj);
                }
            }
            else
            {
                DestroyImmediate(obj);
            }
        }
    }

    public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, OnUpdate func)
    {
        CreateInstance();
        mInst.Add(mb, updateOrder, func, mInst.mOnLate);
    }

    public static void AddUpdate(MonoBehaviour mb, int updateOrder, OnUpdate func)
    {
        CreateInstance();
        mInst.Add(mb, updateOrder, func, mInst.mOnUpdate);
    }

    private static int Compare(UpdateEntry a, UpdateEntry b)
    {
        if (a.index < b.index)
        {
            return 1;
        }
        if (a.index > b.index)
        {
            return -1;
        }
        return 0;
    }

    [DebuggerHidden]
    private IEnumerator CoroutineFunction()
    {
        return new CoroutineFunctioncIterator8 { fthis = this };
    }

    private bool CoroutineUpdate()
    {
        var realtimeSinceStartup = Time.realtimeSinceStartup;
        var delta = realtimeSinceStartup - mTime;
        if (delta >= 0.001f)
        {
            mTime = realtimeSinceStartup;
            UpdateList(mOnCoro, delta);
            var isPlaying = Application.isPlaying;
            var size = mDest.size;
            while (size > 0)
            {
                var entry = mDest.buffer[--size];
                if (!isPlaying || (entry.time < mTime))
                {
                    if (entry.obj != null)
                    {
                        NGUITools.Destroy(entry.obj);
                        entry.obj = null;
                    }
                    mDest.RemoveAt(size);
                }
            }
            if (((mOnUpdate.Count == 0) && (mOnLate.Count == 0)) && ((mOnCoro.Count == 0) && (mDest.size == 0)))
            {
                NGUITools.Destroy(gameObject);
                return false;
            }
        }
        return true;
    }

    private static void CreateInstance()
    {
        if (mInst == null)
        {
            mInst = FindObjectOfType(typeof(UpdateManager)) as UpdateManager;
            if ((mInst == null) && Application.isPlaying)
            {
                var target = new GameObject("_UpdateManager");
                DontDestroyOnLoad(target);
                mInst = target.AddComponent<UpdateManager>();
            }
        }
    }

    private void LateUpdate()
    {
        UpdateList(mOnLate, Time.deltaTime);
        if (!Application.isPlaying)
        {
            CoroutineUpdate();
        }
    }

    private void OnApplicationQuit()
    {
        DestroyImmediate(gameObject);
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            mTime = Time.realtimeSinceStartup;
            StartCoroutine(CoroutineFunction());
        }
    }

    private void Update()
    {
        if (mInst != this)
        {
            NGUITools.Destroy(gameObject);
        }
        else
        {
            UpdateList(mOnUpdate, Time.deltaTime);
        }
    }

    private void UpdateList(List<UpdateEntry> list, float delta)
    {
        var count = list.Count;
        while (count > 0)
        {
            var entry = list[--count];
            if (entry.isMonoBehaviour)
            {
                if (entry.mb == null)
                {
                    list.RemoveAt(count);
                    continue;
                }
                if (!entry.mb.enabled || !NGUITools.GetActive(entry.mb.gameObject))
                {
                    continue;
                }
            }
            entry.func(delta);
        }
    }

    [CompilerGenerated]
    private sealed class CoroutineFunctioncIterator8 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal UpdateManager fthis;

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
                case 1:
                    if (Application.isPlaying)
                    {
                        if (!fthis.CoroutineUpdate())
                        {
                            break;
                        }
                        Scurrent = null;
                        SPC = 1;
                        return true;
                    }
                    break;

                default:
                    goto Label_0061;
            }
            SPC = -1;
        Label_0061:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
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

    public class DestroyEntry
    {
        public UnityEngine.Object obj;
        public float time;
    }

    public delegate void OnUpdate(float delta);

    public class UpdateEntry
    {
        public OnUpdate func;
        public int index;
        public bool isMonoBehaviour;
        public MonoBehaviour mb;
    }
}

