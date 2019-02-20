//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class KillInfoComponent : MonoBehaviour
{
    private float alpha = 1f;
    private int col;
    public GameObject groupBig;
    public GameObject groupSmall;
    public GameObject labelNameLeft;
    public GameObject labelNameRight;
    public GameObject labelScore;
    public GameObject leftTitan;
    private float lifeTime = 8f;
    private float maxScale = 1.5f;
    private int offset = 0x18;
    public GameObject rightTitan;
    public GameObject slabelNameLeft;
    public GameObject slabelNameRight;
    public GameObject slabelScore;
    public GameObject sleftTitan;
    public GameObject spriteSkeleton;
    public GameObject spriteSword;
    public GameObject srightTitan;
    public GameObject sspriteSkeleton;
    public GameObject sspriteSword;
    private bool start;
    private float timeElapsed;

    public void destory()
    {
        timeElapsed = lifeTime;
    }

    public void moveOn()
    {
        col++;
        if (col > 4)
        {
            timeElapsed = lifeTime;
        }
        groupBig.SetActive(false);
        groupSmall.SetActive(true);
    }

    private void setAlpha(float alpha)
    {
        if (groupBig.activeInHierarchy)
        {
            labelScore.GetComponent<UILabel>().color = new Color(labelScore.GetComponent<UILabel>().color.r, labelScore.GetComponent<UILabel>().color.g, labelScore.GetComponent<UILabel>().color.b, alpha);
            leftTitan.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            rightTitan.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            labelNameLeft.GetComponent<UILabel>().color = new Color(1f, 1f, 1f, alpha);
            labelNameRight.GetComponent<UILabel>().color = new Color(1f, 1f, 1f, alpha);
            spriteSkeleton.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            spriteSword.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
        }
        if (groupSmall.activeInHierarchy)
        {
            slabelScore.GetComponent<UILabel>().color = new Color(labelScore.GetComponent<UILabel>().color.r, labelScore.GetComponent<UILabel>().color.g, labelScore.GetComponent<UILabel>().color.b, alpha);
            sleftTitan.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            srightTitan.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            slabelNameLeft.GetComponent<UILabel>().color = new Color(1f, 1f, 1f, alpha);
            slabelNameRight.GetComponent<UILabel>().color = new Color(1f, 1f, 1f, alpha);
            sspriteSkeleton.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
            sspriteSword.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, alpha);
        }
    }

    public void show(bool isTitan1, string name1, bool isTitan2, string name2, int dmg = 0)
    {
        groupBig.SetActive(true);
        groupSmall.SetActive(true);
        if (!isTitan1)
        {
            leftTitan.SetActive(false);
            spriteSkeleton.SetActive(false);
            sleftTitan.SetActive(false);
            sspriteSkeleton.SetActive(false);
            var transform = labelNameLeft.transform;
            transform.position += new Vector3(18f, 0f, 0f);
            var transform2 = slabelNameLeft.transform;
            transform2.position += new Vector3(16f, 0f, 0f);
        }
        else
        {
            spriteSword.SetActive(false);
            sspriteSword.SetActive(false);
            var transform3 = labelNameRight.transform;
            transform3.position -= new Vector3(18f, 0f, 0f);
            var transform4 = slabelNameRight.transform;
            transform4.position -= new Vector3(16f, 0f, 0f);
        }
        if (!isTitan2)
        {
            rightTitan.SetActive(false);
            srightTitan.SetActive(false);
        }
        labelNameLeft.GetComponent<UILabel>().text = name1;
        labelNameRight.GetComponent<UILabel>().text = name2;
        slabelNameLeft.GetComponent<UILabel>().text = name1;
        slabelNameRight.GetComponent<UILabel>().text = name2;
        if (dmg == 0)
        {
            labelScore.GetComponent<UILabel>().text = string.Empty;
            slabelScore.GetComponent<UILabel>().text = string.Empty;
        }
        else
        {
            labelScore.GetComponent<UILabel>().text = dmg.ToString();
            slabelScore.GetComponent<UILabel>().text = dmg.ToString();
            if (dmg > 0x3e8)
            {
                labelScore.GetComponent<UILabel>().color = Color.red;
                slabelScore.GetComponent<UILabel>().color = Color.red;
            }
        }
        groupSmall.SetActive(false);
    }

    private void Start()
    {
        start = true;
        transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        transform.localPosition = new Vector3(0f, -100f + (Screen.height * 0.5f), 0f);
    }

    private void Update()
    {
        if (start)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed < 0.2f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * maxScale, Time.deltaTime * 10f);
            }
            else if (timeElapsed < 1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 10f);
            }
            if (timeElapsed > lifeTime)
            {
                transform.position += new Vector3(0f, Time.deltaTime * 0.15f, 0f);
                alpha = ((1f - (Time.deltaTime * 45f)) + lifeTime) - timeElapsed;
                setAlpha(alpha);
            }
            else
            {
                float num = ((int) (100f - (Screen.height * 0.5f))) + (col * offset);
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, -num, 0f), Time.deltaTime * 10f);
            }
            if (timeElapsed > (lifeTime + 0.5f))
            {
                Destroy(gameObject);
            }
        }
    }
}

