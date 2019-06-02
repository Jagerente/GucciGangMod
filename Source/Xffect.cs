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
        foreach (Transform transform in this.transform)
        {
            transform.gameObject.SetActive(true);
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
        foreach (Transform transform in this.transform)
        {
            transform.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }


    // Xffect
    public void Initialize()
    {
        if (EflList.Count > 0)
        {
            return;
        }

        foreach (Transform transform in this.transform)
        {
            var effectLayer = (EffectLayer) transform.GetComponent(typeof(EffectLayer));
            if (!(effectLayer == null) && !(effectLayer.Material == null))
            {
                var material = effectLayer.Material;
                EflList.Add(effectLayer);
                var transform2 = this.transform.Find("mesh " + material.name);
                if (transform2 != null)
                {
                    var meshFilter = (MeshFilter) transform2.GetComponent(typeof(MeshFilter));
                    var meshRenderer = (MeshRenderer) transform2.GetComponent(typeof(MeshRenderer));
                    meshFilter.mesh.Clear();
                    MatDic[material.name] = new VertexPool(meshFilter.mesh, material);
                }

                if (!MatDic.ContainsKey(material.name))
                {
                    var gameObject = new GameObject("mesh " + material.name);
                    gameObject.transform.parent = this.transform;
                    gameObject.AddComponent("MeshFilter");
                    gameObject.AddComponent("MeshRenderer");
                    var meshFilter = (MeshFilter) gameObject.GetComponent(typeof(MeshFilter));
                    var meshRenderer = (MeshRenderer) gameObject.GetComponent(typeof(MeshRenderer));
                    meshRenderer.castShadows = false;
                    meshRenderer.receiveShadows = false;
                    meshRenderer.renderer.material = material;
                    MatDic[material.name] = new VertexPool(meshFilter.mesh, material);
                }
            }
        }

        foreach (var current in EflList)
        {
            current.Vertexpool = MatDic[current.Material.name];
        }
    }


    private void LateUpdate()
    {
        foreach (var pair in MatDic)
        {
            pair.Value.LateUpdate();
        }

        if (ElapsedTime > LifeTime && LifeTime >= 0f)
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
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
        foreach (Transform transform in this.transform)
        {
            transform.transform.position = Vector3.zero;
            transform.transform.rotation = Quaternion.identity;
            transform.transform.localScale = Vector3.one;
        }

        foreach (var current in EflList)
        {
            current.StartCustom();
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