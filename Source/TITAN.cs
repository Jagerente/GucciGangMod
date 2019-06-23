using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using MonoBehaviour = Photon.MonoBehaviour;
using Random = UnityEngine.Random;

public class TITAN : MonoBehaviour
{
    private static Dictionary<string, int> f__switchmap7;
    private Vector3 abnorma_jump_bite_horizon_v;
    public AbnormalType abnormalType;
    public int activeRad = 0x7fffffff;
    private float angle;
    public bool asClientLookTarget;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private int attackCount;
    public float attackDistance = 13f;
    private bool attacked;
    private float attackEndWait;
    public float attackWait = 1f;
    public Animation baseAnimation;
    public AudioSource baseAudioSource;
    public List<Collider> baseColliders;
    public Transform baseGameObjectTransform;
    public Rigidbody baseRigidBody;
    public Transform baseTransform;
    private float between2;
    public float chaseDistance = 80f;
    public ArrayList checkPoints = new ArrayList();
    public bool colliderEnabled;
    public TITAN_CONTROLLER controller;
    public GameObject currentCamera;
    private Transform currentGrabHand;
    public int currentHealth;
    private float desDeg;
    private float dieTime;
    public bool eye;
    private static Dictionary<string, int> fswitchmap5;
    private static Dictionary<string, int> fswitchmap6;
    private string fxName;
    private Vector3 fxPosition;
    private Quaternion fxRotation;
    private float getdownTime;
    private GameObject grabbedTarget;
    public GameObject grabTF;
    private float gravity = 120f;
    private bool grounded;
    public bool hasDie;
    private bool hasDieSteam;
    public bool hasExplode;
    public bool hasload;
    public bool hasSetLevel;
    public bool hasSpawn;
    private Transform head;
    private Vector3 headscale = Vector3.one;
    public GameObject healthLabel;
    public bool healthLabelEnabled;
    public float healthTime;
    private string hitAnimation;
    private float hitPause;
    public bool isAlarm;
    private bool isAttackMoveByCore;
    private bool isGrabHandLeft;
    public bool isHooked;
    public bool isLook;
    public float lagMax;
    private bool leftHandAttack;
    public GameObject mainMaterial;
    public int maxHealth;
    private float maxStamina = 320f;
    public float maxVelocityChange = 10f;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public FengGameManagerMKII MultiplayerManager;
    public int myDifficulty;
    public float myDistance;
    public GROUP myGroup = GROUP.T;
    public GameObject myHero;
    public float myLevel = 1f;
    public TitanTrigger myTitanTrigger;
    private Transform neck;
    private bool needFreshCorePosition;
    private string nextAttackAnimation;
    public bool nonAI;
    private bool nonAIcombo;
    private Vector3 oldCorePosition;
    private Quaternion oldHeadRotation;
    public PVPcheckPoint PVPfromCheckPt;
    private float random_run_time;
    private float rockInterval;
    public bool rockthrow;
    private string runAnimation;
    private float sbtime;
    public int skin;
    private Vector3 spawnPt;
    public float speed = 7f;
    private float stamina = 320f;
    private TitanState state;
    private int stepSoundPhase = 2;
    private bool stuck;
    private float stuckTime;
    private float stuckTurnAngle;
    private Vector3 targetCheckPt;
    private Quaternion targetHeadRotation;
    private float targetR;
    private float tauntTime;
    private GameObject throwRock;
    private string turnAnimation;
    private float turnDeg;
    private GameObject whoHasTauntMe;
  
