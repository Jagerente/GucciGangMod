using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class CannonBall : MonoBehaviour
{
    private Vector3 correctPos;
    private Vector3 correctVelocity;
    public bool disabled;
    public Transform firingPoint;
    public bool isCollider;
    public HERO myHero;
    public List<TitanTrigger> myTitanTriggers;
    public float SmoothingDelay = 10f;

    private void Awake()
    {
        if (photonView != null)
        {
            photonView.observed = this;
            correctPos = transform.position;
            correctVelocity = Vector3.zero;
            GetComponent<SphereCollider>().enabled = false;
            if (photonView.isMine)
            {
                StartCoroutine(WaitAndDestroy(10f));
                myTitanTriggers = new List<TitanTrigger>();
            }
        }
    }

    public void destroyMe()
    {
        if (!disabled)
        {
            disabled = true;
            var obj2 = PhotonNetwork.Instantiate("FX/boom4", transform.position, transform.rotation, 0);
            foreach (var collider in obj2.GetComponentsInChildren<EnemyCheckCollider>())
            {
                collider.dmg = 0;
            }

            if (RCSettings.deadlyCannons == 1)
            {
                foreach (HERO hero in FengGameManagerMKII.FGM.getPlayers())
                {
                    if (hero != null && Vector3.Distance(hero.transform.position, transform.position) <= 20f && !hero.photonView.isMine)
                    {
                        var gameObject = hero.gameObject;
                        var owner = gameObject.GetPhotonView().owner;
                        if (RCSettings.teamMode > 0 && PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null && owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                        {
                            var num2 = RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]);
                            var num3 = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
                            if (num2 == 0 || num2 != num3)
                            {
                                gameObject.GetComponent<HERO>().markDie();
                                gameObject.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, -1, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) + " ");
                                FengGameManagerMKII.FGM.playerKillInfoUpdate(PhotonNetwork.player, 0);
                            }
                        }
                        else
                        {
                            gameObject.GetComponent<HERO>().markDie();
                            gameObject.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, -1, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) + " ");
                            FengGameManagerMKII.FGM.playerKillInfoUpdate(PhotonNetwork.player, 0);
                        }
                    }
                }
            }

            if (myTitanTriggers != null)
            {
                for (var i = 0; i < myTitanTriggers.Count; i++)
                {
                    if (myTitanTriggers[i] != null)
                    {
                        myTitanTriggers[i].isCollide = false;
                    }
                }
            }

            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (photonView.isMine && !disabled)
        {
            LayerMask mask = 1 << LayerMask.NameToLayer("PlayerAttackBox");
            LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask | mask2;
            if (!isCollider)
            {
                LayerMask mask4 = 1 << LayerMask.NameToLayer("Ground");
                mask3 |= mask4;
            }

            var colliderArray = Physics.OverlapSphere(transform.position, 0.6f, mask3.value);
            var flag2 = false;
            for (var i = 0; i < colliderArray.Length; i++)
            {
                var gameObject = colliderArray[i].gameObject;
                if (gameObject.layer == 16)
                {
                    var component = gameObject.GetComponent<TitanTrigger>();
                    if (!(component == null || myTitanTriggers.Contains(component)))
                    {
                        component.isCollide = true;
                        myTitanTriggers.Add(component);
                    }
                }
                else if (gameObject.layer == 10)
                {
                    var titan = gameObject.transform.root.gameObject.GetComponent<TITAN>();
                    if (titan != null)
                    {
                        if (titan.abnormalType == AbnormalType.TYPE_CRAWLER)
                        {
                            if (gameObject.name == "head")
                            {
                                titan.photonView.RPC("DieByCannon", titan.photonView.owner, myHero.photonView.viewID);
                                titan.dieBlow(transform.position, 0.2f);
                                i = colliderArray.Length;
                            }
                        }
                        else if (gameObject.name == "head")
                        {
                            titan.photonView.RPC("DieByCannon", titan.photonView.owner, myHero.photonView.viewID);
                            titan.dieHeadBlow(transform.position, 0.2f);
                            i = colliderArray.Length;
                        }
                        else if (Random.Range(0f, 1f) < 0.5f)
                        {
                            titan.hitL(transform.position, 0.05f);
                        }
                        else
                        {
                            titan.hitR(transform.position, 0.05f);
                        }

                        destroyMe();
                    }
                }
                else if (gameObject.layer == 9 && (gameObject.transform.root.name.Contains("CannonWall") || gameObject.transform.root.name.Contains("CannonGround")))
                {
                    flag2 = true;
                }
            }

            if (!(isCollider || flag2))
            {
                isCollider = true;
                GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    public void OnCollisionEnter(Collision myCollision)
    {
        if (photonView.isMine)
        {
            destroyMe();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(rigidbody.velocity);
        }
        else
        {
            correctPos = (Vector3) stream.ReceiveNext();
            correctVelocity = (Vector3) stream.ReceiveNext();
        }
    }

    public void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPos, Time.deltaTime * SmoothingDelay);
            rigidbody.velocity = correctVelocity;
        }
    }

    public IEnumerator WaitAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        destroyMe();
    }
}