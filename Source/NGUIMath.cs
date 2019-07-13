using UnityEngine;

public static class NGUIMath
{
    public static Vector3 ApplyHalfPixelOffset(Vector3 pos)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.XBOX360:
                pos.x -= 0.5f;
                pos.y += 0.5f;
                break;
        }
        return pos;
    }

    public static Vector3 ApplyHalfPixelOffset(Vector3 pos, Vector3 scale)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.XBOX360:
                if (Mathf.RoundToInt(scale.x) == Mathf.RoundToInt(scale.x * 0.5f) * 2)
                {
                    pos.x -= 0.5f;
                }
                if (Mathf.RoundToInt(scale.y) == Mathf.RoundToInt(scale.y * 0.5f) * 2)
                {
                    pos.y += 0.5f;
                }
                break;
        }
        return pos;
    }

    public static Bounds CalculateAbsoluteWidgetBounds(Transform trans)
    {
        var componentsInChildren = trans.GetComponentsInChildren<UIWidget>();
        if (componentsInChildren.Length == 0)
        {
            return new Bounds(trans.position, Vector3.zero);
        }
        var rhs = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        var index = 0;
        var length = componentsInChildren.Length;
        while (index < length)
        {
            var widget = componentsInChildren[index];
            var relativeSize = widget.relativeSize;
            var pivotOffset = widget.pivotOffset;
            var num3 = (pivotOffset.x + 0.5f) * relativeSize.x;
            var num4 = (pivotOffset.y - 0.5f) * relativeSize.y;
            relativeSize = relativeSize * 0.5f;
            var cachedTransform = widget.cachedTransform;
            var lhs = cachedTransform.TransformPoint(new Vector3(num3 - relativeSize.x, num4 - relativeSize.y, 0f));
            vector2 = Vector3.Max(lhs, vector2);
            rhs = Vector3.Min(lhs, rhs);
            lhs = cachedTransform.TransformPoint(new Vector3(num3 - relativeSize.x, num4 + relativeSize.y, 0f));
            vector2 = Vector3.Max(lhs, vector2);
            rhs = Vector3.Min(lhs, rhs);
            lhs = cachedTransform.TransformPoint(new Vector3(num3 + relativeSize.x, num4 - relativeSize.y, 0f));
            vector2 = Vector3.Max(lhs, vector2);
            rhs = Vector3.Min(lhs, rhs);
            lhs = cachedTransform.TransformPoint(new Vector3(num3 + relativeSize.x, num4 + relativeSize.y, 0f));
            vector2 = Vector3.Max(lhs, vector2);
            rhs = Vector3.Min(lhs, rhs);
            index++;
        }
        var bounds = new Bounds(rhs, Vector3.zero);
        bounds.Encapsulate(vector2);
        return bounds;
    }

    public static Bounds CalculateRelativeInnerBounds(Transform root, UISprite sprite)
    {
        if (sprite.type != UISprite.Type.Sliced)
        {
            return CalculateRelativeWidgetBounds(root, sprite.cachedTransform);
        }
        var worldToLocalMatrix = root.worldToLocalMatrix;
        var relativeSize = sprite.relativeSize;
        var pivotOffset = sprite.pivotOffset;
        var cachedTransform = sprite.cachedTransform;
        var num = (pivotOffset.x + 0.5f) * relativeSize.x;
        var num2 = (pivotOffset.y - 0.5f) * relativeSize.y;
        relativeSize = relativeSize * 0.5f;
        var x = cachedTransform.localScale.x;
        var y = cachedTransform.localScale.y;
        var border = sprite.border;
        if (x != 0f)
        {
            border.x /= x;
            border.z /= x;
        }
        if (y != 0f)
        {
            border.y /= y;
            border.w /= y;
        }
        var num5 = num - relativeSize.x + border.x;
        var num6 = num + relativeSize.x - border.z;
        var num7 = num2 - relativeSize.y + border.y;
        var num8 = num2 + relativeSize.y - border.w;
        var position = new Vector3(num5, num7, 0f);
        position = cachedTransform.TransformPoint(position);
        position = worldToLocalMatrix.MultiplyPoint3x4(position);
        var bounds = new Bounds(position, Vector3.zero);
        position = new Vector3(num5, num8, 0f);
        position = cachedTransform.TransformPoint(position);
        position = worldToLocalMatrix.MultiplyPoint3x4(position);
        bounds.Encapsulate(position);
        position = new Vector3(num6, num8, 0f);
        position = cachedTransform.TransformPoint(position);
        position = worldToLocalMatrix.MultiplyPoint3x4(position);
        bounds.Encapsulate(position);
        position = new Vector3(num6, num7, 0f);
        position = cachedTransform.TransformPoint(position);
        position = worldToLocalMatrix.MultiplyPoint3x4(position);
        bounds.Encapsulate(position);
        return bounds;
    }

    public static Bounds CalculateRelativeWidgetBounds(Transform trans)
    {
        return CalculateRelativeWidgetBounds(trans, trans);
    }

    public static Bounds CalculateRelativeWidgetBounds(Transform root, Transform child)
    {
        var componentsInChildren = child.GetComponentsInChildren<UIWidget>();
        if (componentsInChildren.Length == 0)
        {
            return new Bounds(Vector3.zero, Vector3.zero);
        }
        var rhs = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        var worldToLocalMatrix = root.worldToLocalMatrix;
        var index = 0;
        var length = componentsInChildren.Length;
        while (index < length)
        {
            var widget = componentsInChildren[index];
            var relativeSize = widget.relativeSize;
            var pivotOffset = widget.pivotOffset;
            var cachedTransform = widget.cachedTransform;
            var num3 = (pivotOffset.x + 0.5f) * relativeSize.x;
            var num4 = (pivotOffset.y - 0.5f) * relativeSize.y;
            relativeSize = relativeSize * 0.5f;
            var position = new Vector3(num3 - relativeSize.x, num4 - relativeSize.y, 0f);
            position = cachedTransform.TransformPoint(position);
            position = worldToLocalMatrix.MultiplyPoint3x4(position);
            vector2 = Vector3.Max(position, vector2);
            rhs = Vector3.Min(position, rhs);
            position = new Vector3(num3 - relativeSize.x, num4 + relativeSize.y, 0f);
            position = cachedTransform.TransformPoint(position);
            position = worldToLocalMatrix.MultiplyPoint3x4(position);
            vector2 = Vector3.Max(position, vector2);
            rhs = Vector3.Min(position, rhs);
            position = new Vector3(num3 + relativeSize.x, num4 - relativeSize.y, 0f);
            position = cachedTransform.TransformPoint(position);
            position = worldToLocalMatrix.MultiplyPoint3x4(position);
            vector2 = Vector3.Max(position, vector2);
            rhs = Vector3.Min(position, rhs);
            position = new Vector3(num3 + relativeSize.x, num4 + relativeSize.y, 0f);
            position = cachedTransform.TransformPoint(position);
            position = worldToLocalMatrix.MultiplyPoint3x4(position);
            vector2 = Vector3.Max(position, vector2);
            rhs = Vector3.Min(position, rhs);
            index++;
        }
        var bounds = new Bounds(rhs, Vector3.zero);
        bounds.Encapsulate(vector2);
        return bounds;
    }

    public static Vector3[] CalculateWidgetCorners(UIWidget w)
    {
        var relativeSize = w.relativeSize;
        var pivotOffset = w.pivotOffset;
        var relativePadding = w.relativePadding;
        var x = pivotOffset.x * relativeSize.x - relativePadding.x;
        var y = pivotOffset.y * relativeSize.y + relativePadding.y;
        var num3 = x + relativeSize.x + relativePadding.x + relativePadding.z;
        var num4 = y - relativeSize.y - relativePadding.y - relativePadding.w;
        var cachedTransform = w.cachedTransform;
        return new[] { cachedTransform.TransformPoint(x, y, 0f), cachedTransform.TransformPoint(x, num4, 0f), cachedTransform.TransformPoint(num3, num4, 0f), cachedTransform.TransformPoint(num3, y, 0f) };
    }

    public static int ClampIndex(int val, int max)
    {
        return val >= 0 ? val >= max ? max - 1 : val : 0;
    }

    public static int ColorToInt(Color c)
    {
        var num = 0;
        num |= Mathf.RoundToInt(c.r * 255f) << 24;
        num |= Mathf.RoundToInt(c.g * 255f) << 16;
        num |= Mathf.RoundToInt(c.b * 255f) << 8;
        return num | Mathf.RoundToInt(c.a * 255f);
    }

    public static Vector2 ConstrainRect(Vector2 minRect, Vector2 maxRect, Vector2 minArea, Vector2 maxArea)
    {
        var zero = Vector2.zero;
        var num = maxRect.x - minRect.x;
        var num2 = maxRect.y - minRect.y;
        var num3 = maxArea.x - minArea.x;
        var num4 = maxArea.y - minArea.y;
        if (num > num3)
        {
            var num5 = num - num3;
            minArea.x -= num5;
            maxArea.x += num5;
        }
        if (num2 > num4)
        {
            var num6 = num2 - num4;
            minArea.y -= num6;
            maxArea.y += num6;
        }
        if (minRect.x < minArea.x)
        {
            zero.x += minArea.x - minRect.x;
        }
        if (maxRect.x > maxArea.x)
        {
            zero.x -= maxRect.x - maxArea.x;
        }
        if (minRect.y < minArea.y)
        {
            zero.y += minArea.y - minRect.y;
        }
        if (maxRect.y > maxArea.y)
        {
            zero.y -= maxRect.y - maxArea.y;
        }
        return zero;
    }

    public static Rect ConvertToPixels(Rect rect, int width, int height, bool round)
    {
        var rect2 = rect;
        if (round)
        {
            rect2.xMin = Mathf.RoundToInt(rect.xMin * width);
            rect2.xMax = Mathf.RoundToInt(rect.xMax * width);
            rect2.yMin = Mathf.RoundToInt((1f - rect.yMax) * height);
            rect2.yMax = Mathf.RoundToInt((1f - rect.yMin) * height);
            return rect2;
        }
        rect2.xMin = rect.xMin * width;
        rect2.xMax = rect.xMax * width;
        rect2.yMin = (1f - rect.yMax) * height;
        rect2.yMax = (1f - rect.yMin) * height;
        return rect2;
    }

    public static Rect ConvertToTexCoords(Rect rect, int width, int height)
    {
        var rect2 = rect;
        if (width != 0f && height != 0f)
        {
            rect2.xMin = rect.xMin / width;
            rect2.xMax = rect.xMax / width;
            rect2.yMin = 1f - rect.yMax / height;
            rect2.yMax = 1f - rect.yMin / height;
        }
        return rect2;
    }

    public static string DecimalToHex(int num)
    {
        num &= 16777215;
        return num.ToString("X6");
    }

    public static char DecimalToHexChar(int num)
    {
        if (num > 15)
        {
            return 'F';
        }
        if (num < 10)
        {
            return (char)(48 + num);
        }
        return (char)(65 + num - 10);
    }

    private static float DistancePointToLineSegment(Vector2 point, Vector2 a, Vector2 b)
    {
        var vector = b - a;
        var sqrMagnitude = vector.sqrMagnitude;
        if (sqrMagnitude == 0f)
        {
            var vector2 = point - a;
            return vector2.magnitude;
        }
        var num2 = Vector2.Dot(point - a, b - a) / sqrMagnitude;
        if (num2 < 0f)
        {
            var vector3 = point - a;
            return vector3.magnitude;
        }
        if (num2 > 1f)
        {
            var vector4 = point - b;
            return vector4.magnitude;
        }
        var vector5 = a + num2 * (b - a);
        var vector6 = point - vector5;
        return vector6.magnitude;
    }

    public static float DistanceToRectangle(Vector2[] screenPoints, Vector2 mousePos)
    {
        var flag = false;
        var val = 4;
        for (var i = 0; i < 5; i++)
        {
            Vector3 vector = screenPoints[RepeatIndex(i, 4)];
            Vector3 vector2 = screenPoints[RepeatIndex(val, 4)];
            if (vector.y > mousePos.y != vector2.y > mousePos.y && mousePos.x < (vector2.x - vector.x) * (mousePos.y - vector.y) / (vector2.y - vector.y) + vector.x)
            {
                flag = !flag;
            }
            val = i;
        }
        if (!flag)
        {
            var num = -1f;
            for (var j = 0; j < 4; j++)
            {
                Vector3 v = screenPoints[j];
                Vector3 v2 = screenPoints[RepeatIndex(j + 1, 4)];
                var num2 = DistancePointToLineSegment(mousePos, v, v2);
                if (num2 < num || num < 0f)
                {
                    num = num2;
                }
            }
            return num;
        }
        return 0f;
    }

    public static float DistanceToRectangle(Vector3[] worldPoints, Vector2 mousePos, Camera cam)
    {
        var screenPoints = new Vector2[4];
        for (var i = 0; i < 4; i++)
        {
            screenPoints[i] = cam.WorldToScreenPoint(worldPoints[i]);
        }
        return DistanceToRectangle(screenPoints, mousePos);
    }

    public static Color HexToColor(uint val)
    {
        return IntToColor((int)val);
    }

    public static int HexToDecimal(char ch)
    {
        var ch2 = ch;
        switch (ch2)
        {
            case '0':
                return 0;

            case '1':
                return 1;

            case '2':
                return 2;

            case '3':
                return 3;

            case '4':
                return 4;

            case '5':
                return 5;

            case '6':
                return 6;

            case '7':
                return 7;

            case '8':
                return 8;

            case '9':
                return 9;

            case 'A':
                break;

            case 'B':
                goto Label_00A5;

            case 'C':
                goto Label_00A8;

            case 'D':
                goto Label_00AB;

            case 'E':
                goto Label_00AE;

            case 'F':
                goto Label_00B1;

            default:
                switch (ch2)
                {
                    case 'a':
                        break;

                    case 'b':
                        goto Label_00A5;

                    case 'c':
                        goto Label_00A8;

                    case 'd':
                        goto Label_00AB;

                    case 'e':
                        goto Label_00AE;

                    case 'f':
                        goto Label_00B1;

                    default:
                        return 15;
                }
                break;
        }
        return 10;
        Label_00A5:
        return 11;
        Label_00A8:
        return 12;
        Label_00AB:
        return 13;
        Label_00AE:
        return 14;
        Label_00B1:
        return 15;
    }

    public static string IntToBinary(int val, int bits)
    {
        var str = string.Empty;
        var num = bits;
        while (num > 0)
        {
            switch (num)
            {
                case 8:
                case 16:
                case 24:
                    str = str + " ";
                    break;
            }
            str = str + ((val & (1 << --num)) == 0 ? '0' : '1');
        }
        return str;
    }

    public static Color IntToColor(int val)
    {
        var num = 0.003921569f;
        var black = Color.black;
        black.r = num * ((val >> 24) & 255);
        black.g = num * ((val >> 16) & 255);
        black.b = num * ((val >> 8) & 255);
        black.a = num * (val & 255);
        return black;
    }

    public static float Lerp(float from, float to, float factor)
    {
        return @from * (1f - factor) + to * factor;
    }

    public static Rect MakePixelPerfect(Rect rect)
    {
        rect.xMin = Mathf.RoundToInt(rect.xMin);
        rect.yMin = Mathf.RoundToInt(rect.yMin);
        rect.xMax = Mathf.RoundToInt(rect.xMax);
        rect.yMax = Mathf.RoundToInt(rect.yMax);
        return rect;
    }

    public static Rect MakePixelPerfect(Rect rect, int width, int height)
    {
        rect = ConvertToPixels(rect, width, height, true);
        rect.xMin = Mathf.RoundToInt(rect.xMin);
        rect.yMin = Mathf.RoundToInt(rect.yMin);
        rect.xMax = Mathf.RoundToInt(rect.xMax);
        rect.yMax = Mathf.RoundToInt(rect.yMax);
        return ConvertToTexCoords(rect, width, height);
    }

    public static int RepeatIndex(int val, int max)
    {
        if (max < 1)
        {
            return 0;
        }
        while (val < 0)
        {
            val += max;
        }
        while (val >= max)
        {
            val -= max;
        }
        return val;
    }

    public static float RotateTowards(float from, float to, float maxAngle)
    {
        var f = WrapAngle(to - from);
        if (Mathf.Abs(f) > maxAngle)
        {
            f = maxAngle * Mathf.Sign(f);
        }
        return @from + f;
    }

    public static Vector2 SpringDampen(ref Vector2 velocity, float strength, float deltaTime)
    {
        if (deltaTime > 1f)
        {
            deltaTime = 1f;
        }
        var num = 1f - strength * 0.001f;
        var num2 = Mathf.RoundToInt(deltaTime * 1000f);
        var zero = Vector2.zero;
        for (var i = 0; i < num2; i++)
        {
            zero += velocity * 0.06f;
            velocity = velocity * num;
        }
        return zero;
    }

    public static Vector3 SpringDampen(ref Vector3 velocity, float strength, float deltaTime)
    {
        if (deltaTime > 1f)
        {
            deltaTime = 1f;
        }
        var num = 1f - strength * 0.001f;
        var num2 = Mathf.RoundToInt(deltaTime * 1000f);
        var zero = Vector3.zero;
        for (var i = 0; i < num2; i++)
        {
            zero += velocity * 0.06f;
            velocity = velocity * num;
        }
        return zero;
    }

    public static float SpringLerp(float strength, float deltaTime)
    {
        if (deltaTime > 1f)
        {
            deltaTime = 1f;
        }
        var num = Mathf.RoundToInt(deltaTime * 1000f);
        deltaTime = 0.001f * strength;
        var from = 0f;
        for (var i = 0; i < num; i++)
        {
            from = Mathf.Lerp(from, 1f, deltaTime);
        }
        return from;
    }

    public static float SpringLerp(float from, float to, float strength, float deltaTime)
    {
        if (deltaTime > 1f)
        {
            deltaTime = 1f;
        }
        var num = Mathf.RoundToInt(deltaTime * 1000f);
        deltaTime = 0.001f * strength;
        for (var i = 0; i < num; i++)
        {
            from = Mathf.Lerp(from, to, deltaTime);
        }
        return from;
    }

    public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
    {
        return Quaternion.Slerp(from, to, SpringLerp(strength, deltaTime));
    }

    public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
    {
        return Vector2.Lerp(from, to, SpringLerp(strength, deltaTime));
    }

    public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
    {
        return Vector3.Lerp(from, to, SpringLerp(strength, deltaTime));
    }

    public static float Wrap01(float val)
    {
        return val - Mathf.FloorToInt(val);
    }

    public static float WrapAngle(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }
        while (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }
}