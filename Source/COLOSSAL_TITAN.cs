using System;
using System.Collections;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;
using Random = UnityEngine.Random;

public class COLOSSAL_TITAN : MonoBehaviour
{
    private string actionName;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private bool attackChkOnce;
    private int attackCount;
    private int attackPattern = -1;
    public GameObject bottomObject;
    private Transform checkHitCapsuleEnd;
    private Vector3 checkHitCapsuleEndOld;
    private float checkHitCapsuleR;
    private Transform checkHitCapsuleStart;
    public GameObject door_broken;
    public GameObject door_closed;
    public bool hasDie;
    public bool hasspawn;
    public GameObject healthLabel;
    public float healthTime;
    private bool isSteamNeed;
    public float lagMax;
    public int maxHealth;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public float myDistance;
    public GameObject myHero;
    public int NapeArmor = 10000;
    public int NapeArmorTotal = 10000;
    public GameObject neckSteamObject;
    public float size;
    private string state = "idle";
    public GameObject sweepSmokeObject;
    private float waitTime = 2f;

    private void attack_sweep( string type = "")
    {
        callTitanHAHA();
        state = "attack_sweep";
        attackAnimation = "sweep" + type;
        attackCheckTimeA = 0.4f;
        attackCheckTimeB = 0.57f;
        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        checkHitCapsuleR = 20f;
        crossFade("attack_" + attackAnimation, 0.1f);
        attackChkOnce = false;
        sweepSmokeObject.GetComponent<ParticleSystem>().enableEmission = true;
        sweepSmokeObject.GetComponent<ParticleSystem>().Play();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType != NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("startSweepSmoke", PhotonTargets.Others);
            }
        }
    }

    private void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    public void beTauntedBy(GameObject target, float tauntTime)
    {
    }

    public void blowPlayer(GameObject player, Transform neck)
    {
        var vector = -(neck.position + transform.forward * 50f - player.transform.position);
        var num = 20f;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            player.GetComponent<HERO>().blowAway(vector.normalized * num + Vector3.up * 1f);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            object[] parameters = { vector.normalized * num + Vector3.up * 1f };
            player.GetComponent<HERO>().photonView.RPC("blowAway", PhotonTargets.All, parameters);
        }
    }

    private void callTitan( bool special = false)
    {
        if (special || GameObject.FindGameObjectsWithTag("titan").Length <= 6)
        {
            GameObject obj4;
            var objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
            var list = new ArrayList();
            foreach (var obj2 in objArray)
            {
                if (obj2.transform.parent.name == "titanRespawnCT")
                {
                    list.Add(obj2);
                }
            }
            var obj3 = (GameObject) list[Random.Range(0, list.Count)];
            string[] strArray = { "TITAN_VER3.1" };
            if (FengGameManagerMKII.LAN)
            {
                obj4 = (GameObject) Network.Instantiate(Resources.Load(strArray[Random.Range(0, strArray.Length)]), obj3.transform.position, obj3.transform.rotation, 0);
            }
            else
            {
                obj4 = PhotonNetwork.Instantiate(strArray[Random.Range(0, strArray.Length)], obj3.transform.position, obj3.transform.rotation, 0);
            }
            if (special)
            {
                var objArray3 = GameObject.FindGameObjectsWithTag("route");
                var route = objArray3[Random.Range(0, objArray3.Length)];
                while (route.name != "routeCT")
                {
                    route = objArray3[Random.Range(0, objArray3.Length)];
                }
                obj4.GetComponent<TITAN>().setRoute(route);
                obj4.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
                obj4.GetComponent<TITAN>().activeRad = 0;
                obj4.GetComponent<TITAN>().toCheckPoint((Vector3) obj4.GetComponent<TITAN>().checkPoints[0], 10f);
            }
            else
            {
                var num2 = 0.7f;
                var num3 = 0.7f;
                if (IN_GAME_MAIN_CAMERA.difficulty != 0)
                {
                    if (IN_GAME_MAIN_CAMERA.difficulty == 1)
                    {
                        num2 = 0.4f;
                        num3 = 0.7f;
                    }
                    else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
                    {
                        num2 = -1f;
                        num3 = 0.7f;
                    }
                }
                if (GameObject.FindGameObjectsWithTag("titan").Length == 5)
                {
                    obj4.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                }
                else if (Random.Range(0f, 1f) >= num2)
                {
                    if (Random.Range(0f, 1f) < num3)
                    {
                        obj4.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                    }
                    else
                    {
                        obj4.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, false);
                    }
                }
                obj4.GetComponent<TITAN>().activeRad = 200;
            }
            if (FengGameManagerMKII.LAN)
            {
                var obj6 = (GameObject) Network.Instantiate(Resources.Load("FX/FXtitanSpawn"), obj4.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
                obj6.transform.localScale = obj4.transform.localScale;
            }
            else
            {
                PhotonNetwork.Instantiate("FX/FXtitanSpawn", obj4.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = obj4.transform.localScale;
            }
        }
    }

    private void callTitanHAHA()
    {
        attackCount++;
        var num = 4;
        var num2 = 7;
        if (IN_GAME_MAIN_CAMERA.difficulty != 0)
        {
            if (IN_GAME_MAIN_CAMERA.difficulty == 1)
            {
                num = 4;
                num2 = 6;
            }
            else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
            {
                num = 3;
                num2 = 5;
            }
        }
        if (attackCount % num == 0)
        {
            callTitan();
        }
        if (NapeArmor < NapeArmorTotal * 0.3)
        {
            if (attackCount % (int) (num2 * 0.5f) == 0)
            {
                callTitan(true);
            }
        }
        else if (attackCount % num2 == 0)
        {
            callTitan(true);
        }
    }

    [RPC]
    private void changeDoor()
    {
        door_broken.SetActive(true);
        door_closed.SetActive(false);
    }

    private RaycastHit[] checkHitCapsule(Vector3 start, Vector3 end, float r)
    {
        return Physics.SphereCastAll(start, r, end - start, Vector3.Distance(start, end));
    }

    private GameObject checkIfHitHand(Transform hand)
    {
        var num = 30f;
        foreach (var collider in Physics.OverlapSphere(hand.GetComponent<SphereCollider>().transform.position, num + 1f))
        {
            if (collider.transform.root.tag == "Player")
            {
                var gameObject = collider.transform.root.gameObject;
                if (gameObject.GetComponent<TITAN_EREN>() != null)
                {
                    if (!gameObject.GetComponent<TITAN_EREN>().isHit)
                    {
                        gameObject.GetComponent<TITAN_EREN>().hitByTitan();
                    }
                    return gameObject;
                }
                if (gameObject.GetComponent<HERO>() != null && !gameObject.GetComponent<HERO>().isInvincible())
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    private void crossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
        if (!FengGameManagerMKII.LAN && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            object[] parameters = { aniName, time };
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    private void findNearestHero()
    {
        myHero = getNearestHero();
    }

    private GameObject getNearestHero()
    {
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        foreach (var obj3 in objArray)
        {
            if ((obj3.GetComponent<HERO>() == null || !obj3.GetComponent<HERO>().HasDied()) && (obj3.GetComponent<TITAN_EREN>() == null || !obj3.GetComponent<TITAN_EREN>().hasDied))
            {
                var num3 = Mathf.Sqrt((obj3.transform.position.x - transform.position.x) * (obj3.transform.position.x - transform.position.x) + (obj3.transform.position.z - transform.position.z) * (obj3.transform.position.z - transform.position.z));
                if (obj3.transform.position.y - transform.position.y < 450f && num3 < positiveInfinity)
                {
                    obj2 = obj3;
                    positiveInfinity = num3;
                }
            }
        }
        return obj2;
    }

    private void idle()
    {
        state = "idle";
        crossFade("idle", 0.2f);
    }

    private void kick()
    {
        state = "kick";
        actionName = "attack_kick_wall";
        attackCheckTime = 0.64f;
        attackChkOnce = false;
        crossFade(actionName, 0.1f);
    }

    private void killPlayer(GameObject hitHero)
    {
        if (hitHero != null)
        {
            var position = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!hitHero.GetComponent<HERO>().HasDied())
                {
                    hitHero.GetComponent<HERO>().die((hitHero.transform.position - position) * 15f * 4f, false);
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (FengGameManagerMKII.LAN)
                {
                    if (!hitHero.GetComponent<HERO>().HasDied())
                    {
                        hitHero.GetComponent<HERO>().markDie();
                    }
                }
                else if (!hitHero.GetComponent<HERO>().HasDied())
                {
                    hitHero.GetComponent<HERO>().markDie();
                    object[] parameters = { (hitHero.transform.position - position) * 15f * 4f, false, -1, "Colossal Titan", true };
                    hitHero.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, parameters);
                }
            }
        }
    }

    [RPC]
    public void labelRPC(int health, int maxHealth)
    {
        if (health < 0)
        {
            if (healthLabel != null)
            {
                Destroy(healthLabel);
            }
        }
        else
        {
            if (healthLabel == null)
            {
                healthLabel = (GameObject) Instantiate(Resources.Load("UI/LabelNameOverHead"));
                healthLabel.name = "LabelNameOverHead";
                healthLabel.transform.parent = transform;
                healthLabel.transform.localPosition = new Vector3(0f, 430f, 0f);
                var a = 15f;
                if (size > 0f && size < 1f)
                {
                    a = 15f / size;
                    a = Mathf.Min(a, 100f);
                }
                healthLabel.transform.localScale = new Vector3(a, a, a);
            }
            var str = "[7FFF00]";
            var num2 = health / (float) maxHealth;
            if (num2 < 0.75f && num2 >= 0.5f)
            {
                str = "[f2b50f]";
            }
            else if (num2 < 0.5f && num2 >= 0.25f)
            {
                str = "[ff8100]";
            }
            else if (num2 < 0.25f)
            {
                str = "[ff3333]";
            }
            healthLabel.GetComponent<UILabel>().text = str + Convert.ToString(health);
        }
    }

    public void loadskin()
    {
        if (PhotonNetwork.isMasterClient && (int) FengGameManagerMKII.settings[1] == 1)
        {
            photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, (string) FengGameManagerMKII.settings[67]);
        }
    }

    public IEnumerator loadskinE(string url)
    {
        while (!hasspawn)
        {
            yield return null;
        }
        var mipmap = true;
        var iteratorVariable1 = false;
        if ((int) FengGameManagerMKII.settings[63] == 1)
        {
            mipmap = false;
        }
        foreach (var iteratorVariable2 in GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable2.name.Contains("hair"))
            {
                if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
                {
                    var link = new WWW(url);
                    yield return link;
                    var iteratorVariable4 = RCextensions.loadimage(link, mipmap, 1000000);
                    link.Dispose();
                    if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable2.material.mainTexture = iteratorVariable4;
                        FengGameManagerMKII.linkHash[2].Add(url, iteratorVariable2.material);
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                    }
                    else
                    {
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                    }
                }
                else
                {
                    iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][url];
                }
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.FGM.unloadAssets();
        }
    }

    [RPC]
    public void loadskinRPC(string url)
    {
        if ((int) FengGameManagerMKII.settings[1] == 1 && (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg")))
        {
            StartCoroutine(loadskinE(url));
        }
    }

    private void neckSteam()
    {
        neckSteamObject.GetComponent<ParticleSystem>().Stop();
        neckSteamObject.GetComponent<ParticleSystem>().Play();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType != NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("startNeckSteam", PhotonTargets.Others);
            }
        }
        isSteamNeed = true;
        var neck = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        var radius = 30f;
        foreach (var collider in Physics.OverlapSphere(neck.transform.position - transform.forward * 10f, radius))
        {
            if (collider.transform.root.tag == "Player")
            {
                var gameObject = collider.transform.root.gameObject;
                if (gameObject.GetComponent<TITAN_EREN>() == null && gameObject.GetComponent<HERO>() != null)
                {
                    blowPlayer(gameObject, neck);
                }
            }
        }
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
    }

    [RPC]
    public void netDie()
    {
        if (!hasDie)
        {
            hasDie = true;
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        animation.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
    }

    private void OnDestroy()
    {
        if (GGM.Caching.GameObjectCache.Find("MultiplayerManager") != null)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().removeCT(this);
        }
    }

    private void playAnimation(string aniName)
    {
        animation.Play(aniName);
        if (!FengGameManagerMKII.LAN && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            object[] parameters = { aniName };
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if (!FengGameManagerMKII.LAN && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            object[] parameters = { aniName, normalizedTime };
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        playsoundRPC(sndname);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            if (FengGameManagerMKII.LAN)
            {
                if (Network.peerType != NetworkPeerType.Server)
                {
                }
            }
            else if (PhotonNetwork.isMasterClient)
            {
                object[] parameters = { sndname };
                photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
            }
        }
    }

    [RPC]
    private void playsoundRPC(string sndname)
    {
        transform.Find(sndname).GetComponent<AudioSource>().Play();
    }

    [RPC]
    private void removeMe()
    {
        Destroy(gameObject);
    }

    [RPC]
    public void setSize(float size, PhotonMessageInfo info)
    {
        size = Mathf.Clamp(size, 0.1f, 50f);
        if (info.sender.isMasterClient)
        {
            var transform = this.transform;
            transform.localScale = transform.localScale * (size * 0.05f);
            this.size = size;
        }
    }

    private void slap(string type)
    {
        callTitanHAHA();
        state = "slap";
        attackAnimation = type;
        if (type == "r1" || type == "r2")
        {
            checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        }
        if (type == "l1" || type == "l2")
        {
            checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        }
        attackCheckTime = 0.57f;
        attackChkOnce = false;
        crossFade("attack_slap_" + attackAnimation, 0.1f);
    }

    private void Start()
    {
        startMain();
        size = 20f;
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.black, false, true);
        }
        if (photonView.isMine)
        {
            if (RCSettings.sizeMode > 0)
            {
                var sizeLower = RCSettings.sizeLower;
                var sizeUpper = RCSettings.sizeUpper;
                size = Random.Range(sizeLower, sizeUpper);
                photonView.RPC("setSize", PhotonTargets.AllBuffered, size);
            }
            lagMax = 150f + size * 3f;
            healthTime = 0f;
            maxHealth = NapeArmor;
            if (RCSettings.healthMode > 0)
            {
                maxHealth = NapeArmor = Random.Range(RCSettings.healthLower, RCSettings.healthUpper);
            }
            if (NapeArmor > 0)
            {
                photonView.RPC("labelRPC", PhotonTargets.AllBuffered, NapeArmor, maxHealth);
            }
            loadskin();
        }
        hasspawn = true;
    }

    private void startMain()
    {
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().addCT(this);
        if (myHero == null)
        {
            findNearestHero();
        }
        name = "COLOSSAL_TITAN";
        NapeArmor = 1000;
        var flag = false;
        if (LevelInfo.getInfo(FengGameManagerMKII.level).respawnMode == RespawnMode.NEVER)
        {
            flag = true;
        }
        if (IN_GAME_MAIN_CAMERA.difficulty == 0)
        {
            NapeArmor = !flag ? 5000 : 2000;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 1)
        {
            NapeArmor = !flag ? 8000 : 3500;
            foreach (AnimationState animationState in animation)
            {
                animationState.speed = 1.02f;
            }
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
        {
            NapeArmor = !flag ? 12000 : 5000;
            foreach (AnimationState animationState2 in animation)
            {
                animationState2.speed = 1.05f;
            }
        }
        NapeArmorTotal = NapeArmor;
        state = "wait";
        transform.position += -Vector3.up * 10000f;
        if (FengGameManagerMKII.LAN)
        {
            GetComponent<PhotonView>().enabled = false;
        }
        else
        {
            GetComponent<NetworkView>().enabled = false;
        }
        door_broken = GGM.Caching.GameObjectCache.Find("door_broke");
        door_closed = GGM.Caching.GameObjectCache.Find("door_fine");
        door_broken.SetActive(false);
        door_closed.SetActive(true);
    }


    [RPC]
    private void startNeckSteam()
    {
        neckSteamObject.GetComponent<ParticleSystem>().Stop();
        neckSteamObject.GetComponent<ParticleSystem>().Play();
    }

    [RPC]
    private void startSweepSmoke()
    {
        sweepSmokeObject.GetComponent<ParticleSystem>().enableEmission = true;
        sweepSmokeObject.GetComponent<ParticleSystem>().Play();
    }

    private void steam()
    {
        callTitanHAHA();
        state = "steam";
        actionName = "attack_steam";
        attackCheckTime = 0.45f;
        crossFade(actionName, 0.1f);
        attackChkOnce = false;
    }

    [RPC]
    private void stopSweepSmoke()
    {
        sweepSmokeObject.GetComponent<ParticleSystem>().enableEmission = false;
        sweepSmokeObject.GetComponent<ParticleSystem>().Stop();
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        var view = PhotonView.Find(viewID);
        if (view != null)
        {
            var vector = view.gameObject.transform.position - transform.transform.position;
            if (vector.magnitude < lagMax && healthTime <= 0f)
            {
                if (speed >= RCSettings.damageMode)
                {
                    NapeArmor -= speed;
                }
                if (maxHealth > 0f)
                {
                    photonView.RPC("labelRPC", PhotonTargets.AllBuffered, NapeArmor, maxHealth);
                }
                neckSteam();
                if (NapeArmor <= 0)
                {
                    NapeArmor = 0;
                    if (!hasDie)
                    {
                        if (FengGameManagerMKII.LAN)
                        {
                            netDie();
                        }
                        else
                        {
                            photonView.RPC("netDie", PhotonTargets.OthersBuffered);
                            netDie();
                            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().titanGetKill(view.owner, speed, name);
                        }
                    }
                }
                else
                {
                    GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().sendKillInfo(false, (string) view.owner.customProperties[PhotonPlayerProperty.name], true, "Colossal Titan's neck", speed);
                    object[] parameters = { speed };
                    GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("netShowDamage", view.owner, parameters);
                }
                healthTime = 0.2f;
            }
        }
    }
    
    public void update()
    {
        healthTime -= Time.deltaTime;
        updateLabel();
        if (state != "null")
        {
            if (state == "wait")
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0f)
                {
                    transform.position = new Vector3(30f, 0f, 784f);
                    Instantiate(Resources.Load("FX/ThunderCT"), transform.position + Vector3.up * 350f, Quaternion.Euler(270f, 0f, 0f));
                    Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().flashBlind();
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        idle();
                    }
                    else if (!FengGameManagerMKII.LAN ? photonView.isMine : networkView.isMine)
                    {
                        idle();
                    }
                    else
                    {
                        state = "null";
                    }
                }
            }
            else if (state != "idle")
            {
                if (state == "attack_sweep")
                {
                    if (attackCheckTimeA != 0f && (animation["attack_" + attackAnimation].normalizedTime >= attackCheckTimeA && animation["attack_" + attackAnimation].normalizedTime <= attackCheckTimeB || !attackChkOnce && animation["attack_" + attackAnimation].normalizedTime >= attackCheckTimeA))
                    {
                        if (!attackChkOnce)
                        {
                            attackChkOnce = true;
                        }
                        foreach (var hit in checkHitCapsule(checkHitCapsuleStart.position, checkHitCapsuleEnd.position, checkHitCapsuleR))
                        {
                            var gameObject = hit.collider.gameObject;
                            if (gameObject.tag == "Player")
                            {
                                killPlayer(gameObject);
                            }
                            if (gameObject.tag == "erenHitbox" && attackAnimation == "combo_3" && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && (!FengGameManagerMKII.LAN ? PhotonNetwork.isMasterClient : Network.isServer))
                            {
                                gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByFTByServer(3);
                            }
                        }
                        foreach (var hit2 in checkHitCapsule(checkHitCapsuleEndOld, checkHitCapsuleEnd.position, checkHitCapsuleR))
                        {
                            var hitHero = hit2.collider.gameObject;
                            if (hitHero.tag == "Player")
                            {
                                killPlayer(hitHero);
                            }
                        }
                        checkHitCapsuleEndOld = checkHitCapsuleEnd.position;
                    }
                    if (animation["attack_" + attackAnimation].normalizedTime >= 1f)
                    {
                        sweepSmokeObject.GetComponent<ParticleSystem>().enableEmission = false;
                        sweepSmokeObject.GetComponent<ParticleSystem>().Stop();
                        if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || FengGameManagerMKII.LAN))
                        {
                            photonView.RPC("stopSweepSmoke", PhotonTargets.Others);
                        }
                        findNearestHero();
                        idle();
                        playAnimation("idle");
                    }
                }
                else if (state == "kick")
                {
                    if (!attackChkOnce && animation[actionName].normalizedTime >= attackCheckTime)
                    {
                        attackChkOnce = true;
                        door_broken.SetActive(true);
                        door_closed.SetActive(false);
                        if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || FengGameManagerMKII.LAN))
                        {
                            photonView.RPC("changeDoor", PhotonTargets.OthersBuffered);
                        }
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            if (FengGameManagerMKII.LAN)
                            {
                                Network.Instantiate(Resources.Load("FX/boom1_CT_KICK"), transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(270f, 0f, 0f), 0);
                                Network.Instantiate(Resources.Load("rock"), transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(0f, 0f, 0f), 0);
                            }
                            else
                            {
                                PhotonNetwork.Instantiate("FX/boom1_CT_KICK", transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(270f, 0f, 0f), 0);
                                PhotonNetwork.Instantiate("rock", transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(0f, 0f, 0f), 0);
                            }
                        }
                        else
                        {
                            Instantiate(Resources.Load("FX/boom1_CT_KICK"), transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(270f, 0f, 0f));
                            Instantiate(Resources.Load("rock"), transform.position + transform.forward * 120f + transform.right * 30f, Quaternion.Euler(0f, 0f, 0f));
                        }
                    }
                    if (animation[actionName].normalizedTime >= 1f)
                    {
                        findNearestHero();
                        idle();
                        playAnimation("idle");
                    }
                }
                else if (state == "slap")
                {
                    if (!attackChkOnce && animation["attack_slap_" + attackAnimation].normalizedTime >= attackCheckTime)
                    {
                        GameObject obj4;
                        attackChkOnce = true;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            if (FengGameManagerMKII.LAN)
                            {
                                obj4 = (GameObject) Network.Instantiate(Resources.Load("FX/boom1"), checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f), 0);
                            }
                            else
                            {
                                obj4 = PhotonNetwork.Instantiate("FX/boom1", checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f), 0);
                            }
                            if (obj4.GetComponent<EnemyfxIDcontainer>() != null)
                            {
                                obj4.GetComponent<EnemyfxIDcontainer>().titanName = name;
                            }
                        }
                        else
                        {
                            obj4 = (GameObject) Instantiate(Resources.Load("FX/boom1"), checkHitCapsuleStart.position, Quaternion.Euler(270f, 0f, 0f));
                        }
                        obj4.transform.localScale = new Vector3(5f, 5f, 5f);
                    }
                    if (animation["attack_slap_" + attackAnimation].normalizedTime >= 1f)
                    {
                        findNearestHero();
                        idle();
                        playAnimation("idle");
                    }
                }
                else if (state == "steam")
                {
                    if (!attackChkOnce && animation[actionName].normalizedTime >= attackCheckTime)
                    {
                        attackChkOnce = true;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            if (FengGameManagerMKII.LAN)
                            {
                                Network.Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.up * 185f, Quaternion.Euler(270f, 0f, 0f), 0);
                                Network.Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.up * 303f, Quaternion.Euler(270f, 0f, 0f), 0);
                                Network.Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.up * 50f, Quaternion.Euler(270f, 0f, 0f), 0);
                            }
                            else
                            {
                                PhotonNetwork.Instantiate("FX/colossal_steam", transform.position + transform.up * 185f, Quaternion.Euler(270f, 0f, 0f), 0);
                                PhotonNetwork.Instantiate("FX/colossal_steam", transform.position + transform.up * 303f, Quaternion.Euler(270f, 0f, 0f), 0);
                                PhotonNetwork.Instantiate("FX/colossal_steam", transform.position + transform.up * 50f, Quaternion.Euler(270f, 0f, 0f), 0);
                            }
                        }
                        else
                        {
                            Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.forward * 185f, Quaternion.Euler(270f, 0f, 0f));
                            Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.forward * 303f, Quaternion.Euler(270f, 0f, 0f));
                            Instantiate(Resources.Load("FX/colossal_steam"), transform.position + transform.forward * 50f, Quaternion.Euler(270f, 0f, 0f));
                        }
                    }
                    if (animation[actionName].normalizedTime >= 1f)
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            if (FengGameManagerMKII.LAN)
                            {
                                Network.Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.up * 185f, Quaternion.Euler(270f, 0f, 0f), 0);
                                Network.Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.up * 303f, Quaternion.Euler(270f, 0f, 0f), 0);
                                Network.Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.up * 50f, Quaternion.Euler(270f, 0f, 0f), 0);
                            }
                            else
                            {
                                var obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", transform.position + transform.up * 185f, Quaternion.Euler(270f, 0f, 0f), 0);
                                if (obj5.GetComponent<EnemyfxIDcontainer>() != null)
                                {
                                    obj5.GetComponent<EnemyfxIDcontainer>().titanName = name;
                                }
                                obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", transform.position + transform.up * 303f, Quaternion.Euler(270f, 0f, 0f), 0);
                                if (obj5.GetComponent<EnemyfxIDcontainer>() != null)
                                {
                                    obj5.GetComponent<EnemyfxIDcontainer>().titanName = name;
                                }
                                obj5 = PhotonNetwork.Instantiate("FX/colossal_steam_dmg", transform.position + transform.up * 50f, Quaternion.Euler(270f, 0f, 0f), 0);
                                if (obj5.GetComponent<EnemyfxIDcontainer>() != null)
                                {
                                    obj5.GetComponent<EnemyfxIDcontainer>().titanName = name;
                                }
                            }
                        }
                        else
                        {
                            Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.forward * 185f, Quaternion.Euler(270f, 0f, 0f));
                            Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.forward * 303f, Quaternion.Euler(270f, 0f, 0f));
                            Instantiate(Resources.Load("FX/colossal_steam_dmg"), transform.position + transform.forward * 50f, Quaternion.Euler(270f, 0f, 0f));
                        }
                        if (hasDie)
                        {
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                Destroy(gameObject);
                            }
                            else if (FengGameManagerMKII.LAN)
                            {
                                if (!networkView.isMine)
                                {
                                }
                            }
                            else if (PhotonNetwork.isMasterClient)
                            {
                                PhotonNetwork.Destroy(photonView);
                            }
                            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameWin();
                        }
                        findNearestHero();
                        idle();
                        playAnimation("idle");
                    }
                }
                else if (state == string.Empty)
                {
                }
            }
            else if (attackPattern == -1)
            {
                slap("r1");
                attackPattern++;
            }
            else if (attackPattern == 0)
            {
                attack_sweep(string.Empty);
                attackPattern++;
            }
            else if (attackPattern == 1)
            {
                steam();
                attackPattern++;
            }
            else if (attackPattern == 2)
            {
                kick();
                attackPattern++;
            }
            else if (isSteamNeed || hasDie)
            {
                steam();
                isSteamNeed = false;
            }
            else if (myHero == null)
            {
                findNearestHero();
            }
            else
            {
                var vector = myHero.transform.position - transform.position;
                var current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                var f = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
                myDistance = Mathf.Sqrt((myHero.transform.position.x - transform.position.x) * (myHero.transform.position.x - transform.position.x) + (myHero.transform.position.z - transform.position.z) * (myHero.transform.position.z - transform.position.z));
                var num4 = myHero.transform.position.y - transform.position.y;
                if (myDistance < 85f && Random.Range(0, 100) < 5)
                {
                    steam();
                }
                else
                {
                    if (num4 > 310f && num4 < 350f)
                    {
                        if (Vector3.Distance(myHero.transform.position, transform.Find("APL1").position) < 40f)
                        {
                            slap("l1");
                            return;
                        }
                        if (Vector3.Distance(myHero.transform.position, transform.Find("APL2").position) < 40f)
                        {
                            slap("l2");
                            return;
                        }
                        if (Vector3.Distance(myHero.transform.position, transform.Find("APR1").position) < 40f)
                        {
                            slap("r1");
                            return;
                        }
                        if (Vector3.Distance(myHero.transform.position, transform.Find("APR2").position) < 40f)
                        {
                            slap("r2");
                            return;
                        }
                        if (myDistance < 150f && Mathf.Abs(f) < 80f)
                        {
                            attack_sweep(string.Empty);
                            return;
                        }
                    }
                    if (num4 < 300f && Mathf.Abs(f) < 80f && myDistance < 85f)
                    {
                        attack_sweep("_vertical");
                    }
                    else
                    {
                        switch (Random.Range(0, 7))
                        {
                            case 0:
                                slap("l1");
                                break;

                            case 1:
                                slap("l2");
                                break;

                            case 2:
                                slap("r1");
                                break;

                            case 3:
                                slap("r2");
                                break;

                            case 4:
                                attack_sweep(string.Empty);
                                break;

                            case 5:
                                attack_sweep("_vertical");
                                break;

                            case 6:
                                steam();
                                break;
                        }
                    }
                }
            }
        }
    }

    public void updateLabel()
    {
        if (healthLabel != null && healthLabel.GetComponent<UILabel>().isVisible)
        {
            healthLabel.transform.LookAt(2f * healthLabel.transform.position - Camera.main.transform.position);
        }
    }

}

