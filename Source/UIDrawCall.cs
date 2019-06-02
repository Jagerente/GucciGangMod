﻿using UnityEngine;

[AddComponentMenu("NGUI/Internal/Draw Call"), ExecuteInEditMode]
public class UIDrawCall : MonoBehaviour
{
    private Material mClippedMat;
    private Clipping mClipping;
    private Vector4 mClipRange;
    private Vector2 mClipSoft;
    private Material mDepthMat;
    private bool mDepthPass;
    private bool mEven = true;
    private MeshFilter mFilter;
    private int[] mIndices;
    private Mesh mMesh0;
    private Mesh mMesh1;
    private MeshRenderer mRen;
    private bool mReset = true;
    private Material mSharedMat;
    private Transform mTrans;

    private Mesh GetMesh(ref bool rebuildIndices, int vertexCount)
    {
        mEven = !mEven;
        if (mEven)
        {
            if (mMesh0 == null)
            {
                mMesh0 = new Mesh();
                mMesh0.hideFlags = HideFlags.DontSave;
                mMesh0.name = "Mesh0 for " + mSharedMat.name;
                mMesh0.MarkDynamic();
                rebuildIndices = true;
            }
            else if (rebuildIndices || mMesh0.vertexCount != vertexCount)
            {
                rebuildIndices = true;
                mMesh0.Clear();
            }
            return mMesh0;
        }
        if (mMesh1 == null)
        {
            mMesh1 = new Mesh();
            mMesh1.hideFlags = HideFlags.DontSave;
            mMesh1.name = "Mesh1 for " + mSharedMat.name;
            mMesh1.MarkDynamic();
            rebuildIndices = true;
        }
        else if (rebuildIndices || mMesh1.vertexCount != vertexCount)
        {
            rebuildIndices = true;
            mMesh1.Clear();
        }
        return mMesh1;
    }

    private void OnDestroy()
    {
        NGUITools.DestroyImmediate(mMesh0);
        NGUITools.DestroyImmediate(mMesh1);
        NGUITools.DestroyImmediate(mClippedMat);
        NGUITools.DestroyImmediate(mDepthMat);
    }

    private void OnWillRenderObject()
    {
        if (mReset)
        {
            mReset = false;
            UpdateMaterials();
        }
        if (mClippedMat != null)
        {
            mClippedMat.mainTextureOffset = new Vector2(-mClipRange.x / mClipRange.z, -mClipRange.y / mClipRange.w);
            mClippedMat.mainTextureScale = new Vector2(1f / mClipRange.z, 1f / mClipRange.w);
            var vector = new Vector2(1000f, 1000f);
            if (mClipSoft.x > 0f)
            {
                vector.x = mClipRange.z / mClipSoft.x;
            }
            if (mClipSoft.y > 0f)
            {
                vector.y = mClipRange.w / mClipSoft.y;
            }
            mClippedMat.SetVector("_ClipSharpness", vector);
        }
    }

    public void Set(BetterList<Vector3> verts, BetterList<Vector3> norms, BetterList<Vector4> tans, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        int size = verts.size;
        if (size > 0 && size == uvs.size && size == cols.size && size % 4 == 0)
        {
            if (mFilter == null)
            {
                mFilter = gameObject.GetComponent<MeshFilter>();
            }
            if (mFilter == null)
            {
                mFilter = gameObject.AddComponent<MeshFilter>();
            }
            if (mRen == null)
            {
                mRen = gameObject.GetComponent<MeshRenderer>();
            }
            if (mRen == null)
            {
                mRen = gameObject.AddComponent<MeshRenderer>();
                UpdateMaterials();
            }
            else if (mClippedMat != null && mClippedMat.mainTexture != mSharedMat.mainTexture)
            {
                UpdateMaterials();
            }
            if (verts.size < 0xfde8)
            {
                var num2 = (size >> 1) * 3;
                var rebuildIndices = mIndices == null || mIndices.Length != num2;
                if (rebuildIndices)
                {
                    mIndices = new int[num2];
                    var num3 = 0;
                    for (var i = 0; i < size; i += 4)
                    {
                        mIndices[num3++] = i;
                        mIndices[num3++] = i + 1;
                        mIndices[num3++] = i + 2;
                        mIndices[num3++] = i + 2;
                        mIndices[num3++] = i + 3;
                        mIndices[num3++] = i;
                    }
                }
                var mesh = GetMesh(ref rebuildIndices, verts.size);
                mesh.vertices = verts.ToArray();
                if (norms != null)
                {
                    mesh.normals = norms.ToArray();
                }
                if (tans != null)
                {
                    mesh.tangents = tans.ToArray();
                }
                mesh.uv = uvs.ToArray();
                mesh.colors32 = cols.ToArray();
                if (rebuildIndices)
                {
                    mesh.triangles = mIndices;
                }
                mesh.RecalculateBounds();
                mFilter.mesh = mesh;
            }
            else
            {
                if (mFilter.mesh != null)
                {
                    mFilter.mesh.Clear();
                }
                Debug.LogError("Too many vertices on one panel: " + verts.size);
            }
        }
        else
        {
            if (mFilter.mesh != null)
            {
                mFilter.mesh.Clear();
            }
            Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
        }
    }

