//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XffectCache : MonoBehaviour
{
    private Dictionary<string, ArrayList> ObjectDic = new Dictionary<string, ArrayList>();

    protected Transform AddObject(string name)
    {
        var original = transform.Find(name);
        if (original == null)
        {
            Debug.Log("object:" + name + "doesn't exist!");
            return null;
        }
        var transform2 = Instantiate(original, Vector3.zero, Quaternion.identity) as Transform;
        ObjectDic[name].Add(transform2);
        transform2.gameObject.SetActive(false);
        var component = transform2.GetComponent<Xffect>();
        if (component != null)
        {
            component.Initialize();
        }
        return transform2;
    }

    private void Awake()
    {
        var enumerator = transform.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (Transform) enumerator.Current;
                ObjectDic[current.name] = new ArrayList();
                ObjectDic[current.name].Add(current);
                var component = current.GetComponent<Xffect>();
                if (component != null)
                {
                    component.Initialize();
                }
                current.gameObject.SetActive(false);
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
    }

    public Transform GetObject(string name)
    {
        var list = ObjectDic[name];
        if (list == null)
        {
            Debug.LogError(name + ": cache doesnt exist!");
            return null;
        }
        var enumerator = list.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (Transform) enumerator.Current;
                if (!current.gameObject.active)
                {
                    current.gameObject.SetActive(true);
                    return current;
                }
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        return AddObject(name);
    }

    public ArrayList GetObjectCache(string name)
    {
        var list = ObjectDic[name];
        if (list == null)
        {
            Debug.LogError(name + ": cache doesnt exist!");
            return null;
        }
        return list;
    }

    private void Start()
    {
    }
}

