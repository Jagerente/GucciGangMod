using UnityEngine;

namespace GGM
{
    class Style : MonoBehaviour
    {
        static string path = "Gucci/";
        static bool styleinited = false;
        static Texture2D Box;
        static Texture2D Window;
        static Texture2D ButtonN;
        static Texture2D ButtonA;
        static Texture2D ButtonH;
        static Texture2D ButtonON;
        static Texture2D ButtonOA;
        static Texture2D ButtonOH;
        static Texture2D TextN;
        static Texture2D TextH;
        static Texture2D TextF;
        static Texture2D TextON;
        static Texture2D ThumbN;
        static Texture2D ThumbA;
        static Texture2D ThumbH;
        static Texture2D ToggleN;
        static Texture2D ToggleA;
        static Texture2D ToggleH;
        static Texture2D ToggleON;
        static Texture2D ToggleOA;
        static Texture2D ToggleOH;
        static Texture2D Slider;
        static Texture2D VerticalScrollBar;
        static Texture2D HorizontalScrollBar;
        static Texture2D ScrollBarThumbN;
        static Texture2D ScrollBarThumbA;
        static Texture2D ScrollBarThumbH;
        public static Texture2D Logo;

        void Start()
        {
            var LOGO = new WWW("file:///" + Application.dataPath + $"/Styles/{path}GGP_Logo.png");
            if (Logo == null)
            {
                Logo = LOGO.texture;
                Logo.Apply();
            }
            var BOX = new WWW("file:///" + Application.dataPath + $"/Styles/{path}Box.png");
            if (Box == null)
            {
                Box = BOX.texture;
                Box.Apply();
            }
            var WINDOW = new WWW("file:///" + Application.dataPath + $"/Styles/{path}Window.png");
            if (Window == null)
            {
                Window = WINDOW.texture;
                Window.Apply();
            }
            var BUTTONN = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonN.png");
            if (ButtonN == null)
            {
                ButtonN = BUTTONN.texture;
                ButtonN.Apply();
            }
            var BUTTONA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonA.png");
            if (ButtonA == null)
            {
                ButtonA = BUTTONA.texture;
                ButtonA.Apply();
            }
            var BUTTONH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonH.png");
            if (ButtonH == null)
            {
                ButtonH = BUTTONH.texture;
                ButtonH.Apply();
            }
            var BUTTONON = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonON.png");
            if (ButtonON == null)
            {
                ButtonON = BUTTONON.texture;
                ButtonON.Apply();
            }
            var BUTTONOH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonOH.png");
            if (ButtonOH == null)
            {
                ButtonOH = BUTTONOH.texture;
                ButtonOH.Apply();
            }
            var BUTTONOA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ButtonOA.png");
            if (ButtonOA == null)
            {
                ButtonOA = BUTTONOA.texture;
                ButtonOA.Apply();
            }
            var TEXTN = new WWW("file:///" + Application.dataPath + $"/Styles/{path}TextN.png");
            if (TextN == null)
            {
                TextN = TEXTN.texture;
                TextN.Apply();
            }
            var TEXTH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}TextH.png");
            if (TextH == null)
            {
                TextH = TEXTH.texture;
                TextH.Apply();
            }
            var TEXTF = new WWW("file:///" + Application.dataPath + $"/Styles/{path}TextF.png");
            if (TextF == null)
            {
                TextF = TEXTF.texture;
                TextF.Apply();
            }
            var TEXTON = new WWW("file:///" + Application.dataPath + $"/Styles/{path}TextON.png");
            if (TextON == null)
            {
                TextON = TEXTON.texture;
                TextON.Apply();
            }
            var THUMBN = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ThumbN.png");
            if (ThumbN == null)
            {
                ThumbN = THUMBN.texture;
                ThumbN.Apply();
            }
            var THUMBA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ThumbA.png");
            if (ThumbA == null)
            {
                ThumbA = THUMBA.texture;
                ThumbA.Apply();
            }
            var THUMBH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ThumbH.png");
            if (ThumbH == null)
            {
                ThumbH = THUMBH.texture;
                ThumbH.Apply();
            }
            var TOGGLEN = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleN.png");
            if (ToggleN == null)
            {
                ToggleN = TOGGLEN.texture;
                ToggleN.Apply();
            }
            var TOGGLEA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleA.png");
            if (ToggleA == null)
            {
                ToggleA = TOGGLEA.texture;
                ToggleA.Apply();
            }
            var TOGGLEH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleH.png");
            if (ToggleH == null)
            {
                ToggleH = TOGGLEH.texture;
                ToggleH.Apply();
            }
            var TOGGLEON = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleON.png");
            if (ToggleON == null)
            {
                ToggleON = TOGGLEON.texture;
                ToggleON.Apply();
            }
            var TOGGLEOA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleOA.png");
            if (ToggleOA == null)
            {
                ToggleOA = TOGGLEOA.texture;
                ToggleOA.Apply();
            }
            var TOGGLEOH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ToggleOH.png");
            if (ToggleOH == null)
            {
                ToggleOH = TOGGLEOH.texture;
                ToggleOH.Apply();
            }
            var VERTICALSCROLLBAR = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ScrollBarH.png");
            if (VerticalScrollBar == null)
            {
                VerticalScrollBar = VERTICALSCROLLBAR.texture;
                VerticalScrollBar.Apply();
            }
            var HORIZONTALSCROLLBAR = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ScrollBarV.png");
            if (HorizontalScrollBar == null)
            {
                HorizontalScrollBar = HORIZONTALSCROLLBAR.texture;
                HorizontalScrollBar.Apply();
            }
            var SBTN = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ScrollBarThumbN.png");
            if (ScrollBarThumbN == null)
            {
                ScrollBarThumbN = SBTN.texture;
                ScrollBarThumbN.Apply();
            }
            var SBTA = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ScrollBarThumbA.png");
            if (ScrollBarThumbA == null)
            {
                ScrollBarThumbA = SBTA.texture;
                ScrollBarThumbA.Apply();
            }
            var SBTH = new WWW("file:///" + Application.dataPath + $"/Styles/{path}ScrollBarThumbH.png");
            if (ScrollBarThumbH == null)
            {
                ScrollBarThumbH = SBTH.texture;
                ScrollBarThumbH.Apply();
            }
            var SLIDER = new WWW("file:///" + Application.dataPath + $"/Styles/{path}Slider.png");
            if (Slider == null)
            {
                Slider = SLIDER.texture;
                Slider.Apply();
            }
        }

