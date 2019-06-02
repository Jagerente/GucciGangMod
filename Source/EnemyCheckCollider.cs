using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class EnemyCheckCollider : MonoBehaviour
{
    public bool active_me;
    private int count;
    public int dmg = 1;
    public bool isThisBite;

    private void FixedUpdate()
    {
        if (count > 1)
        {
            active_me = false;
        }
        else
        {
            count++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || transform.root.gameObject.GetPhotonView().isMine) && active_me)
        {
            if (other.gameObject.tag == "playerHitbox")
            {
                var b = 1f - Vector3.Distance(other.gameObject.transform.position, transform.position) * 0.05f;
                b = Mathf.Min(1f, b);
                var component = other.gameObject.GetComponent<HitBox>();
                if (component != null && component.transform.root != null)
                {
                    if (dmg == 0)
                    {
                        var vector = component.transform.root.transform.position - transform.position;
                        var num2 = 0f;
                        if (gameObject.GetComponent<SphereCollider>() != null)
                        {
                            num2 = transform.localScale.x * gameObject.GetComponent<SphereCollider>().radius;
                        }
                        if (gameObject.GetComponent<CapsuleCollider>() != null)
                        {
                            num2 = transform.localScale.x * gameObject.GetComponent<CapsuleCollider>().height;
                        }
                        var num3 = 5f;
                        if (num2 > 0f)
                        {
                            num3 = Mathf.Max(5f, num2 - vector.magnitude);
                        }
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            component.transform.root.GetComponent<HERO>().blowAway(vector.normalized * num3 + Vector3.up * 1f);
                        }
                        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            object[] parameters = { vector.normalized * num3 + Vector3.up * 1f };
                            component.transform.root.GetComponent<HERO>().photonView.RPC("blowAway", PhotonTargets.All, parameters);
                        }
                    }
                    else if (!component.transform.root.GetComponent<HERO>().isInvincible())
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!component.transform.root.GetComponent<HERO>().isGrabbed)
                            {
                                var vector4 = component.transform.root.transform.position - transform.position;
                                component.transform.root.GetComponent<HERO>().die(vector4.normalized * b * 1000f + Vector3.up * 50f, isThisBite);
                            }
                        }
                        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && !component.transform.root.GetComponent<HERO>().HasDied() && !component.transform.root.GetComponent<HERO>().isGrabbed)
                        {
                            component.transform.root.GetComponent<HERO>().markDie();
                            var myOwnerViewID = -1;
                            var titanName = string.Empty;
                            if (transform.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null)
                            {
                                myOwnerViewID = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID;
                                titanName = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().titanName;
                            }
                            var objArray2 = new object[5];
                            var vector5 = component.transform.root.position - transform.position;
                            objArray2[0] = vector5.normalized * b * 1000f + Vector3.up * 50f;
                            objArray2[1] = isThisBite;
                            objArray2[2] = myOwnerViewID;
                            objArray2[3] = titanName;
                            objArray2[4] = true;
                            component.transform.root.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray2);
                        }
                    }
                }
            }
            else if (other.gameObject.tag == "erenHitbox" && dmg > 0 && !other.gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().isHit)
            {
                other.gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByTitan();
            }
        }
    }

    private void Start()
    {
        active_me = true;
        count = 0;
    }
}

