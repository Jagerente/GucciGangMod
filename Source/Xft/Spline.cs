namespace Xft
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Spline
    {
        public int Granularity = 20;
        private List<SplineControlPoint> mControlPoints = new List<SplineControlPoint>();
        private List<SplineControlPoint> mSegments = new List<SplineControlPoint>();

        public SplineControlPoint AddControlPoint(Vector3 pos, Vector3 up)
        {
            var item = new SplineControlPoint();
            item.Init(this);
            item.Position = pos;
            item.Normal = up;
            mControlPoints.Add(item);
            item.ControlPointIndex = mControlPoints.Count - 1;
            return item;
        }

        public static Vector3 CatmulRom(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
        {
            var num = -0.5;
            var num2 = 1.5;
            var num3 = -1.5;
            var num4 = 0.5;
            var num5 = -2.5;
            var num6 = 2.0;
            var num7 = -0.5;
            var num8 = -0.5;
            var num9 = 0.5;
            var num10 = num * T0.x + num2 * P0.x + num3 * P1.x + num4 * T1.x;
            var num11 = T0.x + num5 * P0.x + num6 * P1.x + num7 * T1.x;
            var num12 = num8 * T0.x + num9 * P1.x;
            double x = P0.x;
            var num14 = num * T0.y + num2 * P0.y + num3 * P1.y + num4 * T1.y;
            var num15 = T0.y + num5 * P0.y + num6 * P1.y + num7 * T1.y;
            var num16 = num8 * T0.y + num9 * P1.y;
            double y = P0.y;
            var num18 = num * T0.z + num2 * P0.z + num3 * P1.z + num4 * T1.z;
            var num19 = T0.z + num5 * P0.z + num6 * P1.z + num7 * T1.z;
            var num20 = num8 * T0.z + num9 * P1.z;
            double z = P0.z;
            var num22 = (float) (((num10 * f + num11) * f + num12) * f + x);
            var num23 = (float) (((num14 * f + num15) * f + num16) * f + y);
            return new Vector3(num22, num23, (float) (((num18 * f + num19) * f + num20) * f + z));
        }

        public void Clear()
        {
            mControlPoints.Clear();
        }

        public Vector3 InterpolateByLen(float tl)
        {
            float num;
            return LenToSegment(tl, out num).Interpolate(num);
        }

        public Vector3 InterpolateNormalByLen(float tl)
        {
            float num;
            return LenToSegment(tl, out num).InterpolateNormal(num);
        }

        public SplineControlPoint LenToSegment(float t, out float localF)
        {
            SplineControlPoint point = null;
            t = Mathf.Clamp01(t);
            var num = t * mSegments[mSegments.Count - 1].Dist;
            var num2 = 0;
            num2 = 0;
            while (num2 < mSegments.Count)
            {
                if (mSegments[num2].Dist >= num)
                {
                    point = mSegments[num2];
                    break;
                }
                num2++;
            }
            if (num2 == 0)
            {
                localF = 0f;
                return point;
            }
            var num3 = 0f;
            var num4 = point.SegmentIndex - 1;
            var point2 = mSegments[num4];
            num3 = point.Dist - point2.Dist;
            localF = (num - point2.Dist) / num3;
            return point2;
        }

        public SplineControlPoint NextControlPoint(SplineControlPoint controlpoint)
        {
            if (mControlPoints.Count == 0)
            {
                return null;
            }
            var num = controlpoint.ControlPointIndex + 1;
            if (num >= mControlPoints.Count)
            {
                return null;
            }
            return mControlPoints[num];
        }

        public Vector3 NextNormal(SplineControlPoint controlpoint)
        {
            var point = NextControlPoint(controlpoint);
            if (point != null)
            {
                return point.Normal;
            }
            return controlpoint.Normal;
        }

        public Vector3 NextPosition(SplineControlPoint controlpoint)
        {
            var point = NextControlPoint(controlpoint);
            if (point != null)
            {
                return point.Position;
            }
            return controlpoint.Position;
        }

        public SplineControlPoint PreviousControlPoint(SplineControlPoint controlpoint)
        {
            if (mControlPoints.Count == 0)
            {
                return null;
            }
            var num = controlpoint.ControlPointIndex - 1;
            if (num < 0)
            {
                return null;
            }
            return mControlPoints[num];
        }

        public Vector3 PreviousNormal(SplineControlPoint controlpoint)
        {
            var point = PreviousControlPoint(controlpoint);
            if (point != null)
            {
                return point.Normal;
            }
            return controlpoint.Normal;
        }

        public Vector3 PreviousPosition(SplineControlPoint controlpoint)
        {
            var point = PreviousControlPoint(controlpoint);
            if (point != null)
            {
                return point.Position;
            }
            return controlpoint.Position;
        }

        private void RefreshDistance()
        {
            if (mSegments.Count >= 1)
            {
                mSegments[0].Dist = 0f;
                for (var i = 1; i < mSegments.Count; i++)
                {
                    var vector = mSegments[i].Position - mSegments[i - 1].Position;
                    var magnitude = vector.magnitude;
                    mSegments[i].Dist = mSegments[i - 1].Dist + magnitude;
                }
            }
        }

        public void RefreshSpline()
        {
            mSegments.Clear();
            for (var i = 0; i < mControlPoints.Count; i++)
            {
                if (mControlPoints[i].IsValid)
                {
                    mSegments.Add(mControlPoints[i]);
                    mControlPoints[i].SegmentIndex = mSegments.Count - 1;
                }
            }
            RefreshDistance();
        }

        public List<SplineControlPoint> ControlPoints
        {
            get
            {
                return mControlPoints;
            }
        }

        public SplineControlPoint this[int index]
        {
            get
            {
                if (index > -1 && index < mSegments.Count)
                {
                    return mSegments[index];
                }
                return null;
            }
        }

        public List<SplineControlPoint> Segments
        {
            get
            {
                return mSegments;
            }
        }
    }
}