        public static void Init()
        {
            if (!styleinited)
            {
                styleinited = true;
                //GUI.skin.label.normal.textColor = Gucci_GUI.RGBColor_1;
                GUI.skin.button.normal.textColor = Color.white;
                GUI.skin.button.active.textColor = Color.white;
                GUI.skin.button.hover.textColor = Color.white;
                GUI.skin.button.onNormal.textColor = Color.white;
                GUI.skin.button.onActive.textColor = Color.white;
                GUI.skin.button.onHover.textColor = Color.white;
                GUI.skin.toggle.normal.textColor = Color.white;
                GUI.skin.toggle.active.textColor = Color.white;
                GUI.skin.toggle.hover.textColor = Color.white;
                GUI.skin.toggle.onNormal.textColor = Color.white;
                GUI.skin.toggle.onActive.textColor = Color.white;
                GUI.skin.toggle.onHover.textColor = Color.white;
                GUI.skin.box.normal.textColor = Color.white;


                GUI.skin.button.normal.background = ButtonN;
                GUI.skin.button.active.background = ButtonA;
                GUI.skin.button.hover.background = ButtonH;
                GUI.skin.button.onNormal.background = ButtonON;
                GUI.skin.button.onActive.background = ButtonOA;
                GUI.skin.button.onHover.background = ButtonOH;

                GUI.skin.textField.normal.background = TextN;
                GUI.skin.textField.hover.background = TextH;
                GUI.skin.textField.focused.background = TextF;
                GUI.skin.textField.onNormal.background = TextON;

                GUI.skin.textArea.normal.background = TextN;
                GUI.skin.textArea.hover.background = TextH;
                GUI.skin.textArea.focused.background = TextF;
                GUI.skin.textArea.onNormal.background = TextON;
                GUI.skin.textArea.focused.textColor = Color.white;

                GUI.skin.verticalSlider.normal.background = Slider;
                GUI.skin.verticalSliderThumb.normal.background = ThumbN;
                GUI.skin.verticalSliderThumb.active.background = ThumbA;
                GUI.skin.verticalSliderThumb.hover.background = ThumbH;

                GUI.skin.horizontalSlider.normal.background = Slider;

                GUI.skin.horizontalSliderThumb.normal.background = ThumbN;
                GUI.skin.horizontalSliderThumb.active.background = ThumbA;
                GUI.skin.horizontalSliderThumb.hover.background = ThumbH;

                GUI.skin.horizontalScrollbar.normal.background = HorizontalScrollBar;
                GUI.skin.verticalScrollbar.normal.background = VerticalScrollBar;

                //GUI.skin.verticalScrollbarThumb.normal.background = ScrollBarThumbN;
                //GUI.skin.verticalScrollbarThumb.active.background = ScrollBarThumbA;
                //GUI.skin.verticalScrollbarThumb.hover.background = ScrollBarThumbH;
                //GUI.skin.horizontalScrollbarThumb.normal.background = ScrollBarThumbN;
                //GUI.skin.horizontalScrollbarThumb.active.background = ScrollBarThumbA;
                //GUI.skin.horizontalScrollbarThumb.hover.background = ScrollBarThumbH;

                GUI.skin.verticalScrollbarThumb.normal.background = HorizontalScrollBar;
                GUI.skin.verticalScrollbarThumb.active.background = HorizontalScrollBar;
                GUI.skin.verticalScrollbarThumb.hover.background = HorizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.normal.background = HorizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.active.background = HorizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.hover.background = HorizontalScrollBar;

                //GUI.skin.horizontalScrollbarLeftButton.normal.background = Slider;
                //GUI.skin.horizontalScrollbarLeftButton.active.background = Slider;
                //GUI.skin.horizontalScrollbarLeftButton.hover.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.normal.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.active.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.hover.background = Slider;

                GUI.skin.toggle.normal.background = ToggleN;
                GUI.skin.toggle.active.background = ToggleA;
                GUI.skin.toggle.hover.background = ToggleH;
                GUI.skin.toggle.onNormal.background = ToggleON;
                GUI.skin.toggle.onActive.background = ToggleOA;
                GUI.skin.toggle.onHover.background = ToggleOH;

                GUI.skin.box.normal.background = Box;

                GUI.skin.window.normal.background = Window;
                GUI.skin.window.active.background = Window;
                GUI.skin.window.onNormal.background = Window;
                GUI.skin.window.onActive.background = Window;
            }
        }
    }
}
