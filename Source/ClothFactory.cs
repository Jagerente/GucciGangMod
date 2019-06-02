﻿using System.Collections.Generic;
using UnityEngine;

public static class ClothFactory
{
    private static Dictionary<string, List<GameObject>> clothCache = new Dictionary<string, List<GameObject>>(CostumeHair.hairsF.Length);

    public static void ClearClothCache()
    {
        clothCache.Clear();
    }

    public static void DisposeObject(GameObject cachedObject)
    {
        if (cachedObject != null)
        {
            var component = cachedObject.GetComponent<ParentFollow>();
            if (component != null)
            {
                if (component.isActiveInScene)
                {
                    cachedObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                    cachedObject.GetComponent<Cloth>().enabled = false;
                    component.isActiveInScene = false;
                    cachedObject.transform.position = new Vector3(0f, -99999f, 0f);
                    cachedObject.GetComponent<ParentFollow>().RemoveParent();
                }
            }
            else
            {
                Object.Destroy(cachedObject);
            }
        }
    }

    private static GameObject GenerateCloth(GameObject go, string res)
    {
        if (go.GetComponent<SkinnedMeshRenderer>() == null)
        {
            go.AddComponent<SkinnedMeshRenderer>();
        }
        var bones = go.GetComponent<SkinnedMeshRenderer>().bones;
        var component = ((GameObject) Object.Instantiate(Resources.Load(res))).GetComponent<SkinnedMeshRenderer>();
        component.transform.localScale = Vector3.one;
        component.bones = bones;
        component.quality = SkinQuality.Bone4;
        return component.gameObject;
    }

    public static GameObject GetCape(GameObject reference, string name, Material material)
    {
        List<GameObject> list;
        GameObject obj4;
        if (clothCache.TryGetValue(name, out list))
        {
            for (var i = 0; i < list.Count; i++)
            {
                var clothObject = list[i];
                if (clothObject == null)
                {
                    list.RemoveAt(i);
                    i = Mathf.Max(i - 1, 0);
                }
                else
                {
                    var component = clothObject.GetComponent<ParentFollow>();
                    if (!component.isActiveInScene)
                    {
                        component.isActiveInScene = true;
                        clothObject.renderer.material = material;
                        clothObject.GetComponent<Cloth>().enabled = true;
                        clothObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                        clothObject.GetComponent<ParentFollow>().SetParent(reference.transform);
                        ReapplyClothBones(reference, clothObject);
                        return clothObject;
                    }
                }
            }
            obj4 = GenerateCloth(reference, name);
            obj4.renderer.material = material;
            obj4.AddComponent<ParentFollow>().SetParent(reference.transform);
            list.Add(obj4);
            clothCache[name] = list;
            return obj4;
        }
        obj4 = GenerateCloth(reference, name);
        obj4.renderer.material = material;
        obj4.AddComponent<ParentFollow>().SetParent(reference.transform);
        list = new List<GameObject> {
            obj4
        };
        clothCache.Add(name, list);
        return obj4;
    }

    public static string GetDebugInfo()
    {
        var num = 0;
        foreach (var pair in clothCache)
        {
            num += clothCache[pair.Key].Count;
        }
        var num2 = 0;
        foreach (var cloth in Object.FindObjectsOfType<Cloth>())
        {
            if (cloth.enabled)
            {
                num2++;
            }
        }
        return string.Format("{0} cached cloths, {1} active cloths, {2} types cached", num, num2, clothCache.Keys.Count);
    }

    public static GameObject GetHair(GameObject reference, string name, Material material, Color color)
    {
        List<GameObject> list;
        GameObject obj4;
        if (clothCache.TryGetValue(name, out list))
        {
            for (var i = 0; i < list.Count; i++)
            {
                var clothObject = list[i];
                if (clothObject == null)
                {
                    list.RemoveAt(i);
                    i = Mathf.Max(i - 1, 0);
                }
                else
                {
                    var component = clothObject.GetComponent<ParentFollow>();
                    if (!component.isActiveInScene)
                    {
                        component.isActiveInScene = true;
                        clothObject.renderer.material = material;
                        clothObject.renderer.material.color = color;
                        clothObject.GetComponent<Cloth>().enabled = true;
                        clothObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                        clothObject.GetComponent<ParentFollow>().SetParent(reference.transform);
                        ReapplyClothBones(reference, clothObject);
                        return clothObject;
                    }
                }
            }
            obj4 = GenerateCloth(reference, name);
            obj4.renderer.material = material;
            obj4.renderer.material.color = color;
            obj4.AddComponent<ParentFollow>().SetParent(reference.transform);
            list.Add(obj4);
            clothCache[name] = list;
            return obj4;
        }
        obj4 = GenerateCloth(reference, name);
        obj4.renderer.material = material;
        obj4.renderer.material.color = color;
        obj4.AddComponent<ParentFollow>().SetParent(reference.transform);
        list = new List<GameObject> {
            obj4
        };
        clothCache.Add(name, list);
        return obj4;
    }

    private static void ReapplyClothBones(GameObject reference, GameObject clothObject)
    {
        var component = reference.GetComponent<SkinnedMeshRenderer>();
        var renderer2 = clothObject.GetComponent<SkinnedMeshRenderer>();
        renderer2.bones = component.bones;
        renderer2.transform.localScale = Vector3.one;
    }
}

