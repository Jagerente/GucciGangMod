//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections;
using UnityEngine;

public class EffectLayer : MonoBehaviour
{
    public EffectNode[] ActiveENodes;
    public bool AlongVelocity;
    public int AngleAroundAxis;
    public bool AttractionAffectorEnable;
    public AnimationCurve AttractionCurve;
    public Vector3 AttractionPosition;
    public float AttractMag = 0.1f;
    public EffectNode[] AvailableENodes;
    public int AvailableNodeCount;
    public Vector3 BoxSize;
    public float ChanceToEmit = 100f;
    public Vector3 CircleDir;
    public Transform ClientTransform;
    public Color Color1 = Color.white;
    public Color Color2;
    public Color Color3;
    public Color Color4;
    public bool ColorAffectorEnable;
    public int ColorAffectType;
    public float ColorGradualTimeLength = 1f;
    public COLOR_GRADUAL_TYPE ColorGradualType;
    public int Cols = 1;
    public float DeltaRot;
    public float DeltaScaleX;
    public float DeltaScaleY;
    public float DiffDistance = 0.1f;
    public int EanIndex;
    public string EanPath = "none";
    public float EmitDelay;
    public float EmitDuration = 10f;
    public int EmitLoop = 1;
    public Vector3 EmitPoint;
    public int EmitRate = 20;
    protected Emitter emitter;
    public int EmitType;
    public bool IsEmitByDistance;
    public bool IsNodeLifeLoop = true;
    public bool IsRandomDir;
    public bool JetAffectorEnable;
    public float JetMax;
    public float JetMin;
    public Vector3 LastClientPos;
    public Vector3 LinearForce;
    public bool LinearForceAffectorEnable;
    public float LinearMagnitude = 1f;
    public float LineLengthLeft = -1f;
    public float LineLengthRight = 1f;
    public int LoopCircles = -1;
    protected Camera MainCamera;
    public Material Material;
    public int MaxENodes = 1;
    public float MaxFps = 60f;
    public int MaxRibbonElements = 6;
    public float NodeLifeMax = 1f;
    public float NodeLifeMin = 1f;
    public Vector2 OriLowerLeftUV = Vector2.zero;
    public int OriPoint;
    public int OriRotationMax;
    public int OriRotationMin;
    public float OriScaleXMax = 1f;
    public float OriScaleXMin = 1f;
    public float OriScaleYMax = 1f;
    public float OriScaleYMin = 1f;
    public float OriSpeed;
    public Vector2 OriUVDimensions = Vector2.one;
    public Vector3 OriVelocityAxis;
    public float Radius;
    public bool RandomOriRot;
    public bool RandomOriScale;
    public int RenderType;
    public float RibbonLen = 1f;
    public float RibbonWidth = 0.5f;
    public bool RotAffectorEnable;
    public AnimationCurve RotateCurve;
    public RSTYPE RotateType;
    public int Rows = 1;
    public bool ScaleAffectorEnable;
    public RSTYPE ScaleType;
    public AnimationCurve ScaleXCurve;
    public AnimationCurve ScaleYCurve;
    public float SpriteHeight = 1f;
    public int SpriteType;
    public int SpriteUVStretch;
    public float SpriteWidth = 1f;
    public float StartTime;
    public int StretchType;
    public bool SyncClient;
    public float TailDistance;
    public bool UseAttractCurve;
    public bool UseVortexCurve;
    public bool UVAffectorEnable;
    public float UVTime = 30f;
    public int UVType;
    public VertexPool Vertexpool;
    public bool VortexAffectorEnable;
    public AnimationCurve VortexCurve;
    public Vector3 VortexDirection;
    public float VortexMag = 0.1f;

    public void AddActiveNode(EffectNode node)
    {
        if (AvailableNodeCount == 0)
        {
            Debug.LogError("out index!");
        }
        if (AvailableENodes[node.Index] != null)
        {
            ActiveENodes[node.Index] = node;
            AvailableENodes[node.Index] = null;
            AvailableNodeCount--;
        }
    }

    protected void AddNodes(int num)
    {
        var num2 = 0;
        for (var i = 0; i < MaxENodes; i++)
        {
            if (num2 == num)
            {
                break;
            }
            var node = AvailableENodes[i];
            if (node != null)
            {
                AddActiveNode(node);
                num2++;
                emitter.SetEmitPosition(node);
                var life = 0f;
                if (IsNodeLifeLoop)
                {
                    life = -1f;
                }
                else
                {
                    life = Random.Range(NodeLifeMin, NodeLifeMax);
                }
                var emitRotation = emitter.GetEmitRotation(node);
                node.Init(emitRotation.normalized, OriSpeed, life, Random.Range(OriRotationMin, OriRotationMax), Random.Range(OriScaleXMin, OriScaleXMax), Random.Range(OriScaleYMin, OriScaleYMax), Color1, OriLowerLeftUV, OriUVDimensions);
            }
        }
    }

    public void FixedUpdateCustom()
    {
        var nodes = emitter.GetNodes();
        AddNodes(nodes);
        for (var i = 0; i < MaxENodes; i++)
        {
            var node = ActiveENodes[i];
            if (node != null)
            {
                node.Update();
            }
        }
    }

    public RibbonTrail GetRibbonTrail()
    {
        if (!((ActiveENodes == null) | (ActiveENodes.Length != 1)) && MaxENodes == 1 && RenderType == 1)
        {
            return ActiveENodes[0].Ribbon;
        }
        return null;
    }

