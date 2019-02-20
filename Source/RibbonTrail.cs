//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RibbonTrail
{
    public const int CHAIN_EMPTY = 0x1869f;
    protected Color Color = Color.white;
    protected float ElapsedTime;
    public int ElemCount;
    public Element[] ElementArray;
    protected float ElemLength;
    protected float Fps;
    public int Head;
    protected Vector3 HeadPosition;
    protected bool IndexDirty;
    protected Vector2 LowerLeftUV;
    public int MaxElements;
    public float SquaredElemLength;
    protected int StretchType;
    public int Tail;
    protected float TrailLength;
    protected float UnitWidth;
    protected Vector2 UVDimensions;
    protected VertexPool.VertexSegment Vertexsegment;

    public RibbonTrail(VertexPool.VertexSegment segment, float width, int maxelemnt, float len, Vector3 pos, int stretchType, float maxFps)
    {
        if (maxelemnt <= 2)
        {
            Debug.LogError("ribbon trail's maxelement should > 2!");
        }
        MaxElements = maxelemnt;
        Vertexsegment = segment;
        ElementArray = new Element[MaxElements];
        Head = Tail = 0x1869f;
        SetTrailLen(len);
        UnitWidth = width;
        HeadPosition = pos;
        StretchType = stretchType;
        var dtls = new Element(HeadPosition, UnitWidth);
        IndexDirty = false;
        Fps = 1f / maxFps;
        AddElememt(dtls);
        var element2 = new Element(HeadPosition, UnitWidth);
        AddElememt(element2);
    }

    public void AddElememt(Element dtls)
    {
        if (Head == 0x1869f)
        {
            Tail = MaxElements - 1;
            Head = Tail;
            IndexDirty = true;
            ElemCount++;
        }
        else
        {
            if (Head == 0)
            {
                Head = MaxElements - 1;
            }
            else
            {
                Head--;
            }
            if (Head == Tail)
            {
                if (Tail == 0)
                {
                    Tail = MaxElements - 1;
                }
                else
                {
                    Tail--;
                }
            }
            else
            {
                ElemCount++;
            }
        }
        ElementArray[Head] = dtls;
        IndexDirty = true;
    }

    public void Reset()
    {
        ResetElementsPos();
    }

    public void ResetElementsPos()
    {
        if ((Head != 0x1869f) && (Head != Tail))
        {
            var head = Head;
            while (true)
            {
                var index = head;
                if (index == MaxElements)
                {
                    index = 0;
                }
                ElementArray[index].Position = HeadPosition;
                if (index == Tail)
                {
                    break;
                }
                head = index + 1;
            }
        }
    }

    public void SetColor(Color color)
    {
        Color = color;
    }

    public void SetHeadPosition(Vector3 pos)
    {
        HeadPosition = pos;
    }

    public void SetTrailLen(float len)
    {
        TrailLength = len;
        ElemLength = TrailLength / (MaxElements - 1);
        SquaredElemLength = ElemLength * ElemLength;
    }

    public void SetUVCoord(Vector2 lowerleft, Vector2 dimensions)
    {
        LowerLeftUV = lowerleft;
        UVDimensions = dimensions;
    }

    public void Smooth()
    {
        if (ElemCount > 3)
        {
            var element = ElementArray[Head];
            var index = Head + 1;
            if (index == MaxElements)
            {
                index = 0;
            }
            var num2 = index + 1;
            if (num2 == MaxElements)
            {
                num2 = 0;
            }
            var element2 = ElementArray[index];
            var element3 = ElementArray[num2];
            var from = element.Position - element2.Position;
            var to = element2.Position - element3.Position;
            var num3 = Vector3.Angle(from, to);
            if (num3 > 60f)
            {
                var vector3 = (element.Position + element3.Position) / 2f;
                var vector4 = vector3 - element2.Position;
                var zero = Vector3.zero;
                var smoothTime = 0.1f / (num3 / 60f);
                element2.Position = Vector3.SmoothDamp(element2.Position, element2.Position + vector4.normalized * element2.Width, ref zero, smoothTime);
            }
        }
    }

    public void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= Fps)
        {
            ElapsedTime -= Fps;
            var flag = false;
            while (!flag)
            {
                var element = ElementArray[Head];
                var index = Head + 1;
                if (index == MaxElements)
                {
                    index = 0;
                }
                var element2 = ElementArray[index];
                var headPosition = HeadPosition;
                var vector2 = headPosition - element2.Position;
                if (vector2.sqrMagnitude >= SquaredElemLength)
                {
                    var vector3 = vector2 * (ElemLength / vector2.magnitude);
                    element.Position = element2.Position + vector3;
                    var dtls = new Element(headPosition, UnitWidth);
                    AddElememt(dtls);
                    vector2 = headPosition - element.Position;
                    if (vector2.sqrMagnitude <= SquaredElemLength)
                    {
                        flag = true;
                    }
                }
                else
                {
                    element.Position = headPosition;
                    flag = true;
                }
                if (((Tail + 1) % MaxElements) == Head)
                {
                    int num3;
                    var element4 = ElementArray[Tail];
                    if (Tail == 0)
                    {
                        num3 = MaxElements - 1;
                    }
                    else
                    {
                        num3 = Tail - 1;
                    }
                    var element5 = ElementArray[num3];
                    var vector4 = element4.Position - element5.Position;
                    var magnitude = vector4.magnitude;
                    if (magnitude > 1E-06)
                    {
                        var num5 = ElemLength - vector2.magnitude;
                        vector4 = vector4 * (num5 / magnitude);
                        element4.Position = element5.Position + vector4;
                    }
                }
            }
            var position = Camera.main.transform.position;
            UpdateVertices(position);
            UpdateIndices();
        }
    }

    public void UpdateIndices()
    {
        if (IndexDirty)
        {
            var pool = Vertexsegment.Pool;
            if ((Head != 0x1869f) && (Head != Tail))
            {
                var head = Head;
                var num2 = 0;
                while (true)
                {
                    var num3 = head + 1;
                    if (num3 == MaxElements)
                    {
                        num3 = 0;
                    }
                    if ((num3 * 2) >= 0x10000)
                    {
                        Debug.LogError("Too many elements!");
                    }
                    var num4 = Vertexsegment.VertStart + (num3 * 2);
                    var num5 = Vertexsegment.VertStart + (head * 2);
                    var index = Vertexsegment.IndexStart + (num2 * 6);
                    pool.Indices[index] = num5;
                    pool.Indices[index + 1] = num5 + 1;
                    pool.Indices[index + 2] = num4;
                    pool.Indices[index + 3] = num5 + 1;
                    pool.Indices[index + 4] = num4 + 1;
                    pool.Indices[index + 5] = num4;
                    if (num3 == Tail)
                    {
                        break;
                    }
                    head = num3;
                    num2++;
                }
                pool.IndiceChanged = true;
            }
            IndexDirty = false;
        }
    }

    public void UpdateVertices(Vector3 eyePos)
    {
        Vector3 vector;
        var num = 0f;
        var num2 = 0f;
        var num3 = ElemLength * (MaxElements - 2);
        if ((Head == 0x1869f) || (Head == Tail))
        {
            return;
        }
        var head = Head;
        var index = Head;
    Label_0052:
        if (index == MaxElements)
        {
            index = 0;
        }
        var element = ElementArray[index];
        if ((index * 2) >= 0x10000)
        {
            Debug.LogError("Too many elements!");
        }
        var num6 = Vertexsegment.VertStart + (index * 2);
        var num7 = index + 1;
        if (num7 == MaxElements)
        {
            num7 = 0;
        }
        if (index == Head)
        {
            vector = ElementArray[num7].Position - element.Position;
        }
        else if (index == Tail)
        {
            vector = element.Position - ElementArray[head].Position;
        }
        else
        {
            vector = ElementArray[num7].Position - ElementArray[head].Position;
        }
        var rhs = eyePos - element.Position;
        var vector3 = Vector3.Cross(vector, rhs);
        vector3.Normalize();
        vector3 = vector3 * (element.Width * 0.5f);
        var vector4 = element.Position - vector3;
        var vector5 = element.Position + vector3;
        var pool = Vertexsegment.Pool;
        if (StretchType == 0)
        {
            num = (num2 / num3) * Mathf.Abs(UVDimensions.y);
        }
        else
        {
            num = (num2 / num3) * Mathf.Abs(UVDimensions.x);
        }
        var zero = Vector2.zero;
        pool.Vertices[num6] = vector4;
        pool.Colors[num6] = Color;
        if (StretchType == 0)
        {
            zero.x = LowerLeftUV.x + UVDimensions.x;
            zero.y = LowerLeftUV.y - num;
        }
        else
        {
            zero.x = LowerLeftUV.x + num;
            zero.y = LowerLeftUV.y;
        }
        pool.UVs[num6] = zero;
        pool.Vertices[num6 + 1] = vector5;
        pool.Colors[num6 + 1] = Color;
        if (StretchType == 0)
        {
            zero.x = LowerLeftUV.x;
            zero.y = LowerLeftUV.y - num;
        }
        else
        {
            zero.x = LowerLeftUV.x + num;
            zero.y = LowerLeftUV.y - Mathf.Abs(UVDimensions.y);
        }
        pool.UVs[num6 + 1] = zero;
        if (index != Tail)
        {
            head = index;
            var vector7 = ElementArray[num7].Position - element.Position;
            num2 += vector7.magnitude;
            index++;
            goto Label_0052;
        }
        Vertexsegment.Pool.UVChanged = true;
        Vertexsegment.Pool.VertChanged = true;
        Vertexsegment.Pool.ColorChanged = true;
    }

    public class Element
    {
        public Vector3 Position;
        public float Width;

        public Element(Vector3 position, float width)
        {
            Position = position;
            Width = width;
        }
    }
}

