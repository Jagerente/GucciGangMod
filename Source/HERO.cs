using GGM;
using GGM.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xft;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using MonoBehaviour = Photon.MonoBehaviour;
using Random = UnityEngine.Random;

public class HERO : MonoBehaviour
{
    private HERO_STATE _state;
    private bool almostSingleHook;
    private string attackAnimation;
    private int attackLoop;
    private bool attackMove;
    private bool attackReleased;
    public AudioSource audio_ally;
    public AudioSource audio_hitwall;
    private GameObject badGuy;
    public Animation baseAnimation;
    public Rigidbody baseRigidBody;
    public Transform baseTransform;
    public float bombCD;
    public bool bombImmune;
    public float bombRadius;
    public float bombSpeed;
    public float bombTime;
    public float bombTimeMax;
    private float buffTime;
    public GameObject bulletLeft;
    private int bulletMAX = 7;
    public GameObject bulletRight;
    private bool buttonAttackRelease;
    public Dictionary<string, UISprite> cachedSprites;
    public float CameraMultiplier;
    public bool canJump = true;
    public GameObject checkBoxLeft;
    public GameObject checkBoxRight;
    public GameObject cross1;
    public GameObject cross2;
    public GameObject crossL1;
    public GameObject crossL2;
    public GameObject crossR1;
    public GameObject crossR2;
    public string currentAnimation;
    private int currentBladeNum = 5;
    private float currentBladeSta = 100f;
    private BUFF currentBuff;
    public Camera currentCamera;
    private float currentGas = 100f;
    public float currentSpeed;
    public Vector3 currentV;
    private bool dashD;
    private Vector3 dashDirection;
    private bool dashL;
    private bool dashR;
    private float dashTime;
    private bool dashU;
    private Vector3 dashV;
    public bool detonate;
    private float dTapTime = -1f;
    private bool EHold;
    private GameObject eren_titan;
    private int escapeTimes = 1;
    private float facingDirection;
    private float flare1CD;
    private float flare2CD;
    private float flare3CD;
    private float flareTotalCD = 30f;
    private Transform forearmL;
    private Transform forearmR;
    private float gravity = 20f;
    private bool grounded;
    private GameObject gunDummy;
    private Vector3 gunTarget;
    private Transform handL;
    private Transform handR;
    private bool hasDied;
    public bool hasspawn;
    private bool hookBySomeOne = true;
    public GameObject hookRefL1;
    public GameObject hookRefL2;
    public GameObject hookRefR1;
    public GameObject hookRefR2;
    private bool hookSomeOne;
    private GameObject hookTarget;
    public FengCustomInputs inputManager;
    private float invincible = 3f;
    public bool isCannon;
    private bool isLaunchLeft;
    private bool isLaunchRight;
    private bool isLeftHandHooked;
    private bool isMounted;
    public bool isPhotonCamera;
    private bool isRightHandHooked;
    public float jumpHeight = 2f;
    private bool justGrounded;
    public GameObject LabelDistance;
    public Transform lastHook;
    private float launchElapsedTimeL;
    private float launchElapsedTimeR;
    private Vector3 launchForce;
    private Vector3 launchPointLeft;
    private Vector3 launchPointRight;
    private bool leanLeft;
    private bool leftArmAim;
    public XWeaponTrail leftbladetrail;
    public XWeaponTrail leftbladetrail2;
    private int leftBulletLeft = 7;
    private bool leftGunHasBullet = true;
    private float lTapTime = -1f;
    public GameObject maincamera;
    public float maxVelocityChange = 10f;
    public AudioSource meatDie;
    public Bomb myBomb;
    public GameObject myCannon;
    public Transform myCannonBase;
    public Transform myCannonPlayer;
    public CannonPropRegion myCannonRegion;
    public GROUP myGroup;
    private GameObject myHorse;
    public GameObject myNetWorkName;
    public float myScale = 1f;
    public int myTeam = 1;
    public List<TITAN> myTitans;
    private bool needLean;
    private Quaternion oldHeadRotation;
    private float originVM;
    private bool QHold;
    private string reloadAnimation = string.Empty;
    private bool rightArmAim;
    public XWeaponTrail rightbladetrail;
    public XWeaponTrail rightbladetrail2;
    private int rightBulletLeft = 7;
    private bool rightGunHasBullet = true;
    public AudioSource rope;
    private float rTapTime = -1f;
    public HERO_SETUP setup;
    private GameObject skillCD;
    public float skillCDDuration;
    public float skillCDLast;
    public float skillCDLastCannon;
    private string skillId;
    public string skillIDHUD;
    public AudioSource slash;
    public AudioSource slashHit;
    private ParticleSystem smoke_3dmg;
    private ParticleSystem sparks;
    public float speed = 10f;
    public GameObject speedFX;
    public GameObject speedFX1;
    private ParticleSystem speedFXPS;
    private string standAnimation = "stand";
    private Quaternion targetHeadRotation;
    private Quaternion targetRotation;
    public Vector3 targetV;
    private bool throwedBlades;
    public bool titanForm;
    private GameObject titanWhoGrabMe;
    private int titanWhoGrabMeID;
    private int totalBladeNum = 5;
    public float totalBladeSta = 100f;
    public static float totalGas = 100f;
    private Transform upperarmL;
    private Transform upperarmR;
    private float useGasSpeed = 0.2f;
    public bool useGun;
    private float uTapTime = -1f;
    private bool wallJump;
    private float wallRunTime;