    public VertexPool GetVertexPool()
    {
        return Vertexpool;
    }

    protected void Init()
    {
        AvailableENodes = new EffectNode[MaxENodes];
        ActiveENodes = new EffectNode[MaxENodes];
        for (var i = 0; i < MaxENodes; i++)
        {
            var node = new EffectNode(i, ClientTransform, SyncClient, this);
            var afts = InitAffectors(node);
            node.SetAffectorList(afts);
            if (RenderType == 0)
            {
                node.SetType(SpriteWidth, SpriteHeight, (STYPE) SpriteType, (ORIPOINT) OriPoint, SpriteUVStretch, MaxFps);
            }
            else
            {
                node.SetType(RibbonWidth, MaxRibbonElements, RibbonLen, ClientTransform.position, StretchType, MaxFps);
            }
            AvailableENodes[i] = node;
        }
        AvailableNodeCount = MaxENodes;
        emitter = new Emitter(this);
    }

    protected ArrayList InitAffectors(EffectNode node)
    {
        var list = new ArrayList();
        if (UVAffectorEnable)
        {
            var frame = new UVAnimation();
            var mainTex = Vertexpool.GetMaterial().GetTexture("_MainTex");
            if (UVType == 2)
            {
                frame.BuildFromFile(EanPath, EanIndex, UVTime, mainTex);
                OriLowerLeftUV = frame.frames[0];
                OriUVDimensions = frame.UVDimensions[0];
            }
            else if (UVType == 1)
            {
                float num = mainTex.width / Cols;
                float num2 = mainTex.height / Rows;
                var cellSize = new Vector2(num / mainTex.width, num2 / mainTex.height);
                var start = new Vector2(0f, 1f);
                frame.BuildUVAnim(start, cellSize, Cols, Rows, Cols * Rows);
                OriLowerLeftUV = start;
                OriUVDimensions = cellSize;
                OriUVDimensions.y = -OriUVDimensions.y;
            }
            if (frame.frames.Length == 1)
            {
                OriLowerLeftUV = frame.frames[0];
                OriUVDimensions = frame.UVDimensions[0];
            }
            else
            {
                frame.loopCycles = LoopCircles;
                Affector affector = new UVAffector(frame, UVTime, node);
                list.Add(affector);
            }
        }
        if (RotAffectorEnable && RotateType != RSTYPE.NONE)
        {
            Affector affector2;
            if (RotateType == RSTYPE.CURVE)
            {
                affector2 = new RotateAffector(RotateCurve, node);
            }
            else
            {
                affector2 = new RotateAffector(DeltaRot, node);
            }
            list.Add(affector2);
        }
        if (ScaleAffectorEnable && ScaleType != RSTYPE.NONE)
        {
            Affector affector3;
            if (ScaleType == RSTYPE.CURVE)
            {
                affector3 = new ScaleAffector(ScaleXCurve, ScaleYCurve, node);
            }
            else
            {
                affector3 = new ScaleAffector(DeltaScaleX, DeltaScaleY, node);
            }
            list.Add(affector3);
        }
        if (ColorAffectorEnable && ColorAffectType != 0)
        {
            ColorAffector affector4;
            if (ColorAffectType == 2)
            {
                var colorArr = new[] { Color1, Color2, Color3, Color4 };
                affector4 = new ColorAffector(colorArr, ColorGradualTimeLength, ColorGradualType, node);
            }
            else
            {
                var colorArray2 = new[] { Color1, Color2 };
                affector4 = new ColorAffector(colorArray2, ColorGradualTimeLength, ColorGradualType, node);
            }
            list.Add(affector4);
        }
        if (LinearForceAffectorEnable)
        {
            Affector affector5 = new LinearForceAffector(LinearForce.normalized * LinearMagnitude, node);
            list.Add(affector5);
        }
        if (JetAffectorEnable)
        {
            Affector affector6 = new JetAffector(JetMin, JetMax, node);
            list.Add(affector6);
        }
        if (VortexAffectorEnable)
        {
            Affector affector7;
            if (UseVortexCurve)
            {
                affector7 = new VortexAffector(VortexCurve, VortexDirection, node);
            }
            else
            {
                affector7 = new VortexAffector(VortexMag, VortexDirection, node);
            }
            list.Add(affector7);
        }
        if (AttractionAffectorEnable)
        {
            Affector affector8;
            if (UseVortexCurve)
            {
                affector8 = new AttractionForceAffector(AttractionCurve, AttractionPosition, node);
            }
            else
            {
                affector8 = new AttractionForceAffector(AttractMag, AttractionPosition, node);
            }
            list.Add(affector8);
        }
        return list;
    }

    private void OnDrawGizmosSelected()
    {
    }

    public void RemoveActiveNode(EffectNode node)
    {
        if (AvailableNodeCount == MaxENodes)
        {
            Debug.LogError("out index!");
        }
        if (ActiveENodes[node.Index] != null)
        {
            ActiveENodes[node.Index] = null;
            AvailableENodes[node.Index] = node;
            AvailableNodeCount++;
        }
    }

    public void Reset()
    {
        for (var i = 0; i < MaxENodes; i++)
        {
            if (ActiveENodes == null)
            {
                return;
            }
            var node = ActiveENodes[i];
            if (node != null)
            {
                node.Reset();
                RemoveActiveNode(node);
            }
        }
        emitter.Reset();
    }

    public void StartCustom()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
        Init();
        LastClientPos = ClientTransform.position;
    }
}

