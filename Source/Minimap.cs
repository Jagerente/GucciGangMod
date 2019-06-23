using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Minimap : MonoBehaviour
{
    private bool assetsInitialized;
    private static UnityEngine.Sprite borderSprite;
    private RectTransform borderT;
    private Canvas canvas;
    private Vector2 cornerPosition;
    private float cornerSizeRatio;
    private Preset initialPreset;
    public static Minimap instance;
    private bool isEnabled;
    private bool isEnabledTemp;
    private Vector3 lastMinimapCenter;
    private float lastMinimapOrthoSize;
    private Camera lastUsedCamera;
    private bool maximized;
    private RectTransform minimap;
    private float MINIMAP_CORNER_SIZE;
    private Vector2 MINIMAP_ICON_SIZE;
    private float MINIMAP_POINTER_DIST;
    private float MINIMAP_POINTER_SIZE;
    private int MINIMAP_SIZE;
    private Vector2 MINIMAP_SUPPLY_SIZE;
    private MinimapIcon[] minimapIcons;
    private bool minimapIsCreated;
    private RectTransform minimapMaskT;
    private Bounds minimapOrthographicBounds;
    public RenderTexture minimapRT;
    public Camera myCam;
    private static UnityEngine.Sprite pointerSprite;
    private CanvasScaler scaler;
    private static UnityEngine.Sprite supplySprite;
    private static UnityEngine.Sprite whiteIconSprite;

    private void AddBorderToTexture(ref Texture2D texture, Color borderColor, int borderPixelSize)
    {
        var num = texture.width * borderPixelSize;
        var colors = new Color[num];
        for (var i = 0; i < num; i++)
        {
            colors[i] = borderColor;
        }
        texture.SetPixels(0, texture.height - borderPixelSize, texture.width - 1, borderPixelSize, colors);
        texture.SetPixels(0, 0, texture.width, borderPixelSize, colors);
        texture.SetPixels(0, 0, borderPixelSize, texture.height, colors);
        texture.SetPixels(texture.width - borderPixelSize, 0, borderPixelSize, texture.height, colors);
        texture.Apply();
    }

    private void AutomaticSetCameraProperties(Camera cam)
    {
        var rendererArray = FindObjectsOfType<Renderer>();
        if (rendererArray.Length > 0)
        {
            minimapOrthographicBounds = new Bounds(rendererArray[0].transform.position, Vector3.zero);
            for (var i = 0; i < rendererArray.Length; i++)
            {
                if (rendererArray[i].gameObject.layer == 9)
                {
                    minimapOrthographicBounds.Encapsulate(rendererArray[i].bounds);
                }
            }
        }
        var size = minimapOrthographicBounds.size;
        var num2 = size.x > size.z ? size.x : size.z;
        size.z = size.x = num2;
        minimapOrthographicBounds.size = size;
        cam.orthographic = true;
        cam.orthographicSize = num2 * 0.5f;
        var center = minimapOrthographicBounds.center;
        center.y = cam.farClipPlane * 0.5f;
        var transform = cam.transform;
        transform.position = center;
        transform.eulerAngles = new Vector3(90f, 0f, 0f);
        cam.aspect = 1f;
        lastMinimapCenter = center;
        lastMinimapOrthoSize = cam.orthographicSize;
    }

    private void AutomaticSetOrthoBounds()
    {
        var rendererArray = FindObjectsOfType<Renderer>();
        if (rendererArray.Length > 0)
        {
            minimapOrthographicBounds = new Bounds(rendererArray[0].transform.position, Vector3.zero);
            for (var i = 0; i < rendererArray.Length; i++)
            {
                minimapOrthographicBounds.Encapsulate(rendererArray[i].bounds);
            }
        }
        var size = minimapOrthographicBounds.size;
        var num2 = size.x > size.z ? size.x : size.z;
        size.z = size.x = num2;
        minimapOrthographicBounds.size = size;
        lastMinimapCenter = minimapOrthographicBounds.center;
        lastMinimapOrthoSize = num2 * 0.5f;
    }

    private void Awake()
    {
        instance = this;
    }

    private Texture2D CaptureMinimap(Camera cam)
    {
        var active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        var textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false) {
            filterMode = FilterMode.Bilinear
        };
        textured.ReadPixels(new Rect(0f, 0f, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        textured.Apply();
        RenderTexture.active = active;
        return textured;
    }

    private void CaptureMinimapRT(Camera cam)
    {
        var active = RenderTexture.active;
        RenderTexture.active = minimapRT;
        cam.targetTexture = minimapRT;
        cam.Render();
        RenderTexture.active = active;
    }

    private void CheckUserInput()
    {
        if ((int) FengGameManagerMKII.settings[0xe7] == 1 && RCSettings.globalDisableMinimap == 0)
        {
            if (minimapIsCreated)
            {
                if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.mapMaximize))
                {
                    if (!maximized)
                    {
                        Maximize();
                    }
                }
                else if (maximized)
                {
                    Minimize();
                }
                if (FengGameManagerMKII.inputRC.isInputHumanDown(InputCodeRC.mapToggle))
                {
                    SetEnabled(!isEnabled);
                }
                if (maximized)
                {
                    var flag2 = false;
                    if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.mapReset))
                    {
                        if (initialPreset != null)
                        {
                            ManualSetCameraProperties(lastUsedCamera, initialPreset.center, initialPreset.orthographicSize);
                        }
                        else
                        {
                            AutomaticSetCameraProperties(lastUsedCamera);
                        }
                        flag2 = true;
                    }
                    else
                    {
                        float num2;
                        var axis = Input.GetAxis("Mouse ScrollWheel");
                        if (axis != 0f)
                        {
                            if (Input.GetKey(KeyCode.LeftShift))
                            {
                                axis *= 3f;
                            }
                            lastMinimapOrthoSize = Mathf.Max(lastMinimapOrthoSize + axis, 1f);
                            flag2 = true;
                        }
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            num2 = Time.deltaTime * ((Input.GetKey(KeyCode.LeftShift) ? 2f : 0.75f) * lastMinimapOrthoSize);
                            lastMinimapCenter.z += num2;
                            flag2 = true;
                        }
                        else if (Input.GetKey(KeyCode.DownArrow))
                        {
                            num2 = Time.deltaTime * ((Input.GetKey(KeyCode.LeftShift) ? 2f : 0.75f) * lastMinimapOrthoSize);
                            lastMinimapCenter.z -= num2;
                            flag2 = true;
                        }
                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            num2 = Time.deltaTime * ((Input.GetKey(KeyCode.LeftShift) ? 2f : 0.75f) * lastMinimapOrthoSize);
                            lastMinimapCenter.x += num2;
                            flag2 = true;
                        }
                        else if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            num2 = Time.deltaTime * ((Input.GetKey(KeyCode.LeftShift) ? 2f : 0.75f) * lastMinimapOrthoSize);
                            lastMinimapCenter.x -= num2;
                            flag2 = true;
                        }
                    }
                    if (flag2)
                    {
                        RecaptureMinimap(lastUsedCamera, lastMinimapCenter, lastMinimapOrthoSize);
                    }
                }
            }
        }
        else if (isEnabled)
        {
            SetEnabled(!isEnabled);
        }
    }

    public void CreateMinimap(Camera cam, int minimapResolution = 0x200, float cornerSize = 0.3f, Preset mapPreset = null)
    {
        isEnabled = true;
        lastUsedCamera = cam;
        if (!assetsInitialized)
        {
            Initialize();
        }
        var obj2 = GGM.Caching.GameObjectCache.Find("mainLight");
        Light component = null;
        var identity = Quaternion.identity;
        var none = LightShadows.None;
        var clear = Color.clear;
        var intensity = 0f;
        var nearClipPlane = cam.nearClipPlane;
        var farClipPlane = cam.farClipPlane;
        var cullingMask = cam.cullingMask;
        if (obj2 != null)
        {
            component = obj2.GetComponent<Light>();
            identity = component.transform.rotation;
            none = component.shadows;
            intensity = component.intensity;
            clear = component.color;
            component.shadows = LightShadows.None;
            component.color = Color.white;
            component.intensity = 0.5f;
            component.transform.eulerAngles = new Vector3(90f, 0f, 0f);
        }
        cam.nearClipPlane = 0.3f;
        cam.farClipPlane = 1000f;
        cam.cullingMask = 0x200;
        cam.clearFlags = CameraClearFlags.Color;
        MINIMAP_SIZE = minimapResolution;
        MINIMAP_CORNER_SIZE = MINIMAP_SIZE * cornerSize;
        cornerSizeRatio = cornerSize;
        CreateMinimapRT(cam, minimapResolution);
        if (mapPreset != null)
        {
            initialPreset = mapPreset;
            ManualSetCameraProperties(cam, mapPreset.center, mapPreset.orthographicSize);
        }
        else
        {
            AutomaticSetCameraProperties(cam);
        }
        CaptureMinimapRT(cam);
        if (obj2 != null)
        {
            component.shadows = none;
            component.transform.rotation = identity;
            component.color = clear;
            component.intensity = intensity;
        }
        cam.nearClipPlane = nearClipPlane;
        cam.farClipPlane = farClipPlane;
        cam.cullingMask = cullingMask;
        cam.orthographic = false;
        cam.clearFlags = CameraClearFlags.Skybox;
        CreateUnityUIRT(minimapResolution);
        minimapIsCreated = true;
        StartCoroutine(HackRoutine());
    }

    private void CreateMinimapRT(Camera cam, int pixelSize)
    {
        if (minimapRT == null)
        {
            var flag2 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGB565);
            var format = flag2 ? RenderTextureFormat.RGB565 : RenderTextureFormat.Default;
            minimapRT = new RenderTexture(pixelSize, pixelSize, 0x10, RenderTextureFormat.RGB565);
            if (!flag2)
            {
                Debug.Log(SystemInfo.graphicsDeviceName + " (" + SystemInfo.graphicsDeviceVendor + ") does not support RGB565 format, the minimap will have transparency issues on certain maps");
            }
        }
        cam.targetTexture = minimapRT;
    }

    private void CreateUnityUI(Texture2D map, int minimapResolution)
    {
        var obj2 = new GameObject("Canvas");
        obj2.AddComponent<RectTransform>();
        canvas = obj2.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        scaler = obj2.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(900f, 600f);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        var obj3 = new GameObject("CircleMask");
        obj3.transform.SetParent(obj2.transform, false);
        minimapMaskT = obj3.AddComponent<RectTransform>();
        obj3.AddComponent<CanvasRenderer>();
        minimapMaskT.anchorMin = minimapMaskT.anchorMax = Vector2.one;
        var num = MINIMAP_CORNER_SIZE * 0.5f;
        cornerPosition = new Vector2(-(num + 5f), -(num + 70f));
        minimapMaskT.anchoredPosition = cornerPosition;
        minimapMaskT.sizeDelta = new Vector2(MINIMAP_CORNER_SIZE, MINIMAP_CORNER_SIZE);
        var obj4 = new GameObject("Minimap");
        obj4.transform.SetParent(minimapMaskT, false);
        minimap = obj4.AddComponent<RectTransform>();
        obj4.AddComponent<CanvasRenderer>();
        minimap.anchorMin = minimap.anchorMax = new Vector2(0.5f, 0.5f);
        minimap.anchoredPosition = Vector2.zero;
        minimap.sizeDelta = minimapMaskT.sizeDelta;
        var image = obj4.AddComponent<Image>();
        var rect = new Rect(0f, 0f, map.width, map.height);
        image.sprite = UnityEngine.Sprite.Create(map, rect, new Vector3(0.5f, 0.5f));
        image.type = Image.Type.Simple;
    }

    private void CreateUnityUIRT(int minimapResolution)
    {
        var obj2 = new GameObject("Canvas");
        obj2.AddComponent<RectTransform>();
        canvas = obj2.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        scaler = obj2.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(800f, 600f);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 1f;
        var obj3 = new GameObject("Mask");
        obj3.transform.SetParent(obj2.transform, false);
        minimapMaskT = obj3.AddComponent<RectTransform>();
        obj3.AddComponent<CanvasRenderer>();
        minimapMaskT.anchorMin = minimapMaskT.anchorMax = Vector2.one;
        var num = MINIMAP_CORNER_SIZE * 0.5f;
        cornerPosition = new Vector2(-(num + 5f), -(num + 70f));
        minimapMaskT.anchoredPosition = cornerPosition;
        minimapMaskT.sizeDelta = new Vector2(MINIMAP_CORNER_SIZE, MINIMAP_CORNER_SIZE);
        var obj4 = new GameObject("MapBorder");
        obj4.transform.SetParent(minimapMaskT, false);
        borderT = obj4.AddComponent<RectTransform>();
        borderT.anchorMin = borderT.anchorMax = new Vector2(0.5f, 0.5f);
        borderT.sizeDelta = minimapMaskT.sizeDelta;
        obj4.AddComponent<CanvasRenderer>();
        var image = obj4.AddComponent<Image>();
        image.sprite = borderSprite;
        image.type = Image.Type.Sliced;
        var obj5 = new GameObject("Minimap");
        obj5.transform.SetParent(minimapMaskT, false);
        minimap = obj5.AddComponent<RectTransform>();
        minimap.SetAsFirstSibling();
        obj5.AddComponent<CanvasRenderer>();
        minimap.anchorMin = minimap.anchorMax = new Vector2(0.5f, 0.5f);
        minimap.anchoredPosition = Vector2.zero;
        minimap.sizeDelta = minimapMaskT.sizeDelta;
        var image2 = obj5.AddComponent<RawImage>();
        image2.texture = minimapRT;
        image2.maskable = true;
        obj5.AddComponent<Mask>().showMaskGraphic = true;
    }

    private Vector2 GetSizeForStyle(IconStyle style)
    {
        if (style == IconStyle.CIRCLE)
        {
            return MINIMAP_ICON_SIZE;
        }
        if (style == IconStyle.SUPPLY)
        {
            return MINIMAP_SUPPLY_SIZE;
        }
        return Vector2.zero;
    }

    private static UnityEngine.Sprite GetSpriteForStyle(IconStyle style)
    {
        if (style == IconStyle.CIRCLE)
        {
            return whiteIconSprite;
        }
        if (style == IconStyle.SUPPLY)
        {
            return supplySprite;
        }
        return null;
    }

    private IEnumerator HackRoutine()
    {
        yield return new WaitForEndOfFrame();
        RecaptureMinimap(lastUsedCamera, lastMinimapCenter, lastMinimapOrthoSize);
    }

    private void Initialize()
    {
        var pivot = new Vector3(0.5f, 0.5f);
        var texture = (Texture2D) GGM.Caching.ResourcesCache.RCLoadT2D("icon");
        var rect = new Rect(0f, 0f, texture.width, texture.height);
        whiteIconSprite = UnityEngine.Sprite.Create(texture, rect, pivot);
        texture = (Texture2D) GGM.Caching.ResourcesCache.RCLoadT2D("iconpointer");
        rect = new Rect(0f, 0f, texture.width, texture.height);
        pointerSprite = UnityEngine.Sprite.Create(texture, rect, pivot);
        texture = (Texture2D) GGM.Caching.ResourcesCache.RCLoadT2D("supplyicon");
        rect = new Rect(0f, 0f, texture.width, texture.height);
        supplySprite = UnityEngine.Sprite.Create(texture, rect, pivot);
        texture = (Texture2D) GGM.Caching.ResourcesCache.RCLoadT2D("mapborder");
        rect = new Rect(0f, 0f, texture.width, texture.height);
        var border = new Vector4(5f, 5f, 5f, 5f);
        borderSprite = UnityEngine.Sprite.Create(texture, rect, pivot, 100f, 1, SpriteMeshType.FullRect, border);
        MINIMAP_ICON_SIZE = new Vector2(whiteIconSprite.texture.width, whiteIconSprite.texture.height);
        MINIMAP_POINTER_SIZE = (pointerSprite.texture.width + pointerSprite.texture.height) / 2f;
        MINIMAP_POINTER_DIST = (MINIMAP_ICON_SIZE.x + MINIMAP_ICON_SIZE.y) * 0.25f;
        MINIMAP_SUPPLY_SIZE = new Vector2(supplySprite.texture.width, supplySprite.texture.height);
        assetsInitialized = true;
    }

    private void ManualSetCameraProperties(Camera cam, Vector3 centerPoint, float orthoSize)
    {
        var transform = cam.transform;
        centerPoint.y = cam.farClipPlane * 0.5f;
        transform.position = centerPoint;
        transform.eulerAngles = new Vector3(90f, 0f, 0f);
        cam.orthographic = true;
        cam.orthographicSize = orthoSize;
        var x = orthoSize * 2f;
        minimapOrthographicBounds = new Bounds(centerPoint, new Vector3(x, 0f, x));
        lastMinimapCenter = centerPoint;
        lastMinimapOrthoSize = orthoSize;
    }

    private void ManualSetOrthoBounds(Vector3 centerPoint, float orthoSize)
    {
        var x = orthoSize * 2f;
        minimapOrthographicBounds = new Bounds(centerPoint, new Vector3(x, 0f, x));
        lastMinimapCenter = centerPoint;
        lastMinimapOrthoSize = orthoSize;
    }

    public void Maximize()
    {
        isEnabledTemp = true;
        if (!isEnabled)
        {
            SetEnabledTemp(true);
        }
        minimapMaskT.anchorMin = minimapMaskT.anchorMax = new Vector2(0.5f, 0.5f);
        minimapMaskT.anchoredPosition = Vector2.zero;
        minimapMaskT.sizeDelta = new Vector2(MINIMAP_SIZE, MINIMAP_SIZE);
        minimap.sizeDelta = minimapMaskT.sizeDelta;
        borderT.sizeDelta = minimapMaskT.sizeDelta;
        if (minimapIcons != null)
        {
            for (var i = 0; i < minimapIcons.Length; i++)
            {
                var icon = minimapIcons[i];
                if (icon != null)
                {
                    icon.SetSize(GetSizeForStyle(icon.style));
                    if (icon.rotation)
                    {
                        icon.SetPointerSize(MINIMAP_POINTER_SIZE, MINIMAP_POINTER_DIST);
                    }
                }
            }
        }
        maximized = true;
    }

    public void Minimize()
    {
        isEnabledTemp = false;
        if (!isEnabled)
        {
            SetEnabledTemp(false);
        }
        minimapMaskT.anchorMin = minimapMaskT.anchorMax = Vector2.one;
        minimapMaskT.anchoredPosition = cornerPosition;
        minimapMaskT.sizeDelta = new Vector2(MINIMAP_CORNER_SIZE, MINIMAP_CORNER_SIZE);
        minimap.sizeDelta = minimapMaskT.sizeDelta;
        borderT.sizeDelta = minimapMaskT.sizeDelta;
        if (minimapIcons != null)
        {
            var num = 1f - (MINIMAP_SIZE - MINIMAP_CORNER_SIZE) / MINIMAP_SIZE;
            var a = MINIMAP_POINTER_SIZE * num;
            a = Mathf.Max(a, MINIMAP_POINTER_SIZE * 0.5f);
            var originDistance = (MINIMAP_POINTER_SIZE - a) / MINIMAP_POINTER_SIZE;
            originDistance = MINIMAP_POINTER_DIST * originDistance;
            for (var i = 0; i < minimapIcons.Length; i++)
            {
                var icon = minimapIcons[i];
                if (icon != null)
                {
                    var sizeForStyle = GetSizeForStyle(icon.style);
                    sizeForStyle.x = Mathf.Max(sizeForStyle.x * num, sizeForStyle.x * 0.5f);
                    sizeForStyle.y = Mathf.Max(sizeForStyle.y * num, sizeForStyle.y * 0.5f);
                    icon.SetSize(sizeForStyle);
                    if (icon.rotation)
                    {
                        icon.SetPointerSize(a, originDistance);
                    }
                }
            }
        }
        maximized = false;
    }

    public static void OnScreenResolutionChanged()
    {
        if (instance != null)
        {
            var instance = Minimap.instance;
            instance.StartCoroutine(instance.ScreenResolutionChangedRoutine());
        }
    }

    private void RecaptureMinimap()
    {
        if (lastUsedCamera != null)
        {
            RecaptureMinimap(lastUsedCamera, lastMinimapCenter, lastMinimapOrthoSize);
        }
    }

    private void RecaptureMinimap(Camera cam, Vector3 centerPosition, float orthoSize)
    {
        if (minimap != null)
        {
            var obj2 = GGM.Caching.GameObjectCache.Find("mainLight");
            Light component = null;
            var identity = Quaternion.identity;
            var none = LightShadows.None;
            var clear = Color.clear;
            var intensity = 0f;
            var nearClipPlane = cam.nearClipPlane;
            var farClipPlane = cam.farClipPlane;
            var cullingMask = cam.cullingMask;
            if (obj2 != null)
            {
                component = obj2.GetComponent<Light>();
                identity = component.transform.rotation;
                none = component.shadows;
                clear = component.color;
                intensity = component.intensity;
                component.shadows = LightShadows.None;
                component.color = Color.white;
                component.intensity = 0.5f;
                component.transform.eulerAngles = new Vector3(90f, 0f, 0f);
            }
            cam.nearClipPlane = 0.3f;
            cam.farClipPlane = 1000f;
            cam.clearFlags = CameraClearFlags.Color;
            cam.cullingMask = 0x200;
            CreateMinimapRT(cam, MINIMAP_SIZE);
            ManualSetCameraProperties(cam, centerPosition, orthoSize);
            CaptureMinimapRT(cam);
            if (obj2 != null)
            {
                component.shadows = none;
                component.transform.rotation = identity;
                component.color = clear;
                component.intensity = intensity;
            }
            cam.nearClipPlane = nearClipPlane;
            cam.farClipPlane = farClipPlane;
            cam.cullingMask = cullingMask;
            cam.orthographic = false;
            cam.clearFlags = CameraClearFlags.Skybox;
        }
    }

    private IEnumerator ScreenResolutionChangedRoutine()
    {
        yield return 0;
        RecaptureMinimap();
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
        if (canvas != null)
        {
            canvas.gameObject.SetActive(enabled);
        }
    }

    public void SetEnabledTemp(bool enabled)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(enabled);
        }
    }

    public void TrackGameObjectOnMinimap(GameObject objToTrack, Color iconColor, bool trackOrientation, bool depthAboveAll = false, IconStyle iconStyle = 0)
    {
        if (minimap != null)
        {
            MinimapIcon icon;
            if (trackOrientation)
            {
                icon = MinimapIcon.CreateWithRotation(minimap, objToTrack, iconStyle, MINIMAP_POINTER_DIST);
            }
            else
            {
                icon = MinimapIcon.Create(minimap, objToTrack, iconStyle);
            }
            icon.SetColor(iconColor);
            icon.SetDepth(depthAboveAll);
            var sizeForStyle = GetSizeForStyle(iconStyle);
            if (maximized)
            {
                icon.SetSize(sizeForStyle);
                if (icon.rotation)
                {
                    icon.SetPointerSize(MINIMAP_POINTER_SIZE, MINIMAP_POINTER_DIST);
                }
            }
            else
            {
                var num = 1f - (MINIMAP_SIZE - MINIMAP_CORNER_SIZE) / MINIMAP_SIZE;
                sizeForStyle.x = Mathf.Max(sizeForStyle.x * num, sizeForStyle.x * 0.5f);
                sizeForStyle.y = Mathf.Max(sizeForStyle.y * num, sizeForStyle.y * 0.5f);
                icon.SetSize(sizeForStyle);
                if (icon.rotation)
                {
                    var a = MINIMAP_POINTER_SIZE * num;
                    a = Mathf.Max(a, MINIMAP_POINTER_SIZE * 0.5f);
                    var originDistance = (MINIMAP_POINTER_SIZE - a) / MINIMAP_POINTER_SIZE;
                    originDistance = MINIMAP_POINTER_DIST * originDistance;
                    icon.SetPointerSize(a, originDistance);
                }
            }
            if (minimapIcons == null)
            {
                minimapIcons = new[] { icon };
            }
            else
            {
                var iconArray2 = new MinimapIcon[minimapIcons.Length + 1];
                for (var i = 0; i < minimapIcons.Length; i++)
                {
                    iconArray2[i] = minimapIcons[i];
                }
                iconArray2[iconArray2.Length - 1] = icon;
                minimapIcons = iconArray2;
            }
        }
    }

    public static void TryRecaptureInstance()
    {
        if (instance != null)
        {
            instance.RecaptureMinimap();
        }
    }

    public IEnumerator TryRecaptureInstanceE(float time)
    {
        yield return new WaitForSeconds(time);
        TryRecaptureInstance();
    }

    private void Update()
    {
        CheckUserInput();
        if ((isEnabled || isEnabledTemp) && minimapIsCreated && minimapIcons != null)
        {
            for (var i = 0; i < minimapIcons.Length; i++)
            {
                var icon = minimapIcons[i];
                if (icon == null)
                {
                    RCextensions.RemoveAt(ref minimapIcons, i);
                }
                else if (!icon.UpdateUI(minimapOrthographicBounds, maximized ? MINIMAP_SIZE : MINIMAP_CORNER_SIZE))
                {
                    icon.Destroy();
                    RCextensions.RemoveAt(ref minimapIcons, i);
                }
            }
        }
    }

    public static void WaitAndTryRecaptureInstance(float time)
    {
        instance.StartCoroutine(instance.TryRecaptureInstanceE(time));
    }




    public enum IconStyle
    {
        CIRCLE,
        SUPPLY
    }

    public class MinimapIcon
    {
        private Transform obj;
        private RectTransform pointerRect;
        public readonly bool rotation;
        public readonly IconStyle style;
        private RectTransform uiRect;

        public MinimapIcon(GameObject trackedObject, GameObject uiElement, IconStyle style)
        {
            rotation = false;
            this.style = style;
            obj = trackedObject.transform;
            uiRect = uiElement.GetComponent<RectTransform>();
            var component = obj.GetComponent<CatchDestroy>();
            if (component == null)
            {
                obj.gameObject.AddComponent<CatchDestroy>().target = uiElement;
            }
            else if (component.target != null && component.target != uiElement)
            {
                Object.Destroy(component.target);
            }
            else
            {
                component.target = uiElement;
            }
        }

        public MinimapIcon(GameObject trackedObject, GameObject uiElement, GameObject uiPointer, IconStyle style)
        {
            rotation = true;
            this.style = style;
            obj = trackedObject.transform;
            uiRect = uiElement.GetComponent<RectTransform>();
            pointerRect = uiPointer.GetComponent<RectTransform>();
            var component = obj.GetComponent<CatchDestroy>();
            if (component == null)
            {
                obj.gameObject.AddComponent<CatchDestroy>().target = uiElement;
            }
            else if (component.target != null && component.target != uiElement)
            {
                Object.Destroy(component.target);
            }
            else
            {
                component.target = uiElement;
            }
        }

        public static MinimapIcon Create(RectTransform parent, GameObject trackedObject, IconStyle style)
        {
            var spriteForStyle = GetSpriteForStyle(style);
            var uiElement = new GameObject("MinimapIcon");
            var transform = uiElement.AddComponent<RectTransform>();
            transform.anchorMin = transform.anchorMax = new Vector3(0.5f, 0.5f);
            transform.sizeDelta = new Vector2(spriteForStyle.texture.width, spriteForStyle.texture.height);
            var image = uiElement.AddComponent<Image>();
            image.sprite = spriteForStyle;
            image.type = Image.Type.Simple;
            uiElement.transform.SetParent(parent, false);
            return new MinimapIcon(trackedObject, uiElement, style);
        }

        public static MinimapIcon CreateWithRotation(RectTransform parent, GameObject trackedObject, IconStyle style, float pointerDist)
        {
            var spriteForStyle = GetSpriteForStyle(style);
            var uiElement = new GameObject("MinimapIcon");
            var transform = uiElement.AddComponent<RectTransform>();
            transform.anchorMin = transform.anchorMax = new Vector3(0.5f, 0.5f);
            transform.sizeDelta = new Vector2(spriteForStyle.texture.width, spriteForStyle.texture.height);
            var image = uiElement.AddComponent<Image>();
            image.sprite = spriteForStyle;
            image.type = Image.Type.Simple;
            uiElement.transform.SetParent(parent, false);
            var uiPointer = new GameObject("IconPointer");
            var transform2 = uiPointer.AddComponent<RectTransform>();
            transform2.anchorMin = transform2.anchorMax = transform.anchorMin;
            transform2.sizeDelta = new Vector2(pointerSprite.texture.width, pointerSprite.texture.height);
            var image2 = uiPointer.AddComponent<Image>();
            image2.sprite = pointerSprite;
            image2.type = Image.Type.Simple;
            uiPointer.transform.SetParent(transform, false);
            transform2.anchoredPosition = new Vector2(0f, pointerDist);
            return new MinimapIcon(trackedObject, uiElement, uiPointer, style);
        }

        public void Destroy()
        {
            if (uiRect != null)
            {
                Object.Destroy(uiRect.gameObject);
            }
        }

        public void SetColor(Color color)
        {
            if (uiRect != null)
            {
                uiRect.GetComponent<Image>().color = color;
            }
        }

        public void SetDepth(bool aboveAll)
        {
            if (uiRect != null)
            {
                if (aboveAll)
                {
                    uiRect.SetAsLastSibling();
                }
                else
                {
                    uiRect.SetAsFirstSibling();
                }
            }
        }

        public void SetPointerSize(float size, float originDistance)
        {
            if (pointerRect != null)
            {
                pointerRect.sizeDelta = new Vector2(size, size);
                pointerRect.anchoredPosition = new Vector2(0f, originDistance);
            }
        }

        public void SetSize(Vector2 size)
        {
            if (uiRect != null)
            {
                uiRect.sizeDelta = size;
            }
        }

        public bool UpdateUI(Bounds worldBounds, float minimapSize)
        {
            if (obj == null)
            {
                return false;
            }
            var x = worldBounds.size.x;
            var vector = obj.position - worldBounds.center;
            vector.y = vector.z;
            vector.z = 0f;
            var num2 = Mathf.Abs(vector.x) / x;
            vector.x = vector.x < 0f ? -num2 : num2;
            var num3 = Mathf.Abs(vector.y) / x;
            vector.y = vector.y < 0f ? -num3 : num3;
            Vector2 vector2 = vector * minimapSize;
            uiRect.anchoredPosition = vector2;
            if (rotation)
            {
                var z = Mathf.Atan2(obj.forward.z, obj.forward.x) * 57.29578f - 90f;
                uiRect.eulerAngles = new Vector3(0f, 0f, z);
            }
            return true;
        }
    }

    public class Preset
    {
        public readonly Vector3 center;
        public readonly float orthographicSize;

        public Preset(Vector3 center, float orthographicSize)
        {
            this.center = center;
            this.orthographicSize = orthographicSize;
        }
    }
}

