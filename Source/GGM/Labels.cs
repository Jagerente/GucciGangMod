using GGM.Config;
using UnityEngine;
using static GGM.Caching.ColorCache;
using static GGM.GUI.Styles;

namespace GGM
{
    internal class Labels
    {
        private static TextMesh bottomRight;
        private static TextMesh center;
        private static TextMesh topCenter;
        private static TextMesh topLeft;
        private static TextMesh topRight;
        private static TextMesh networkStatus;
        private static TextMesh version;
        private static TextMesh crosshair;

        private const int Font = 1;

        public static string BottomRight
        {
            get
            {
                if (bottomRight != null)
                {
                    return bottomRight.text;
                }

                bottomRight = CreateLabel("LabelInfoBottomRight", 32, TextAnchor.LowerRight, White, Fonts[Font], TextAlignment.Right);
                return bottomRight == null ? string.Empty : bottomRight.text;
            }
            set
            {
                if (bottomRight == null)
                {
                    bottomRight = CreateLabel("LabelInfoBottomRight", 32, TextAnchor.LowerRight, White, Fonts[Font], TextAlignment.Right);
                    if (bottomRight == null)
                    {
                        return;
                    }
                }

                bottomRight.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                center = CreateLabel("LabelInfoCenter", 32, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                return center == null ? string.Empty : center.text;
            }
            set
            {
                if (center == null)
                {
                    center = CreateLabel("LabelInfoCenter", 32, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                    if (center == null)
                        return;
                }

                center.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                networkStatus = CreateLabel("LabelNetworkStatus", 32, TextAnchor.UpperLeft, White, Fonts[Font], TextAlignment.Left);
                return networkStatus == null ? string.Empty : networkStatus.text;
            }
            set
            {
                if (networkStatus == null)
                {
                    if (Fonts == null)
                        return;
                    networkStatus = CreateLabel("LabelNetworkStatus", 32, TextAnchor.UpperLeft, White, Fonts[Font], TextAlignment.Left);
                    if (networkStatus == null)
                        return;
                }

                networkStatus.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                topCenter = CreateLabel("LabelInfoTopCenter", 32, TextAnchor.UpperCenter, White, Fonts[Font], TextAlignment.Center);
                return topCenter == null ? string.Empty : topCenter.text;
            }
            set
            {
                if (topCenter == null)
                {
                    topCenter = CreateLabel("LabelInfoTopCenter", 32, TextAnchor.UpperCenter, White, Fonts[Font], TextAlignment.Center);
                    if (topCenter == null)
                        return;
                }

                topCenter.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                topLeft = CreateLabel("LabelInfoTopLeft", 28, TextAnchor.UpperLeft, White, Fonts[Font], TextAlignment.Left);
                return topLeft == null ? string.Empty : topLeft.text;
            }
            set
            {
                if (topLeft == null)
                {
                    topLeft = CreateLabel("LabelInfoTopLeft", 28, TextAnchor.UpperLeft, White, Fonts[Font], TextAlignment.Left);
                    if (topLeft == null)
                        return;
                }

                topLeft.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                topRight = CreateLabel("LabelInfoTopRight", 28, TextAnchor.UpperRight, White, Fonts[Font], TextAlignment.Right);
                return topRight == null ? string.Empty : topRight.text;
            }
            set
            {
                if (topRight == null)
                {
                    topRight = CreateLabel("LabelInfoTopRight", 28, TextAnchor.UpperRight, White, Fonts[Font], TextAlignment.Right);
                    if (topRight == null)
                        return;
                }

                topRight.text = Settings.UserInterfaceSetting ? string.Empty : value;
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

                version = CreateLabel("VERSION", 28, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                return version == null ? string.Empty : version.text;
            }
            set
            {
                if (version == null)
                {
                    version = CreateLabel("VERSION", 28, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                    if (version == null)
                        return;
                }

                version.text = value;
            }
        }

        public static string Crosshair
        {
            get
            {
                if (crosshair != null)
                {
                    return crosshair.text;
                }

                crosshair = CreateLabel("LabelDistance", 26, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                return crosshair == null ? string.Empty : crosshair.text;
            }
            set
            {
                if (crosshair == null)
                {
                    crosshair = CreateLabel("LabelDistance", 26, TextAnchor.MiddleCenter, White, Fonts[Font], TextAlignment.Center);
                    if (crosshair == null)
                        return;
                }

                crosshair.text = "\n\n\n" + value;
            }
        }

        internal static TextMesh CreateLabel(string name, int size, TextAnchor anchor, Color color, Font font, TextAlignment align)
        {
            if (font == null)
                return null;
            var res = Caching.GameObjectCache.Find(name);
            if (res == null)
                return null;
            if (res.GetComponent<UILabel>() == null)
                return null;
            var text = res.GetComponent<TextMesh>();
            if (text == null)
                text = res.AddComponent<TextMesh>();
            var render = res.GetComponent<MeshRenderer>();
            if (render == null)
                render = res.AddComponent<MeshRenderer>();
            res.transform.localScale = new Vector3(4.9f, 4.9f);
            var label = res.GetComponent<UILabel>();
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
    }
}