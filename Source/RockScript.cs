using UnityEngine;

public class RockScript : MonoBehaviour
{
    private Vector3 desPt = new Vector3(-200f, 0f, -280f);
    private bool disable;
    private float g = 500f;
    private float speed = 800f;
    private Vector3 vh;
    private Vector3 vv;

    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 676f);
        vh = desPt - transform.position;
        vv = new Vector3(0f, g * vh.magnitude / (2f * speed), 0f);
        vh.Normalize();
        vh = vh * speed;
    }

    private void Update()
    {
        if (!disable)
        {
            vv += -Vector3.up * g * Time.deltaTime;
            var transform = this.transform;
            transform.position += vv * Time.deltaTime;
            var transform2 = this.transform;
            transform2.position += vh * Time.deltaTime;
            if (Vector3.Distance(desPt, this.transform.position) < 20f || this.transform.position.y < 0f)
            {
                this.transform.position = desPt;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                {
                    if (FengGameManagerMKII.LAN)
                    {
                        Network.Instantiate(Resources.Load("FX/boom1_CT_KICK"), this.transform.position + Vector3.up * 30f, Quaternion.Euler(270f, 0f, 0f), 0);
                    }
                    else
                    {
                        PhotonNetwork.Instantiate("FX/boom1_CT_KICK", this.transform.position + Vector3.up * 30f, Quaternion.Euler(270f, 0f, 0f), 0);
                    }
                }
                else
                {
                    Instantiate(Resources.Load("FX/boom1_CT_KICK"), this.transform.position + Vector3.up * 30f, Quaternion.Euler(270f, 0f, 0f));
                }

                disable = true;
            }
        }
    }
}