//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;
using UnityEngine.UI;

public class StyledItemButtonImageText : StyledItem
{
    public Button buttonCtrl;
    public RawImage rawImageCtrl;
    public Text textCtrl;

    public override Button GetButton()
    {
        return buttonCtrl;
    }

    public override RawImage GetRawImage()
    {
        return rawImageCtrl;
    }

    public override Text GetText()
    {
        return textCtrl;
    }

    public override void Populate(object o)
    {
        var textured = o as Texture2D;
        if (textured != null)
        {
            if (rawImageCtrl != null)
            {
                rawImageCtrl.texture = textured;
            }
        }
        else
        {
            var data = o as Data;
            if (data == null)
            {
                if (textCtrl != null)
                {
                    textCtrl.text = o.ToString();
                }
            }
            else
            {
                if (rawImageCtrl != null)
                {
                    rawImageCtrl.texture = data.image;
                }
                if (textCtrl != null)
                {
                    textCtrl.text = data.text;
                }
            }
        }
    }

    public class Data
    {
        public Texture2D image;
        public string text;

        public Data(string t, Texture2D tex)
        {
            text = t;
            image = tex;
        }
    }
}

