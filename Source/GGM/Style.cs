using UnityEngine;

namespace GGM
{
    internal class Style : MonoBehaviour
    {
        private static readonly string _path = "Gucci/";
        private static bool _styleInited;
        public static Texture2D _box;
        private static Texture2D _window;
        private static Texture2D _buttonN;
        private static Texture2D _buttonA;
        private static Texture2D _buttonH;
        private static Texture2D _buttonON;
        private static Texture2D _buttonOA;
        private static Texture2D _buttonOH;
        private static Texture2D _textN;
        private static Texture2D _textH;
        private static Texture2D _textF;
        private static Texture2D _textON;
        private static Texture2D _thumbN;
        private static Texture2D _thumbA;
        private static Texture2D _thumbH;
        private static Texture2D _toggleN;
        private static Texture2D _toggleA;
        private static Texture2D _toggleH;
        private static Texture2D _toggleON;
        private static Texture2D _toggleOA;
        private static Texture2D _toggleOH;
        private static Texture2D _slider;
        private static Texture2D _verticalScrollBar;
        private static Texture2D _horizontalScrollBar;
        private static Texture2D _scrollBarThumbN;
        private static Texture2D _scrollBarThumbA;
        private static Texture2D _scrollBarThumbH;
        public static Texture2D Logo;

