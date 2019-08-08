using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), AddComponentMenu("NGUI/UI/Camera"), ExecuteInEditMode]
public class UICamera : MonoBehaviour
{
    private static Comparison<RaycastHit> f__amcache31;
    public bool allowMultiTouch = true;
    public KeyCode cancelKey0 = KeyCode.Escape;
    public KeyCode cancelKey1 = KeyCode.JoystickButton1;
    public bool clipRaycasts = true;
    public static UICamera current;
    public static Camera currentCamera;
    public static MouseOrTouch currentTouch;
    public static int currentTouchID = -1;
    public bool debug;
    public LayerMask eventReceiverMask = -1;
    public static GameObject fallThrough;
    public static GameObject genericEventHandler;
    public string horizontalAxisName = "Horizontal";
    public static GameObject hoveredObject;
    public static bool inputHasFocus;
    public static bool isDragging;
    public static RaycastHit lastHit;
    public static Vector2 lastTouchPosition = Vector2.zero;
    private Camera mCam;
    private static MouseOrTouch mController = new MouseOrTouch();
    private static List<Highlighted> mHighlighted = new List<Highlighted>();
    private static GameObject mHover;
    private bool mIsEditor;
    private static List<UICamera> mList = new List<UICamera>();
    private static MouseOrTouch[] mMouse = { new MouseOrTouch(), new MouseOrTouch(), new MouseOrTouch() };
    private static float mNextEvent;
    public float mouseClickThreshold = 10f;
    public float mouseDragThreshold = 4f;
    private static GameObject mSel;
    private GameObject mTooltip;
    private float mTooltipTime;
    private static Dictionary<int, MouseOrTouch> mTouches = new Dictionary<int, MouseOrTouch>();
    public static OnCustomInput onCustomInput;
    public float rangeDistance = -1f;
    public string scrollAxisName = "Mouse ScrollWheel";
    public static bool showTooltips = true;
    public bool stickyPress = true;
    public bool stickyTooltip = true;
    public KeyCode submitKey0 = KeyCode.Return;
    public KeyCode submitKey1 = KeyCode.JoystickButton0;
    public float tooltipDelay = 1f;
    public float touchClickThreshold = 40f;
    public float touchDragThreshold = 40f;
    public bool useController = true;
    public bool useKeyboard = true;
    public bool useMouse = true;
    public bool useTouch = true;
    public string verticalAxisName = "Vertical";

