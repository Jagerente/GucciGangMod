//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using GGM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FEMALE_TITAN : Photon.MonoBehaviour
{
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap1;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap2;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap3;
    private Vector3 abnorma_jump_bite_horizon_v;
    public int AnkleLHP = 200;
    private int AnkleLHPMAX = 200;
    public int AnkleRHP = 200;
    private int AnkleRHPMAX = 200;
    private string attackAnimation;
    private float attackCheckTime;
    private float attackCheckTimeA;
    private float attackCheckTimeB;
    private bool attackChkOnce;
    public float attackDistance = 13f;
    private bool attacked;
    public float attackWait = 1f;
    private float attention = 10f;
    public GameObject bottomObject;
    public float chaseDistance = 80f;
    private Transform checkHitCapsuleEnd;
    private Vector3 checkHitCapsuleEndOld;
    private float checkHitCapsuleR;
    private Transform checkHitCapsuleStart;
    public GameObject currentCamera;
    private Transform currentGrabHand;
    private float desDeg;
    private float dieTime;
    private GameObject eren;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchsmap1;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchsmap2;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchsmap3;
    private string fxName;
    private Vector3 fxPosition;
    private Quaternion fxRotation;
    private GameObject grabbedTarget;
    public GameObject grabTF;
    private float gravity = 120f;
    private bool grounded;
    public bool hasDie;
    private bool hasDieSteam;
    public bool hasspawn;
    public GameObject healthLabel;
    public float healthTime;
    private string hitAnimation;
    private bool isAttackMoveByCore;
    private bool isGrabHandLeft;
    public float lagMax;
    public int maxHealth;
    public float maxVelocityChange = 80f;
    public static float minusDistance = 99999f;
    public static GameObject minusDistanceEnemy;
    public float myDistance;
    public GameObject myHero;
    public int NapeArmor = 0x3e8;
    private bool needFreshCorePosition;
    private string nextAttackAnimation;
    private Vector3 oldCorePosition;
    private float sbtime;
    public float size;
    public float speed = 80f;
    private bool startJump;
    private string state = "idle";
    private int stepSoundPhase = 2;
    private float tauntTime;
    private string turnAnimation;
    private float turnDeg;
    private GameObject whoHasTauntMe;

    private void attack(string type)
    {
        state = "attack";
        attacked = false;
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
        startJump = false;
        attackChkOnce = false;
        nextAttackAnimation = null;
        fxName = null;
        isAttackMoveByCore = false;
        attackCheckTime = 0f;
        attackCheckTimeA = 0f;
        attackCheckTimeB = 0f;
        fxRotation = Quaternion.Euler(270f, 0f, 0f);
        var key = type;
        if (key != null)
        {
            int num;
            if (fswitchSmap2 == null)
            {
                var dictionary = new Dictionary<string, int>(0x11);
                dictionary.Add("combo_1", 0);
                dictionary.Add("combo_2", 1);
                dictionary.Add("combo_3", 2);
                dictionary.Add("combo_blind_1", 3);
                dictionary.Add("combo_blind_2", 4);
                dictionary.Add("combo_blind_3", 5);
                dictionary.Add("front", 6);
                dictionary.Add("jumpCombo_1", 7);
                dictionary.Add("jumpCombo_2", 8);
                dictionary.Add("jumpCombo_3", 9);
                dictionary.Add("jumpCombo_4", 10);
                dictionary.Add("sweep", 11);
                dictionary.Add("sweep_back", 12);
                dictionary.Add("sweep_front_left", 13);
                dictionary.Add("sweep_front_right", 14);
                dictionary.Add("sweep_head_b_l", 15);
                dictionary.Add("sweep_head_b_r", 0x10);
                fswitchSmap2 = dictionary;
            }
            if (fswitchSmap2.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        attackCheckTimeA = 0.63f;
                        attackCheckTimeB = 0.8f;
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R");
                        checkHitCapsuleR = 5f;
                        isAttackMoveByCore = true;
                        nextAttackAnimation = "combo_2";
                        break;

                    case 1:
                        attackCheckTimeA = 0.27f;
                        attackCheckTimeB = 0.43f;
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/thigh_L/shin_L/foot_L");
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/thigh_L");
                        checkHitCapsuleR = 5f;
                        isAttackMoveByCore = true;
                        nextAttackAnimation = "combo_3";
                        break;

                    case 2:
                        attackCheckTimeA = 0.15f;
                        attackCheckTimeB = 0.3f;
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R");
                        checkHitCapsuleR = 5f;
                        isAttackMoveByCore = true;
                        break;

                    case 3:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.72f;
                        attackCheckTimeB = 0.83f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        nextAttackAnimation = "combo_blind_2";
                        break;

                    case 4:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.5f;
                        attackCheckTimeB = 0.6f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        nextAttackAnimation = "combo_blind_3";
                        break;

                    case 5:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.2f;
                        attackCheckTimeB = 0.28f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 6:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.44f;
                        attackCheckTimeB = 0.55f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 7:
                        isAttackMoveByCore = false;
                        nextAttackAnimation = "jumpCombo_2";
                        abnorma_jump_bite_horizon_v = Vector3.zero;
                        break;

                    case 8:
                        isAttackMoveByCore = false;
                        attackCheckTimeA = 0.48f;
                        attackCheckTimeB = 0.7f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        nextAttackAnimation = "jumpCombo_3";
                        break;

                    case 9:
                        isAttackMoveByCore = false;
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/thigh_L/shin_L/foot_L");
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/thigh_L");
                        checkHitCapsuleR = 5f;
                        attackCheckTimeA = 0.22f;
                        attackCheckTimeB = 0.42f;
                        break;

                    case 10:
                        isAttackMoveByCore = false;
                        break;

                    case 11:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.39f;
                        attackCheckTimeB = 0.6f;
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R");
                        checkHitCapsuleR = 5f;
                        break;

                    case 12:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.41f;
                        attackCheckTimeB = 0.48f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 13:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.53f;
                        attackCheckTimeB = 0.63f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 14:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.5f;
                        attackCheckTimeB = 0.62f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 15:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.4f;
                        attackCheckTimeB = 0.51f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001");
                        checkHitCapsuleR = 4f;
                        break;

                    case 0x10:
                        isAttackMoveByCore = true;
                        attackCheckTimeA = 0.4f;
                        attackCheckTimeB = 0.51f;
                        checkHitCapsuleStart = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
                        checkHitCapsuleEnd = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                        checkHitCapsuleR = 4f;
                        break;
                }
            }
        }
        checkHitCapsuleEndOld = checkHitCapsuleEnd.transform.position;
        needFreshCorePosition = true;
    }

    private bool attackTarget(GameObject target)
    {
        int num5;
        var current = 0f;
        var f = 0f;
        var vector = target.transform.position - transform.position;
        current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        f = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
        if ((eren != null) && (myDistance < 35f))
        {
            attack("combo_1");
            return true;
        }
        var num3 = 0;
        var str = string.Empty;
        var list = new ArrayList();
        if (myDistance < 40f)
        {
            if (Mathf.Abs(f) < 90f)
            {
                if (f > 0f)
                {
                    num3 = 1;
                }
                else
                {
                    num3 = 2;
                }
            }
            else if (f > 0f)
            {
                num3 = 4;
            }
            else
            {
                num3 = 3;
            }
            var num4 = target.transform.position.y - transform.position.y;
            if (Mathf.Abs(f) < 90f)
            {
                if (((num4 > 0f) && (num4 < 12f)) && (myDistance < 22f))
                {
                    list.Add("attack_sweep");
                }
                if ((num4 >= 55f) && (num4 < 90f))
                {
                    list.Add("attack_jumpCombo_1");
                }
            }
            if (((Mathf.Abs(f) < 90f) && (num4 > 12f)) && (num4 < 40f))
            {
                list.Add("attack_combo_1");
            }
            if (Mathf.Abs(f) < 30f)
            {
                if (((num4 > 0f) && (num4 < 12f)) && ((myDistance > 20f) && (myDistance < 30f)))
                {
                    list.Add("attack_front");
                }
                if (((myDistance < 12f) && (num4 > 33f)) && (num4 < 51f))
                {
                    list.Add("grab_up");
                }
            }
            if (((Mathf.Abs(f) > 100f) && (myDistance < 11f)) && ((num4 >= 15f) && (num4 < 32f)))
            {
                list.Add("attack_sweep_back");
            }
            num5 = num3;
            switch (num5)
            {
                case 1:
                    if (myDistance >= 11f)
                    {
                        if (myDistance < 20f)
                        {
                            if ((num4 >= 12f) && (num4 < 21f))
                            {
                                list.Add("grab_bottom_right");
                            }
                            else if ((num4 >= 21f) && (num4 < 32f))
                            {
                                list.Add("grab_mid_right");
                            }
                            else if ((num4 >= 32f) && (num4 < 47f))
                            {
                                list.Add("grab_up_right");
                            }
                        }
                        break;
                    }
                    if ((num4 >= 21f) && (num4 < 32f))
                    {
                        list.Add("attack_sweep_front_right");
                    }
                    break;

                case 2:
                    if (myDistance >= 11f)
                    {
                        if (myDistance < 20f)
                        {
                            if ((num4 >= 12f) && (num4 < 21f))
                            {
                                list.Add("grab_bottom_left");
                            }
                            else if ((num4 >= 21f) && (num4 < 32f))
                            {
                                list.Add("grab_mid_left");
                            }
                            else if ((num4 >= 32f) && (num4 < 47f))
                            {
                                list.Add("grab_up_left");
                            }
                        }
                        break;
                    }
                    if ((num4 >= 21f) && (num4 < 32f))
                    {
                        list.Add("attack_sweep_front_left");
                    }
                    break;

                case 3:
                    if (myDistance >= 11f)
                    {
                        list.Add("turn180");
                        break;
                    }
                    if ((num4 >= 33f) && (num4 < 51f))
                    {
                        list.Add("attack_sweep_head_b_l");
                    }
                    break;

                case 4:
                    if (myDistance >= 11f)
                    {
                        list.Add("turn180");
                        break;
                    }
                    if ((num4 >= 33f) && (num4 < 51f))
                    {
                        list.Add("attack_sweep_head_b_r");
                    }
                    break;
            }
        }
        if (list.Count > 0)
        {
            str = (string) list[UnityEngine.Random.Range(0, list.Count)];
        }
        else if (UnityEngine.Random.Range(0, 100) < 10)
        {
            var objArray = GameObject.FindGameObjectsWithTag("Player");
            myHero = objArray[UnityEngine.Random.Range(0, objArray.Length)];
            attention = UnityEngine.Random.Range(5f, 10f);
            return true;
        }
        var key = str;
        if (key != null)
        {
            if (fswitchSmap1 == null)
            {
                var dictionary = new Dictionary<string, int>(0x11);
                dictionary.Add("grab_bottom_left", 0);
                dictionary.Add("grab_bottom_right", 1);
                dictionary.Add("grab_mid_left", 2);
                dictionary.Add("grab_mid_right", 3);
                dictionary.Add("grab_up", 4);
                dictionary.Add("grab_up_left", 5);
                dictionary.Add("grab_up_right", 6);
                dictionary.Add("attack_combo_1", 7);
                dictionary.Add("attack_front", 8);
                dictionary.Add("attack_jumpCombo_1", 9);
                dictionary.Add("attack_sweep", 10);
                dictionary.Add("attack_sweep_back", 11);
                dictionary.Add("attack_sweep_front_left", 12);
                dictionary.Add("attack_sweep_front_right", 13);
                dictionary.Add("attack_sweep_head_b_l", 14);
                dictionary.Add("attack_sweep_head_b_r", 15);
                dictionary.Add("turn180", 0x10);
                fswitchSmap1 = dictionary;
            }
            if (fswitchSmap1.TryGetValue(key, out num5))
            {
                switch (num5)
                {
                    case 0:
                        grab("bottom_left");
                        return true;

                    case 1:
                        grab("bottom_right");
                        return true;

                    case 2:
                        grab("mid_left");
                        return true;

                    case 3:
                        grab("mid_right");
                        return true;

                    case 4:
                        grab("up");
                        return true;

                    case 5:
                        grab("up_left");
                        return true;

                    case 6:
                        grab("up_right");
                        return true;

                    case 7:
                        attack("combo_1");
                        return true;

                    case 8:
                        attack("front");
                        return true;

                    case 9:
                        attack("jumpCombo_1");
                        return true;

                    case 10:
                        attack("sweep");
                        return true;

                    case 11:
                        attack("sweep_back");
                        return true;

                    case 12:
                        attack("sweep_front_left");
                        return true;

                    case 13:
                        attack("sweep_front_right");
                        return true;

                    case 14:
                        attack("sweep_head_b_l");
                        return true;

                    case 15:
                        attack("sweep_head_b_r");
                        return true;

                    case 0x10:
                        turn180();
                        return true;
                }
            }
        }
        return false;
    }

    private void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    public void beTauntedBy(GameObject target, float tauntTime)
    {
        whoHasTauntMe = target;
        this.tauntTime = tauntTime;
    }

    private void chase()
    {
        state = "chase";
        crossFade("run", 0.5f);
    }

    private RaycastHit[] checkHitCapsule(Vector3 start, Vector3 end, float r)
    {
        return Physics.SphereCastAll(start, r, end - start, Vector3.Distance(start, end));
    }

    private GameObject checkIfHitHand(Transform hand)
    {
        var num = 9.6f;
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
                if ((gameObject.GetComponent<HERO>() != null) && !gameObject.GetComponent<HERO>().isInvincible())
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    private GameObject checkIfHitHead(Transform head, float rad)
    {
        var num = rad * 4f;
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
        {
            if ((obj2.GetComponent<TITAN_EREN>() == null) && !obj2.GetComponent<HERO>().isInvincible())
            {
                var num3 = obj2.GetComponent<CapsuleCollider>().height * 0.5f;
                if (Vector3.Distance(obj2.transform.position + Vector3.up * num3, head.transform.position + (Vector3.up * 1.5f) * 4f) < (num + num3))
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
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            var parameters = new object[] { aniName, time };
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    private void eatSet(GameObject grabTarget)
    {
        if (!grabTarget.GetComponent<HERO>().isGrabbed)
        {
            grabToRight();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                var parameters = new object[] { photonView.viewID, false };
                grabTarget.GetPhotonView().RPC("netGrabbed", PhotonTargets.All, parameters);
                var objArray2 = new object[] { "grabbed" };
                grabTarget.GetPhotonView().RPC("netPlayAnimation", PhotonTargets.All, objArray2);
                photonView.RPC("grabToRight", PhotonTargets.Others, new object[0]);
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
        if (!grabTarget.GetComponent<HERO>().isGrabbed)
        {
            grabToLeft();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                var parameters = new object[] { photonView.viewID, true };
                grabTarget.GetPhotonView().RPC("netGrabbed", PhotonTargets.All, parameters);
                var objArray2 = new object[] { "grabbed" };
                grabTarget.GetPhotonView().RPC("netPlayAnimation", PhotonTargets.All, objArray2);
                photonView.RPC("grabToLeft", PhotonTargets.Others, new object[0]);
            }
            else
            {
                grabTarget.GetComponent<HERO>().grabbed(gameObject, true);
                grabTarget.GetComponent<HERO>().animation.Play("grabbed");
            }
        }
    }

    public void erenIsHere(GameObject target)
    {
        myHero = eren = target;
    }

    private void findNearestFacingHero()
    {
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        var position = transform.position;
        var current = 0f;
        var num3 = 180f;
        var f = 0f;
        foreach (var obj3 in objArray)
        {
            var vector2 = obj3.transform.position - position;
            var sqrMagnitude = vector2.sqrMagnitude;
            if (sqrMagnitude < positiveInfinity)
            {
                var vector3 = obj3.transform.position - transform.position;
                current = -Mathf.Atan2(vector3.z, vector3.x) * 57.29578f;
                f = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
                if (Mathf.Abs(f) < num3)
                {
                    obj2 = obj3;
                    positiveInfinity = sqrMagnitude;
                }
            }
        }
        if (obj2 != null)
        {
            myHero = obj2;
            tauntTime = 5f;
        }
    }

    private void findNearestHero()
    {
        myHero = getNearestHero();
        attention = UnityEngine.Random.Range(5f, 10f);
    }

    private void FixedUpdate()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || photonView.isMine))
        {
            if (bottomObject.GetComponent<CheckHitGround>().isGrounded)
            {
                grounded = true;
                bottomObject.GetComponent<CheckHitGround>().isGrounded = false;
            }
            else
            {
                grounded = false;
            }
            if (needFreshCorePosition)
            {
                oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
                needFreshCorePosition = false;
            }
            if (((state == "attack") && isAttackMoveByCore) || (((state == "hit") || (state == "turn180")) || (state == "anklehurt")))
            {
                var vector = (transform.position - transform.Find("Amarture/Core").position) - oldCorePosition;
                rigidbody.velocity = (vector / Time.deltaTime) + (Vector3.up * rigidbody.velocity.y);
                oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
            }
            else if (state == "chase")
            {
                if (myHero == null)
                {
                    return;
                }
                var vector3 = transform.forward * speed;
                var velocity = rigidbody.velocity;
                var force = vector3 - velocity;
                force.y = 0f;
                rigidbody.AddForce(force, ForceMode.VelocityChange);
                var current = 0f;
                var vector6 = myHero.transform.position - transform.position;
                current = -Mathf.Atan2(vector6.z, vector6.x) * 57.29578f;
                var num2 = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, gameObject.transform.rotation.eulerAngles.y + num2, 0f), speed * Time.deltaTime);
            }
            else if (grounded && !animation.IsPlaying("attack_jumpCombo_1"))
            {
                rigidbody.AddForce(new Vector3(-rigidbody.velocity.x, 0f, -rigidbody.velocity.z), ForceMode.VelocityChange);
            }
            rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
        }
    }

    private void getDown()
    {
        state = "anklehurt";
        playAnimation("legHurt");
        AnkleRHP = AnkleRHPMAX;
        AnkleLHP = AnkleLHPMAX;
        needFreshCorePosition = true;
    }

    private GameObject getNearestHero()
    {
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject obj2 = null;
        var positiveInfinity = float.PositiveInfinity;
        var position = transform.position;
        foreach (var obj3 in objArray)
        {
            if (((obj3.GetComponent<HERO>() == null) || !obj3.GetComponent<HERO>().HasDied()) && ((obj3.GetComponent<TITAN_EREN>() == null) || !obj3.GetComponent<TITAN_EREN>().hasDied))
            {
                var vector2 = obj3.transform.position - position;
                var sqrMagnitude = vector2.sqrMagnitude;
                if (sqrMagnitude < positiveInfinity)
                {
                    obj2 = obj3;
                    positiveInfinity = sqrMagnitude;
                }
            }
        }
        return obj2;
    }

    private float getNearestHeroDistance()
    {
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        var positiveInfinity = float.PositiveInfinity;
        var position = transform.position;
        foreach (var obj2 in objArray)
        {
            var vector2 = obj2.transform.position - position;
            var magnitude = vector2.magnitude;
            if (magnitude < positiveInfinity)
            {
                positiveInfinity = magnitude;
            }
        }
        return positiveInfinity;
    }

    private void grab(string type)
    {
        state = "grab";
        attacked = false;
        attackAnimation = type;
        if (animation.IsPlaying("attack_grab_" + type))
        {
            animation["attack_grab_" + type].normalizedTime = 0f;
            playAnimation("attack_grab_" + type);
        }
        else
        {
            crossFade("attack_grab_" + type, 0.1f);
        }
        isGrabHandLeft = true;
        grabbedTarget = null;
        attackCheckTime = 0f;
        var key = type;
        if (key != null)
        {
            int num;
            if (fswitchSmap3 == null)
            {
                var dictionary = new Dictionary<string, int>(7);
                dictionary.Add("bottom_left", 0);
                dictionary.Add("bottom_right", 1);
                dictionary.Add("mid_left", 2);
                dictionary.Add("mid_right", 3);
                dictionary.Add("up", 4);
                dictionary.Add("up_left", 5);
                dictionary.Add("up_right", 6);
                fswitchSmap3 = dictionary;
            }
            if (fswitchSmap3.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        attackCheckTimeA = 0.28f;
                        attackCheckTimeB = 0.38f;
                        attackCheckTime = 0.65f;
                        isGrabHandLeft = false;
                        break;

                    case 1:
                        attackCheckTimeA = 0.27f;
                        attackCheckTimeB = 0.37f;
                        attackCheckTime = 0.65f;
                        break;

                    case 2:
                        attackCheckTimeA = 0.27f;
                        attackCheckTimeB = 0.37f;
                        attackCheckTime = 0.65f;
                        isGrabHandLeft = false;
                        break;

                    case 3:
                        attackCheckTimeA = 0.27f;
                        attackCheckTimeB = 0.36f;
                        attackCheckTime = 0.66f;
                        break;

                    case 4:
                        attackCheckTimeA = 0.25f;
                        attackCheckTimeB = 0.32f;
                        attackCheckTime = 0.67f;
                        break;

                    case 5:
                        attackCheckTimeA = 0.26f;
                        attackCheckTimeB = 0.4f;
                        attackCheckTime = 0.66f;
                        break;

                    case 6:
                        attackCheckTimeA = 0.26f;
                        attackCheckTimeB = 0.4f;
                        attackCheckTime = 0.66f;
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
        grabTF.transform.parent = transform;
        grabTF.transform.position = transform.GetComponent<SphereCollider>().transform.position;
        grabTF.transform.rotation = transform.GetComponent<SphereCollider>().transform.rotation;
        var transform1 = grabTF.transform;
        transform1.localPosition -= (Vector3.right * transform.GetComponent<SphereCollider>().radius) * 0.3f;
        var transform2 = grabTF.transform;
        transform2.localPosition -= (Vector3.up * transform.GetComponent<SphereCollider>().radius) * 0.51f;
        var transform3 = grabTF.transform;
        transform3.localPosition -= (Vector3.forward * transform.GetComponent<SphereCollider>().radius) * 0.3f;
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
        transform1.localPosition -= (Vector3.right * transform.GetComponent<SphereCollider>().radius) * 0.3f;
        var transform2 = grabTF.transform;
        transform2.localPosition += (Vector3.up * transform.GetComponent<SphereCollider>().radius) * 0.51f;
        var transform3 = grabTF.transform;
        transform3.localPosition -= (Vector3.forward * transform.GetComponent<SphereCollider>().radius) * 0.3f;
        grabTF.transform.localRotation = Quaternion.Euler(grabTF.transform.localRotation.eulerAngles.x, grabTF.transform.localRotation.eulerAngles.y + 180f, grabTF.transform.localRotation.eulerAngles.z);
    }

    public void hit(int dmg)
    {
        NapeArmor -= dmg;
        if (NapeArmor <= 0)
        {
            NapeArmor = 0;
        }
    }

    public void hitAnkleL(int dmg)
    {
        if (!hasDie && (state != "anklehurt"))
        {
            AnkleLHP -= dmg;
            if (AnkleLHP <= 0)
            {
                getDown();
            }
        }
    }

    [RPC]
    public void hitAnkleLRPC(int viewID, int dmg)
    {
        if (!hasDie && (state != "anklehurt"))
        {
            var view = PhotonView.Find(viewID);
            if (view != null)
            {
                if (grabbedTarget != null)
                {
                    grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                var vector = view.gameObject.transform.position - transform.Find("Amarture/Core/Controller_Body").transform.position;
                if (vector.magnitude < 20f)
                {
                    AnkleLHP -= dmg;
                    if (AnkleLHP <= 0)
                    {
                        getDown();
                    }
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().sendKillInfo(false, (string) view.owner.customProperties[PhotonPlayerProperty.name], true, "Female Titan's ankle", dmg);
                    var parameters = new object[] { dmg };
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("netShowDamage", view.owner, parameters);
                }
            }
        }
    }

    public void hitAnkleR(int dmg)
    {
        if (!hasDie && (state != "anklehurt"))
        {
            AnkleRHP -= dmg;
            if (AnkleRHP <= 0)
            {
                getDown();
            }
        }
    }

    [RPC]
    public void hitAnkleRRPC(int viewID, int dmg)
    {
        if (!hasDie && (state != "anklehurt"))
        {
            var view = PhotonView.Find(viewID);
            if (view != null)
            {
                if (grabbedTarget != null)
                {
                    grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                }
                var vector = view.gameObject.transform.position - transform.Find("Amarture/Core/Controller_Body").transform.position;
                if (vector.magnitude < 20f)
                {
                    AnkleRHP -= dmg;
                    if (AnkleRHP <= 0)
                    {
                        getDown();
                    }
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().sendKillInfo(false, (string) view.owner.customProperties[PhotonPlayerProperty.name], true, "Female Titan's ankle", dmg);
                    var parameters = new object[] { dmg };
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("netShowDamage", view.owner, parameters);
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
            if (grabbedTarget != null)
            {
                grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All, new object[0]);
            }
            var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
            var view = PhotonView.Find(viewID);
            if (view != null)
            {
                var vector = view.gameObject.transform.position - transform.transform.position;
                if (vector.magnitude < 20f)
                {
                    justHitEye();
                }
            }
        }
    }

    private void idle(float sbtime = 0f)
    {
        this.sbtime = sbtime;
        this.sbtime = Mathf.Max(0.5f, this.sbtime);
        state = "idle";
        crossFade("idle", 0.2f);
    }

    public bool IsGrounded()
    {
        return bottomObject.GetComponent<CheckHitGround>().isGrounded;
    }

    private void justEatHero(GameObject target, Transform hand)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            if (!target.GetComponent<HERO>().HasDied())
            {
                target.GetComponent<HERO>().markDie();
                var parameters = new object[] { -1, "Female Titan" };
                target.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, parameters);
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            target.GetComponent<HERO>().die2(hand);
        }
    }

    private void justHitEye()
    {
        attack("combo_blind_1");
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
                    hitHero.GetComponent<HERO>().die(((hitHero.transform.position - position) * 15f) * 4f, false);
                }
            }
            else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient) && !hitHero.GetComponent<HERO>().HasDied())
            {
                hitHero.GetComponent<HERO>().markDie();
                var parameters = new object[] { ((hitHero.transform.position - position) * 15f) * 4f, false, -1, "Female Titan", true };
                hitHero.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, parameters);
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
                healthLabel.transform.localPosition = new Vector3(0f, 52f, 0f);
                var a = 4f;
                if ((size > 0f) && (size < 1f))
                {
                    a = 4f / size;
                    a = Mathf.Min(a, 15f);
                }
                healthLabel.transform.localScale = new Vector3(a, a, a);
            }
            var str = "[7FFF00]";
            var num2 = health / ((float) maxHealth);
            if ((num2 < 0.75f) && (num2 >= 0.5f))
            {
                str = "[f2b50f]";
            }
            else if ((num2 < 0.5f) && (num2 >= 0.25f))
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
        if (!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
        {
            if (animation.IsPlaying("run"))
            {
                if ((((animation["run"].normalizedTime % 1f) > 0.1f) && ((animation["run"].normalizedTime % 1f) < 0.6f)) && (stepSoundPhase == 2))
                {
                    stepSoundPhase = 1;
                    var transform = this.transform.Find("snd_titan_foot");
                    transform.GetComponent<AudioSource>().Stop();
                    transform.GetComponent<AudioSource>().Play();
                }
                if (((animation["run"].normalizedTime % 1f) > 0.6f) && (stepSoundPhase == 1))
                {
                    stepSoundPhase = 2;
                    var transform2 = transform.Find("snd_titan_foot");
                    transform2.GetComponent<AudioSource>().Stop();
                    transform2.GetComponent<AudioSource>().Play();
                }
            }
            updateLabel();
            healthTime -= Time.deltaTime;
        }
    }

    public void loadskin()
    {
        if (Settings.TitanSkins == 1)
        {
            photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { (string) FengGameManagerMKII.settings[0x42] });
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
        if (Settings.MipMapping == 1)
        {
            mipmap = false;
        }
        foreach (var iteratorVariable4 in GetComponentsInChildren<Renderer>())
        {
            if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
            {
                var link = new WWW(url);
                yield return link;
                var iteratorVariable6 = RCextensions.loadimage(link, mipmap, 0xf4240);
                link.Dispose();
                if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
                {
                    iteratorVariable1 = true;
                    iteratorVariable4.material.mainTexture = iteratorVariable6;
                    FengGameManagerMKII.linkHash[2].Add(url, iteratorVariable4.material);
                    iteratorVariable4.material = (Material) FengGameManagerMKII.linkHash[2][url];
                }
                else
                {
                    iteratorVariable4.material = (Material) FengGameManagerMKII.linkHash[2][url];
                }
            }
            else
            {
                iteratorVariable4.material = (Material) FengGameManagerMKII.linkHash[2][url];
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
    }

    [RPC]
    public void loadskinRPC(string url)
    {
        if ((Settings.TitanSkins == 1) && ((url.EndsWith(".jpg") || url.EndsWith(".png")) || url.EndsWith(".jpeg")))
        {
            StartCoroutine(loadskinE(url));
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
            crossFade("die", 0.05f);
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
        if (GameObject.Find("MultiplayerManager") != null)
        {
            GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().removeFT(this);
        }
    }

    private void playAnimation(string aniName)
    {
        animation.Play(aniName);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            var parameters = new object[] { aniName };
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            var parameters = new object[] { aniName, normalizedTime };
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        playsoundRPC(sndname);
        if (Network.peerType == NetworkPeerType.Server)
        {
            var parameters = new object[] { sndname };
            photonView.RPC("playsoundRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void playsoundRPC(string sndname)
    {
        transform.Find(sndname).GetComponent<AudioSource>().Play();
    }

    [RPC]
    public void setSize(float size, PhotonMessageInfo info)
    {
        size = Mathf.Clamp(size, 0.2f, 30f);
        if (info.sender.isMasterClient)
        {
            var transform = this.transform;
            transform.localScale = transform.localScale * (size * 0.25f);
            this.size = size;
        }
    }

    private void Start()
    {
        startMain();
        size = 4f;
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.black, false, true, Minimap.IconStyle.CIRCLE);
        }
        if (photonView.isMine)
        {
            if (RCSettings.sizeMode > 0)
            {
                var sizeLower = RCSettings.sizeLower;
                var sizeUpper = RCSettings.sizeUpper;
                size = UnityEngine.Random.Range(sizeLower, sizeUpper);
                photonView.RPC("setSize", PhotonTargets.AllBuffered, new object[] { size });
            }
            lagMax = 150f + (size * 3f);
            healthTime = 0f;
            maxHealth = NapeArmor;
            if (RCSettings.healthMode > 0)
            {
                maxHealth = NapeArmor = UnityEngine.Random.Range(RCSettings.healthLower, RCSettings.healthUpper);
            }
            if (NapeArmor > 0)
            {
                photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { NapeArmor, maxHealth });
            }
            loadskin();
        }
        hasspawn = true;
    }

    private void startMain()
    {
        GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().addFT(this);
        name = "Female Titan";
        grabTF = new GameObject();
        grabTF.name = "titansTmpGrabTF";
        currentCamera = GameObject.Find("MainCamera");
        oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
        if (myHero == null)
        {
            findNearestHero();
        }
        var enumerator = animation.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (AnimationState) enumerator.Current;
                current.speed = 0.7f;
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        animation["turn180"].speed = 0.5f;
        NapeArmor = 0x3e8;
        AnkleLHP = 50;
        AnkleRHP = 50;
        AnkleLHPMAX = 50;
        AnkleRHPMAX = 50;
        var flag = false;
        if (LevelInfo.getInfo(FengGameManagerMKII.level).respawnMode == RespawnMode.NEVER)
        {
            flag = true;
        }
        if (IN_GAME_MAIN_CAMERA.difficulty == 0)
        {
            NapeArmor = !flag ? 0x3e8 : 0x3e8;
            AnkleLHP = AnkleLHPMAX = !flag ? 50 : 50;
            AnkleRHP = AnkleRHPMAX = !flag ? 50 : 50;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 1)
        {
            NapeArmor = !flag ? 0xbb8 : 0x9c4;
            AnkleLHP = AnkleLHPMAX = !flag ? 200 : 100;
            AnkleRHP = AnkleRHPMAX = !flag ? 200 : 100;
            var enumerator2 = animation.GetEnumerator();
            try
            {
                while (enumerator2.MoveNext())
                {
                    var state2 = (AnimationState) enumerator2.Current;
                    state2.speed = 0.7f;
                }
            }
            finally
            {
                var disposable2 = enumerator2 as IDisposable;
                if (disposable2 != null)
                	disposable2.Dispose();
            }
            animation["turn180"].speed = 0.7f;
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
        {
            NapeArmor = !flag ? 0x1770 : 0xfa0;
            AnkleLHP = AnkleLHPMAX = !flag ? 0x3e8 : 200;
            AnkleRHP = AnkleRHPMAX = !flag ? 0x3e8 : 200;
            var enumerator3 = animation.GetEnumerator();
            try
            {
                while (enumerator3.MoveNext())
                {
                    var state3 = (AnimationState) enumerator3.Current;
                    state3.speed = 1f;
                }
            }
            finally
            {
                var disposable3 = enumerator3 as IDisposable;
                if (disposable3 != null)
                	disposable3.Dispose();
            }
            animation["turn180"].speed = 0.9f;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            NapeArmor = (int) (NapeArmor * 0.8f);
        }
        animation["legHurt"].speed = 1f;
        animation["legHurt_loop"].speed = 1f;
        animation["legHurt_getup"].speed = 1f;
    }

    [RPC]
    public void titanGetHit(int viewID, int speed)
    {
        var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        var view = PhotonView.Find(viewID);
        if (view != null)
        {
            var vector = view.gameObject.transform.position - transform.transform.position;
            if ((vector.magnitude < lagMax) && (healthTime <= 0f))
            {
                if (speed >= RCSettings.damageMode)
                {
                    NapeArmor -= speed;
                }
                if (maxHealth > 0f)
                {
                    photonView.RPC("labelRPC", PhotonTargets.AllBuffered, new object[] { NapeArmor, maxHealth });
                }
                if (NapeArmor <= 0)
                {
                    NapeArmor = 0;
                    if (!hasDie)
                    {
                        photonView.RPC("netDie", PhotonTargets.OthersBuffered, new object[0]);
                        if (grabbedTarget != null)
                        {
                            grabbedTarget.GetPhotonView().RPC("netUngrabbed", PhotonTargets.All, new object[0]);
                        }
                        netDie();
                        GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().titanGetKill(view.owner, speed, name);
                    }
                }
                else
                {
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().sendKillInfo(false, (string) view.owner.customProperties[PhotonPlayerProperty.name], true, "Female Titan's neck", speed);
                    var parameters = new object[] { speed };
                    GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("netShowDamage", view.owner, parameters);
                }
                healthTime = 0.2f;
            }
        }
    }

    private void turn(float d)
    {
        if (d > 0f)
        {
            turnAnimation = "turnaround1";
        }
        else
        {
            turnAnimation = "turnaround2";
        }
        playAnimation(turnAnimation);
        animation[turnAnimation].time = 0f;
        d = Mathf.Clamp(d, -120f, 120f);
        turnDeg = d;
        desDeg = gameObject.transform.rotation.eulerAngles.y + turnDeg;
        state = "turn";
    }

    private void turn180()
    {
        turnAnimation = "turn180";
        playAnimation(turnAnimation);
        animation[turnAnimation].time = 0f;
        state = "turn180";
        needFreshCorePosition = true;
    }

    public void update()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || photonView.isMine))
        {
            if (hasDie)
            {
                dieTime += Time.deltaTime;
                if (animation["die"].normalizedTime >= 1f)
                {
                    playAnimation("die_cry");
                    if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE)
                    {
                        for (var i = 0; i < 15; i++)
                        {
                            GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().randomSpawnOneTitan("titanRespawn", 50).GetComponent<TITAN>().beTauntedBy(gameObject, 20f);
                        }
                    }
                }
                if ((dieTime > 2f) && !hasDieSteam)
                {
                    hasDieSteam = true;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        var obj3 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie1"));
                        obj3.transform.position = transform.Find("Amarture/Core/Controller_Body/hip").position;
                        obj3.transform.localScale = transform.localScale;
                    }
                    else if (photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie1", transform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = transform.localScale;
                    }
                }
                if (dieTime > ((IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE) ? 20f : 5f))
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        var obj5 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie"));
                        obj5.transform.position = transform.Find("Amarture/Core/Controller_Body/hip").position;
                        obj5.transform.localScale = transform.localScale;
                        Destroy(gameObject);
                    }
                    else if (photonView.isMine)
                    {
                        PhotonNetwork.Instantiate("FX/FXtitanDie", transform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = transform.localScale;
                        PhotonNetwork.Destroy(gameObject);
                    }
                }
            }
            else
            {
                if (attention > 0f)
                {
                    attention -= Time.deltaTime;
                    if (attention < 0f)
                    {
                        attention = 0f;
                        var objArray = GameObject.FindGameObjectsWithTag("Player");
                        myHero = objArray[UnityEngine.Random.Range(0, objArray.Length)];
                        attention = UnityEngine.Random.Range(5f, 10f);
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
                }
                if (eren != null)
                {
                    if (!eren.GetComponent<TITAN_EREN>().hasDied)
                    {
                        myHero = eren;
                    }
                    else
                    {
                        eren = null;
                        myHero = null;
                    }
                }
                if (myHero == null)
                {
                    findNearestHero();
                    if (myHero != null)
                    {
                        return;
                    }
                }
                if (myHero == null)
                {
                    myDistance = float.MaxValue;
                }
                else
                {
                    myDistance = Mathf.Sqrt(((myHero.transform.position.x - transform.position.x) * (myHero.transform.position.x - transform.position.x)) + ((myHero.transform.position.z - transform.position.z) * (myHero.transform.position.z - transform.position.z)));
                }
                if (state == "idle")
                {
                    if (myHero != null)
                    {
                        var current = 0f;
                        var f = 0f;
                        var vector9 = myHero.transform.position - transform.position;
                        current = -Mathf.Atan2(vector9.z, vector9.x) * 57.29578f;
                        f = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
                        if (!attackTarget(myHero))
                        {
                            if (Mathf.Abs(f) < 90f)
                            {
                                chase();
                            }
                            else if (UnityEngine.Random.Range(0, 100) < 1)
                            {
                                turn180();
                            }
                            else if (Mathf.Abs(f) > 100f)
                            {
                                if (UnityEngine.Random.Range(0, 100) < 10)
                                {
                                    turn180();
                                }
                            }
                            else if ((Mathf.Abs(f) > 45f) && (UnityEngine.Random.Range(0, 100) < 30))
                            {
                                turn(f);
                            }
                        }
                    }
                }
                else if (state == "attack")
                {
                    if ((!attacked && (attackCheckTime != 0f)) && (animation["attack_" + attackAnimation].normalizedTime >= attackCheckTime))
                    {
                        GameObject obj7;
                        attacked = true;
                        fxPosition = transform.Find("ap_" + attackAnimation).position;
                        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                        {
                            obj7 = PhotonNetwork.Instantiate("FX/" + fxName, fxPosition, fxRotation, 0);
                        }
                        else
                        {
                            obj7 = (GameObject) Instantiate(Resources.Load("FX/" + fxName), fxPosition, fxRotation);
                        }
                        obj7.transform.localScale = transform.localScale;
                        var b = 1f - (Vector3.Distance(currentCamera.transform.position, obj7.transform.position) * 0.05f);
                        b = Mathf.Min(1f, b);
                        currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(b, b, 0.95f);
                    }
                    if ((attackCheckTimeA != 0f) && (((animation["attack_" + attackAnimation].normalizedTime >= attackCheckTimeA) && (animation["attack_" + attackAnimation].normalizedTime <= attackCheckTimeB)) || (!attackChkOnce && (animation["attack_" + attackAnimation].normalizedTime >= attackCheckTimeA))))
                    {
                        if (!attackChkOnce)
                        {
                            attackChkOnce = true;
                            playSound("snd_eren_swing" + UnityEngine.Random.Range(1, 3));
                        }
                        foreach (var hit in checkHitCapsule(checkHitCapsuleStart.position, checkHitCapsuleEnd.position, checkHitCapsuleR))
                        {
                            var gameObject = hit.collider.gameObject;
                            if (gameObject.tag == "Player")
                            {
                                killPlayer(gameObject);
                            }
                            if (gameObject.tag == "erenHitbox")
                            {
                                if (attackAnimation == "combo_1")
                                {
                                    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                                    {
                                        gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByFTByServer(1);
                                    }
                                }
                                else if (attackAnimation == "combo_2")
                                {
                                    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
                                    {
                                        gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByFTByServer(2);
                                    }
                                }
                                else if (((attackAnimation == "combo_3") && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && PhotonNetwork.isMasterClient)
                                {
                                    gameObject.transform.root.gameObject.GetComponent<TITAN_EREN>().hitByFTByServer(3);
                                }
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
                    if (((attackAnimation == "jumpCombo_1") && (animation["attack_" + attackAnimation].normalizedTime >= 0.65f)) && (!startJump && (myHero != null)))
                    {
                        startJump = true;
                        var y = myHero.rigidbody.velocity.y;
                        var num7 = -20f;
                        var gravity = this.gravity;
                        var num9 = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position.y;
                        var num10 = (num7 - gravity) * 0.5f;
                        var num11 = y;
                        var num12 = myHero.transform.position.y - num9;
                        var num13 = Mathf.Abs((Mathf.Sqrt((num11 * num11) - ((4f * num10) * num12)) - num11) / (2f * num10));
                        var vector14 = (myHero.transform.position + (myHero.rigidbody.velocity * num13)) + ((((Vector3.up * 0.5f) * num7) * num13) * num13);
                        var num14 = vector14.y;
                        if ((num12 < 0f) || ((num14 - num9) < 0f))
                        {
                            idle(0f);
                            num13 = 0.5f;
                            vector14 = transform.position + (num9 + 5f) * Vector3.up;
                            num14 = vector14.y;
                        }
                        var num15 = num14 - num9;
                        var num16 = Mathf.Sqrt((2f * num15) / this.gravity);
                        var num17 = (this.gravity * num16) + 20f;
                        num17 = Mathf.Clamp(num17, 20f, 90f);
                        var vector15 = (vector14 - transform.position) / num13;
                        abnorma_jump_bite_horizon_v = new Vector3(vector15.x, 0f, vector15.z);
                        var velocity = rigidbody.velocity;
                        var vector17 = new Vector3(abnorma_jump_bite_horizon_v.x, num17, abnorma_jump_bite_horizon_v.z);
                        if (vector17.magnitude > 90f)
                        {
                            vector17 = vector17.normalized * 90f;
                        }
                        var force = vector17 - velocity;
                        rigidbody.AddForce(force, ForceMode.VelocityChange);
                        var num18 = Vector2.Angle(new Vector2(transform.position.x, transform.position.z), new Vector2(myHero.transform.position.x, myHero.transform.position.z));
                        num18 = Mathf.Atan2(myHero.transform.position.x - transform.position.x, myHero.transform.position.z - transform.position.z) * 57.29578f;
                        gameObject.transform.rotation = Quaternion.Euler(0f, num18, 0f);
                    }
                    if (attackAnimation == "jumpCombo_3")
                    {
                        if ((animation["attack_" + attackAnimation].normalizedTime >= 1f) && IsGrounded())
                        {
                            attack("jumpCombo_4");
                        }
                    }
                    else if (animation["attack_" + attackAnimation].normalizedTime >= 1f)
                    {
                        if (nextAttackAnimation != null)
                        {
                            attack(nextAttackAnimation);
                            if (eren != null)
                            {
                                gameObject.transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(eren.transform.position - transform.position).eulerAngles.y, 0f);
                            }
                        }
                        else
                        {
                            findNearestHero();
                            idle(0f);
                        }
                    }
                }
                else if (state == "grab")
                {
                    if (((animation["attack_grab_" + attackAnimation].normalizedTime >= attackCheckTimeA) && (animation["attack_grab_" + attackAnimation].normalizedTime <= attackCheckTimeB)) && (grabbedTarget == null))
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
                    if ((animation["attack_grab_" + attackAnimation].normalizedTime > attackCheckTime) && (grabbedTarget != null))
                    {
                        justEatHero(grabbedTarget, currentGrabHand);
                        grabbedTarget = null;
                    }
                    if (animation["attack_grab_" + attackAnimation].normalizedTime >= 1f)
                    {
                        idle(0f);
                    }
                }
                else if (state == "turn")
                {
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, desDeg, 0f), (Time.deltaTime * Mathf.Abs(turnDeg)) * 0.1f);
                    if (animation[turnAnimation].normalizedTime >= 1f)
                    {
                        idle(0f);
                    }
                }
                else if (state == "chase")
                {
                    if (((((eren == null) || (myDistance >= 35f)) || !attackTarget(myHero)) && (((getNearestHeroDistance() >= 50f) || (UnityEngine.Random.Range(0, 100) >= 20)) || !attackTarget(getNearestHero()))) && (myDistance < (attackDistance - 15f)))
                    {
                        idle(UnityEngine.Random.Range(0.05f, 0.2f));
                    }
                }
                else if (state == "turn180")
                {
                    if (animation[turnAnimation].normalizedTime >= 1f)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y + 180f, gameObject.transform.rotation.eulerAngles.z);
                        idle(0f);
                        playAnimation("idle");
                    }
                }
                else if (state == "anklehurt")
                {
                    if (animation["legHurt"].normalizedTime >= 1f)
                    {
                        crossFade("legHurt_loop", 0.2f);
                    }
                    if (animation["legHurt_loop"].normalizedTime >= 3f)
                    {
                        crossFade("legHurt_getup", 0.2f);
                    }
                    if (animation["legHurt_getup"].normalizedTime >= 1f)
                    {
                        idle(0f);
                        playAnimation("idle");
                    }
                }
            }
        }
    }

    public void updateLabel()
    {
        if ((healthLabel != null) && healthLabel.GetComponent<UILabel>().isVisible)
        {
            healthLabel.transform.LookAt(2f * healthLabel.transform.position - Camera.main.transform.position);
        }
    }

}

