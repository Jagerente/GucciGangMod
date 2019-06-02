using System.Collections.Generic;
using UnityEngine;

namespace Xft
{
    public class VertexPool
    {
        public const int BlockSize = 0x6c;
        public float BoundsScheduleTime = 1f;
        public bool ColorChanged;
        public Color[] Colors;
        public float ElapsedTime;
        public bool FirstUpdate = true;
        protected int IndexTotal;
        protected int IndexUsed;
        public bool IndiceChanged;
        public int[] Indices;
        public Material Material;
        public Mesh Mesh;
        protected List<VertexSegment> SegmentList = new List<VertexSegment>();
        public bool UV2Changed;
        public bool UVChanged;
        public Vector2[] UVs;
        public Vector2[] UVs2;
        public bool VertChanged;
        protected bool VertCountChanged;
        protected int VertexTotal;
        protected int VertexUsed;
        public Vector3[] Vertices;

        public VertexPool(Mesh mesh, Material material)
        {
            VertexTotal = VertexUsed = 0;
            VertCountChanged = false;
            Mesh = mesh;
            Material = material;
            InitArrays();
            IndiceChanged = ColorChanged = UVChanged = UV2Changed = VertChanged = true;
        }

        public void EnlargeArrays(int count, int icount)
        {
            var vertices = Vertices;
            Vertices = new Vector3[Vertices.Length + count];
            vertices.CopyTo(Vertices, 0);
            var uVs = UVs;
            UVs = new Vector2[UVs.Length + count];
            uVs.CopyTo(UVs, 0);
            var vectorArray3 = UVs2;
            UVs2 = new Vector2[UVs2.Length + count];
            vectorArray3.CopyTo(UVs2, 0);
            InitDefaultShaderParam(UVs2);
            var colors = Colors;
            Colors = new Color[Colors.Length + count];
            colors.CopyTo(Colors, 0);
            var indices = Indices;
            Indices = new int[Indices.Length + icount];
            indices.CopyTo(Indices, 0);
            VertCountChanged = true;
            IndiceChanged = true;
            ColorChanged = true;
            UVChanged = true;
            VertChanged = true;
            UV2Changed = true;
        }

        public Material GetMaterial()
        {
            return Material;
        }

        public VertexSegment GetRopeVertexSeg(int maxcount)
        {
            return GetVertices(maxcount * 2, (maxcount - 1) * 6);
        }

        public VertexSegment GetVertices(int vcount, int icount)
        {
            var count = 0;
            var num2 = 0;
            if (VertexUsed + vcount >= VertexTotal)
            {
                count = (vcount / 0x6c + 1) * 0x6c;
            }

            if (IndexUsed + icount >= IndexTotal)
            {
                num2 = (icount / 0x6c + 1) * 0x6c;
            }

            VertexUsed += vcount;
            IndexUsed += icount;
            if (count != 0 || num2 != 0)
            {
                EnlargeArrays(count, num2);
                VertexTotal += count;
                IndexTotal += num2;
            }

            return new VertexSegment(VertexUsed - vcount, vcount, IndexUsed - icount, icount, this);
        }

        protected void InitArrays()
        {
            Vertices = new Vector3[4];
            UVs = new Vector2[4];
            UVs2 = new Vector2[4];
            Colors = new Color[4];
            Indices = new int[6];
            VertexTotal = 4;
            IndexTotal = 6;
            InitDefaultShaderParam(UVs2);
        }

        private void InitDefaultShaderParam(Vector2[] uv2)
        {
            for (var i = 0; i < uv2.Length; i++)
            {
                uv2[i].x = 1f;
                uv2[i].y = 0f;
            }
        }

        public void LateUpdate()
        {
            if (VertCountChanged)
            {
                Mesh.Clear();
            }

            Mesh.vertices = Vertices;
            if (UVChanged)
            {
                Mesh.uv = UVs;
            }

            if (UV2Changed)
            {
                Mesh.uv2 = UVs2;
            }

            if (ColorChanged)
            {
                Mesh.colors = Colors;
            }

            if (IndiceChanged)
            {
                Mesh.triangles = Indices;
            }

            ElapsedTime += Time.deltaTime;
            if (ElapsedTime > BoundsScheduleTime || FirstUpdate)
            {
                RecalculateBounds();
                ElapsedTime = 0f;
            }

            if (ElapsedTime > BoundsScheduleTime)
            {
                FirstUpdate = false;
            }

            VertCountChanged = false;
            IndiceChanged = false;
            ColorChanged = false;
            UVChanged = false;
            UV2Changed = false;
            VertChanged = false;
        }

        public void RecalculateBounds()
        {
            Mesh.RecalculateBounds();
        }

        public class VertexSegment
        {
            public int IndexCount;
            public int IndexStart;
            public VertexPool Pool;
            public int VertCount;
            public int VertStart;

            public VertexSegment(int start, int count, int istart, int icount, VertexPool pool)
            {
                VertStart = start;
                VertCount = count;
                IndexCount = icount;
                IndexStart = istart;
                Pool = pool;
            }

            public void ClearIndices()
            {
                for (var i = IndexStart; i < IndexStart + IndexCount; i++)
                {
                    Pool.Indices[i] = 0;
                }

                Pool.IndiceChanged = true;
            }
        }
    }
}