    private void Awake()
    {
        cachedCamera.eventMask = 0;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            useMouse = false;
            useTouch = true;
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                useKeyboard = false;
                useController = false;
            }
        }
        else if (Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360)
        {
            useMouse = false;
            useTouch = false;
            useKeyboard = false;
            useController = true;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
                 Application.platform == RuntimePlatform.OSXEditor)
        {
            mIsEditor = true;
        }

        mMouse[0].pos.x = Input.mousePosition.x;
        mMouse[0].pos.y = Input.mousePosition.y;
        lastTouchPosition = mMouse[0].pos;
        if (eventReceiverMask == -1)
        {
            eventReceiverMask = cachedCamera.cullingMask;
        }
    }

    private static int CompareFunc(UICamera a, UICamera b)
    {
        if (a.cachedCamera.depth < b.cachedCamera.depth)
        {
            return 1;
        }

        if (a.cachedCamera.depth > b.cachedCamera.depth)
        {
            return -1;
        }

        return 0;
    }

    public static UICamera FindCameraForLayer(int layer)
    {
        var num = 1 << layer;
        for (var i = 0; i < mList.Count; i++)
        {
            var camera = mList[i];
            var cachedCamera = camera.cachedCamera;
            if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
            {
                return camera;
            }
        }

        return null;
    }

    private void FixedUpdate()
    {
        if (useMouse && Application.isPlaying && handlesEvents)
        {
            hoveredObject = !Raycast(Input.mousePosition, ref lastHit) ? fallThrough : lastHit.collider.gameObject;
            if (hoveredObject == null)
            {
                hoveredObject = genericEventHandler;
            }

            for (var i = 0; i < 3; i++)
            {
                mMouse[i].current = hoveredObject;
            }
        }
    }

    private static int GetDirection(string axis)
    {
        var realtimeSinceStartup = Time.realtimeSinceStartup;
        if (mNextEvent < realtimeSinceStartup)
        {
            var num2 = Input.GetAxis(axis);
            if (num2 > 0.75f)
            {
                mNextEvent = realtimeSinceStartup + 0.25f;
                return 1;
            }

            if (num2 < -0.75f)
            {
                mNextEvent = realtimeSinceStartup + 0.25f;
                return -1;
            }
        }

        return 0;
    }

    private static int GetDirection(KeyCode up, KeyCode down)
    {
        if (Input.GetKeyDown(up))
        {
            return 1;
        }

        if (Input.GetKeyDown(down))
        {
            return -1;
        }

        return 0;
    }

    private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
    {
        if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
        {
            return 1;
        }

        if (!Input.GetKeyDown(down0) && !Input.GetKeyDown(down1))
        {
            return 0;
        }

        return -1;
    }

    public static MouseOrTouch GetTouch(int id)
    {
        MouseOrTouch touch = null;
        if (!mTouches.TryGetValue(id, out touch))
        {
            touch = new MouseOrTouch
            {
                touchBegan = true
            };
            mTouches.Add(id, touch);
        }

        return touch;
    }

    private static void Highlight(GameObject go, bool highlighted)
    {
        if (go != null)
        {
            var count = mHighlighted.Count;
            while (count > 0)
            {
                var item = mHighlighted[--count];
                if (item == null || item.go == null)
                {
                    mHighlighted.RemoveAt(count);
                }
                else if (item.go == go)
                {
                    if (highlighted)
                    {
                        item.counter++;
                    }
                    else if (--item.counter < 1)
                    {
                        mHighlighted.Remove(item);
                        Notify(go, "OnHover", false);
                    }

                    return;
                }
            }

            if (highlighted)
            {
                var highlighted3 = new Highlighted
                {
                    go = go,
                    counter = 1
                };
                mHighlighted.Add(highlighted3);
                Notify(go, "OnHover", true);
            }
        }
    }

    public static bool IsHighlighted(GameObject go)
    {
        var count = mHighlighted.Count;
        while (count > 0)
        {
            var highlighted = mHighlighted[--count];
            if (highlighted.go == go)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsVisible(ref RaycastHit hit)
    {
        var panel = NGUITools.FindInParents<UIPanel>(hit.collider.gameObject);
        if (panel != null && !panel.IsVisible(hit.point))
        {
            return false;
        }

        return true;
    }

    public static void Notify(GameObject go, string funcName, object obj)
    {
        if (go != null)
        {
            go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
            if (genericEventHandler != null && genericEventHandler != go)
            {
                genericEventHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnApplicationQuit()
    {
        mHighlighted.Clear();
    }

    private void OnDestroy()
    {
        mList.Remove(this);
    }

    public void ProcessMouse()
    {
        var flag = useMouse && Time.timeScale < 0.9f;
        if (!flag)
        {
            for (var k = 0; k < 3; k++)
            {
                if (Input.GetMouseButton(k) || Input.GetMouseButtonUp(k))
                {
                    flag = true;
                    break;
                }
            }
        }

        mMouse[0].pos = Input.mousePosition;
        mMouse[0].delta = mMouse[0].pos - lastTouchPosition;
        var flag2 = mMouse[0].pos != lastTouchPosition;
        lastTouchPosition = mMouse[0].pos;
        if (flag)
        {
            hoveredObject = !Raycast(Input.mousePosition, ref lastHit) ? fallThrough : lastHit.collider.gameObject;
            if (hoveredObject == null)
            {
                hoveredObject = genericEventHandler;
            }

            mMouse[0].current = hoveredObject;
        }

        for (var i = 1; i < 3; i++)
        {
            mMouse[i].pos = mMouse[0].pos;
            mMouse[i].delta = mMouse[0].delta;
            mMouse[i].current = mMouse[0].current;
        }

        var flag3 = false;
        for (var j = 0; j < 3; j++)
        {
            if (Input.GetMouseButton(j))
            {
                flag3 = true;
                break;
            }
        }

        if (flag3)
        {
            mTooltipTime = 0f;
        }
        else if (useMouse && flag2 && (!stickyTooltip || mHover != mMouse[0].current))
        {
            if (mTooltipTime != 0f)
            {
                mTooltipTime = Time.realtimeSinceStartup + tooltipDelay;
            }
            else if (mTooltip != null)
            {
                ShowTooltip(false);
            }
        }

        if (useMouse && !flag3 && mHover != null && mHover != mMouse[0].current)
        {
            if (mTooltip != null)
            {
                ShowTooltip(false);
            }

            Highlight(mHover, false);
            mHover = null;
        }

        if (useMouse)
        {
            for (var m = 0; m < 3; m++)
            {
                var mouseButtonDown = Input.GetMouseButtonDown(m);
                var mouseButtonUp = Input.GetMouseButtonUp(m);
                currentTouch = mMouse[m];
                currentTouchID = -1 - m;
                if (mouseButtonDown)
                {
                    currentTouch.pressedCam = currentCamera;
                }
                else if (currentTouch.pressed != null)
                {
                    currentCamera = currentTouch.pressedCam;
                }

                ProcessTouch(mouseButtonDown, mouseButtonUp);
            }

            currentTouch = null;
        }

        if (useMouse && !flag3 && mHover != mMouse[0].current)
        {
            mTooltipTime = Time.realtimeSinceStartup + tooltipDelay;
            mHover = mMouse[0].current;
            Highlight(mHover, true);
        }
    }

    public void ProcessOthers()
    {
        currentTouchID = -100;
        currentTouch = mController;
        inputHasFocus = mSel != null && mSel.GetComponent<UIInput>() != null;
        var pressed = submitKey0 != KeyCode.None && Input.GetKeyDown(submitKey0) ||
                      submitKey1 != KeyCode.None && Input.GetKeyDown(submitKey1);
        var unpressed = submitKey0 != KeyCode.None && Input.GetKeyUp(submitKey0) ||
                        submitKey1 != KeyCode.None && Input.GetKeyUp(submitKey1);
        if (pressed || unpressed)
        {
            currentTouch.current = mSel;
            ProcessTouch(pressed, unpressed);
            currentTouch.current = null;
        }

        var num = 0;
        var num2 = 0;
        if (useKeyboard)
        {
            if (inputHasFocus)
            {
                num += GetDirection(KeyCode.UpArrow, KeyCode.DownArrow);
                num2 += GetDirection(KeyCode.RightArrow, KeyCode.LeftArrow);
            }
            else
            {
                num += GetDirection(KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow);
                num2 += GetDirection(KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow);
            }
        }

        if (useController)
        {
            if (!string.IsNullOrEmpty(verticalAxisName))
            {
                num += GetDirection(verticalAxisName);
            }

            if (!string.IsNullOrEmpty(horizontalAxisName))
            {
                num2 += GetDirection(horizontalAxisName);
            }
        }

        if (num != 0)
        {
            Notify(mSel, "OnKey", num <= 0 ? KeyCode.DownArrow : KeyCode.UpArrow);
        }

        if (num2 != 0)
        {
            Notify(mSel, "OnKey", num2 <= 0 ? KeyCode.LeftArrow : KeyCode.RightArrow);
        }

        if (useKeyboard && Input.GetKeyDown(KeyCode.Tab))
        {
            Notify(mSel, "OnKey", KeyCode.Tab);
        }

        if (cancelKey0 != KeyCode.None && Input.GetKeyDown(cancelKey0))
        {
            Notify(mSel, "OnKey", KeyCode.Escape);
        }

        if (cancelKey1 != KeyCode.None && Input.GetKeyDown(cancelKey1))
        {
            Notify(mSel, "OnKey", KeyCode.Escape);
        }

        currentTouch = null;
    }

    public void ProcessTouch(bool pressed, bool unpressed)
    {
        var flag = currentTouch == mMouse[0] || currentTouch == mMouse[1] || currentTouch == mMouse[2];
        var num = !flag ? touchDragThreshold : mouseDragThreshold;
        var num2 = !flag ? touchClickThreshold : mouseClickThreshold;
        if (pressed)
        {
            if (mTooltip != null)
            {
                ShowTooltip(false);
            }

            currentTouch.pressStarted = true;
            Notify(currentTouch.pressed, "OnPress", false);
            currentTouch.pressed = currentTouch.current;
            currentTouch.dragged = currentTouch.current;
            currentTouch.clickNotification = !flag ? ClickNotification.Always : ClickNotification.BasedOnDelta;
            currentTouch.totalDelta = Vector2.zero;
            currentTouch.dragStarted = false;
            Notify(currentTouch.pressed, "OnPress", true);
            if (currentTouch.pressed != mSel)
            {
                if (mTooltip != null)
                {
                    ShowTooltip(false);
                }

                selectedObject = null;
            }
        }
        else
        {
            if (currentTouch.clickNotification != ClickNotification.None && !stickyPress && !unpressed &&
                currentTouch.pressStarted && currentTouch.pressed != hoveredObject)
            {
                isDragging = true;
                Notify(currentTouch.pressed, "OnPress", false);
                currentTouch.pressed = hoveredObject;
                Notify(currentTouch.pressed, "OnPress", true);
                isDragging = false;
            }

            if (currentTouch.pressed != null && currentTouch.delta.magnitude != 0f)
            {
                currentTouch.totalDelta += currentTouch.delta;
                var magnitude = currentTouch.totalDelta.magnitude;
                if (!currentTouch.dragStarted && num < magnitude)
                {
                    currentTouch.dragStarted = true;
                    currentTouch.delta = currentTouch.totalDelta;
                }

                if (currentTouch.dragStarted)
                {
                    if (mTooltip != null)
                    {
                        ShowTooltip(false);
                    }

                    isDragging = true;
                    var flag2 = currentTouch.clickNotification == ClickNotification.None;
                    Notify(currentTouch.dragged, "OnDrag", currentTouch.delta);
                    isDragging = false;
                    if (flag2)
                    {
                        currentTouch.clickNotification = ClickNotification.None;
                    }
                    else if (currentTouch.clickNotification == ClickNotification.BasedOnDelta && num2 < magnitude)
                    {
                        currentTouch.clickNotification = ClickNotification.None;
                    }
                }
            }
        }

        if (unpressed)
        {
            currentTouch.pressStarted = false;
            if (mTooltip != null)
            {
                ShowTooltip(false);
            }

            if (currentTouch.pressed != null)
            {
                Notify(currentTouch.pressed, "OnPress", false);
                if (useMouse && currentTouch.pressed == mHover)
                {
                    Notify(currentTouch.pressed, "OnHover", true);
                }

                if (currentTouch.dragged == currentTouch.current ||
                    currentTouch.clickNotification != ClickNotification.None && currentTouch.totalDelta.magnitude < num)
                {
                    if (currentTouch.pressed != mSel)
                    {
                        mSel = currentTouch.pressed;
                        Notify(currentTouch.pressed, "OnSelect", true);
                    }
                    else
                    {
                        mSel = currentTouch.pressed;
                    }

                    if (currentTouch.clickNotification != ClickNotification.None)
                    {
                        var realtimeSinceStartup = Time.realtimeSinceStartup;
                        Notify(currentTouch.pressed, "OnClick", null);
                        if (currentTouch.clickTime + 0.35f > realtimeSinceStartup)
                        {
                            Notify(currentTouch.pressed, "OnDoubleClick", null);
                        }

                        currentTouch.clickTime = realtimeSinceStartup;
                    }
                }
                else
                {
                    Notify(currentTouch.current, "OnDrop", currentTouch.dragged);
                }
            }

            currentTouch.dragStarted = false;
            currentTouch.pressed = null;
            currentTouch.dragged = null;
        }
    }

    public void ProcessTouches()
    {
        for (var i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            currentTouchID = !allowMultiTouch ? 1 : touch.fingerId;
            currentTouch = GetTouch(currentTouchID);
            var pressed = touch.phase == TouchPhase.Began || currentTouch.touchBegan;
            var unpressed = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
            currentTouch.touchBegan = false;
            if (pressed)
            {
                currentTouch.delta = Vector2.zero;
            }
            else
            {
                currentTouch.delta = touch.position - currentTouch.pos;
            }

            currentTouch.pos = touch.position;
            hoveredObject = !Raycast(currentTouch.pos, ref lastHit) ? fallThrough : lastHit.collider.gameObject;
            if (hoveredObject == null)
            {
                hoveredObject = genericEventHandler;
            }

            currentTouch.current = hoveredObject;
            lastTouchPosition = currentTouch.pos;
            if (pressed)
            {
                currentTouch.pressedCam = currentCamera;
            }
            else if (currentTouch.pressed != null)
            {
                currentCamera = currentTouch.pressedCam;
            }

            if (touch.tapCount > 1)
            {
                currentTouch.clickTime = Time.realtimeSinceStartup;
            }

            ProcessTouch(pressed, unpressed);
            if (unpressed)
            {
                RemoveTouch(currentTouchID);
            }

            currentTouch = null;
            if (!allowMultiTouch)
            {
                break;
            }
        }
    }

    public static bool Raycast(Vector3 inPos, ref RaycastHit hit)
    {
        for (var i = 0; i < mList.Count; i++)
        {
            var camera = mList[i];
            if (camera.enabled && NGUITools.GetActive(camera.gameObject))
            {
                currentCamera = camera.cachedCamera;
                var vector = currentCamera.ScreenToViewportPoint(inPos);
                if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
                {
                    var ray = currentCamera.ScreenPointToRay(inPos);
                    var layerMask = currentCamera.cullingMask & camera.eventReceiverMask;
                    var distance = camera.rangeDistance <= 0f
                        ? currentCamera.farClipPlane - currentCamera.nearClipPlane
                        : camera.rangeDistance;
                    if (camera.clipRaycasts)
                    {
                        var array = Physics.RaycastAll(ray, distance, layerMask);
                        if (array.Length <= 1)
                        {
                            if (array.Length == 1 && IsVisible(ref array[0]))
                            {
                                hit = array[0];
                                return true;
                            }
                        }
                        else
                        {
                            if (f__amcache31 == null)
                            {
                                f__amcache31 = (r1, r2) => r1.distance.CompareTo(r2.distance);
                            }

                            Array.Sort(array, f__amcache31);
                            var index = 0;
                            var length = array.Length;
                            while (index < length)
                            {
                                if (IsVisible(ref array[index]))
                                {
                                    hit = array[index];
                                    return true;
                                }

                                index++;
                            }
                        }
                    }
                    else if (Physics.Raycast(ray, out hit, distance, layerMask))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static void RemoveTouch(int id)
    {
        mTouches.Remove(id);
    }

    public void ShowTooltip(bool val)
    {
        mTooltipTime = 0f;
        Notify(mTooltip, "OnTooltip", val);
        if (!val)
        {
            mTooltip = null;
        }
    }

    private void Start()
    {
        mList.Add(this);
        mList.Sort(CompareFunc);
    }

    private void Update()
    {
        if (Application.isPlaying && handlesEvents)
        {
            current = this;
            if (useMouse || useTouch && mIsEditor)
            {
                ProcessMouse();
            }

            if (useTouch)
            {
                ProcessTouches();
            }

            onCustomInput?.Invoke();
            if (useMouse && mSel != null && (cancelKey0 != KeyCode.None && Input.GetKeyDown(cancelKey0) ||
                                             cancelKey1 != KeyCode.None && Input.GetKeyDown(cancelKey1)))
            {
                selectedObject = null;
            }

            if (mSel != null)
            {
                var inputString = Input.inputString;
                if (useKeyboard && Input.GetKeyDown(KeyCode.Delete))
                {
                    inputString = inputString + "\b";
                }

                if (inputString.Length > 0)
                {
                    if (!stickyTooltip && mTooltip != null)
                    {
                        ShowTooltip(false);
                    }

                    Notify(mSel, "OnInput", inputString);
                }
            }
            else
            {
                inputHasFocus = false;
            }

            if (mSel != null)
            {
                ProcessOthers();
            }

            if (useMouse && mHover != null)
            {
                var axis = Input.GetAxis(scrollAxisName);
                if (axis != 0f)
                {
                    Notify(mHover, "OnScroll", axis);
                }

                if (showTooltips && mTooltipTime != 0f &&
                    (mTooltipTime < Time.realtimeSinceStartup || Input.GetKey(KeyCode.LeftShift) ||
                     Input.GetKey(KeyCode.RightShift)))
                {
                    mTooltip = mHover;
                    ShowTooltip(true);
                }
            }

            current = null;
        }
    }

    public Camera cachedCamera
    {
        get
        {
            if (mCam == null)
            {
                mCam = camera;
            }

            return mCam;
        }
    }

    public static int dragCount
    {
        get
        {
            var num = 0;
            for (var i = 0; i < mTouches.Count; i++)
            {
                if (mTouches[i].dragged != null)
                {
                    num++;
                }
            }

            for (var j = 0; j < mMouse.Length; j++)
            {
                if (mMouse[j].dragged != null)
                {
                    num++;
                }
            }

            if (mController.dragged != null)
            {
                num++;
            }

            return num;
        }
    }

    public static UICamera eventHandler
    {
        get
        {
            for (var i = 0; i < mList.Count; i++)
            {
                var camera = mList[i];
                if (camera != null && camera.enabled && NGUITools.GetActive(camera.gameObject))
                {
                    return camera;
                }
            }

            return null;
        }
    }

    private bool handlesEvents
    {
        get { return eventHandler == this; }
    }

    public static Camera mainCamera
    {
        get
        {
            var eventHandler = UICamera.eventHandler;
            return eventHandler == null ? null : eventHandler.cachedCamera;
        }
    }

    public static GameObject selectedObject
    {
        get { return mSel; }
        set
        {
            if (mSel != value)
            {
                if (mSel != null)
                {
                    var camera = FindCameraForLayer(mSel.layer);
                    if (camera != null)
                    {
                        current = camera;
                        currentCamera = camera.mCam;
                        Notify(mSel, "OnSelect", false);
                        if (camera.useController || camera.useKeyboard)
                        {
                            Highlight(mSel, false);
                        }

                        current = null;
                    }
                }

                mSel = value;
                if (mSel != null)
                {
                    var camera2 = FindCameraForLayer(mSel.layer);
                    if (camera2 != null)
                    {
                        current = camera2;
                        currentCamera = camera2.mCam;
                        if (camera2.useController || camera2.useKeyboard)
                        {
                            Highlight(mSel, true);
                        }

                        Notify(mSel, "OnSelect", true);
                        current = null;
                    }
                }
            }
        }
    }

    public static int touchCount
    {
        get
        {
            var num = 0;
            for (var i = 0; i < mTouches.Count; i++)
            {
                if (mTouches[i].pressed != null)
                {
                    num++;
                }
            }

            for (var j = 0; j < mMouse.Length; j++)
            {
                if (mMouse[j].pressed != null)
                {
                    num++;
                }
            }

            if (mController.pressed != null)
            {
                num++;
            }

            return num;
        }
    }

    public enum ClickNotification
    {
        None,
        Always,
        BasedOnDelta
    }

    private class Highlighted
    {
        public int counter;
        public GameObject go;
    }

    public class MouseOrTouch
    {
        public ClickNotification clickNotification = ClickNotification.Always;
        public float clickTime;
        public GameObject current;
        public Vector2 delta;
        public GameObject dragged;
        public bool dragStarted;
        public Vector2 pos;
        public GameObject pressed;
        public Camera pressedCam;
        public bool pressStarted;
        public Vector2 totalDelta;
        public bool touchBegan = true;
    }

    public delegate void OnCustomInput();
}