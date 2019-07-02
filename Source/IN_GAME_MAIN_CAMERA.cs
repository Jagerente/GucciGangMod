using UnityEngine;

public class IN_GAME_MAIN_CAMERA : MonoBehaviour
{
    public static float cameraDistance = 0.6f;
    public static CAMERA_TYPE cameraMode;
    public static int cameraTilt = 1;
    private float closestDistance;
    private int currentPeekPlayerIndex;
    public static DayLight dayLight = DayLight.Dawn;
    private float decay;
    public static int difficulty;
    private float distance = 10f;
    private float distanceMulti;
    private float distanceOffsetMulti;
    private float duration;
    private float flashDuration;
    private bool flip;
    public static GAMEMODE gamemode;
    public bool gameOver;
    public static GAMETYPE gametype = GAMETYPE.STOP;
    private bool hasSnapShot;
    private Transform head;
    private float heightMulti;
    public FengCustomInputs inputManager;
    public static int invertY = 1;
    public static bool isPausing;
    public static bool isTyping;
    public static int level;
    private bool lockAngle;
    private Vector3 lockCameraPosition;
    private GameObject locker;
    private GameObject lockTarget;
    public GameObject main_object;
    public float maximumX = 360f;
    public float maximumY = 60f;
    public float minimumX = -360f;
    public float minimumY = -60f;
    private bool needSetHUD;
    private float R;
    public int score;
    public static float sensitivityMulti = 0.5f;
    public static string singleCharacter;
    public Material skyBoxDAWN;
    public Material skyBoxDAY;
    public Material skyBoxNIGHT;
    private Texture2D snapshot1;
    private Texture2D snapshot2;
    private Texture2D snapshot3;
    public GameObject snapShotCamera;
    private int snapShotCount;
    private float snapShotCountDown;
    private int snapShotDmg;
    private float snapShotInterval = 0.02f;
    public RenderTexture snapshotRT;
    private float snapShotStartCountDownTime;
    private GameObject snapShotTarget;
    private Vector3 snapShotTargetPosition;
    public bool spectatorMode;
    private bool startSnapShotFrameCount;
    public static STEREO_3D_TYPE stereoType;
    public Texture texture;
    public float timer;
    public static bool triggerAutoLock;
    public static bool usingTitan;
    private Vector3 verticalHeightOffset = Vector3.zero;

    private void Awake()
    {
        isTyping = false;
        isPausing = false;
        name = "MainCamera";
        if (PlayerPrefs.HasKey("GameQuality"))
        {
            if (PlayerPrefs.GetFloat("GameQuality") >= 0.9f)
            {
                GetComponent<TiltShift>().enabled = true;
            }
            else
            {
                GetComponent<TiltShift>().enabled = false;
            }
        }
        else
        {
            GetComponent<TiltShift>().enabled = true;
        }
        CreateMinimap();
    }

