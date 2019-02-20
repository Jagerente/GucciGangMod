//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Xffect")]
public class Xffect : MonoBehaviour
{
    private List<EffectLayer> EflList = new List<EffectLayer>();
    protected float ElapsedTime;
    public float LifeTime = -1f;
    private Dictionary<string, VertexPool> MatDic = new Dictionary<string, VertexPool>();

    public void Active()
    {
        var enumerator = transform.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (Transform) enumerator.Current;
                current.gameObject.SetActive(true);
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        gameObject.SetActive(true);
        ElapsedTime = 0f;
    }

    private void Awake()
    {
        Initialize();
    }

    public void DeActive()
    {
        var enumerator = transform.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (Transform) enumerator.Current;
                current.gameObject.SetActive(false);
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        if (EflList.Count <= 0)
        {
            var enumerator = transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    var component = (EffectLayer) current.GetComponent(typeof(EffectLayer));
                    if ((component != null) && (component.Material != null))
                    {
                        MeshFilter filter;
                        MeshRenderer renderer;
                        var material = component.Material;
                        EflList.Add(component);
                        var transform2 = transform.Find("mesh " + material.name);
                        if (transform2 != null)
                        {
                            filter = (MeshFilter) transform2.GetComponent(typeof(MeshFilter));
                            renderer = (MeshRenderer) transform2.GetComponent(typeof(MeshRenderer));
                            filter.mesh.Clear();
                            MatDic[material.name] = new VertexPool(filter.mesh, material);
                        }
                        if (!MatDic.ContainsKey(material.name))
                        {
                            var obj2 = new GameObject("mesh " + material.name) {
                                transform = { parent = transform }
                            };
                            obj2.AddComponent("MeshFilter");
                            obj2.AddComponent("MeshRenderer");
                            filter = (MeshFilter) obj2.GetComponent(typeof(MeshFilter));
                            renderer = (MeshRenderer) obj2.GetComponent(typeof(MeshRenderer));
                            renderer.castShadows = false;
                            renderer.receiveShadows = false;
                            renderer.renderer.material = material;
                            MatDic[material.name] = new VertexPool(filter.mesh, material);
                        }
                    }
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
            foreach (var layer2 in EflList)
            {
                layer2.Vertexpool = MatDic[layer2.Material.name];
            }
        }
    }

    private void LateUpdate()
    {
        foreach (var pair in MatDic)
        {
            pair.Value.LateUpdate();
        }
        if ((ElapsedTime > LifeTime) && (LifeTime >= 0f))
        {
            foreach (var layer in EflList)
            {
                layer.Reset();
            }
            DeActive();
            ElapsedTime = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
    }

    public void SetClient(Transform client)
    {
        foreach (var layer in EflList)
        {
            layer.ClientTransform = client;
        }
    }

    public void SetDirectionAxis(Vector3 axis)
    {
        foreach (var layer in EflList)
        {
            layer.OriVelocityAxis = axis;
        }
    }

    public void SetEmitPosition(Vector3 pos)
    {
        foreach (var layer in EflList)
        {
            layer.EmitPoint = pos;
        }
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        var enumerator = transform.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (Transform) enumerator.Current;
                current.transform.position = Vector3.zero;
                current.transform.rotation = Quaternion.identity;
                current.transform.localScale = Vector3.one;
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        foreach (var layer in EflList)
        {
            layer.StartCustom();
        }
    }

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        foreach (var layer in EflList)
        {
            if (ElapsedTime > layer.StartTime)
            {
                layer.FixedUpdateCustom();
            }
        }
    }
}

