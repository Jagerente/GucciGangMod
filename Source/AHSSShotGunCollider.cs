using System.Collections;
using GGM.Config;
using UnityEngine;

public class AHSSShotGunCollider : MonoBehaviour
{
    public bool active_me;
    private int count;
    public GameObject currentCamera;
    public ArrayList currentHits = new ArrayList();
    public int dmg = 1;
    private int myTeam = 1;
    private string ownerName = string.Empty;
    public float scoreMulti;
    private int viewID = -1;

    private bool checkIfBehind(GameObject titan)
    {
        var transform = titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        var to = this.transform.position - transform.transform.position;
        Debug.DrawRay(transform.transform.position, -transform.transform.forward * 10f, Color.white, 5f);
        Debug.DrawRay(transform.transform.position, to * 10f, Color.green, 5f);
        return Vector3.Angle(-transform.transform.forward, to) < 100f;
    }

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
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER ||
             transform.root.gameObject.GetPhotonView().isMine) && active_me)
        {
            if (other.gameObject.tag == "playerHitbox")
            {
                if (LevelInfo.getInfo(FengGameManagerMKII.level).pvp)
                {
                    var b = 1f - Vector3.Distance(other.gameObject.transform.position, transform.position) * 0.05f;
                    b = Mathf.Min(1f, b);
                    var component = other.gameObject.GetComponent<HitBox>();
                    if (component != null && component.transform.root != null &&
                        component.transform.root.GetComponent<HERO>().myTeam != myTeam &&
                        !component.transform.root.GetComponent<HERO>().isInvincible())
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!component.transform.root.GetComponent<HERO>().isGrabbed)
                            {
                                var vector = component.transform.root.transform.position - transform.position;
                                component.transform.root.GetComponent<HERO>()
                                    .die(vector.normalized * b * 1000f + Vector3.up * 50f, false);
                            }
                        }
                        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER &&
                                 !component.transform.root.GetComponent<HERO>().HasDied() &&
                                 !component.transform.root.GetComponent<HERO>().isGrabbed)
                        {
                            component.transform.root.GetComponent<HERO>().markDie();
                            var parameters = new object[5];
                            var vector2 = component.transform.root.position - transform.position;
                            parameters[0] = vector2.normalized * b * 1000f + Vector3.up * 50f;
                            parameters[1] = false;
                            parameters[2] = viewID;
                            parameters[3] = ownerName;
                            parameters[4] = false;
                            component.transform.root.GetComponent<HERO>().photonView
                                .RPC("netDie", PhotonTargets.All, parameters);
                        }
                    }
                }
            }
            else if (other.gameObject.tag == "erenHitbox")
            {
                if (dmg > 0 && !other.gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().isHit)
                {
                    other.gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByTitan();
                }
            }
            else if (other.gameObject.tag == "titanneck")
            {
                var item = other.gameObject.GetComponent<HitBox>();
                if (item != null && checkIfBehind(item.transform.root.gameObject) && !currentHits.Contains(item))
                {
                    item.hitPosition = (transform.position + item.transform.position) * 0.5f;
                    currentHits.Add(item);
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (item.transform.root.GetComponent<TITAN>() != null &&
                            !item.transform.root.GetComponent<TITAN>().hasDie)
                        {
                            var vector3 =
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                item.transform.root.rigidbody.velocity;
                            var num2 = (int) (vector3.magnitude * 10f * scoreMulti);
                            num2 = Mathf.Max(10, num2);
                            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>()
                                .netShowDamage(num2);
                            if (num2 > item.transform.root.GetComponent<TITAN>().myLevel * 100f)
                            {
                                item.transform.root.GetComponent<TITAN>().die();
                                if (Settings.SnapshotsSetting)
                                {
                                    GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>()
                                        .startSnapShot(item.transform.position, num2, item.transform.root.gameObject,
                                            0.02f);
                                }

                                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>()
                                    .playerKillInfoSingleUpdate(num2);
                            }
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (item.transform.root.GetComponent<TITAN>() != null)
                        {
                            if (!item.transform.root.GetComponent<TITAN>().hasDie)
                            {
                                var vector4 =
                                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                    item.transform.root.rigidbody.velocity;
                                var num3 = (int) (vector4.magnitude * 10f * scoreMulti);
                                num3 = Mathf.Max(10, num3);
                                if (num3 > item.transform.root.GetComponent<TITAN>().myLevel * 100f)
                                {
                                    if (Settings.SnapshotsSetting)
                                    {
                                        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>()
                                            .startSnapShot(item.transform.position, num3,
                                                item.transform.root.gameObject, 0.02f);
                                        item.transform.root.GetComponent<TITAN>().asClientLookTarget = false;
                                    }

                                    object[] objArray2 = {transform.root.gameObject.GetPhotonView().viewID, num3};
                                    item.transform.root.GetComponent<TITAN>().photonView.RPC("titanGetHit",
                                        item.transform.root.GetComponent<TITAN>().photonView.owner, objArray2);
                                }
                            }
                        }
                        else if (item.transform.root.GetComponent<FEMALE_TITAN>() != null)
                        {
                            var vector5 =
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                item.transform.root.rigidbody.velocity;
                            var num4 = (int) (vector5.magnitude * 10f * scoreMulti);
                            num4 = Mathf.Max(10, num4);
                            if (!item.transform.root.GetComponent<FEMALE_TITAN>().hasDie)
                            {
                                object[] objArray3 = {transform.root.gameObject.GetPhotonView().viewID, num4};
                                item.transform.root.GetComponent<FEMALE_TITAN>().photonView.RPC("titanGetHit",
                                    item.transform.root.GetComponent<FEMALE_TITAN>().photonView.owner, objArray3);
                            }
                        }
                        else if (item.transform.root.GetComponent<COLOSSAL_TITAN>() != null &&
                                 !item.transform.root.GetComponent<COLOSSAL_TITAN>().hasDie)
                        {
                            var vector6 =
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                item.transform.root.rigidbody.velocity;
                            var num5 = (int) (vector6.magnitude * 10f * scoreMulti);
                            num5 = Mathf.Max(10, num5);
                            object[] objArray4 = {transform.root.gameObject.GetPhotonView().viewID, num5};
                            item.transform.root.GetComponent<COLOSSAL_TITAN>().photonView.RPC("titanGetHit",
                                item.transform.root.GetComponent<COLOSSAL_TITAN>().photonView.owner, objArray4);
                        }
                    }
                    else if (item.transform.root.GetComponent<TITAN>() != null)
                    {
                        if (!item.transform.root.GetComponent<TITAN>().hasDie)
                        {
                            var vector7 =
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                item.transform.root.rigidbody.velocity;
                            var num6 = (int) (vector7.magnitude * 10f * scoreMulti);
                            num6 = Mathf.Max(10, num6);
                            if (num6 > item.transform.root.GetComponent<TITAN>().myLevel * 100f)
                            {
                                if (Settings.SnapshotsSetting)
                                {
                                    GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>()
                                        .startSnapShot(item.transform.position, num6, item.transform.root.gameObject,
                                            0.02f);
                                }

                                item.transform.root.GetComponent<TITAN>()
                                    .titanGetHit(transform.root.gameObject.GetPhotonView().viewID, num6);
                            }
                        }
                    }
                    else if (item.transform.root.GetComponent<FEMALE_TITAN>() != null)
                    {
                        if (!item.transform.root.GetComponent<FEMALE_TITAN>().hasDie)
                        {
                            var vector8 =
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                item.transform.root.rigidbody.velocity;
                            var num7 = (int) (vector8.magnitude * 10f * scoreMulti);
                            num7 = Mathf.Max(10, num7);
                            if (Settings.SnapshotsSetting)
                            {
                                GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>()
                                    .startSnapShot(item.transform.position, num7, null, 0.02f);
                            }

                            item.transform.root.GetComponent<FEMALE_TITAN>()
                                .titanGetHit(transform.root.gameObject.GetPhotonView().viewID, num7);
                        }
                    }
                    else if (item.transform.root.GetComponent<COLOSSAL_TITAN>() != null &&
                             !item.transform.root.GetComponent<COLOSSAL_TITAN>().hasDie)
                    {
                        var vector9 = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                                      item.transform.root.rigidbody.velocity;
                        var num8 = (int) (vector9.magnitude * 10f * scoreMulti);
                        num8 = Mathf.Max(10, num8);
                        if (Settings.SnapshotsSetting)
                        {
                            GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>()
                                .startSnapShot(item.transform.position, num8, null, 0.02f);
                        }

                        item.transform.root.GetComponent<COLOSSAL_TITAN>()
                            .titanGetHit(transform.root.gameObject.GetPhotonView().viewID, num8);
                    }

                    showCriticalHitFX(other.gameObject.transform.position);
                }
            }
            else if (other.gameObject.tag == "titaneye")
            {
                if (!currentHits.Contains(other.gameObject))
                {
                    currentHits.Add(other.gameObject);
                    var gameObject = other.gameObject.transform.root.gameObject;
                    if (gameObject.GetComponent<FEMALE_TITAN>() != null)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!gameObject.GetComponent<FEMALE_TITAN>().hasDie)
                            {
                                gameObject.GetComponent<FEMALE_TITAN>().hitEye();
                            }
                        }
                        else if (!PhotonNetwork.isMasterClient)
                        {
                            if (!gameObject.GetComponent<FEMALE_TITAN>().hasDie)
                            {
                                object[] objArray5 = {transform.root.gameObject.GetPhotonView().viewID};
                                gameObject.GetComponent<FEMALE_TITAN>().photonView.RPC("hitEyeRPC",
                                    PhotonTargets.MasterClient, objArray5);
                            }
                        }
                        else if (!gameObject.GetComponent<FEMALE_TITAN>().hasDie)
                        {
                            gameObject.GetComponent<FEMALE_TITAN>()
                                .hitEyeRPC(transform.root.gameObject.GetPhotonView().viewID);
                        }
                    }
                    else if (gameObject.GetComponent<TITAN>().abnormalType != AbnormalType.TYPE_CRAWLER)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            if (!gameObject.GetComponent<TITAN>().hasDie)
                            {
                                gameObject.GetComponent<TITAN>().hitEye();
                            }
                        }
                        else if (!PhotonNetwork.isMasterClient)
                        {
                            if (!gameObject.GetComponent<TITAN>().hasDie)
                            {
                                object[] objArray6 = {transform.root.gameObject.GetPhotonView().viewID};
                                gameObject.GetComponent<TITAN>().photonView
                                    .RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray6);
                            }
                        }
                        else if (!gameObject.GetComponent<TITAN>().hasDie)
                        {
                            gameObject.GetComponent<TITAN>()
                                .hitEyeRPC(transform.root.gameObject.GetPhotonView().viewID);
                        }

                        showCriticalHitFX(other.gameObject.transform.position);
                    }
                }
            }
            else if (other.gameObject.tag == "titanankle" && !currentHits.Contains(other.gameObject))
            {
                currentHits.Add(other.gameObject);
                var obj3 = other.gameObject.transform.root.gameObject;
                var vector10 = currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity -
                               obj3.rigidbody.velocity;
                var num9 = (int) (vector10.magnitude * 10f * scoreMulti);
                num9 = Mathf.Max(10, num9);
                if (obj3.GetComponent<TITAN>() != null &&
                    obj3.GetComponent<TITAN>().abnormalType != AbnormalType.TYPE_CRAWLER)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (!obj3.GetComponent<TITAN>().hasDie)
                        {
                            obj3.GetComponent<TITAN>().hitAnkle();
                        }
                    }
                    else
                    {
                        if (!PhotonNetwork.isMasterClient)
                        {
                            if (!obj3.GetComponent<TITAN>().hasDie)
                            {
                                object[] objArray7 = {transform.root.gameObject.GetPhotonView().viewID};
                                obj3.GetComponent<TITAN>().photonView
                                    .RPC("hitAnkleRPC", PhotonTargets.MasterClient, objArray7);
                            }
                        }
                        else if (!obj3.GetComponent<TITAN>().hasDie)
                        {
                            obj3.GetComponent<TITAN>().hitAnkle();
                        }

                        showCriticalHitFX(other.gameObject.transform.position);
                    }
                }
                else if (obj3.GetComponent<FEMALE_TITAN>() != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (other.gameObject.name == "ankleR")
                        {
                            if (obj3.GetComponent<FEMALE_TITAN>() != null && !obj3.GetComponent<FEMALE_TITAN>().hasDie)
                            {
                                obj3.GetComponent<FEMALE_TITAN>().hitAnkleR(num9);
                            }
                        }
                        else if (obj3.GetComponent<FEMALE_TITAN>() != null && !obj3.GetComponent<FEMALE_TITAN>().hasDie)
                        {
                            obj3.GetComponent<FEMALE_TITAN>().hitAnkleL(num9);
                        }
                    }
                    else if (other.gameObject.name == "ankleR")
                    {
                        if (!PhotonNetwork.isMasterClient)
                        {
                            if (!obj3.GetComponent<FEMALE_TITAN>().hasDie)
                            {
                                object[] objArray8 = {transform.root.gameObject.GetPhotonView().viewID, num9};
                                obj3.GetComponent<FEMALE_TITAN>().photonView.RPC("hitAnkleRRPC",
                                    PhotonTargets.MasterClient, objArray8);
                            }
                        }
                        else if (!obj3.GetComponent<FEMALE_TITAN>().hasDie)
                        {
                            obj3.GetComponent<FEMALE_TITAN>()
                                .hitAnkleRRPC(transform.root.gameObject.GetPhotonView().viewID, num9);
                        }
                    }
                    else if (!PhotonNetwork.isMasterClient)
                    {
                        if (!obj3.GetComponent<FEMALE_TITAN>().hasDie)
                        {
                            object[] objArray9 = {transform.root.gameObject.GetPhotonView().viewID, num9};
                            obj3.GetComponent<FEMALE_TITAN>().photonView
                                .RPC("hitAnkleLRPC", PhotonTargets.MasterClient, objArray9);
                        }
                    }
                    else if (!obj3.GetComponent<FEMALE_TITAN>().hasDie)
                    {
                        obj3.GetComponent<FEMALE_TITAN>()
                            .hitAnkleLRPC(transform.root.gameObject.GetPhotonView().viewID, num9);
                    }

                    showCriticalHitFX(other.gameObject.transform.position);
                }
            }
        }
    }

    private void showCriticalHitFX(Vector3 position)
    {
        GameObject obj2;
        currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(0.2f, 0.3f);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            obj2 = PhotonNetwork.Instantiate("redCross1", transform.position, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        else
        {
            obj2 = (GameObject) Instantiate(Resources.Load("redCross1"));
        }

        obj2.transform.position = position;
    }

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (!transform.root.gameObject.GetPhotonView().isMine)
            {
                enabled = false;
                return;
            }

            if (transform.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null)
            {
                viewID = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID;
                ownerName = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().titanName;
                myTeam = PhotonView.Find(viewID).gameObject.GetComponent<HERO>().myTeam;
            }
        }
        else
        {
            myTeam = GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.GetComponent<HERO>()
                .myTeam;
        }

        active_me = true;
        count = 0;
        currentCamera = GGM.Caching.GameObjectCache.Find("MainCamera");
    }
}