using System.Collections;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class Bomb : MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool disabled;
    public GameObject myExplosion;
    public float SmoothingDelay = 10f;

    public void Awake()
    {
        if (photonView != null)
        {
            float num2;
            float num3;
            float num4;
            photonView.observed = this;
            correctPlayerPos = transform.position;
            correctPlayerRot = Quaternion.identity;
            var owner = photonView.owner;
            if (RCSettings.teamMode > 0)
            {
                var num = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
                if (num == 1)
                {
                    GetComponent<ParticleSystem>().startColor = Color.cyan;
                }
                else if (num == 2)
                {
                    GetComponent<ParticleSystem>().startColor = Color.magenta;
                }
                else
                {
                    num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
                    num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
                    num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
                    GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, 1f);
                }
            }
            else
            {
                num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
                num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
                num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
                GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, 1f);
            }
        }
    }

    public void destroyMe()
    {
        if (photonView.isMine)
        {
            if (myExplosion != null)
            {
                PhotonNetwork.Destroy(myExplosion);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void Explode(float radius)
    {
        disabled = true;
        rigidbody.velocity = Vector3.zero;
        var position = transform.position;
        myExplosion = PhotonNetwork.Instantiate("RCAsset/BombExplodeMain", position, Quaternion.Euler(0f, 0f, 0f), 0);
        foreach (HERO hero in FengGameManagerMKII.instance.getPlayers())
        {
            var gameObject = hero.gameObject;
            if (Vector3.Distance(gameObject.transform.position, position) < radius && !gameObject.GetPhotonView().isMine && !hero.bombImmune)
            {
                var owner = gameObject.GetPhotonView().owner;
                if (RCSettings.teamMode > 0 && PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null && owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    var num = RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]);
                    var num2 = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
                    if (num == 0 || num != num2)
                    {
                        gameObject.GetComponent<HERO>().markDie();
                        gameObject.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, -1, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) + " ");
                        FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                    }
                }
                else
                {
                    gameObject.GetComponent<HERO>().markDie();
                    gameObject.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, -1, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) + " ");
                    FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                }
            }
        }
        StartCoroutine(WaitAndFade(1.5f));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidbody.velocity);
        }
        else
        {
            correctPlayerPos = (Vector3) stream.ReceiveNext();
            correctPlayerRot = (Quaternion) stream.ReceiveNext();
            correctPlayerVelocity = (Vector3) stream.ReceiveNext();
        }
    }

    public void Update()
    {
        if (!(disabled || photonView.isMine))
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * SmoothingDelay);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * SmoothingDelay);
            rigidbody.velocity = correctPlayerVelocity;
        }
    }

    private IEnumerator WaitAndFade(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(myExplosion);
        PhotonNetwork.Destroy(gameObject);
    }

}