    private void applyForceToBody(GameObject GO, Vector3 v)
    {
        GO.rigidbody.AddForce(v);
        GO.rigidbody.AddTorque(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    public void attackAccordingToMouse()
    {
        if (Input.mousePosition.x < Screen.width * 0.5)
        {
            attackAnimation = "attack2";
        }
        else
        {
            attackAnimation = "attack1";
        }
    }

    public void attackAccordingToTarget(Transform a)
    {
        var vector = a.position - transform.position;
        var current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        var f = -Mathf.DeltaAngle(current, transform.rotation.eulerAngles.y - 90f);
        if (Mathf.Abs(f) < 90f && vector.magnitude < 6f && a.position.y <= transform.position.y + 2f && a.position.y >= transform.position.y - 5f)
        {
            attackAnimation = "attack4";
        }
        else if (f > 0f)
        {
            attackAnimation = "attack1";
        }
        else
        {
            attackAnimation = "attack2";
        }
    }

    private void Awake()
    {
        cache();
        setup = gameObject.GetComponent<HERO_SETUP>();
        baseRigidBody.freezeRotation = true;
        baseRigidBody.useGravity = false;
        handL = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
        handR = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
        forearmL = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L");
        forearmR = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        upperarmL = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
        upperarmR = baseTransform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
    }

    public void backToHuman()
    {
        gameObject.GetComponent<SmoothSyncMovement>().disabled = false;
        rigidbody.velocity = Vector3.zero;
        titanForm = false;
        ungrabbed();
        falseAttack();
        skillCDDuration = skillCDLast;
        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(gameObject);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            photonView.RPC("backToHumanRPC", PhotonTargets.Others);
        }
    }

    [RPC]
    private void backToHumanRPC()
    {
        titanForm = false;
        eren_titan = null;
        gameObject.GetComponent<SmoothSyncMovement>().disabled = false;
    }

    [RPC]
    public void badGuyReleaseMe()
    {
        hookBySomeOne = false;
        badGuy = null;
    }

    [RPC]
    public void blowAway(Vector3 force)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);
            transform.LookAt(transform.position);
        }
    }

    private void bodyLean()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            var z = 0f;
            needLean = false;
            if (!useGun && state == HERO_STATE.Attack && attackAnimation != "attack3_1" && attackAnimation != "attack3_2")
            {
                var y = rigidbody.velocity.y;
                var x = rigidbody.velocity.x;
                var num4 = rigidbody.velocity.z;
                var num5 = Mathf.Sqrt(x * x + num4 * num4);
                var num6 = Mathf.Atan2(y, num5) * 57.29578f;
                targetRotation = Quaternion.Euler(-num6 * (1f - Vector3.Angle(rigidbody.velocity, transform.forward) / 90f), facingDirection, 0f);
                if (isLeftHandHooked && bulletLeft != null || isRightHandHooked && bulletRight != null)
                {
                    transform.rotation = targetRotation;
                }
            }
            else
            {
                if (isLeftHandHooked && bulletLeft != null && isRightHandHooked && bulletRight != null)
                {
                    if (almostSingleHook)
                    {
                        needLean = true;
                        z = getLeanAngle(bulletRight.transform.position, true);
                    }
                }
                else if (isLeftHandHooked && bulletLeft != null)
                {
                    needLean = true;
                    z = getLeanAngle(bulletLeft.transform.position, true);
                }
                else if (isRightHandHooked && bulletRight != null)
                {
                    needLean = true;
                    z = getLeanAngle(bulletRight.transform.position, false);
                }

                if (needLean)
                {
                    var a = 0f;
                    if (!useGun && state != HERO_STATE.Attack)
                    {
                        a = currentSpeed * 0.1f;
                        a = Mathf.Min(a, 20f);
                    }

                    targetRotation = Quaternion.Euler(-a, facingDirection, z);
                }
                else if (state != HERO_STATE.Attack)
                {
                    targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                }
            }
        }
    }

    public void bombInit()
    {
        skillIDHUD = skillId;
        skillCDDuration = skillCDLast;
        if (RCSettings.bombMode == 1)
        {
            var num = Settings.BombSettings[0].Value;
            var num2 = Settings.BombSettings[1].Value;
            var num3 = Settings.BombSettings[1].Value;
            var num4 = Settings.BombSettings[3].Value;
            bombTimeMax = (num2 * 60f + 200f) / (num3 * 60f + 200f);
            bombRadius = num * 4f + 20f;
            bombCD = num4 * -0.4f + 5f;
            bombSpeed = num3 * 60f + 200f;
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.RCBombR, Settings.BombColorSetting[0].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombG, Settings.BombColorSetting[1].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombB, Settings.BombColorSetting[2].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombRadius, Settings.BombSettings[0].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombRange, Settings.BombSettings[1].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombSpeed, Settings.BombSettings[2].ToString());
            propertiesToSet.Add(PhotonPlayerProperty.RCBombCooldown, Settings.BombSettings[3].ToString());
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            skillId = "bomb";
            skillIDHUD = "armin";
            skillCDLast = bombCD;
            skillCDDuration = 10f;
            if (FengGameManagerMKII.FGM.roundTime > 10f)
            {
                skillCDDuration = 5f;
            }
        }
    }

    private void breakApart(Vector3 v, bool isBite)
    {
        GameObject obj6;
        GameObject obj7;
        GameObject obj8;
        GameObject obj9;
        GameObject obj10;
        var obj2 = (GameObject)Instantiate(Resources.Load("Character_parts/AOTTG_HERO_body"), this.transform.position, this.transform.rotation);
        obj2.gameObject.GetComponent<HERO_SETUP>().myCostume = setup.myCostume;
        obj2.GetComponent<HERO_SETUP>().isDeadBody = true;
        obj2.GetComponent<HERO_DEAD_BODY_SETUP>().init(currentAnimation, animation[currentAnimation].normalizedTime, BODY_PARTS.ARM_R);
        if (!isBite)
        {
            var gO = (GameObject)Instantiate(Resources.Load("Character_parts/AOTTG_HERO_body"), this.transform.position, this.transform.rotation);
            var obj4 = (GameObject)Instantiate(Resources.Load("Character_parts/AOTTG_HERO_body"), this.transform.position, this.transform.rotation);
            var obj5 = (GameObject)Instantiate(Resources.Load("Character_parts/AOTTG_HERO_body"), this.transform.position, this.transform.rotation);
            gO.gameObject.GetComponent<HERO_SETUP>().myCostume = setup.myCostume;
            obj4.gameObject.GetComponent<HERO_SETUP>().myCostume = setup.myCostume;
            obj5.gameObject.GetComponent<HERO_SETUP>().myCostume = setup.myCostume;
            gO.GetComponent<HERO_SETUP>().isDeadBody = true;
            obj4.GetComponent<HERO_SETUP>().isDeadBody = true;
            obj5.GetComponent<HERO_SETUP>().isDeadBody = true;
            gO.GetComponent<HERO_DEAD_BODY_SETUP>().init(currentAnimation, animation[currentAnimation].normalizedTime, BODY_PARTS.UPPER);
            obj4.GetComponent<HERO_DEAD_BODY_SETUP>().init(currentAnimation, animation[currentAnimation].normalizedTime, BODY_PARTS.LOWER);
            obj5.GetComponent<HERO_DEAD_BODY_SETUP>().init(currentAnimation, animation[currentAnimation].normalizedTime, BODY_PARTS.ARM_L);
            applyForceToBody(gO, v);
            applyForceToBody(obj4, v);
            applyForceToBody(obj5, v);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(gO, false);
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(obj2, false);
        }

        applyForceToBody(obj2, v);
        var transform = this.transform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L").transform;
        var transform2 = this.transform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R").transform;
        if (useGun)
        {
            obj6 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
            obj7 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_r"), transform2.position, transform2.rotation);
            obj8 = (GameObject)Instantiate(Resources.Load("Character_parts/character_3dmg_2"), this.transform.position, this.transform.rotation);
            obj9 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_mag_l"), this.transform.position, this.transform.rotation);
            obj10 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_mag_r"), this.transform.position, this.transform.rotation);
        }
        else
        {
            obj6 = (GameObject)Instantiate(Resources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
            obj7 = (GameObject)Instantiate(Resources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
            obj8 = (GameObject)Instantiate(Resources.Load("Character_parts/character_3dmg"), this.transform.position, this.transform.rotation);
            obj9 = (GameObject)Instantiate(Resources.Load("Character_parts/character_3dmg_gas_l"), this.transform.position, this.transform.rotation);
            obj10 = (GameObject)Instantiate(Resources.Load("Character_parts/character_3dmg_gas_r"), this.transform.position, this.transform.rotation);
        }

        obj6.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        obj7.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        obj8.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        obj9.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        obj10.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        applyForceToBody(obj6, v);
        applyForceToBody(obj7, v);
        applyForceToBody(obj8, v);
        applyForceToBody(obj9, v);
        applyForceToBody(obj10, v);
    }

    private void bufferUpdate()
    {
        if (buffTime > 0f)
        {
            buffTime -= Time.deltaTime;
            if (buffTime <= 0f)
            {
                buffTime = 0f;
                if (currentBuff == BUFF.SpeedUp && animation.IsPlaying("run_sasha"))
                {
                    crossFade("run", 0.1f);
                }

                currentBuff = BUFF.NoBuff;
            }
        }
    }

    public void cache()
    {
        baseTransform = transform;
        baseRigidBody = rigidbody;
        maincamera = GGM.Caching.GameObjectCache.Find("MainCamera");
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            baseAnimation = animation;
            cross1 = GGM.Caching.GameObjectCache.Find("cross1");
            cross2 = GGM.Caching.GameObjectCache.Find("cross2");
            crossL1 = GGM.Caching.GameObjectCache.Find("crossL1");
            crossL2 = GGM.Caching.GameObjectCache.Find("crossL2");
            crossR1 = GGM.Caching.GameObjectCache.Find("crossR1");
            crossR2 = GGM.Caching.GameObjectCache.Find("crossR2");
            LabelDistance = GGM.Caching.GameObjectCache.Find("LabelDistance");
            cachedSprites = new Dictionary<string, UISprite>();
            foreach (GameObject obj2 in FindObjectsOfType(typeof(GameObject)))
            {
                if (obj2.GetComponent<UISprite>() != null && obj2.activeInHierarchy)
                {
                    var name = obj2.name;
                    if (!(!name.Contains("blade") && !name.Contains("bullet") && !name.Contains("gas") && !name.Contains("flare") && !name.Contains("skill_cd") || cachedSprites.ContainsKey(name)))
                    {
                        cachedSprites.Add(name, obj2.GetComponent<UISprite>());
                    }
                }
            }
        }
    }

    private void calcFlareCD()
    {
        if (flare1CD > 0f)
        {
            flare1CD -= Time.deltaTime;
            if (flare1CD < 0f)
            {
                flare1CD = 0f;
            }
        }

        if (flare2CD > 0f)
        {
            flare2CD -= Time.deltaTime;
            if (flare2CD < 0f)
            {
                flare2CD = 0f;
            }
        }

        if (flare3CD > 0f)
        {
            flare3CD -= Time.deltaTime;
            if (flare3CD < 0f)
            {
                flare3CD = 0f;
            }
        }
    }

    private void calcSkillCD()
    {
        if (skillCDDuration > 0f)
        {
            skillCDDuration -= Time.deltaTime;
            if (skillCDDuration < 0f)
            {
                skillCDDuration = 0f;
            }
        }
    }

    private float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2f * jumpHeight * gravity);
    }

    private void changeBlade()
    {
        if (!useGun || grounded || LevelInfo.getInfo(FengGameManagerMKII.level).type != GAMEMODE.PVP_AHSS)
        {
            state = HERO_STATE.ChangeBlade;
            throwedBlades = false;
            if (useGun)
            {
                if (!leftGunHasBullet && !rightGunHasBullet)
                {
                    if (grounded)
                    {
                        reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        reloadAnimation = "AHSS_gun_reload_both_air";
                    }
                }
                else if (!leftGunHasBullet)
                {
                    if (grounded)
                    {
                        reloadAnimation = "AHSS_gun_reload_l";
                    }
                    else
                    {
                        reloadAnimation = "AHSS_gun_reload_l_air";
                    }
                }
                else if (!rightGunHasBullet)
                {
                    if (grounded)
                    {
                        reloadAnimation = "AHSS_gun_reload_r";
                    }
                    else
                    {
                        reloadAnimation = "AHSS_gun_reload_r_air";
                    }
                }
                else
                {
                    if (grounded)
                    {
                        reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        reloadAnimation = "AHSS_gun_reload_both_air";
                    }

                    leftGunHasBullet = rightGunHasBullet = Settings.InfiniteBulletsNoReloadingSetting;
                }

                crossFade(reloadAnimation, 0.05f);
            }
            else
            {
                if (!grounded)
                {
                    reloadAnimation = "changeBlade_air";
                }
                else
                {
                    reloadAnimation = "changeBlade";
                }

                crossFade(reloadAnimation, 0.1f);
            }
        }
    }

    private void checkDashDoubleTap()
    {
        if (uTapTime >= 0f)
        {
            uTapTime += Time.deltaTime;
            if (uTapTime > 0.2f)
            {
                uTapTime = -1f;
            }
        }

        if (dTapTime >= 0f)
        {
            dTapTime += Time.deltaTime;
            if (dTapTime > 0.2f)
            {
                dTapTime = -1f;
            }
        }

        if (lTapTime >= 0f)
        {
            lTapTime += Time.deltaTime;
            if (lTapTime > 0.2f)
            {
                lTapTime = -1f;
            }
        }

        if (rTapTime >= 0f)
        {
            rTapTime += Time.deltaTime;
            if (rTapTime > 0.2f)
            {
                rTapTime = -1f;
            }
        }

        if (inputManager.isInputDown[InputCode.up])
        {
            if (uTapTime == -1f)
            {
                uTapTime = 0f;
            }

            if (uTapTime != 0f)
            {
                dashU = true;
            }
        }

        if (inputManager.isInputDown[InputCode.down])
        {
            if (dTapTime == -1f)
            {
                dTapTime = 0f;
            }

            if (dTapTime != 0f)
            {
                dashD = true;
            }
        }

        if (inputManager.isInputDown[InputCode.left])
        {
            if (lTapTime == -1f)
            {
                lTapTime = 0f;
            }

            if (lTapTime != 0f)
            {
                dashL = true;
            }
        }

        if (inputManager.isInputDown[InputCode.right])
        {
            if (rTapTime == -1f)
            {
                rTapTime = 0f;
            }

            if (rTapTime != 0f)
            {
                dashR = true;
            }
        }
    }

    private void checkDashRebind()
    {
        if (FengGameManagerMKII.inputRC.isInputHumanDown(InputCodeRC.dash))
        {
            if (inputManager.isInput[InputCode.up])
            {
                dashU = true;
            }
            else if (inputManager.isInput[InputCode.down])
            {
                dashD = true;
            }
            else if (inputManager.isInput[InputCode.left])
            {
                dashL = true;
            }
            else if (inputManager.isInput[InputCode.right])
            {
                dashR = true;
            }
        }
    }

    public void checkTitan()
    {
        int count;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = 1 << LayerMask.NameToLayer("PlayerAttackBox");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask3 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask4 = mask | mask2 | mask3;
        var hitArray = Physics.RaycastAll(ray, 180f, mask4.value);
        var list = new List<RaycastHit>();
        var list2 = new List<TITAN>();
        for (count = 0; count < hitArray.Length; count++)
        {
            var item = hitArray[count];
            list.Add(item);
        }

        list.Sort((x, y) => x.distance.CompareTo(y.distance));
        var num2 = 180f;
        for (count = 0; count < list.Count; count++)
        {
            var hit2 = list[count];
            var gameObject = hit2.collider.gameObject;
            if (gameObject.layer == 16)
            {
                if (gameObject.name.Contains("PlayerDetectorRC") && (hit2 = list[count]).distance < num2)
                {
                    num2 -= 60f;
                    if (num2 <= 60f)
                    {
                        count = list.Count;
                    }

                    var component = gameObject.transform.root.gameObject.GetComponent<TITAN>();
                    if (component != null)
                    {
                        list2.Add(component);
                    }
                }
            }
            else
            {
                count = list.Count;
            }
        }

        for (count = 0; count < myTitans.Count; count++)
        {
            var titan2 = myTitans[count];
            if (!list2.Contains(titan2))
            {
                titan2.isLook = false;
            }
        }

        for (count = 0; count < list2.Count; count++)
        {
            var titan3 = list2[count];
            titan3.isLook = true;
        }

        myTitans = list2;
    }

    public void ClearPopup()
    {
        FengGameManagerMKII.FGM.ShowHUDInfoCenter(string.Empty);
    }

    public void continueAnimation()
    {
        foreach (AnimationState animationState in animation)
        {
            if (animationState.speed == 1f)
            {
                return;
            }

            animationState.speed = 1f;
        }

        customAnimationSpeed();
        playAnimation(currentPlayingClipName());
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            photonView.RPC("netContinueAnimation", PhotonTargets.Others);
        }
    }

    public void crossFade(string aniName, float time)
    {
        currentAnimation = aniName;
        animation.CrossFade(aniName, time);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName, time };
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    public string currentPlayingClipName()
    {
        foreach (AnimationState animationState in animation)
        {
            if (animation.IsPlaying(animationState.name))
            {
                return animationState.name;
            }
        }

        return string.Empty;
    }

    private void customAnimationSpeed()
    {
        animation["attack5"].speed = 1.85f;
        animation["changeBlade"].speed = 1.2f;
        animation["air_release"].speed = 0.6f;
        animation["changeBlade_air"].speed = 0.8f;
        animation["AHSS_gun_reload_both"].speed = 0.38f;
        animation["AHSS_gun_reload_both_air"].speed = 0.5f;
        animation["AHSS_gun_reload_l"].speed = 0.4f;
        animation["AHSS_gun_reload_l_air"].speed = 0.5f;
        animation["AHSS_gun_reload_r"].speed = 0.4f;
        animation["AHSS_gun_reload_r_air"].speed = 0.5f;
    }

    private void dash(float horizontal, float vertical)
    {
        print(dashTime + " " + currentGas);
        if (dashTime <= 0f && currentGas > 0f && !isMounted)
        {
            useGas(totalGas * 0.04f);
            facingDirection = getGlobalFacingDirection(horizontal, vertical);
            dashV = getGlobaleFacingVector3(facingDirection);
            originVM = currentSpeed;
            var quaternion = Quaternion.Euler(0f, facingDirection, 0f);
            rigidbody.rotation = quaternion;
            targetRotation = quaternion;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                Instantiate(Resources.Load("FX/boost_smoke"), transform.position, transform.rotation);
            }
            else
            {
                PhotonNetwork.Instantiate("FX/boost_smoke", transform.position, transform.rotation, 0);
            }

            dashTime = 0.5f;
            crossFade("dash", 0.1f);
            animation["dash"].time = 0.1f;
            state = HERO_STATE.AirDodge;
            falseAttack();
            rigidbody.AddForce(dashV * 40f, ForceMode.VelocityChange);
        }
    }

    public void die(Vector3 v, bool isBite)
    {
        if (invincible <= 0f)
        {
            if (titanForm && eren_titan != null)
            {
                eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }

            if (bulletLeft != null)
            {
                bulletLeft.GetComponent<Bullet>().removeMe();
            }

            if (bulletRight != null)
            {
                bulletRight.GetComponent<Bullet>().removeMe();
            }

            meatDie.Play();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine) && !useGun)
            {
                leftbladetrail.Deactivate();
                rightbladetrail.Deactivate();
                leftbladetrail2.Deactivate();
                rightbladetrail2.Deactivate();
            }

            breakApart(v, isBite);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameLose();
            falseAttack();
            hasDied = true;
            var transform = this.transform.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
            if (Settings.SnapshotsSetting)
            {
                GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().startSnapShot(this.transform.position, 0, null, 0.02f);
            }

            Destroy(gameObject);
        }
    }

    public void die2(Transform tf)
    {
        if (invincible <= 0f)
        {
            if (titanForm && eren_titan != null)
            {
                eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }

            if (bulletLeft != null)
            {
                bulletLeft.GetComponent<Bullet>().removeMe();
            }

            if (bulletRight != null)
            {
                bulletRight.GetComponent<Bullet>().removeMe();
            }

            var transform = this.transform.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
            meatDie.Play();
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameLose();
            falseAttack();
            hasDied = true;
            var obj2 = (GameObject)Instantiate(Resources.Load("hitMeat2"));
            obj2.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }

    private void dodge(bool offTheWall = false)
    {
        if (myHorse != null && !isMounted && Vector3.Distance(myHorse.transform.position, transform.position) < 15f)
        {
            getOnHorse();
        }
        else
        {
            state = HERO_STATE.GroundDodge;
            if (!offTheWall)
            {
                float num;
                float num2;
                if (inputManager.isInput[InputCode.up])
                {
                    num = 1f;
                }
                else if (inputManager.isInput[InputCode.down])
                {
                    num = -1f;
                }
                else
                {
                    num = 0f;
                }

                if (inputManager.isInput[InputCode.left])
                {
                    num2 = -1f;
                }
                else if (inputManager.isInput[InputCode.right])
                {
                    num2 = 1f;
                }
                else
                {
                    num2 = 0f;
                }

                var num3 = getGlobalFacingDirection(num2, num);
                if (num2 != 0f || num != 0f)
                {
                    facingDirection = num3 + 180f;
                    targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                }

                crossFade("dodge", 0.1f);
            }
            else
            {
                playAnimation("dodge");
                playAnimationAt("dodge", 0.2f);
            }

            sparks.enableEmission = false;
        }
    }

    private void dodge2(bool offTheWall = false)
    {
        if (!FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseMount) || myHorse == null || isMounted || Vector3.Distance(myHorse.transform.position, transform.position) >= 15f)
        {
            state = HERO_STATE.GroundDodge;
            if (!offTheWall)
            {
                float num;
                float num2;
                if (inputManager.isInput[InputCode.up])
                {
                    num = 1f;
                }
                else if (inputManager.isInput[InputCode.down])
                {
                    num = -1f;
                }
                else
                {
                    num = 0f;
                }

                if (inputManager.isInput[InputCode.left])
                {
                    num2 = -1f;
                }
                else if (inputManager.isInput[InputCode.right])
                {
                    num2 = 1f;
                }
                else
                {
                    num2 = 0f;
                }

                var num3 = getGlobalFacingDirection(num2, num);
                if (num2 != 0f || num != 0f)
                {
                    facingDirection = num3 + 180f;
                    targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                }

                crossFade("dodge", 0.1f);
            }
            else
            {
                playAnimation("dodge");
                playAnimationAt("dodge", 0.2f);
            }

            sparks.enableEmission = false;
        }
    }

    private void erenTransform()
    {
        skillCDDuration = skillCDLast;
        if (bulletLeft != null)
        {
            bulletLeft.GetComponent<Bullet>().removeMe();
        }

        if (bulletRight != null)
        {
            bulletRight.GetComponent<Bullet>().removeMe();
        }

        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            eren_titan = (GameObject)Instantiate(Resources.Load("TITAN_EREN"), transform.position, transform.rotation);
        }
        else
        {
            eren_titan = PhotonNetwork.Instantiate("TITAN_EREN", transform.position, transform.rotation, 0);
        }

        eren_titan.GetComponent<TITAN_EREN>().realBody = gameObject;
        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().flashBlind();
        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(eren_titan);
        eren_titan.GetComponent<TITAN_EREN>().born();
        eren_titan.rigidbody.velocity = rigidbody.velocity;
        rigidbody.velocity = Vector3.zero;
        transform.position = eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
        titanForm = true;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            object[] parameters = { eren_titan.GetPhotonView().viewID };
            photonView.RPC("whoIsMyErenTitan", PhotonTargets.Others, parameters);
        }

        if (smoke_3dmg.enableEmission && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            object[] objArray2 = { false };
            photonView.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray2);
        }

        smoke_3dmg.enableEmission = false;
    }

    private void escapeFromGrab()
    {
    }

    public void falseAttack()
    {
        attackMove = false;
        if (useGun)
        {
            if (!attackReleased)
            {
                continueAnimation();
                attackReleased = true;
            }
        }
        else
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                leftbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                rightbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                leftbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
                rightbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
            }

            attackLoop = 0;
            if (!attackReleased)
            {
                continueAnimation();
                attackReleased = true;
            }
        }
    }

    public void fillGas()
    {
        currentGas = totalGas;
    }

    private GameObject findNearestTitan()
    {
        var objArray = GameObject.FindGameObjectsWithTag("titan");
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

    private void FixedUpdate()
    {
        if (!titanForm && !isCannon && (!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
        {
            currentSpeed = baseRigidBody.velocity.magnitude;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                if (!(baseAnimation.IsPlaying("attack3_2") || baseAnimation.IsPlaying("attack5") || baseAnimation.IsPlaying("special_petra")))
                {
                    baseRigidBody.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 6f);
                }

                if (state == HERO_STATE.Grab)
                {
                    baseRigidBody.AddForce(-baseRigidBody.velocity, ForceMode.VelocityChange);
                }
                else
                {
                    if (IsGrounded())
                    {
                        if (!grounded)
                        {
                            justGrounded = true;
                        }

                        grounded = true;
                    }
                    else
                    {
                        grounded = false;
                    }

                    if (hookSomeOne)
                    {
                        if (hookTarget != null)
                        {
                            var vector2 = hookTarget.transform.position - baseTransform.position;
                            var magnitude = vector2.magnitude;
                            if (magnitude > 2f)
                            {
                                baseRigidBody.AddForce(vector2.normalized * Mathf.Pow(magnitude, 0.15f) * 30f - baseRigidBody.velocity * 0.95f, ForceMode.VelocityChange);
                            }
                        }
                        else
                        {
                            hookSomeOne = false;
                        }
                    }
                    else if (hookBySomeOne && badGuy != null)
                    {
                        if (badGuy != null)
                        {
                            var vector3 = badGuy.transform.position - baseTransform.position;
                            var f = vector3.magnitude;
                            if (f > 5f)
                            {
                                baseRigidBody.AddForce(vector3.normalized * Mathf.Pow(f, 0.15f) * 0.2f, ForceMode.Impulse);
                            }
                        }
                        else
                        {
                            hookBySomeOne = false;
                        }
                    }

                    var x = 0f;
                    var z = 0f;
                    if (!IN_GAME_MAIN_CAMERA.isTyping)
                    {
                        if (inputManager.isInput[InputCode.up])
                        {
                            z = 1f;
                        }
                        else if (inputManager.isInput[InputCode.down])
                        {
                            z = -1f;
                        }
                        else
                        {
                            z = 0f;
                        }

                        if (inputManager.isInput[InputCode.left])
                        {
                            x = -1f;
                        }
                        else if (inputManager.isInput[InputCode.right])
                        {
                            x = 1f;
                        }
                        else
                        {
                            x = 0f;
                        }
                    }

                    var flag2 = false;
                    var flag3 = false;
                    var flag4 = false;
                    isLeftHandHooked = false;
                    isRightHandHooked = false;
                    if (isLaunchLeft)
                    {
                        if (bulletLeft != null && bulletLeft.GetComponent<Bullet>().isHooked())
                        {
                            isLeftHandHooked = true;
                            var to = bulletLeft.transform.position - baseTransform.position;
                            to.Normalize();
                            to = to * 10f;
                            if (!isLaunchRight)
                            {
                                to = to * 2f;
                            }

                            if (Vector3.Angle(baseRigidBody.velocity, to) > 90f && inputManager.isInput[InputCode.jump])
                            {
                                flag3 = true;
                                flag2 = true;
                            }

                            if (!flag3)
                            {
                                baseRigidBody.AddForce(to);
                                if (Vector3.Angle(baseRigidBody.velocity, to) > 90f)
                                {
                                    baseRigidBody.AddForce(-baseRigidBody.velocity * 2f, ForceMode.Acceleration);
                                }
                            }

                            if (!Settings.BodyLean && !useGun)
                            {
                                facingDirection = Mathf.Atan2(to.x, to.z) * 57.29578f;
                                var rotation = Quaternion.Euler(0f, facingDirection, 0f);
                                baseTransform.rotation = rotation;
                                baseTransform.rotation = rotation;
                            }
                        }

                        launchElapsedTimeL += Time.deltaTime;
                        if (QHold && currentGas > 0f)
                        {
                            useGas(useGasSpeed * Time.deltaTime);
                        }
                        else if (launchElapsedTimeL > 0.3f)
                        {
                            isLaunchLeft = false;
                            if (bulletLeft != null)
                            {
                                bulletLeft.GetComponent<Bullet>().disable();
                                releaseIfIHookSb();
                                bulletLeft = null;
                                flag3 = false;
                            }
                        }
                    }

                    if (isLaunchRight)
                    {
                        if (bulletRight != null && bulletRight.GetComponent<Bullet>().isHooked())
                        {
                            isRightHandHooked = true;
                            var vector5 = bulletRight.transform.position - baseTransform.position;
                            vector5.Normalize();
                            vector5 = vector5 * 10f;
                            if (!isLaunchLeft)
                            {
                                vector5 = vector5 * 2f;
                            }

                            if (Vector3.Angle(baseRigidBody.velocity, vector5) > 90f && inputManager.isInput[InputCode.jump])
                            {
                                flag4 = true;
                                flag2 = true;
                            }

                            if (!flag4)
                            {
                                baseRigidBody.AddForce(vector5);
                                if (Vector3.Angle(baseRigidBody.velocity, vector5) > 90f)
                                {
                                    baseRigidBody.AddForce(-baseRigidBody.velocity * 2f, ForceMode.Acceleration);
                                }
                            }

                            if (!Settings.BodyLean && !useGun)
                            {
                                facingDirection = Mathf.Atan2(vector5.x, vector5.z) * 57.29578f;
                                var rotation = Quaternion.Euler(0f, facingDirection, 0f);
                                baseTransform.rotation = rotation;
                                baseTransform.rotation = rotation;
                            }
                        }

                        launchElapsedTimeR += Time.deltaTime;
                        if (EHold && currentGas > 0f)
                        {
                            useGas(useGasSpeed * Time.deltaTime);
                        }
                        else if (launchElapsedTimeR > 0.3f)
                        {
                            isLaunchRight = false;
                            if (bulletRight != null)
                            {
                                bulletRight.GetComponent<Bullet>().disable();
                                releaseIfIHookSb();
                                bulletRight = null;
                                flag4 = false;
                            }
                        }
                    }

                    if (grounded)
                    {
                        Vector3 vector7;
                        var zero = Vector3.zero;
                        if (state == HERO_STATE.Attack)
                        {
                            if (attackAnimation == "attack5")
                            {
                                if (baseAnimation[attackAnimation].normalizedTime > 0.4f && baseAnimation[attackAnimation].normalizedTime < 0.61f)
                                {
                                    baseRigidBody.AddForce(gameObject.transform.forward * 200f);
                                }
                            }
                            else if (attackAnimation == "special_petra")
                            {
                                if (baseAnimation[attackAnimation].normalizedTime > 0.35f && baseAnimation[attackAnimation].normalizedTime < 0.48f)
                                {
                                    baseRigidBody.AddForce(gameObject.transform.forward * 200f);
                                }
                            }
                            else if (baseAnimation.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                            else if (baseAnimation.IsPlaying("attack1") || baseAnimation.IsPlaying("attack2"))
                            {
                                baseRigidBody.AddForce(gameObject.transform.forward * 200f);
                            }

                            if (baseAnimation.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                        }

                        if (justGrounded)
                        {
                            if (state != HERO_STATE.Attack || attackAnimation != "attack3_1" && attackAnimation != "attack5" && attackAnimation != "special_petra")
                            {
                                if (state != HERO_STATE.Attack && x == 0f && z == 0f && bulletLeft == null && bulletRight == null && state != HERO_STATE.FillGas)
                                {
                                    state = HERO_STATE.Land;
                                    crossFade("dash_land", 0.01f);
                                }
                                else
                                {
                                    buttonAttackRelease = true;
                                    if (state != HERO_STATE.Attack && baseRigidBody.velocity.x * baseRigidBody.velocity.x + baseRigidBody.velocity.z * baseRigidBody.velocity.z > speed * speed * 1.5f && state != HERO_STATE.FillGas)
                                    {
                                        state = HERO_STATE.Slide;
                                        crossFade("slide", 0.05f);
                                        facingDirection = Mathf.Atan2(baseRigidBody.velocity.x, baseRigidBody.velocity.z) * 57.29578f;
                                        targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                                        sparks.enableEmission = true;
                                    }
                                }
                            }

                            justGrounded = false;
                            zero = baseRigidBody.velocity;
                        }

                        if (state == HERO_STATE.Attack && attackAnimation == "attack3_1" && baseAnimation[attackAnimation].normalizedTime >= 1f)
                        {
                            playAnimation("attack3_2");
                            resetAnimationSpeed();
                            vector7 = Vector3.zero;
                            baseRigidBody.velocity = vector7;
                            zero = vector7;
                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(0.2f, 0.3f);
                        }

                        if (state == HERO_STATE.GroundDodge)
                        {
                            if (baseAnimation["dodge"].normalizedTime >= 0.2f && baseAnimation["dodge"].normalizedTime < 0.8f)
                            {
                                zero = -baseTransform.forward * 2.4f * speed;
                            }

                            if (baseAnimation["dodge"].normalizedTime > 0.8f)
                            {
                                zero = baseRigidBody.velocity * 0.9f;
                            }
                        }
                        else if (state == HERO_STATE.Idle)
                        {
                            var vector8 = new Vector3(x, 0f, z);
                            var resultAngle = getGlobalFacingDirection(x, z);
                            zero = getGlobaleFacingVector3(resultAngle);
                            var num6 = vector8.magnitude <= 0.95f ? vector8.magnitude >= 0.25f ? vector8.magnitude : 0f : 1f;
                            zero = zero * num6;
                            zero = zero * speed;
                            if (buffTime > 0f && currentBuff == BUFF.SpeedUp)
                            {
                                zero = zero * 4f;
                            }

                            if (x != 0f || z != 0f)
                            {
                                if (!baseAnimation.IsPlaying("run") && !baseAnimation.IsPlaying("jump") && !baseAnimation.IsPlaying("run_sasha") && (!baseAnimation.IsPlaying("horse_geton") || baseAnimation["horse_geton"].normalizedTime >= 0.5f))
                                {
                                    if (buffTime > 0f && currentBuff == BUFF.SpeedUp)
                                    {
                                        crossFade("run_sasha", 0.1f);
                                    }
                                    else
                                    {
                                        crossFade("run", 0.1f);
                                    }
                                }
                            }
                            else
                            {
                                if (!(baseAnimation.IsPlaying(standAnimation) || state == HERO_STATE.Land || baseAnimation.IsPlaying("jump") || baseAnimation.IsPlaying("horse_geton") || baseAnimation.IsPlaying("grabbed")))
                                {
                                    crossFade(standAnimation, 0.1f);
                                    zero = zero * 0f;
                                }

                                resultAngle = -874f;
                            }

                            if (resultAngle != -874f)
                            {
                                facingDirection = resultAngle;
                                targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                            }
                        }
                        else if (state == HERO_STATE.Land)
                        {
                            zero = baseRigidBody.velocity * 0.96f;
                        }
                        else if (state == HERO_STATE.Slide)
                        {
                            zero = baseRigidBody.velocity * 0.99f;
                            if (currentSpeed < speed * 1.2f)
                            {
                                idle();
                                sparks.enableEmission = false;
                            }
                        }

                        var velocity = baseRigidBody.velocity;
                        var force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -maxVelocityChange, maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -maxVelocityChange, maxVelocityChange);
                        force.y = 0f;
                        if (baseAnimation.IsPlaying("jump") && baseAnimation["jump"].normalizedTime > 0.18f)
                        {
                            force.y += 8f;
                        }

                        if (baseAnimation.IsPlaying("horse_geton") && baseAnimation["horse_geton"].normalizedTime > 0.18f && baseAnimation["horse_geton"].normalizedTime < 1f)
                        {
                            var num7 = 6f;
                            force = -baseRigidBody.velocity;
                            force.y = num7;
                            var num8 = Vector3.Distance(myHorse.transform.position, baseTransform.position);
                            var num9 = 0.6f * gravity * num8 / (2f * num7);
                            vector7 = myHorse.transform.position - baseTransform.position;
                            force += num9 * vector7.normalized;
                        }

                        if (!(state == HERO_STATE.Attack && useGun))
                        {
                            baseRigidBody.AddForce(force, ForceMode.VelocityChange);
                            baseRigidBody.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, facingDirection, 0f), Time.deltaTime * 10f);
                        }
                    }
                    else
                    {
                        if (sparks.enableEmission)
                        {
                            sparks.enableEmission = false;
                        }

                        if (myHorse != null && (baseAnimation.IsPlaying("horse_geton") || baseAnimation.IsPlaying("air_fall")) && baseRigidBody.velocity.y < 0f && Vector3.Distance(myHorse.transform.position + Vector3.up * 1.65f, baseTransform.position) < 0.5f)
                        {
                            baseTransform.position = myHorse.transform.position + Vector3.up * 1.65f;
                            baseTransform.rotation = myHorse.transform.rotation;
                            isMounted = true;
                            crossFade("horse_idle", 0.1f);
                            myHorse.GetComponent<Horse>().mounted();
                        }

                        if (state == HERO_STATE.Idle && !baseAnimation.IsPlaying("dash") && !baseAnimation.IsPlaying("wallrun") && !baseAnimation.IsPlaying("toRoof") && !baseAnimation.IsPlaying("horse_geton") && !baseAnimation.IsPlaying("horse_getoff") && !baseAnimation.IsPlaying("air_release") && !isMounted && (!baseAnimation.IsPlaying("air_hook_l_just") || baseAnimation["air_hook_l_just"].normalizedTime >= 1f) && (!baseAnimation.IsPlaying("air_hook_r_just") || baseAnimation["air_hook_r_just"].normalizedTime >= 1f) || baseAnimation["dash"].normalizedTime >= 0.99f)
                        {
                            if (!isLeftHandHooked && !isRightHandHooked && (baseAnimation.IsPlaying("air_hook_l") || baseAnimation.IsPlaying("air_hook_r") || baseAnimation.IsPlaying("air_hook")) && baseRigidBody.velocity.y > 20f)
                            {
                                baseAnimation.CrossFade("air_release");
                            }
                            else
                            {
                                var flag5 = Mathf.Abs(baseRigidBody.velocity.x) + Mathf.Abs(baseRigidBody.velocity.z) > 25f;
                                var flag6 = baseRigidBody.velocity.y < 0f;
                                if (!flag5)
                                {
                                    if (flag6)
                                    {
                                        if (!baseAnimation.IsPlaying("air_fall"))
                                        {
                                            crossFade("air_fall", 0.2f);
                                        }
                                    }
                                    else if (!baseAnimation.IsPlaying("air_rise"))
                                    {
                                        crossFade("air_rise", 0.2f);
                                    }
                                }
                                else if (!isLeftHandHooked && !isRightHandHooked)
                                {
                                    var current = -Mathf.Atan2(baseRigidBody.velocity.z, baseRigidBody.velocity.x) * 57.29578f;
                                    var num11 = -Mathf.DeltaAngle(current, baseTransform.rotation.eulerAngles.y - 90f);
                                    if (Mathf.Abs(num11) < 45f)
                                    {
                                        if (!baseAnimation.IsPlaying("air2"))
                                        {
                                            crossFade("air2", 0.2f);
                                        }
                                    }
                                    else if (num11 < 135f && num11 > 0f)
                                    {
                                        if (!baseAnimation.IsPlaying("air2_right"))
                                        {
                                            crossFade("air2_right", 0.2f);
                                        }
                                    }
                                    else if (num11 > -135f && num11 < 0f)
                                    {
                                        if (!baseAnimation.IsPlaying("air2_left"))
                                        {
                                            crossFade("air2_left", 0.2f);
                                        }
                                    }
                                    else if (!baseAnimation.IsPlaying("air2_backward"))
                                    {
                                        crossFade("air2_backward", 0.2f);
                                    }
                                }
                                else if (useGun)
                                {
                                    if (!isRightHandHooked)
                                    {
                                        if (!baseAnimation.IsPlaying("AHSS_hook_forward_l"))
                                        {
                                            crossFade("AHSS_hook_forward_l", 0.1f);
                                        }
                                    }
                                    else if (!isLeftHandHooked)
                                    {
                                        if (!baseAnimation.IsPlaying("AHSS_hook_forward_r"))
                                        {
                                            crossFade("AHSS_hook_forward_r", 0.1f);
                                        }
                                    }
                                    else if (!baseAnimation.IsPlaying("AHSS_hook_forward_both"))
                                    {
                                        crossFade("AHSS_hook_forward_both", 0.1f);
                                    }
                                }
                                else if (!isRightHandHooked)
                                {
                                    if (!baseAnimation.IsPlaying("air_hook_l"))
                                    {
                                        crossFade("air_hook_l", 0.1f);
                                    }
                                }
                                else if (!isLeftHandHooked)
                                {
                                    if (!baseAnimation.IsPlaying("air_hook_r"))
                                    {
                                        crossFade("air_hook_r", 0.1f);
                                    }
                                }
                                else if (!baseAnimation.IsPlaying("air_hook"))
                                {
                                    crossFade("air_hook", 0.1f);
                                }
                            }
                        }

                        if (state == HERO_STATE.Idle && baseAnimation.IsPlaying("air_release") && baseAnimation["air_release"].normalizedTime >= 1f)
                        {
                            crossFade("air_rise", 0.2f);
                        }

                        if (baseAnimation.IsPlaying("horse_getoff") && baseAnimation["horse_getoff"].normalizedTime >= 1f)
                        {
                            crossFade("air_rise", 0.2f);
                        }

                        if (baseAnimation.IsPlaying("toRoof"))
                        {
                            if (baseAnimation["toRoof"].normalizedTime < 0.22f)
                            {
                                baseRigidBody.velocity = Vector3.zero;
                                baseRigidBody.AddForce(new Vector3(0f, gravity * baseRigidBody.mass, 0f));
                            }
                            else
                            {
                                if (!wallJump)
                                {
                                    wallJump = true;
                                    baseRigidBody.AddForce(Vector3.up * 8f, ForceMode.Impulse);
                                }

                                baseRigidBody.AddForce(baseTransform.forward * 0.05f, ForceMode.Impulse);
                            }

                            if (baseAnimation["toRoof"].normalizedTime >= 1f)
                            {
                                playAnimation("air_rise");
                            }
                        }
                        else if (!(state != HERO_STATE.Idle || !isPressDirectionTowardsHero(x, z) || inputManager.isInput[InputCode.jump] || inputManager.isInput[InputCode.leftRope] || inputManager.isInput[InputCode.rightRope] || inputManager.isInput[InputCode.bothRope] || !IsFrontGrounded() || baseAnimation.IsPlaying("wallrun") || baseAnimation.IsPlaying("dodge")))
                        {
                            crossFade("wallrun", 0.1f);
                            wallRunTime = 0f;
                        }
                        else if (baseAnimation.IsPlaying("wallrun"))
                        {
                            baseRigidBody.AddForce(Vector3.up * speed - baseRigidBody.velocity, ForceMode.VelocityChange);
                            wallRunTime += Time.deltaTime;
                            if (wallRunTime > 1f || z == 0f && x == 0f)
                            {
                                baseRigidBody.AddForce(-baseTransform.forward * speed * 0.75f, ForceMode.Impulse);
                                dodge2(true);
                            }
                            else if (!IsUpFrontGrounded())
                            {
                                wallJump = false;
                                crossFade("toRoof", 0.1f);
                            }
                            else if (!IsFrontGrounded())
                            {
                                crossFade("air_fall", 0.1f);
                            }
                        }
                        else if (!baseAnimation.IsPlaying("attack5") && !baseAnimation.IsPlaying("special_petra") && !baseAnimation.IsPlaying("dash") && !baseAnimation.IsPlaying("jump"))
                        {
                            var vector11 = new Vector3(x, 0f, z);
                            var num12 = getGlobalFacingDirection(x, z);
                            var vector12 = getGlobaleFacingVector3(num12);
                            var num13 = vector11.magnitude <= 0.95f ? vector11.magnitude >= 0.25f ? vector11.magnitude : 0f : 1f;
                            vector12 = vector12 * num13;
                            vector12 = vector12 * (setup.myCostume.stat.ACL / 10f * 2f);
                            if (x == 0f && z == 0f)
                            {
                                if (state == HERO_STATE.Attack)
                                {
                                    vector12 = vector12 * 0f;
                                }

                                num12 = -874f;
                            }

                            if (num12 != -874f)
                            {
                                facingDirection = num12;
                                targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                            }

                            if (!flag3 && !flag4 && !isMounted && inputManager.isInput[InputCode.jump] && currentGas > 0f)
                            {
                                if (x != 0f || z != 0f)
                                {
                                    baseRigidBody.AddForce(vector12, ForceMode.Acceleration);
                                }
                                else
                                {
                                    baseRigidBody.AddForce(baseTransform.forward * vector12.magnitude, ForceMode.Acceleration);
                                }

                                flag2 = true;
                            }
                        }

                        if (baseAnimation.IsPlaying("air_fall") && currentSpeed < 0.2f && IsFrontGrounded())
                        {
                            crossFade("onWall", 0.3f);
                        }
                    }

                    if (flag3 && flag4)
                    {
                        var num14 = currentSpeed + 0.1f;
                        baseRigidBody.AddForce(-baseRigidBody.velocity, ForceMode.VelocityChange);
                        var vector13 = (bulletRight.transform.position + bulletLeft.transform.position) * 0.5f - baseTransform.position;
                        var num15 = 0f;
                        if (Settings.ReelingSettings[0] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num15 = -1f;
                        }
                        else if (Settings.ReelingSettings[1] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num15 = 1f;
                        }
                        else
                        {
                            num15 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }

                        num15 = Mathf.Clamp(num15, -0.8f, 0.8f);
                        var num16 = 1f + num15;
                        var vector14 = Vector3.RotateTowards(vector13, baseRigidBody.velocity, 1.53938f * num16, 1.53938f * num16);
                        vector14.Normalize();
                        baseRigidBody.velocity = vector14 * num14;
                    }
                    else if (flag3)
                    {
                        var num17 = currentSpeed + 0.1f;
                        baseRigidBody.AddForce(-baseRigidBody.velocity, ForceMode.VelocityChange);
                        var vector15 = bulletLeft.transform.position - baseTransform.position;
                        var num18 = 0f;
                        if (Settings.ReelingSettings[0] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num18 = -1f;
                        }
                        else if (Settings.ReelingSettings[1] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num18 = 1f;
                        }
                        else
                        {
                            num18 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }

                        num18 = Mathf.Clamp(num18, -0.8f, 0.8f);
                        var num19 = 1f + num18;
                        var vector16 = Vector3.RotateTowards(vector15, baseRigidBody.velocity, 1.53938f * num19, 1.53938f * num19);
                        vector16.Normalize();
                        baseRigidBody.velocity = vector16 * num17;
                    }
                    else if (flag4)
                    {
                        var num20 = currentSpeed + 0.1f;
                        baseRigidBody.AddForce(-baseRigidBody.velocity, ForceMode.VelocityChange);
                        var vector17 = bulletRight.transform.position - baseTransform.position;
                        var num21 = 0f;
                        if (Settings.ReelingSettings[0] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num21 = -1f;
                        }
                        else if (Settings.ReelingSettings[1] && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num21 = 1f;
                        }
                        else
                        {
                            num21 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }

                        num21 = Mathf.Clamp(num21, -0.8f, 0.8f);
                        var num22 = 1f + num21;
                        var vector18 = Vector3.RotateTowards(vector17, baseRigidBody.velocity, 1.53938f * num22, 1.53938f * num22);
                        vector18.Normalize();
                        baseRigidBody.velocity = vector18 * num20;
                    }

                    if (state == HERO_STATE.Attack && (attackAnimation == "attack5" || attackAnimation == "special_petra") && baseAnimation[attackAnimation].normalizedTime > 0.4f && !attackMove)
                    {
                        attackMove = true;
                        if (launchPointRight.magnitude > 0f)
                        {
                            var vector19 = launchPointRight - baseTransform.position;
                            vector19.Normalize();
                            vector19 = vector19 * 13f;
                            baseRigidBody.AddForce(vector19, ForceMode.Impulse);
                        }

                        if (attackAnimation == "special_petra" && launchPointLeft.magnitude > 0f)
                        {
                            var vector20 = launchPointLeft - baseTransform.position;
                            vector20.Normalize();
                            vector20 = vector20 * 13f;
                            baseRigidBody.AddForce(vector20, ForceMode.Impulse);
                            if (bulletRight != null)
                            {
                                bulletRight.GetComponent<Bullet>().disable();
                                releaseIfIHookSb();
                            }

                            if (bulletLeft != null)
                            {
                                bulletLeft.GetComponent<Bullet>().disable();
                                releaseIfIHookSb();
                            }
                        }

                        baseRigidBody.AddForce(Vector3.up * 2f, ForceMode.Impulse);
                    }

                    var flag7 = false;
                    if (bulletLeft != null || bulletRight != null)
                    {
                        if (bulletLeft != null && bulletLeft.transform.position.y > gameObject.transform.position.y && isLaunchLeft && bulletLeft.GetComponent<Bullet>().isHooked())
                        {
                            flag7 = true;
                        }

                        if (bulletRight != null && bulletRight.transform.position.y > gameObject.transform.position.y && isLaunchRight && bulletRight.GetComponent<Bullet>().isHooked())
                        {
                            flag7 = true;
                        }
                    }

                    if (flag7)
                    {
                        baseRigidBody.AddForce(new Vector3(0f, -10f * baseRigidBody.mass, 0f));
                    }
                    else
                    {
                        baseRigidBody.AddForce(new Vector3(0f, -gravity * baseRigidBody.mass, 0f));
                    }

                    if (!Settings.CameraStaticFOVSetting)
                    {
                        if (currentSpeed > 10f)
                        {
                            currentCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentCamera.GetComponent<Camera>().fieldOfView, Mathf.Min(100f, currentSpeed + 40f), 0.1f);
                        }
                        else
                        {
                            currentCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentCamera.GetComponent<Camera>().fieldOfView, 50f, 0.1f);
                        }
                    }
                    else
                    {
                        currentCamera.GetComponent<Camera>().fieldOfView = Settings.CameraFOVSetting;
                    }

                    if (flag2)
                    {
                        useGas(useGasSpeed * Time.deltaTime);
                        if (!smoke_3dmg.enableEmission && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
                        {
                            object[] parameters = { true };
                            photonView.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
                        }

                        smoke_3dmg.enableEmission = true;
                    }
                    else
                    {
                        if (smoke_3dmg.enableEmission && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
                        {
                            object[] objArray3 = { false };
                            photonView.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray3);
                        }

                        smoke_3dmg.enableEmission = false;
                    }

                    if (currentSpeed > 80f)
                    {
                        if (!speedFXPS.enableEmission)
                        {
                            speedFXPS.enableEmission = true;
                        }

                        speedFXPS.startSpeed = currentSpeed;
                        speedFX.transform.LookAt(baseTransform.position + baseRigidBody.velocity);
                    }
                    else if (speedFXPS.enableEmission)
                    {
                        speedFXPS.enableEmission = false;
                    }
                }
            }
        }
    }

    public string getDebugInfo()
    {
        var str = "\n";
        str = "Left:" + isLeftHandHooked + " ";
        if (isLeftHandHooked && bulletLeft != null)
        {
            var vector = bulletLeft.transform.position - transform.position;
            str = str + (int)(Mathf.Atan2(vector.x, vector.z) * 57.29578f);
        }

        var str2 = str;
        object[] objArray1 = { str2, "\nRight:", isRightHandHooked, " " };
        str = string.Concat(objArray1);
        if (isRightHandHooked && bulletRight != null)
        {
            var vector2 = bulletRight.transform.position - transform.position;
            str = str + (int)(Mathf.Atan2(vector2.x, vector2.z) * 57.29578f);
        }

        str = str + "\nfacingDirection:" + (int)facingDirection + "\nActual facingDirection:" + (int)transform.rotation.eulerAngles.y + "\nState:" + state + "\n\n\n\n\n";
        if (state == HERO_STATE.Attack)
        {
            targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
        }

        return str;
    }

    private Vector3 getGlobaleFacingVector3(float resultAngle)
    {
        var num = -resultAngle + 90f;
        var x = Mathf.Cos(num * 0.01745329f);
        return new Vector3(x, 0f, Mathf.Sin(num * 0.01745329f));
    }

    private Vector3 getGlobaleFacingVector3(float horizontal, float vertical)
    {
        var num = -getGlobalFacingDirection(horizontal, vertical) + 90f;
        var x = Mathf.Cos(num * 0.01745329f);
        return new Vector3(x, 0f, Mathf.Sin(num * 0.01745329f));
    }

    private float getGlobalFacingDirection(float horizontal, float vertical)
    {
        if (vertical == 0f && horizontal == 0f)
        {
            return transform.rotation.eulerAngles.y;
        }

        var y = currentCamera.transform.rotation.eulerAngles.y;
        var num2 = Mathf.Atan2(vertical, horizontal) * 57.29578f;
        num2 = -num2 + 90f;
        return y + num2;
    }

    private float getLeanAngle(Vector3 p, bool left)
    {
        if (!useGun && state == HERO_STATE.Attack)
        {
            return 0f;
        }

        var num = p.y - transform.position.y;
        var num2 = Vector3.Distance(p, transform.position);
        var a = Mathf.Acos(num / num2) * 57.29578f;
        a *= 0.1f;
        a *= 1f + Mathf.Pow(rigidbody.velocity.magnitude, 0.2f);
        var vector3 = p - transform.position;
        var current = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
        var target = Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.z) * 57.29578f;
        var num6 = Mathf.DeltaAngle(current, target);
        a += Mathf.Abs(num6 * 0.5f);
        if (state != HERO_STATE.Attack)
        {
            a = Mathf.Min(a, 80f);
        }

        if (num6 > 0f)
        {
            leanLeft = true;
        }
        else
        {
            leanLeft = false;
        }

        if (useGun)
        {
            return a * (num6 >= 0f ? 1 : -1);
        }

        var num7 = 0f;
        if (left && num6 < 0f || !left && num6 > 0f)
        {
            num7 = 0.1f;
        }
        else
        {
            num7 = 0.5f;
        }

        return a * (num6 >= 0f ? num7 : -num7);
    }

    private void getOffHorse()
    {
        playAnimation("horse_getoff");
        rigidbody.AddForce(Vector3.up * 10f - transform.forward * 2f - transform.right * 1f, ForceMode.VelocityChange);
        unmounted();
    }

    private void getOnHorse()
    {
        playAnimation("horse_geton");
        facingDirection = myHorse.transform.rotation.eulerAngles.y;
        targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
    }

    public void getSupply()
    {
        if ((animation.IsPlaying(standAnimation) || animation.IsPlaying("run") || animation.IsPlaying("run_sasha")) && (currentBladeSta != totalBladeSta || currentBladeNum != totalBladeNum || currentGas != totalGas || leftBulletLeft != bulletMAX || rightBulletLeft != bulletMAX))
        {
            state = HERO_STATE.FillGas;
            crossFade("supply", 0.1f);
        }
    }

    public void grabbed(GameObject titan, bool leftHand)
    {
        if (isMounted)
        {
            unmounted();
        }

        state = HERO_STATE.Grab;
        GetComponent<CapsuleCollider>().isTrigger = true;
        falseAttack();
        titanWhoGrabMe = titan;
        if (titanForm && eren_titan != null)
        {
            eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
        }

        if (!useGun && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine))
        {
            leftbladetrail.Deactivate();
            rightbladetrail.Deactivate();
            leftbladetrail2.Deactivate();
            rightbladetrail2.Deactivate();
        }

        smoke_3dmg.enableEmission = false;
        sparks.enableEmission = false;
    }

    public bool HasDied()
    {
        return hasDied || isInvincible();
    }

    private void headMovement()
    {
        var transform = this.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
        var transform2 = this.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck");
        var x = Mathf.Sqrt((gunTarget.x - this.transform.position.x) * (gunTarget.x - this.transform.position.x) + (gunTarget.z - this.transform.position.z) * (gunTarget.z - this.transform.position.z));
        targetHeadRotation = transform.rotation;
        var vector5 = gunTarget - this.transform.position;
        var current = -Mathf.Atan2(vector5.z, vector5.x) * 57.29578f;
        var num3 = -Mathf.DeltaAngle(current, this.transform.rotation.eulerAngles.y - 90f);
        num3 = Mathf.Clamp(num3, -40f, 40f);
        var y = transform2.position.y - gunTarget.y;
        var num5 = Mathf.Atan2(y, x) * 57.29578f;
        num5 = Mathf.Clamp(num5, -40f, 30f);
        targetHeadRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + num5, transform.rotation.eulerAngles.y + num3, transform.rotation.eulerAngles.z);
        oldHeadRotation = Quaternion.Lerp(oldHeadRotation, targetHeadRotation, Time.deltaTime * 60f);
        transform.rotation = oldHeadRotation;
    }

    public void hookedByHuman(int hooker, Vector3 hookPosition)
    {
        object[] parameters = { hooker, hookPosition };
        photonView.RPC("RPCHookedByHuman", photonView.owner, parameters);
    }

    [RPC]
    public void hookFail()
    {
        hookTarget = null;
        hookSomeOne = false;
    }

    public void hookToHuman(GameObject target, Vector3 hookPosition)
    {
        releaseIfIHookSb();
        hookTarget = target;
        hookSomeOne = true;
        if (target.GetComponent<HERO>() != null)
        {
            target.GetComponent<HERO>().hookedByHuman(photonView.viewID, hookPosition);
        }

        launchForce = hookPosition - transform.position;
        var num = Mathf.Pow(launchForce.magnitude, 0.1f);
        if (grounded)
        {
            rigidbody.AddForce(Vector3.up * Mathf.Min(launchForce.magnitude * 0.2f, 10f), ForceMode.Impulse);
        }

        rigidbody.AddForce(launchForce * num * 0.1f, ForceMode.Impulse);
    }

    private void idle()
    {
        if (state == HERO_STATE.Attack)
        {
            falseAttack();
        }

        state = HERO_STATE.Idle;
        crossFade(standAnimation, 0.1f);
    }

    private bool IsFrontGrounded()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(gameObject.transform.position + gameObject.transform.up * 1f, gameObject.transform.forward, 1f, mask3.value);
    }

    public bool IsGrounded()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(gameObject.transform.position + Vector3.up * 0.1f, -Vector3.up, 0.3f, mask3.value);
    }

    public bool isInvincible()
    {
        return invincible > 0f;
    }

    private bool isPressDirectionTowardsHero(float h, float v)
    {
        if (h == 0f && v == 0f)
        {
            return false;
        }

        return Mathf.Abs(Mathf.DeltaAngle(getGlobalFacingDirection(h, v), transform.rotation.eulerAngles.y)) < 45f;
    }

    private bool IsUpFrontGrounded()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(gameObject.transform.position + gameObject.transform.up * 3f, gameObject.transform.forward, 1.2f, mask3.value);
    }

    [RPC]
    private void killObject()
    {
        Destroy(gameObject);
    }

    public void lateUpdate()
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && myNetWorkName != null)
        {
            if (titanForm && eren_titan != null)
            {
                myNetWorkName.transform.localPosition = Vector3.up * Screen.height * 2f;
            }

            var start = new Vector3(baseTransform.position.x, baseTransform.position.y + 2f, baseTransform.position.z);
            var maincamera = this.maincamera;
            LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if (Vector3.Angle(maincamera.transform.forward, start - maincamera.transform.position) > 90f || Physics.Linecast(start, maincamera.transform.position, mask3))
            {
                myNetWorkName.transform.localPosition = Vector3.up * Screen.height * 2f;
            }
            else
            {
                Vector2 vector2 = this.maincamera.GetComponent<Camera>().WorldToScreenPoint(start);
                myNetWorkName.transform.localPosition = new Vector3((int)(vector2.x - Screen.width * 0.5f), (int)(vector2.y - Screen.height * 0.5f), 0f);
            }
        }

        if (!titanForm && !isCannon)
        {
            if (Settings.CameraTiltSetting && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine))
            {
                Quaternion quaternion2;
                var zero = Vector3.zero;
                var position = Vector3.zero;
                if (isLaunchLeft && bulletLeft != null && bulletLeft.GetComponent<Bullet>().isHooked())
                {
                    zero = bulletLeft.transform.position;
                }

                if (isLaunchRight && bulletRight != null && bulletRight.GetComponent<Bullet>().isHooked())
                {
                    position = bulletRight.transform.position;
                }

                var vector5 = Vector3.zero;
                if (zero.magnitude != 0f && position.magnitude == 0f)
                {
                    vector5 = zero;
                }
                else if (zero.magnitude == 0f && position.magnitude != 0f)
                {
                    vector5 = position;
                }
                else if (zero.magnitude != 0f && position.magnitude != 0f)
                {
                    vector5 = (zero + position) * 0.5f;
                }

                var from = Vector3.Project(vector5 - baseTransform.position, maincamera.transform.up);
                var vector7 = Vector3.Project(vector5 - baseTransform.position, maincamera.transform.right);
                if (vector5.magnitude > 0f)
                {
                    var to = from + vector7;
                    var num = Vector3.Angle(vector5 - baseTransform.position, baseRigidBody.velocity) * 0.005f;
                    var vector9 = maincamera.transform.right + vector7.normalized;
                    quaternion2 = Quaternion.Euler(maincamera.transform.rotation.eulerAngles.x, maincamera.transform.rotation.eulerAngles.y, vector9.magnitude >= 1f ? -Vector3.Angle(@from, to) * num : Vector3.Angle(@from, to) * num);
                }
                else
                {
                    quaternion2 = Quaternion.Euler(maincamera.transform.rotation.eulerAngles.x, maincamera.transform.rotation.eulerAngles.y, 0f);
                }

                maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, quaternion2, Time.deltaTime * 2f);
            }

            if (state == HERO_STATE.Grab && titanWhoGrabMe != null)
            {
                if (titanWhoGrabMe.GetComponent<TITAN>() != null)
                {
                    baseTransform.position = titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.position;
                    baseTransform.rotation = titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.rotation;
                }
                else if (titanWhoGrabMe.GetComponent<FEMALE_TITAN>() != null)
                {
                    baseTransform.position = titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.position;
                    baseTransform.rotation = titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.rotation;
                }
            }

            if (useGun)
            {
                if (leftArmAim || rightArmAim)
                {
                    var vector10 = gunTarget - baseTransform.position;
                    var current = -Mathf.Atan2(vector10.z, vector10.x) * 57.29578f;
                    var num3 = -Mathf.DeltaAngle(current, baseTransform.rotation.eulerAngles.y - 90f);
                    headMovement();
                    if (!isLeftHandHooked && leftArmAim && num3 < 40f && num3 > -90f)
                    {
                        leftArmAimTo(gunTarget);
                    }

                    if (!isRightHandHooked && rightArmAim && num3 > -40f && num3 < 90f)
                    {
                        rightArmAimTo(gunTarget);
                    }
                }
                else if (!grounded)
                {
                    handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
                    handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                }

                if (isLeftHandHooked && bulletLeft != null)
                {
                    leftArmAimTo(bulletLeft.transform.position);
                }

                if (isRightHandHooked && bulletRight != null)
                {
                    rightArmAimTo(bulletRight.transform.position);
                }
            }

            setHookedPplDirection();
            if (Settings.BodyLean || useGun) bodyLean();
        }
    }

    public void launch(Vector3 des, bool left = true, bool leviMode = false)
    {
        if (isMounted)
        {
            unmounted();
        }

        if (state != HERO_STATE.Attack)
        {
            idle();
        }

        var vector = des - transform.position;
        if (left)
        {
            launchPointLeft = des;
        }
        else
        {
            launchPointRight = des;
        }

        vector.Normalize();
        vector = vector * 20f;
        if (bulletLeft != null && bulletRight != null && bulletLeft.GetComponent<Bullet>().isHooked() && bulletRight.GetComponent<Bullet>().isHooked())
        {
            vector = vector * 0.8f;
        }

        if (animation.IsPlaying("attack5") || animation.IsPlaying("special_petra"))
        {
            leviMode = true;
        }
        else
        {
            leviMode = false;
        }

        if (!leviMode)
        {
            falseAttack();
            idle();

            if (useGun)
            {
                crossFade("AHSS_hook_forward_both", 0.1f);
            }
            else if (Settings.BodyLean)
            {
                if (left && !isRightHandHooked)
                {
                    crossFade("air_hook_l_just", 0.1f);
                }
                else if (!left && !isLeftHandHooked)
                {
                    crossFade("air_hook_r_just", 0.1f);
                }
                else
                {
                    crossFade("dash", 0.1f);
                    animation["dash"].time = 0f;
                }
            }
        }

        if (left)
        {
            isLaunchLeft = true;
        }

        if (!left)
        {
            isLaunchRight = true;
        }

        launchForce = vector;
        if (!leviMode)
        {
            if (vector.y < 30f)
            {
                launchForce += Vector3.up * (30f - vector.y);
            }

            if (des.y >= transform.position.y)
            {
                launchForce += Vector3.up * (des.y - transform.position.y) * 10f;
            }

            rigidbody.AddForce(launchForce);
        }

        facingDirection = Mathf.Atan2(launchForce.x, launchForce.z) * 57.29578f;
        var quaternion = Quaternion.Euler(0f, facingDirection, 0f);
        gameObject.transform.rotation = quaternion;
        rigidbody.rotation = quaternion;
        targetRotation = quaternion;
        if (left)
        {
            launchElapsedTimeL = 0f;
        }
        else
        {
            launchElapsedTimeR = 0f;
        }

        if (leviMode)
        {
            launchElapsedTimeR = -100f;
        }

        if (animation.IsPlaying("special_petra"))
        {
            launchElapsedTimeR = -100f;
            launchElapsedTimeL = -100f;
            if (bulletRight != null)
            {
                bulletRight.GetComponent<Bullet>().disable();
                releaseIfIHookSb();
            }

            if (bulletLeft != null)
            {
                bulletLeft.GetComponent<Bullet>().disable();
                releaseIfIHookSb();
            }
        }

        sparks.enableEmission = false;
    }

    private void launchLeftRope(RaycastHit hit, bool single, int mode)
    {
        if (currentGas != 0f)
        {
            useGas(0f);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                bulletLeft = (GameObject)Instantiate(Resources.Load("hook"));
            }
            else if (photonView.isMine)
            {
                bulletLeft = PhotonNetwork.Instantiate("hook", transform.position, transform.rotation, 0);
            }

            var obj2 = !useGun ? hookRefL1 : hookRefL2;
            var str = !useGun ? "hookRefL1" : "hookRefL2";
            bulletLeft.transform.position = obj2.transform.position;
            var component = bulletLeft.GetComponent<Bullet>();
            var num = !single ? hit.distance <= 50f ? hit.distance * 0.05f : hit.distance * 0.3f : 0f;
            var vector = hit.point - transform.right * num - bulletLeft.transform.position;
            vector.Normalize();
            if (mode == 1)
            {
                component.launch(vector * 3f, rigidbody.velocity, str, true, gameObject, true);
            }
            else
            {
                component.launch(vector * 3f, rigidbody.velocity, str, true, gameObject);
            }

            launchPointLeft = Vector3.zero;
        }
    }

    private void launchRightRope(RaycastHit hit, bool single, int mode)
    {
        if (currentGas != 0f)
        {
            useGas(0f);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                bulletRight = (GameObject)Instantiate(Resources.Load("hook"));
            }
            else if (photonView.isMine)
            {
                bulletRight = PhotonNetwork.Instantiate("hook", transform.position, transform.rotation, 0);
            }

            var obj2 = !useGun ? hookRefR1 : hookRefR2;
            var str = !useGun ? "hookRefR1" : "hookRefR2";
            bulletRight.transform.position = obj2.transform.position;
            var component = bulletRight.GetComponent<Bullet>();
            var num = !single ? hit.distance <= 50f ? hit.distance * 0.05f : hit.distance * 0.3f : 0f;
            var vector = hit.point + transform.right * num - bulletRight.transform.position;
            vector.Normalize();
            if (mode == 1)
            {
                component.launch(vector * 5f, rigidbody.velocity, str, false, gameObject, true);
            }
            else
            {
                component.launch(vector * 3f, rigidbody.velocity, str, false, gameObject);
            }

            launchPointRight = Vector3.zero;
        }
    }

    private void leftArmAimTo(Vector3 target)
    {
        var y = target.x - upperarmL.transform.position.x;
        var num2 = target.y - upperarmL.transform.position.y;
        var x = target.z - upperarmL.transform.position.z;
        var num4 = Mathf.Sqrt(y * y + x * x);
        handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
        forearmL.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        upperarmL.rotation = Quaternion.Euler(0f, 90f + Mathf.Atan2(y, x) * 57.29578f, -Mathf.Atan2(num2, num4) * 57.29578f);
    }

    public void loadskin()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            if (!Settings.WindSetting)
            {
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    if (renderer.name.Contains("speed"))
                    {
                        renderer.enabled = false;
                    }
                }
            }

            switch (Settings.HumanSkinsSetting)
            {
                case 0:
                    return;

                case 1:
                    {
                        if (Settings.HumanSkinsCountSetting <= 0) return;
                        var url = "";
                        for (var i = 0; i < 13; i++)
                        {
                            url += Settings.HumanSkinsList[Settings.HumanSkinsCurrentSetSetting][i] + (i != 12 ? "," : "");
                        }

                        StartCoroutine(loadskinE(-1, url));
                        return;
                    }

                case 2:
                    {
                        if (Settings.HumanSkinsCountSetting <= 0) return;
                        var url = "";
                        for (var i = 0; i < 13; i++)
                        {
                            url += Settings.HumanSkinsList[Settings.HumanSkinsCurrentSetSetting][i] + (i != 12 ? "," : "");
                        }

                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            StartCoroutine(loadskinE(-1, url));
                        }
                        else
                        {
                            var viewID = -1;
                            if (myHorse != null)
                            {
                                viewID = myHorse.GetPhotonView().viewID;
                            }

                            photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, viewID, url);
                        }

                        return;
                    }
            }
        }
    }

    public IEnumerator loadskinE(int horse, string url)
    {
        while (!hasspawn)
        {
            yield return null;
        }

        var mipmap = Settings.MipMappingSetting;

        var unloadAssets = false;

        var skins = url.Split(',');

        var customGas = Settings.CustomGasSetting;

        var customHorse = LevelInfo.getInfo(FengGameManagerMKII.level).horse || RCSettings.horseMode == 1;

        var isLocal = IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine;

        #region Hair

        if (setup.part_hair_1 != null)
        {
            var hairRenderer = setup.part_hair_1.renderer;
            if (skins[1].EndsWith(".jpg") || skins[1].EndsWith(".png") || skins[1].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[1]))
                {
                    var hairLink = new WWW(skins[1]);
                    yield return hairLink;
                    var hairSkin = RCextensions.loadimage(hairLink, mipmap, 200000);
                    hairLink.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[1]))
                    {
                        unloadAssets = true;
                        if (setup.myCostume.hairInfo.id >= 0)
                        {
                            hairRenderer.material = CharacterMaterials.materials[setup.myCostume.hairInfo.texture];
                        }

                        hairRenderer.material.mainTexture = hairSkin;
                        FengGameManagerMKII.linkHash[0].Add(skins[1], hairRenderer.material);
                        hairRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                    }
                    else
                    {
                        hairRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                    }
                }
                else
                {
                    hairRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                }
            }
            else if (skins[1].ToLower() == "transparent")
            {
                hairRenderer.enabled = false;
            }
        }

        #endregion Hair

        #region Cape

        if (setup.part_cape != null)
        {
            var capeRenderer = setup.part_cape.renderer;
            if (skins[7].EndsWith(".jpg") || skins[7].EndsWith(".png") || skins[7].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[7]))
                {
                    var capeLink = new WWW(skins[7]);
                    yield return capeLink;
                    var capeSkin = RCextensions.loadimage(capeLink, mipmap, 200000);
                    capeLink.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[7]))
                    {
                        unloadAssets = true;
                        capeRenderer.material.mainTexture = capeSkin;
                        FengGameManagerMKII.linkHash[0].Add(skins[7], capeRenderer.material);
                        capeRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                    }
                    else
                    {
                        capeRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                    }
                }
                else
                {
                    capeRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                }
            }
            else if (skins[7].ToLower() == "transparent")
            {
                capeRenderer.enabled = false;
            }
        }

        #endregion Cape

        #region Costume

        if (setup.part_chest_3 != null)
        {
            var chestRenderer = setup.part_chest_3.renderer;
            if (skins[6].EndsWith(".jpg") || skins[6].EndsWith(".png") || skins[6].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[6]))
                {
                    var costumeLink = new WWW(skins[6]);
                    yield return costumeLink;
                    var costumeSkin = RCextensions.loadimage(costumeLink, mipmap, 500000);
                    costumeLink.Dispose();
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[6]))
                    {
                        unloadAssets = true;
                        chestRenderer.material.mainTexture = costumeSkin;
                        FengGameManagerMKII.linkHash[1].Add(skins[6], chestRenderer.material);
                        chestRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                    }
                    else
                    {
                        chestRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                    }
                }
                else
                {
                    chestRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                }
            }
            else if (skins[6].ToLower() == "transparent")
            {
                chestRenderer.enabled = false;
            }
        }

        #endregion Costume

        foreach (var otherRenderer in GetComponentsInChildren<Renderer>())
        {
            #region Hair

            if (otherRenderer.name.Contains(FengGameManagerMKII.s[1]))
            {
                if (skins[1].EndsWith(".jpg") || skins[1].EndsWith(".png") || skins[1].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[1]))
                    {
                        var hairLink = new WWW(skins[1]);
                        yield return hairLink;
                        var hairSkin = RCextensions.loadimage(hairLink, mipmap, 200000);
                        hairLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[1]))
                        {
                            unloadAssets = true;
                            if (setup.myCostume.hairInfo.id >= 0)
                            {
                                otherRenderer.material = CharacterMaterials.materials[setup.myCostume.hairInfo.texture];
                            }

                            otherRenderer.material.mainTexture = hairSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[1], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[1]];
                    }
                }
                else if (skins[1].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Hair

            #region Eyes

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[2]))
            {
                if (skins[2].EndsWith(".jpg") || skins[2].EndsWith(".png") || skins[2].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[2]))
                    {
                        var eyesLink = new WWW(skins[2]);
                        yield return eyesLink;
                        var eyesSkin = RCextensions.loadimage(eyesLink, mipmap, 200000);
                        eyesLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[2]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTextureScale = otherRenderer.material.mainTextureScale * 8f;
                            otherRenderer.material.mainTextureOffset = new Vector2(0f, 0f);
                            otherRenderer.material.mainTexture = eyesSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[2], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[2]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[2]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[2]];
                    }
                }
                else if (skins[2].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Eyes

            #region Glasses

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[3]))
            {
                if (skins[3].EndsWith(".jpg") || skins[3].EndsWith(".png") || skins[3].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[3]))
                    {
                        var glassesLink = new WWW(skins[3]);
                        yield return glassesLink;
                        var glassesSkin = RCextensions.loadimage(glassesLink, mipmap, 200000);
                        glassesLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[3]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTextureScale = otherRenderer.material.mainTextureScale * 8f;
                            otherRenderer.material.mainTextureOffset = new Vector2(0f, 0f);
                            otherRenderer.material.mainTexture = glassesSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[3], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[3]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[3]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[3]];
                    }
                }
                else if (skins[3].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Glasses

            #region Face

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[4]))
            {
                if (skins[4].EndsWith(".jpg") || skins[4].EndsWith(".png") || skins[4].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[4]))
                    {
                        var faceLink = new WWW(skins[4]);
                        yield return faceLink;
                        var faceSkin = RCextensions.loadimage(faceLink, mipmap, 200000);
                        faceLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[4]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTextureScale = otherRenderer.material.mainTextureScale * 8f;
                            otherRenderer.material.mainTextureOffset = new Vector2(0f, 0f);
                            otherRenderer.material.mainTexture = faceSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[4], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[4]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[4]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[4]];
                    }
                }
                else if (skins[4].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Face

            #region Skin

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[5]) || otherRenderer.name.Contains(FengGameManagerMKII.s[6]) || otherRenderer.name.Contains(FengGameManagerMKII.s[10]))
            {
                if (skins[5].EndsWith(".jpg") || skins[5].EndsWith(".png") || skins[5].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[5]))
                    {
                        var skinLink = new WWW(skins[5]);
                        yield return skinLink;
                        var skinSkin = RCextensions.loadimage(skinLink, mipmap, 200000);
                        skinLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[5]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = skinSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[5], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[5]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[5]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[5]];
                    }
                }
                else if (skins[5].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Skin

            #region Costume

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[7]) || otherRenderer.name.Contains(FengGameManagerMKII.s[8]) || otherRenderer.name.Contains(FengGameManagerMKII.s[9]) || otherRenderer.name.Contains(FengGameManagerMKII.s[24]))
            {
                if (skins[6].EndsWith(".jpg") || skins[6].EndsWith(".png") || skins[6].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[6]))
                    {
                        var costumeLink = new WWW(skins[6]);
                        yield return costumeLink;
                        var costumeSkin = RCextensions.loadimage(costumeLink, mipmap, 500000);
                        costumeLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[6]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = costumeSkin;
                            FengGameManagerMKII.linkHash[1].Add(skins[6], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[6]];
                    }
                }
                else if (skins[6].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Costume

            #region Logo & Cape

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[11]) || otherRenderer.name.Contains(FengGameManagerMKII.s[12]))
            {
                if (skins[7].EndsWith(".jpg") || skins[7].EndsWith(".png") || skins[7].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[7]))
                    {
                        var capeLink = new WWW(skins[7]);
                        yield return capeLink;
                        var capeSkin = RCextensions.loadimage(capeLink, mipmap, 200000);
                        capeLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[7]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = capeSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[7], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[7]];
                    }
                }
                else if (skins[7].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Logo & Cape

            #region 3DMG Blade & Gun Left

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[15]) || (otherRenderer.name.Contains(FengGameManagerMKII.s[13]) || otherRenderer.name.Contains(FengGameManagerMKII.s[26])) && !otherRenderer.name.Contains("_r"))
            {
                if (skins[8].EndsWith(".jpg") || skins[8].EndsWith(".png") || skins[8].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[8]))
                    {
                        var left3DMGLink = new WWW(skins[8]);
                        yield return left3DMGLink;
                        var left3DMGSkin = RCextensions.loadimage(left3DMGLink, mipmap, 500000);
                        left3DMGLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[8]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = left3DMGSkin;
                            FengGameManagerMKII.linkHash[1].Add(skins[8], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[8]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[8]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[8]];
                    }
                }
                else if (skins[8].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion 3DMG Blade & Gun Left

            #region 3DMG Blade & Gun Right

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[17]) || otherRenderer.name.Contains(FengGameManagerMKII.s[16]) || otherRenderer.name.Contains(FengGameManagerMKII.s[26]) && otherRenderer.name.Contains("_r"))
            {
                if (skins[9].EndsWith(".jpg") || skins[9].EndsWith(".png") || skins[9].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[9]))
                    {
                        var right3DMGLink = new WWW(skins[9]);
                        yield return right3DMGLink;
                        var right3DMGSkin = RCextensions.loadimage(right3DMGLink, mipmap, 500000);
                        right3DMGLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[9]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = right3DMGSkin;
                            FengGameManagerMKII.linkHash[1].Add(skins[9], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[9]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[9]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[9]];
                    }
                }
                else if (skins[9].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion 3DMG Blade & Gun Right

            #region Gas

            else if (otherRenderer.name == FengGameManagerMKII.s[18] && customGas)
            {
                if (skins[10].EndsWith(".jpg") || skins[10].EndsWith(".png") || skins[10].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[10]))
                    {
                        var gasLink = new WWW(skins[10]);
                        yield return gasLink;
                        var gasSkin = RCextensions.loadimage(gasLink, mipmap, 200000);
                        gasLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[10]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = gasSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[10], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[10]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[10]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[10]];
                    }
                }
                else if (skins[10].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Gas

            #region Hoodie

            else if (otherRenderer.name.Contains(FengGameManagerMKII.s[25]))
            {
                if (skins[11].EndsWith(".jpg") || skins[11].EndsWith(".png") || skins[11].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[11]))
                    {
                        var hoodieLink = new WWW(skins[11]);
                        yield return hoodieLink;
                        var hoodieSkin = RCextensions.loadimage(hoodieLink, mipmap, 200000);
                        hoodieLink.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[11]))
                        {
                            unloadAssets = true;
                            otherRenderer.material.mainTexture = hoodieSkin;
                            FengGameManagerMKII.linkHash[0].Add(skins[11], otherRenderer.material);
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[11]];
                        }
                        else
                        {
                            otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[11]];
                        }
                    }
                    else
                    {
                        otherRenderer.material = (Material)FengGameManagerMKII.linkHash[0][skins[11]];
                    }
                }
                else if (skins[11].ToLower() == "transparent")
                {
                    otherRenderer.enabled = false;
                }
            }

            #endregion Hoodie
        }

        #region Horse

        if (customHorse && horse >= 0)
        {
            var horseObj = PhotonView.Find(horse).gameObject;
            if (horseObj != null)
            {
                foreach (var horseRenderer in horseObj.GetComponentsInChildren<Renderer>())
                {
                    if (horseRenderer.name.Contains(FengGameManagerMKII.s[19]))
                    {
                        if (skins[0].EndsWith(".jpg") || skins[0].EndsWith(".png") || skins[0].EndsWith(".jpeg"))
                        {
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[0]))
                            {
                                var horseLink = new WWW(skins[0]);
                                yield return horseLink;
                                var iteratorVariable41 = RCextensions.loadimage(horseLink, mipmap, 500000);
                                horseLink.Dispose();
                                if (!FengGameManagerMKII.linkHash[1].ContainsKey(skins[0]))
                                {
                                    unloadAssets = true;
                                    horseRenderer.material.mainTexture = iteratorVariable41;
                                    FengGameManagerMKII.linkHash[1].Add(skins[0], horseRenderer.material);
                                    horseRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[0]];
                                }
                                else
                                {
                                    horseRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[0]];
                                }
                            }
                            else
                            {
                                horseRenderer.material = (Material)FengGameManagerMKII.linkHash[1][skins[0]];
                            }
                        }
                        else if (skins[0].ToLower() == "transparent")
                        {
                            horseRenderer.enabled = false;
                        }
                    }
                }
            }
        }

        #endregion Horse

        #region Blade Trails

        if (isLocal && (skins[12].EndsWith(".jpg") || skins[12].EndsWith(".png") || skins[12].EndsWith(".jpeg")))
        {
            if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[12]))
            {
                var trailsLink = new WWW(skins[12]);
                yield return trailsLink;
                var trailsSkin = RCextensions.loadimage(trailsLink, mipmap, 200000);
                trailsLink.Dispose();
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(skins[12]))
                {
                    unloadAssets = true;
                    leftbladetrail.MyMaterial.mainTexture = trailsSkin;
                    rightbladetrail.MyMaterial.mainTexture = trailsSkin;
                    FengGameManagerMKII.linkHash[0].Add(skins[12], leftbladetrail.MyMaterial);
                    leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                    rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                    if (Settings.BladeTrailsAppearanceSetting == 0)
                    {
                        leftbladetrail2.MyMaterial = leftbladetrail.MyMaterial;
                        rightbladetrail2.MyMaterial = leftbladetrail.MyMaterial;
                    }
                }
                else
                {
                    if (Settings.BladeTrailsAppearanceSetting == 0)
                    {
                        leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                        rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                    }

                    leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                    rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                }
            }
            else
            {
                if (Settings.BladeTrailsAppearanceSetting == 0)
                {
                    leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                    rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                }

                leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
                rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][skins[12]];
            }
        }

        #endregion Blade Trails

        if (unloadAssets)
        {
            FengGameManagerMKII.FGM.unloadAssets();
        }
    }

    public static Dictionary<int, string> PlayersSkins;

    [RPC]
    public void loadskinRPC(int horse, string url, PhotonMessageInfo info)
    {
        if (PlayersSkins == null) PlayersSkins = new Dictionary<int, string>();
        if (PlayersSkins.Count > 30) PlayersSkins.Remove(PlayersSkins.First().Key);
        if (!(PlayersSkins.ContainsValue(url) || PlayersSkins.ContainsKey(info.sender.ID)))
        {
            PlayersSkins.Add(info.sender.ID, url);
        }
        if (Settings.HumanSkinsSetting != 0)
        {
            StartCoroutine(loadskinE(horse, url));
        }
    }

    public void markDie()
    {
        hasDied = true;
        state = HERO_STATE.Die;
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
    private void net3DMGSMOKE(bool ifON)
    {
        if (smoke_3dmg != null)
        {
            smoke_3dmg.enableEmission = ifON;
        }
    }

    [RPC]
    private void netContinueAnimation()
    {
        foreach (AnimationState animationState in animation)
        {
            if (animationState.speed == 1f)
            {
                return;
            }

            animationState.speed = 1f;
        }

        playAnimation(currentPlayingClipName());
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        currentAnimation = aniName;
        if (animation != null)
        {
            animation.CrossFade(aniName, time);
        }
    }

    [RPC]
    public void netDie(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true, PhotonMessageInfo info = null)
    {
        if (photonView.isMine && info != null && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT)
        {
            if (FengGameManagerMKII.ignoreList.Contains(info.sender.ID))
            {
                photonView.RPC("backToHumanRPC", PhotonTargets.Others);
                return;
            }

            if (!info.sender.isLocal && !info.sender.isMasterClient)
            {
                if (info.sender.customProperties[PhotonPlayerProperty.name] == null || info.sender.customProperties[PhotonPlayerProperty.isTitan] == null)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
                else if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender, ". [Possibly valid].");
                        return;
                    }
                    else
                    {
                        InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                        return;
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
                else if (PhotonView.Find(viewID).owner.ID != info.sender.ID)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
            }
        }

        if (PhotonNetwork.isMasterClient)
        {
            onDeathEvent(viewID, killByTitan);
            var iD = photonView.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }

        if (photonView.isMine)
        {
            var vector = Vector3.up * 5000f;
            if (myBomb != null)
            {
                myBomb.destroyMe();
            }

            if (myCannon != null)
            {
                PhotonNetwork.Destroy(myCannon);
            }

            if (titanForm && eren_titan != null)
            {
                eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }

            if (skillCD != null)
            {
                skillCD.transform.localPosition = vector;
            }
        }

        if (bulletLeft != null)
        {
            bulletLeft.GetComponent<Bullet>().removeMe();
        }

        if (bulletRight != null)
        {
            bulletRight.GetComponent<Bullet>().removeMe();
        }

        meatDie.Play();
        if (!(useGun || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && !photonView.isMine))
        {
            leftbladetrail.Deactivate();
            rightbladetrail.Deactivate();
            leftbladetrail2.Deactivate();
            rightbladetrail2.Deactivate();
        }

        falseAttack();
        breakApart(v, isBite);
        if (photonView.isMine)
        {
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            FengGameManagerMKII.FGM.myRespawnTime = 0f;
        }

        hasDied = true;
        var transform = this.transform.Find("audio_die");
        if (transform != null)
        {
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
        }

        gameObject.GetComponent<SmoothSyncMovement>().disabled = true;
        if (photonView.isMine)
        {
            PhotonNetwork.RemoveRPCs(photonView);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.deaths, RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths]) + 1);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            object[] parameters = { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.FGM.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                var view2 = PhotonView.Find(viewID);
                if (view2 != null)
                {
                    FengGameManagerMKII.FGM.sendKillInfo(killByTitan, "[FFC000][" + info.sender.ID + "][FFFFFF]" + RCextensions.returnStringFromObject(view2.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view2.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view2.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.FGM.sendKillInfo(!(titanName == string.Empty), "[FFC000][" + info.sender.ID + "][FFFFFF]" + titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }
        }

        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(photonView);
        }
    }

    [RPC]
    private void netDie2(int viewID = -1, string titanName = "", PhotonMessageInfo info = null)
    {
        GameObject obj2;
        if (photonView.isMine && info != null && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT)
        {
            if (FengGameManagerMKII.ignoreList.Contains(info.sender.ID))
            {
                photonView.RPC("backToHumanRPC", PhotonTargets.Others);
                return;
            }

            if (!info.sender.isLocal && !info.sender.isMasterClient)
            {
                if (info.sender.customProperties[PhotonPlayerProperty.name] == null || info.sender.customProperties[PhotonPlayerProperty.isTitan] == null)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
                else if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender, ". Possibly valid.");
                        return;
                    }
                    else if (RCSettings.bombMode == 0 && RCSettings.deadlyCannons == 0)
                    {
                        InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                        return;
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
                else if (PhotonView.Find(viewID).owner.ID != info.sender.ID)
                {
                    InRoomChat.SystemMessageLocal("Unusual Kill from", info.sender);
                    return;
                }
            }
        }

        if (photonView.isMine)
        {
            var vector = Vector3.up * 5000f;
            if (myBomb != null)
            {
                myBomb.destroyMe();
            }

            if (myCannon != null)
            {
                PhotonNetwork.Destroy(myCannon);
            }

            PhotonNetwork.RemoveRPCs(photonView);
            if (titanForm && eren_titan != null)
            {
                eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }

            if (skillCD != null)
            {
                skillCD.transform.localPosition = vector;
            }
        }

        meatDie.Play();
        if (bulletLeft != null)
        {
            bulletLeft.GetComponent<Bullet>().removeMe();
        }

        if (bulletRight != null)
        {
            bulletRight.GetComponent<Bullet>().removeMe();
        }

        var transform = this.transform.Find("audio_die");
        transform.parent = null;
        transform.GetComponent<AudioSource>().Play();
        if (photonView.isMine)
        {
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            FengGameManagerMKII.FGM.myRespawnTime = 0f;
        }

        falseAttack();
        hasDied = true;
        gameObject.GetComponent<SmoothSyncMovement>().disabled = true;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            PhotonNetwork.RemoveRPCs(photonView);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.deaths, (int)PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths] + 1);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            if (viewID != -1)
            {
                var view2 = PhotonView.Find(viewID);
                if (view2 != null)
                {
                    FengGameManagerMKII.FGM.sendKillInfo(true, "[FFC000][" + info.sender.ID + "][FFFFFF]" + RCextensions.returnStringFromObject(view2.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view2.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view2.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.FGM.sendKillInfo(true, "[FFC000][" + info.sender.ID + "][FFFFFF]" + titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }

            object[] parameters = { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.FGM.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
        }

        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            obj2 = PhotonNetwork.Instantiate("hitMeat2", this.transform.position, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        else
        {
            obj2 = (GameObject)Instantiate(Resources.Load("hitMeat2"));
        }

        obj2.transform.position = this.transform.position;
        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(photonView);
        }

        if (PhotonNetwork.isMasterClient)
        {
            onDeathEvent(viewID, true);
            var iD = photonView.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }

    public void netDieLocal(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true)
    {
        if (photonView.isMine)
        {
            var vector = Vector3.up * 5000f;
            if (titanForm && eren_titan != null)
            {
                eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }

            if (myBomb != null)
            {
                myBomb.destroyMe();
            }

            if (myCannon != null)
            {
                PhotonNetwork.Destroy(myCannon);
            }

            if (skillCD != null)
            {
                skillCD.transform.localPosition = vector;
            }
        }

        if (bulletLeft != null)
        {
            bulletLeft.GetComponent<Bullet>().removeMe();
        }

        if (bulletRight != null)
        {
            bulletRight.GetComponent<Bullet>().removeMe();
        }

        meatDie.Play();
        if (!(useGun || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && !photonView.isMine))
        {
            leftbladetrail.Deactivate();
            rightbladetrail.Deactivate();
            leftbladetrail2.Deactivate();
            rightbladetrail2.Deactivate();
        }

        falseAttack();
        breakApart(v, isBite);
        if (photonView.isMine)
        {
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);
            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            FengGameManagerMKII.FGM.myRespawnTime = 0f;
        }

        hasDied = true;
        var transform = this.transform.Find("audio_die");
        transform.parent = null;
        transform.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<SmoothSyncMovement>().disabled = true;
        if (photonView.isMine)
        {
            PhotonNetwork.RemoveRPCs(photonView);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            if (titanName != string.Empty)
            {
                propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.deaths, RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths]) + 1);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }

            object[] parameters = { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.FGM.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                var view = PhotonView.Find(viewID);
                if (view != null)
                {
                    FengGameManagerMKII.FGM.sendKillInfo(killByTitan, RCextensions.returnStringFromObject(view.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.FGM.sendKillInfo(!(titanName == string.Empty), titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }
        }

        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(photonView);
        }

        if (PhotonNetwork.isMasterClient)
        {
            onDeathEvent(viewID, killByTitan);
            var iD = photonView.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }

    [RPC]
    private void netGrabbed(int id, bool leftHand)
    {
        titanWhoGrabMeID = id;
        grabbed(PhotonView.Find(id).gameObject, leftHand);
    }

    [RPC]
    private void netlaughAttack()
    {
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (Vector3.Distance(obj2.transform.position, transform.position) < 50f && Vector3.Angle(obj2.transform.forward, transform.position - obj2.transform.position) < 90f && obj2.GetComponent<TITAN>() != null)
            {
                obj2.GetComponent<TITAN>().beLaughAttacked();
            }
        }
    }

    [RPC]
    private void netPauseAnimation()
    {
        foreach (AnimationState animationState in animation)
        {
            animationState.speed = 0f;
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        currentAnimation = aniName;
        if (animation != null)
        {
            animation.Play(aniName);
        }
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        currentAnimation = aniName;
        if (animation != null)
        {
            animation.Play(aniName);
            animation[aniName].normalizedTime = normalizedTime;
        }
    }

    [RPC]
    private void netSetIsGrabbedFalse()
    {
        state = HERO_STATE.Idle;
    }

    [RPC]
    private void netTauntAttack(float tauntTime, float distance = 100f)
    {
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (Vector3.Distance(obj2.transform.position, transform.position) < distance && obj2.GetComponent<TITAN>() != null)
            {
                obj2.GetComponent<TITAN>().beTauntedBy(gameObject, tauntTime);
            }
        }
    }

    [RPC]
    private void netUngrabbed()
    {
        ungrabbed();
        netPlayAnimation(standAnimation);
        falseAttack();
    }

    public void onDeathEvent(int viewID, bool isTitan)
    {
        RCEvent event2;
        string[] strArray;
        if (isTitan)
        {
            if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByTitan"))
            {
                event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByTitan"];
                strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByTitan"];
                if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
                {
                    FengGameManagerMKII.playerVariables[strArray[0]] = photonView.owner;
                }
                else
                {
                    FengGameManagerMKII.playerVariables.Add(strArray[0], photonView.owner);
                }

                if (FengGameManagerMKII.titanVariables.ContainsKey(strArray[1]))
                {
                    FengGameManagerMKII.titanVariables[strArray[1]] = PhotonView.Find(viewID).gameObject.GetComponent<TITAN>();
                }
                else
                {
                    FengGameManagerMKII.titanVariables.Add(strArray[1], PhotonView.Find(viewID).gameObject.GetComponent<TITAN>());
                }

                event2.checkEvent();
            }
        }
        else if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByPlayer"))
        {
            event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByPlayer"];
            strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByPlayer"];
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
            {
                FengGameManagerMKII.playerVariables[strArray[0]] = photonView.owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[0], photonView.owner);
            }

            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[1]))
            {
                FengGameManagerMKII.playerVariables[strArray[1]] = PhotonView.Find(viewID).owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[1], PhotonView.Find(viewID).owner);
            }

            event2.checkEvent();
        }
    }

    private void OnDestroy()
    {
        if (myNetWorkName != null)
        {
            Destroy(myNetWorkName);
        }

        if (gunDummy != null)
        {
            Destroy(gunDummy);
        }

        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            releaseIfIHookSb();
        }

        if (GGM.Caching.GameObjectCache.Find("MultiplayerManager") != null)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().removeHero(this);
        }

        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            var vector = Vector3.up * 5000f;
            cross1.transform.localPosition = vector;
            cross2.transform.localPosition = vector;
            crossL1.transform.localPosition = vector;
            crossL2.transform.localPosition = vector;
            crossR1.transform.localPosition = vector;
            crossR2.transform.localPosition = vector;
            LabelDistance.transform.localPosition = vector;
        }

        if (setup.part_cape != null)
        {
            ClothFactory.DisposeObject(setup.part_cape);
        }

        if (setup.part_hair_1 != null)
        {
            ClothFactory.DisposeObject(setup.part_hair_1);
        }

        if (setup.part_hair_2 != null)
        {
            ClothFactory.DisposeObject(setup.part_hair_2);
        }
    }

    public void pauseAnimation()
    {
        foreach (AnimationState animationState in animation)
        {
            animationState.speed = 0f;
        }

        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
        {
            photonView.RPC("netPauseAnimation", PhotonTargets.Others);
        }
    }

    public void playAnimation(string aniName)
    {
        currentAnimation = aniName;
        animation.Play(aniName);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName };
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        currentAnimation = aniName;
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName, normalizedTime };
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void releaseIfIHookSb()
    {
        if (hookSomeOne && hookTarget != null)
        {
            hookTarget.GetPhotonView().RPC("badGuyReleaseMe", hookTarget.GetPhotonView().owner);
            hookTarget = null;
            hookSomeOne = false;
        }
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if (FengGameManagerMKII.skyMaterial != null && Camera.main.GetComponent<Skybox>().material != FengGameManagerMKII.skyMaterial)
        {
            Camera.main.GetComponent<Skybox>().material = FengGameManagerMKII.skyMaterial;
        }
    }

    public void resetAnimationSpeed()
    {
        foreach (AnimationState animationState in animation)
        {
            animationState.speed = 1f;
        }

        customAnimationSpeed();
    }

    [RPC]
    public void ReturnFromCannon(PhotonMessageInfo info)
    {
        if (info.sender == photonView.owner)
        {
            isCannon = false;
            gameObject.GetComponent<SmoothSyncMovement>().disabled = false;
        }
    }

    private void rightArmAimTo(Vector3 target)
    {
        var y = target.x - upperarmR.transform.position.x;
        var num2 = target.y - upperarmR.transform.position.y;
        var x = target.z - upperarmR.transform.position.z;
        var num4 = Mathf.Sqrt(y * y + x * x);
        handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        forearmR.localRotation = Quaternion.Euler(90f, 0f, 0f);
        upperarmR.rotation = Quaternion.Euler(180f, 90f + Mathf.Atan2(y, x) * 57.29578f, Mathf.Atan2(num2, num4) * 57.29578f);
    }

    [RPC]
    private void RPCHookedByHuman(int hooker, Vector3 hookPosition)
    {
        hookBySomeOne = true;
        badGuy = PhotonView.Find(hooker).gameObject;
        if (Vector3.Distance(hookPosition, transform.position) < 15f)
        {
            launchForce = PhotonView.Find(hooker).gameObject.transform.position - transform.position;
            rigidbody.AddForce(-rigidbody.velocity * 0.9f, ForceMode.VelocityChange);
            var num = Mathf.Pow(launchForce.magnitude, 0.1f);
            if (grounded)
            {
                rigidbody.AddForce(Vector3.up * Mathf.Min(launchForce.magnitude * 0.2f, 10f), ForceMode.Impulse);
            }

            rigidbody.AddForce(launchForce * num * 0.1f, ForceMode.Impulse);
            if (state != HERO_STATE.Grab)
            {
                dashTime = 1f;
                crossFade("dash", 0.05f);
                animation["dash"].time = 0.1f;
                state = HERO_STATE.AirDodge;
                falseAttack();
                facingDirection = Mathf.Atan2(launchForce.x, launchForce.z) * 57.29578f;
                var quaternion = Quaternion.Euler(0f, facingDirection, 0f);
                gameObject.transform.rotation = quaternion;
                rigidbody.rotation = quaternion;
                targetRotation = quaternion;
            }
        }
        else
        {
            hookBySomeOne = false;
            badGuy = null;
            PhotonView.Find(hooker).RPC("hookFail", PhotonView.Find(hooker).owner);
        }
    }

    private void salute()
    {
        state = HERO_STATE.Salute;
        crossFade("salute", 0.1f);
    }

    private void setHookedPplDirection()
    {
        almostSingleHook = false;
        if (isRightHandHooked && isLeftHandHooked)
        {
            if (bulletLeft != null && bulletRight != null)
            {
                var normal = bulletLeft.transform.position - bulletRight.transform.position;
                if (normal.sqrMagnitude < 4f)
                {
                    var vector2 = (bulletLeft.transform.position + bulletRight.transform.position) * 0.5f - transform.position;
                    facingDirection = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
                    if (useGun && state != HERO_STATE.Attack)
                    {
                        var current = -Mathf.Atan2(rigidbody.velocity.z, rigidbody.velocity.x) * 57.29578f;
                        var target = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                        var num3 = -Mathf.DeltaAngle(current, target);
                        facingDirection += num3;
                    }

                    almostSingleHook = true;
                }
                else
                {
                    var to = transform.position - bulletLeft.transform.position;
                    var vector6 = transform.position - bulletRight.transform.position;
                    var vector7 = (bulletLeft.transform.position + bulletRight.transform.position) * 0.5f;
                    var from = transform.position - vector7;
                    if (Vector3.Angle(@from, to) < 30f && Vector3.Angle(@from, vector6) < 30f)
                    {
                        almostSingleHook = true;
                        var vector9 = vector7 - transform.position;
                        facingDirection = Mathf.Atan2(vector9.x, vector9.z) * 57.29578f;
                    }
                    else
                    {
                        almostSingleHook = false;
                        var forward = transform.forward;
                        Vector3.OrthoNormalize(ref normal, ref forward);
                        facingDirection = Mathf.Atan2(forward.x, forward.z) * 57.29578f;
                        var num4 = Mathf.Atan2(to.x, to.z) * 57.29578f;
                        if (Mathf.DeltaAngle(num4, facingDirection) > 0f)
                        {
                            facingDirection += 180f;
                        }
                    }
                }
            }
        }
        else
        {
            almostSingleHook = true;
            var zero = Vector3.zero;
            if (isRightHandHooked && bulletRight != null)
            {
                zero = bulletRight.transform.position - transform.position;
            }
            else if (isLeftHandHooked && bulletLeft != null)
            {
                zero = bulletLeft.transform.position - transform.position;
            }
            else
            {
                return;
            }

            facingDirection = Mathf.Atan2(zero.x, zero.z) * 57.29578f;
            if (state != HERO_STATE.Attack)
            {
                var num6 = -Mathf.Atan2(rigidbody.velocity.z, rigidbody.velocity.x) * 57.29578f;
                var num7 = -Mathf.Atan2(zero.z, zero.x) * 57.29578f;
                var num8 = -Mathf.DeltaAngle(num6, num7);
                if (useGun)
                {
                    facingDirection += num8;
                }
                else
                {
                    var num9 = 0f;
                    if (isLeftHandHooked && num8 < 0f || isRightHandHooked && num8 > 0f)
                    {
                        num9 = -0.1f;
                    }
                    else
                    {
                        num9 = 0.1f;
                    }

                    facingDirection += num8 * num9;
                }
            }
        }
    }

    [RPC]
    public void SetMyCannon(int viewID, PhotonMessageInfo info)
    {
        if (info.sender == photonView.owner)
        {
            var view = PhotonView.Find(viewID);
            if (view != null)
            {
                myCannon = view.gameObject;
                if (myCannon != null)
                {
                    myCannonBase = myCannon.transform;
                    myCannonPlayer = myCannonBase.Find("PlayerPoint");
                    isCannon = true;
                }
            }
        }
    }

    [RPC]
    public void SetMyPhotonCamera(float offset, PhotonMessageInfo info)
    {
        if (photonView.owner == info.sender)
        {
            CameraMultiplier = offset;
            GetComponent<SmoothSyncMovement>().PhotonCamera = true;
            isPhotonCamera = true;
        }
    }

    [RPC]
    private void setMyTeam(int val)
    {
        myTeam = val;
        checkBoxLeft.GetComponent<TriggerColliderWeapon>().myTeam = val;
        checkBoxRight.GetComponent<TriggerColliderWeapon>().myTeam = val;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
        {
            object[] objArray;
            if (RCSettings.friendlyMode > 0)
            {
                if (val != 1)
                {
                    objArray = new object[] { 1 };
                    photonView.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if (RCSettings.pvpMode == 1)
            {
                var num = 0;
                if (photonView.owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    num = RCextensions.returnIntFromObject(photonView.owner.customProperties[PhotonPlayerProperty.RCteam]);
                }

                if (val != num)
                {
                    objArray = new object[] { num };
                    photonView.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if (RCSettings.pvpMode == 2 && val != photonView.owner.ID)
            {
                objArray = new object[] { photonView.owner.ID };
                photonView.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
            }
        }
    }

    public void setSkillHUDPosition()
    {
        skillCD = GGM.Caching.GameObjectCache.Find("skill_cd_" + skillIDHUD);
        if (skillCD != null)
        {
            skillCD.transform.localPosition = GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition;
        }

        if (useGun && RCSettings.bombMode == 0)
        {
            skillCD.transform.localPosition = Vector3.up * 5000f;
        }
    }

    public void setStat()
    {
        skillCDLast = 1.5f;
        skillId = setup.myCostume.stat.skillId;
        if (skillId == "levi")
        {
            skillCDLast = 3.5f;
        }

        customAnimationSpeed();
        if (skillId == "armin")
        {
            skillCDLast = 5f;
        }

        if (skillId == "marco")
        {
            skillCDLast = 10f;
        }

        if (skillId == "jean")
        {
            skillCDLast = 0.001f;
        }

        if (skillId == "eren")
        {
            skillCDLast = 120f;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan || LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.RACING || LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.PVP_CAPTURE || LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.TROST)
                {
                    skillId = "petra";
                    skillCDLast = 1f;
                }
                else
                {
                    var num = 0;
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1 && RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.character]).ToUpper() == "EREN")
                        {
                            num++;
                        }
                    }

                    if (num > 1)
                    {
                        skillId = "petra";
                        skillCDLast = 1f;
                    }
                }
            }
        }

        if (skillId == "sasha")
        {
            skillCDLast = 20f;
        }

        if (skillId == "petra")
        {
            skillCDLast = 3.5f;
        }

        bombInit();
        speed = setup.myCostume.stat.SPD / 10f;
        totalGas = currentGas = setup.myCostume.stat.GAS;
        totalBladeSta = currentBladeSta = setup.myCostume.stat.BLA;
        baseRigidBody.mass = 0.5f - (setup.myCostume.stat.ACL - 100) * 0.001f;
        GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition = new Vector3(0f, -Screen.height * 0.5f + 5f, 0f);
        skillCD = GGM.Caching.GameObjectCache.Find("skill_cd_" + skillIDHUD);
        skillCD.transform.localPosition = GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition;
        GGM.Caching.GameObjectCache.Find("GasUI").transform.localPosition = GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
        {
            GGM.Caching.GameObjectCache.Find("bulletL").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL1").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR1").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL2").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR2").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL3").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR3").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL4").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR4").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL5").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR5").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL6").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR6").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletL7").GetComponent<UISprite>().enabled = false;
            GGM.Caching.GameObjectCache.Find("bulletR7").GetComponent<UISprite>().enabled = false;
        }

        if (setup.myCostume.uniform_type == UNIFORM_TYPE.CasualAHSS)
        {
            standAnimation = "AHSS_stand_gun";
            useGun = true;
            gunDummy = new GameObject();
            gunDummy.name = "gunDummy";
            gunDummy.transform.position = baseTransform.position;
            gunDummy.transform.rotation = baseTransform.rotation;
            myGroup = GROUP.A;
            setTeam(2);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                GGM.Caching.GameObjectCache.Find("bladeCL").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladeCR").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladel1").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("blader1").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladel2").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("blader2").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladel3").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("blader3").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladel4").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("blader4").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bladel5").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("blader5").GetComponent<UISprite>().enabled = false;
                GGM.Caching.GameObjectCache.Find("bulletL").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL1").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR1").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL2").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR2").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL3").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR3").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL4").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR4").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL5").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR5").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL6").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR6").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletL7").GetComponent<UISprite>().enabled = true;
                GGM.Caching.GameObjectCache.Find("bulletR7").GetComponent<UISprite>().enabled = true;
                if (skillId != "bomb")
                {
                    skillCD.transform.localPosition = Vector3.up * 5000f;
                }
            }
        }
        else if (setup.myCostume.sex == SEX.FEMALE)
        {
            standAnimation = "stand";
            setTeam(1);
        }
        else
        {
            standAnimation = "stand_levi";
            setTeam(1);
        }
    }

    public void setTeam(int team)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            object[] parameters = { team };
            photonView.RPC("setMyTeam", PhotonTargets.AllBuffered, parameters);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.team, team);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else
        {
            setMyTeam(team);
        }
    }

    public void shootFlare(int type)
    {
        var flag = false;
        if (type == 1 && flare1CD == 0f)
        {
            flare1CD = flareTotalCD;
            flag = true;
        }

        if (type == 2 && flare2CD == 0f)
        {
            flare2CD = flareTotalCD;
            flag = true;
        }

        if (type == 3 && flare3CD == 0f)
        {
            flare3CD = flareTotalCD;
            flag = true;
        }

        if (flag)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                var obj2 = (GameObject)Instantiate(Resources.Load("FX/flareBullet" + type), transform.position, transform.rotation);
                obj2.GetComponent<FlareMovement>().dontShowHint();
                Destroy(obj2, 25f);
            }
            else
            {
                PhotonNetwork.Instantiate("FX/flareBullet" + type, transform.position, transform.rotation, 0).GetComponent<FlareMovement>().dontShowHint();
            }
        }
    }

    private void showAimUI()
    {
        Vector3 vector;
        if (Screen.showCursor)
        {
            var obj2 = cross1;
            var obj3 = cross2;
            var obj4 = crossL1;
            var obj5 = crossL2;
            var obj6 = crossR1;
            var obj7 = crossR2;
            var labelDistance = LabelDistance;
            vector = Vector3.up * 10000f;
            obj7.transform.localPosition = vector;
            obj6.transform.localPosition = vector;
            obj5.transform.localPosition = vector;
            obj4.transform.localPosition = vector;
            labelDistance.transform.localPosition = vector;
            obj3.transform.localPosition = vector;
            obj2.transform.localPosition = vector;
        }
        else
        {
            checkTitan();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if (Physics.Raycast(ray, out var hit, 1E+07f, mask3.value))
            {
                RaycastHit hit2;
                var obj9 = cross1;
                var obj10 = cross2;
                obj9.transform.localPosition = Input.mousePosition;
                var transform = obj9.transform;
                transform.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                obj10.transform.localPosition = obj9.transform.localPosition;
                vector = hit.point - baseTransform.position;
                var magnitude = vector.magnitude;
                var obj11 = LabelDistance;
                var str = magnitude <= Settings.DrawDistanceSetting ? ((int)magnitude).ToString() : "???";
                var hitDamage = currentSpeed;
                var singleShotDamage = currentSpeed * 0.4f;
                var doubleShotDamage = currentSpeed * 0.6f;
                switch (Settings.SpeedometerSetting)
                {
                    case 1:
                        str = str + "\n" + currentSpeed.ToString("F1") + " U/S";
                        break;

                    case 2:
                        if (!useGun)
                        {
                            str = str + "\n" + (hitDamage / 100).ToString("F1") + "K";
                        }
                        else
                        {
                            switch (Settings.SpeedometerAHSSSetting)
                            {
                                case 0:
                                    str = str + "\n" + (singleShotDamage / 100).ToString("F1") + "K\n" + (doubleShotDamage / 100f).ToString("F1") + "K";
                                    break;

                                case 1:
                                    str = str + "\n" + (singleShotDamage / 100).ToString("F1") + "K";
                                    break;

                                case 2:
                                    str = str + "\n" + (doubleShotDamage / 100f).ToString("F1") + "K";
                                    break;
                            }
                        }

                        break;
                }

                if (Settings.LegacyLabelsSetting)
                {
                    obj11.GetComponent<UILabel>().text = str;
                }
                else
                {
                    Labels.Crosshair = str;
                }

                if (magnitude > 120f)
                {
                    var transform11 = obj9.transform;
                    transform11.localPosition += Vector3.up * 10000f;
                    obj11.transform.localPosition = obj10.transform.localPosition;
                }
                else
                {
                    var transform12 = obj10.transform;
                    transform12.localPosition += Vector3.up * 10000f;
                    obj11.transform.localPosition = obj9.transform.localPosition;
                }

                var transform13 = obj11.transform;
                transform13.localPosition -= new Vector3(0f, 15f, 0f);
                var vector2 = new Vector3(0f, 0.4f, 0f);
                vector2 -= baseTransform.right * 0.3f;
                var vector3 = new Vector3(0f, 0.4f, 0f);
                vector3 += baseTransform.right * 0.3f;
                var num4 = hit.distance <= 50f ? hit.distance * 0.05f : hit.distance * 0.3f;
                var vector4 = hit.point - baseTransform.right * num4 - (baseTransform.position + vector2);
                var vector5 = hit.point + baseTransform.right * num4 - (baseTransform.position + vector3);
                vector4.Normalize();
                vector5.Normalize();
                vector4 = vector4 * 1000000f;
                vector5 = vector5 * 1000000f;
                if (Physics.Linecast(baseTransform.position + vector2, baseTransform.position + vector2 + vector4, out hit2, mask3.value))
                {
                    var obj12 = crossL1;
                    obj12.transform.localPosition = currentCamera.WorldToScreenPoint(hit2.point);
                    var transform14 = obj12.transform;
                    transform14.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                    obj12.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(obj12.transform.localPosition.y - (Input.mousePosition.y - Screen.height * 0.5f), obj12.transform.localPosition.x - (Input.mousePosition.x - Screen.width * 0.5f)) * 57.29578f + 180f);
                    var obj13 = crossL2;
                    obj13.transform.localPosition = obj12.transform.localPosition;
                    obj13.transform.localRotation = obj12.transform.localRotation;
                    if (hit2.distance > 120f)
                    {
                        var transform15 = obj12.transform;
                        transform15.localPosition += Vector3.up * 10000f;
                    }
                    else
                    {
                        var transform16 = obj13.transform;
                        transform16.localPosition += Vector3.up * 10000f;
                    }
                }

                if (Physics.Linecast(baseTransform.position + vector3, baseTransform.position + vector3 + vector5, out hit2, mask3.value))
                {
                    var obj14 = crossR1;
                    obj14.transform.localPosition = currentCamera.WorldToScreenPoint(hit2.point);
                    var transform17 = obj14.transform;
                    transform17.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                    obj14.transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(obj14.transform.localPosition.y - (Input.mousePosition.y - Screen.height * 0.5f), obj14.transform.localPosition.x - (Input.mousePosition.x - Screen.width * 0.5f)) * 57.29578f);
                    var obj15 = crossR2;
                    obj15.transform.localPosition = obj14.transform.localPosition;
                    obj15.transform.localRotation = obj14.transform.localRotation;
                    if (hit2.distance > 120f)
                    {
                        var transform18 = obj14.transform;
                        transform18.localPosition += Vector3.up * 10000f;
                    }
                    else
                    {
                        var transform19 = obj15.transform;
                        transform19.localPosition += Vector3.up * 10000f;
                    }
                }
            }
        }
    }

    private void showFlareCD()
    {
        if (cachedSprites["UIflare1"] != null)
        {
            cachedSprites["UIflare1"].fillAmount = (flareTotalCD - flare1CD) / flareTotalCD;
            cachedSprites["UIflare2"].fillAmount = (flareTotalCD - flare2CD) / flareTotalCD;
            cachedSprites["UIflare3"].fillAmount = (flareTotalCD - flare3CD) / flareTotalCD;
        }
    }

    private void showGas()
    {
        var num = currentGas / totalGas;
        var num2 = currentBladeSta / totalBladeSta;
        cachedSprites["gasL1"].fillAmount = currentGas / totalGas;
        cachedSprites["gasR1"].fillAmount = currentGas / totalGas;
        if (!useGun)
        {
            cachedSprites["bladeCL"].fillAmount = currentBladeSta / totalBladeSta;
            cachedSprites["bladeCR"].fillAmount = currentBladeSta / totalBladeSta;
            if (num <= 0f)
            {
                cachedSprites["gasL"].color = Color.red;
                cachedSprites["gasR"].color = Color.red;
            }
            else if (num < 0.3f)
            {
                cachedSprites["gasL"].color = Color.yellow;
                cachedSprites["gasR"].color = Color.yellow;
            }
            else
            {
                cachedSprites["gasL"].color = Color.white;
                cachedSprites["gasR"].color = Color.white;
            }

            if (num2 <= 0f)
            {
                cachedSprites["bladel1"].color = Color.red;
                cachedSprites["blader1"].color = Color.red;
            }
            else if (num2 < 0.3f)
            {
                cachedSprites["bladel1"].color = Color.yellow;
                cachedSprites["blader1"].color = Color.yellow;
            }
            else
            {
                cachedSprites["bladel1"].color = Color.white;
                cachedSprites["blader1"].color = Color.white;
            }

            if (currentBladeNum <= 4)
            {
                cachedSprites["bladel5"].enabled = false;
                cachedSprites["blader5"].enabled = false;
            }
            else
            {
                cachedSprites["bladel5"].enabled = true;
                cachedSprites["blader5"].enabled = true;
            }

            if (currentBladeNum <= 3)
            {
                cachedSprites["bladel4"].enabled = false;
                cachedSprites["blader4"].enabled = false;
            }
            else
            {
                cachedSprites["bladel4"].enabled = true;
                cachedSprites["blader4"].enabled = true;
            }

            if (currentBladeNum <= 2)
            {
                cachedSprites["bladel3"].enabled = false;
                cachedSprites["blader3"].enabled = false;
            }
            else
            {
                cachedSprites["bladel3"].enabled = true;
                cachedSprites["blader3"].enabled = true;
            }

            if (currentBladeNum <= 1)
            {
                cachedSprites["bladel2"].enabled = false;
                cachedSprites["blader2"].enabled = false;
            }
            else
            {
                cachedSprites["bladel2"].enabled = true;
                cachedSprites["blader2"].enabled = true;
            }

            if (currentBladeNum <= 0)
            {
                cachedSprites["bladel1"].enabled = false;
                cachedSprites["blader1"].enabled = false;
            }
            else
            {
                cachedSprites["bladel1"].enabled = true;
                cachedSprites["blader1"].enabled = true;
            }
        }
        else
        {
            if (leftGunHasBullet)
            {
                cachedSprites["bulletL"].enabled = true;
            }
            else
            {
                cachedSprites["bulletL"].enabled = false;
            }

            if (rightGunHasBullet)
            {
                cachedSprites["bulletR"].enabled = true;
            }
            else
            {
                cachedSprites["bulletR"].enabled = false;
            }
        }
    }

    [RPC]
    private void showHitDamage()
    {
        var target = GGM.Caching.GameObjectCache.Find("LabelScore");
        if (target != null)
        {
            speed = Mathf.Max(10f, speed);
            target.GetComponent<UILabel>().text = speed.ToString();
            target.transform.localScale = Vector3.zero;
            speed = (int)(speed * 0.1f);
            speed = Mathf.Clamp(speed, 40f, 150f);
            iTween.Stop(target);
            object[] args = { "x", speed, "y", speed, "z", speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(target, iTween.Hash(args));
            object[] objArray2 = { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(target, iTween.Hash(objArray2));
        }
    }

    private void showSkillCD()
    {
        if (skillCD != null)
        {
            skillCD.GetComponent<UISprite>().fillAmount = (skillCDLast - skillCDDuration) / skillCDLast;
        }
    }

    [RPC]
    public void SpawnCannonRPC(string settings, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient && photonView.isMine && myCannon == null)
        {
            if (myHorse != null && isMounted)
            {
                getOffHorse();
            }

            idle();
            if (bulletLeft != null)
            {
                bulletLeft.GetComponent<Bullet>().removeMe();
            }

            if (bulletRight != null)
            {
                bulletRight.GetComponent<Bullet>().removeMe();
            }

            if (smoke_3dmg.enableEmission && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && photonView.isMine)
            {
                object[] parameters = { false };
                photonView.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
            }

            smoke_3dmg.enableEmission = false;
            rigidbody.velocity = Vector3.zero;
            var strArray = settings.Split(',');
            if (strArray.Length > 15)
            {
                myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[16]), Convert.ToSingle(strArray[17]), Convert.ToSingle(strArray[18])), 0);
            }
            else
            {
                myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0);
            }

            myCannonBase = myCannon.transform;
            myCannonPlayer = myCannon.transform.Find("PlayerPoint");
            isCannon = true;
            myCannon.GetComponent<Cannon>().myHero = this;
            myCannonRegion = null;
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(myCannon.transform.Find("Barrel").Find("FiringPoint").gameObject);
            Camera.main.fieldOfView = 55f;
            photonView.RPC("SetMyCannon", PhotonTargets.OthersBuffered, myCannon.GetPhotonView().viewID);
            skillCDLastCannon = skillCDLast;
            skillCDLast = 3.5f;
            skillCDDuration = 3.5f;
        }
    }

    private void Start()
    {
        FengGameManagerMKII.FGM.addHero(this);
        if ((LevelInfo.getInfo(FengGameManagerMKII.level).horse || RCSettings.horseMode == 1) && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
        {
            myHorse = PhotonNetwork.Instantiate("horse", baseTransform.position + Vector3.up * 5f, baseTransform.rotation, 0);
            myHorse.GetComponent<Horse>().myHero = gameObject;
            myHorse.GetComponent<TITAN_CONTROLLER>().isHorse = true;
        }

        sparks = baseTransform.Find("slideSparks").GetComponent<ParticleSystem>();
        smoke_3dmg = baseTransform.Find("3dmg_smoke").GetComponent<ParticleSystem>();
        baseTransform.localScale = new Vector3(myScale, myScale, myScale);
        facingDirection = baseTransform.rotation.eulerAngles.y;
        targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
        smoke_3dmg.enableEmission = false;
        sparks.enableEmission = false;
        speedFXPS = speedFX1.GetComponent<ParticleSystem>();
        speedFXPS.enableEmission = false;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER)
        {
            if (Minimap.instance != null)
            {
                Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.green, false, true);
            }
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                var iD = photonView.owner.ID;
                if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                {
                    FengGameManagerMKII.heroHash[iD] = this;
                }
                else
                {
                    FengGameManagerMKII.heroHash.Add(iD, this);
                }
            }

            var obj2 = GGM.Caching.GameObjectCache.Find("UI_IN_GAME");
            myNetWorkName = (GameObject)Instantiate(Resources.Load("UI/LabelNameOverHead"));
            myNetWorkName.name = "LabelNameOverHead";
            myNetWorkName.transform.parent = obj2.GetComponent<UIReferArray>().panels[0].transform;
            myNetWorkName.transform.localScale = new Vector3(14f, 14f, 14f);
            myNetWorkName.GetComponent<UILabel>().text = string.Empty;
            if (photonView.isMine)
            {
                if (Minimap.instance != null)
                {
                    Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.green, false, true);
                }

                GetComponent<SmoothSyncMovement>().PhotonCamera = true;
                photonView.RPC("SetMyPhotonCamera", PhotonTargets.OthersBuffered, PlayerPrefs.GetFloat("cameraDistance") + 0.3f);
            }
            else
            {
                var flag2 = false;
                if (photonView.owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    switch (RCextensions.returnIntFromObject(photonView.owner.customProperties[PhotonPlayerProperty.RCteam]))
                    {
                        case 1:
                            flag2 = true;
                            if (Minimap.instance != null)
                            {
                                Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.cyan, false, true);
                            }

                            break;

                        case 2:
                            flag2 = true;
                            if (Minimap.instance != null)
                            {
                                Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.magenta, false, true);
                            }

                            break;
                    }
                }

                if (RCextensions.returnIntFromObject(photonView.owner.customProperties[PhotonPlayerProperty.team]) == 2)
                {
                    myNetWorkName.GetComponent<UILabel>().text = "[FF0000]AHSS\n[FFFFFF]";
                    if (!flag2 && Minimap.instance != null)
                    {
                        Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.red, false, true);
                    }
                }
                else if (!flag2 && Minimap.instance != null)
                {
                    Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.blue, false, true);
                }
            }

            var str = RCextensions.returnStringFromObject(photonView.owner.customProperties[PhotonPlayerProperty.guildName]);
            if (str != string.Empty)
            {
                var component = myNetWorkName.GetComponent<UILabel>();
                var text = component.text;
                string[] strArray2 = { text, "[FFFF00]", str, "\n[FFFFFF]", RCextensions.returnStringFromObject(photonView.owner.customProperties[PhotonPlayerProperty.name]) };
                component.text = string.Concat(strArray2);
            }
            else
            {
                var label2 = myNetWorkName.GetComponent<UILabel>();
                label2.text = label2.text + RCextensions.returnStringFromObject(photonView.owner.customProperties[PhotonPlayerProperty.name]);
            }
        }

        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && !photonView.isMine)
        {
            gameObject.layer = LayerMask.NameToLayer("NetworkObject");
            if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
            {
                var obj3 = (GameObject)Instantiate(Resources.Load("flashlight"));
                obj3.transform.parent = baseTransform;
                obj3.transform.position = baseTransform.position + Vector3.up;
                obj3.transform.rotation = Quaternion.Euler(353f, 0f, 0f);
            }

            setup.init();
            setup.myCostume = new HeroCostume();
            setup.myCostume = CostumeConeveter.PhotonDataToHeroCostume2(photonView.owner);
            setup.setCharacterComponent();
            Destroy(checkBoxLeft);
            Destroy(checkBoxRight);
            Destroy(leftbladetrail);
            Destroy(rightbladetrail);
            Destroy(leftbladetrail2);
            Destroy(rightbladetrail2);
            hasspawn = true;
        }
        else
        {
            currentCamera = GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<Camera>();
            inputManager = GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>();
            loadskin();
            hasspawn = true;
            StartCoroutine(reloadSky());
        }

        bombImmune = false;
        if (RCSettings.bombMode == 1)
        {
            bombImmune = true;
            StartCoroutine(stopImmunity());
        }
    }

    public IEnumerator stopImmunity()
    {
        yield return new WaitForSeconds(5f);
        bombImmune = false;
    }

    private void suicide()
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            netDieLocal(rigidbody.velocity * 50f, false, -1, string.Empty);
            FengGameManagerMKII.FGM.needChooseSide = true;
            FengGameManagerMKII.FGM.justSuicide = true;
        }
    }

    private void throwBlades()
    {
        var transform = setup.part_blade_l.transform;
        var transform2 = setup.part_blade_r.transform;
        var obj2 = (GameObject)Instantiate(Resources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
        var obj3 = (GameObject)Instantiate(Resources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
        obj2.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        obj3.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
        var force = this.transform.forward + this.transform.up * 2f - this.transform.right;
        obj2.rigidbody.AddForce(force, ForceMode.Impulse);
        var vector2 = this.transform.forward + this.transform.up * 2f + this.transform.right;
        obj3.rigidbody.AddForce(vector2, ForceMode.Impulse);
        var torque = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        torque.Normalize();
        obj2.rigidbody.AddTorque(torque);
        torque = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        torque.Normalize();
        obj3.rigidbody.AddTorque(torque);
        setup.part_blade_l.SetActive(false);
        setup.part_blade_r.SetActive(false);
        currentBladeNum--;
        if (currentBladeNum == 0)
        {
            currentBladeSta = 0f;
        }

        if (state == HERO_STATE.Attack)
        {
            falseAttack();
        }
    }

    public void ungrabbed()
    {
        facingDirection = 0f;
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.parent = null;
        GetComponent<CapsuleCollider>().isTrigger = false;
        state = HERO_STATE.Idle;
    }

    private void unmounted()
    {
        myHorse.GetComponent<Horse>().unmounted();
        isMounted = false;
    }

    public void update()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing)
        {
            if (invincible > 0f)
            {
                invincible -= Time.deltaTime;
            }

            if (!hasDied)
            {
                if (titanForm && eren_titan != null)
                {
                    baseTransform.position = eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
                    gameObject.GetComponent<SmoothSyncMovement>().disabled = true;
                }
                else if (isCannon && myCannon != null)
                {
                    updateCannon();
                    gameObject.GetComponent<SmoothSyncMovement>().disabled = true;
                }

                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
                {
                    if (myCannonRegion != null)
                    {
                        FengGameManagerMKII.FGM.ShowHUDInfoCenter("Press 'Cannon Mount' key to use Cannon.");
                        if (FengGameManagerMKII.inputRC.isInputCannonDown(InputCodeRC.cannonMount))
                        {
                            myCannonRegion.photonView.RPC("RequestControlRPC", PhotonTargets.MasterClient, photonView.viewID);
                        }
                    }

                    if (state == HERO_STATE.Grab && !useGun)
                    {
                        if (skillId == "jean")
                        {
                            if (state != HERO_STATE.Attack && (inputManager.isInputDown[InputCode.attack0] || inputManager.isInputDown[InputCode.attack1]) && escapeTimes > 0 && !baseAnimation.IsPlaying("grabbed_jean"))
                            {
                                playAnimation("grabbed_jean");
                                baseAnimation["grabbed_jean"].time = 0f;
                                escapeTimes--;
                            }

                            if (baseAnimation.IsPlaying("grabbed_jean") && baseAnimation["grabbed_jean"].normalizedTime > 0.64f && titanWhoGrabMe.GetComponent<TITAN>() != null)
                            {
                                ungrabbed();
                                baseRigidBody.velocity = Vector3.up * 30f;
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                }
                                else
                                {
                                    photonView.RPC("netSetIsGrabbedFalse", PhotonTargets.All);
                                    if (PhotonNetwork.isMasterClient)
                                    {
                                        titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                    }
                                    else
                                    {
                                        PhotonView.Find(titanWhoGrabMeID).RPC("grabbedTargetEscape", PhotonTargets.MasterClient);
                                    }
                                }
                            }
                        }
                        else if (skillId == "eren")
                        {
                            showSkillCD();
                            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE && !IN_GAME_MAIN_CAMERA.isPausing)
                            {
                                calcSkillCD();
                                calcFlareCD();
                            }

                            if (inputManager.isInputDown[InputCode.attack1])
                            {
                                var flag2 = false;
                                if (skillCDDuration > 0f || flag2)
                                {
                                    flag2 = true;
                                }
                                else
                                {
                                    skillCDDuration = skillCDLast;
                                    if (skillId == "eren" && titanWhoGrabMe.GetComponent<TITAN>() != null)
                                    {
                                        ungrabbed();
                                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                        {
                                            titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                        }
                                        else
                                        {
                                            photonView.RPC("netSetIsGrabbedFalse", PhotonTargets.All);
                                            if (PhotonNetwork.isMasterClient)
                                            {
                                                titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                            }
                                            else
                                            {
                                                PhotonView.Find(titanWhoGrabMeID).photonView.RPC("grabbedTargetEscape", PhotonTargets.MasterClient);
                                            }
                                        }

                                        erenTransform();
                                    }
                                }
                            }
                        }
                    }
                    else if (!titanForm && !isCannon)
                    {
                        bufferUpdate();
                        updateExt();
                        if (!grounded && state != HERO_STATE.AirDodge)
                        {
                            if (Settings.DashSetting)
                            {
                                checkDashRebind();
                            }
                            else
                            {
                                checkDashDoubleTap();
                            }

                            if (dashD)
                            {
                                dashD = false;
                                dash(0f, -1f);
                                return;
                            }

                            if (dashU)
                            {
                                dashU = false;
                                dash(0f, 1f);
                                return;
                            }

                            if (dashL)
                            {
                                dashL = false;
                                dash(-1f, 0f);
                                return;
                            }

                            if (dashR)
                            {
                                dashR = false;
                                dash(1f, 0f);
                                return;
                            }
                        }

                        if (grounded && (state == HERO_STATE.Idle || state == HERO_STATE.Slide))
                        {
                            if (!(!inputManager.isInputDown[InputCode.jump] || baseAnimation.IsPlaying("jump") || baseAnimation.IsPlaying("horse_geton")))
                            {
                                idle();
                                crossFade("jump", 0.1f);
                                sparks.enableEmission = false;
                            }

                            if (FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount) && !baseAnimation.IsPlaying("jump") && !baseAnimation.IsPlaying("horse_geton") && myHorse != null && !isMounted && Vector3.Distance(myHorse.transform.position, transform.position) < 15f)
                            {
                                getOnHorse();
                            }

                            if (!(!inputManager.isInputDown[InputCode.dodge] || baseAnimation.IsPlaying("jump") || baseAnimation.IsPlaying("horse_geton")))
                            {
                                dodge2();
                                return;
                            }
                        }

                        if (state == HERO_STATE.Idle)
                        {
                            if (inputManager.isInputDown[InputCode.flare1])
                            {
                                shootFlare(1);
                            }

                            if (inputManager.isInputDown[InputCode.flare2])
                            {
                                shootFlare(2);
                            }

                            if (inputManager.isInputDown[InputCode.flare3])
                            {
                                shootFlare(3);
                            }

                            if (inputManager.isInputDown[InputCode.restart])
                            {
                                suicide();
                            }

                            if (myHorse != null && isMounted && FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount))
                            {
                                getOffHorse();
                            }

                            if ((animation.IsPlaying(standAnimation) || !grounded) && inputManager.isInputDown[InputCode.reload] && (!useGun || RCSettings.ahssReload != 1 || grounded))
                            {
                                changeBlade();
                                return;
                            }

                            if (baseAnimation.IsPlaying(standAnimation) && inputManager.isInputDown[InputCode.salute])
                            {
                                salute();
                                return;
                            }

                            if (!isMounted && (inputManager.isInputDown[InputCode.attack0] || inputManager.isInputDown[InputCode.attack1]) && !useGun)
                            {
                                var flag3 = false;
                                if (inputManager.isInputDown[InputCode.attack1])
                                {
                                    if (skillCDDuration > 0f || flag3)
                                    {
                                        flag3 = true;
                                    }
                                    else
                                    {
                                        skillCDDuration = skillCDLast;
                                        if (skillId == "eren")
                                        {
                                            erenTransform();
                                            return;
                                        }

                                        if (skillId == "marco")
                                        {
                                            if (IsGrounded())
                                            {
                                                attackAnimation = Random.Range(0, 2) != 0 ? "special_marco_1" : "special_marco_0";
                                                playAnimation(attackAnimation);
                                            }
                                            else
                                            {
                                                flag3 = true;
                                                skillCDDuration = 0f;
                                            }
                                        }
                                        else if (skillId == "armin")
                                        {
                                            if (IsGrounded())
                                            {
                                                attackAnimation = "special_armin";
                                                playAnimation("special_armin");
                                            }
                                            else
                                            {
                                                flag3 = true;
                                                skillCDDuration = 0f;
                                            }
                                        }
                                        else if (skillId == "sasha")
                                        {
                                            if (IsGrounded())
                                            {
                                                attackAnimation = "special_sasha";
                                                playAnimation("special_sasha");
                                                currentBuff = BUFF.SpeedUp;
                                                buffTime = 10f;
                                            }
                                            else
                                            {
                                                flag3 = true;
                                                skillCDDuration = 0f;
                                            }
                                        }
                                        else if (skillId == "mikasa")
                                        {
                                            attackAnimation = "attack3_1";
                                            playAnimation("attack3_1");
                                            baseRigidBody.velocity = Vector3.up * 10f;
                                        }
                                        else if (skillId == "levi")
                                        {
                                            RaycastHit hit;
                                            attackAnimation = "attack5";
                                            playAnimation("attack5");
                                            baseRigidBody.velocity += Vector3.up * 5f;
                                            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                            LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
                                            LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
                                            LayerMask mask3 = mask2 | mask;
                                            if (Physics.Raycast(ray, out hit, 1E+07f, mask3.value))
                                            {
                                                if (bulletRight != null)
                                                {
                                                    bulletRight.GetComponent<Bullet>().disable();
                                                    releaseIfIHookSb();
                                                }

                                                dashDirection = hit.point - baseTransform.position;
                                                launchRightRope(hit, true, 1);
                                                rope.Play();
                                            }

                                            facingDirection = Mathf.Atan2(dashDirection.x, dashDirection.z) * 57.29578f;
                                            targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                                            attackLoop = 3;
                                        }
                                        else if (skillId == "petra")
                                        {
                                            RaycastHit hit2;
                                            attackAnimation = "special_petra";
                                            playAnimation("special_petra");
                                            baseRigidBody.velocity += Vector3.up * 5f;
                                            var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                            LayerMask mask4 = 1 << LayerMask.NameToLayer("Ground");
                                            LayerMask mask5 = 1 << LayerMask.NameToLayer("EnemyBox");
                                            LayerMask mask6 = mask5 | mask4;
                                            if (Physics.Raycast(ray2, out hit2, 1E+07f, mask6.value))
                                            {
                                                if (bulletRight != null)
                                                {
                                                    bulletRight.GetComponent<Bullet>().disable();
                                                    releaseIfIHookSb();
                                                }

                                                if (bulletLeft != null)
                                                {
                                                    bulletLeft.GetComponent<Bullet>().disable();
                                                    releaseIfIHookSb();
                                                }

                                                dashDirection = hit2.point - baseTransform.position;
                                                launchLeftRope(hit2, true, 0);
                                                launchRightRope(hit2, true, 0);
                                                rope.Play();
                                            }

                                            facingDirection = Mathf.Atan2(dashDirection.x, dashDirection.z) * 57.29578f;
                                            targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                                            attackLoop = 3;
                                        }
                                        else
                                        {
                                            if (needLean)
                                            {
                                                if (leanLeft)
                                                {
                                                    attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_l1" : "attack1_hook_l2";
                                                }
                                                else
                                                {
                                                    attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_r1" : "attack1_hook_r2";
                                                }
                                            }
                                            else
                                            {
                                                attackAnimation = "attack1";
                                            }

                                            playAnimation(attackAnimation);
                                        }
                                    }
                                }
                                else if (inputManager.isInputDown[InputCode.attack0])
                                {
                                    if (needLean)
                                    {
                                        if (inputManager.isInput[InputCode.left])
                                        {
                                            attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_l1" : "attack1_hook_l2";
                                        }
                                        else if (inputManager.isInput[InputCode.right])
                                        {
                                            attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_r1" : "attack1_hook_r2";
                                        }
                                        else if (leanLeft)
                                        {
                                            attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_l1" : "attack1_hook_l2";
                                        }
                                        else
                                        {
                                            attackAnimation = Random.Range(0, 100) >= 50 ? "attack1_hook_r1" : "attack1_hook_r2";
                                        }
                                    }
                                    else if (inputManager.isInput[InputCode.left])
                                    {
                                        attackAnimation = "attack2";
                                    }
                                    else if (inputManager.isInput[InputCode.right])
                                    {
                                        attackAnimation = "attack1";
                                    }
                                    else if (lastHook != null)
                                    {
                                        if (lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck") != null)
                                        {
                                            attackAccordingToTarget(lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck"));
                                        }
                                        else
                                        {
                                            flag3 = true;
                                        }
                                    }
                                    else if (bulletLeft != null && bulletLeft.transform.parent != null)
                                    {
                                        var a = bulletLeft.transform.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                        if (a != null)
                                        {
                                            attackAccordingToTarget(a);
                                        }
                                        else
                                        {
                                            attackAccordingToMouse();
                                        }
                                    }
                                    else if (bulletRight != null && bulletRight.transform.parent != null)
                                    {
                                        var transform2 = bulletRight.transform.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                        if (transform2 != null)
                                        {
                                            attackAccordingToTarget(transform2);
                                        }
                                        else
                                        {
                                            attackAccordingToMouse();
                                        }
                                    }
                                    else
                                    {
                                        var obj2 = findNearestTitan();
                                        if (obj2 != null)
                                        {
                                            var transform3 = obj2.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                            if (transform3 != null)
                                            {
                                                attackAccordingToTarget(transform3);
                                            }
                                            else
                                            {
                                                attackAccordingToMouse();
                                            }
                                        }
                                        else
                                        {
                                            attackAccordingToMouse();
                                        }
                                    }
                                }

                                if (!flag3)
                                {
                                    checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                    checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                    if (grounded)
                                    {
                                        baseRigidBody.AddForce(gameObject.transform.forward * 200f);
                                    }

                                    playAnimation(attackAnimation);
                                    baseAnimation[attackAnimation].time = 0f;
                                    buttonAttackRelease = false;
                                    state = HERO_STATE.Attack;
                                    if (grounded || attackAnimation == "attack3_1" || attackAnimation == "attack5" || attackAnimation == "special_petra")
                                    {
                                        attackReleased = true;
                                        buttonAttackRelease = true;
                                    }
                                    else
                                    {
                                        attackReleased = false;
                                    }

                                    sparks.enableEmission = false;
                                }
                            }

                            if (useGun)
                            {
                                if (inputManager.isInput[InputCode.attack1])
                                {
                                    leftArmAim = true;
                                    rightArmAim = true;
                                }
                                else if (inputManager.isInput[InputCode.attack0])
                                {
                                    if (leftGunHasBullet)
                                    {
                                        leftArmAim = true;
                                        rightArmAim = false;
                                    }
                                    else
                                    {
                                        leftArmAim = false;
                                        if (rightGunHasBullet)
                                        {
                                            rightArmAim = true;
                                        }
                                        else
                                        {
                                            rightArmAim = false;
                                        }
                                    }
                                }
                                else
                                {
                                    leftArmAim = false;
                                    rightArmAim = false;
                                }

                                if (leftArmAim || rightArmAim)
                                {
                                    RaycastHit hit3;
                                    var ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                    LayerMask mask7 = 1 << LayerMask.NameToLayer("Ground");
                                    LayerMask mask8 = 1 << LayerMask.NameToLayer("EnemyBox");
                                    LayerMask mask9 = mask8 | mask7;
                                    if (Physics.Raycast(ray3, out hit3, 1E+07f, mask9.value))
                                    {
                                        gunTarget = hit3.point;
                                    }
                                }

                                var flag4 = false;
                                var flag5 = false;
                                var flag6 = false;
                                if (inputManager.isInputUp[InputCode.attack1] && skillId != "bomb")
                                {
                                    if (leftGunHasBullet && rightGunHasBullet)
                                    {
                                        if (grounded)
                                        {
                                            attackAnimation = "AHSS_shoot_both";
                                        }
                                        else
                                        {
                                            attackAnimation = "AHSS_shoot_both_air";
                                        }

                                        flag4 = true;
                                    }
                                    else if (!(leftGunHasBullet || rightGunHasBullet))
                                    {
                                        flag5 = true;
                                    }
                                    else
                                    {
                                        flag6 = true;
                                    }
                                }

                                if (flag6 || inputManager.isInputUp[InputCode.attack0])
                                {
                                    if (grounded)
                                    {
                                        if (leftGunHasBullet && rightGunHasBullet)
                                        {
                                            if (isLeftHandHooked)
                                            {
                                                attackAnimation = "AHSS_shoot_r";
                                            }
                                            else
                                            {
                                                attackAnimation = "AHSS_shoot_l";
                                            }
                                        }
                                        else if (leftGunHasBullet)
                                        {
                                            attackAnimation = "AHSS_shoot_l";
                                        }
                                        else if (rightGunHasBullet)
                                        {
                                            attackAnimation = "AHSS_shoot_r";
                                        }
                                    }
                                    else if (leftGunHasBullet && rightGunHasBullet)
                                    {
                                        if (isLeftHandHooked)
                                        {
                                            attackAnimation = "AHSS_shoot_r_air";
                                        }
                                        else
                                        {
                                            attackAnimation = "AHSS_shoot_l_air";
                                        }
                                    }
                                    else if (leftGunHasBullet)
                                    {
                                        attackAnimation = "AHSS_shoot_l_air";
                                    }
                                    else if (rightGunHasBullet)
                                    {
                                        attackAnimation = "AHSS_shoot_r_air";
                                    }

                                    if (leftGunHasBullet || rightGunHasBullet)
                                    {
                                        flag4 = true;
                                    }
                                    else
                                    {
                                        flag5 = true;
                                    }
                                }

                                if (flag4)
                                {
                                    state = HERO_STATE.Attack;
                                    crossFade(attackAnimation, 0.05f);
                                    gunDummy.transform.position = baseTransform.position;
                                    gunDummy.transform.rotation = baseTransform.rotation;
                                    gunDummy.transform.LookAt(gunTarget);
                                    attackReleased = false;
                                    facingDirection = gunDummy.transform.rotation.eulerAngles.y;
                                    targetRotation = Quaternion.Euler(0f, facingDirection, 0f);
                                }
                                else if (flag5 && (grounded || LevelInfo.getInfo(FengGameManagerMKII.level).type != GAMEMODE.PVP_AHSS && RCSettings.ahssReload == 0))
                                {
                                    changeBlade();
                                }
                            }
                        }
                        else if (state == HERO_STATE.Attack)
                        {
                            if (!useGun)
                            {
                                if (!inputManager.isInput[InputCode.attack0])
                                {
                                    buttonAttackRelease = true;
                                }

                                if (!attackReleased)
                                {
                                    if (buttonAttackRelease)
                                    {
                                        continueAnimation();
                                        attackReleased = true;
                                    }
                                    else if (baseAnimation[attackAnimation].normalizedTime >= 0.32f)
                                    {
                                        pauseAnimation();
                                    }
                                }

                                if (attackAnimation == "attack3_1" && currentBladeSta > 0f)
                                {
                                    if (baseAnimation[attackAnimation].normalizedTime >= 0.8f)
                                    {
                                        if (!checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            if (Settings.BladeTrailsSetting)
                                            {
                                                leftbladetrail2.Activate();
                                                rightbladetrail2.Activate();
                                                leftbladetrail.Activate();
                                                rightbladetrail.Activate();
                                            }

                                            baseRigidBody.velocity = -Vector3.up * 30f;
                                        }

                                        if (!checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            slash.Play();
                                        }
                                    }
                                    else if (checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                    {
                                        checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                        checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                        leftbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                                        rightbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                                        leftbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
                                        rightbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
                                    }
                                }
                                else
                                {
                                    float num;
                                    float num2;
                                    if (currentBladeSta == 0f)
                                    {
                                        num2 = num = -1f;
                                    }
                                    else if (attackAnimation == "attack5")
                                    {
                                        num2 = 0.35f;
                                        num = 0.5f;
                                    }
                                    else if (attackAnimation == "special_petra")
                                    {
                                        num2 = 0.35f;
                                        num = 0.48f;
                                    }
                                    else if (attackAnimation == "special_armin")
                                    {
                                        num2 = 0.25f;
                                        num = 0.35f;
                                    }
                                    else if (attackAnimation == "attack4")
                                    {
                                        num2 = 0.6f;
                                        num = 0.9f;
                                    }
                                    else if (attackAnimation == "special_sasha")
                                    {
                                        num2 = num = -1f;
                                    }
                                    else
                                    {
                                        num2 = 0.5f;
                                        num = 0.85f;
                                    }

                                    if (baseAnimation[attackAnimation].normalizedTime > num2 && baseAnimation[attackAnimation].normalizedTime < num)
                                    {
                                        if (!checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = true;
                                            slash.Play();
                                            if (Settings.BladeTrailsSetting)
                                            {
                                                leftbladetrail2.Activate();
                                                rightbladetrail2.Activate();
                                                leftbladetrail.Activate();
                                                rightbladetrail.Activate();
                                            }
                                        }

                                        if (!checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me)
                                        {
                                            checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = true;
                                        }
                                    }
                                    else if (checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me)
                                    {
                                        checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                                        checkBoxLeft.GetComponent<TriggerColliderWeapon>().clearHits();
                                        checkBoxRight.GetComponent<TriggerColliderWeapon>().clearHits();
                                        leftbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
                                        rightbladetrail2.StopSmoothly(XWeaponTrail.FadeTime);
                                        leftbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                                        rightbladetrail.StopSmoothly(XWeaponTrail.FadeTime);
                                    }

                                    if (attackLoop > 0 && baseAnimation[attackAnimation].normalizedTime > num)
                                    {
                                        attackLoop--;
                                        playAnimationAt(attackAnimation, num2);
                                    }
                                }

                                if (baseAnimation[attackAnimation].normalizedTime >= 1f)
                                {
                                    if (attackAnimation == "special_marco_0" || attackAnimation == "special_marco_1")
                                    {
                                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                        {
                                            if (!PhotonNetwork.isMasterClient)
                                            {
                                                object[] parameters = { 5f, 100f };
                                                photonView.RPC("netTauntAttack", PhotonTargets.MasterClient, parameters);
                                            }
                                            else
                                            {
                                                netTauntAttack(5f);
                                            }
                                        }
                                        else
                                        {
                                            netTauntAttack(5f);
                                        }

                                        falseAttack();
                                        idle();
                                    }
                                    else if (attackAnimation == "special_armin")
                                    {
                                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                        {
                                            if (!PhotonNetwork.isMasterClient)
                                            {
                                                photonView.RPC("netlaughAttack", PhotonTargets.MasterClient);
                                            }
                                            else
                                            {
                                                netlaughAttack();
                                            }
                                        }
                                        else
                                        {
                                            foreach (var obj3 in GameObject.FindGameObjectsWithTag("titan"))
                                            {
                                                if (Vector3.Distance(obj3.transform.position, baseTransform.position) < 50f && Vector3.Angle(obj3.transform.forward, baseTransform.position - obj3.transform.position) < 90f && obj3.GetComponent<TITAN>() != null)
                                                {
                                                    obj3.GetComponent<TITAN>().beLaughAttacked();
                                                }
                                            }
                                        }

                                        falseAttack();
                                        idle();
                                    }
                                    else if (attackAnimation == "attack3_1")
                                    {
                                        baseRigidBody.velocity -= Vector3.up * Time.deltaTime * 30f;
                                    }
                                    else
                                    {
                                        falseAttack();
                                        idle();
                                    }
                                }

                                if (baseAnimation.IsPlaying("attack3_2") && baseAnimation["attack3_2"].normalizedTime >= 1f)
                                {
                                    falseAttack();
                                    idle();
                                }
                            }
                            else
                            {
                                baseTransform.rotation = Quaternion.Lerp(baseTransform.rotation, gunDummy.transform.rotation, Time.deltaTime * 30f);
                                if (!attackReleased && baseAnimation[attackAnimation].normalizedTime > 0.167f)
                                {
                                    GameObject obj4;
                                    attackReleased = true;
                                    var flag7 = false;
                                    if (attackAnimation == "AHSS_shoot_both" || attackAnimation == "AHSS_shoot_both_air")
                                    {
                                        flag7 = true;
                                        leftGunHasBullet = Settings.InfiniteBulletsNoReloadingSetting;
                                        rightGunHasBullet = Settings.InfiniteBulletsNoReloadingSetting;
                                        baseRigidBody.AddForce(-baseTransform.forward * 1000f, ForceMode.Acceleration);
                                    }
                                    else
                                    {
                                        if (attackAnimation == "AHSS_shoot_l" || attackAnimation == "AHSS_shoot_l_air")
                                        {
                                            leftGunHasBullet = Settings.InfiniteBulletsNoReloadingSetting;
                                        }
                                        else
                                        {
                                            rightGunHasBullet = Settings.InfiniteBulletsNoReloadingSetting;
                                        }

                                        baseRigidBody.AddForce(-baseTransform.forward * 600f, ForceMode.Acceleration);
                                    }

                                    baseRigidBody.AddForce(Vector3.up * 200f, ForceMode.Acceleration);
                                    var prefabName = "FX/shotGun";
                                    if (flag7)
                                    {
                                        prefabName = "FX/shotGun 1";
                                    }

                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && photonView.isMine)
                                    {
                                        obj4 = PhotonNetwork.Instantiate(prefabName, baseTransform.position + baseTransform.up * 0.8f - baseTransform.right * 0.1f, baseTransform.rotation, 0);
                                        if (obj4.GetComponent<EnemyfxIDcontainer>() != null)
                                        {
                                            obj4.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = photonView.viewID;
                                        }
                                    }
                                    else
                                    {
                                        obj4 = (GameObject)Instantiate(Resources.Load(prefabName), baseTransform.position + baseTransform.up * 0.8f - baseTransform.right * 0.1f, baseTransform.rotation);
                                    }
                                }

                                if (baseAnimation[attackAnimation].normalizedTime >= 1f)
                                {
                                    falseAttack();
                                    idle();
                                }

                                if (!baseAnimation.IsPlaying(attackAnimation))
                                {
                                    falseAttack();
                                    idle();
                                }
                            }
                        }
                        else if (state == HERO_STATE.ChangeBlade)
                        {
                            if (useGun)
                            {
                                if (baseAnimation[reloadAnimation].normalizedTime > 0.22f)
                                {
                                    if (!(leftGunHasBullet || !setup.part_blade_l.activeSelf))
                                    {
                                        setup.part_blade_l.SetActive(false);
                                        var transform = setup.part_blade_l.transform;
                                        var obj5 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
                                        obj5.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
                                        var force = -baseTransform.forward * 10f + baseTransform.up * 5f - baseTransform.right;
                                        obj5.rigidbody.AddForce(force, ForceMode.Impulse);
                                        var torque = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
                                        obj5.rigidbody.AddTorque(torque, ForceMode.Acceleration);
                                    }

                                    if (!(rightGunHasBullet || !setup.part_blade_r.activeSelf))
                                    {
                                        setup.part_blade_r.SetActive(false);
                                        var transform5 = setup.part_blade_r.transform;
                                        var obj6 = (GameObject)Instantiate(Resources.Load("Character_parts/character_gun_r"), transform5.position, transform5.rotation);
                                        obj6.renderer.material = CharacterMaterials.materials[setup.myCostume._3dmg_texture];
                                        var vector3 = -baseTransform.forward * 10f + baseTransform.up * 5f + baseTransform.right;
                                        obj6.rigidbody.AddForce(vector3, ForceMode.Impulse);
                                        var vector4 = new Vector3(Random.Range(-300, 300), Random.Range(-300, 300), Random.Range(-300, 300));
                                        obj6.rigidbody.AddTorque(vector4, ForceMode.Acceleration);
                                    }
                                }

                                if (baseAnimation[reloadAnimation].normalizedTime > 0.62f && !throwedBlades)
                                {
                                    throwedBlades = true;
                                    if (!(leftBulletLeft <= 0 || leftGunHasBullet))
                                    {
                                        if (!Settings.InfiniteBulletsSetting) leftBulletLeft--;
                                        setup.part_blade_l.SetActive(true);
                                        leftGunHasBullet = true;
                                    }

                                    if (!(rightBulletLeft <= 0 || rightGunHasBullet))
                                    {
                                        setup.part_blade_r.SetActive(true);
                                        if (!Settings.InfiniteBulletsSetting) rightBulletLeft--;
                                        rightGunHasBullet = true;
                                    }

                                    updateRightMagUI();
                                    updateLeftMagUI();
                                }

                                if (baseAnimation[reloadAnimation].normalizedTime > 1f)
                                {
                                    idle();
                                }
                            }
                            else
                            {
                                if (!grounded)
                                {
                                    if (animation[reloadAnimation].normalizedTime >= 0.2f && !throwedBlades)
                                    {
                                        throwedBlades = true;
                                        if (setup.part_blade_l.activeSelf)
                                        {
                                            throwBlades();
                                        }
                                    }

                                    if (animation[reloadAnimation].normalizedTime >= 0.56f && currentBladeNum > 0)
                                    {
                                        setup.part_blade_l.SetActive(true);
                                        setup.part_blade_r.SetActive(true);
                                        currentBladeSta = totalBladeSta;
                                    }
                                }
                                else
                                {
                                    if (baseAnimation[reloadAnimation].normalizedTime >= 0.13f && !throwedBlades)
                                    {
                                        throwedBlades = true;
                                        if (setup.part_blade_l.activeSelf)
                                        {
                                            throwBlades();
                                        }
                                    }

                                    if (baseAnimation[reloadAnimation].normalizedTime >= 0.37f && currentBladeNum > 0)
                                    {
                                        setup.part_blade_l.SetActive(true);
                                        setup.part_blade_r.SetActive(true);
                                        currentBladeSta = totalBladeSta;
                                    }
                                }

                                if (baseAnimation[reloadAnimation].normalizedTime >= 1f)
                                {
                                    idle();
                                }
                            }
                        }
                        else if (state == HERO_STATE.Salute)
                        {
                            if (baseAnimation["salute"].normalizedTime >= 1f)
                            {
                                idle();
                            }
                        }
                        else if (state == HERO_STATE.GroundDodge)
                        {
                            if (baseAnimation.IsPlaying("dodge"))
                            {
                                if (!(grounded || baseAnimation["dodge"].normalizedTime <= 0.6f))
                                {
                                    idle();
                                }

                                if (baseAnimation["dodge"].normalizedTime >= 1f)
                                {
                                    idle();
                                }
                            }
                        }
                        else if (state == HERO_STATE.Land)
                        {
                            if (baseAnimation.IsPlaying("dash_land") && baseAnimation["dash_land"].normalizedTime >= 1f)
                            {
                                idle();
                            }
                        }
                        else if (state == HERO_STATE.FillGas)
                        {
                            if (baseAnimation.IsPlaying("supply") && baseAnimation["supply"].normalizedTime >= 1f)
                            {
                                currentBladeSta = totalBladeSta;
                                currentBladeNum = totalBladeNum;
                                currentGas = totalGas;
                                if (!useGun)
                                {
                                    setup.part_blade_l.SetActive(true);
                                    setup.part_blade_r.SetActive(true);
                                }
                                else
                                {
                                    leftBulletLeft = rightBulletLeft = bulletMAX;
                                    leftGunHasBullet = rightGunHasBullet = true;
                                    setup.part_blade_l.SetActive(true);
                                    setup.part_blade_r.SetActive(true);
                                    updateRightMagUI();
                                    updateLeftMagUI();
                                }

                                idle();
                            }
                        }
                        else if (state == HERO_STATE.Slide)
                        {
                            if (!grounded)
                            {
                                idle();
                            }
                        }
                        else if (state == HERO_STATE.AirDodge)
                        {
                            if (dashTime > 0f)
                            {
                                dashTime -= Time.deltaTime;
                                if (currentSpeed > originVM)
                                {
                                    baseRigidBody.AddForce(-baseRigidBody.velocity * Time.deltaTime * 1.7f, ForceMode.VelocityChange);
                                }
                            }
                            else
                            {
                                dashTime = 0f;
                                idle();
                            }
                        }

                        if (inputManager.isInput[InputCode.leftRope] && (!baseAnimation.IsPlaying("attack3_1") && !baseAnimation.IsPlaying("attack5") && !baseAnimation.IsPlaying("special_petra") && state != HERO_STATE.Grab || state == HERO_STATE.Idle))
                        {
                            if (bulletLeft != null)
                            {
                                QHold = true;
                            }
                            else
                            {
                                RaycastHit hit4;
                                var ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask10 = 1 << LayerMask.NameToLayer("Ground");
                                LayerMask mask11 = 1 << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask12 = mask11 | mask10;
                                if (Physics.Raycast(ray4, out hit4, 10000f, mask12.value))
                                {
                                    launchLeftRope(hit4, true, 0);
                                    rope.Play();
                                }
                            }
                        }
                        else
                        {
                            QHold = false;
                        }

                        if (inputManager.isInput[InputCode.rightRope] && (!baseAnimation.IsPlaying("attack3_1") && !baseAnimation.IsPlaying("attack5") && !baseAnimation.IsPlaying("special_petra") && state != HERO_STATE.Grab || state == HERO_STATE.Idle))
                        {
                            if (bulletRight != null)
                            {
                                EHold = true;
                            }
                            else
                            {
                                RaycastHit hit5;
                                var ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask13 = 1 << LayerMask.NameToLayer("Ground");
                                LayerMask mask14 = 1 << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask15 = mask14 | mask13;
                                if (Physics.Raycast(ray5, out hit5, 10000f, mask15.value))
                                {
                                    launchRightRope(hit5, true, 0);
                                    rope.Play();
                                }
                            }
                        }
                        else
                        {
                            EHold = false;
                        }

                        if (inputManager.isInput[InputCode.bothRope] && (!baseAnimation.IsPlaying("attack3_1") && !baseAnimation.IsPlaying("attack5") && !baseAnimation.IsPlaying("special_petra") && state != HERO_STATE.Grab || state == HERO_STATE.Idle))
                        {
                            QHold = true;
                            EHold = true;
                            if (bulletLeft == null && bulletRight == null)
                            {
                                RaycastHit hit6;
                                var ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask16 = 1 << LayerMask.NameToLayer("Ground");
                                LayerMask mask17 = 1 << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask18 = mask17 | mask16;
                                if (Physics.Raycast(ray6, out hit6, 1000000f, mask18.value))
                                {
                                    launchLeftRope(hit6, false, 0);
                                    launchRightRope(hit6, false, 0);
                                    rope.Play();
                                }
                            }
                        }

                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE && !IN_GAME_MAIN_CAMERA.isPausing)
                        {
                            calcSkillCD();
                            calcFlareCD();
                        }

                        if (!useGun)
                        {
                            if (leftbladetrail.gameObject.GetActive())
                            {
                                leftbladetrail.update();
                                rightbladetrail.update();
                            }

                            if (leftbladetrail2.gameObject.GetActive())
                            {
                                leftbladetrail2.update();
                                rightbladetrail2.update();
                            }

                            if (leftbladetrail.gameObject.GetActive())
                            {
                                leftbladetrail.lateUpdate();
                                rightbladetrail.lateUpdate();
                            }

                            if (leftbladetrail2.gameObject.GetActive())
                            {
                                leftbladetrail2.lateUpdate();
                                rightbladetrail2.lateUpdate();
                            }
                        }

                        if (!IN_GAME_MAIN_CAMERA.isPausing)
                        {
                            showSkillCD();
                            showFlareCD();
                            showGas();
                            showAimUI();
                        }
                    }
                    else if (isCannon && !IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        showAimUI();
                        calcSkillCD();
                        showSkillCD();
                    }
                }
            }
        }

        var propertiesToSet = new Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.current_gas, currentGas);
    }

    public void updateCannon()
    {
        baseTransform.position = myCannonPlayer.position;
        baseTransform.rotation = myCannonBase.rotation;
    }

    public void updateExt()
    {
        if (skillId == "bomb")
        {
            if (inputManager.isInputDown[InputCode.attack1] && skillCDDuration <= 0f)
            {
                if (!(myBomb == null || myBomb.disabled))
                {
                    myBomb.Explode(bombRadius);
                }

                detonate = false;
                skillCDDuration = bombCD;
                var hitInfo = new RaycastHit();
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
                LayerMask mask3 = mask2 | mask;
                currentV = baseTransform.position;
                targetV = currentV + Vector3.forward * 200f;
                if (Physics.Raycast(ray, out hitInfo, 1000000f, mask3.value))
                {
                    targetV = hitInfo.point;
                }

                var vector = Vector3.Normalize(targetV - currentV);
                var obj2 = PhotonNetwork.Instantiate("RCAsset/BombMain", currentV + vector * 4f, new Quaternion(0f, 0f, 0f, 1f), 0);
                obj2.rigidbody.velocity = vector * bombSpeed;
                myBomb = obj2.GetComponent<Bomb>();
                bombTime = 0f;
            }
            else if (myBomb != null && !myBomb.disabled)
            {
                bombTime += Time.deltaTime;
                var flag2 = false;
                if (inputManager.isInputUp[InputCode.attack1])
                {
                    detonate = true;
                }
                else if (inputManager.isInputDown[InputCode.attack1] && detonate)
                {
                    detonate = false;
                    flag2 = true;
                }

                if (bombTime >= bombTimeMax)
                {
                    flag2 = true;
                }

                if (flag2)
                {
                    myBomb.Explode(bombRadius);
                    detonate = false;
                }
            }
        }
    }

    private void updateLeftMagUI()
    {
        for (var i = 1; i <= bulletMAX; i++)
        {
            GGM.Caching.GameObjectCache.Find("bulletL" + i).GetComponent<UISprite>().enabled = false;
        }

        for (var j = 1; j <= leftBulletLeft; j++)
        {
            GGM.Caching.GameObjectCache.Find("bulletL" + j).GetComponent<UISprite>().enabled = true;
        }
    }

    private void updateRightMagUI()
    {
        for (var i = 1; i <= bulletMAX; i++)
        {
            GGM.Caching.GameObjectCache.Find("bulletR" + i).GetComponent<UISprite>().enabled = false;
        }

        for (var j = 1; j <= rightBulletLeft; j++)
        {
            GGM.Caching.GameObjectCache.Find("bulletR" + j).GetComponent<UISprite>().enabled = true;
        }
    }

    public void useBlade(int amount)
    {
        if (Settings.InfiniteBladesSetting) return;
        if (amount == 0)
        {
            amount = 1;
        }

        amount *= 2;
        if (currentBladeSta > 0f)
        {
            currentBladeSta -= amount;
            if (currentBladeSta <= 0f)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
                {
                    leftbladetrail.Deactivate();
                    rightbladetrail.Deactivate();
                    leftbladetrail2.Deactivate();
                    rightbladetrail2.Deactivate();
                    checkBoxLeft.GetComponent<TriggerColliderWeapon>().active_me = false;
                    checkBoxRight.GetComponent<TriggerColliderWeapon>().active_me = false;
                }

                currentBladeSta = 0f;
                throwBlades();
            }
        }
    }

    private void useGas(float amount)
    {
        if (Settings.InfiniteGasSetting) return;
        if (amount == 0f)
        {
            amount = useGasSpeed;
        }

        if (currentGas > 0f)
        {
            currentGas -= amount;
            if (currentGas < 0f)
            {
                currentGas = 0f;
            }
        }
    }

    [RPC]
    private void whoIsMyErenTitan(int id)
    {
        eren_titan = PhotonView.Find(id).gameObject;
        titanForm = true;
    }

    public bool isGrabbed
    {
        get { return state == HERO_STATE.Grab; }
    }

    private HERO_STATE state
    {
        get { return _state; }
        set
        {
            if (_state == HERO_STATE.AirDodge || _state == HERO_STATE.GroundDodge)
            {
                dashTime = 0f;
            }

            _state = value;
        }
    }
}