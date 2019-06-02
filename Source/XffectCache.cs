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
        foreach (Transform transform in this.transform)
        {
            ObjectDic[transform.name] = new ArrayList();
            ObjectDic[transform.name].Add(transform);
            var component = transform.GetComponent<Xffect>();
            if (component != null)
            {
                component.Initialize();
            }

            transform.gameObject.SetActive(false);
        }
    }


    public Transform GetObject(string name)
    {
        var arrayList = ObjectDic[name];
        if (arrayList == null)
        {
            Debug.LogError(name + ": cache doesnt exist!");
            return null;
        }

        foreach (Transform transform in arrayList)
        {
            if (!transform.gameObject.activeInHierarchy)
            {
                transform.gameObject.SetActive(true);
                return transform;
            }
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