    private void UpdateMaterials()
    {
        if (mClipping != Clipping.None)
        {
            Shader shader = null;
            if (mClipping != Clipping.None)
            {
                var str = mSharedMat.shader.name.Replace(" (AlphaClip)", string.Empty).Replace(" (SoftClip)", string.Empty);
                if (mClipping == Clipping.HardClip || mClipping == Clipping.AlphaClip)
                {
                    shader = Shader.Find(str + " (AlphaClip)");
                }
                else if (mClipping == Clipping.SoftClip)
                {
                    shader = Shader.Find(str + " (SoftClip)");
                }
                if (shader == null)
                {
                    mClipping = Clipping.None;
                }
            }
            if (shader != null)
            {
                if (mClippedMat == null)
                {
                    mClippedMat = new Material(mSharedMat);
                    mClippedMat.hideFlags = HideFlags.DontSave;
                }
                mClippedMat.shader = shader;
                mClippedMat.CopyPropertiesFromMaterial(mSharedMat);
            }
            else if (mClippedMat != null)
            {
                NGUITools.Destroy(mClippedMat);
                mClippedMat = null;
            }
        }
        else if (mClippedMat != null)
        {
            NGUITools.Destroy(mClippedMat);
            mClippedMat = null;
        }
        if (mDepthPass)
        {
            if (mDepthMat == null)
            {
                var shader2 = Shader.Find("Unlit/Depth Cutout");
                mDepthMat = new Material(shader2);
                mDepthMat.hideFlags = HideFlags.DontSave;
            }
            mDepthMat.mainTexture = mSharedMat.mainTexture;
        }
        else if (mDepthMat != null)
        {
            NGUITools.Destroy(mDepthMat);
            mDepthMat = null;
        }
        var material = mClippedMat == null ? mSharedMat : mClippedMat;
        if (mDepthMat != null)
        {
            if (mRen.sharedMaterials == null || mRen.sharedMaterials.Length != 2 || mRen.sharedMaterials[1] != material)
            {
                mRen.sharedMaterials = new[] { mDepthMat, material };
            }
        }
        else if (mRen.sharedMaterial != material)
        {
            mRen.sharedMaterials = new[] { material };
        }
    }

    public Transform cachedTransform
    {
        get
        {
            if (mTrans == null)
            {
                mTrans = transform;
            }
            return mTrans;
        }
    }

    public Clipping clipping
    {
        get
        {
            return mClipping;
        }
        set
        {
            if (mClipping != value)
            {
                mClipping = value;
                mReset = true;
            }
        }
    }

    public Vector4 clipRange
    {
        get
        {
            return mClipRange;
        }
        set
        {
            mClipRange = value;
        }
    }

    public Vector2 clipSoftness
    {
        get
        {
            return mClipSoft;
        }
        set
        {
            mClipSoft = value;
        }
    }

    public bool depthPass
    {
        get
        {
            return mDepthPass;
        }
        set
        {
            if (mDepthPass != value)
            {
                mDepthPass = value;
                mReset = true;
            }
        }
    }

    public bool isClipped
    {
        get
        {
            return mClippedMat != null;
        }
    }

    public Material material
    {
        get
        {
            return mSharedMat;
        }
        set
        {
            mSharedMat = value;
        }
    }

    public int triangles
    {
        get
        {
            var mesh = !mEven ? mMesh1 : mMesh0;
            return mesh == null ? 0 : mesh.vertexCount >> 1;
        }
    }

    public enum Clipping
    {
        None,
        HardClip,
        AlphaClip,
        SoftClip
    }
}

