using UnityEngine;

public class FlareMovement : MonoBehaviour
{
    public string color;
    private GameObject hero;
    private GameObject hint;
    private bool nohint;
    private Vector3 offY;
    private float timer;

    public void dontShowHint()
    {
        Destroy(hint);
        nohint = true;
    }

    private void Start()
    {
        hero = GameObject.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object;
        if (!nohint && hero != null)
        {
            hint = (GameObject) Instantiate(Resources.Load("UI/" + color + "FlareHint"));
            if (color == "Black")
            {
                offY = Vector3.up * 0.4f;
            }
            else
            {
                offY = Vector3.up * 0.5f;
            }
            hint.transform.parent = transform.root;
            hint.transform.position = hero.transform.position + offY;
            var vector = transform.position - hint.transform.position;
            var num = Mathf.Atan2(-vector.z, vector.x) * 57.29578f;
            hint.transform.rotation = Quaternion.Euler(-90f, num + 180f, 0f);
            hint.transform.localScale = Vector3.zero;
            object[] args = { "x", 1f, "y", 1f, "z", 1f, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(hint, iTween.Hash(args));
            object[] objArray2 = { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2.5f };
            iTween.ScaleTo(hint, iTween.Hash(objArray2));
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (hint != null)
        {
            if (timer < 3f)
            {
                hint.transform.position = hero.transform.position + offY;
                var vector = transform.position - hint.transform.position;
                var num = Mathf.Atan2(-vector.z, vector.x) * 57.29578f;
                hint.transform.rotation = Quaternion.Euler(-90f, num + 180f, 0f);
            }
            else if (hint != null)
            {
                Destroy(hint);
            }
        }
        if (timer < 4f)
        {
            rigidbody.AddForce((transform.forward + transform.up * 5f) * Time.deltaTime * 5f, ForceMode.VelocityChange);
        }
        else
        {
            rigidbody.AddForce(-transform.up * Time.deltaTime * 7f, ForceMode.Acceleration);
        }
    }
}