    private void camareMovement()
    {
        distanceOffsetMulti = cameraDistance * (200f - camera.fieldOfView) / 150f;
        this.transform.position = head == null ? main_object.transform.position : head.transform.position;
        var transform = this.transform;
        transform.position += Vector3.up * heightMulti;
        var transform2 = this.transform;
        transform2.position -= Vector3.up * (0.6f - cameraDistance) * 2f;
        if (cameraMode == CAMERA_TYPE.WOW)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                var angle = Input.GetAxis("Mouse X") * 10f * getSensitivityMulti();
                var num2 = -Input.GetAxis("Mouse Y") * 10f * getSensitivityMulti() * getReverse();
                this.transform.RotateAround(this.transform.position, Vector3.up, angle);
                this.transform.RotateAround(this.transform.position, this.transform.right, num2);
            }
            var transform3 = this.transform;
            transform3.position -= this.transform.forward * distance * distanceMulti * distanceOffsetMulti;
        }
        else if (cameraMode == CAMERA_TYPE.ORIGINAL)
        {
            var num3 = 0f;
            if (Input.mousePosition.x < Screen.width * 0.4f)
            {
                num3 = -((Screen.width * 0.4f - Input.mousePosition.x) / Screen.width * 0.4f) * getSensitivityMultiWithDeltaTime() * 150f;
                this.transform.RotateAround(this.transform.position, Vector3.up, num3);
            }
            else if (Input.mousePosition.x > Screen.width * 0.6f)
            {
                num3 = (Input.mousePosition.x - Screen.width * 0.6f) / Screen.width * 0.4f * getSensitivityMultiWithDeltaTime() * 150f;
                this.transform.RotateAround(this.transform.position, Vector3.up, num3);
            }
            var x = 140f * (Screen.height * 0.6f - Input.mousePosition.y) / Screen.height * 0.5f;
            this.transform.rotation = Quaternion.Euler(x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
            var transform4 = this.transform;
            transform4.position -= this.transform.forward * distance * distanceMulti * distanceOffsetMulti;
        }
        else if (cameraMode == CAMERA_TYPE.TPS)
        {
            if (!inputManager.menuOn)
            {
                Screen.lockCursor = true;
            }
            var num5 = Input.GetAxis("Mouse X") * 10f * getSensitivityMulti();
            var num6 = -Input.GetAxis("Mouse Y") * 10f * getSensitivityMulti() * getReverse();
            this.transform.RotateAround(this.transform.position, Vector3.up, num5);
            var num7 = this.transform.rotation.eulerAngles.x % 360f;
            var num8 = num7 + num6;
            if ((num6 <= 0f || (num7 >= 260f || num8 <= 260f) && (num7 >= 80f || num8 <= 80f)) && (num6 >= 0f || (num7 <= 280f || num8 >= 280f) && (num7 <= 100f || num8 >= 100f)))
            {
                this.transform.RotateAround(this.transform.position, this.transform.right, num6);
            }
            var transform5 = this.transform;
            transform5.position -= this.transform.forward * distance * distanceMulti * distanceOffsetMulti;
        }
        if (cameraDistance < 0.65f)
        {
            var transform6 = this.transform;
            transform6.position += this.transform.right * Mathf.Max((0.6f - cameraDistance) * 2f, 0.65f);
        }
    }

    public void CameraMovementLive(HERO hero)
    {
        var magnitude = hero.rigidbody.velocity.magnitude;
        if (magnitude > 10f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Mathf.Min(100f, magnitude + 40f), 0.1f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50f, 0.1f);
        }
        var num2 = hero.CameraMultiplier * (200f - Camera.main.fieldOfView) / 150f;
        this.transform.position = head.transform.position + Vector3.up * heightMulti - Vector3.up * (0.6f - cameraDistance) * 2f;
        var transform = this.transform;
        transform.position -= this.transform.forward * distance * distanceMulti * num2;
        if (hero.CameraMultiplier < 0.65f)
        {
            var transform2 = this.transform;
            transform2.position += this.transform.right * Mathf.Max((0.6f - hero.CameraMultiplier) * 2f, 0.65f);
        }
        this.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, hero.GetComponent<SmoothSyncMovement>().correctCameraRot, Time.deltaTime * 5f);
    }

    private void CreateMinimap()
    {
        var info = LevelInfo.getInfo(FengGameManagerMKII.level);
        if (info != null)
        {
            var minimap = gameObject.AddComponent<Minimap>();
            if (Minimap.instance.myCam == null)
            {
                Minimap.instance.myCam = new GameObject().AddComponent<Camera>();
                Minimap.instance.myCam.nearClipPlane = 0.3f;
                Minimap.instance.myCam.farClipPlane = 1000f;
                Minimap.instance.myCam.enabled = false;
            }
            minimap.CreateMinimap(Minimap.instance.myCam, 512, 0.3f, info.minimapPreset);
            if ((int) FengGameManagerMKII.settings[231] == 0 || RCSettings.globalDisableMinimap == 1)
            {
                minimap.SetEnabled(false);
            }
        }
    }

    public void createSnapShotRT()
    {
        if (snapshotRT != null)
        {
            snapshotRT.Release();
        }
        if (QualitySettings.GetQualityLevel() > 3)
        {
            snapshotRT = new RenderTexture((int) (Screen.width * 0.8f), (int) (Screen.height * 0.8f), 24);
            snapShotCamera.GetComponent<Camera>().targetTexture = snapshotRT;
        }
        else
        {
            snapshotRT = new RenderTexture((int) (Screen.width * 0.4f), (int) (Screen.height * 0.4f), 24);
            snapShotCamera.GetComponent<Camera>().targetTexture = snapshotRT;
        }
    }

    private GameObject findNearestTitan()
    {
        var objArray = GameObject.FindGameObjectsWithTag("titan");
        GameObject obj2 = null;
        var num2 = closestDistance = float.PositiveInfinity;
        var position = main_object.transform.position;
        foreach (var obj3 in objArray)
        {
            var vector2 = obj3.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position - position;
            var magnitude = vector2.magnitude;
            if (magnitude < num2 && (obj3.GetComponent<TITAN>() == null || !obj3.GetComponent<TITAN>().hasDie))
            {
                obj2 = obj3;
                num2 = magnitude;
                closestDistance = num2;
            }
        }
        return obj2;
    }

    public void flashBlind()
    {
        GGM.Caching.GameObjectCache.Find("flash").GetComponent<UISprite>().alpha = 1f;
        flashDuration = 2f;
    }

    private int getReverse()
    {
        return invertY;
    }

    private float getSensitivityMulti()
    {
        return sensitivityMulti;
    }

    private float getSensitivityMultiWithDeltaTime()
    {
        return sensitivityMulti * Time.deltaTime * 62f;
    }

    private void reset()
    {
        if (gametype == GAMETYPE.SINGLE)
        {
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().restartGameSingle();
        }
    }

    private Texture2D RTImage(Camera cam)
    {
        var active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        var textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        var num = (int) (cam.targetTexture.width * 0.04f);
        var destX = (int) (cam.targetTexture.width * 0.02f);
        try
        {
            textured.SetPixel(0, 0, Color.white);
            textured.ReadPixels(new Rect(num, num, cam.targetTexture.width - num, cam.targetTexture.height - num), destX, destX);
            textured.Apply();
            RenderTexture.active = active;
        }
        catch
        {
            textured = new Texture2D(1, 1);
            textured.SetPixel(0, 0, Color.white);
            return textured;
        }
        return textured;
    }

    public void setDayLight(DayLight val)
    {
        dayLight = val;
        if (dayLight == DayLight.Night)
        {
            var obj2 = (GameObject) Instantiate(Resources.Load("flashlight"));
            obj2.transform.parent = transform;
            obj2.transform.position = transform.position;
            obj2.transform.rotation = Quaternion.Euler(353f, 0f, 0f);
            RenderSettings.ambientLight = FengColor.nightAmbientLight;
            GGM.Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = FengColor.nightLight;
            gameObject.GetComponent<Skybox>().material = skyBoxNIGHT;
        }
        if (dayLight == DayLight.Day)
        {
            RenderSettings.ambientLight = FengColor.dayAmbientLight;
            GGM.Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = FengColor.dayLight;
            gameObject.GetComponent<Skybox>().material = skyBoxDAY;
        }
        if (dayLight == DayLight.Dawn)
        {
            RenderSettings.ambientLight = FengColor.dawnAmbientLight;
            GGM.Caching.GameObjectCache.Find("mainLight").GetComponent<Light>().color = FengColor.dawnAmbientLight;
            gameObject.GetComponent<Skybox>().material = skyBoxDAWN;
        }
        snapShotCamera.gameObject.GetComponent<Skybox>().material = gameObject.GetComponent<Skybox>().material;
    }

    public void setHUDposition()
    {
        GGM.Caching.GameObjectCache.Find("Flare").transform.localPosition = new Vector3((int) (-Screen.width * 0.5f) + 14, (int) (-Screen.height * 0.5f), 0f);
        var obj2 = GGM.Caching.GameObjectCache.Find("LabelInfoBottomRight");
        obj2.transform.localPosition = new Vector3((int) (Screen.width * 0.5f), (int) (-Screen.height * 0.5f), 0f);
        GGM.Labels.BottomRight = "Pause : " + GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().inputString[InputCode.pause] + " ";
        GGM.Caching.GameObjectCache.Find("LabelInfoTopCenter").transform.localPosition = new Vector3(0f, (int) (Screen.height * 0.5f), 0f);
        GGM.Caching.GameObjectCache.Find("LabelInfoTopRight").transform.localPosition = new Vector3((int) (Screen.width * 0.5f), (int) (Screen.height * 0.5f), 0f);
        GGM.Caching.GameObjectCache.Find("LabelNetworkStatus").transform.localPosition = new Vector3((int) (-Screen.width * 0.5f), (int) (Screen.height * 0.5f), 0f);
        GGM.Caching.GameObjectCache.Find("LabelInfoTopLeft").transform.localPosition = new Vector3((int) (-Screen.width * 0.5f), (int) (Screen.height * 0.5f - 20f), 0f);
        GGM.Caching.GameObjectCache.Find("Chatroom").transform.localPosition = new Vector3((int) (-Screen.width * 0.5f), (int) (-Screen.height * 0.5f), 0f);
        if (GGM.Caching.GameObjectCache.Find("Chatroom") != null)
        {
            GGM.Caching.GameObjectCache.Find("Chatroom").GetComponent<InRoomChat>().SetPosition();
        }
        if (!usingTitan || gametype == GAMETYPE.SINGLE)
        {
            GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition = new Vector3(0f, (int) (-Screen.height * 0.5f + 5f), 0f);
            GGM.Caching.GameObjectCache.Find("GasUI").transform.localPosition = GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition;
            GGM.Caching.GameObjectCache.Find("stamina_titan").transform.localPosition = new Vector3(0f, 9999f, 0f);
            GGM.Caching.GameObjectCache.Find("stamina_titan_bottom").transform.localPosition = new Vector3(0f, 9999f, 0f);
        }
        else
        {
            var vector = new Vector3(0f, 9999f, 0f);
            GGM.Caching.GameObjectCache.Find("skill_cd_bottom").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_armin").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_eren").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_jean").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_levi").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_marco").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_mikasa").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_petra").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("skill_cd_sasha").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("GasUI").transform.localPosition = vector;
            GGM.Caching.GameObjectCache.Find("stamina_titan").transform.localPosition = new Vector3(-160f, (int) (-Screen.height * 0.5f + 15f), 0f);
            GGM.Caching.GameObjectCache.Find("stamina_titan_bottom").transform.localPosition = new Vector3(-160f, (int) (-Screen.height * 0.5f + 15f), 0f);
        }
        if (main_object != null && main_object.GetComponent<HERO>() != null)
        {
            if (gametype == GAMETYPE.SINGLE)
            {
                main_object.GetComponent<HERO>().setSkillHUDPosition();
            }
            else if (main_object.GetPhotonView() != null && main_object.GetPhotonView().isMine)
            {
                main_object.GetComponent<HERO>().setSkillHUDPosition();
            }
        }
        if (stereoType == STEREO_3D_TYPE.SIDE_BY_SIDE)
        {
            gameObject.GetComponent<Camera>().aspect = Screen.width / Screen.height;
        }
        createSnapShotRT();
    }

    public GameObject setMainObject(GameObject obj,  bool resetRotation = true, bool lockAngle = false)
    {
        main_object = obj;
        if (obj == null)
        {
            head = null;
            distanceMulti = heightMulti = 1f;
        }
        else if (main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            head = main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
            distanceMulti = head != null ? Vector3.Distance(head.transform.position, main_object.transform.position) * 0.2f : 1f;
            heightMulti = head != null ? Vector3.Distance(head.transform.position, main_object.transform.position) * 0.33f : 1f;
            if (resetRotation)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else if (main_object.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            head = main_object.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
            distanceMulti = heightMulti = 0.64f;
            if (resetRotation)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            head = null;
            distanceMulti = heightMulti = 1f;
            if (resetRotation)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        this.lockAngle = lockAngle;
        return obj;
    }

    public GameObject setMainObjectASTITAN(GameObject obj)
    {
        main_object = obj;
        if (main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head") != null)
        {
            head = main_object.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
            distanceMulti = head != null ? Vector3.Distance(head.transform.position, main_object.transform.position) * 0.4f : 1f;
            heightMulti = head != null ? Vector3.Distance(head.transform.position, main_object.transform.position) * 0.45f : 1f;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        return obj;
    }

    public void setSpectorMode(bool val)
    {
        spectatorMode = val;
        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<SpectatorMovement>().disable = !val;
        GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<MouseLook>().disable = !val;
    }

    private void shakeUpdate()
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
            if (flip)
            {
                var transform = gameObject.transform;
                transform.position += Vector3.up * R;
            }
            else
            {
                var transform2 = gameObject.transform;
                transform2.position -= Vector3.up * R;
            }
            flip = !flip;
            R *= decay;
        }
    }

    public void snapShot2(int index)
    {
        Vector3 vector;
        RaycastHit hit;
        snapShotCamera.transform.position = head == null ? main_object.transform.position : head.transform.position;
        var transform = snapShotCamera.transform;
        transform.position += Vector3.up * heightMulti;
        var transform2 = snapShotCamera.transform;
        transform2.position -= Vector3.up * 1.1f;
        var worldPosition = vector = snapShotCamera.transform.position;
        var vector3 = (worldPosition + snapShotTargetPosition) * 0.5f;
        snapShotCamera.transform.position = vector3;
        worldPosition = vector3;
        snapShotCamera.transform.LookAt(snapShotTargetPosition);
        if (index == 3)
        {
            snapShotCamera.transform.RotateAround(this.transform.position, Vector3.up, Random.Range(-180f, 180f));
        }
        else
        {
            snapShotCamera.transform.RotateAround(this.transform.position, Vector3.up, Random.Range(-20f, 20f));
        }
        snapShotCamera.transform.LookAt(worldPosition);
        snapShotCamera.transform.RotateAround(worldPosition, this.transform.right, Random.Range(-20f, 20f));
        var num = Vector3.Distance(snapShotTargetPosition, vector);
        if (snapShotTarget != null && snapShotTarget.GetComponent<TITAN>() != null)
        {
            num += (index - 1) * snapShotTarget.transform.localScale.x * 10f;
        }
        var transform3 = snapShotCamera.transform;
        transform3.position -= snapShotCamera.transform.forward * Random.Range(num + 3f, num + 10f);
        snapShotCamera.transform.LookAt(worldPosition);
        snapShotCamera.transform.RotateAround(worldPosition, this.transform.forward, Random.Range(-30f, 30f));
        var end = head == null ? main_object.transform.position : head.transform.position;
        var vector5 = (head == null ? main_object.transform.position : head.transform.position) - snapShotCamera.transform.position;
        end -= vector5;
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask | mask2;
        if (head != null)
        {
            if (Physics.Linecast(head.transform.position, end, out hit, mask))
            {
                snapShotCamera.transform.position = hit.point;
            }
            else if (Physics.Linecast(head.transform.position - vector5 * distanceMulti * 3f, end, out hit, mask3))
            {
                snapShotCamera.transform.position = hit.point;
            }
        }
        else if (Physics.Linecast(main_object.transform.position + Vector3.up, end, out hit, mask3))
        {
            snapShotCamera.transform.position = hit.point;
        }
        switch (index)
        {
            case 1:
                snapshot1 = RTImage(snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(snapshot1, snapShotDmg);
                break;

            case 2:
                snapshot2 = RTImage(snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(snapshot2, snapShotDmg);
                break;

            case 3:
                snapshot3 = RTImage(snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(snapshot3, snapShotDmg);
                break;
        }
        snapShotCount = index;
        hasSnapShot = true;
        snapShotCountDown = 2f;
        if (index == 1)
        {
            GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = snapshot1;
            GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.localScale = new Vector3(Screen.width * 0.4f, Screen.height * 0.4f, 1f);
            GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.localPosition = new Vector3(-Screen.width * 0.225f, Screen.height * 0.225f, 0f);
            GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.rotation = Quaternion.Euler(0f, 0f, 10f);
            if (PlayerPrefs.HasKey("showSSInGame") && PlayerPrefs.GetInt("showSSInGame") == 1)
            {
                GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = true;
            }
            else
            {
                GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = false;
            }
        }
    }

    public void snapShotUpdate()
    {
        if (startSnapShotFrameCount)
        {
            snapShotStartCountDownTime -= Time.deltaTime;
            if (snapShotStartCountDownTime <= 0f)
            {
                snapShot2(1);
                startSnapShotFrameCount = false;
            }
        }
        if (hasSnapShot)
        {
            snapShotCountDown -= Time.deltaTime;
            if (snapShotCountDown <= 0f)
            {
                GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = false;
                hasSnapShot = false;
                snapShotCountDown = 0f;
            }
            else if (snapShotCountDown < 1f)
            {
                GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = snapshot3;
            }
            else if (snapShotCountDown < 1.5f)
            {
                GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = snapshot2;
            }
            if (snapShotCount < 3)
            {
                snapShotInterval -= Time.deltaTime;
                if (snapShotInterval <= 0f)
                {
                    snapShotInterval = 0.05f;
                    snapShotCount++;
                    snapShot2(snapShotCount);
                }
            }
        }
    }

    private void Start()
    {
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().addCamera(this);
        isPausing = false;
        sensitivityMulti = PlayerPrefs.GetFloat("MouseSensitivity");
        invertY = PlayerPrefs.GetInt("invertMouseY");
        inputManager = GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>();
        setDayLight(dayLight);
        locker = GGM.Caching.GameObjectCache.Find("locker");
        if (PlayerPrefs.HasKey("cameraTilt"))
        {
            cameraTilt = PlayerPrefs.GetInt("cameraTilt");
        }
        else
        {
            cameraTilt = 1;
        }
        if (PlayerPrefs.HasKey("cameraDistance"))
        {
            cameraDistance = PlayerPrefs.GetFloat("cameraDistance") + 0.3f;
        }
        createSnapShotRT();
    }

    public void startShake(float R, float duration, float decay = 0.95f)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
            this.decay = decay;
        }
    }

    public void startSnapShot(Vector3 p, int dmg, GameObject target, float startTime)
    {
        int num;
        if (int.TryParse((string) FengGameManagerMKII.settings[95], out num))
        {
            if (dmg >= num)
            {
                snapShotCount = 1;
                startSnapShotFrameCount = true;
                snapShotTargetPosition = p;
                snapShotTarget = target;
                snapShotStartCountDownTime = startTime;
                snapShotInterval = 0.05f + Random.Range(0f, 0.03f);
                snapShotDmg = dmg;
            }
        }
        else
        {
            snapShotCount = 1;
            startSnapShotFrameCount = true;
            snapShotTargetPosition = p;
            snapShotTarget = target;
            snapShotStartCountDownTime = startTime;
            snapShotInterval = 0.05f + Random.Range(0f, 0.03f);
            snapShotDmg = dmg;
        }
    }

   
    public void update()
    {
        if (flashDuration > 0f)
        {
            flashDuration -= Time.deltaTime;
            if (flashDuration <= 0f)
            {
                flashDuration = 0f;
            }
            GGM.Caching.GameObjectCache.Find("flash").GetComponent<UISprite>().alpha = flashDuration * 0.5f;
        }
        if (gametype == GAMETYPE.STOP)
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
        }
        else
        {
            if (gametype != GAMETYPE.SINGLE && gameOver)
            {
                if (inputManager.isInputDown[InputCode.attack1])
                {
                    if (spectatorMode)
                    {
                        setSpectorMode(false);
                    }
                    else
                    {
                        setSpectorMode(true);
                    }
                }
                if (inputManager.isInputDown[InputCode.flare1])
                {
                    currentPeekPlayerIndex++;
                    var length = GameObject.FindGameObjectsWithTag("Player").Length;
                    if (currentPeekPlayerIndex >= length)
                    {
                        currentPeekPlayerIndex = 0;
                    }
                    if (length > 0)
                    {
                        setMainObject(GameObject.FindGameObjectsWithTag("Player")[currentPeekPlayerIndex]);
                        setSpectorMode(false);
                        lockAngle = false;
                    }
                }
                if (inputManager.isInputDown[InputCode.flare2])
                {
                    currentPeekPlayerIndex--;
                    var num2 = GameObject.FindGameObjectsWithTag("Player").Length;
                    if (currentPeekPlayerIndex >= num2)
                    {
                        currentPeekPlayerIndex = 0;
                    }
                    if (currentPeekPlayerIndex < 0)
                    {
                        currentPeekPlayerIndex = num2;
                    }
                    if (num2 > 0)
                    {
                        setMainObject(GameObject.FindGameObjectsWithTag("Player")[currentPeekPlayerIndex]);
                        setSpectorMode(false);
                        lockAngle = false;
                    }
                }
                if (spectatorMode)
                {
                    return;
                }
            }
            if (inputManager.isInputDown[InputCode.pause])
            {
                if (isPausing)
                {
                    if (main_object != null)
                    {
                        var position = transform.position;
                        position = head == null ? main_object.transform.position : head.transform.position;
                        position += Vector3.up * heightMulti;
                        transform.position = Vector3.Lerp(transform.position, position - transform.forward * 5f, 0.2f);
                    }
                    return;
                }
                isPausing = !isPausing;
                if (isPausing)
                {
                    if (gametype == GAMETYPE.SINGLE)
                    {
                        Time.timeScale = 0f;
                    }
                    GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = true;
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                }

                GGM.GUI.Pages.Page = isPausing ? "Menu" : string.Empty;
            }
            if (needSetHUD)
            {
                needSetHUD = false;
                setHUDposition();
                Screen.lockCursor = !Screen.lockCursor;
                Screen.lockCursor = !Screen.lockCursor;
            }
            if (inputManager.isInputDown[InputCode.fullscreen])
            {
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    Screen.SetResolution(960, 600, false);
                }
                else
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                }
                needSetHUD = true;
                Minimap.OnScreenResolutionChanged();
            }
            if (inputManager.isInputDown[InputCode.restart])
            {
                reset();
            }
            if (main_object != null)
            {
                RaycastHit hit;
                if (inputManager.isInputDown[InputCode.camera])
                {
                    if (cameraMode == CAMERA_TYPE.ORIGINAL)
                    {
                        cameraMode = CAMERA_TYPE.WOW;
                        Screen.lockCursor = false;
                    }
                    else if (cameraMode == CAMERA_TYPE.WOW)
                    {
                        cameraMode = CAMERA_TYPE.TPS;
                        Screen.lockCursor = true;
                    }
                    else if (cameraMode == CAMERA_TYPE.TPS)
                    {
                        cameraMode = CAMERA_TYPE.ORIGINAL;
                        Screen.lockCursor = false;
                    }
                    if ((int) FengGameManagerMKII.settings[245] == 1 || main_object.GetComponent<HERO>() == null)
                    {
                        Screen.showCursor = false;
                    }
                }
                if (inputManager.isInputDown[InputCode.hideCursor])
                {
                    Screen.showCursor = !Screen.showCursor;
                }
                if (inputManager.isInputDown[InputCode.focus])
                {
                    triggerAutoLock = !triggerAutoLock;
                    if (triggerAutoLock)
                    {
                        lockTarget = findNearestTitan();
                        if (closestDistance >= 150f)
                        {
                            lockTarget = null;
                            triggerAutoLock = false;
                        }
                    }
                }
                if (gameOver && main_object != null)
                {
                    if (FengGameManagerMKII.inputRC.isInputHumanDown(InputCodeRC.liveCam))
                    {
                        if ((int) FengGameManagerMKII.settings[263] == 0)
                        {
                            FengGameManagerMKII.settings[263] = 1;
                        }
                        else
                        {
                            FengGameManagerMKII.settings[263] = 0;
                        }
                    }
                    var component = main_object.GetComponent<HERO>();
                    if (component != null && (int) FengGameManagerMKII.settings[263] == 1 && component.GetComponent<SmoothSyncMovement>().enabled && component.isPhotonCamera)
                    {
                        CameraMovementLive(component);
                    }
                    else if (lockAngle)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, main_object.transform.rotation, 0.2f);
                        transform.position = Vector3.Lerp(transform.position, main_object.transform.position - main_object.transform.forward * 5f, 0.2f);
                    }
                    else
                    {
                        camareMovement();
                    }
                }
                else
                {
                    camareMovement();
                }
                if (triggerAutoLock && lockTarget != null)
                {
                    var z = this.transform.eulerAngles.z;
                    var transform = lockTarget.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                    var vector2 = transform.position - (head == null ? main_object.transform.position : head.transform.position);
                    vector2.Normalize();
                    lockCameraPosition = head == null ? main_object.transform.position : head.transform.position;
                    lockCameraPosition -= vector2 * distance * distanceMulti * distanceOffsetMulti;
                    lockCameraPosition += Vector3.up * 3f * heightMulti * distanceOffsetMulti;
                    this.transform.position = Vector3.Lerp(this.transform.position, lockCameraPosition, Time.deltaTime * 4f);
                    if (head != null)
                    {
                        this.transform.LookAt(head.transform.position * 0.8f + transform.position * 0.2f);
                    }
                    else
                    {
                        this.transform.LookAt(main_object.transform.position * 0.8f + transform.position * 0.2f);
                    }
                    this.transform.localEulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, z);
                    Vector2 vector3 = camera.WorldToScreenPoint(transform.position - transform.forward * lockTarget.transform.localScale.x);
                    locker.transform.localPosition = new Vector3(vector3.x - Screen.width * 0.5f, vector3.y - Screen.height * 0.5f, 0f);
                    if (lockTarget.GetComponent<TITAN>() != null && lockTarget.GetComponent<TITAN>().hasDie)
                    {
                        lockTarget = null;
                    }
                }
                else
                {
                    locker.transform.localPosition = new Vector3(0f, -Screen.height * 0.5f - 50f, 0f);
                }
                var end = head == null ? main_object.transform.position : head.transform.position;
                var vector5 = (head == null ? main_object.transform.position : head.transform.position) - this.transform.position;
                var normalized = vector5.normalized;
                end -= distance * normalized * distanceMulti;
                LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
                LayerMask mask3 = mask | mask2;
                if (head != null)
                {
                    if (Physics.Linecast(head.transform.position, end, out hit, mask))
                    {
                        transform.position = hit.point;
                    }
                    else if (Physics.Linecast(head.transform.position - normalized * distanceMulti * 3f, end, out hit, mask2))
                    {
                        transform.position = hit.point;
                    }
                    Debug.DrawLine(head.transform.position - normalized * distanceMulti * 3f, end, Color.red);
                }
                else if (Physics.Linecast(main_object.transform.position + Vector3.up, end, out hit, mask3))
                {
                    transform.position = hit.point;
                }
                shakeUpdate();
            }
        }
    }

    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
}

