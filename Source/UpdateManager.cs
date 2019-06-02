using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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

        var item = new UpdateEntry
        {
            index = updateOrder,
            func = func,
            mb = mb,
            isMonoBehaviour = mb != null
        };
        list.Add(item);
        if (updateOrder != 0)
        {
            list.Sort(Compare);
        }
    }

    public static void AddCoroutine(MonoBehaviour mb, int updateOrder, OnUpdate func)
    {
        CreateInstance();
        mInst.Add(mb, updateOrder, func, mInst.mOnCoro);
    }

    public static void AddDestroy(Object obj, float delay)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
            {
                if (delay > 0f)
                {
                    CreateInstance();
                    var item = new DestroyEntry
                    {
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


    private IEnumerator CoroutineFunction()
    {
        return new CoroutineFunctionc__Iterator8 {f__this = this};
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
            int size = mDest.size;
            while (size > 0)
            {
                DestroyEntry entry = mDest.buffer[--size];
                if (!isPlaying || entry.time < mTime)
                {
                    if (entry.obj != null)
                    {
                        NGUITools.Destroy(entry.obj);
                        entry.obj = null;
                    }

                    mDest.RemoveAt(size);
                }
            }

            if (mOnUpdate.Count == 0 && mOnLate.Count == 0 && mOnCoro.Count == 0 && mDest.size == 0)
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
            if (mInst == null && Application.isPlaying)
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


    private sealed class CoroutineFunctionc__Iterator8 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal UpdateManager f__this;


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
                case 1:
                    if (Application.isPlaying)
                    {
                        if (!f__this.CoroutineUpdate())
                        {
                            break;
                        }

                        current = null;
                        PC = 1;
                        return true;
                    }

                    break;

                default:
                    goto Label_0061;
            }

            PC = -1;
            Label_0061:
            return false;
        }


        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            get { return current; }
        }

        object IEnumerator.Current
        {
            get { return current; }
        }
    }

    public class DestroyEntry
    {
        public Object obj;
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