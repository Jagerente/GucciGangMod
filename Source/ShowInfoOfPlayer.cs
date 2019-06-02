using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

[RequireComponent(typeof(PhotonView))]
public class ShowInfoOfPlayer : MonoBehaviour
{
    public bool DisableOnOwnObjects;
    public Font font;
    private const int FontSize3D = 0;
    private GameObject textGo;
    private TextMesh tm;

    private void OnDisable()
    {
        if (textGo != null)
        {
            textGo.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (textGo != null)
        {
            textGo.SetActive(true);
        }
    }

    private void Start()
    {
        if (font == null)
        {
            font = (Font) Resources.FindObjectsOfTypeAll(typeof(Font))[0];
            Debug.LogWarning("No font defined. Found font: " + font);
        }

        if (tm == null)
        {
            textGo = new GameObject("3d text");
            textGo.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            textGo.transform.parent = gameObject.transform;
            textGo.transform.localPosition = Vector3.zero;
            textGo.AddComponent<MeshRenderer>().material = font.material;
            tm = textGo.AddComponent<TextMesh>();
            tm.font = font;
            tm.fontSize = 0;
            tm.anchor = TextAnchor.MiddleCenter;
        }

        if (!DisableOnOwnObjects && photonView.isMine)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (DisableOnOwnObjects)
        {
            enabled = false;
            if (textGo != null)
            {
                textGo.SetActive(false);
            }
        }
        else
        {
            var owner = photonView.owner;
            if (owner != null)
            {
                tm.text = !string.IsNullOrEmpty(owner.name) ? owner.name : "n/a";
            }
            else if (photonView.isSceneView)
            {
                if (!DisableOnOwnObjects && photonView.isMine)
                {
                    enabled = false;
                    textGo.SetActive(false);
                }
                else
                {
                    tm.text = "scn";
                }
            }
            else
            {
                tm.text = "n/a";
            }
        }
    }
}