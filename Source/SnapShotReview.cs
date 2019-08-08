using System;
using UnityEngine;

public class SnapShotReview : MonoBehaviour
{
    public GameObject labelDMG;
    public GameObject labelInfo;
    public GameObject labelPage;
    private UILabel page;
    public GameObject texture;
    private float textureH = 600f;
    private float textureW = 960f;

    private void freshInfo()
    {
        if (SnapShotSaves.getLength() == 0)
        {
            page.text = "0/0";
        }
        else
        {
            page.text = SnapShotSaves.getCurrentIndex() + 1 + "/" + SnapShotSaves.getLength();
        }
        if (SnapShotSaves.getCurrentDMG() > 0)
        {
            labelDMG.GetComponent<UILabel>().text = SnapShotSaves.getCurrentDMG().ToString();
        }
        else
        {
            labelDMG.GetComponent<UILabel>().text = string.Empty;
        }
    }

    private void setTextureWH()
    {
        if (SnapShotSaves.getLength() != 0)
        {
            var num = 1.6f;
            var num2 = texture.GetComponent<UITexture>().mainTexture.width / (float)texture.GetComponent<UITexture>().mainTexture.height;
            if (num2 > num)
            {
                texture.transform.localScale = new Vector3(textureW, textureW / num2, 0f);
                labelDMG.transform.localPosition = new Vector3((int)(textureW * 0.5f - 20f), (int)(0f + textureW * 0.5f / num2 - 20f), -20f);
                labelInfo.transform.localPosition = new Vector3((int)(textureW * 0.5f - 20f), (int)(0f - textureW * 0.5f / num2 + 20f), -20f);
            }
            else
            {
                texture.transform.localScale = new Vector3(textureH * num2, textureH, 0f);
                labelDMG.transform.localPosition = new Vector3((int)(textureH * num2 * 0.5f - 20f), (int)(0f + textureH * 0.5f - 20f), -20f);
                labelInfo.transform.localPosition = new Vector3((int)(textureH * num2 * 0.5f - 20f), (int)(0f - textureH * 0.5f + 20f), -20f);
            }
        }
    }

    public void ShowNextIMG()
    {
        texture.GetComponent<UITexture>().mainTexture = SnapShotSaves.GetNextIMG();
        setTextureWH();
        freshInfo();
    }

    public void ShowPrevIMG()
    {
        texture.GetComponent<UITexture>().mainTexture = SnapShotSaves.GetPrevIMG();
        setTextureWH();
        freshInfo();
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(5, true);
        page = labelPage.GetComponent<UILabel>();
        if (SnapShotSaves.getLength() > 0)
        {
            texture.GetComponent<UITexture>().mainTexture = SnapShotSaves.getCurrentIMG();
        }
        labelInfo.GetComponent<UILabel>().text = LoginFengKAI.player.name + " " + DateTime.Today.ToShortDateString();
        freshInfo();
        setTextureWH();
    }
}