        private void Start()
        {
            var logo = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}GGP_Logo.png");
            if (Logo == null)
            {
                Logo = logo.texture;
                Logo.Apply();
            }
            var box = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}Box.png");
            if (_box == null)
            {
                _box = box.texture;
                _box.Apply();
            }
            var window = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}Window.png");
            if (_window == null)
            {
                _window = window.texture;
                _window.Apply();
            }
            var buttonn = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonN.png");
            if (_buttonN == null)
            {
                _buttonN = buttonn.texture;
                _buttonN.Apply();
            }
            var buttona = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonA.png");
            if (_buttonA == null)
            {
                _buttonA = buttona.texture;
                _buttonA.Apply();
            }
            var buttonh = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonH.png");
            if (_buttonH == null)
            {
                _buttonH = buttonh.texture;
                _buttonH.Apply();
            }
            var buttonon = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonON.png");
            if (_buttonON == null)
            {
                _buttonON = buttonon.texture;
                _buttonON.Apply();
            }
            var buttonoh = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonOH.png");
            if (_buttonOH == null)
            {
                _buttonOH = buttonoh.texture;
                _buttonOH.Apply();
            }
            var buttonoa = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ButtonOA.png");
            if (_buttonOA == null)
            {
                _buttonOA = buttonoa.texture;
                _buttonOA.Apply();
            }
            var textn = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}TextN.png");
            if (_textN == null)
            {
                _textN = textn.texture;
                _textN.Apply();
            }
            var texth = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}TextH.png");
            if (_textH == null)
            {
                _textH = texth.texture;
                _textH.Apply();
            }
            var textf = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}TextF.png");
            if (_textF == null)
            {
                _textF = textf.texture;
                _textF.Apply();
            }
            var texton = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}TextON.png");
            if (_textON == null)
            {
                _textON = texton.texture;
                _textON.Apply();
            }
            var thumbn = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ThumbN.png");
            if (_thumbN == null)
            {
                _thumbN = thumbn.texture;
                _thumbN.Apply();
            }
            var thumba = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ThumbA.png");
            if (_thumbA == null)
            {
                _thumbA = thumba.texture;
                _thumbA.Apply();
            }
            var thumbh = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ThumbH.png");
            if (_thumbH == null)
            {
                _thumbH = thumbh.texture;
                _thumbH.Apply();
            }
            var togglen = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleN.png");
            if (_toggleN == null)
            {
                _toggleN = togglen.texture;
                _toggleN.Apply();
            }
            var togglea = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleA.png");
            if (_toggleA == null)
            {
                _toggleA = togglea.texture;
                _toggleA.Apply();
            }
            var toggleh = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleH.png");
            if (_toggleH == null)
            {
                _toggleH = toggleh.texture;
                _toggleH.Apply();
            }
            var toggleon = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleON.png");
            if (_toggleON == null)
            {
                _toggleON = toggleon.texture;
                _toggleON.Apply();
            }
            var toggleoa = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleOA.png");
            if (_toggleOA == null)
            {
                _toggleOA = toggleoa.texture;
                _toggleOA.Apply();
            }
            var toggleoh = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ToggleOH.png");
            if (_toggleOH == null)
            {
                _toggleOH = toggleoh.texture;
                _toggleOH.Apply();
            }
            var verticalscrollbar = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ScrollBarH.png");
            if (_verticalScrollBar == null)
            {
                _verticalScrollBar = verticalscrollbar.texture;
                _verticalScrollBar.Apply();
            }
            var horizontalscrollbar = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ScrollBarV.png");
            if (_horizontalScrollBar == null)
            {
                _horizontalScrollBar = horizontalscrollbar.texture;
                _horizontalScrollBar.Apply();
            }
            var sbtn = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ScrollBarThumbN.png");
            if (_scrollBarThumbN == null)
            {
                _scrollBarThumbN = sbtn.texture;
                _scrollBarThumbN.Apply();
            }
            var sbta = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ScrollBarThumbA.png");
            if (_scrollBarThumbA == null)
            {
                _scrollBarThumbA = sbta.texture;
                _scrollBarThumbA.Apply();
            }
            var sbth = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}ScrollBarThumbH.png");
            if (_scrollBarThumbH == null)
            {
                _scrollBarThumbH = sbth.texture;
                _scrollBarThumbH.Apply();
            }
            var slider = new WWW("file:///" + Application.dataPath + $"/Styles/{_path}Slider.png");
            if (_slider == null)
            {
                _slider = slider.texture;
                _slider.Apply();
            }
        }

        public static void Init()
        {
            if (!_styleInited)
            {
                _styleInited = true;
                //GUI.skin.label.normal.textColor = Gucci_GUI.RGBColor_1;
                GUI.skin.button.normal.textColor = Color.white;
                GUI.skin.button.active.textColor = Color.white;
                GUI.skin.button.hover.textColor = Color.white;
                GUI.skin.button.onNormal.textColor = Color.white;
                GUI.skin.button.onActive.textColor = Color.white;
                GUI.skin.button.onHover.textColor = Color.white;
                GUI.skin.button.normal.background = _buttonN;
                GUI.skin.button.active.background = _buttonA;
                GUI.skin.button.hover.background = _buttonH;
                GUI.skin.button.onNormal.background = _buttonON;
                GUI.skin.button.onActive.background = _buttonOA;
                GUI.skin.button.onHover.background = _buttonOH;
                GUI.skin.button.fontStyle = FontStyle.Bold;

                GUI.skin.textField.normal.background = _textN;
                GUI.skin.textField.hover.background = _textH;
                GUI.skin.textField.focused.background = _textF;
                GUI.skin.textField.onNormal.background = _textON;

                GUI.skin.textArea.normal.background = _textN;
                GUI.skin.textArea.hover.background = _textH;
                GUI.skin.textArea.focused.background = _textF;
                GUI.skin.textArea.onNormal.background = _textON;
                GUI.skin.textArea.focused.textColor = Color.white;

                GUI.skin.verticalSlider.normal.background = _slider;
                GUI.skin.verticalSliderThumb.normal.background = _thumbN;
                GUI.skin.verticalSliderThumb.active.background = _thumbA;
                GUI.skin.verticalSliderThumb.hover.background = _thumbH;

                GUI.skin.horizontalSlider.normal.background = _slider;

                GUI.skin.horizontalSliderThumb.normal.background = _thumbN;
                GUI.skin.horizontalSliderThumb.active.background = _thumbA;
                GUI.skin.horizontalSliderThumb.hover.background = _thumbH;

                GUI.skin.horizontalScrollbar.normal.background = _horizontalScrollBar;
                GUI.skin.verticalScrollbar.normal.background = _verticalScrollBar;

                //GUI.skin.verticalScrollbarThumb.normal.background = ScrollBarThumbN;
                //GUI.skin.verticalScrollbarThumb.active.background = ScrollBarThumbA;
                //GUI.skin.verticalScrollbarThumb.hover.background = ScrollBarThumbH;
                //GUI.skin.horizontalScrollbarThumb.normal.background = ScrollBarThumbN;
                //GUI.skin.horizontalScrollbarThumb.active.background = ScrollBarThumbA;
                //GUI.skin.horizontalScrollbarThumb.hover.background = ScrollBarThumbH;

                GUI.skin.verticalScrollbarThumb.normal.background = _horizontalScrollBar;
                GUI.skin.verticalScrollbarThumb.active.background = _horizontalScrollBar;
                GUI.skin.verticalScrollbarThumb.hover.background = _horizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.normal.background = _horizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.active.background = _horizontalScrollBar;
                GUI.skin.horizontalScrollbarThumb.hover.background = _horizontalScrollBar;

                //GUI.skin.horizontalScrollbarLeftButton.normal.background = Slider;
                //GUI.skin.horizontalScrollbarLeftButton.active.background = Slider;
                //GUI.skin.horizontalScrollbarLeftButton.hover.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.normal.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.active.background = Slider;
                //GUI.skin.horizontalScrollbarRightButton.hover.background = Slider;

                GUI.skin.toggle.normal.textColor = Color.white;
                GUI.skin.toggle.active.textColor = Color.white;
                GUI.skin.toggle.hover.textColor = Color.white;
                GUI.skin.toggle.onNormal.textColor = Color.white;
                GUI.skin.toggle.onActive.textColor = Color.white;
                GUI.skin.toggle.onHover.textColor = Color.white;
                GUI.skin.toggle.normal.background = _toggleN;
                GUI.skin.toggle.active.background = _toggleA;
                GUI.skin.toggle.hover.background = _toggleH;
                GUI.skin.toggle.onNormal.background = _toggleON;
                GUI.skin.toggle.onActive.background = _toggleOA;
                GUI.skin.toggle.onHover.background = _toggleOH;

                GUI.skin.box.normal.textColor = Color.white;
                GUI.skin.box.normal.background = _box;

                GUI.skin.window.normal.background = _window;
                GUI.skin.window.active.background = _window;
                GUI.skin.window.onNormal.background = _window;
                GUI.skin.window.onActive.background = _window;
            }
        }
    }
}