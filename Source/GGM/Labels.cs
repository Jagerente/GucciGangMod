using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static GGM.Caching.Colors;

namespace GGM
{
    internal class Labels
    {
        internal static Font Tahoma;
        private static TextMesh bottomRight;
        private static TextMesh center;
        private static TextMesh topCenter;
        private static TextMesh topLeft;
        private static TextMesh topRight;
        private static TextMesh networkStatus;
        private static TextMesh version;

        public static string BottomRight
        {
            get
            {
                if (bottomRight != null)
                {
                    return bottomRight.text;
                }
                bottomRight = CreateLabel("LabelInfoBottomRight", 32, TextAnchor.LowerRight, white, Tahoma, TextAlignment.Right);
                if (bottomRight == null)
                    return "";
                return bottomRight.text;
            }
            set
            {
                if (bottomRight == null)
                {
                    bottomRight = CreateLabel("LabelInfoBottomRight", 32, TextAnchor.LowerRight, white, Tahoma, TextAlignment.Right);
                    if (bottomRight == null)
                    {
                        return;
                    }

                }
                bottomRight.text = value;
            }
        }

        public static string Center
        {
            get
            {
                if (center != null)
                {
                    return center.text;
                }
                center = CreateLabel("LabelInfoCenter", 32, TextAnchor.MiddleCenter, white, Tahoma, TextAlignment.Center);
                if (center == null)
                    return "";
                return center.text;
            }
            set
            {
                if (center == null)
                {
                    center = CreateLabel("LabelInfoCenter", 32, TextAnchor.MiddleCenter, white, Tahoma, TextAlignment.Center);
                    if (center == null)
                        return;
                }
                center.text = value;
            }
        }

        public static string NetworkStatus
        {
            get
            {
                if (networkStatus != null)
                {
                    return networkStatus.text;
                }
                networkStatus = CreateLabel("LabelNetworkStatus", 32, TextAnchor.UpperLeft, white, Tahoma, TextAlignment.Left);
                if (networkStatus == null)
                    return string.Empty;
                return networkStatus.text;
            }
            set
            {
                if (networkStatus == null)
                {
                    networkStatus = CreateLabel("LabelNetworkStatus", 32, TextAnchor.UpperLeft, white, Tahoma, TextAlignment.Left);
                    if (networkStatus == null)
                        return;
                }
                networkStatus.text = value;
            }
        }

        public static string TopCenter
        {
            get
            {
                if (topCenter != null)
                {
                    return topCenter.text;
                }
                topCenter = CreateLabel("LabelInfoTopCenter", 32, TextAnchor.UpperCenter, white, Tahoma, TextAlignment.Center);
                if (topCenter == null)
                    return "";
                return topCenter.text;
            }
            set
            {
                if (topCenter == null)
                {
                    topCenter = CreateLabel("LabelInfoTopCenter", 32, TextAnchor.UpperCenter, white, Tahoma, TextAlignment.Center);
                    if (topCenter == null)
                        return;
                }
                topCenter.text = value;
            }
        }

        public static string TopLeft
        {
            get
            {
                if (topLeft != null)
                {
                    return topLeft.text;
                }
                topLeft = CreateLabel("LabelInfoTopLeft", 28, TextAnchor.UpperLeft, white, Tahoma, TextAlignment.Left);
                if (topLeft == null)
                    return "";
                return topLeft.text;
            }
            set
            {
                if (topLeft == null)
                {
                    topLeft = CreateLabel("LabelInfoTopLeft", 28, TextAnchor.UpperLeft, white, Tahoma, TextAlignment.Left);
                    if (topLeft == null)
                        return;
                }
                topLeft.text = value;
            }
        }

        public static string TopRight
        {
            get
            {
                if (topRight != null)
                {
                    return topRight.text;
                }
                topRight = CreateLabel("LabelInfoTopRight", 28, TextAnchor.UpperRight, white, Tahoma, TextAlignment.Right);
                if (topRight == null)
                    return "";
                return topRight.text;
            }
            set
            {
                if (topRight == null)
                {
                    topRight = CreateLabel("LabelInfoTopRight", 28, TextAnchor.UpperRight, white, Tahoma, TextAlignment.Right);
                    if (topRight == null)
                        return;
                }
                topRight.text = value;
            }
        }

        public static string Version
        {
            get
            {
                if (version != null)
                {
                    return version.text;
                }
                version = CreateLabel("VERSION", 28, TextAnchor.MiddleCenter, white, Tahoma, TextAlignment.Center);
                if (version == null)
                    return "";
                return version.text;
            }
            set
            {
                if (version == null)
                {
                    version = CreateLabel("VERSION", 28, TextAnchor.MiddleCenter, white, Tahoma, TextAlignment.Center);
                    if (version == null)
                        return;
                }
                version.text = value;
            }
        }

        internal static TextMesh CreateLabel(string name, int size, TextAnchor anchor, Color color, Font font, TextAlignment align)
        {
            if (font == null)
                return null;
            GameObject res = Caching.GameObjectCache.Find(name);
            if (res == null)
                return null;
            if (res.GetComponent<UILabel>() == null)
                return null;
            TextMesh text = res.GetComponent<TextMesh>();
            if (text == null)
                text = res.AddComponent<TextMesh>();
            MeshRenderer render = res.GetComponent<MeshRenderer>();
            if (render == null)
                render = res.AddComponent<MeshRenderer>();
            res.transform.localScale = new Vector3(4.9f, 4.9f);
            UILabel label = res.GetComponent<UILabel>();
            render.material = font.material;
            text.font = font;
            text.fontSize = size;
            text.anchor = anchor;
            text.alignment = align;
            text.color = color;
            if (label != null)
            {
                text.text = label.text;
                label.enabled = false;
            }
            text.richText = true;
            return text;
        }

        public static System.Collections.IEnumerator LoadFonts()
        {
            if (Tahoma != null)
                yield break;
            AssetBundleCreateRequest bundle = AssetBundle.CreateFromMemory(System.IO.File.ReadAllBytes(Application.dataPath + "/Resources/Fonts.unity3d"));
            yield return bundle;
            Tahoma = (Font)(bundle.assetBundle.Load("tahoma"));
        }
    }
}
