using System.Collections;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class TITAN_EREN : MonoBehaviour
{
    private string attackAnimation;
    private Transform attackBox;
    private bool attackChkOnce;
    public GameObject bottomObject;
    public bool canJump = true;
    private ArrayList checkPoints = new ArrayList();
    public Camera currentCamera;
    private float dieTime;
    private float facingDirection;
    private float gravity = 500f;
    private bool grounded;
    public bool hasDied;
    private bool hasDieSteam;
    public bool hasSpawn;
    private string hitAnimation;
    private float hitPause;
    private ArrayList hitTargets;
    public FengCustomInputs inputManager;
    private bool isAttack;
    public bool isHit;
    private bool isHitWhileCarryingRock;
    private bool isNextAttack;
    private bool isPlayRoar;
    private bool isROCKMOVE;
    public float jumpHeight = 2f;
    private bool justGrounded;
    public float lifeTime = 9999f;
    private float lifeTimeMax = 9999f;
    public float maxVelocityChange = 100f;
    private float myR;
    private bool needFreshCorePosition;
    private bool needRoar;
    private Vector3 oldCorePosition;
    public GameObject realBody;
    public GameObject rock;
    private bool rockHitGround;
    public bool rockLift;
    private int rockPhase;
    public float speed = 80f;
    private float sqrt2 = Mathf.Sqrt(2f);
    private int stepSoundPhase = 2;
    private Vector3 targetCheckPt;
    private float waitCounter;

    public void born()
    {
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (obj2.GetComponent<FEMALE_TITAN>() != null)
            {
                obj2.GetComponent<FEMALE_TITAN>().erenIsHere(gameObject);
            }
        }
        if (!bottomObject.GetComponent<CheckHitGround>().isGrounded)
        {
            playAnimation("jump_air");
            needRoar = true;
        }
        else
        {
            needRoar = false;
            playAnimation("born");
            isPlayRoar = false;
        }
        playSound("snd_eren_shift");
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Instantiate(Resources.Load("FX/Thunder"), transform.position + Vector3.up * 23f, Quaternion.Euler(270f, 0f, 0f));
        }
        else if (photonView.isMine)
        {
            PhotonNetwork.Instantiate("FX/Thunder", transform.position + Vector3.up * 23f, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        lifeTimeMax = lifeTime = 30f;
    }

    private void crossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName, time };
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void endMovingRock()
    {
        isROCKMOVE = false;
    }

    private void falseAttack()
    {
        isAttack = false;
        isNextAttack = false;
        hitTargets = new ArrayList();
        attackChkOnce = false;
    }

    private void FixedUpdate()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            if (rockLift)
            {
                RockUpdate();
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                if (hitPause > 0f)
                {
                    rigidbody.velocity = Vector3.zero;
                }
                else if (hasDied)
                {
                    rigidbody.velocity = Vector3.zero + Vector3.up * rigidbody.velocity.y;
                    rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
                {
                    if (rigidbody.velocity.magnitude > 50f)
                    {
                        currentCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentCamera.GetComponent<Camera>().fieldOfView, Mathf.Min(100f, rigidbody.velocity.magnitude), 0.1f);
                    }
                    else
                    {
                        currentCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentCamera.GetComponent<Camera>().fieldOfView, 50f, 0.1f);
                    }
                    if (bottomObject.GetComponent<CheckHitGround>().isGrounded)
                    {
                        if (!grounded)
                        {
                            justGrounded = true;
                        }
                        grounded = true;
                        bottomObject.GetComponent<CheckHitGround>().isGrounded = false;
                    }
                    else
                    {
                        grounded = false;
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
                    if (needFreshCorePosition)
                    {
                        oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
                        needFreshCorePosition = false;
                    }
                    if (isAttack || isHit)
                    {
                        var vector4 = transform.position - transform.Find("Amarture/Core").position - oldCorePosition;
                        oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
                        rigidbody.velocity = vector4 / Time.deltaTime + Vector3.up * rigidbody.velocity.y;
                        rigidbody.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, facingDirection, 0f), Time.deltaTime * 10f);
                        if (justGrounded)
                        {
                            justGrounded = false;
                        }
                    }
                    else if (grounded)
                    {
                        var zero = Vector3.zero;
                        if (justGrounded)
                        {
                            justGrounded = false;
                            zero = rigidbody.velocity;
                            if (animation.IsPlaying("jump_air"))
                            {
                                var obj2 = (GameObject) Instantiate(Resources.Load("FX/boom2_eren"), transform.position, Quaternion.Euler(270f, 0f, 0f));
                                obj2.transform.localScale = Vector3.one * 1.5f;
                                if (needRoar)
                                {
                                    playAnimation("born");
                                    needRoar = false;
                                    isPlayRoar = false;
                                }
                                else
                                {
                                    playAnimation("jump_land");
                                }
                            }
                        }
                        if (!animation.IsPlaying("jump_land") && !isAttack && !isHit && !animation.IsPlaying("born"))
                        {
                            var vector7 = new Vector3(x, 0f, z);
                            var y = currentCamera.transform.rotation.eulerAngles.y;
                            var num4 = Mathf.Atan2(z, x) * 57.29578f;
                            num4 = -num4 + 90f;
                            var num5 = y + num4;
                            var num6 = -num5 + 90f;
                            var num7 = Mathf.Cos(num6 * 0.01745329f);
                            var num8 = Mathf.Sin(num6 * 0.01745329f);
                            zero = new Vector3(num7, 0f, num8);
                            var num9 = vector7.magnitude <= 0.95f ? vector7.magnitude >= 0.25f ? vector7.magnitude : 0f : 1f;
                            zero = zero * num9;
                            zero = zero * speed;
                            if (x != 0f || z != 0f)
                            {
                                if (!animation.IsPlaying("run") && !animation.IsPlaying("jump_start") && !animation.IsPlaying("jump_air"))
                                {
                                    crossFade("run", 0.1f);
                                }
                            }
                            else
                            {
                                if (!animation.IsPlaying("idle") && !animation.IsPlaying("dash_land") && !animation.IsPlaying("dodge") && !animation.IsPlaying("jump_start") && !animation.IsPlaying("jump_air") && !animation.IsPlaying("jump_land"))
                                {
                                    crossFade("idle", 0.1f);
                                    zero = zero * 0f;
                                }
                                num5 = -874f;
                            }
                            if (num5 != -874f)
                            {
                                facingDirection = num5;
                            }
                        }
                        var velocity = rigidbody.velocity;
                        var force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -maxVelocityChange, maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -maxVelocityChange, maxVelocityChange);
                        force.y = 0f;
                        if (animation.IsPlaying("jump_start") && animation["jump_start"].normalizedTime >= 1f)
                        {
                            playAnimation("jump_air");
                            force.y += 240f;
                        }
                        else if (animation.IsPlaying("jump_start"))
                        {
                            force = -rigidbody.velocity;
                        }
                        rigidbody.AddForce(force, ForceMode.VelocityChange);
                        rigidbody.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, facingDirection, 0f), Time.deltaTime * 10f);
                    }
                    else
                    {
                        if (animation.IsPlaying("jump_start") && animation["jump_start"].normalizedTime >= 1f)
                        {
                            playAnimation("jump_air");
                            rigidbody.AddForce(Vector3.up * 240f, ForceMode.VelocityChange);
                        }
                        if (!animation.IsPlaying("jump") && !isHit)
                        {
                            var vector11 = new Vector3(x, 0f, z);
                            var num10 = currentCamera.transform.rotation.eulerAngles.y;
                            var num11 = Mathf.Atan2(z, x) * 57.29578f;
                            num11 = -num11 + 90f;
                            var num12 = num10 + num11;
                            var num13 = -num12 + 90f;
                            var num14 = Mathf.Cos(num13 * 0.01745329f);
                            var num15 = Mathf.Sin(num13 * 0.01745329f);
                            var vector13 = new Vector3(num14, 0f, num15);
                            var num16 = vector11.magnitude <= 0.95f ? vector11.magnitude >= 0.25f ? vector11.magnitude : 0f : 1f;
                            vector13 = vector13 * num16;
                            vector13 = vector13 * (speed * 2f);
                            if (x != 0f || z != 0f)
                            {
                                rigidbody.AddForce(vector13, ForceMode.Impulse);
                            }
                            else
                            {
                                num12 = -874f;
                            }
                            if (num12 != -874f)
                            {
                                facingDirection = num12;
                            }
                            if (!animation.IsPlaying(string.Empty) && !animation.IsPlaying("attack3_2") && !animation.IsPlaying("attack5"))
                            {
                                rigidbody.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, facingDirection, 0f), Time.deltaTime * 6f);
                            }
                        }
                    }
                    rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
                }
            }
        }
    }

    public void hitByFT(int phase)
    {
        if (!hasDied)
        {
            isHit = true;
            hitAnimation = "hit_annie_" + phase;
            falseAttack();
            playAnimation(hitAnimation);
            needFreshCorePosition = true;
            if (phase == 3)
            {
                GameObject obj2;
                hasDied = true;
                var transform = this.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                {
                    obj2 = PhotonNetwork.Instantiate("bloodExplore", transform.position + Vector3.up * 1f * 4f, Quaternion.Euler(270f, 0f, 0f), 0);
                }
                else
                {
                    obj2 = (GameObject) Instantiate(Resources.Load("bloodExplore"), transform.position + Vector3.up * 1f * 4f, Quaternion.Euler(270f, 0f, 0f));
                }
                obj2.transform.localScale = this.transform.localScale;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                {
                    obj2 = PhotonNetwork.Instantiate("bloodsplatter", transform.position, Quaternion.Euler(90f + transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 0);
                }
                else
                {
                    obj2 = (GameObject) Instantiate(Resources.Load("bloodsplatter"), transform.position, Quaternion.Euler(90f + transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                }
                obj2.transform.localScale = this.transform.localScale;
                obj2.transform.parent = transform;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                {
                    obj2 = PhotonNetwork.Instantiate("FX/justSmoke", transform.position, Quaternion.Euler(270f, 0f, 0f), 0);
                }
                else
                {
                    obj2 = (GameObject) Instantiate(Resources.Load("FX/justSmoke"), transform.position, Quaternion.Euler(270f, 0f, 0f));
                }
                obj2.transform.parent = transform;
            }
        }
    }

    public void hitByFTByServer(int phase)
    {
        object[] parameters = { phase };
        photonView.RPC("hitByFTRPC", PhotonTargets.All, parameters);
    }

    [RPC]
    private void hitByFTRPC(int phase)
    {
        if (photonView.isMine)
        {
            hitByFT(phase);
        }
    }

    public void hitByTitan()
    {
        if (!isHit && !hasDied && !animation.IsPlaying("born"))
        {
            if (rockLift)
            {
                crossFade("die", 0.1f);
                isHitWhileCarryingRock = true;
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameLose();
                object[] parameters = { "set" };
                photonView.RPC("rockPlayAnimation", PhotonTargets.All, parameters);
            }
            else
            {
                isHit = true;
                hitAnimation = "hit_titan";
                falseAttack();
                playAnimation(hitAnimation);
                needFreshCorePosition = true;
            }
        }
    }

    public void hitByTitanByServer()
    {
        photonView.RPC("hitByTitanRPC", PhotonTargets.All);
    }

    [RPC]
    private void hitByTitanRPC()
    {
        if (photonView.isMine)
        {
            hitByTitan();
        }
    }

    public bool IsGrounded()
    {
        return bottomObject.GetComponent<CheckHitGround>().isGrounded;
    }

    public void lateUpdate()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !rockLift && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine))
        {
            var to = Quaternion.Euler(GGM.Caching.GameObjectCache.Find("MainCamera").transform.rotation.eulerAngles.x, GGM.Caching.GameObjectCache.Find("MainCamera").transform.rotation.eulerAngles.y, 0f);
            GGM.Caching.GameObjectCache.Find("MainCamera").transform.rotation = Quaternion.Lerp(GGM.Caching.GameObjectCache.Find("MainCamera").transform.rotation, to, Time.deltaTime * 2f);
        }
    }

    public void loadskin()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            var url = (string) FengGameManagerMKII.settings[0x41];
            if ((int) FengGameManagerMKII.settings[1] == 1 && (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg")))
            {
                StartCoroutine(loadskinE(url));
            }
        }
        else if (photonView.isMine && (int) FengGameManagerMKII.settings[1] == 1)
        {
            photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, (string) FengGameManagerMKII.settings[0x41]);
        }
    }

    public IEnumerator loadskinE(string url)
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
        foreach (var iteratorVariable4 in GetComponentsInChildren<Renderer>())
        {
            if (!FengGameManagerMKII.linkHash[2].ContainsKey(url))
            {
                var link = new WWW(url);
                yield return link;
                Texture2D iteratorVariable6 = RCextensions.loadimage(link, mipmap, 0xf4240);
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
        if ((int) FengGameManagerMKII.settings[1] == 1 && (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg")))
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
    private void netTauntAttack(float tauntTime,float distance = 100f)
    {
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (Vector3.Distance(obj2.transform.position, transform.position) < distance && obj2.GetComponent<TITAN>() != null)
            {
                obj2.GetComponent<TITAN>().beTauntedBy(gameObject, tauntTime);
            }
            if (obj2.GetComponent<FEMALE_TITAN>() != null)
            {
                obj2.GetComponent<FEMALE_TITAN>().erenIsHere(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (GGM.Caching.GameObjectCache.Find("MultiplayerManager") != null)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().removeET(this);
        }
    }

    public void playAnimation(string aniName)
    {
        animation.Play(aniName);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName };
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = { aniName, normalizedTime };
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void playSound(string sndname)
    {
        playsoundRPC(sndname);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
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

    [RPC]
    private void removeMe()
    {
        PhotonNetwork.RemoveRPCs(photonView);
        Destroy(gameObject);
    }

    [RPC]
    private void rockPlayAnimation(string anim)
    {
        rock.animation.Play(anim);
        rock.animation[anim].speed = 1f;
    }

    private void RockUpdate()
    {
        if (!isHitWhileCarryingRock)
        {
            if (isROCKMOVE)
            {
                rock.transform.position = transform.position;
                rock.transform.rotation = transform.rotation;
            }
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                if (rockPhase == 0)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -10f * rigidbody.mass, 0f));
                    waitCounter += Time.deltaTime;
                    if (waitCounter > 20f)
                    {
                        rockPhase++;
                        crossFade("idle", 1f);
                        waitCounter = 0f;
                        setRoute();
                    }
                }
                else if (rockPhase == 1)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
                    waitCounter += Time.deltaTime;
                    if (waitCounter > 2f)
                    {
                        rockPhase++;
                        crossFade("run", 0.2f);
                        waitCounter = 0f;
                    }
                }
                else if (rockPhase == 2)
                {
                    var vector = transform.forward * 30f;
                    var velocity = rigidbody.velocity;
                    var force = vector - velocity;
                    force.x = Mathf.Clamp(force.x, -maxVelocityChange, maxVelocityChange);
                    force.z = Mathf.Clamp(force.z, -maxVelocityChange, maxVelocityChange);
                    force.y = 0f;
                    rigidbody.AddForce(force, ForceMode.VelocityChange);
                    if (transform.position.z < -238f)
                    {
                        transform.position = new Vector3(transform.position.x, 0f, -238f);
                        rockPhase++;
                        crossFade("idle", 0.2f);
                        waitCounter = 0f;
                    }
                }
                else if (rockPhase == 3)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -10f * rigidbody.mass, 0f));
                    waitCounter += Time.deltaTime;
                    if (waitCounter > 1f)
                    {
                        rockPhase++;
                        crossFade("rock_lift", 0.1f);
                        object[] parameters = { "lift" };
                        photonView.RPC("rockPlayAnimation", PhotonTargets.All, parameters);
                        waitCounter = 0f;
                        targetCheckPt = (Vector3) checkPoints[0];
                    }
                }
                else if (rockPhase == 4)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
                    waitCounter += Time.deltaTime;
                    if (waitCounter > 4.2f)
                    {
                        rockPhase++;
                        crossFade("rock_walk", 0.1f);
                        object[] objArray3 = { "move" };
                        photonView.RPC("rockPlayAnimation", PhotonTargets.All, objArray3);
                        rock.animation["move"].normalizedTime = animation["rock_walk"].normalizedTime;
                        waitCounter = 0f;
                        photonView.RPC("startMovingRock", PhotonTargets.All);
                    }
                }
                else if (rockPhase == 5)
                {
                    if (Vector3.Distance(transform.position, targetCheckPt) < 10f)
                    {
                        if (checkPoints.Count > 0)
                        {
                            if (checkPoints.Count == 1)
                            {
                                rockPhase++;
                            }
                            else
                            {
                                var vector6 = (Vector3) checkPoints[0];
                                targetCheckPt = vector6;
                                checkPoints.RemoveAt(0);
                                var objArray = GameObject.FindGameObjectsWithTag("titanRespawn2");
                                var obj2 = GGM.Caching.GameObjectCache.Find("titanRespawnTrost" + (7 - checkPoints.Count));
                                if (obj2 != null)
                                {
                                    foreach (var obj3 in objArray)
                                    {
                                        if (obj3.transform.parent.gameObject == obj2)
                                        {
                                            var obj4 = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().spawnTitan(70, obj3.transform.position, obj3.transform.rotation, false);
                                            obj4.GetComponent<TITAN>().isAlarm = true;
                                            obj4.GetComponent<TITAN>().chaseDistance = 999999f;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            rockPhase++;
                        }
                    }
                    if (checkPoints.Count > 0 && Random.Range(0, 0xbb8) < 10 - checkPoints.Count)
                    {
                        Quaternion quaternion;
                        RaycastHit hit;
                        if (Random.Range(0, 10) > 5)
                        {
                            quaternion = transform.rotation * Quaternion.Euler(0f, Random.Range(150f, 210f), 0f);
                        }
                        else
                        {
                            quaternion = transform.rotation * Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                        }
                        var vector7 = quaternion * new Vector3(Random.Range(100f, 200f), 0f, 0f);
                        var position = transform.position + vector7;
                        LayerMask mask2 = 1 << LayerMask.NameToLayer("Ground");
                        var y = 0f;
                        if (Physics.Raycast(position + Vector3.up * 500f, -Vector3.up, out hit, 1000f, mask2.value))
                        {
                            y = hit.point.y;
                        }
                        position += Vector3.up * y;
                        var obj5 = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().spawnTitan(70, position, transform.rotation, false);
                        obj5.GetComponent<TITAN>().isAlarm = true;
                        obj5.GetComponent<TITAN>().chaseDistance = 999999f;
                    }
                    var vector10 = transform.forward * 6f;
                    var vector11 = rigidbody.velocity;
                    var vector12 = vector10 - vector11;
                    vector12.x = Mathf.Clamp(vector12.x, -maxVelocityChange, maxVelocityChange);
                    vector12.z = Mathf.Clamp(vector12.z, -maxVelocityChange, maxVelocityChange);
                    vector12.y = 0f;
                    rigidbody.AddForce(vector12, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -gravity * rigidbody.mass, 0f));
                    var vector13 = targetCheckPt - transform.position;
                    var current = -Mathf.Atan2(vector13.z, vector13.x) * 57.29578f;
                    var num4 = -Mathf.DeltaAngle(current, gameObject.transform.rotation.eulerAngles.y - 90f);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, gameObject.transform.rotation.eulerAngles.y + num4, 0f), 0.8f * Time.deltaTime);
                }
                else if (rockPhase == 6)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -10f * rigidbody.mass, 0f));
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    rockPhase++;
                    crossFade("rock_fix_hole", 0.1f);
                    object[] objArray4 = { "set" };
                    photonView.RPC("rockPlayAnimation", PhotonTargets.All, objArray4);
                    photonView.RPC("endMovingRock", PhotonTargets.All);
                }
                else if (rockPhase == 7)
                {
                    rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
                    rigidbody.AddForce(new Vector3(0f, -10f * rigidbody.mass, 0f));
                    if (animation["rock_fix_hole"].normalizedTime >= 1.2f)
                    {
                        crossFade("die", 0.1f);
                        rockPhase++;
                        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameWin();
                    }
                    if (animation["rock_fix_hole"].normalizedTime >= 0.62f && !rockHitGround)
                    {
                        rockHitGround = true;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
                        {
                            PhotonNetwork.Instantiate("FX/boom1_CT_KICK", new Vector3(0f, 30f, 684f), Quaternion.Euler(270f, 0f, 0f), 0);
                        }
                        else
                        {
                            Instantiate(Resources.Load("FX/boom1_CT_KICK"), new Vector3(0f, 30f, 684f), Quaternion.Euler(270f, 0f, 0f));
                        }
                    }
                }
            }
        }
    }

    public void setRoute()
    {
        var obj2 = GGM.Caching.GameObjectCache.Find("routeTrost");
        checkPoints = new ArrayList();
        for (var i = 1; i <= 7; i++)
        {
            checkPoints.Add(obj2.transform.Find("r" + i).position);
        }
        checkPoints.Add("end");
    }

    private void showAimUI()
    {
        var obj2 = GGM.Caching.GameObjectCache.Find("cross1");
        var obj3 = GGM.Caching.GameObjectCache.Find("cross2");
        var obj4 = GGM.Caching.GameObjectCache.Find("crossL1");
        var obj5 = GGM.Caching.GameObjectCache.Find("crossL2");
        var obj6 = GGM.Caching.GameObjectCache.Find("crossR1");
        var obj7 = GGM.Caching.GameObjectCache.Find("crossR2");
        var obj8 = GGM.Caching.GameObjectCache.Find("LabelDistance");
        var vector = Vector3.up * 10000f;
        obj7.transform.localPosition = vector;
        obj6.transform.localPosition = vector;
        obj5.transform.localPosition = vector;
        obj4.transform.localPosition = vector;
        obj8.transform.localPosition = vector;
        obj3.transform.localPosition = vector;
        obj2.transform.localPosition = vector;
    }

    private void showSkillCD()
    {
        GGM.Caching.GameObjectCache.Find("skill_cd_eren").GetComponent<UISprite>().fillAmount = lifeTime / lifeTimeMax;
    }

    private void Start()
    {
        loadskin();
        FengGameManagerMKII.instance.addET(this);
        if (rockLift)
        {
            rock = GGM.Caching.GameObjectCache.Find("rock");
            rock.animation["lift"].speed = 0f;
        }
        else
        {
            currentCamera = GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<Camera>();
            inputManager = GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>();
            oldCorePosition = transform.position - transform.Find("Amarture/Core").position;
            myR = sqrt2 * 6f;
            animation["hit_annie_1"].speed = 0.8f;
            animation["hit_annie_2"].speed = 0.7f;
            animation["hit_annie_3"].speed = 0.7f;
        }
        hasSpawn = true;
    }

    [RPC]
    private void startMovingRock()
    {
        isROCKMOVE = true;
    }

    public void update()
    {
        if ((!IN_GAME_MAIN_CAMERA.isPausing || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !rockLift)
        {
            if (animation.IsPlaying("run"))
            {
                if (animation["run"].normalizedTime % 1f > 0.3f && animation["run"].normalizedTime % 1f < 0.75f && stepSoundPhase == 2)
                {
                    stepSoundPhase = 1;
                    var transform = this.transform.Find("snd_eren_foot");
                    transform.GetComponent<AudioSource>().Stop();
                    transform.GetComponent<AudioSource>().Play();
                }
                if (animation["run"].normalizedTime % 1f > 0.75f && stepSoundPhase == 1)
                {
                    stepSoundPhase = 2;
                    var transform2 = transform.Find("snd_eren_foot");
                    transform2.GetComponent<AudioSource>().Stop();
                    transform2.GetComponent<AudioSource>().Play();
                }
            }
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                if (hasDied)
                {
                    if (animation["die"].normalizedTime >= 1f || hitAnimation == "hit_annie_3")
                    {
                        if (realBody != null)
                        {
                            realBody.GetComponent<HERO>().backToHuman();
                            realBody.transform.position = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position + Vector3.up * 2f;
                            realBody = null;
                        }
                        dieTime += Time.deltaTime;
                        if (dieTime > 2f && !hasDieSteam)
                        {
                            hasDieSteam = true;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                var obj2 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie1"));
                                obj2.transform.position = transform.Find("Amarture/Core/Controller_Body/hip").position;
                                obj2.transform.localScale = transform.localScale;
                            }
                            else if (photonView.isMine)
                            {
                                PhotonNetwork.Instantiate("FX/FXtitanDie1", transform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = transform.localScale;
                            }
                        }
                        if (dieTime > 5f)
                        {
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                var obj4 = (GameObject) Instantiate(Resources.Load("FX/FXtitanDie"));
                                obj4.transform.position = transform.Find("Amarture/Core/Controller_Body/hip").position;
                                obj4.transform.localScale = transform.localScale;
                                Destroy(gameObject);
                            }
                            else if (photonView.isMine)
                            {
                                PhotonNetwork.Instantiate("FX/FXtitanDie", transform.Find("Amarture/Core/Controller_Body/hip").position, Quaternion.Euler(-90f, 0f, 0f), 0).transform.localScale = transform.localScale;
                                PhotonNetwork.Destroy(photonView);
                            }
                        }
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
                {
                    if (isHit)
                    {
                        if (animation[hitAnimation].normalizedTime >= 1f)
                        {
                            isHit = false;
                            falseAttack();
                            playAnimation("idle");
                        }
                    }
                    else
                    {
                        if (lifeTime > 0f)
                        {
                            lifeTime -= Time.deltaTime;
                            if (lifeTime <= 0f)
                            {
                                hasDied = true;
                                playAnimation("die");
                                return;
                            }
                        }
                        if (grounded && !isAttack && !animation.IsPlaying("jump_land") && !isAttack && !animation.IsPlaying("born"))
                        {
                            if (inputManager.isInputDown[InputCode.attack0] || inputManager.isInputDown[InputCode.attack1])
                            {
                                var flag = false;
                                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.WOW && inputManager.isInput[InputCode.down] || inputManager.isInputDown[InputCode.attack1])
                                {
                                    if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.WOW && inputManager.isInputDown[InputCode.attack1] && inputManager.inputKey[11] == KeyCode.Mouse1)
                                    {
                                        flag = true;
                                    }
                                    if (flag)
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        attackAnimation = "attack_kick";
                                    }
                                }
                                else
                                {
                                    attackAnimation = "attack_combo_001";
                                }
                                if (!flag)
                                {
                                    playAnimation(attackAnimation);
                                    animation[attackAnimation].time = 0f;
                                    isAttack = true;
                                    needFreshCorePosition = true;
                                    if (attackAnimation == "attack_combo_001" || attackAnimation == "attack_combo_001")
                                    {
                                        attackBox = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
                                    }
                                    else if (attackAnimation == "attack_combo_002")
                                    {
                                        attackBox = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
                                    }
                                    else if (attackAnimation == "attack_kick")
                                    {
                                        attackBox = transform.Find("Amarture/Core/Controller_Body/hip/thigh_R/shin_R/foot_R");
                                    }
                                    hitTargets = new ArrayList();
                                }
                            }
                            if (inputManager.isInputDown[InputCode.salute])
                            {
                                crossFade("born", 0.1f);
                                animation["born"].normalizedTime = 0.28f;
                                isPlayRoar = false;
                            }
                        }
                        if (!isAttack)
                        {
                            if ((grounded || animation.IsPlaying("idle")) && !animation.IsPlaying("jump_start") && !animation.IsPlaying("jump_air") && !animation.IsPlaying("jump_land") && inputManager.isInput[InputCode.bothRope])
                            {
                                crossFade("jump_start", 0.1f);
                            }
                        }
                        else
                        {
                            if (animation[attackAnimation].time >= 0.1f && inputManager.isInputDown[InputCode.attack0])
                            {
                                isNextAttack = true;
                            }
                            var num = 0f;
                            var num2 = 0f;
                            var num3 = 0f;
                            var str = string.Empty;
                            if (attackAnimation == "attack_combo_001")
                            {
                                num = 0.4f;
                                num2 = 0.5f;
                                num3 = 0.66f;
                                str = "attack_combo_002";
                            }
                            else if (attackAnimation == "attack_combo_002")
                            {
                                num = 0.15f;
                                num2 = 0.25f;
                                num3 = 0.43f;
                                str = "attack_combo_003";
                            }
                            else if (attackAnimation == "attack_combo_003")
                            {
                                num3 = 0f;
                                num = 0.31f;
                                num2 = 0.37f;
                            }
                            else if (attackAnimation == "attack_kick")
                            {
                                num3 = 0f;
                                num = 0.32f;
                                num2 = 0.38f;
                            }
                            else
                            {
                                num = 0.5f;
                                num2 = 0.85f;
                            }
                            if (hitPause > 0f)
                            {
                                hitPause -= Time.deltaTime;
                                if (hitPause <= 0f)
                                {
                                    animation[attackAnimation].speed = 1f;
                                    hitPause = 0f;
                                }
                            }
                            if (num3 > 0f && isNextAttack && animation[attackAnimation].normalizedTime >= num3)
                            {
                                if (hitTargets.Count > 0)
                                {
                                    var transform3 = (Transform) hitTargets[0];
                                    if (transform3 != null)
                                    {
                                        transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(transform3.position - transform.position).eulerAngles.y, 0f);
                                        facingDirection = transform.rotation.eulerAngles.y;
                                    }
                                }
                                falseAttack();
                                attackAnimation = str;
                                crossFade(attackAnimation, 0.1f);
                                animation[attackAnimation].time = 0f;
                                animation[attackAnimation].speed = 1f;
                                isAttack = true;
                                needFreshCorePosition = true;
                                if (attackAnimation == "attack_combo_002")
                                {
                                    attackBox = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
                                }
                                else if (attackAnimation == "attack_combo_003")
                                {
                                    attackBox = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
                                }
                                hitTargets = new ArrayList();
                            }
                            if (animation[attackAnimation].normalizedTime >= num && animation[attackAnimation].normalizedTime <= num2 || !attackChkOnce && animation[attackAnimation].normalizedTime >= num)
                            {
                                if (!attackChkOnce)
                                {
                                    if (attackAnimation == "attack_combo_002")
                                    {
                                        playSound("snd_eren_swing2");
                                    }
                                    else if (attackAnimation == "attack_combo_001")
                                    {
                                        playSound("snd_eren_swing1");
                                    }
                                    else if (attackAnimation == "attack_combo_003")
                                    {
                                        playSound("snd_eren_swing3");
                                    }
                                    attackChkOnce = true;
                                }
                                var colliderArray = Physics.OverlapSphere(attackBox.transform.position, 8f);
                                for (var i = 0; i < colliderArray.Length; i++)
                                {
                                    if (colliderArray[i].gameObject.transform.root.GetComponent<TITAN>() == null)
                                    {
                                        continue;
                                    }
                                    var flag2 = false;
                                    for (var j = 0; j < hitTargets.Count; j++)
                                    {
                                        if (colliderArray[i].gameObject.transform.root == (Transform)hitTargets[j])
                                        {
                                            flag2 = true;
                                            break;
                                        }
                                    }
                                    if (!flag2 && !colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().hasDie)
                                    {
                                        animation[attackAnimation].speed = 0f;
                                        if (attackAnimation == "attack_combo_002")
                                        {
                                            hitPause = 0.05f;
                                            colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().hitL(transform.position, hitPause);
                                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(1f, 0.03f);
                                        }
                                        else if (attackAnimation == "attack_combo_001")
                                        {
                                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(1.2f, 0.04f);
                                            hitPause = 0.08f;
                                            colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().hitR(transform.position, hitPause);
                                        }
                                        else if (attackAnimation == "attack_combo_003")
                                        {
                                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(3f, 0.1f);
                                            hitPause = 0.3f;
                                            colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().dieHeadBlow(transform.position, hitPause);
                                        }
                                        else if (attackAnimation == "attack_kick")
                                        {
                                            currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(3f, 0.1f);
                                            hitPause = 0.2f;
                                            if (colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().abnormalType == AbnormalType.TYPE_CRAWLER)
                                            {
                                                colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().dieBlow(transform.position, hitPause);
                                            }
                                            else if (colliderArray[i].gameObject.transform.root.transform.localScale.x < 2f)
                                            {
                                                colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().dieBlow(transform.position, hitPause);
                                            }
                                            else
                                            {
                                                colliderArray[i].gameObject.transform.root.GetComponent<TITAN>().hitR(transform.position, hitPause);
                                            }
                                        }
                                        hitTargets.Add(colliderArray[i].gameObject.transform.root);
                                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                        {
                                            PhotonNetwork.Instantiate("hitMeatBIG", (colliderArray[i].transform.position + attackBox.position) * 0.5f, Quaternion.Euler(270f, 0f, 0f), 0);
                                        }
                                        else
                                        {
                                            Instantiate(Resources.Load("hitMeatBIG"), (colliderArray[i].transform.position + attackBox.position) * 0.5f, Quaternion.Euler(270f, 0f, 0f));
                                        }
                                    }
                                }
                            }
                            if (animation[attackAnimation].normalizedTime >= 1f)
                            {
                                falseAttack();
                                playAnimation("idle");
                            }
                        }
                        if (animation.IsPlaying("jump_land") && animation["jump_land"].normalizedTime >= 1f)
                        {
                            crossFade("idle", 0.1f);
                        }
                        if (animation.IsPlaying("born"))
                        {
                            if (animation["born"].normalizedTime >= 0.28f && !isPlayRoar)
                            {
                                isPlayRoar = true;
                                playSound("snd_eren_roar");
                            }
                            if (animation["born"].normalizedTime >= 0.5f && animation["born"].normalizedTime <= 0.7f)
                            {
                                currentCamera.GetComponent<IN_GAME_MAIN_CAMERA>().startShake(0.5f, 1f);
                            }
                            if (animation["born"].normalizedTime >= 1f)
                            {
                                crossFade("idle", 0.1f);
                                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                {
                                    if (PhotonNetwork.isMasterClient)
                                    {
                                        object[] parameters = { 10f, 500f };
                                        photonView.RPC("netTauntAttack", PhotonTargets.MasterClient, parameters);
                                    }
                                    else
                                    {
                                        netTauntAttack(10f, 500f);
                                    }
                                }
                                else
                                {
                                    netTauntAttack(10f, 500f);
                                }
                            }
                        }
                        showAimUI();
                        showSkillCD();
                    }
                }
            }
        }
    }

}