    private void attack(string type)
    {
        state = TitanState.attack;
        attacked = false;
        isAlarm = true;
        if (attackAnimation == type)
        {
            attackAnimation = type;
            playAnimationAt("attack_" + type, 0f);
        }
        else
        {
            attackAnimation = type;
            playAnimationAt("attack_" + type, 0f);
        }
        nextAttackAnimation = null;
        fxName = null;
        isAttackMoveByCore = false;
        attackCheckTime = 0f;
        attackCheckTimeA = 0f;
        attackCheckTimeB = 0f;
        attackEndWait = 0f;
        fxRotation = Quaternion.Euler(270f, 0f, 0f);
        var key = type;
        if (key != null)
        {
            int num;
            if (fswitchmap6 == null)
            {
                var dictionary = new Dictionary<string, int>(0x16);
                dictionary.Add("abnormal_getup", 0);
                dictionary.Add("abnormal_jump", 1);
                dictionary.Add("combo_1", 2);
                dictionary.Add("combo_2", 3);
                dictionary.Add("combo_3", 4);
                dictionary.Add("front_ground", 5);
                dictionary.Add("kick", 6);
                dictionary.Add("slap_back", 7);
                dictionary.Add("slap_face", 8);
                dictionary.Add("stomp", 9);
                dictionary.Add("bite", 10);
                dictionary.Add("bite_l", 11);
                dictionary.Add("bite_r", 12);
                dictionary.Add("jumper_0", 13);
                dictionary.Add("crawler_jump_0", 14);
                dictionary.Add("anti_AE_l", 15);
                dictionary.Add("anti_AE_r", 0x10);
                dictionary.Add("anti_AE_low_l", 0x11);
                dictionary.Add("anti_AE_low_r", 0x12);
                dictionary.Add("quick_turn_l", 0x13);
                dictionary.Add("quick_turn_r", 20);
                dictionary.Add("throw", 0x15);
                fswitchmap6 = dictionary;
            }
            if (fswitchmap6.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        attackCheckTime = 0f;
                        fxName = string.Empty;
                        break;

                    case 1:
                        nextAttackAnimation = "abnormal_getup";
                        if (nonAI)
                        {
                            attackEndWait = 0f;
                        }
                        else
                        {
                            attackEndWait = myDifficulty <= 0 ? Random.Range(1f, 4f) : Random.Range(0f, 1f);
                        }
                        attackCheckTime = 0.75f;
                        fxName = "boom4";
                        fxRotation = Quaternion.Euler(270f, baseTransform.rotation.eulerAngles.y, 0f);
                        break;

                    case 2:
                        nextAttackAnimation = "combo_2";
                        attackCheckTimeA = 0.54f;
                        attackCheckTimeB = 0.76f;
                        nonAIcombo = false;
                        isAttackMoveByCore = true;
                        leftHandAttack = false;
                        break;

                    case 3:
                        if (!(abnormalType == AbnormalType.TYPE_PUNK || nonAI))
                        {
                            nextAttackAnimation = "combo_3";
                        }
                        attackCheckTimeA = 0.37f;
                        attackCheckTimeB = 0.57f;
                        nonAIcombo = false;
                        isAttackMoveByCore = true;
                        leftHandAttack = true;
                        break;

                    case 4:
                        nonAIcombo = false;
                        isAttackMoveByCore = true;
                        attackCheckTime = 0.21f;
                        fxName = "boom1";
                        break;

                    case 5:
                        fxName = "boom1";
                        attackCheckTime = 0.45f;
                        break;

                    case 6:
                        fxName = "boom5";
                        fxRotation = baseTransform.rotation;
                        attackCheckTime = 0.43f;
                        break;

                    case 7:
                        fxName = "boom3";
                        attackCheckTime = 0.66f;
                        break;

                    case 8:
                        fxName = "boom3";
                        attackCheckTime = 0.655f;
                        break;

                    case 9:
                        fxName = "boom2";
                        attackCheckTime = 0.42f;
                        break;

                    case 10:
                        fxName = "bite";
                        attackCheckTime = 0.6f;
                        break;

                    case 11:
                        fxName = "bite";
                        attackCheckTime = 0.4f;
                        break;

                    case 12:
                        fxName = "bite";
                        attackCheckTime = 0.4f;
                        break;

                    case 13:
                        abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 14:
                        abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 15:
                        attackCheckTimeA = 0.31f;
                        attackCheckTimeB = 0.4f;
                        leftHandAttack = true;
                        break;

                    case 0x10:
                        attackCheckTimeA = 0.31f;
                        attackCheckTimeB = 0.4f;
                        leftHandAttack = false;
                        break;

                    case 0x11:
                        attackCheckTimeA = 0.31f;
                        attackCheckTimeB = 0.4f;
                        leftHandAttack = true;
                        break;

                    case 0x12:
                        attackCheckTimeA = 0.31f;
                        attackCheckTimeB = 0.4f;
                        leftHandAttack = false;
                        break;

                    case 0x13:
                        attackCheckTimeA = 2f;
                        attackCheckTimeB = 2f;
                        isAttackMoveByCore = true;
                        break;

                    case 20:
                        attackCheckTimeA = 2f;
                        attackCheckTimeB = 2f;
                        isAttackMoveByCore = true;
                        break;

                    case 0x15:
                        isAlarm = true;
                        chaseDistance = 99999f;
                        break;
                }
            }
        }
        needFreshCorePosition = true;
    }

    private void Awake()
    {
        cache();
        baseRigidBody.freezeRotation = true;
        baseRigidBody.useGravity = false;
    }

    public void beLaughAttacked()
    {
        if (!hasDie && abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = { 0f };
                photonView.RPC("laugh", PhotonTargets.All, parameters);
            }
            else if (state == TitanState.idle || state == TitanState.turn || state == TitanState.chase)
            {
                laugh();
            }
        }
    }

    public void beTauntedBy(GameObject target, float tauntTime)
    {
        whoHasTauntMe = target;
        this.tauntTime = tauntTime;
        isAlarm = true;
    }

    public void cache()
    {
        baseAudioSource = transform.Find("snd_titan_foot").GetComponent<AudioSource>();
        baseAnimation = animation;
        baseTransform = transform;
        baseRigidBody = rigidbody;
        baseColliders = new List<Collider>();
        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            if (collider.name != "AABB")
            {
                baseColliders.Add(collider);
            }
        }
        var obj2 = new GameObject {
            name = "PlayerDetectorRC"
        };
        var collider2 = obj2.AddComponent<CapsuleCollider>();
        var component = baseTransform.Find("AABB").GetComponent<CapsuleCollider>();
        collider2.center = component.center;
        collider2.radius = Math.Abs(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").position.y - baseTransform.position.y);
        collider2.height = component.height * 1.2f;
        collider2.material = component.material;
        collider2.isTrigger = true;
        collider2.name = "PlayerDetectorRC";
        myTitanTrigger = obj2.AddComponent<TitanTrigger>();
        myTitanTrigger.isCollide = false;
        obj2.layer = 0x10;
        obj2.transform.parent = baseTransform.Find("AABB");
        obj2.transform.localPosition = new Vector3(0f, 0f, 0f);
        MultiplayerManager = FengGameManagerMKII.instance;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            baseGameObjectTransform = gameObject.transform;
        }
    }

    private void chase()
    {
        state = TitanState.chase;
        isAlarm = true;
        crossFade(runAnimation, 0.5f);
    }

    private GameObject checkIfHitCrawlerMouth(Transform head, float rad)
    {
        var num = rad * myLevel;
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj2.GetComponent<TITAN_EREN>() == null && (obj2.GetComponent<HERO>() == null || !obj2.GetComponent<HERO>().isInvincible()))
            {
                var num3 = obj2.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(obj2.transform.position + Vector3.up * num3, head.position - Vector3.up * 1.5f * myLevel) < num + num3)
                {
                    return obj2;
                }
            }
        }
        return null;
    }

    private GameObject checkIfHitHand(Transform hand)
    {
        var num = 2.4f * myLevel;
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
                }
                else if (gameObject.GetComponent<HERO>() != null && !gameObject.GetComponent<HERO>().isInvincible())
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    private GameObject checkIfHitHead(Transform head, float rad)
    {
        var num = rad * myLevel;
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj2.GetComponent<TITAN_EREN>() == null && (obj2.GetComponent<HERO>() == null || !obj2.GetComponent<HERO>().isInvincible()))
            {
                var num3 = obj2.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(obj2.transform.position + Vector3.up * num3, head.position + Vector3.up * 1.5f * myLevel) < num + num3)
                {
                    return obj2;
                }
            }
        }
        return null;
    }

    private void crossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            object[] parameters = { aniName, time };
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    public bool die()
    {
        if (hasDie)
        {
            return false;
        }
        hasDie = true;
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().oneTitanDown(string.Empty, false);
        dieAnimation();
        return true;
    }

    private void dieAnimation()
    {
        if (animation.IsPlaying("sit_idle") || animation.IsPlaying("sit_hit_eye"))
        {
            crossFade("sit_die", 0.1f);
        }
        else if (abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            crossFade("crawler_die", 0.2f);
        }
        else if (abnormalType == AbnormalType.NORMAL)
        {
            crossFade("die_front", 0.05f);
        }
        else if (animation.IsPlaying("attack_abnormal_jump") && animation["attack_abnormal_jump"].normalizedTime > 0.7f || animation.IsPlaying("attack_abnormal_getup") && animation["attack_abnormal_getup"].normalizedTime < 0.7f || animation.IsPlaying("tired"))
        {
            crossFade("die_ground", 0.2f);
        }
        else
        {
            crossFade("die_back", 0.05f);
        }
    }

    public void dieBlow(Vector3 attacker, float hitPauseTime)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            dieBlowFunc(attacker, hitPauseTime);
            if (GameObject.FindGameObjectsWithTag("titan").Length <= 1)
            {
                GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            }
        }
        else
        {
            object[] parameters = { attacker, hitPauseTime };
            photonView.RPC("dieBlowRPC", PhotonTargets.All, parameters);
        }
    }

    public void dieBlowFunc(Vector3 attacker, float hitPauseTime)
    {
        if (!hasDie)
        {
            transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - transform.position).eulerAngles.y, 0f);
            hasDie = true;
            hitAnimation = "die_blow";
            hitPause = hitPauseTime;
            playAnimation(hitAnimation);
            animation[hitAnimation].time = 0f;
            animation[hitAnimation].speed = 0f;
            needFreshCorePosition = true;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().oneTitanDown(string.Empty, false);
            if (photonView.isMine)
            {
                if (grabbedTarget != null)
                {
                    grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
                }
                if (nonAI)
                {
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
                    var propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.dead, true);
                    PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                    propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.deaths, (int) PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths] + 1);
                    PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                }
            }
        }
    }

    [RPC]
    private void dieBlowRPC(Vector3 attacker, float hitPauseTime)
    {
        if (photonView.isMine)
        {
            var vector = attacker - transform.position;
            if (vector.magnitude < 80f)
            {
                dieBlowFunc(attacker, hitPauseTime);
            }
        }
    }

    [RPC]
    public void DieByCannon(int viewID)
    {
        var view = PhotonView.Find(viewID);
        if (view != null)
        {
            var damage = 0;
            if (PhotonNetwork.isMasterClient)
            {
                OnTitanDie(view);
            }
            if (nonAI)
            {
                FengGameManagerMKII.instance.titanGetKill(view.owner, damage, (string) PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
            }
            else
            {
                FengGameManagerMKII.instance.titanGetKill(view.owner, damage, name);
            }
        }
        else
        {
            FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, speed);
        }
    }

    public void dieHeadBlow(Vector3 attacker, float hitPauseTime)
    {
        if (abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                dieHeadBlowFunc(attacker, hitPauseTime);
                if (GameObject.FindGameObjectsWithTag("titan").Length <= 1)
                {
                    GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
                }
            }
            else
            {
                object[] parameters = { attacker, hitPauseTime };
                photonView.RPC("dieHeadBlowRPC", PhotonTargets.All, parameters);
            }
        }
    }

    public void dieHeadBlowFunc(Vector3 attacker, float hitPauseTime)
    {
        if (!hasDie)
        {
            GameObject obj2;
            playSound("snd_titan_head_blow");
            transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - transform.position).eulerAngles.y, 0f);
            hasDie = true;
            hitAnimation = "die_headOff";
            hitPause = hitPauseTime;
            playAnimation(hitAnimation);
            animation[hitAnimation].time = 0f;
            animation[hitAnimation].speed = 0f;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().oneTitanDown(string.Empty, false);
            needFreshCorePosition = true;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                obj2 = PhotonNetwork.Instantiate("bloodExplore", head.position + Vector3.up * 1f * myLevel, Quaternion.Euler(270f, 0f, 0f), 0);
            }
            else
            {
                obj2 = (GameObject) Instantiate(Resources.Load("bloodExplore"), head.position + Vector3.up * 1f * myLevel, Quaternion.Euler(270f, 0f, 0f));
            }
            obj2.transform.localScale = transform.localScale;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                obj2 = PhotonNetwork.Instantiate("bloodsplatter", head.position, Quaternion.Euler(270f + neck.rotation.eulerAngles.x, neck.rotation.eulerAngles.y, neck.rotation.eulerAngles.z), 0);
            }
            else
            {
                obj2 = (GameObject) Instantiate(Resources.Load("bloodsplatter"), head.position, Quaternion.Euler(270f + neck.rotation.eulerAngles.x, neck.rotation.eulerAngles.y, neck.rotation.eulerAngles.z));
            }
            obj2.transform.localScale = transform.localScale;
            obj2.transform.parent = neck;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                obj2 = PhotonNetwork.Instantiate("FX/justSmoke", neck.position, Quaternion.Euler(270f, 0f, 0f), 0);
            }
            else
            {
                obj2 = (GameObject) Instantiate(Resources.Load("FX/justSmoke"), neck.position, Quaternion.Euler(270f, 0f, 0f));
            }
            obj2.transform.parent = neck;
            if (photonView.isMine)
            {
                if (grabbedTarget != null)
                {
                    grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
                }
                if (nonAI)
                {
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
                    currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
                    var propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.dead, true);
                    PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                    propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.deaths, (int) PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths] + 1);
                    PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                }
            }
        }
    }

    [RPC]
    private void dieHeadBlowRPC(Vector3 attacker, float hitPauseTime)
    {
        if (photonView.isMine)
        {
            var vector = attacker - neck.position;
            if (vector.magnitude < lagMax)
            {
                dieHeadBlowFunc(attacker, hitPauseTime);
            }
        }
    }

    private void eat()
    {
        state = TitanState.eat;
        attacked = false;
        if (isGrabHandLeft)
        {
            attackAnimation = "eat_l";
            crossFade("eat_l", 0.1f);
        }
        else
        {
            attackAnimation = "eat_r";
            crossFade("eat_r", 0.1f);
        }
    }

    private void eatSet(GameObject grabTarget)
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine) || !grabTarget.GetComponent<HERO>().isGrabbed)
        {
            grabToRight();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                photonView.RPC("grabToRight", PhotonTargets.Others);
                object[] parameters = { "grabbed" };
                grabTarget.GetPhotonView().RPC("netPlayAnimation", PhotonTargets.All, parameters);
                object[] objArray2 = { photonView.viewID, false };
                grabTarget.GetPhotonView().RPC("netGrabbed", PhotonTargets.All, objArray2);
            }
            else
            {
                grabTarget.GetComponent<HERO>().grabbed(gameObject, false);
                grabTarget.GetComponent<HERO>().animation.Play("grabbed");
            }
        }
    }

    private void eatSetL(GameObject grabTarget)
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine) || !grabTarget.GetComponent<HERO>().isGrabbed)
        {
            grabToLeft();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                photonView.RPC("grabToLeft", PhotonTargets.Others);
                object[] parameters = { "grabbed" };
                grabTarget.GetPhotonView().RPC("netPlayAnimation", PhotonTargets.All, parameters);
                object[] objArray2 = { photonView.viewID, true };
                grabTarget.GetPhotonView().RPC("netGrabbed", PhotonTargets.All, objArray2);
            }
            else
            {
                grabTarget.GetComponent<HERO>().grabbed(gameObject, true);
                grabTarget.GetComponent<HERO>().animation.Play("grabbed");
            }
        }
    }
    
    private bool executeAttack(string decidedAction)
    {
        var key = decidedAction;
        if (key != null)
        {
            int num;
            if (fswitchmap5 == null)
            {
                var dictionary = new Dictionary<string, int>(0x12);
                dictionary.Add("grab_ground_front_l", 0);
                dictionary.Add("grab_ground_front_r", 1);
                dictionary.Add("grab_ground_back_l", 2);
                dictionary.Add("grab_ground_back_r", 3);
                dictionary.Add("grab_head_front_l", 4);
                dictionary.Add("grab_head_front_r", 5);
                dictionary.Add("grab_head_back_l", 6);
                dictionary.Add("grab_head_back_r", 7);
                dictionary.Add("attack_abnormal_jump", 8);
                dictionary.Add("attack_combo", 9);
                dictionary.Add("attack_front_ground", 10);
                dictionary.Add("attack_kick", 11);
                dictionary.Add("attack_slap_back", 12);
                dictionary.Add("attack_slap_face", 13);
                dictionary.Add("attack_stomp", 14);
                dictionary.Add("attack_bite", 15);
                dictionary.Add("attack_bite_l", 0x10);
                dictionary.Add("attack_bite_r", 0x11);
                fswitchmap5 = dictionary;
            }
            if (fswitchmap5.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        grab("ground_front_l");
                        return true;

                    case 1:
                        grab("ground_front_r");
                        return true;

                    case 2:
                        grab("ground_back_l");
                        return true;

                    case 3:
                        grab("ground_back_r");
                        return true;

                    case 4:
                        grab("head_front_l");
                        return true;

                    case 5:
                        grab("head_front_r");
                        return true;

                    case 6:
                        grab("head_back_l");
                        return true;

                    case 7:
                        grab("head_back_r");
                        return true;

                    case 8:
                        attack("abnormal_jump");
                        return true;

                    case 9:
                        attack("combo_1");
                        return true;

                    case 10:
                        attack("front_ground");
                        return true;

                    case 11:
                        attack("kick");
                        return true;

                    case 12:
                        attack("slap_back");
                        return true;

                    case 13:
                        attack("slap_face");
                        return true;

                    case 14:
                        attack("stomp");
                        return true;

                    case 15:
                        attack("bite");
                        return true;

                    case 0x10:
                        attack("bite_l");
                        return true;

                    case 0x11:
                        attack("bite_r");
                        return true;
                }
            }
        }
        return false;
    }

    public void explode()
    {
        if (RCSettings.explodeMode > 0 && hasDie && dieTime >= 1f && !hasExplode)
        {
            var num = 0;
            var num2 = myLevel * 10f;
            if (abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                if (dieTime >= 2f)
                {
                    hasExplode = true;
                    num2 = 0f;
                    num = 1;
                }
            }
            else
            {
                num = 1;
                hasExplode = true;
            }
            if (num == 1)
            {
                var position = baseTransform.position + Vector3.up * num2;
                PhotonNetwork.Instantiate("FX/Thunder", position, Quaternion.Euler(270f, 0f, 0f), 0);
                PhotonNetwork.Instantiate("FX/boom1", position, Quaternion.Euler(270f, 0f, 0f), 0);
                foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (Vector3.Distance(obj2.transform.position, position) < RCSettings.explodeMode)
                    {
                        obj2.GetComponent<HERO>().markDie();
                        obj2.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, -1, "Server ");
                    }
                }
            }
        }
    }
    
    private void findNearestFacingHero()
    {
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        var position = baseTransform.position;
        var current = 0f;
        var num3 = abnormalType != AbnormalType.NORMAL ? 180f : 100f;
        var f = 0f;
        foreach (HERO hero in MultiplayerManager.getPlayers())
        {
            var gameObject = hero.gameObject;
            var num5 = Vector3.Distance(gameObject.transform.position, position);
            if (num5 < positiveInfinity)
            {
                var vector2 = gameObject.transform.position - baseTransform.position;
                current = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                f = -Mathf.DeltaAngle(current, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                if (Mathf.Abs(f) < num3)
                {
                    obj2 = gameObject;
                    positiveInfinity = num5;
                }
            }
        }
        if (obj2 != null)
        {
            var myHero = this.myHero;
            this.myHero = obj2;
            if (myHero != this.myHero && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
            {
                if (this.myHero == null)
                {
                    object[] parameters = { -1 };
                    photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
                }
                else
                {
                    object[] objArray3 = { this.myHero.GetPhotonView().viewID };
                    photonView.RPC("setMyTarget", PhotonTargets.Others, objArray3);
                }
            }
            tauntTime = 5f;
        }
    }
    private void findNearestHero()
    {
        var myHero = this.myHero;
        this.myHero = getNearestHero2();
        if (this.myHero != myHero && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            if (this.myHero == null)
            {
                object[] parameters = { -1 };
                photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
            }
            else
            {
                object[] objArray3 = { this.myHero.GetPhotonView().viewID };
                photonView.RPC("setMyTarget", PhotonTargets.Others, objArray3);
            }
        }
        oldHeadRotation = head.rotation;
    }

    private void FixedUpdate()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine))
        {
            baseRigidBody.AddForce(new Vector3(0f, -gravity * baseRigidBody.mass, 0f));
            if (needFreshCorePosition)
            {
                oldCorePosition = baseTransform.position - baseTransform.Find("Amarture/Core").position;
                needFreshCorePosition = false;
            }
            if (hasDie)
            {
                if (hitPause <= 0f && baseAnimation.IsPlaying("die_headOff"))
                {
                    var vector = baseTransform.position - baseTransform.Find("Amarture/Core").position - oldCorePosition;
                    baseRigidBody.velocity = vector / Time.deltaTime + Vector3.up * baseRigidBody.velocity.y;
                }
                oldCorePosition = baseTransform.position - baseTransform.Find("Amarture/Core").position;
            }
            else if (state == TitanState.attack && isAttackMoveByCore || state == TitanState.hit)
            {
                var vector2 = baseTransform.position - baseTransform.Find("Amarture/Core").position - oldCorePosition;
                baseRigidBody.velocity = vector2 / Time.deltaTime + Vector3.up * baseRigidBody.velocity.y;
                oldCorePosition = baseTransform.position - baseTransform.Find("Amarture/Core").position;
            }
            if (hasDie)
            {
                if (hitPause > 0f)
                {
                    hitPause -= Time.deltaTime;
                    if (hitPause <= 0f)
                    {
                        baseAnimation[hitAnimation].speed = 1f;
                        hitPause = 0f;
                    }
                }
                else if (baseAnimation.IsPlaying("die_blow"))
                {
                    if (baseAnimation["die_blow"].normalizedTime < 0.55f)
                    {
                        baseRigidBody.velocity = -baseTransform.forward * 300f + Vector3.up * baseRigidBody.velocity.y;
                    }
                    else if (baseAnimation["die_blow"].normalizedTime < 0.83f)
                    {
                        baseRigidBody.velocity = -baseTransform.forward * 100f + Vector3.up * baseRigidBody.velocity.y;
                    }
                    else
                    {
                        baseRigidBody.velocity = Vector3.up * baseRigidBody.velocity.y;
                    }
                }
            }
            else
            {
                if (nonAI && !IN_GAME_MAIN_CAMERA.isPausing && (state == TitanState.idle || state == TitanState.attack && attackAnimation == "jumper_1"))
                {
                    var zero = Vector3.zero;
                    if (controller.targetDirection != -874f)
                    {
                        var flag2 = false;
                        if (stamina < 5f)
                        {
                            flag2 = true;
                        }
                        else if (!(stamina >= 40f || baseAnimation.IsPlaying("run_abnormal") || baseAnimation.IsPlaying("crawler_run")))
                        {
                            flag2 = true;
                        }
                        if (controller.isWALKDown || flag2)
                        {
                            zero = baseTransform.forward * speed * Mathf.Sqrt(myLevel) * 0.2f;
                        }
                        else
                        {
                            zero = baseTransform.forward * speed * Mathf.Sqrt(myLevel);
                        }
                        baseGameObjectTransform.rotation = Quaternion.Lerp(baseGameObjectTransform.rotation, Quaternion.Euler(0f, controller.targetDirection, 0f), speed * 0.15f * Time.deltaTime);
                        if (state == TitanState.idle)
                        {
                            if (controller.isWALKDown || flag2)
                            {
                                if (abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    if (!baseAnimation.IsPlaying("crawler_run"))
                                    {
                                        crossFade("crawler_run", 0.1f);
                                    }
                                }
                                else if (!baseAnimation.IsPlaying("run_walk"))
                                {
                                    crossFade("run_walk", 0.1f);
                                }
                            }
                            else if (abnormalType == AbnormalType.TYPE_CRAWLER)
                            {
                                if (!baseAnimation.IsPlaying("crawler_run"))
                                {
                                    crossFade("crawler_run", 0.1f);
                                }
                                var obj2 = checkIfHitCrawlerMouth(head, 2.2f);
                                if (obj2 != null)
                                {
                                    var position = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        obj2.GetComponent<HERO>().die((obj2.transform.position - position) * 15f * myLevel, false);
                                    }
                                    else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj2.GetComponent<HERO>().HasDied()))
                                    {
                                        obj2.GetComponent<HERO>().markDie();
                                        object[] parameters = { (obj2.transform.position - position) * 15f * myLevel, true, !nonAI ? -1 : photonView.viewID, name, true };
                                        obj2.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, parameters);
                                    }
                                }
                            }
                            else if (!baseAnimation.IsPlaying("run_abnormal"))
                            {
                                crossFade("run_abnormal", 0.1f);
                            }
                        }
                    }
                    else if (state == TitanState.idle)
                    {
                        if (abnormalType == AbnormalType.TYPE_CRAWLER)
                        {
                            if (!baseAnimation.IsPlaying("crawler_idle"))
                            {
                                crossFade("crawler_idle", 0.1f);
                            }
                        }
                        else if (!baseAnimation.IsPlaying("idle"))
                        {
                            crossFade("idle", 0.1f);
                        }
                        zero = Vector3.zero;
                    }
                    if (state == TitanState.idle)
                    {
                        var velocity = baseRigidBody.velocity;
                        var force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -maxVelocityChange, maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -maxVelocityChange, maxVelocityChange);
                        force.y = 0f;
                        baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                    }
                    else if (state == TitanState.attack && attackAnimation == "jumper_0")
                    {
                        var vector7 = baseRigidBody.velocity;
                        var vector8 = zero * 0.8f - vector7;
                        vector8.x = Mathf.Clamp(vector8.x, -maxVelocityChange, maxVelocityChange);
                        vector8.z = Mathf.Clamp(vector8.z, -maxVelocityChange, maxVelocityChange);
                        vector8.y = 0f;
                        baseRigidBody.AddForce(vector8, ForceMode.VelocityChange);
                    }
                }
                if ((abnormalType == AbnormalType.TYPE_I || abnormalType == AbnormalType.TYPE_JUMPER) && !nonAI && state == TitanState.attack && attackAnimation == "jumper_0")
                {
                    var vector9 = baseTransform.forward * speed * myLevel * 0.5f;
                    var vector10 = baseRigidBody.velocity;
                    if (baseAnimation["attack_jumper_0"].normalizedTime <= 0.28f || baseAnimation["attack_jumper_0"].normalizedTime >= 0.8f)
                    {
                        vector9 = Vector3.zero;
                    }
                    var vector11 = vector9 - vector10;
                    vector11.x = Mathf.Clamp(vector11.x, -maxVelocityChange, maxVelocityChange);
                    vector11.z = Mathf.Clamp(vector11.z, -maxVelocityChange, maxVelocityChange);
                    vector11.y = 0f;
                    baseRigidBody.AddForce(vector11, ForceMode.VelocityChange);
                }
                if (state == TitanState.chase || state == TitanState.wander || state == TitanState.to_check_point || state == TitanState.to_pvp_pt || state == TitanState.random_run)
                {
                    var vector12 = baseTransform.forward * speed;
                    var vector13 = baseRigidBody.velocity;
                    var vector14 = vector12 - vector13;
                    vector14.x = Mathf.Clamp(vector14.x, -maxVelocityChange, maxVelocityChange);
                    vector14.z = Mathf.Clamp(vector14.z, -maxVelocityChange, maxVelocityChange);
                    vector14.y = 0f;
                    baseRigidBody.AddForce(vector14, ForceMode.VelocityChange);
                    if (!stuck && abnormalType != AbnormalType.TYPE_CRAWLER && !nonAI)
                    {
                        if (baseAnimation.IsPlaying(runAnimation) && baseRigidBody.velocity.magnitude < speed * 0.5f)
                        {
                            stuck = true;
                            stuckTime = 2f;
                            stuckTurnAngle = Random.Range(0, 2) * 140f - 70f;
                        }
                        if (state == TitanState.chase && myHero != null && myDistance > attackDistance && myDistance < 150f)
                        {
                            var num = 0.05f;
                            if (myDifficulty > 1)
                            {
                                num += 0.05f;
                            }
                            if (abnormalType != AbnormalType.NORMAL)
                            {
                                num += 0.1f;
                            }
                            if (Random.Range(0f, 1f) < num)
                            {
                                stuck = true;
                                stuckTime = 1f;
                                var num2 = Random.Range(20f, 50f);
                                stuckTurnAngle = Random.Range(0, 2) * num2 * 2f - num2;
                            }
                        }
                    }
                    var current = 0f;
                    if (state == TitanState.wander)
                    {
                        current = baseTransform.rotation.eulerAngles.y - 90f;
                    }
                    else if (state == TitanState.to_check_point || state == TitanState.to_pvp_pt || state == TitanState.random_run)
                    {
                        var vector16 = targetCheckPt - baseTransform.position;
                        current = -Mathf.Atan2(vector16.z, vector16.x) * 57.29578f;
                    }
                    else
                    {
                        if (myHero == null)
                        {
                            return;
                        }
                        var vector17 = myHero.transform.position - baseTransform.position;
                        current = -Mathf.Atan2(vector17.z, vector17.x) * 57.29578f;
                    }
                    if (stuck)
                    {
                        stuckTime -= Time.deltaTime;
                        if (stuckTime < 0f)
                        {
                            stuck = false;
                        }
                        if (stuckTurnAngle > 0f)
                        {
                            stuckTurnAngle -= Time.deltaTime * 10f;
                        }
                        else
                        {
                            stuckTurnAngle += Time.deltaTime * 10f;
                        }
                        current += stuckTurnAngle;
                    }
                    var num4 = -Mathf.DeltaAngle(current, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                    if (abnormalType == AbnormalType.TYPE_CRAWLER)
                    {
                        baseGameObjectTransform.rotation = Quaternion.Lerp(baseGameObjectTransform.rotation, Quaternion.Euler(0f, baseGameObjectTransform.rotation.eulerAngles.y + num4, 0f), speed * 0.3f * Time.deltaTime / myLevel);
                    }
                    else
                    {
                        baseGameObjectTransform.rotation = Quaternion.Lerp(baseGameObjectTransform.rotation, Quaternion.Euler(0f, baseGameObjectTransform.rotation.eulerAngles.y + num4, 0f), speed * 0.5f * Time.deltaTime / myLevel);
                    }
                }
            }
        }
    }

    private string[] GetAttackStrategy()
    {
        string[] strArray = null;
        var num = 0;
        if (isAlarm || myHero.transform.position.y + 3f <= neck.position.y + 10f * myLevel)
        {
            if (myHero.transform.position.y > neck.position.y - 3f * myLevel)
            {
                if (myDistance < attackDistance * 0.5f)
                {
                    if (Vector3.Distance(myHero.transform.position, transform.Find("chkOverHead").position) < 3.6f * myLevel)
                    {
                        if (between2 > 0f)
                        {
                            strArray = new[] { "grab_head_front_r" };
                        }
                        else
                        {
                            strArray = new[] { "grab_head_front_l" };
                        }
                    }
                    else if (Mathf.Abs(between2) < 90f)
                    {
                        if (Mathf.Abs(between2) < 30f)
                        {
                            if (Vector3.Distance(myHero.transform.position, transform.Find("chkFront").position) < 2.5f * myLevel)
                            {
                                strArray = new[] { "attack_bite", "attack_bite", "attack_slap_face" };
                            }
                        }
                        else if (between2 > 0f)
                        {
                            if (Vector3.Distance(myHero.transform.position, transform.Find("chkFrontRight").position) < 2.5f * myLevel)
                            {
                                strArray = new[] { "attack_bite_r" };
                            }
                        }
                        else if (Vector3.Distance(myHero.transform.position, transform.Find("chkFrontLeft").position) < 2.5f * myLevel)
                        {
                            strArray = new[] { "attack_bite_l" };
                        }
                    }
                    else if (between2 > 0f)
                    {
                        if (Vector3.Distance(myHero.transform.position, transform.Find("chkBackRight").position) < 2.8f * myLevel)
                        {
                            strArray = new[] { "grab_head_back_r", "grab_head_back_r", "attack_slap_back" };
                        }
                    }
                    else if (Vector3.Distance(myHero.transform.position, transform.Find("chkBackLeft").position) < 2.8f * myLevel)
                    {
                        strArray = new[] { "grab_head_back_l", "grab_head_back_l", "attack_slap_back" };
                    }
                }
                if (strArray != null)
                {
                    return strArray;
                }
                if (abnormalType == AbnormalType.NORMAL || abnormalType == AbnormalType.TYPE_PUNK)
                {
                    if (myDifficulty <= 0 && Random.Range(0, 0x3e8) >= 3)
                    {
                        return strArray;
                    }
                    if (Mathf.Abs(between2) >= 60f)
                    {
                        return strArray;
                    }
                    return new[] { "attack_combo" };
                }
                if (abnormalType != AbnormalType.TYPE_I && abnormalType != AbnormalType.TYPE_JUMPER)
                {
                    return strArray;
                }
                if (myDifficulty <= 0 && Random.Range(0, 100) >= 50)
                {
                    return strArray;
                }
                return new[] { "attack_abnormal_jump" };
            }
            if (Mathf.Abs(between2) < 90f)
            {
                if (between2 > 0f)
                {
                    num = 1;
                }
                else
                {
                    num = 2;
                }
            }
            else if (between2 > 0f)
            {
                num = 4;
            }
            else
            {
                num = 3;
            }
            switch (num)
            {
                case 1:
                    if (myDistance >= attackDistance * 0.25f)
                    {
                        if (myDistance < attackDistance * 0.5f)
                        {
                            if (abnormalType != AbnormalType.TYPE_PUNK && abnormalType == AbnormalType.NORMAL)
                            {
                                return new[] { "grab_ground_front_r", "grab_ground_front_r", "attack_stomp" };
                            }
                            return new[] { "grab_ground_front_r", "grab_ground_front_r", "attack_abnormal_jump" };
                        }
                        if (abnormalType == AbnormalType.TYPE_PUNK)
                        {
                            return new[] { "attack_combo", "attack_combo", "attack_abnormal_jump" };
                        }
                        if (abnormalType == AbnormalType.NORMAL)
                        {
                            if (myDifficulty > 0)
                            {
                                return new[] { "attack_front_ground", "attack_combo", "attack_combo" };
                            }
                            return new[] { "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_combo" };
                        }
                        return new[] { "attack_abnormal_jump" };
                    }
                    if (abnormalType != AbnormalType.TYPE_PUNK)
                    {
                        if (abnormalType == AbnormalType.NORMAL)
                        {
                            return new[] { "attack_front_ground", "attack_stomp" };
                        }
                        return new[] { "attack_kick" };
                    }
                    return new[] { "attack_kick", "attack_stomp" };

                case 2:
                    if (myDistance >= attackDistance * 0.25f)
                    {
                        if (myDistance < attackDistance * 0.5f)
                        {
                            if (abnormalType != AbnormalType.TYPE_PUNK && abnormalType == AbnormalType.NORMAL)
                            {
                                return new[] { "grab_ground_front_l", "grab_ground_front_l", "attack_stomp" };
                            }
                            return new[] { "grab_ground_front_l", "grab_ground_front_l", "attack_abnormal_jump" };
                        }
                        if (abnormalType == AbnormalType.TYPE_PUNK)
                        {
                            return new[] { "attack_combo", "attack_combo", "attack_abnormal_jump" };
                        }
                        if (abnormalType == AbnormalType.NORMAL)
                        {
                            if (myDifficulty > 0)
                            {
                                return new[] { "attack_front_ground", "attack_combo", "attack_combo" };
                            }
                            return new[] { "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_front_ground", "attack_combo" };
                        }
                        return new[] { "attack_abnormal_jump" };
                    }
                    if (abnormalType != AbnormalType.TYPE_PUNK)
                    {
                        if (abnormalType == AbnormalType.NORMAL)
                        {
                            return new[] { "attack_front_ground", "attack_stomp" };
                        }
                        return new[] { "attack_kick" };
                    }
                    return new[] { "attack_kick", "attack_stomp" };

                case 3:
                    if (myDistance >= attackDistance * 0.5f)
                    {
                        return strArray;
                    }
                    if (abnormalType != AbnormalType.NORMAL)
                    {
                        return new[] { "grab_ground_back_l" };
                    }
                    return new[] { "grab_ground_back_l" };

                case 4:
                    if (myDistance >= attackDistance * 0.5f)
                    {
                        return strArray;
                    }
                    if (abnormalType != AbnormalType.NORMAL)
                    {
                        return new[] { "grab_ground_back_r" };
                    }
                    return new[] { "grab_ground_back_r" };
            }
        }
        return strArray;
    }

    private void getDown()
    {
        state = TitanState.down;
        isAlarm = true;
        playAnimation("sit_hunt_down");
        getdownTime = Random.Range(3f, 5f);
    }

    private GameObject getNearestHero()
    {
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        var position = transform.position;
        foreach (var obj3 in objArray)
        {
            var vector2 = obj3.transform.position - position;
            var sqrMagnitude = vector2.sqrMagnitude;
            if (sqrMagnitude < positiveInfinity)
            {
                obj2 = obj3;
                positiveInfinity = sqrMagnitude;
            }
        }
        return obj2;
    }

    private GameObject getNearestHero2()
    {
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        var position = baseTransform.position;
        foreach (HERO hero in MultiplayerManager.getPlayers())
        {
            var gameObject = hero.gameObject;
            var num2 = Vector3.Distance(gameObject.transform.position, position);
            if (num2 < positiveInfinity)
            {
                obj2 = gameObject;
                positiveInfinity = num2;
            }
        }
        return obj2;
    }

    private int getPunkNumber()
    {
        var num = 0;
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (obj2.GetComponent<TITAN>() != null && obj2.GetComponent<TITAN>().name == "Punk")
            {
                num++;
            }
        }
        return num;
    }

    private void grab(string type)
    {
        state = TitanState.grab;
        attacked = false;
        isAlarm = true;
        attackAnimation = type;
        crossFade("grab_" + type, 0.1f);
        isGrabHandLeft = true;
        grabbedTarget = null;
        var key = type;
        if (key != null)
        {
            int num;
            if (f__switchmap7 == null)
            {
                var dictionary = new Dictionary<string, int>(8);
                dictionary.Add("ground_back_l", 0);
                dictionary.Add("ground_back_r", 1);
                dictionary.Add("ground_front_l", 2);
                dictionary.Add("ground_front_r", 3);
                dictionary.Add("head_back_l", 4);
                dictionary.Add("head_back_r", 5);
                dictionary.Add("head_front_l", 6);
                dictionary.Add("head_front_r", 7);
                f__switchmap7 = dictionary;
            }
            if (f__switchmap7.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        attackCheckTimeA = 0.34f;
                        attackCheckTimeB = 0.49f;
                        break;

                    case 1:
                        attackCheckTimeA = 0.34f;
                        attackCheckTimeB = 0.49f;
                        isGrabHandLeft = false;
                        break;

                    case 2:
                        attackCheckTimeA = 0.37f;
                        attackCheckTimeB = 0.6f;
                        break;

                    case 3:
                        attackCheckTimeA = 0.37f;
                        attackCheckTimeB = 0.6f;
                        isGrabHandLeft = false;
                        break;

                    case 4:
                        attackCheckTimeA = 0.45f;
                        attackCheckTimeB = 0.5f;
                        isGrabHandLeft = false;
                        break;

                    case 5:
                        attackCheckTimeA = 0.45f;
                        attackCheckTimeB = 0.5f;
                        break;

                    case 6:
                        attackCheckTimeA = 0.38f;
                        attackCheckTimeB = 0.55f;
                        break;

                    case 7:
                        attackCheckTimeA = 0.38f;
                        attackCheckTimeB = 0.55f;
                        isGrabHandLeft = false;
                        break;
                }
            }
        }
        if (isGrabHandLeft)
        {
            currentGrabHand = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        }
        else
        {
            currentGrabHand = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        }
    }

    [RPC]
    public void grabbedTargetEscape()
    {
        grabbedTarget = null;
    }

    [RPC]
    public void grabToLeft()
    {
        var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
        grabTF.transform.parent = transform;
        grabTF.transform.position = transform.GetComponent<SphereCollider>().transform.position;
        grabTF.transform.rotation = transform.GetComponent<SphereCollider>().transform.rotation;
        var transform1 = grabTF.transform;
        transform1.localPosition -= Vector3.right * transform.GetComponent<SphereCollider>().radius * 0.3f;
        var transform2 = grabTF.transform;
        transform2.localPosition -= Vector3.up * transform.GetComponent<SphereCollider>().radius * 0.51f;
        var transform3 = grabTF.transform;
        transform3.localPosition -= Vector3.forward * transform.GetComponent<SphereCollider>().radius * 0.3f;
        grabTF.transform.localRotation = Quaternion.Euler(grabTF.transform.localRotation.eulerAngles.x, grabTF.transform.localRotation.eulerAngles.y + 180f, grabTF.transform.localRotation.eulerAngles.z + 180f);
    }

    [RPC]
    public void grabToRight()
    {
        var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        grabTF.transform.parent = transform;
        grabTF.transform.position = transform.GetComponent<SphereCollider>().transform.position;
        grabTF.transform.rotation = transform.GetComponent<SphereCollider>().transform.rotation;
        var transform1 = grabTF.transform;
        transform1.localPosition -= Vector3.right * transform.GetComponent<SphereCollider>().radius * 0.3f;
        var transform2 = grabTF.transform;
        transform2.localPosition += Vector3.up * transform.GetComponent<SphereCollider>().radius * 0.51f;
        var transform3 = grabTF.transform;
        transform3.localPosition -= Vector3.forward * transform.GetComponent<SphereCollider>().radius * 0.3f;
        grabTF.transform.localRotation = Quaternion.Euler(grabTF.transform.localRotation.eulerAngles.x, grabTF.transform.localRotation.eulerAngles.y + 180f, grabTF.transform.localRotation.eulerAngles.z);
    }

    public void headMovement()
    {
        if (!hasDie)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
            {
                if (photonView.isMine)
                {
                    targetHeadRotation = head.rotation;
                    var flag = false;
                    if (abnormalType != AbnormalType.TYPE_CRAWLER && state != TitanState.attack && state != TitanState.down && state != TitanState.hit && state != TitanState.recover && state != TitanState.eat && state != TitanState.hit_eye && !hasDie && myDistance < 100f && myHero != null)
                    {
                        var vector = myHero.transform.position - transform.position;
                        angle = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                        var num = -Mathf.DeltaAngle(angle, transform.rotation.eulerAngles.y - 90f);
                        num = Mathf.Clamp(num, -40f, 40f);
                        var y = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                        var num3 = Mathf.Atan2(y, myDistance) * 57.29578f;
                        num3 = Mathf.Clamp(num3, -40f, 30f);
                        targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num3, head.rotation.eulerAngles.y + num, head.rotation.eulerAngles.z);
                        if (!asClientLookTarget)
                        {
                            asClientLookTarget = true;
                            object[] parameters = { true };
                            photonView.RPC("setIfLookTarget", PhotonTargets.Others, parameters);
                        }
                        flag = true;
                    }
                    if (!flag && asClientLookTarget)
                    {
                        asClientLookTarget = false;
                        object[] objArray2 = { false };
                        photonView.RPC("setIfLookTarget", PhotonTargets.Others, objArray2);
                    }
                    if (state == TitanState.attack || state == TitanState.hit || state == TitanState.hit_eye)
                    {
                        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 20f);
                    }
                    else
                    {
                        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
                else
                {
                    targetHeadRotation = head.rotation;
                    if (asClientLookTarget && myHero != null)
                    {
                        var vector8 = myHero.transform.position - transform.position;
                        angle = -Mathf.Atan2(vector8.z, vector8.x) * 57.29578f;
                        var num4 = -Mathf.DeltaAngle(angle, transform.rotation.eulerAngles.y - 90f);
                        num4 = Mathf.Clamp(num4, -40f, 40f);
                        var num5 = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                        var num6 = Mathf.Atan2(num5, myDistance) * 57.29578f;
                        num6 = Mathf.Clamp(num6, -40f, 30f);
                        targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num6, head.rotation.eulerAngles.y + num4, head.rotation.eulerAngles.z);
                    }
                    if (!hasDie)
                    {
                        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
            }
            else
            {
                targetHeadRotation = head.rotation;
                if (abnormalType != AbnormalType.TYPE_CRAWLER && state != TitanState.attack && state != TitanState.down && state != TitanState.hit && state != TitanState.recover && state != TitanState.hit_eye && !hasDie && myDistance < 100f && myHero != null)
                {
                    var vector15 = myHero.transform.position - transform.position;
                    angle = -Mathf.Atan2(vector15.z, vector15.x) * 57.29578f;
                    var num7 = -Mathf.DeltaAngle(angle, transform.rotation.eulerAngles.y - 90f);
                    num7 = Mathf.Clamp(num7, -40f, 40f);
                    var num8 = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                    var num9 = Mathf.Atan2(num8, myDistance) * 57.29578f;
                    num9 = Mathf.Clamp(num9, -40f, 30f);
                    targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num9, head.rotation.eulerAngles.y + num7, head.rotation.eulerAngles.z);
                }
                if (state == TitanState.attack || state == TitanState.hit || state == TitanState.hit_eye)
                {
                    oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 20f);
                }
                else
                {
                    oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                }
            }
            head.rotation = oldHeadRotation;
        }
        if (!animation.IsPlaying("die_headOff"))
        {
            head.localScale = headscale;
        }
    }

    public void headMovement2()
    {
        if (!hasDie)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
            {
                if (photonView.isMine)
                {
                    targetHeadRotation = head.rotation;
                    var flag2 = false;
                    if (abnormalType != AbnormalType.TYPE_CRAWLER && state != TitanState.attack && state != TitanState.down && state != TitanState.hit && state != TitanState.recover && state != TitanState.eat && state != TitanState.hit_eye && !hasDie && myDistance < 100f && myHero != null)
                    {
                        var vector = myHero.transform.position - transform.position;
                        angle = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                        var num = -Mathf.DeltaAngle(angle, transform.rotation.eulerAngles.y - 90f);
                        num = Mathf.Clamp(num, -40f, 40f);
                        var y = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                        var num3 = Mathf.Atan2(y, myDistance) * 57.29578f;
                        num3 = Mathf.Clamp(num3, -40f, 30f);
                        targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num3, head.rotation.eulerAngles.y + num, head.rotation.eulerAngles.z);
                        if (!asClientLookTarget)
                        {
                            asClientLookTarget = true;
                            object[] parameters = { true };
                            photonView.RPC("setIfLookTarget", PhotonTargets.Others, parameters);
                        }
                        flag2 = true;
                    }
                    if (!(flag2 || !asClientLookTarget))
                    {
                        asClientLookTarget = false;
                        object[] objArray3 = { false };
                        photonView.RPC("setIfLookTarget", PhotonTargets.Others, objArray3);
                    }
                    if (state == TitanState.attack || state == TitanState.hit || state == TitanState.hit_eye)
                    {
                        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 20f);
                    }
                    else
                    {
                        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
                else
                {
                    var flag3 = myHero != null;
                    if (flag3)
                    {
                        myDistance = Mathf.Sqrt((myHero.transform.position.x - baseTransform.position.x) * (myHero.transform.position.x - baseTransform.position.x) + (myHero.transform.position.z - baseTransform.position.z) * (myHero.transform.position.z - baseTransform.position.z));
                    }
                    else
                    {
                        myDistance = float.MaxValue;
                    }
                    targetHeadRotation = head.rotation;
                    if (asClientLookTarget && flag3 && myDistance < 100f)
                    {
                        var vector2 = myHero.transform.position - baseTransform.position;
                        angle = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                        var num4 = -Mathf.DeltaAngle(angle, baseTransform.rotation.eulerAngles.y - 90f);
                        num4 = Mathf.Clamp(num4, -40f, 40f);
                        var num5 = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                        var num6 = Mathf.Atan2(num5, myDistance) * 57.29578f;
                        num6 = Mathf.Clamp(num6, -40f, 30f);
                        targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num6, head.rotation.eulerAngles.y + num4, head.rotation.eulerAngles.z);
                    }
                    if (!hasDie)
                    {
                        oldHeadRotation = Quaternion.Slerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                    }
                }
            }
            else
            {
                targetHeadRotation = head.rotation;
                if (abnormalType != AbnormalType.TYPE_CRAWLER && state != TitanState.attack && state != TitanState.down && state != TitanState.hit && state != TitanState.recover && state != TitanState.hit_eye && !hasDie && myDistance < 100f && myHero != null)
                {
                    var vector3 = myHero.transform.position - transform.position;
                    angle = -Mathf.Atan2(vector3.z, vector3.x) * 57.29578f;
                    var num7 = -Mathf.DeltaAngle(angle, transform.rotation.eulerAngles.y - 90f);
                    num7 = Mathf.Clamp(num7, -40f, 40f);
                    var num8 = neck.position.y + myLevel * 2f - myHero.transform.position.y;
                    var num9 = Mathf.Atan2(num8, myDistance) * 57.29578f;
                    num9 = Mathf.Clamp(num9, -40f, 30f);
                    targetHeadRotation = Quaternion.Euler(head.rotation.eulerAngles.x + num9, head.rotation.eulerAngles.y + num7, head.rotation.eulerAngles.z);
                }
                if (state == TitanState.attack || state == TitanState.hit || state == TitanState.hit_eye)
                {
                    oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 20f);
                }
                else
                {
                    oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 10f);
                }
            }
            head.rotation = oldHeadRotation;
        }
        if (!animation.IsPlaying("die_headOff"))
        {
            head.localScale = headscale;
        }
    }

    private void hit(string animationName, Vector3 attacker, float hitPauseTime)
    {
        state = TitanState.hit;
        hitAnimation = animationName;
        hitPause = hitPauseTime;
        playAnimation(hitAnimation);
        animation[hitAnimation].time = 0f;
        animation[hitAnimation].speed = 0f;
        transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(attacker - transform.position).eulerAngles.y, 0f);
        needFreshCorePosition = true;
        if (photonView.isMine && grabbedTarget != null)
        {
            grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
        }
    }

    public void hitAnkle()
    {
        if (!hasDie && state != TitanState.down)
        {
            if (grabbedTarget != null)
            {
                grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
            }
            getDown();
        }
    }

    [RPC]
    public void hitAnkleRPC(int viewID)
    {
        if (!hasDie && state != TitanState.down)
        {
            var view = PhotonView.Find(viewID);
            if (view != null)
            {
                var vector = view.gameObject.transform.position - transform.position;
                if (vector.magnitude < 20f)
                {
                    if (photonView.isMine && grabbedTarget != null)
                    {
                        grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
                    }
                    getDown();
                }
            }
        }
    }

    public void hitEye()
    {
        if (!hasDie)
        {
            justHitEye();
        }
    }

    [RPC]
    public void hitEyeRPC(int viewID)
    {
        if (!hasDie)
        {
            var vector = PhotonView.Find(viewID).gameObject.transform.position - neck.position;
            if (vector.magnitude < 20f)
            {
                if (photonView.isMine && grabbedTarget != null)
                {
                    grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
                }
                if (!hasDie)
                {
                    justHitEye();
                }
            }
        }
    }

    public void hitL(Vector3 attacker, float hitPauseTime)
    {
        if (abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                hit("hit_eren_L", attacker, hitPauseTime);
            }
            else
            {
                object[] parameters = { attacker, hitPauseTime };
                photonView.RPC("hitLRPC", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void hitLRPC(Vector3 attacker, float hitPauseTime)
    {
        if (photonView.isMine)
        {
            var vector = attacker - transform.position;
            if (vector.magnitude < 80f)
            {
                hit("hit_eren_L", attacker, hitPauseTime);
            }
        }
    }

    public void hitR(Vector3 attacker, float hitPauseTime)
    {
        if (abnormalType != AbnormalType.TYPE_CRAWLER)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                hit("hit_eren_R", attacker, hitPauseTime);
            }
            else
            {
                object[] parameters = { attacker, hitPauseTime };
                photonView.RPC("hitRRPC", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void hitRRPC(Vector3 attacker, float hitPauseTime)
    {
        if (photonView.isMine && !hasDie)
        {
            var vector = attacker - transform.position;
            if (vector.magnitude < 80f)
            {
                hit("hit_eren_R", attacker, hitPauseTime);
            }
        }
    }

    private void idle( float sbtime = 0f)
    {
        stuck = false;
        this.sbtime = sbtime;
        if (myDifficulty == 2 && (abnormalType == AbnormalType.TYPE_JUMPER || abnormalType == AbnormalType.TYPE_I))
        {
            this.sbtime = Random.Range(0f, 1.5f);
        }
        else if (myDifficulty >= 1)
        {
            this.sbtime = 0f;
        }
        this.sbtime = Mathf.Max(0.5f, this.sbtime);
        if (abnormalType == AbnormalType.TYPE_PUNK)
        {
            this.sbtime = 0.1f;
            if (myDifficulty == 1)
            {
                this.sbtime += 0.4f;
            }
        }
        state = TitanState.idle;
        if (abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            crossFade("crawler_idle", 0.2f);
        }
        else
        {
            crossFade("idle", 0.2f);
        }
    }

    public bool IsGrounded()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyAABB");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(gameObject.transform.position + Vector3.up * 0.1f, -Vector3.up, 0.3f, mask3.value);
    }

    private void justEatHero(GameObject target, Transform hand)
    {
        if (target != null)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
            {
                if (!target.GetComponent<HERO>().HasDied())
                {
                    target.GetComponent<HERO>().markDie();
                    if (nonAI)
                    {
                        object[] parameters = { photonView.viewID, name };
                        target.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, parameters);
                    }
                    else
                    {
                        object[] objArray2 = { -1, name };
                        target.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, objArray2);
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                target.GetComponent<HERO>().die2(hand);
            }
        }
    }

    private void justHitEye()
    {
        if (state != TitanState.hit_eye)
        {
            if (state == TitanState.down || state == TitanState.sit)
            {
                playAnimation("sit_hit_eye");
            }
            else
            {
                playAnimation("hit_eye");
            }
            state = TitanState.hit_eye;
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
                healthLabel.transform.localPosition = new Vector3(0f, 20f + 1f / myLevel, 0f);
                if (abnormalType == AbnormalType.TYPE_CRAWLER)
                {
                    healthLabel.transform.localPosition = new Vector3(0f, 10f + 1f / myLevel, 0f);
                }
                var x = 1f;
                if (myLevel < 1f)
                {
                    x = 1f / myLevel;
                }
                healthLabel.transform.localScale = new Vector3(x, x, x);
                healthLabelEnabled = true;
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

    public void lateUpdate()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            if (animation.IsPlaying("run_walk"))
            {
                if (animation["run_walk"].normalizedTime % 1f > 0.1f && animation["run_walk"].normalizedTime % 1f < 0.6f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    var transform = this.transform.Find("snd_titan_foot");
                    transform.GetComponent<AudioSource>().Stop();
                    transform.GetComponent<AudioSource>().Play();
                }
                if (animation["run_walk"].normalizedTime % 1f > 0.6f && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    var transform2 = transform.Find("snd_titan_foot");
                    transform2.GetComponent<AudioSource>().Stop();
                    transform2.GetComponent<AudioSource>().Play();
                }
            }
            if (animation.IsPlaying("crawler_run"))
            {
                if (animation["crawler_run"].normalizedTime % 1f > 0.1f && animation["crawler_run"].normalizedTime % 1f < 0.56f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    var transform3 = transform.Find("snd_titan_foot");
                    transform3.GetComponent<AudioSource>().Stop();
                    transform3.GetComponent<AudioSource>().Play();
                }
                if (animation["crawler_run"].normalizedTime % 1f > 0.56f && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    var transform4 = transform.Find("snd_titan_foot");
                    transform4.GetComponent<AudioSource>().Stop();
                    transform4.GetComponent<AudioSource>().Play();
                }
            }
            if (animation.IsPlaying("run_abnormal"))
            {
                if (animation["run_abnormal"].normalizedTime % 1f > 0.47f && animation["run_abnormal"].normalizedTime % 1f < 0.95f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    var transform5 = transform.Find("snd_titan_foot");
                    transform5.GetComponent<AudioSource>().Stop();
                    transform5.GetComponent<AudioSource>().Play();
                }
                if ((animation["run_abnormal"].normalizedTime % 1f > 0.95f || animation["run_abnormal"].normalizedTime % 1f < 0.47f) && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    var transform6 = transform.Find("snd_titan_foot");
                    transform6.GetComponent<AudioSource>().Stop();
                    transform6.GetComponent<AudioSource>().Play();
                }
            }
            headMovement();
            grounded = false;
        }
    }

    public void lateUpdate2()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            if (baseAnimation.IsPlaying("run_walk"))
            {
                if (baseAnimation["run_walk"].normalizedTime % 1f > 0.1f && baseAnimation["run_walk"].normalizedTime % 1f < 0.6f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
                else if (baseAnimation["run_walk"].normalizedTime % 1f > 0.6f && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
            }
            else if (baseAnimation.IsPlaying("crawler_run"))
            {
                if (baseAnimation["crawler_run"].normalizedTime % 1f > 0.1f && baseAnimation["crawler_run"].normalizedTime % 1f < 0.56f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
                else if (baseAnimation["crawler_run"].normalizedTime % 1f > 0.56f && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
            }
            else if (baseAnimation.IsPlaying("run_abnormal"))
            {
                if (baseAnimation["run_abnormal"].normalizedTime % 1f > 0.47f && baseAnimation["run_abnormal"].normalizedTime % 1f < 0.95f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
                else if ((baseAnimation["run_abnormal"].normalizedTime % 1f > 0.95f || baseAnimation["run_abnormal"].normalizedTime % 1f < 0.47f) && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    baseAudioSource.Stop();
                    baseAudioSource.Play();
                }
            }
            headMovement2();
            grounded = false;
            updateLabel();
            updateCollider();
        }
    }

    [RPC]
    private void laugh(float sbtime = 0f)
    {
        if (state == TitanState.idle || state == TitanState.turn || state == TitanState.chase)
        {
            this.sbtime = sbtime;
            state = TitanState.laugh;
            crossFade("laugh", 0.2f);
        }
    }

    public void loadskin()
    {
        skin = 0x56;
        this.eye = false;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine) && (int) FengGameManagerMKII.settings[1] == 1)
        {
            var index = (int) Random.Range(86f, 90f);
            var num2 = index - 60;
            if ((int) FengGameManagerMKII.settings[0x20] == 1)
            {
                num2 = Random.Range(0x1a, 30);
            }
            var body = (string) FengGameManagerMKII.settings[index];
            var eye = (string) FengGameManagerMKII.settings[num2];
            skin = index;
            if (eye.EndsWith(".jpg") || eye.EndsWith(".png") || eye.EndsWith(".jpeg"))
            {
                this.eye = true;
            }
            GetComponent<TITAN_SETUP>().setVar(skin, this.eye);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                StartCoroutine(loadskinE(body, eye));
            }
            else
            {
                photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, body, eye);
            }
        }
    }

    public IEnumerator loadskinE(string body, string eye)
    {
        while (!hasSpawn)
        {
            yield return null;
        }
        var mipmap = true;
        var iteratorVariable1 = false;
        if ((int) FengGameManagerMKII.settings[0x3f] == 1)
        {
            mipmap = false;
        }
        foreach (var iteratorVariable2 in GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable2.name.Contains("eye"))
            {
                if (eye.ToLower() == "transparent")
                {
                    iteratorVariable2.enabled = false;
                }
                else if (eye.EndsWith(".jpg") || eye.EndsWith(".png") || eye.EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(eye))
                    {
                        var link = new WWW(eye);
                        yield return link;
                        var iteratorVariable4 = RCextensions.loadimage(link, mipmap, 0x30d40);
                        link.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(eye))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable2.material.mainTextureScale = new Vector2(iteratorVariable2.material.mainTextureScale.x * 4f, iteratorVariable2.material.mainTextureScale.y * 8f);
                            iteratorVariable2.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable2.material.mainTexture = iteratorVariable4;
                            FengGameManagerMKII.linkHash[0].Add(eye, iteratorVariable2.material);
                            iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[0][eye];
                        }
                        else
                        {
                            iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[0][eye];
                        }
                    }
                    else
                    {
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[0][eye];
                    }
                }
            }
            else if (iteratorVariable2.name == "hair" && (body.EndsWith(".jpg") || body.EndsWith(".png") || body.EndsWith(".jpeg")))
            {
                if (!FengGameManagerMKII.linkHash[2].ContainsKey(body))
                {
                    var iteratorVariable5 = new WWW(body);
                    yield return iteratorVariable5;
                    var iteratorVariable6 = RCextensions.loadimage(iteratorVariable5, mipmap, 0xf4240);
                    iteratorVariable5.Dispose();
                    if (!FengGameManagerMKII.linkHash[2].ContainsKey(body))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable2.material = mainMaterial.GetComponent<SkinnedMeshRenderer>().material;
                        iteratorVariable2.material.mainTexture = iteratorVariable6;
                        FengGameManagerMKII.linkHash[2].Add(body, iteratorVariable2.material);
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][body];
                    }
                    else
                    {
                        iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][body];
                    }
                }
                else
                {
                    iteratorVariable2.material = (Material) FengGameManagerMKII.linkHash[2][body];
                }
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
    }

    [RPC]
    public void loadskinRPC(string body, string eye)
    {
        if ((int) FengGameManagerMKII.settings[1] == 1)
        {
            StartCoroutine(loadskinE(body, eye));
        }
    }

    private bool longRangeAttackCheck()
    {
        if (abnormalType == AbnormalType.TYPE_PUNK && myHero != null)
        {
            var line = myHero.rigidbody.velocity * Time.deltaTime * 30f;
            if (line.sqrMagnitude > 10f)
            {
                if (simpleHitTestLineAndBall(line, baseTransform.Find("chkAeLeft").position - myHero.transform.position, 5f * myLevel))
                {
                    attack("anti_AE_l");
                    return true;
                }
                if (simpleHitTestLineAndBall(line, baseTransform.Find("chkAeLLeft").position - myHero.transform.position, 5f * myLevel))
                {
                    attack("anti_AE_low_l");
                    return true;
                }
                if (simpleHitTestLineAndBall(line, baseTransform.Find("chkAeRight").position - myHero.transform.position, 5f * myLevel))
                {
                    attack("anti_AE_r");
                    return true;
                }
                if (simpleHitTestLineAndBall(line, baseTransform.Find("chkAeLRight").position - myHero.transform.position, 5f * myLevel))
                {
                    attack("anti_AE_low_r");
                    return true;
                }
            }
            var vector2 = myHero.transform.position - baseTransform.position;
            var current = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
            var f = -Mathf.DeltaAngle(current, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
            if (rockInterval > 0f)
            {
                rockInterval -= Time.deltaTime;
            }
            else if (Mathf.Abs(f) < 5f)
            {
                var vector3 = myHero.transform.position + line;
                var vector4 = vector3 - baseTransform.position;
                var sqrMagnitude = vector4.sqrMagnitude;
                if (sqrMagnitude > 8000f && sqrMagnitude < 90000f && RCSettings.disableRock == 0)
                {
                    attack("throw");
                    rockInterval = 2f;
                    return true;
                }
            }
        }
        return false;
    }

    public void moveTo(float posX, float posY, float posZ)
    {
        transform.position = new Vector3(posX, posY, posZ);
    }

    [RPC]
    public void moveToRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            transform.position = new Vector3(posX, posY, posZ);
        }
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
    }

    [RPC]
    private void netDie()
    {
        asClientLookTarget = false;
        if (!hasDie)
        {
            hasDie = true;
            if (nonAI)
            {
                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
                var propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.dead, true);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.deaths, (int) PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths] + 1);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }
            dieAnimation();
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

    [RPC]
    private void netSetAbnormalType(int type)
    {
        if (!hasload)
        {
            hasload = true;
            loadskin();
        }
        if (type == 0)
        {
            abnormalType = AbnormalType.NORMAL;
            name = "Titan";
            runAnimation = "run_walk";
            GetComponent<TITAN_SETUP>().setHair2();
        }
        else if (type == 1)
        {
            abnormalType = AbnormalType.TYPE_I;
            name = "Aberrant";
            runAnimation = "run_abnormal";
            GetComponent<TITAN_SETUP>().setHair2();
        }
        else if (type == 2)
        {
            abnormalType = AbnormalType.TYPE_JUMPER;
            name = "Jumper";
            runAnimation = "run_abnormal";
            GetComponent<TITAN_SETUP>().setHair2();
        }
        else if (type == 3)
        {
            abnormalType = AbnormalType.TYPE_CRAWLER;
            name = "Crawler";
            runAnimation = "crawler_run";
            GetComponent<TITAN_SETUP>().setHair2();
        }
        else if (type == 4)
        {
            abnormalType = AbnormalType.TYPE_PUNK;
            name = "Punk";
            runAnimation = "run_abnormal_1";
            GetComponent<TITAN_SETUP>().setHair2();
        }
        if (abnormalType == AbnormalType.TYPE_I || abnormalType == AbnormalType.TYPE_JUMPER || abnormalType == AbnormalType.TYPE_PUNK)
        {
            speed = 18f;
            if (myLevel > 1f)
            {
                speed *= Mathf.Sqrt(myLevel);
            }
            if (myDifficulty == 1)
            {
                speed *= 1.4f;
            }
            if (myDifficulty == 2)
            {
                speed *= 1.6f;
            }
            baseAnimation["turnaround1"].speed = 2f;
            baseAnimation["turnaround2"].speed = 2f;
        }
        if (abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            chaseDistance += 50f;
            speed = 25f;
            if (myLevel > 1f)
            {
                speed *= Mathf.Sqrt(myLevel);
            }
            if (myDifficulty == 1)
            {
                speed *= 2f;
            }
            if (myDifficulty == 2)
            {
                speed *= 2.2f;
            }
            baseTransform.Find("AABB").gameObject.GetComponent<CapsuleCollider>().height = 10f;
            baseTransform.Find("AABB").gameObject.GetComponent<CapsuleCollider>().radius = 5f;
            baseTransform.Find("AABB").gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 5.05f, 0f);
        }
        if (nonAI)
        {
            if (abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                speed = Mathf.Min(70f, speed);
            }
            else
            {
                speed = Mathf.Min(60f, speed);
            }
            baseAnimation["attack_jumper_0"].speed = 7f;
            baseAnimation["attack_crawler_jump_0"].speed = 4f;
        }
        baseAnimation["attack_combo_1"].speed = 1f;
        baseAnimation["attack_combo_2"].speed = 1f;
        baseAnimation["attack_combo_3"].speed = 1f;
        baseAnimation["attack_quick_turn_l"].speed = 1f;
        baseAnimation["attack_quick_turn_r"].speed = 1f;
        baseAnimation["attack_anti_AE_l"].speed = 1.1f;
        baseAnimation["attack_anti_AE_low_l"].speed = 1.1f;
        baseAnimation["attack_anti_AE_r"].speed = 1.1f;
        baseAnimation["attack_anti_AE_low_r"].speed = 1.1f;
        idle();
    }

    [RPC]
    private void netSetLevel(float level, int AI, int skinColor)
    {
        setLevel2(level, AI, skinColor);
        if (level > 5f)
        {
            headscale = new Vector3(1f, 1f, 1f);
        }
        else if (level < 1f && FengGameManagerMKII.level.StartsWith("Custom"))
        {
            var component = myTitanTrigger.GetComponent<CapsuleCollider>();
            component.radius *= 2.5f - level;
        }
    }

    private void OnCollisionStay()
    {
        grounded = true;
    }

    private void OnDestroy()
    {
        if (GGM.Caching.GameObjectCache.Find("MultiplayerManager") != null)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().removeTitan(this);
        }
    }

    public void OnTitanDie(PhotonView view)
    {
        if (FengGameManagerMKII.logicLoaded && FengGameManagerMKII.RCEvents.ContainsKey("OnTitanDie"))
        {
            var event2 = (RCEvent) FengGameManagerMKII.RCEvents["OnTitanDie"];
            var strArray = (string[]) FengGameManagerMKII.RCVariableNames["OnTitanDie"];
            if (FengGameManagerMKII.titanVariables.ContainsKey(strArray[0]))
            {
                FengGameManagerMKII.titanVariables[strArray[0]] = this;
            }
            else
            {
                FengGameManagerMKII.titanVariables.Add(strArray[0], this);
            }
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[1]))
            {
                FengGameManagerMKII.playerVariables[strArray[1]] = view.owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[1], view.owner);
            }
            event2.checkEvent();
        }
    }

    private void playAnimation(string aniName)
    {
        animation.Play(aniName);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            object[] parameters = { aniName };
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            object[] parameters = { aniName, normalizedTime };
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        playsoundRPC(sndname);
        if (photonView.isMine)
        {
            object[] parameters = { sndname };
            photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void playsoundRPC(string sndname)
    {
        transform.Find(sndname).GetComponent<AudioSource>().Play();
    }

    public void pt()
    {
        if (controller.bite)
        {
            attack("bite");
        }
        if (controller.bitel)
        {
            attack("bite_l");
        }
        if (controller.biter)
        {
            attack("bite_r");
        }
        if (controller.chopl)
        {
            attack("anti_AE_low_l");
        }
        if (controller.chopr)
        {
            attack("anti_AE_low_r");
        }
        if (controller.choptl)
        {
            attack("anti_AE_l");
        }
        if (controller.choptr)
        {
            attack("anti_AE_r");
        }
        if (controller.cover && stamina > 75f)
        {
            recoverpt();
            stamina -= 75f;
        }
        if (controller.grabbackl)
        {
            grab("ground_back_l");
        }
        if (controller.grabbackr)
        {
            grab("ground_back_r");
        }
        if (controller.grabfrontl)
        {
            grab("ground_front_l");
        }
        if (controller.grabfrontr)
        {
            grab("ground_front_r");
        }
        if (controller.grabnapel)
        {
            grab("head_back_l");
        }
        if (controller.grabnaper)
        {
            grab("head_back_r");
        }
    }

    public void randomRun(Vector3 targetPt, float r)
    {
        state = TitanState.random_run;
        targetCheckPt = targetPt;
        targetR = r;
        random_run_time = Random.Range(1f, 2f);
        crossFade(runAnimation, 0.5f);
    }

    private void recover()
    {
        state = TitanState.recover;
        playAnimation("idle_recovery");
        getdownTime = Random.Range(2f, 5f);
    }

    private void recoverpt()
    {
        state = TitanState.recover;
        playAnimation("idle_recovery");
        getdownTime = Random.Range(1.8f, 2.5f);
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if (FengGameManagerMKII.skyMaterial != null && Camera.main.GetComponent<Skybox>().material != FengGameManagerMKII.skyMaterial)
        {
            Camera.main.GetComponent<Skybox>().material = FengGameManagerMKII.skyMaterial;
        }
    }

    private void remainSitdown()
    {
        state = TitanState.sit;
        playAnimation("sit_idle");
        getdownTime = Random.Range(10f, 30f);
    }

    public void resetLevel(float level)
    {
        myLevel = level;
        setmyLevel();
    }

    public void setAbnormalType(AbnormalType type, bool forceCrawler)
    {
        var flag = false;
        if (RCSettings.spawnMode > 0 || (int) FengGameManagerMKII.settings[0x5b] == 1 && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            flag = true;
        }
        if (FengGameManagerMKII.level.StartsWith("Custom"))
        {
            flag = true;
        }
        var num = 0;
        var num2 = 0.02f * (IN_GAME_MAIN_CAMERA.difficulty + 1);
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            num2 = 100f;
        }
        if (type == AbnormalType.NORMAL)
        {
            if (Random.Range(0f, 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 0;
            }
            if (flag)
            {
                num = 0;
            }
        }
        else if (type == AbnormalType.TYPE_I)
        {
            if (Random.Range(0f, 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 1;
            }
            if (flag)
            {
                num = 1;
            }
        }
        else if (type == AbnormalType.TYPE_JUMPER)
        {
            if (Random.Range(0f, 1f) < num2)
            {
                num = 4;
            }
            else
            {
                num = 2;
            }
            if (flag)
            {
                num = 2;
            }
        }
        else if (type == AbnormalType.TYPE_CRAWLER)
        {
            num = 3;
            if (GGM.Caching.GameObjectCache.Find("Crawler") != null && Random.Range(0, 0x3e8) > 5)
            {
                num = 2;
            }
            if (flag)
            {
                num = 3;
            }
        }
        else if (type == AbnormalType.TYPE_PUNK)
        {
            num = 4;
        }
        if (forceCrawler)
        {
            num = 3;
        }
        if (num == 4)
        {
            if (!LevelInfo.getInfo(FengGameManagerMKII.level).punk)
            {
                num = 1;
            }
            else
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE && getPunkNumber() >= 3)
                {
                    num = 1;
                }
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    var wave = FengGameManagerMKII.instance.wave;
                    if (wave != 5 && wave != 10 && wave != 15 && wave != 20)
                    {
                        num = 1;
                    }
                }
            }
            if (flag)
            {
                num = 4;
            }
        }
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            object[] parameters = { num };
            photonView.RPC("netSetAbnormalType", PhotonTargets.AllBuffered, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            netSetAbnormalType(num);
        }
    }

    [RPC]
    private void setIfLookTarget(bool bo)
    {
        asClientLookTarget = bo;
    }

    // TITAN
    private void setLevel(float level, int AI, int skinColor)
    {
        myLevel = level;
        myLevel = Mathf.Clamp(myLevel, 0.7f, 3f);
        attackWait += Random.Range(0f, 2f);
        chaseDistance += myLevel * 10f;
        transform.localScale = new Vector3(myLevel, myLevel, myLevel);
        var num = Mathf.Pow(2f / myLevel, 0.35f);
        num = Mathf.Min(num, 1.25f);
        headscale = new Vector3(num, num, num);
        head = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        head.localScale = headscale;
        if (skinColor != 0)
        {
            mainMaterial.GetComponent<SkinnedMeshRenderer>().material.color = skinColor != 1 ? skinColor != 2 ? FengColor.titanSkin3 : FengColor.titanSkin2 : FengColor.titanSkin1;
        }
        var num2 = 1.4f - (myLevel - 0.7f) * 0.15f;
        num2 = Mathf.Clamp(num2, 0.9f, 1.5f);
        foreach (AnimationState animationState in animation)
        {
            animationState.speed = num2;
        }
        rigidbody.mass *= myLevel;
        rigidbody.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        if (myLevel > 1f)
        {
            speed *= Mathf.Sqrt(myLevel);
        }
        myDifficulty = AI;
        if (myDifficulty == 1 || myDifficulty == 2)
        {
            foreach (AnimationState animationState2 in animation)
            {
                animationState2.speed = num2 * 1.05f;
            }
            if (nonAI)
            {
                speed *= 1.1f;
            }
            else
            {
                speed *= 1.4f;
            }
            chaseDistance *= 1.15f;
        }
        if (myDifficulty == 2)
        {
            foreach (AnimationState animationState3 in animation)
            {
                animationState3.speed = num2 * 1.05f;
            }
            if (nonAI)
            {
                speed *= 1.1f;
            }
            else
            {
                speed *= 1.5f;
            }
            chaseDistance *= 1.3f;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            chaseDistance = 999999f;
        }
        if (nonAI)
        {
            if (abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                speed = Mathf.Min(70f, speed);
            }
            else
            {
                speed = Mathf.Min(60f, speed);
            }
        }
        attackDistance = Vector3.Distance(transform.position, transform.Find("ap_front_ground").position) * 1.65f;
    }


    // TITAN
    private void setLevel2(float level, int AI, int skinColor)
    {
        myLevel = level;
        myLevel = Mathf.Clamp(myLevel, 0.1f, 50f);
        attackWait += Random.Range(0f, 2f);
        chaseDistance += myLevel * 10f;
        transform.localScale = new Vector3(myLevel, myLevel, myLevel);
        var num = Mathf.Min(Mathf.Pow(2f / myLevel, 0.35f), 1.25f);
        headscale = new Vector3(num, num, num);
        head = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        head.localScale = headscale;
        if (skinColor != 0)
        {
            mainMaterial.GetComponent<SkinnedMeshRenderer>().material.color = skinColor != 1 ? skinColor != 2 ? FengColor.titanSkin3 : FengColor.titanSkin2 : FengColor.titanSkin1;
        }
        var num2 = 1.4f - (myLevel - 0.7f) * 0.15f;
        num2 = Mathf.Clamp(num2, 0.9f, 1.5f);
        foreach (AnimationState animationState in animation)
        {
            animationState.speed = num2;
        }
        var rigidbody = this.rigidbody;
        rigidbody.mass *= myLevel;
        this.rigidbody.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        if (myLevel > 1f)
        {
            speed *= Mathf.Sqrt(myLevel);
        }
        myDifficulty = AI;
        if (myDifficulty == 1 || myDifficulty == 2)
        {
            foreach (AnimationState animationState2 in animation)
            {
                animationState2.speed = num2 * 1.05f;
            }
            if (nonAI)
            {
                speed *= 1.1f;
            }
            else
            {
                speed *= 1.4f;
            }
            chaseDistance *= 1.15f;
        }
        if (myDifficulty == 2)
        {
            foreach (AnimationState animationState3 in animation)
            {
                animationState3.speed = num2 * 1.05f;
            }
            if (nonAI)
            {
                speed *= 1.1f;
            }
            else
            {
                speed *= 1.5f;
            }
            chaseDistance *= 1.3f;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            chaseDistance = 999999f;
        }
        if (nonAI)
        {
            if (abnormalType == AbnormalType.TYPE_CRAWLER)
            {
                speed = Mathf.Min(70f, speed);
            }
            else
            {
                speed = Mathf.Min(60f, speed);
            }
        }
        attackDistance = Vector3.Distance(transform.position, transform.Find("ap_front_ground").position) * 1.65f;
    }


    private void setmyLevel()
    {
        animation.cullingType = AnimationCullingType.BasedOnRenderers;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            object[] parameters = { myLevel, GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().difficulty, Random.Range(0, 4) };
            photonView.RPC("netSetLevel", PhotonTargets.AllBuffered, parameters);
            animation.cullingType = AnimationCullingType.AlwaysAnimate;
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            setLevel2(myLevel, IN_GAME_MAIN_CAMERA.difficulty, Random.Range(0, 4));
        }
    }

    [RPC]
    private void setMyTarget(int ID)
    {
        if (ID == -1)
        {
            myHero = null;
        }
        var view = PhotonView.Find(ID);
        if (view != null)
        {
            myHero = view.gameObject;
        }
    }

    public void setRoute(GameObject route)
    {
        checkPoints = new ArrayList();
        for (var i = 1; i <= 10; i++)
        {
            checkPoints.Add(route.transform.Find("r" + i).position);
        }
        checkPoints.Add("end");
    }

    private bool simpleHitTestLineAndBall(Vector3 line, Vector3 ball, float R)
    {
        var rhs = Vector3.Project(ball, line);
        var vector2 = ball - rhs;
        if (vector2.magnitude > R)
        {
            return false;
        }
        if (Vector3.Dot(line, rhs) < 0f)
        {
            return false;
        }
        if (rhs.sqrMagnitude > line.sqrMagnitude)
        {
            return false;
        }
        return true;
    }

    private void sitdown()
    {
        state = TitanState.sit;
        playAnimation("sit_down");
        getdownTime = Random.Range(10f, 30f);
    }

    private void Start()
    {
        MultiplayerManager.addTitan(this);
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.yellow, false, true);
        }
        currentCamera = GGM.Caching.GameObjectCache.Find("MainCamera");
        runAnimation = "run_walk";
        grabTF = new GameObject();
        grabTF.name = "titansTmpGrabTF";
        head = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        neck = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        oldHeadRotation = head.rotation;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || photonView.isMine)
        {
            if (!hasSetLevel)
            {
                myLevel = Random.Range(0.7f, 3f);
                if (RCSettings.sizeMode > 0)
                {
                    var sizeLower = RCSettings.sizeLower;
                    var sizeUpper = RCSettings.sizeUpper;
                    myLevel = Random.Range(sizeLower, sizeUpper);
                }
                hasSetLevel = true;
            }
            spawnPt = baseTransform.position;
            setmyLevel();
            setAbnormalType(abnormalType, false);
            if (myHero == null)
            {
                findNearestHero();
            }
            controller = gameObject.GetComponent<TITAN_CONTROLLER>();
            if (nonAI)
            {
                StartCoroutine(reloadSky());
            }
        }
        if (maxHealth == 0 && RCSettings.healthMode > 0)
        {
            if (RCSettings.healthMode == 1)
            {
                maxHealth = currentHealth = Random.Range(RCSettings.healthLower, RCSettings.healthUpper + 1);
            }
            else if (RCSettings.healthMode == 2)
            {
                maxHealth = currentHealth = Mathf.Clamp(Mathf.RoundToInt(myLevel / 4f * Random.Range(RCSettings.healthLower, RCSettings.healthUpper + 1)), RCSettings.healthLower, RCSettings.healthUpper);
            }
        }
        lagMax = 150f + myLevel * 3f;
        healthTime = Time.time;
        if (currentHealth > 0 && photonView.isMine)
        {
            photonView.RPC("labelRPC", PhotonTargets.AllBuffered, currentHealth, maxHealth);
        }
        hasExplode = false;
        colliderEnabled = true;
        isHooked = false;
        isLook = false;
        hasSpawn = true;
    }

    public void suicide()
    {
        netDie();
        if (nonAI)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().sendKillInfo(false, string.Empty, true, (string) PhotonNetwork.player.customProperties[PhotonPlayerProperty.name], 0);
        }
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().needChooseSide = true;
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().justSuicide = true;
    }

    public void testVisual(bool setCollider)
    {
        if (setCollider)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.white;
            }
        }
        else
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.black;
            }
        }
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        var view = PhotonView.Find(viewID);
        if (view != null)
        {
            var vector = view.gameObject.transform.position - neck.position;
            if (vector.magnitude < lagMax && !hasDie && Time.time - healthTime > 0.2f)
            {
                healthTime = Time.time;
                if (speed >= RCSettings.damageMode || abnormalType == AbnormalType.TYPE_CRAWLER)
                {
                    currentHealth -= speed;
                }
                if (maxHealth > 0f)
                {
                    photonView.RPC("labelRPC", PhotonTargets.AllBuffered, currentHealth, maxHealth);
                }
                if (currentHealth < 0f)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        OnTitanDie(view);
                    }
                    photonView.RPC("netDie", PhotonTargets.OthersBuffered);
                    if (grabbedTarget != null)
                    {
                        grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All);
                    }
                    netDie();
                    if (nonAI)
                    {
                        FengGameManagerMKII.instance.titanGetKill(view.owner, speed, (string) PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
                    }
                    else
                    {
                        FengGameManagerMKII.instance.titanGetKill(view.owner, speed, name);
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.photonView.RPC("netShowDamage", view.owner, speed);
                }
            }
        }
    }

    public void toCheckPoint(Vector3 targetPt, float r)
    {
        state = TitanState.to_check_point;
        targetCheckPt = targetPt;
        targetR = r;
        crossFade(runAnimation, 0.5f);
    }

    public void toPVPCheckPoint(Vector3 targetPt, float r)
    {
        state = TitanState.to_pvp_pt;
        targetCheckPt = targetPt;
        targetR = r;
        crossFade(runAnimation, 0.5f);
    }

    private void turn(float d)
    {
        if (abnormalType == AbnormalType.TYPE_CRAWLER)
        {
            if (d > 0f)
            {
                turnAnimation = "crawler_turnaround_R";
            }
            else
            {
                turnAnimation = "crawler_turnaround_L";
            }
        }
        else if (d > 0f)
        {
            turnAnimation = "turnaround2";
        }
        else
        {
            turnAnimation = "turnaround1";
        }
        playAnimation(turnAnimation);
        animation[turnAnimation].time = 0f;
        d = Mathf.Clamp(d, -120f, 120f);
        turnDeg = d;
        desDeg = gameObject.transform.rotation.eulerAngles.y + turnDeg;
        state = TitanState.turn;
    }

   
    public void update()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && myDifficulty >= 0 && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine))
        {
            explode();
            if (!nonAI)
            {
                if (activeRad < 0x7fffffff && (state == TitanState.idle || state == TitanState.wander || state == TitanState.chase))
                {
                    if (checkPoints.Count > 1)
                    {
                        if (Vector3.Distance((Vector3) checkPoints[0], baseTransform.position) > activeRad)
                        {
                            toCheckPoint((Vector3) checkPoints[0], 10f);
                        }
                    }
                    else if (Vector3.Distance(spawnPt, baseTransform.position) > activeRad)
                    {
                        toCheckPoint(spawnPt, 10f);
                    }
                }
                if (whoHasTauntMe != null)
                {
                    tauntTime -= Time.deltaTime;
                    if (tauntTime <= 0f)
                    {
                        whoHasTauntMe = null;
                    }
                    myHero = whoHasTauntMe;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                    {
                        object[] parameters = { myHero.GetPhotonView().viewID };
                        photonView.RPC("setMyTarget", PhotonTargets.Others, parameters);
                    }
                }
            }
            if (hasDie)
            {
                dieTime += Time.deltaTime;
                if (dieTime > 2f && !hasDieSteam)
                {
                    hasDieSteam = true;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        var obj2 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie1"));
                        obj2.transform.position = baseTransform.Find("Amarture/Core/Controller_Body/hip").position;
                        obj2.transform.localScale = baseTransform.localScale;
                    }
                    else if (photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie1", baseTransform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = baseTransform.localScale;
                    }
                }
                if (dieTime > 5f)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        var obj3 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie"));
                        obj3.transform.position = baseTransform.Find("Amarture/Core/Controller_Body/hip").position;
                        obj3.transform.localScale = baseTransform.localScale;
                        Destroy(gameObject);
                    }
                    else if (photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie", baseTransform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = baseTransform.localScale;
                        PhotonNetwork.Destroy(gameObject);
                        myDifficulty = -1;
                    }
                }
            }
            else
            {
                if (state == TitanState.hit)
                {
                    if (hitPause > 0f)
                    {
                        hitPause -= Time.deltaTime;
                        if (hitPause <= 0f)
                        {
                            baseAnimation[hitAnimation].speed = 1f;
                            hitPause = 0f;
                        }
                    }
                    if (baseAnimation[hitAnimation].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
                if (!nonAI)
                {
                    if (myHero == null)
                    {
                        findNearestHero();
                    }
                    if ((state == TitanState.idle || state == TitanState.chase || state == TitanState.wander) && whoHasTauntMe == null && Random.Range(0, 100) < 10)
                    {
                        findNearestFacingHero();
                    }
                    if (myHero == null)
                    {
                        myDistance = float.MaxValue;
                    }
                    else
                    {
                        myDistance = Mathf.Sqrt((myHero.transform.position.x - baseTransform.position.x) * (myHero.transform.position.x - baseTransform.position.x) + (myHero.transform.position.z - baseTransform.position.z) * (myHero.transform.position.z - baseTransform.position.z));
                    }
                }
                else
                {
                    if (stamina < maxStamina)
                    {
                        if (baseAnimation.IsPlaying("idle"))
                        {
                            stamina += Time.deltaTime * 30f;
                        }
                        if (baseAnimation.IsPlaying("crawler_idle"))
                        {
                            stamina += Time.deltaTime * 35f;
                        }
                        if (baseAnimation.IsPlaying("run_walk"))
                        {
                            stamina += Time.deltaTime * 10f;
                        }
                    }
                    if (baseAnimation.IsPlaying("run_abnormal_1"))
                    {
                        stamina -= Time.deltaTime * 5f;
                    }
                    if (baseAnimation.IsPlaying("crawler_run"))
                    {
                        stamina -= Time.deltaTime * 15f;
                    }
                    if (stamina < 0f)
                    {
                        stamina = 0f;
                    }
                    if (!IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        GGM.Caching.GameObjectCache.Find("stamina_titan").transform.localScale = new Vector3(stamina, 16f);
                    }
                }
                if (state == TitanState.laugh)
                {
                    if (baseAnimation["laugh"].normalizedTime >= 1f)
                    {
                        idle(2f);
                    }
                }
                else if (state == TitanState.idle)
                {
                    if (nonAI)
                    {
                        if (!IN_GAME_MAIN_CAMERA.isPausing)
                        {
                            pt();
                            if (abnormalType != AbnormalType.TYPE_CRAWLER)
                            {
                                if (controller.isAttackDown && stamina > 25f)
                                {
                                    stamina -= 25f;
                                    attack("combo_1");
                                }
                                else if (controller.isAttackIIDown && stamina > 50f)
                                {
                                    stamina -= 50f;
                                    attack("abnormal_jump");
                                }
                                else if (controller.isJumpDown && stamina > 15f)
                                {
                                    stamina -= 15f;
                                    attack("jumper_0");
                                }
                            }
                            else if (controller.isAttackDown && stamina > 40f)
                            {
                                stamina -= 40f;
                                attack("crawler_jump_0");
                            }
                            if (controller.isSuicide)
                            {
                                suicide();
                            }
                        }
                    }
                    else if (sbtime > 0f)
                    {
                        sbtime -= Time.deltaTime;
                    }
                    else
                    {
                        if (!isAlarm)
                        {
                            if (abnormalType != AbnormalType.TYPE_PUNK && abnormalType != AbnormalType.TYPE_CRAWLER && Random.Range(0f, 1f) < 0.005f)
                            {
                                sitdown();
                                return;
                            }
                            if (Random.Range(0f, 1f) < 0.02f)
                            {
                                wander();
                                return;
                            }
                            if (Random.Range(0f, 1f) < 0.01f)
                            {
                                turn(Random.Range(30, 120));
                                return;
                            }
                            if (Random.Range(0f, 1f) < 0.01f)
                            {
                                turn(Random.Range(-30, -120));
                                return;
                            }
                        }
                        angle = 0f;
                        between2 = 0f;
                        if (myDistance < chaseDistance || whoHasTauntMe != null)
                        {
                            var vector = myHero.transform.position - baseTransform.position;
                            angle = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
                            between2 = -Mathf.DeltaAngle(angle, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                            if (myDistance >= attackDistance)
                            {
                                if (isAlarm || Mathf.Abs(between2) < 90f)
                                {
                                    chase();
                                    return;
                                }
                                if (!(isAlarm || myDistance >= chaseDistance * 0.1f))
                                {
                                    chase();
                                    return;
                                }
                            }
                        }
                        if (!longRangeAttackCheck())
                        {
                            if (myDistance < chaseDistance)
                            {
                                if (abnormalType == AbnormalType.TYPE_JUMPER && (myDistance > attackDistance || myHero.transform.position.y > head.position.y + 4f * myLevel) && Mathf.Abs(between2) < 120f && Vector3.Distance(baseTransform.position, myHero.transform.position) < 1.5f * myHero.transform.position.y)
                                {
                                    attack("jumper_0");
                                    return;
                                }
                                if (abnormalType == AbnormalType.TYPE_CRAWLER && myDistance < attackDistance * 3f && Mathf.Abs(between2) < 90f && myHero.transform.position.y < neck.position.y + 30f * myLevel && myHero.transform.position.y > neck.position.y + 10f * myLevel)
                                {
                                    attack("crawler_jump_0");
                                    return;
                                }
                            }
                            if (abnormalType == AbnormalType.TYPE_PUNK && myDistance < 90f && Mathf.Abs(between2) > 90f)
                            {
                                if (Random.Range(0f, 1f) < 0.4f)
                                {
                                    randomRun(baseTransform.position + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f)), 10f);
                                }
                                if (Random.Range(0f, 1f) < 0.2f)
                                {
                                    recover();
                                }
                                else if (Random.Range(0, 2) == 0)
                                {
                                    attack("quick_turn_l");
                                }
                                else
                                {
                                    attack("quick_turn_r");
                                }
                            }
                            else
                            {
                                if (myDistance < attackDistance)
                                {
                                    if (abnormalType == AbnormalType.TYPE_CRAWLER)
                                    {
                                        if (myHero.transform.position.y + 3f <= neck.position.y + 20f * myLevel && Random.Range(0f, 1f) < 0.1f)
                                        {
                                            chase();
                                        }
                                        return;
                                    }
                                    var decidedAction = string.Empty;
                                    var attackStrategy = GetAttackStrategy();
                                    if (attackStrategy != null)
                                    {
                                        decidedAction = attackStrategy[Random.Range(0, attackStrategy.Length)];
                                    }
                                    if ((abnormalType == AbnormalType.TYPE_JUMPER || abnormalType == AbnormalType.TYPE_I) && Mathf.Abs(between2) > 40f)
                                    {
                                        if (decidedAction.Contains("grab") || decidedAction.Contains("kick") || decidedAction.Contains("slap") || decidedAction.Contains("bite"))
                                        {
                                            if (Random.Range(0, 100) < 30)
                                            {
                                                turn(between2);
                                                return;
                                            }
                                        }
                                        else if (Random.Range(0, 100) < 90)
                                        {
                                            turn(between2);
                                            return;
                                        }
                                    }
                                    if (executeAttack(decidedAction))
                                    {
                                        return;
                                    }
                                    if (abnormalType == AbnormalType.NORMAL)
                                    {
                                        if (Random.Range(0, 100) < 30 && Mathf.Abs(between2) > 45f)
                                        {
                                            turn(between2);
                                            return;
                                        }
                                    }
                                    else if (Mathf.Abs(between2) > 45f)
                                    {
                                        turn(between2);
                                        return;
                                    }
                                }
                                if (PVPfromCheckPt != null)
                                {
                                    if (PVPfromCheckPt.state == CheckPointState.Titan)
                                    {
                                        GameObject chkPtNext;
                                        if (Random.Range(0, 100) > 0x30)
                                        {
                                            chkPtNext = PVPfromCheckPt.chkPtNext;
                                            if (chkPtNext != null && (chkPtNext.GetComponent<PVPcheckPoint>().state != CheckPointState.Titan || Random.Range(0, 100) < 20))
                                            {
                                                toPVPCheckPoint(chkPtNext.transform.position, 5 + Random.Range(0, 10));
                                                PVPfromCheckPt = chkPtNext.GetComponent<PVPcheckPoint>();
                                            }
                                        }
                                        else
                                        {
                                            chkPtNext = PVPfromCheckPt.chkPtPrevious;
                                            if (chkPtNext != null && (chkPtNext.GetComponent<PVPcheckPoint>().state != CheckPointState.Titan || Random.Range(0, 100) < 5))
                                            {
                                                toPVPCheckPoint(chkPtNext.transform.position, 5 + Random.Range(0, 10));
                                                PVPfromCheckPt = chkPtNext.GetComponent<PVPcheckPoint>();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        toPVPCheckPoint(PVPfromCheckPt.transform.position, 5 + Random.Range(0, 10));
                                    }
                                }
                            }
                        }
                    }
                }
                else if (state == TitanState.attack)
                {
                    if (attackAnimation == "combo")
                    {
                        if (nonAI)
                        {
                            if (controller.isAttackDown)
                            {
                                nonAIcombo = true;
                            }
                            if (!(nonAIcombo || baseAnimation["attack_" + attackAnimation].normalizedTime < 0.385f))
                            {
                                idle();
                                return;
                            }
                        }
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.11f && baseAnimation["attack_" + attackAnimation].normalizedTime <= 0.16f)
                        {
                            var obj5 = checkIfHitHand(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001"));
                            if (obj5 != null)
                            {
                                var position = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    obj5.GetComponent<HERO>().die((obj5.transform.position - position) * 15f * myLevel, false);
                                }
                                else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj5.GetComponent<HERO>().HasDied()))
                                {
                                    obj5.GetComponent<HERO>().markDie();
                                    object[] objArray3 = { (obj5.transform.position - position) * 15f * myLevel, false, !nonAI ? -1 : photonView.viewID, name, true };
                                    obj5.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray3);
                                }
                            }
                        }
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.27f && baseAnimation["attack_" + attackAnimation].normalizedTime <= 0.32f)
                        {
                            var obj6 = checkIfHitHand(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001"));
                            if (obj6 != null)
                            {
                                var vector3 = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    obj6.GetComponent<HERO>().die((obj6.transform.position - vector3) * 15f * myLevel, false);
                                }
                                else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj6.GetComponent<HERO>().HasDied()))
                                {
                                    obj6.GetComponent<HERO>().markDie();
                                    object[] objArray4 = { (obj6.transform.position - vector3) * 15f * myLevel, false, !nonAI ? -1 : photonView.viewID, name, true };
                                    obj6.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray4);
                                }
                            }
                        }
                    }
                    if (attackCheckTimeA != 0f && baseAnimation["attack_" + attackAnimation].normalizedTime >= attackCheckTimeA && baseAnimation["attack_" + attackAnimation].normalizedTime <= attackCheckTimeB)
                    {
                        if (leftHandAttack)
                        {
                            var obj7 = checkIfHitHand(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001"));
                            if (obj7 != null)
                            {
                                var vector4 = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    obj7.GetComponent<HERO>().die((obj7.transform.position - vector4) * 15f * myLevel, false);
                                }
                                else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj7.GetComponent<HERO>().HasDied()))
                                {
                                    obj7.GetComponent<HERO>().markDie();
                                    object[] objArray5 = { (obj7.transform.position - vector4) * 15f * myLevel, false, !nonAI ? -1 : photonView.viewID, name, true };
                                    obj7.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray5);
                                }
                            }
                        }
                        else
                        {
                            var obj8 = checkIfHitHand(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001"));
                            if (obj8 != null)
                            {
                                var vector5 = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    obj8.GetComponent<HERO>().die((obj8.transform.position - vector5) * 15f * myLevel, false);
                                }
                                else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj8.GetComponent<HERO>().HasDied()))
                                {
                                    obj8.GetComponent<HERO>().markDie();
                                    object[] objArray6 = { (obj8.transform.position - vector5) * 15f * myLevel, false, !nonAI ? -1 : photonView.viewID, name, true };
                                    obj8.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray6);
                                }
                            }
                        }
                    }
                    if (!attacked && attackCheckTime != 0f && baseAnimation["attack_" + attackAnimation].normalizedTime >= attackCheckTime)
                    {
                        GameObject obj9;
                        attacked = true;
                        fxPosition = baseTransform.Find("ap_" + attackAnimation).position;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                        {
                            obj9 = PhotonNetwork.Instantiate("FX/" + fxName, fxPosition, fxRotation, 0);
                        }
                        else
                        {
                            obj9 = (GameObject) Instantiate(Resources.Load("FX/" + fxName), fxPosition, fxRotation);
                        }
                        if (nonAI)
                        {
                            obj9.transform.localScale = baseTransform.localScale * 1.5f;
                            if (obj9.GetComponent<EnemyfxIDcontainer>() != null)
                            {
                                obj9.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = photonView.viewID;
                            }
                        }
                        else
                        {
                            obj9.transform.localScale = baseTransform.localScale;
                        }
                        if (obj9.GetComponent<EnemyfxIDcontainer>() != null)
                        {
                            obj9.GetComponent<EnemyfxIDcontainer>().titanName = name;
                        }
                        var b = 1f - Vector3.Distance(currentCamera.transform.position, obj9.transform.position) * 0.05f;
                        b = Mathf.Min(1f, b);
                        currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(b, b);
                    }
                    if (attackAnimation == "throw")
                    {
                        if (!attacked && baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.11f)
                        {
                            attacked = true;
                            var transform = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                            {
                                throwRock = PhotonNetwork.Instantiate("FX/rockThrow", transform.position, transform.rotation, 0);
                            }
                            else
                            {
                                throwRock = (GameObject) Instantiate(Resources.Load("FX/rockThrow"), transform.position, transform.rotation);
                            }
                            throwRock.transform.localScale = baseTransform.localScale;
                            var transform1 = throwRock.transform;
                            transform1.position -= throwRock.transform.forward * 2.5f * myLevel;
                            if (throwRock.GetComponent<EnemyfxIDcontainer>() != null)
                            {
                                if (nonAI)
                                {
                                    throwRock.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = photonView.viewID;
                                }
                                throwRock.GetComponent<EnemyfxIDcontainer>().titanName = name;
                            }
                            throwRock.transform.parent = transform;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                            {
                                object[] objArray7 = { photonView.viewID, baseTransform.localScale, throwRock.transform.localPosition, myLevel };
                                throwRock.GetPhotonView().RPC("initRPC", PhotonTargets.Others, objArray7);
                            }
                        }
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.11f)
                        {
                            var y = Mathf.Atan2(myHero.transform.position.x - baseTransform.position.x, myHero.transform.position.z - baseTransform.position.z) * 57.29578f;
                            baseGameObjectTransform.rotation = Quaternion.Euler(0f, y, 0f);
                        }
                        if (throwRock != null && baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.62f)
                        {
                            Vector3 vector6;
                            var num3 = 1f;
                            var num4 = -20f;
                            if (myHero != null)
                            {
                                vector6 = (myHero.transform.position - throwRock.transform.position) / num3 + myHero.rigidbody.velocity;
                                var num5 = myHero.transform.position.y + 2f * myLevel;
                                var num6 = num5 - throwRock.transform.position.y;
                                vector6 = new Vector3(vector6.x, num6 / num3 - 0.5f * num4 * num3, vector6.z);
                            }
                            else
                            {
                                vector6 = baseTransform.forward * 60f + Vector3.up * 10f;
                            }
                            throwRock.GetComponent<RockThrow>().launch(vector6);
                            throwRock.transform.parent = null;
                            throwRock = null;
                        }
                    }
                    if (attackAnimation == "jumper_0" || attackAnimation == "crawler_jump_0")
                    {
                        if (!attacked)
                        {
                            if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 0.68f)
                            {
                                attacked = true;
                                if (myHero == null || nonAI)
                                {
                                    var num7 = 120f;
                                    var vector7 = baseTransform.forward * speed + Vector3.up * num7;
                                    if (nonAI && abnormalType == AbnormalType.TYPE_CRAWLER)
                                    {
                                        num7 = 100f;
                                        var a = speed * 2.5f;
                                        a = Mathf.Min(a, 100f);
                                        vector7 = baseTransform.forward * a + Vector3.up * num7;
                                    }
                                    baseRigidBody.velocity = vector7;
                                }
                                else
                                {
                                    float num18;
                                    var num9 = myHero.rigidbody.velocity.y;
                                    var num10 = -20f;
                                    var gravity = this.gravity;
                                    var num12 = neck.position.y;
                                    var num13 = (num10 - gravity) * 0.5f;
                                    var num14 = num9;
                                    var num15 = myHero.transform.position.y - num12;
                                    var num16 = Mathf.Abs((Mathf.Sqrt(num14 * num14 - 4f * num13 * num15) - num14) / (2f * num13));
                                    var vector8 = myHero.transform.position + myHero.rigidbody.velocity * num16 + Vector3.up * 0.5f * num10 * num16 * num16;
                                    var num17 = vector8.y;
                                    if (num15 < 0f || num17 - num12 < 0f)
                                    {
                                        num18 = 60f;
                                        var num19 = speed * 2.5f;
                                        num19 = Mathf.Min(num19, 100f);
                                        var vector9 = baseTransform.forward * num19 + Vector3.up * num18;
                                        baseRigidBody.velocity = vector9;
                                        return;
                                    }
                                    var num20 = num17 - num12;
                                    var num21 = Mathf.Sqrt(2f * num20 / this.gravity);
                                    num18 = this.gravity * num21;
                                    num18 = Mathf.Max(30f, num18);
                                    var vector10 = (vector8 - baseTransform.position) / num16;
                                    abnorma_jump_bite_horizon_v = new Vector3(vector10.x, 0f, vector10.z);
                                    var velocity = baseRigidBody.velocity;
                                    var force = new Vector3(abnorma_jump_bite_horizon_v.x, velocity.y, abnorma_jump_bite_horizon_v.z) - velocity;
                                    baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                                    baseRigidBody.AddForce(Vector3.up * num18, ForceMode.VelocityChange);
                                    var num22 = Vector2.Angle(new Vector2(baseTransform.position.x, baseTransform.position.z), new Vector2(myHero.transform.position.x, myHero.transform.position.z));
                                    num22 = Mathf.Atan2(myHero.transform.position.x - baseTransform.position.x, myHero.transform.position.z - baseTransform.position.z) * 57.29578f;
                                    baseGameObjectTransform.rotation = Quaternion.Euler(0f, num22, 0f);
                                }
                            }
                            else
                            {
                                baseRigidBody.velocity = Vector3.zero;
                            }
                        }
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 1f)
                        {
                            Debug.DrawLine(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").position + Vector3.up * 1.5f * myLevel, baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").position + Vector3.up * 1.5f * myLevel + Vector3.up * 3f * myLevel, Color.green);
                            Debug.DrawLine(baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").position + Vector3.up * 1.5f * myLevel, baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").position + Vector3.up * 1.5f * myLevel + Vector3.forward * 3f * myLevel, Color.green);
                            var obj10 = checkIfHitHead(head, 3f);
                            if (obj10 != null)
                            {
                                var vector13 = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    obj10.GetComponent<HERO>().die((obj10.transform.position - vector13) * 15f * myLevel, false);
                                }
                                else if (!(IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || !photonView.isMine || obj10.GetComponent<HERO>().HasDied()))
                                {
                                    obj10.GetComponent<HERO>().markDie();
                                    object[] objArray8 = { (obj10.transform.position - vector13) * 15f * myLevel, true, !nonAI ? -1 : photonView.viewID, name, true };
                                    obj10.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray8);
                                }
                                if (abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    attackAnimation = "crawler_jump_1";
                                }
                                else
                                {
                                    attackAnimation = "jumper_1";
                                }
                                playAnimation("attack_" + attackAnimation);
                            }
                            if (Mathf.Abs(baseRigidBody.velocity.y) < 0.5f || baseRigidBody.velocity.y < 0f || IsGrounded())
                            {
                                if (abnormalType == AbnormalType.TYPE_CRAWLER)
                                {
                                    attackAnimation = "crawler_jump_1";
                                }
                                else
                                {
                                    attackAnimation = "jumper_1";
                                }
                                playAnimation("attack_" + attackAnimation);
                            }
                        }
                    }
                    else if (attackAnimation == "jumper_1" || attackAnimation == "crawler_jump_1")
                    {
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 1f && grounded)
                        {
                            GameObject obj11;
                            if (abnormalType == AbnormalType.TYPE_CRAWLER)
                            {
                                attackAnimation = "crawler_jump_2";
                            }
                            else
                            {
                                attackAnimation = "jumper_2";
                            }
                            crossFade("attack_" + attackAnimation, 0.1f);
                            fxPosition = baseTransform.position;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                            {
                                obj11 = PhotonNetwork.Instantiate("FX/boom2", fxPosition, fxRotation, 0);
                            }
                            else
                            {
                                obj11 = (GameObject) Instantiate(Resources.Load("FX/boom2"), fxPosition, fxRotation);
                            }
                            obj11.transform.localScale = baseTransform.localScale * 1.6f;
                            var num23 = 1f - Vector3.Distance(currentCamera.transform.position, obj11.transform.position) * 0.05f;
                            num23 = Mathf.Min(1f, num23);
                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(num23, num23);
                        }
                    }
                    else if (attackAnimation == "jumper_2" || attackAnimation == "crawler_jump_2")
                    {
                        if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 1f)
                        {
                            idle();
                        }
                    }
                    else if (baseAnimation.IsPlaying("tired"))
                    {
                        if (baseAnimation["tired"].normalizedTime >= 1f + Mathf.Max(attackEndWait * 2f, 3f))
                        {
                            idle(Random.Range(attackWait - 1f, 3f));
                        }
                    }
                    else if (baseAnimation["attack_" + attackAnimation].normalizedTime >= 1f + attackEndWait)
                    {
                        if (nextAttackAnimation != null)
                        {
                            attack(nextAttackAnimation);
                        }
                        else if (attackAnimation == "quick_turn_l" || attackAnimation == "quick_turn_r")
                        {
                            baseTransform.rotation = Quaternion.Euler(baseTransform.rotation.eulerAngles.x, baseTransform.rotation.eulerAngles.y + 180f, baseTransform.rotation.eulerAngles.z);
                            idle(Random.Range(0.5f, 1f));
                            playAnimation("idle");
                        }
                        else if (abnormalType == AbnormalType.TYPE_I || abnormalType == AbnormalType.TYPE_JUMPER)
                        {
                            attackCount++;
                            if (attackCount > 3 && attackAnimation == "abnormal_getup")
                            {
                                attackCount = 0;
                                crossFade("tired", 0.5f);
                            }
                            else
                            {
                                idle(Random.Range(attackWait - 1f, 3f));
                            }
                        }
                        else
                        {
                            idle(Random.Range(attackWait - 1f, 3f));
                        }
                    }
                }
                else if (state == TitanState.grab)
                {
                    if (baseAnimation["grab_" + attackAnimation].normalizedTime >= attackCheckTimeA && baseAnimation["grab_" + attackAnimation].normalizedTime <= attackCheckTimeB && grabbedTarget == null)
                    {
                        var grabTarget = checkIfHitHand(currentGrabHand);
                        if (grabTarget != null)
                        {
                            if (isGrabHandLeft)
                            {
                                eatSetL(grabTarget);
                                grabbedTarget = grabTarget;
                            }
                            else
                            {
                                eatSet(grabTarget);
                                grabbedTarget = grabTarget;
                            }
                        }
                    }
                    if (baseAnimation["grab_" + attackAnimation].normalizedTime >= 1f)
                    {
                        if (grabbedTarget != null)
                        {
                            eat();
                        }
                        else
                        {
                            idle(Random.Range(attackWait - 1f, 2f));
                        }
                    }
                }
                else if (state == TitanState.eat)
                {
                    if (!(attacked || baseAnimation[attackAnimation].normalizedTime < 0.48f))
                    {
                        attacked = true;
                        justEatHero(grabbedTarget, currentGrabHand);
                    }
                    if (grabbedTarget == null)
                    {
                    }
                    if (baseAnimation[attackAnimation].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.chase)
                {
                    if (myHero == null)
                    {
                        idle();
                    }
                    else if (!longRangeAttackCheck())
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE && PVPfromCheckPt != null && myDistance > chaseDistance)
                        {
                            idle();
                        }
                        else if (abnormalType == AbnormalType.TYPE_CRAWLER)
                        {
                            var vector14 = myHero.transform.position - baseTransform.position;
                            var current = -Mathf.Atan2(vector14.z, vector14.x) * 57.29578f;
                            var f = -Mathf.DeltaAngle(current, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                            if (myDistance < attackDistance * 3f && Random.Range(0f, 1f) < 0.1f && Mathf.Abs(f) < 90f && myHero.transform.position.y < neck.position.y + 30f * myLevel && myHero.transform.position.y > neck.position.y + 10f * myLevel)
                            {
                                attack("crawler_jump_0");
                            }
                            else
                            {
                                var obj13 = checkIfHitCrawlerMouth(head, 2.2f);
                                if (obj13 != null)
                                {
                                    var vector15 = baseTransform.Find("Amarture/Core/Controller_Body/hip/spine/chest").position;
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        obj13.GetComponent<HERO>().die((obj13.transform.position - vector15) * 15f * myLevel, false);
                                    }
                                    else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                                    {
                                        if (obj13.GetComponent<TITAN_EREN>() != null)
                                        {
                                            obj13.GetComponent<TITAN_EREN>().hitByTitan();
                                        }
                                        else if (!obj13.GetComponent<HERO>().HasDied())
                                        {
                                            obj13.GetComponent<HERO>().markDie();
                                            object[] objArray9 = { (obj13.transform.position - vector15) * 15f * myLevel, true, !nonAI ? -1 : photonView.viewID, name, true };
                                            obj13.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, objArray9);
                                        }
                                    }
                                }
                                if (myDistance < attackDistance && Random.Range(0f, 1f) < 0.02f)
                                {
                                    idle(Random.Range(0.05f, 0.2f));
                                }
                            }
                        }
                        else if (abnormalType == AbnormalType.TYPE_JUMPER && (myDistance > attackDistance && myHero.transform.position.y > head.position.y + 4f * myLevel || myHero.transform.position.y > head.position.y + 4f * myLevel) && Vector3.Distance(baseTransform.position, myHero.transform.position) < 1.5f * myHero.transform.position.y)
                        {
                            attack("jumper_0");
                        }
                        else if (myDistance < attackDistance)
                        {
                            idle(Random.Range(0.05f, 0.2f));
                        }
                    }
                }
                else if (state == TitanState.wander)
                {
                    var num26 = 0f;
                    var num27 = 0f;
                    if (myDistance < chaseDistance || whoHasTauntMe != null)
                    {
                        var vector16 = myHero.transform.position - baseTransform.position;
                        num26 = -Mathf.Atan2(vector16.z, vector16.x) * 57.29578f;
                        num27 = -Mathf.DeltaAngle(num26, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                        if (isAlarm || Mathf.Abs(num27) < 90f)
                        {
                            chase();
                            return;
                        }
                        if (!(isAlarm || myDistance >= chaseDistance * 0.1f))
                        {
                            chase();
                            return;
                        }
                    }
                    if (Random.Range(0f, 1f) < 0.01f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.turn)
                {
                    baseGameObjectTransform.rotation = Quaternion.Lerp(baseGameObjectTransform.rotation, Quaternion.Euler(0f, desDeg, 0f), Time.deltaTime * Mathf.Abs(turnDeg) * 0.015f);
                    if (baseAnimation[turnAnimation].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.hit_eye)
                {
                    if (baseAnimation.IsPlaying("sit_hit_eye") && baseAnimation["sit_hit_eye"].normalizedTime >= 1f)
                    {
                        remainSitdown();
                    }
                    else if (baseAnimation.IsPlaying("hit_eye") && baseAnimation["hit_eye"].normalizedTime >= 1f)
                    {
                        if (nonAI)
                        {
                            idle();
                        }
                        else
                        {
                            attack("combo_1");
                        }
                    }
                }
                else if (state == TitanState.to_check_point)
                {
                    if (checkPoints.Count <= 0 && myDistance < attackDistance)
                    {
                        var str2 = string.Empty;
                        var strArray2 = GetAttackStrategy();
                        if (strArray2 != null)
                        {
                            str2 = strArray2[Random.Range(0, strArray2.Length)];
                        }
                        if (executeAttack(str2))
                        {
                            return;
                        }
                    }
                    if (Vector3.Distance(baseTransform.position, targetCheckPt) < targetR)
                    {
                        if (checkPoints.Count > 0)
                        {
                            if (checkPoints.Count == 1)
                            {
                                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)
                                {
                                    MultiplayerManager.gameLose();
                                    checkPoints = new ArrayList();
                                    idle();
                                }
                            }
                            else
                            {
                                if (checkPoints.Count == 4)
                                {
                                    InRoomChat.SystemMessageLocal("WARNING!", false);
                                    InRoomChat.SystemMessageLocal("An abnormal titan is approaching the north gate!");
                                }
                                var vector17 = (Vector3) checkPoints[0];
                                targetCheckPt = vector17;
                                checkPoints.RemoveAt(0);
                            }
                        }
                        else
                        {
                            idle();
                        }
                    }
                }
                else if (state == TitanState.to_pvp_pt)
                {
                    if (myDistance < chaseDistance * 0.7f)
                    {
                        chase();
                    }
                    if (Vector3.Distance(baseTransform.position, targetCheckPt) < targetR)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.random_run)
                {
                    random_run_time -= Time.deltaTime;
                    if (Vector3.Distance(baseTransform.position, targetCheckPt) < targetR || random_run_time <= 0f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.down)
                {
                    getdownTime -= Time.deltaTime;
                    if (baseAnimation.IsPlaying("sit_hunt_down") && baseAnimation["sit_hunt_down"].normalizedTime >= 1f)
                    {
                        playAnimation("sit_idle");
                    }
                    if (getdownTime <= 0f)
                    {
                        crossFade("sit_getup", 0.1f);
                    }
                    if (baseAnimation.IsPlaying("sit_getup") && baseAnimation["sit_getup"].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.sit)
                {
                    getdownTime -= Time.deltaTime;
                    angle = 0f;
                    between2 = 0f;
                    if (myDistance < chaseDistance || whoHasTauntMe != null)
                    {
                        if (myDistance < 50f)
                        {
                            isAlarm = true;
                        }
                        else
                        {
                            var vector18 = myHero.transform.position - baseTransform.position;
                            angle = -Mathf.Atan2(vector18.z, vector18.x) * 57.29578f;
                            between2 = -Mathf.DeltaAngle(angle, baseGameObjectTransform.rotation.eulerAngles.y - 90f);
                            if (Mathf.Abs(between2) < 100f)
                            {
                                isAlarm = true;
                            }
                        }
                    }
                    if (baseAnimation.IsPlaying("sit_down") && baseAnimation["sit_down"].normalizedTime >= 1f)
                    {
                        playAnimation("sit_idle");
                    }
                    if ((getdownTime <= 0f || isAlarm) && baseAnimation.IsPlaying("sit_idle"))
                    {
                        crossFade("sit_getup", 0.1f);
                    }
                    if (baseAnimation.IsPlaying("sit_getup") && baseAnimation["sit_getup"].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
                else if (state == TitanState.recover)
                {
                    getdownTime -= Time.deltaTime;
                    if (getdownTime <= 0f)
                    {
                        idle();
                    }
                    if (baseAnimation.IsPlaying("idle_recovery") && baseAnimation["idle_recovery"].normalizedTime >= 1f)
                    {
                        idle();
                    }
                }
            }
        }
    }

    public void updateCollider()
    {
        if (colliderEnabled)
        {
            if (!isHooked && !myTitanTrigger.isCollide && !isLook)
            {
                foreach (var collider in baseColliders)
                {
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }
                }
                colliderEnabled = false;
            }
        }
        else if (isHooked || myTitanTrigger.isCollide || isLook)
        {
            foreach (var collider in baseColliders)
            {
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
            colliderEnabled = true;
        }
    }

    public void updateLabel()
    {
        if (healthLabel != null && healthLabel.GetComponent<UILabel>().isVisible)
        {
            healthLabel.transform.LookAt(2f * healthLabel.transform.position - Camera.main.transform.position);
        }
    }

    private void wander( float sbtime = 0f)
    {
        state = TitanState.wander;
        crossFade(runAnimation, 0.5f);
    }


}

