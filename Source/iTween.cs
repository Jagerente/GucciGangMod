//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class iTween : MonoBehaviour
{
    public string _name;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap10;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap11;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap12;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap13;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap14;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap8;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap9;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapA;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapB;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapC;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapD;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapE;
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmapF;
    private ApplyTween apply;
    private AudioSource audioSource;
    private static GameObject cameraFade;
    private Color[,] colors;
    public float delay;
    private float delayStarted;
    private EasingFunction ease;
    public EaseType easeType;
    private float[] floats;
    public string id;
    private bool isLocal;
    public bool isPaused;
    public bool isRunning;
    private bool kinematic;
    private float lastRealTime;
    private bool loop;
    public LoopType loopType;
    public string method;
    private NamedValueColor namedcolorvalue;
    private CRSpline path;
    private float percentage;
    private bool physics;
    private Vector3 postUpdate;
    private Vector3 preUpdate;
    private Rect[] rects;
    private bool reverse;
    private float runningTime;
    private Space space;
    private Transform thisTransform;
    public float time;
    private Hashtable tweenArguments;
    public static List<Hashtable> tweens = new List<Hashtable>();
    public string type;
    private bool useRealTime;
    private Vector2[] vector2s;
    private Vector3[] vector3s;
    private bool wasPaused;

    private iTween(Hashtable h)
    {
        tweenArguments = h;
    }

    private void ApplyAudioToTargets()
    {
        vector2s[2].x = ease(vector2s[0].x, vector2s[1].x, percentage);
        vector2s[2].y = ease(vector2s[0].y, vector2s[1].y, percentage);
        audioSource.volume = vector2s[2].x;
        audioSource.pitch = vector2s[2].y;
        if (percentage == 1f)
        {
            audioSource.volume = vector2s[1].x;
            audioSource.pitch = vector2s[1].y;
        }
    }

    private void ApplyColorTargets()
    {
        colors[0, 2].r = ease(colors[0, 0].r, colors[0, 1].r, percentage);
        colors[0, 2].g = ease(colors[0, 0].g, colors[0, 1].g, percentage);
        colors[0, 2].b = ease(colors[0, 0].b, colors[0, 1].b, percentage);
        colors[0, 2].a = ease(colors[0, 0].a, colors[0, 1].a, percentage);
        tweenArguments["onupdateparams"] = colors[0, 2];
        if (percentage == 1f)
        {
            tweenArguments["onupdateparams"] = colors[0, 1];
        }
    }

    private void ApplyColorToTargets()
    {
        for (var i = 0; i < colors.GetLength(0); i++)
        {
            colors[i, 2].r = ease(colors[i, 0].r, colors[i, 1].r, percentage);
            colors[i, 2].g = ease(colors[i, 0].g, colors[i, 1].g, percentage);
            colors[i, 2].b = ease(colors[i, 0].b, colors[i, 1].b, percentage);
            colors[i, 2].a = ease(colors[i, 0].a, colors[i, 1].a, percentage);
        }
        if (GetComponent<GUITexture>() != null)
        {
            guiTexture.color = colors[0, 2];
        }
        else if (GetComponent<GUIText>() != null)
        {
            guiText.material.color = colors[0, 2];
        }
        else if (renderer != null)
        {
            for (var j = 0; j < colors.GetLength(0); j++)
            {
                renderer.materials[j].SetColor(namedcolorvalue.ToString(), colors[j, 2]);
            }
        }
        else if (light != null)
        {
            light.color = colors[0, 2];
        }
        if (percentage == 1f)
        {
            if (GetComponent<GUITexture>() != null)
            {
                guiTexture.color = colors[0, 1];
            }
            else if (GetComponent<GUIText>() != null)
            {
                guiText.material.color = colors[0, 1];
            }
            else if (renderer != null)
            {
                for (var k = 0; k < colors.GetLength(0); k++)
                {
                    renderer.materials[k].SetColor(namedcolorvalue.ToString(), colors[k, 1]);
                }
            }
            else if (light != null)
            {
                light.color = colors[0, 1];
            }
        }
    }

    private void ApplyFloatTargets()
    {
        floats[2] = ease(floats[0], floats[1], percentage);
        tweenArguments["onupdateparams"] = floats[2];
        if (percentage == 1f)
        {
            tweenArguments["onupdateparams"] = floats[1];
        }
    }

    private void ApplyLookToTargets()
    {
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        if (isLocal)
        {
            thisTransform.localRotation = Quaternion.Euler(vector3s[2]);
        }
        else
        {
            thisTransform.rotation = Quaternion.Euler(vector3s[2]);
        }
    }

    private void ApplyMoveByTargets()
    {
        preUpdate = thisTransform.position;
        var eulerAngles = new Vector3();
        if (tweenArguments.Contains("looktarget"))
        {
            eulerAngles = thisTransform.eulerAngles;
            thisTransform.eulerAngles = vector3s[4];
        }
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        thisTransform.Translate(vector3s[2] - vector3s[3], space);
        vector3s[3] = vector3s[2];
        if (tweenArguments.Contains("looktarget"))
        {
            thisTransform.eulerAngles = eulerAngles;
        }
        postUpdate = thisTransform.position;
        if (physics)
        {
            thisTransform.position = preUpdate;
            rigidbody.MovePosition(postUpdate);
        }
    }

    private void ApplyMoveToPathTargets()
    {
        preUpdate = thisTransform.position;
        var num = ease(0f, 1f, percentage);
        if (isLocal)
        {
            thisTransform.localPosition = path.Interp(Mathf.Clamp(num, 0f, 1f));
        }
        else
        {
            thisTransform.position = path.Interp(Mathf.Clamp(num, 0f, 1f));
        }
        if (tweenArguments.Contains("orienttopath") && (bool) tweenArguments["orienttopath"])
        {
            float lookAhead;
            if (tweenArguments.Contains("lookahead"))
            {
                lookAhead = (float) tweenArguments["lookahead"];
            }
            else
            {
                lookAhead = Defaults.lookAhead;
            }
            var num3 = ease(0f, 1f, Mathf.Min(1f, percentage + lookAhead));
            tweenArguments["looktarget"] = path.Interp(Mathf.Clamp(num3, 0f, 1f));
        }
        postUpdate = thisTransform.position;
        if (physics)
        {
            thisTransform.position = preUpdate;
            rigidbody.MovePosition(postUpdate);
        }
    }

    private void ApplyMoveToTargets()
    {
        preUpdate = thisTransform.position;
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        if (isLocal)
        {
            thisTransform.localPosition = vector3s[2];
        }
        else
        {
            thisTransform.position = vector3s[2];
        }
        if (percentage == 1f)
        {
            if (isLocal)
            {
                thisTransform.localPosition = vector3s[1];
            }
            else
            {
                thisTransform.position = vector3s[1];
            }
        }
        postUpdate = thisTransform.position;
        if (physics)
        {
            thisTransform.position = preUpdate;
            rigidbody.MovePosition(postUpdate);
        }
    }

    private void ApplyPunchPositionTargets()
    {
        preUpdate = thisTransform.position;
        var eulerAngles = new Vector3();
        if (tweenArguments.Contains("looktarget"))
        {
            eulerAngles = thisTransform.eulerAngles;
            thisTransform.eulerAngles = vector3s[4];
        }
        if (vector3s[1].x > 0f)
        {
            vector3s[2].x = punch(vector3s[1].x, percentage);
        }
        else if (vector3s[1].x < 0f)
        {
            vector3s[2].x = -punch(Mathf.Abs(vector3s[1].x), percentage);
        }
        if (vector3s[1].y > 0f)
        {
            vector3s[2].y = punch(vector3s[1].y, percentage);
        }
        else if (vector3s[1].y < 0f)
        {
            vector3s[2].y = -punch(Mathf.Abs(vector3s[1].y), percentage);
        }
        if (vector3s[1].z > 0f)
        {
            vector3s[2].z = punch(vector3s[1].z, percentage);
        }
        else if (vector3s[1].z < 0f)
        {
            vector3s[2].z = -punch(Mathf.Abs(vector3s[1].z), percentage);
        }
        thisTransform.Translate(vector3s[2] - vector3s[3], space);
        vector3s[3] = vector3s[2];
        if (tweenArguments.Contains("looktarget"))
        {
            thisTransform.eulerAngles = eulerAngles;
        }
        postUpdate = thisTransform.position;
        if (physics)
        {
            thisTransform.position = preUpdate;
            rigidbody.MovePosition(postUpdate);
        }
    }

    private void ApplyPunchRotationTargets()
    {
        preUpdate = thisTransform.eulerAngles;
        if (vector3s[1].x > 0f)
        {
            vector3s[2].x = punch(vector3s[1].x, percentage);
        }
        else if (vector3s[1].x < 0f)
        {
            vector3s[2].x = -punch(Mathf.Abs(vector3s[1].x), percentage);
        }
        if (vector3s[1].y > 0f)
        {
            vector3s[2].y = punch(vector3s[1].y, percentage);
        }
        else if (vector3s[1].y < 0f)
        {
            vector3s[2].y = -punch(Mathf.Abs(vector3s[1].y), percentage);
        }
        if (vector3s[1].z > 0f)
        {
            vector3s[2].z = punch(vector3s[1].z, percentage);
        }
        else if (vector3s[1].z < 0f)
        {
            vector3s[2].z = -punch(Mathf.Abs(vector3s[1].z), percentage);
        }
        thisTransform.Rotate(vector3s[2] - vector3s[3], space);
        vector3s[3] = vector3s[2];
        postUpdate = thisTransform.eulerAngles;
        if (physics)
        {
            thisTransform.eulerAngles = preUpdate;
            rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
        }
    }

    private void ApplyPunchScaleTargets()
    {
        if (vector3s[1].x > 0f)
        {
            vector3s[2].x = punch(vector3s[1].x, percentage);
        }
        else if (vector3s[1].x < 0f)
        {
            vector3s[2].x = -punch(Mathf.Abs(vector3s[1].x), percentage);
        }
        if (vector3s[1].y > 0f)
        {
            vector3s[2].y = punch(vector3s[1].y, percentage);
        }
        else if (vector3s[1].y < 0f)
        {
            vector3s[2].y = -punch(Mathf.Abs(vector3s[1].y), percentage);
        }
        if (vector3s[1].z > 0f)
        {
            vector3s[2].z = punch(vector3s[1].z, percentage);
        }
        else if (vector3s[1].z < 0f)
        {
            vector3s[2].z = -punch(Mathf.Abs(vector3s[1].z), percentage);
        }
        thisTransform.localScale = vector3s[0] + vector3s[2];
    }

    private void ApplyRectTargets()
    {
        rects[2].x = ease(rects[0].x, rects[1].x, percentage);
        rects[2].y = ease(rects[0].y, rects[1].y, percentage);
        rects[2].width = ease(rects[0].width, rects[1].width, percentage);
        rects[2].height = ease(rects[0].height, rects[1].height, percentage);
        tweenArguments["onupdateparams"] = rects[2];
        if (percentage == 1f)
        {
            tweenArguments["onupdateparams"] = rects[1];
        }
    }

    private void ApplyRotateAddTargets()
    {
        preUpdate = thisTransform.eulerAngles;
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        thisTransform.Rotate(vector3s[2] - vector3s[3], space);
        vector3s[3] = vector3s[2];
        postUpdate = thisTransform.eulerAngles;
        if (physics)
        {
            thisTransform.eulerAngles = preUpdate;
            rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
        }
    }

    private void ApplyRotateToTargets()
    {
        preUpdate = thisTransform.eulerAngles;
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        if (isLocal)
        {
            thisTransform.localRotation = Quaternion.Euler(vector3s[2]);
        }
        else
        {
            thisTransform.rotation = Quaternion.Euler(vector3s[2]);
        }
        if (percentage == 1f)
        {
            if (isLocal)
            {
                thisTransform.localRotation = Quaternion.Euler(vector3s[1]);
            }
            else
            {
                thisTransform.rotation = Quaternion.Euler(vector3s[1]);
            }
        }
        postUpdate = thisTransform.eulerAngles;
        if (physics)
        {
            thisTransform.eulerAngles = preUpdate;
            rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
        }
    }

    private void ApplyScaleToTargets()
    {
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        thisTransform.localScale = vector3s[2];
        if (percentage == 1f)
        {
            thisTransform.localScale = vector3s[1];
        }
    }

    private void ApplyShakePositionTargets()
    {
        if (isLocal)
        {
            preUpdate = thisTransform.localPosition;
        }
        else
        {
            preUpdate = thisTransform.position;
        }
        var eulerAngles = new Vector3();
        if (tweenArguments.Contains("looktarget"))
        {
            eulerAngles = thisTransform.eulerAngles;
            thisTransform.eulerAngles = vector3s[3];
        }
        if (percentage == 0f)
        {
            thisTransform.Translate(vector3s[1], space);
        }
        if (isLocal)
        {
            thisTransform.localPosition = vector3s[0];
        }
        else
        {
            thisTransform.position = vector3s[0];
        }
        var num = 1f - percentage;
        vector3s[2].x = UnityEngine.Random.Range(-vector3s[1].x * num, vector3s[1].x * num);
        vector3s[2].y = UnityEngine.Random.Range(-vector3s[1].y * num, vector3s[1].y * num);
        vector3s[2].z = UnityEngine.Random.Range(-vector3s[1].z * num, vector3s[1].z * num);
        if (isLocal)
        {
            thisTransform.localPosition += vector3s[2];
        }
        else
        {
            thisTransform.position += vector3s[2];
        }
        if (tweenArguments.Contains("looktarget"))
        {
            thisTransform.eulerAngles = eulerAngles;
        }
        postUpdate = thisTransform.position;
        if (physics)
        {
            thisTransform.position = preUpdate;
            rigidbody.MovePosition(postUpdate);
        }
    }

    private void ApplyShakeRotationTargets()
    {
        preUpdate = thisTransform.eulerAngles;
        if (percentage == 0f)
        {
            thisTransform.Rotate(vector3s[1], space);
        }
        thisTransform.eulerAngles = vector3s[0];
        var num = 1f - percentage;
        vector3s[2].x = UnityEngine.Random.Range(-vector3s[1].x * num, vector3s[1].x * num);
        vector3s[2].y = UnityEngine.Random.Range(-vector3s[1].y * num, vector3s[1].y * num);
        vector3s[2].z = UnityEngine.Random.Range(-vector3s[1].z * num, vector3s[1].z * num);
        thisTransform.Rotate(vector3s[2], space);
        postUpdate = thisTransform.eulerAngles;
        if (physics)
        {
            thisTransform.eulerAngles = preUpdate;
            rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
        }
    }

    private void ApplyShakeScaleTargets()
    {
        if (percentage == 0f)
        {
            thisTransform.localScale = vector3s[1];
        }
        thisTransform.localScale = vector3s[0];
        var num = 1f - percentage;
        vector3s[2].x = UnityEngine.Random.Range(-vector3s[1].x * num, vector3s[1].x * num);
        vector3s[2].y = UnityEngine.Random.Range(-vector3s[1].y * num, vector3s[1].y * num);
        vector3s[2].z = UnityEngine.Random.Range(-vector3s[1].z * num, vector3s[1].z * num);
        thisTransform.localScale += vector3s[2];
    }

    private void ApplyStabTargets()
    {
    }

    private void ApplyVector2Targets()
    {
        vector2s[2].x = ease(vector2s[0].x, vector2s[1].x, percentage);
        vector2s[2].y = ease(vector2s[0].y, vector2s[1].y, percentage);
        tweenArguments["onupdateparams"] = vector2s[2];
        if (percentage == 1f)
        {
            tweenArguments["onupdateparams"] = vector2s[1];
        }
    }

    private void ApplyVector3Targets()
    {
        vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
        vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
        vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
        tweenArguments["onupdateparams"] = vector3s[2];
        if (percentage == 1f)
        {
            tweenArguments["onupdateparams"] = vector3s[1];
        }
    }

    public static void AudioFrom(GameObject target, Hashtable args)
    {
        AudioSource source;
        Vector2 vector;
        Vector2 vector2;
        args = CleanArgs(args);
        if (args.Contains("audiosource"))
        {
            source = (AudioSource) args["audiosource"];
        }
        else if (target.GetComponent<AudioSource>() != null)
        {
            source = target.audio;
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: AudioFrom requires an AudioSource.");
            return;
        }
        vector.x = vector2.x = source.volume;
        vector.y = vector2.y = source.pitch;
        if (args.Contains("volume"))
        {
            vector2.x = (float) args["volume"];
        }
        if (args.Contains("pitch"))
        {
            vector2.y = (float) args["pitch"];
        }
        source.volume = vector2.x;
        source.pitch = vector2.y;
        args["volume"] = vector.x;
        args["pitch"] = vector.y;
        if (!args.Contains("easetype"))
        {
            args.Add("easetype", EaseType.linear);
        }
        args["type"] = "audio";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void AudioFrom(GameObject target, float volume, float pitch, float time)
    {
        var args = new object[] { "volume", volume, "pitch", pitch, "time", time };
        AudioFrom(target, Hash(args));
    }

    public static void AudioTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (!args.Contains("easetype"))
        {
            args.Add("easetype", EaseType.linear);
        }
        args["type"] = "audio";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void AudioTo(GameObject target, float volume, float pitch, float time)
    {
        var args = new object[] { "volume", volume, "pitch", pitch, "time", time };
        AudioTo(target, Hash(args));
    }

    public static void AudioUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        AudioSource source;
        CleanArgs(args);
        var vectorArray = new Vector2[4];
        if (args.Contains("time"))
        {
            updateTime = (float) args["time"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        if (args.Contains("audiosource"))
        {
            source = (AudioSource) args["audiosource"];
        }
        else if (target.GetComponent<AudioSource>() != null)
        {
            source = target.audio;
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: AudioUpdate requires an AudioSource.");
            return;
        }
        vectorArray[0] = vectorArray[1] = new Vector2(source.volume, source.pitch);
        if (args.Contains("volume"))
        {
            vectorArray[1].x = (float) args["volume"];
        }
        if (args.Contains("pitch"))
        {
            vectorArray[1].y = (float) args["pitch"];
        }
        vectorArray[3].x = Mathf.SmoothDampAngle(vectorArray[0].x, vectorArray[1].x, ref vectorArray[2].x, updateTime);
        vectorArray[3].y = Mathf.SmoothDampAngle(vectorArray[0].y, vectorArray[1].y, ref vectorArray[2].y, updateTime);
        source.volume = vectorArray[3].x;
        source.pitch = vectorArray[3].y;
    }

    public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
    {
        var args = new object[] { "volume", volume, "pitch", pitch, "time", time };
        AudioUpdate(target, Hash(args));
    }

    private void Awake()
    {
        thisTransform = transform;
        RetrieveArgs();
        lastRealTime = Time.realtimeSinceStartup;
    }

    private void CallBack(string callbackType)
    {
        if (tweenArguments.Contains(callbackType) && !tweenArguments.Contains("ischild"))
        {
            GameObject gameObject;
            if (tweenArguments.Contains(callbackType + "target"))
            {
                gameObject = (GameObject) tweenArguments[callbackType + "target"];
            }
            else
            {
                gameObject = this.gameObject;
            }
            if (tweenArguments[callbackType].GetType() == typeof(string))
            {
                gameObject.SendMessage((string) tweenArguments[callbackType], tweenArguments[callbackType + "params"], SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                UnityEngine.Debug.LogError("iTween Error: Callback method references must be passed as a String!");
                Destroy(this);
            }
        }
    }

    public static GameObject CameraFadeAdd()
    {
        if (cameraFade != null)
        {
            return null;
        }
        cameraFade = new GameObject("iTween Camera Fade");
        cameraFade.transform.position = new Vector3(0.5f, 0.5f, Defaults.cameraFadeDepth);
        cameraFade.AddComponent<GUITexture>();
        cameraFade.guiTexture.texture = CameraTexture(Color.black);
        cameraFade.guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 0f);
        return cameraFade;
    }

    public static GameObject CameraFadeAdd(Texture2D texture)
    {
        if (cameraFade != null)
        {
            return null;
        }
        cameraFade = new GameObject("iTween Camera Fade");
        cameraFade.transform.position = new Vector3(0.5f, 0.5f, Defaults.cameraFadeDepth);
        cameraFade.AddComponent<GUITexture>();
        cameraFade.guiTexture.texture = texture;
        cameraFade.guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 0f);
        return cameraFade;
    }

    public static GameObject CameraFadeAdd(Texture2D texture, int depth)
    {
        if (cameraFade != null)
        {
            return null;
        }
        cameraFade = new GameObject("iTween Camera Fade");
        cameraFade.transform.position = new Vector3(0.5f, 0.5f, depth);
        cameraFade.AddComponent<GUITexture>();
        cameraFade.guiTexture.texture = texture;
        cameraFade.guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 0f);
        return cameraFade;
    }

    public static void CameraFadeDepth(int depth)
    {
        if (cameraFade != null)
        {
            cameraFade.transform.position = new Vector3(cameraFade.transform.position.x, cameraFade.transform.position.y, depth);
        }
    }

    public static void CameraFadeDestroy()
    {
        if (cameraFade != null)
        {
            Destroy(cameraFade);
        }
    }

    public static void CameraFadeFrom(Hashtable args)
    {
        if (cameraFade != null)
        {
            ColorFrom(cameraFade, args);
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
        }
    }

    public static void CameraFadeFrom(float amount, float time)
    {
        if (cameraFade != null)
        {
            var args = new object[] { "amount", amount, "time", time };
            CameraFadeFrom(Hash(args));
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
        }
    }

    public static void CameraFadeSwap(Texture2D texture)
    {
        if (cameraFade != null)
        {
            cameraFade.guiTexture.texture = texture;
        }
    }

    public static void CameraFadeTo(Hashtable args)
    {
        if (cameraFade != null)
        {
            ColorTo(cameraFade, args);
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
        }
    }

    public static void CameraFadeTo(float amount, float time)
    {
        if (cameraFade != null)
        {
            var args = new object[] { "amount", amount, "time", time };
            CameraFadeTo(Hash(args));
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
        }
    }

    public static Texture2D CameraTexture(Color color)
    {
        var textured = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        var colors = new Color[Screen.width * Screen.height];
        for (var i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }
        textured.SetPixels(colors);
        textured.Apply();
        return textured;
    }

    private static Hashtable CleanArgs(Hashtable args)
    {
        var hashtable = new Hashtable(args.Count);
        var hashtable2 = new Hashtable(args.Count);
        var enumerator = args.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = (DictionaryEntry) enumerator.Current;
                hashtable.Add(current.Key, current.Value);
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        var enumerator2 = hashtable.GetEnumerator();
        try
        {
            while (enumerator2.MoveNext())
            {
                var entry2 = (DictionaryEntry) enumerator2.Current;
                if (entry2.Value.GetType() == typeof(int))
                {
                    var num = (int) entry2.Value;
                    float num2 = num;
                    args[entry2.Key] = num2;
                }
                if (entry2.Value.GetType() == typeof(double))
                {
                    var num3 = (double) entry2.Value;
                    var num4 = (float) num3;
                    args[entry2.Key] = num4;
                }
            }
        }
        finally
        {
            var disposable2 = enumerator2 as IDisposable;
            if (disposable2 != null)
            	disposable2.Dispose();
        }
        var enumerator3 = args.GetEnumerator();
        try
        {
            while (enumerator3.MoveNext())
            {
                var entry3 = (DictionaryEntry) enumerator3.Current;
                var key = entry3.Key.ToString().ToLower();
                hashtable2.Add(key, entry3.Value);
            }
        }
        finally
        {
            var disposable3 = enumerator3 as IDisposable;
            if (disposable3 != null)
            	disposable3.Dispose();
        }
        args = hashtable2;
        return args;
    }

    private float clerp(float start, float end, float value)
    {
        var num = 0f;
        var num2 = 360f;
        var num3 = Mathf.Abs((num2 - num) * 0.5f);
        var num5 = 0f;
        if (end - start < -num3)
        {
            num5 = (num2 - start + end) * value;
            return start + num5;
        }
        if (end - start > num3)
        {
            num5 = -(num2 - end + start) * value;
            return start + num5;
        }
        return start + (end - start) * value;
    }

    public static void ColorFrom(GameObject target, Hashtable args)
    {
        var color = new Color();
        var color2 = new Color();
        args = CleanArgs(args);
        if (!args.Contains("includechildren") || (bool) args["includechildren"])
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    var hashtable = (Hashtable) args.Clone();
                    hashtable["ischild"] = true;
                    ColorFrom(current.gameObject, hashtable);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
        if (!args.Contains("easetype"))
        {
            args.Add("easetype", EaseType.linear);
        }
        if (target.GetComponent<GUITexture>() != null)
        {
            color2 = color = target.guiTexture.color;
        }
        else if (target.GetComponent<GUIText>() != null)
        {
            color2 = color = target.guiText.material.color;
        }
        else if (target.renderer != null)
        {
            color2 = color = target.renderer.material.color;
        }
        else if (target.light != null)
        {
            color2 = color = target.light.color;
        }
        if (args.Contains("color"))
        {
            color = (Color) args["color"];
        }
        else
        {
            if (args.Contains("r"))
            {
                color.r = (float) args["r"];
            }
            if (args.Contains("g"))
            {
                color.g = (float) args["g"];
            }
            if (args.Contains("b"))
            {
                color.b = (float) args["b"];
            }
            if (args.Contains("a"))
            {
                color.a = (float) args["a"];
            }
        }
        if (args.Contains("amount"))
        {
            color.a = (float) args["amount"];
            args.Remove("amount");
        }
        else if (args.Contains("alpha"))
        {
            color.a = (float) args["alpha"];
            args.Remove("alpha");
        }
        if (target.GetComponent<GUITexture>() != null)
        {
            target.guiTexture.color = color;
        }
        else if (target.GetComponent<GUIText>() != null)
        {
            target.guiText.material.color = color;
        }
        else if (target.renderer != null)
        {
            target.renderer.material.color = color;
        }
        else if (target.light != null)
        {
            target.light.color = color;
        }
        args["color"] = color2;
        args["type"] = "color";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void ColorFrom(GameObject target, Color color, float time)
    {
        var args = new object[] { "color", color, "time", time };
        ColorFrom(target, Hash(args));
    }

    public static void ColorTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (!args.Contains("includechildren") || (bool) args["includechildren"])
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    var hashtable = (Hashtable) args.Clone();
                    hashtable["ischild"] = true;
                    ColorTo(current.gameObject, hashtable);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
        if (!args.Contains("easetype"))
        {
            args.Add("easetype", EaseType.linear);
        }
        args["type"] = "color";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void ColorTo(GameObject target, Color color, float time)
    {
        var args = new object[] { "color", color, "time", time };
        ColorTo(target, Hash(args));
    }

    public static void ColorUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        CleanArgs(args);
        var colorArray = new Color[4];
        if (!args.Contains("includechildren") || (bool) args["includechildren"])
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    ColorUpdate(current.gameObject, args);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
        if (args.Contains("time"))
        {
            updateTime = (float) args["time"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        if (target.GetComponent<GUITexture>() != null)
        {
            colorArray[0] = colorArray[1] = target.guiTexture.color;
        }
        else if (target.GetComponent<GUIText>() != null)
        {
            colorArray[0] = colorArray[1] = target.guiText.material.color;
        }
        else if (target.renderer != null)
        {
            colorArray[0] = colorArray[1] = target.renderer.material.color;
        }
        else if (target.light != null)
        {
            colorArray[0] = colorArray[1] = target.light.color;
        }
        if (args.Contains("color"))
        {
            colorArray[1] = (Color) args["color"];
        }
        else
        {
            if (args.Contains("r"))
            {
                colorArray[1].r = (float) args["r"];
            }
            if (args.Contains("g"))
            {
                colorArray[1].g = (float) args["g"];
            }
            if (args.Contains("b"))
            {
                colorArray[1].b = (float) args["b"];
            }
            if (args.Contains("a"))
            {
                colorArray[1].a = (float) args["a"];
            }
        }
        colorArray[3].r = Mathf.SmoothDamp(colorArray[0].r, colorArray[1].r, ref colorArray[2].r, updateTime);
        colorArray[3].g = Mathf.SmoothDamp(colorArray[0].g, colorArray[1].g, ref colorArray[2].g, updateTime);
        colorArray[3].b = Mathf.SmoothDamp(colorArray[0].b, colorArray[1].b, ref colorArray[2].b, updateTime);
        colorArray[3].a = Mathf.SmoothDamp(colorArray[0].a, colorArray[1].a, ref colorArray[2].a, updateTime);
        if (target.GetComponent<GUITexture>() != null)
        {
            target.guiTexture.color = colorArray[3];
        }
        else if (target.GetComponent<GUIText>() != null)
        {
            target.guiText.material.color = colorArray[3];
        }
        else if (target.renderer != null)
        {
            target.renderer.material.color = colorArray[3];
        }
        else if (target.light != null)
        {
            target.light.color = colorArray[3];
        }
    }

    public static void ColorUpdate(GameObject target, Color color, float time)
    {
        var args = new object[] { "color", color, "time", time };
        ColorUpdate(target, Hash(args));
    }

    private void ConflictCheck()
    {
        foreach (var tween in GetComponents<iTween>())
        {
            if (tween.type == "value")
            {
                return;
            }
            if (tween.isRunning && tween.type == type)
            {
                if (tween.method != method)
                {
                    return;
                }
                if (tween.tweenArguments.Count != tweenArguments.Count)
                {
                    tween.Dispose();
                    return;
                }
                var enumerator = tweenArguments.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (DictionaryEntry) enumerator.Current;
                        if (!tween.tweenArguments.Contains(current.Key))
                        {
                            tween.Dispose();
                            break;
                        }
                        if (!tween.tweenArguments[current.Key].Equals(tweenArguments[current.Key]) && (string) current.Key != "id")
                        {
                            tween.Dispose();
                            break;
                        }
                    }
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                    	disposable.Dispose();
                }
                Dispose();
            }
        }
    }

    public static int Count()
    {
        return tweens.Count;
    }

    public static int Count(string type)
    {
        var num = 0;
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            if (((string) hashtable["type"] + (string) hashtable["method"]).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                num++;
            }
        }
        return num;
    }

    public static int Count(GameObject target)
    {
        return target.GetComponents<iTween>().Length;
    }

    public static int Count(GameObject target, string type)
    {
        var num = 0;
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                num++;
            }
        }
        return num;
    }

    private void DisableKinematic()
    {
    }

    private void Dispose()
    {
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            if ((string) hashtable["id"] == id)
            {
                tweens.RemoveAt(i);
                break;
            }
        }
        Destroy(this);
    }

    public static void DrawLine(Transform[] line)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, Defaults.color, "gizmos");
        }
    }

    public static void DrawLine(Vector3[] line)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, Defaults.color, "gizmos");
        }
    }

    public static void DrawLine(Transform[] line, Color color)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, color, "gizmos");
        }
    }

    public static void DrawLine(Vector3[] line, Color color)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, color, "gizmos");
        }
    }

    public static void DrawLineGizmos(Transform[] line)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, Defaults.color, "gizmos");
        }
    }

    public static void DrawLineGizmos(Vector3[] line)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, Defaults.color, "gizmos");
        }
    }

    public static void DrawLineGizmos(Transform[] line, Color color)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, color, "gizmos");
        }
    }

    public static void DrawLineGizmos(Vector3[] line, Color color)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, color, "gizmos");
        }
    }

    public static void DrawLineHandles(Transform[] line)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, Defaults.color, "handles");
        }
    }

    public static void DrawLineHandles(Vector3[] line)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, Defaults.color, "handles");
        }
    }

    public static void DrawLineHandles(Transform[] line, Color color)
    {
        if (line.Length > 0)
        {
            var vectorArray = new Vector3[line.Length];
            for (var i = 0; i < line.Length; i++)
            {
                vectorArray[i] = line[i].position;
            }
            DrawLineHelper(vectorArray, color, "handles");
        }
    }

    public static void DrawLineHandles(Vector3[] line, Color color)
    {
        if (line.Length > 0)
        {
            DrawLineHelper(line, color, "handles");
        }
    }

    private static void DrawLineHelper(Vector3[] line, Color color, string method)
    {
        Gizmos.color = color;
        for (var i = 0; i < line.Length - 1; i++)
        {
            if (method == "gizmos")
            {
                Gizmos.DrawLine(line[i], line[i + 1]);
            }
            else if (method == "handles")
            {
                UnityEngine.Debug.LogError("iTween Error: Drawing a line with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
            }
        }
    }

    public static void DrawPath(Transform[] path)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, Defaults.color, "gizmos");
        }
    }

    public static void DrawPath(Vector3[] path)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, Defaults.color, "gizmos");
        }
    }

    public static void DrawPath(Transform[] path, Color color)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, color, "gizmos");
        }
    }

    public static void DrawPath(Vector3[] path, Color color)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, color, "gizmos");
        }
    }

    public static void DrawPathGizmos(Transform[] path)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, Defaults.color, "gizmos");
        }
    }

    public static void DrawPathGizmos(Vector3[] path)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, Defaults.color, "gizmos");
        }
    }

    public static void DrawPathGizmos(Transform[] path, Color color)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, color, "gizmos");
        }
    }

    public static void DrawPathGizmos(Vector3[] path, Color color)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, color, "gizmos");
        }
    }

    public static void DrawPathHandles(Transform[] path)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, Defaults.color, "handles");
        }
    }

    public static void DrawPathHandles(Vector3[] path)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, Defaults.color, "handles");
        }
    }

    public static void DrawPathHandles(Transform[] path, Color color)
    {
        if (path.Length > 0)
        {
            var vectorArray = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++)
            {
                vectorArray[i] = path[i].position;
            }
            DrawPathHelper(vectorArray, color, "handles");
        }
    }

    public static void DrawPathHandles(Vector3[] path, Color color)
    {
        if (path.Length > 0)
        {
            DrawPathHelper(path, color, "handles");
        }
    }

    private static void DrawPathHelper(Vector3[] path, Color color, string method)
    {
        var pts = PathControlPointGenerator(path);
        var to = Interp(pts, 0f);
        Gizmos.color = color;
        var num = path.Length * 20;
        for (var i = 1; i <= num; i++)
        {
            var t = i / (float) num;
            var from = Interp(pts, t);
            if (method == "gizmos")
            {
                Gizmos.DrawLine(from, to);
            }
            else if (method == "handles")
            {
                UnityEngine.Debug.LogError("iTween Error: Drawing a path with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
            }
            to = from;
        }
    }

    private float easeInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1f;
        var num = 1.70158f;
        return end * value * value * ((num + 1f) * value - num) + start;
    }

    private float easeInBounce(float start, float end, float value)
    {
        end -= start;
        var num = 1f;
        return end - easeOutBounce(0f, end, num - value) + start;
    }

    private float easeInCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
    }

    private float easeInCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    private float easeInElastic(float start, float end, float value)
    {
        end -= start;
        var num = 1f;
        var num2 = num * 0.3f;
        var num3 = 0f;
        var num4 = 0f;
        if (value == 0f)
        {
            return start;
        }
        if ((value /= num) == 1f)
        {
            return start + end;
        }
        if (num4 == 0f || num4 < Mathf.Abs(end))
        {
            num4 = end;
            num3 = num2 / 4f;
        }
        else
        {
            num3 = num2 / 6.283185f * Mathf.Asin(end / num4);
        }
        return -(num4 * Mathf.Pow(2f, 10f * --value) * Mathf.Sin((value * num - num3) * 6.283185f / num2)) + start;
    }

    private float easeInExpo(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
    }

    private float easeInOutBack(float start, float end, float value)
    {
        var num = 1.70158f;
        end -= start;
        value /= 0.5f;
        if (value < 1f)
        {
            num *= 1.525f;
            return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
        }
        value -= 2f;
        num *= 1.525f;
        return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
    }

    private float easeInOutBounce(float start, float end, float value)
    {
        end -= start;
        var num = 1f;
        if (value < num * 0.5f)
        {
            return easeInBounce(0f, end, value * 2f) * 0.5f + start;
        }
        return easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
    }

    private float easeInOutCirc(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
        }
        value -= 2f;
        return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
    }

    private float easeInOutCubic(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return end * 0.5f * value * value * value + start;
        }
        value -= 2f;
        return end * 0.5f * (value * value * value + 2f) + start;
    }

    private float easeInOutElastic(float start, float end, float value)
    {
        end -= start;
        var num = 1f;
        var num2 = num * 0.3f;
        var num3 = 0f;
        var num4 = 0f;
        if (value == 0f)
        {
            return start;
        }
        if ((value /= num * 0.5f) == 2f)
        {
            return start + end;
        }
        if (num4 == 0f || num4 < Mathf.Abs(end))
        {
            num4 = end;
            num3 = num2 / 4f;
        }
        else
        {
            num3 = num2 / 6.283185f * Mathf.Asin(end / num4);
        }
        if (value < 1f)
        {
            return -0.5f * (num4 * Mathf.Pow(2f, 10f * --value) * Mathf.Sin((value * num - num3) * 6.283185f / num2)) + start;
        }
        return num4 * Mathf.Pow(2f, -10f * --value) * Mathf.Sin((value * num - num3) * 6.283185f / num2) * 0.5f + end + start;
    }

    private float easeInOutExpo(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
        }
        value--;
        return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
    }

    private float easeInOutQuad(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return end * 0.5f * value * value + start;
        }
        value--;
        return -end * 0.5f * (value * (value - 2f) - 1f) + start;
    }

    private float easeInOutQuart(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return end * 0.5f * value * value * value * value + start;
        }
        value -= 2f;
        return -end * 0.5f * (value * value * value * value - 2f) + start;
    }

    private float easeInOutQuint(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1f)
        {
            return end * 0.5f * value * value * value * value * value + start;
        }
        value -= 2f;
        return end * 0.5f * (value * value * value * value * value + 2f) + start;
    }

    private float easeInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (Mathf.Cos(3.141593f * value) - 1f) + start;
    }

    private float easeInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    private float easeInQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    private float easeInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    private float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value * 1.570796f) + end + start;
    }

    private float easeOutBack(float start, float end, float value)
    {
        var num = 1.70158f;
        end -= start;
        value--;
        return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
    }

    private float easeOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < 0.3636364f)
        {
            return end * (7.5625f * value * value) + start;
        }
        if (value < 0.7272727f)
        {
            value -= 0.5454546f;
            return end * (7.5625f * value * value + 0.75f) + start;
        }
        if (value < 0.90909090909090906)
        {
            value -= 0.8181818f;
            return end * (7.5625f * value * value + 0.9375f) + start;
        }
        value -= 0.9545454f;
        return end * (7.5625f * value * value + 0.984375f) + start;
    }

    private float easeOutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1f - value * value) + start;
    }

    private float easeOutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1f) + start;
    }

    private float easeOutElastic(float start, float end, float value)
    {
        end -= start;
        var num = 1f;
        var num2 = num * 0.3f;
        var num3 = 0f;
        var num4 = 0f;
        if (value == 0f)
        {
            return start;
        }
        if ((value /= num) == 1f)
        {
            return start + end;
        }
        if (num4 == 0f || num4 < Mathf.Abs(end))
        {
            num4 = end;
            num3 = num2 * 0.25f;
        }
        else
        {
            num3 = num2 / 6.283185f * Mathf.Asin(end / num4);
        }
        return num4 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num3) * 6.283185f / num2) + end + start;
    }

    private float easeOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
    }

    private float easeOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2f) + start;
    }

    private float easeOutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1f) + start;
    }

    private float easeOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1f) + start;
    }

    private float easeOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value * 1.570796f) + start;
    }

    private void EnableKinematic()
    {
    }

    public static void FadeFrom(GameObject target, Hashtable args)
    {
        ColorFrom(target, args);
    }

    public static void FadeFrom(GameObject target, float alpha, float time)
    {
        var args = new object[] { "alpha", alpha, "time", time };
        FadeFrom(target, Hash(args));
    }

    public static void FadeTo(GameObject target, Hashtable args)
    {
        ColorTo(target, args);
    }

    public static void FadeTo(GameObject target, float alpha, float time)
    {
        var args = new object[] { "alpha", alpha, "time", time };
        FadeTo(target, Hash(args));
    }

    public static void FadeUpdate(GameObject target, Hashtable args)
    {
        args["a"] = args["alpha"];
        ColorUpdate(target, args);
    }

    public static void FadeUpdate(GameObject target, float alpha, float time)
    {
        var args = new object[] { "alpha", alpha, "time", time };
        FadeUpdate(target, Hash(args));
    }

    private void FixedUpdate()
    {
        if (isRunning && physics)
        {
            if (!reverse)
            {
                if (percentage < 1f)
                {
                    TweenUpdate();
                }
                else
                {
                    TweenComplete();
                }
            }
            else if (percentage > 0f)
            {
                TweenUpdate();
            }
            else
            {
                TweenComplete();
            }
        }
    }

    public static float FloatUpdate(float currentValue, float targetValue, float speed)
    {
        var num = targetValue - currentValue;
        currentValue += num * speed * Time.deltaTime;
        return currentValue;
    }

    private void GenerateAudioToTargets()
    {
        vector2s = new Vector2[3];
        if (tweenArguments.Contains("audiosource"))
        {
            audioSource = (AudioSource) tweenArguments["audiosource"];
        }
        else if (GetComponent<AudioSource>() != null)
        {
            audioSource = audio;
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: AudioTo requires an AudioSource.");
            Dispose();
        }
        vector2s[0] = vector2s[1] = new Vector2(audioSource.volume, audioSource.pitch);
        if (tweenArguments.Contains("volume"))
        {
            vector2s[1].x = (float) tweenArguments["volume"];
        }
        if (tweenArguments.Contains("pitch"))
        {
            vector2s[1].y = (float) tweenArguments["pitch"];
        }
    }

    private void GenerateColorTargets()
    {
        colors = new Color[1, 3];
        colors[0, 0] = (Color) tweenArguments["from"];
        colors[0, 1] = (Color) tweenArguments["to"];
    }

    private void GenerateColorToTargets()
    {
        if (GetComponent<GUITexture>() != null)
        {
            colors = new Color[1, 3];
            colors[0, 0] = colors[0, 1] = guiTexture.color;
        }
        else if (GetComponent<GUIText>() != null)
        {
            colors = new Color[1, 3];
            colors[0, 0] = colors[0, 1] = guiText.material.color;
        }
        else if (renderer != null)
        {
            colors = new Color[renderer.materials.Length, 3];
            for (var i = 0; i < renderer.materials.Length; i++)
            {
                colors[i, 0] = renderer.materials[i].GetColor(namedcolorvalue.ToString());
                colors[i, 1] = renderer.materials[i].GetColor(namedcolorvalue.ToString());
            }
        }
        else if (light != null)
        {
            colors = new Color[1, 3];
            colors[0, 0] = colors[0, 1] = light.color;
        }
        else
        {
            colors = new Color[1, 3];
        }
        if (tweenArguments.Contains("color"))
        {
            for (var j = 0; j < colors.GetLength(0); j++)
            {
                colors[j, 1] = (Color) tweenArguments["color"];
            }
        }
        else
        {
            if (tweenArguments.Contains("r"))
            {
                for (var k = 0; k < colors.GetLength(0); k++)
                {
                    colors[k, 1].r = (float) tweenArguments["r"];
                }
            }
            if (tweenArguments.Contains("g"))
            {
                for (var m = 0; m < colors.GetLength(0); m++)
                {
                    colors[m, 1].g = (float) tweenArguments["g"];
                }
            }
            if (tweenArguments.Contains("b"))
            {
                for (var n = 0; n < colors.GetLength(0); n++)
                {
                    colors[n, 1].b = (float) tweenArguments["b"];
                }
            }
            if (tweenArguments.Contains("a"))
            {
                for (var num6 = 0; num6 < colors.GetLength(0); num6++)
                {
                    colors[num6, 1].a = (float) tweenArguments["a"];
                }
            }
        }
        if (tweenArguments.Contains("amount"))
        {
            for (var num7 = 0; num7 < colors.GetLength(0); num7++)
            {
                colors[num7, 1].a = (float) tweenArguments["amount"];
            }
        }
        else if (tweenArguments.Contains("alpha"))
        {
            for (var num8 = 0; num8 < colors.GetLength(0); num8++)
            {
                colors[num8, 1].a = (float) tweenArguments["alpha"];
            }
        }
    }

    private void GenerateFloatTargets()
    {
        floats = new float[3];
        floats[0] = (float) tweenArguments["from"];
        floats[1] = (float) tweenArguments["to"];
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(floats[0] - floats[1]);
            time = num / (float) tweenArguments["speed"];
        }
    }

    private static string GenerateID()
    {
        return Guid.NewGuid().ToString();
    }

    private void GenerateLookToTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = thisTransform.eulerAngles;
        if (tweenArguments.Contains("looktarget"))
        {
            if (tweenArguments["looktarget"].GetType() == typeof(Transform))
            {
                var nullable = (Vector3?) tweenArguments["up"];
                thisTransform.LookAt((Transform) tweenArguments["looktarget"], !nullable.HasValue ? Defaults.up : nullable.Value);
            }
            else if (tweenArguments["looktarget"].GetType() == typeof(Vector3))
            {
                var nullable2 = (Vector3?) tweenArguments["up"];
                thisTransform.LookAt((Vector3) tweenArguments["looktarget"], !nullable2.HasValue ? Defaults.up : nullable2.Value);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: LookTo needs a 'looktarget' property!");
            Dispose();
        }
        vector3s[1] = thisTransform.eulerAngles;
        thisTransform.eulerAngles = vector3s[0];
        if (tweenArguments.Contains("axis"))
        {
            var key = (string) tweenArguments["axis"];
            if (key != null)
            {
                int num;
                if (fswitchSmap13 == null)
                {
                    var dictionary = new Dictionary<string, int>(3);
                    dictionary.Add("x", 0);
                    dictionary.Add("y", 1);
                    dictionary.Add("z", 2);
                    fswitchSmap13 = dictionary;
                }
                if (fswitchSmap13.TryGetValue(key, out num))
                {
                    switch (num)
                    {
                        case 0:
                            vector3s[1].y = vector3s[0].y;
                            vector3s[1].z = vector3s[0].z;
                            break;

                        case 1:
                            vector3s[1].x = vector3s[0].x;
                            vector3s[1].z = vector3s[0].z;
                            break;

                        case 2:
                            vector3s[1].x = vector3s[0].x;
                            vector3s[1].y = vector3s[0].y;
                            break;
                    }
                }
            }
        }
        vector3s[1] = new Vector3(clerp(vector3s[0].x, vector3s[1].x, 1f), clerp(vector3s[0].y, vector3s[1].y, 1f), clerp(vector3s[0].z, vector3s[1].z, 1f));
        if (tweenArguments.Contains("speed"))
        {
            var num2 = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num2 / (float) tweenArguments["speed"];
        }
    }

    private void GenerateMoveByTargets()
    {
        Vector3 vector;
        vector3s = new Vector3[6];
        vector3s[4] = thisTransform.eulerAngles;
        vector3s[3] = vector = thisTransform.position;
        vector3s[0] = vector3s[1] = vector;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = vector3s[0] + (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = vector3s[0].x + (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = vector3s[0].y + (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = vector3s[0].z + (float) tweenArguments["z"];
            }
        }
        thisTransform.Translate(vector3s[1], space);
        vector3s[5] = thisTransform.position;
        thisTransform.position = vector3s[0];
        if (tweenArguments.Contains("orienttopath") && (bool) tweenArguments["orienttopath"])
        {
            tweenArguments["looktarget"] = vector3s[1];
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateMoveToPathTargets()
    {
        Vector3[] vectorArray2;
        bool flag;
        int num2;
        if (tweenArguments["path"].GetType() == typeof(Vector3[]))
        {
            var sourceArray = (Vector3[]) tweenArguments["path"];
            if (sourceArray.Length == 1)
            {
                UnityEngine.Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
                Dispose();
            }
            vectorArray2 = new Vector3[sourceArray.Length];
            Array.Copy(sourceArray, vectorArray2, sourceArray.Length);
        }
        else
        {
            var transformArray = (Transform[]) tweenArguments["path"];
            if (transformArray.Length == 1)
            {
                UnityEngine.Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
                Dispose();
            }
            vectorArray2 = new Vector3[transformArray.Length];
            for (var i = 0; i < transformArray.Length; i++)
            {
                vectorArray2[i] = transformArray[i].position;
            }
        }
        if (thisTransform.position != vectorArray2[0])
        {
            if (!tweenArguments.Contains("movetopath") || (bool) tweenArguments["movetopath"])
            {
                flag = true;
                num2 = 3;
            }
            else
            {
                flag = false;
                num2 = 2;
            }
        }
        else
        {
            flag = false;
            num2 = 2;
        }
        vector3s = new Vector3[vectorArray2.Length + num2];
        if (flag)
        {
            vector3s[1] = thisTransform.position;
            num2 = 2;
        }
        else
        {
            num2 = 1;
        }
        Array.Copy(vectorArray2, 0, vector3s, num2, vectorArray2.Length);
        vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
        vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
        if (vector3s[1] == vector3s[vector3s.Length - 2])
        {
            var destinationArray = new Vector3[vector3s.Length];
            Array.Copy(vector3s, destinationArray, vector3s.Length);
            destinationArray[0] = destinationArray[destinationArray.Length - 3];
            destinationArray[destinationArray.Length - 1] = destinationArray[2];
            vector3s = new Vector3[destinationArray.Length];
            Array.Copy(destinationArray, vector3s, destinationArray.Length);
        }
        path = new CRSpline(vector3s);
        if (tweenArguments.Contains("speed"))
        {
            var num3 = PathLength(vector3s);
            time = num3 / (float) tweenArguments["speed"];
        }
    }

    private void GenerateMoveToTargets()
    {
        vector3s = new Vector3[3];
        if (isLocal)
        {
            vector3s[0] = vector3s[1] = thisTransform.localPosition;
        }
        else
        {
            vector3s[0] = vector3s[1] = thisTransform.position;
        }
        if (tweenArguments.Contains("position"))
        {
            if (tweenArguments["position"].GetType() == typeof(Transform))
            {
                var transform = (Transform) tweenArguments["position"];
                vector3s[1] = transform.position;
            }
            else if (tweenArguments["position"].GetType() == typeof(Vector3))
            {
                vector3s[1] = (Vector3) tweenArguments["position"];
            }
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("orienttopath") && (bool) tweenArguments["orienttopath"])
        {
            tweenArguments["looktarget"] = vector3s[1];
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GeneratePunchPositionTargets()
    {
        vector3s = new Vector3[5];
        vector3s[4] = thisTransform.eulerAngles;
        vector3s[0] = thisTransform.position;
        vector3s[1] = vector3s[3] = Vector3.zero;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GeneratePunchRotationTargets()
    {
        vector3s = new Vector3[4];
        vector3s[0] = thisTransform.eulerAngles;
        vector3s[1] = vector3s[3] = Vector3.zero;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GeneratePunchScaleTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = thisTransform.localScale;
        vector3s[1] = Vector3.zero;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GenerateRectTargets()
    {
        rects = new Rect[3];
        rects[0] = (Rect) tweenArguments["from"];
        rects[1] = (Rect) tweenArguments["to"];
    }

    private void GenerateRotateAddTargets()
    {
        Vector3 vector;
        vector3s = new Vector3[5];
        vector3s[3] = vector = thisTransform.eulerAngles;
        vector3s[0] = vector3s[1] = vector;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] += (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x += (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y += (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z += (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateRotateByTargets()
    {
        Vector3 vector;
        vector3s = new Vector3[4];
        vector3s[3] = vector = thisTransform.eulerAngles;
        vector3s[0] = vector3s[1] = vector;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] += Vector3.Scale((Vector3) tweenArguments["amount"], new Vector3(360f, 360f, 360f));
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x += 360f * (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y += 360f * (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z += 360f * (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateRotateToTargets()
    {
        vector3s = new Vector3[3];
        if (isLocal)
        {
            vector3s[0] = vector3s[1] = thisTransform.localEulerAngles;
        }
        else
        {
            vector3s[0] = vector3s[1] = thisTransform.eulerAngles;
        }
        if (tweenArguments.Contains("rotation"))
        {
            if (tweenArguments["rotation"].GetType() == typeof(Transform))
            {
                var transform = (Transform) tweenArguments["rotation"];
                vector3s[1] = transform.eulerAngles;
            }
            else if (tweenArguments["rotation"].GetType() == typeof(Vector3))
            {
                vector3s[1] = (Vector3) tweenArguments["rotation"];
            }
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
        vector3s[1] = new Vector3(clerp(vector3s[0].x, vector3s[1].x, 1f), clerp(vector3s[0].y, vector3s[1].y, 1f), clerp(vector3s[0].z, vector3s[1].z, 1f));
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateScaleAddTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = vector3s[1] = thisTransform.localScale;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] += (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x += (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y += (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z += (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateScaleByTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = vector3s[1] = thisTransform.localScale;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = Vector3.Scale(vector3s[1], (Vector3) tweenArguments["amount"]);
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x *= (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y *= (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z *= (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateScaleToTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = vector3s[1] = thisTransform.localScale;
        if (tweenArguments.Contains("scale"))
        {
            if (tweenArguments["scale"].GetType() == typeof(Transform))
            {
                var transform = (Transform) tweenArguments["scale"];
                vector3s[1] = transform.localScale;
            }
            else if (tweenArguments["scale"].GetType() == typeof(Vector3))
            {
                vector3s[1] = (Vector3) tweenArguments["scale"];
            }
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateShakePositionTargets()
    {
        vector3s = new Vector3[4];
        vector3s[3] = thisTransform.eulerAngles;
        vector3s[0] = thisTransform.position;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GenerateShakeRotationTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = thisTransform.eulerAngles;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GenerateShakeScaleTargets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = thisTransform.localScale;
        if (tweenArguments.Contains("amount"))
        {
            vector3s[1] = (Vector3) tweenArguments["amount"];
        }
        else
        {
            if (tweenArguments.Contains("x"))
            {
                vector3s[1].x = (float) tweenArguments["x"];
            }
            if (tweenArguments.Contains("y"))
            {
                vector3s[1].y = (float) tweenArguments["y"];
            }
            if (tweenArguments.Contains("z"))
            {
                vector3s[1].z = (float) tweenArguments["z"];
            }
        }
    }

    private void GenerateStabTargets()
    {
        if (tweenArguments.Contains("audiosource"))
        {
            audioSource = (AudioSource) tweenArguments["audiosource"];
        }
        else if (GetComponent<AudioSource>() != null)
        {
            audioSource = audio;
        }
        else
        {
            gameObject.AddComponent<AudioSource>();
            audioSource = audio;
            audioSource.playOnAwake = false;
        }
        audioSource.clip = (AudioClip) tweenArguments["audioclip"];
        if (tweenArguments.Contains("pitch"))
        {
            audioSource.pitch = (float) tweenArguments["pitch"];
        }
        if (tweenArguments.Contains("volume"))
        {
            audioSource.volume = (float) tweenArguments["volume"];
        }
        time = audioSource.clip.length / audioSource.pitch;
    }

    private void GenerateTargets()
    {
        var type = this.type;
        if (type != null)
        {
            Dictionary<string, int> dictionary;
            int num;
            if (fswitchSmap12 == null)
            {
                dictionary = new Dictionary<string, int>(10);
                dictionary.Add("value", 0);
                dictionary.Add("color", 1);
                dictionary.Add("audio", 2);
                dictionary.Add("move", 3);
                dictionary.Add("scale", 4);
                dictionary.Add("rotate", 5);
                dictionary.Add("shake", 6);
                dictionary.Add("punch", 7);
                dictionary.Add("look", 8);
                dictionary.Add("stab", 9);
                fswitchSmap12 = dictionary;
            }
            if (fswitchSmap12.TryGetValue(type, out num))
            {
                string method;
                int num2;
                switch (num)
                {
                    case 0:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmap9 == null)
                            {
                                dictionary = new Dictionary<string, int>(5);
                                dictionary.Add("float", 0);
                                dictionary.Add("vector2", 1);
                                dictionary.Add("vector3", 2);
                                dictionary.Add("color", 3);
                                dictionary.Add("rect", 4);
                                fswitchSmap9 = dictionary;
                            }
                            if (fswitchSmap9.TryGetValue(method, out num2))
                            {
                                switch (num2)
                                {
                                    case 0:
                                        GenerateFloatTargets();
                                        apply = new ApplyTween(ApplyFloatTargets);
                                        return;

                                    case 1:
                                        GenerateVector2Targets();
                                        apply = new ApplyTween(ApplyVector2Targets);
                                        return;

                                    case 2:
                                        GenerateVector3Targets();
                                        apply = new ApplyTween(ApplyVector3Targets);
                                        return;

                                    case 3:
                                        GenerateColorTargets();
                                        apply = new ApplyTween(ApplyColorTargets);
                                        return;

                                    case 4:
                                        GenerateRectTargets();
                                        apply = new ApplyTween(ApplyRectTargets);
                                        return;
                                }
                            }
                        }
                        break;

                    case 1:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapA == null)
                            {
                                dictionary = new Dictionary<string, int>(1);
                                dictionary.Add("to", 0);
                                fswitchSmapA = dictionary;
                            }
                            if (fswitchSmapA.TryGetValue(method, out num2) && num2 == 0)
                            {
                                GenerateColorToTargets();
                                apply = new ApplyTween(ApplyColorToTargets);
                            }
                        }
                        break;

                    case 2:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapB == null)
                            {
                                dictionary = new Dictionary<string, int>(1);
                                dictionary.Add("to", 0);
                                fswitchSmapB = dictionary;
                            }
                            if (fswitchSmapB.TryGetValue(method, out num2) && num2 == 0)
                            {
                                GenerateAudioToTargets();
                                apply = new ApplyTween(ApplyAudioToTargets);
                            }
                        }
                        break;

                    case 3:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapC == null)
                            {
                                dictionary = new Dictionary<string, int>(3);
                                dictionary.Add("to", 0);
                                dictionary.Add("by", 1);
                                dictionary.Add("add", 1);
                                fswitchSmapC = dictionary;
                            }
                            if (fswitchSmapC.TryGetValue(method, out num2))
                            {
                                if (num2 == 0)
                                {
                                    if (tweenArguments.Contains("path"))
                                    {
                                        GenerateMoveToPathTargets();
                                        apply = new ApplyTween(ApplyMoveToPathTargets);
                                    }
                                    else
                                    {
                                        GenerateMoveToTargets();
                                        apply = new ApplyTween(ApplyMoveToTargets);
                                    }
                                }
                                else if (num2 == 1)
                                {
                                    GenerateMoveByTargets();
                                    apply = new ApplyTween(ApplyMoveByTargets);
                                    break;
                                }
                            }
                        }
                        break;

                    case 4:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapD == null)
                            {
                                dictionary = new Dictionary<string, int>(3);
                                dictionary.Add("to", 0);
                                dictionary.Add("by", 1);
                                dictionary.Add("add", 2);
                                fswitchSmapD = dictionary;
                            }
                            if (fswitchSmapD.TryGetValue(method, out num2))
                            {
                                switch (num2)
                                {
                                    case 0:
                                        GenerateScaleToTargets();
                                        apply = new ApplyTween(ApplyScaleToTargets);
                                        return;

                                    case 1:
                                        GenerateScaleByTargets();
                                        apply = new ApplyTween(ApplyScaleToTargets);
                                        return;

                                    case 2:
                                        GenerateScaleAddTargets();
                                        apply = new ApplyTween(ApplyScaleToTargets);
                                        return;
                                }
                            }
                        }
                        break;

                    case 5:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapE == null)
                            {
                                dictionary = new Dictionary<string, int>(3);
                                dictionary.Add("to", 0);
                                dictionary.Add("add", 1);
                                dictionary.Add("by", 2);
                                fswitchSmapE = dictionary;
                            }
                            if (fswitchSmapE.TryGetValue(method, out num2))
                            {
                                switch (num2)
                                {
                                    case 0:
                                        GenerateRotateToTargets();
                                        apply = new ApplyTween(ApplyRotateToTargets);
                                        return;

                                    case 1:
                                        GenerateRotateAddTargets();
                                        apply = new ApplyTween(ApplyRotateAddTargets);
                                        return;

                                    case 2:
                                        GenerateRotateByTargets();
                                        apply = new ApplyTween(ApplyRotateAddTargets);
                                        return;
                                }
                            }
                        }
                        break;

                    case 6:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmapF == null)
                            {
                                dictionary = new Dictionary<string, int>(3);
                                dictionary.Add("position", 0);
                                dictionary.Add("scale", 1);
                                dictionary.Add("rotation", 2);
                                fswitchSmapF = dictionary;
                            }
                            if (fswitchSmapF.TryGetValue(method, out num2))
                            {
                                switch (num2)
                                {
                                    case 0:
                                        GenerateShakePositionTargets();
                                        apply = new ApplyTween(ApplyShakePositionTargets);
                                        return;

                                    case 1:
                                        GenerateShakeScaleTargets();
                                        apply = new ApplyTween(ApplyShakeScaleTargets);
                                        return;

                                    case 2:
                                        GenerateShakeRotationTargets();
                                        apply = new ApplyTween(ApplyShakeRotationTargets);
                                        return;
                                }
                            }
                        }
                        break;

                    case 7:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmap10 == null)
                            {
                                dictionary = new Dictionary<string, int>(3);
                                dictionary.Add("position", 0);
                                dictionary.Add("rotation", 1);
                                dictionary.Add("scale", 2);
                                fswitchSmap10 = dictionary;
                            }
                            if (fswitchSmap10.TryGetValue(method, out num2))
                            {
                                switch (num2)
                                {
                                    case 0:
                                        GeneratePunchPositionTargets();
                                        apply = new ApplyTween(ApplyPunchPositionTargets);
                                        return;

                                    case 1:
                                        GeneratePunchRotationTargets();
                                        apply = new ApplyTween(ApplyPunchRotationTargets);
                                        return;

                                    case 2:
                                        GeneratePunchScaleTargets();
                                        apply = new ApplyTween(ApplyPunchScaleTargets);
                                        return;
                                }
                            }
                        }
                        break;

                    case 8:
                        method = this.method;
                        if (method != null)
                        {
                            if (fswitchSmap11 == null)
                            {
                                dictionary = new Dictionary<string, int>(1);
                                dictionary.Add("to", 0);
                                fswitchSmap11 = dictionary;
                            }
                            if (fswitchSmap11.TryGetValue(method, out num2) && num2 == 0)
                            {
                                GenerateLookToTargets();
                                apply = new ApplyTween(ApplyLookToTargets);
                            }
                        }
                        break;

                    case 9:
                        GenerateStabTargets();
                        apply = new ApplyTween(ApplyStabTargets);
                        break;
                }
            }
        }
    }

    private void GenerateVector2Targets()
    {
        vector2s = new Vector2[3];
        vector2s[0] = (Vector2) tweenArguments["from"];
        vector2s[1] = (Vector2) tweenArguments["to"];
        if (tweenArguments.Contains("speed"))
        {
            var a = new Vector3(vector2s[0].x, vector2s[0].y, 0f);
            var b = new Vector3(vector2s[1].x, vector2s[1].y, 0f);
            var num = Math.Abs(Vector3.Distance(a, b));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GenerateVector3Targets()
    {
        vector3s = new Vector3[3];
        vector3s[0] = (Vector3) tweenArguments["from"];
        vector3s[1] = (Vector3) tweenArguments["to"];
        if (tweenArguments.Contains("speed"))
        {
            var num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
            time = num / (float) tweenArguments["speed"];
        }
    }

    private void GetEasingFunction()
    {
        switch (easeType)
        {
            case EaseType.easeInQuad:
                ease = new EasingFunction(easeInQuad);
                break;

            case EaseType.easeOutQuad:
                ease = new EasingFunction(easeOutQuad);
                break;

            case EaseType.easeInOutQuad:
                ease = new EasingFunction(easeInOutQuad);
                break;

            case EaseType.easeInCubic:
                ease = new EasingFunction(easeInCubic);
                break;

            case EaseType.easeOutCubic:
                ease = new EasingFunction(easeOutCubic);
                break;

            case EaseType.easeInOutCubic:
                ease = new EasingFunction(easeInOutCubic);
                break;

            case EaseType.easeInQuart:
                ease = new EasingFunction(easeInQuart);
                break;

            case EaseType.easeOutQuart:
                ease = new EasingFunction(easeOutQuart);
                break;

            case EaseType.easeInOutQuart:
                ease = new EasingFunction(easeInOutQuart);
                break;

            case EaseType.easeInQuint:
                ease = new EasingFunction(easeInQuint);
                break;

            case EaseType.easeOutQuint:
                ease = new EasingFunction(easeOutQuint);
                break;

            case EaseType.easeInOutQuint:
                ease = new EasingFunction(easeInOutQuint);
                break;

            case EaseType.easeInSine:
                ease = new EasingFunction(easeInSine);
                break;

            case EaseType.easeOutSine:
                ease = new EasingFunction(easeOutSine);
                break;

            case EaseType.easeInOutSine:
                ease = new EasingFunction(easeInOutSine);
                break;

            case EaseType.easeInExpo:
                ease = new EasingFunction(easeInExpo);
                break;

            case EaseType.easeOutExpo:
                ease = new EasingFunction(easeOutExpo);
                break;

            case EaseType.easeInOutExpo:
                ease = new EasingFunction(easeInOutExpo);
                break;

            case EaseType.easeInCirc:
                ease = new EasingFunction(easeInCirc);
                break;

            case EaseType.easeOutCirc:
                ease = new EasingFunction(easeOutCirc);
                break;

            case EaseType.easeInOutCirc:
                ease = new EasingFunction(easeInOutCirc);
                break;

            case EaseType.linear:
                ease = new EasingFunction(linear);
                break;

            case EaseType.spring:
                ease = new EasingFunction(spring);
                break;

            case EaseType.easeInBounce:
                ease = new EasingFunction(easeInBounce);
                break;

            case EaseType.easeOutBounce:
                ease = new EasingFunction(easeOutBounce);
                break;

            case EaseType.easeInOutBounce:
                ease = new EasingFunction(easeInOutBounce);
                break;

            case EaseType.easeInBack:
                ease = new EasingFunction(easeInBack);
                break;

            case EaseType.easeOutBack:
                ease = new EasingFunction(easeOutBack);
                break;

            case EaseType.easeInOutBack:
                ease = new EasingFunction(easeInOutBack);
                break;

            case EaseType.easeInElastic:
                ease = new EasingFunction(easeInElastic);
                break;

            case EaseType.easeOutElastic:
                ease = new EasingFunction(easeOutElastic);
                break;

            case EaseType.easeInOutElastic:
                ease = new EasingFunction(easeInOutElastic);
                break;
        }
    }

    public static Hashtable Hash(params object[] args)
    {
        var hashtable = new Hashtable(args.Length / 2);
        if (args.Length % 2 != 0)
        {
            UnityEngine.Debug.LogError("Tween Error: Hash requires an even number of arguments!");
            return null;
        }
        for (var i = 0; i < args.Length - 1; i += 2)
        {
            hashtable.Add(args[i], args[i + 1]);
        }
        return hashtable;
    }

    public static void Init(GameObject target)
    {
        MoveBy(target, Vector3.zero, 0f);
    }

    private static Vector3 Interp(Vector3[] pts, float t)
    {
        var num = pts.Length - 3;
        var index = Mathf.Min(Mathf.FloorToInt(t * num), num - 1);
        var num3 = t * num - index;
        var vector = pts[index];
        var vector2 = pts[index + 1];
        var vector3 = pts[index + 2];
        var vector4 = pts[index + 3];
        return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
    }

    private void LateUpdate()
    {
        if (tweenArguments.Contains("looktarget") && isRunning && (type == "move" || type == "shake" || type == "punch"))
        {
            LookUpdate(gameObject, tweenArguments);
        }
    }

    private static void Launch(GameObject target, Hashtable args)
    {
        if (!args.Contains("id"))
        {
            args["id"] = GenerateID();
        }
        if (!args.Contains("target"))
        {
            args["target"] = target;
        }
        tweens.Insert(0, args);
        target.AddComponent<iTween>();
    }

    private float linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    public static void LookFrom(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        var eulerAngles = target.transform.eulerAngles;
        if (args["looktarget"].GetType() == typeof(Transform))
        {
            var nullable = (Vector3?) args["up"];
            target.transform.LookAt((Transform) args["looktarget"], !nullable.HasValue ? Defaults.up : nullable.Value);
        }
        else if (args["looktarget"].GetType() == typeof(Vector3))
        {
            var nullable2 = (Vector3?) args["up"];
            target.transform.LookAt((Vector3) args["looktarget"], !nullable2.HasValue ? Defaults.up : nullable2.Value);
        }
        if (args.Contains("axis"))
        {
            var vector2 = target.transform.eulerAngles;
            var key = (string) args["axis"];
            if (key != null)
            {
                int num;
                if (fswitchSmap8 == null)
                {
                    var dictionary = new Dictionary<string, int>(3);
                    dictionary.Add("x", 0);
                    dictionary.Add("y", 1);
                    dictionary.Add("z", 2);
                    fswitchSmap8 = dictionary;
                }
                if (fswitchSmap8.TryGetValue(key, out num))
                {
                    switch (num)
                    {
                        case 0:
                            vector2.y = eulerAngles.y;
                            vector2.z = eulerAngles.z;
                            break;

                        case 1:
                            vector2.x = eulerAngles.x;
                            vector2.z = eulerAngles.z;
                            break;

                        case 2:
                            vector2.x = eulerAngles.x;
                            vector2.y = eulerAngles.y;
                            break;
                    }
                }
            }
            target.transform.eulerAngles = vector2;
        }
        args["rotation"] = eulerAngles;
        args["type"] = "rotate";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void LookFrom(GameObject target, Vector3 looktarget, float time)
    {
        var args = new object[] { "looktarget", looktarget, "time", time };
        LookFrom(target, Hash(args));
    }

    public static void LookTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
        {
            var transform = (Transform) args["looktarget"];
            args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        args["type"] = "look";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void LookTo(GameObject target, Vector3 looktarget, float time)
    {
        var args = new object[] { "looktarget", looktarget, "time", time };
        LookTo(target, Hash(args));
    }

    public static void LookUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        CleanArgs(args);
        var vectorArray = new Vector3[5];
        if (args.Contains("looktime"))
        {
            updateTime = (float) args["looktime"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else if (args.Contains("time"))
        {
            updateTime = (float) args["time"] * 0.15f;
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        vectorArray[0] = target.transform.eulerAngles;
        if (args.Contains("looktarget"))
        {
            if (args["looktarget"].GetType() == typeof(Transform))
            {
                var nullable = (Vector3?) args["up"];
                target.transform.LookAt((Transform) args["looktarget"], !nullable.HasValue ? Defaults.up : nullable.Value);
            }
            else if (args["looktarget"].GetType() == typeof(Vector3))
            {
                var nullable2 = (Vector3?) args["up"];
                target.transform.LookAt((Vector3) args["looktarget"], !nullable2.HasValue ? Defaults.up : nullable2.Value);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("iTween Error: LookUpdate needs a 'looktarget' property!");
            return;
        }
        vectorArray[1] = target.transform.eulerAngles;
        target.transform.eulerAngles = vectorArray[0];
        vectorArray[3].x = Mathf.SmoothDampAngle(vectorArray[0].x, vectorArray[1].x, ref vectorArray[2].x, updateTime);
        vectorArray[3].y = Mathf.SmoothDampAngle(vectorArray[0].y, vectorArray[1].y, ref vectorArray[2].y, updateTime);
        vectorArray[3].z = Mathf.SmoothDampAngle(vectorArray[0].z, vectorArray[1].z, ref vectorArray[2].z, updateTime);
        target.transform.eulerAngles = vectorArray[3];
        if (args.Contains("axis"))
        {
            vectorArray[4] = target.transform.eulerAngles;
            var key = (string) args["axis"];
            if (key != null)
            {
                int num2;
                if (fswitchSmap14 == null)
                {
                    var dictionary = new Dictionary<string, int>(3);
                    dictionary.Add("x", 0);
                    dictionary.Add("y", 1);
                    dictionary.Add("z", 2);
                    fswitchSmap14 = dictionary;
                }
                if (fswitchSmap14.TryGetValue(key, out num2))
                {
                    switch (num2)
                    {
                        case 0:
                            vectorArray[4].y = vectorArray[0].y;
                            vectorArray[4].z = vectorArray[0].z;
                            break;

                        case 1:
                            vectorArray[4].x = vectorArray[0].x;
                            vectorArray[4].z = vectorArray[0].z;
                            break;

                        case 2:
                            vectorArray[4].x = vectorArray[0].x;
                            vectorArray[4].y = vectorArray[0].y;
                            break;
                    }
                }
            }
            target.transform.eulerAngles = vectorArray[4];
        }
    }

    public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
    {
        var args = new object[] { "looktarget", looktarget, "time", time };
        LookUpdate(target, Hash(args));
    }

    public static void MoveAdd(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "move";
        args["method"] = "add";
        Launch(target, args);
    }

    public static void MoveAdd(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        MoveAdd(target, Hash(args));
    }

    public static void MoveBy(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "move";
        args["method"] = "by";
        Launch(target, args);
    }

    public static void MoveBy(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        MoveBy(target, Hash(args));
    }

    public static void MoveFrom(GameObject target, Hashtable args)
    {
        bool isLocal;
        args = CleanArgs(args);
        if (args.Contains("islocal"))
        {
            isLocal = (bool) args["islocal"];
        }
        else
        {
            isLocal = Defaults.isLocal;
        }
        if (args.Contains("path"))
        {
            Vector3[] vectorArray2;
            if (args["path"].GetType() == typeof(Vector3[]))
            {
                var sourceArray = (Vector3[]) args["path"];
                vectorArray2 = new Vector3[sourceArray.Length];
                Array.Copy(sourceArray, vectorArray2, sourceArray.Length);
            }
            else
            {
                var transformArray = (Transform[]) args["path"];
                vectorArray2 = new Vector3[transformArray.Length];
                for (var i = 0; i < transformArray.Length; i++)
                {
                    vectorArray2[i] = transformArray[i].position;
                }
            }
            if (vectorArray2[vectorArray2.Length - 1] != target.transform.position)
            {
                var destinationArray = new Vector3[vectorArray2.Length + 1];
                Array.Copy(vectorArray2, destinationArray, vectorArray2.Length);
                if (isLocal)
                {
                    destinationArray[destinationArray.Length - 1] = target.transform.localPosition;
                    target.transform.localPosition = destinationArray[0];
                }
                else
                {
                    destinationArray[destinationArray.Length - 1] = target.transform.position;
                    target.transform.position = destinationArray[0];
                }
                args["path"] = destinationArray;
            }
            else
            {
                if (isLocal)
                {
                    target.transform.localPosition = vectorArray2[0];
                }
                else
                {
                    target.transform.position = vectorArray2[0];
                }
                args["path"] = vectorArray2;
            }
        }
        else
        {
            Vector3 position;
            Vector3 vector2;
            if (isLocal)
            {
                vector2 = position = target.transform.localPosition;
            }
            else
            {
                vector2 = position = target.transform.position;
            }
            if (args.Contains("position"))
            {
                if (args["position"].GetType() == typeof(Transform))
                {
                    var transform = (Transform) args["position"];
                    position = transform.position;
                }
                else if (args["position"].GetType() == typeof(Vector3))
                {
                    position = (Vector3) args["position"];
                }
            }
            else
            {
                if (args.Contains("x"))
                {
                    position.x = (float) args["x"];
                }
                if (args.Contains("y"))
                {
                    position.y = (float) args["y"];
                }
                if (args.Contains("z"))
                {
                    position.z = (float) args["z"];
                }
            }
            if (isLocal)
            {
                target.transform.localPosition = position;
            }
            else
            {
                target.transform.position = position;
            }
            args["position"] = vector2;
        }
        args["type"] = "move";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void MoveFrom(GameObject target, Vector3 position, float time)
    {
        var args = new object[] { "position", position, "time", time };
        MoveFrom(target, Hash(args));
    }

    public static void MoveTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
        {
            var transform = (Transform) args["position"];
            args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        args["type"] = "move";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void MoveTo(GameObject target, Vector3 position, float time)
    {
        var args = new object[] { "position", position, "time", time };
        MoveTo(target, Hash(args));
    }

    public static void MoveUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        bool isLocal;
        CleanArgs(args);
        var vectorArray = new Vector3[4];
        var position = target.transform.position;
        if (args.Contains("time"))
        {
            updateTime = (float) args["time"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        if (args.Contains("islocal"))
        {
            isLocal = (bool) args["islocal"];
        }
        else
        {
            isLocal = Defaults.isLocal;
        }
        if (isLocal)
        {
            vectorArray[0] = vectorArray[1] = target.transform.localPosition;
        }
        else
        {
            vectorArray[0] = vectorArray[1] = target.transform.position;
        }
        if (args.Contains("position"))
        {
            if (args["position"].GetType() == typeof(Transform))
            {
                var transform = (Transform) args["position"];
                vectorArray[1] = transform.position;
            }
            else if (args["position"].GetType() == typeof(Vector3))
            {
                vectorArray[1] = (Vector3) args["position"];
            }
        }
        else
        {
            if (args.Contains("x"))
            {
                vectorArray[1].x = (float) args["x"];
            }
            if (args.Contains("y"))
            {
                vectorArray[1].y = (float) args["y"];
            }
            if (args.Contains("z"))
            {
                vectorArray[1].z = (float) args["z"];
            }
        }
        vectorArray[3].x = Mathf.SmoothDamp(vectorArray[0].x, vectorArray[1].x, ref vectorArray[2].x, updateTime);
        vectorArray[3].y = Mathf.SmoothDamp(vectorArray[0].y, vectorArray[1].y, ref vectorArray[2].y, updateTime);
        vectorArray[3].z = Mathf.SmoothDamp(vectorArray[0].z, vectorArray[1].z, ref vectorArray[2].z, updateTime);
        if (args.Contains("orienttopath") && (bool) args["orienttopath"])
        {
            args["looktarget"] = vectorArray[3];
        }
        if (args.Contains("looktarget"))
        {
            LookUpdate(target, args);
        }
        if (isLocal)
        {
            target.transform.localPosition = vectorArray[3];
        }
        else
        {
            target.transform.position = vectorArray[3];
        }
        if (target.rigidbody != null)
        {
            var vector3 = target.transform.position;
            target.transform.position = position;
            target.rigidbody.MovePosition(vector3);
        }
    }

    public static void MoveUpdate(GameObject target, Vector3 position, float time)
    {
        var args = new object[] { "position", position, "time", time };
        MoveUpdate(target, Hash(args));
    }

    private void OnDisable()
    {
        DisableKinematic();
    }

    private void OnEnable()
    {
        if (isRunning)
        {
            EnableKinematic();
        }
        if (isPaused)
        {
            isPaused = false;
            if (delay > 0f)
            {
                wasPaused = true;
                ResumeDelay();
            }
        }
    }

    private static Vector3[] PathControlPointGenerator(Vector3[] path)
    {
        var sourceArray = path;
        var num = 2;
        var destinationArray = new Vector3[sourceArray.Length + num];
        Array.Copy(sourceArray, 0, destinationArray, 1, sourceArray.Length);
        destinationArray[0] = destinationArray[1] + (destinationArray[1] - destinationArray[2]);
        destinationArray[destinationArray.Length - 1] = destinationArray[destinationArray.Length - 2] + (destinationArray[destinationArray.Length - 2] - destinationArray[destinationArray.Length - 3]);
        if (destinationArray[1] == destinationArray[destinationArray.Length - 2])
        {
            var vectorArray3 = new Vector3[destinationArray.Length];
            Array.Copy(destinationArray, vectorArray3, destinationArray.Length);
            vectorArray3[0] = vectorArray3[vectorArray3.Length - 3];
            vectorArray3[vectorArray3.Length - 1] = vectorArray3[2];
            destinationArray = new Vector3[vectorArray3.Length];
            Array.Copy(vectorArray3, destinationArray, vectorArray3.Length);
        }
        return destinationArray;
    }

    public static float PathLength(Transform[] path)
    {
        var vectorArray = new Vector3[path.Length];
        var num = 0f;
        for (var i = 0; i < path.Length; i++)
        {
            vectorArray[i] = path[i].position;
        }
        var pts = PathControlPointGenerator(vectorArray);
        var a = Interp(pts, 0f);
        var num3 = path.Length * 20;
        for (var j = 1; j <= num3; j++)
        {
            var t = j / (float) num3;
            var b = Interp(pts, t);
            num += Vector3.Distance(a, b);
            a = b;
        }
        return num;
    }

    public static float PathLength(Vector3[] path)
    {
        var num = 0f;
        var pts = PathControlPointGenerator(path);
        var a = Interp(pts, 0f);
        var num2 = path.Length * 20;
        for (var i = 1; i <= num2; i++)
        {
            var t = i / (float) num2;
            var b = Interp(pts, t);
            num += Vector3.Distance(a, b);
            a = b;
        }
        return num;
    }

    public static void Pause()
    {
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var target = (GameObject) hashtable["target"];
            Pause(target);
        }
    }

    public static void Pause(string type)
    {
        var list = new ArrayList();
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var obj2 = (GameObject) hashtable["target"];
            list.Insert(list.Count, obj2);
        }
        for (var j = 0; j < list.Count; j++)
        {
            Pause((GameObject) list[j], type);
        }
    }

    public static void Pause(GameObject target)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if (tween.delay > 0f)
            {
                tween.delay -= Time.time - tween.delayStarted;
                tween.StopCoroutine("TweenDelay");
            }
            tween.isPaused = true;
            tween.enabled = false;
        }
    }

    public static void Pause(GameObject target, bool includechildren)
    {
        Pause(target);
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Pause(current.gameObject, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    public static void Pause(GameObject target, string type)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                if (tween.delay > 0f)
                {
                    tween.delay -= Time.time - tween.delayStarted;
                    tween.StopCoroutine("TweenDelay");
                }
                tween.isPaused = true;
                tween.enabled = false;
            }
        }
    }

    public static void Pause(GameObject target, string type, bool includechildren)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                if (tween.delay > 0f)
                {
                    tween.delay -= Time.time - tween.delayStarted;
                    tween.StopCoroutine("TweenDelay");
                }
                tween.isPaused = true;
                tween.enabled = false;
            }
        }
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Pause(current.gameObject, type, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    public static Vector3 PointOnPath(Transform[] path, float percent)
    {
        var vectorArray = new Vector3[path.Length];
        for (var i = 0; i < path.Length; i++)
        {
            vectorArray[i] = path[i].position;
        }
        return Interp(PathControlPointGenerator(vectorArray), percent);
    }

    public static Vector3 PointOnPath(Vector3[] path, float percent)
    {
        return Interp(PathControlPointGenerator(path), percent);
    }

    private float punch(float amplitude, float value)
    {
        var num = 9f;
        if (value == 0f)
        {
            return 0f;
        }
        if (value == 1f)
        {
            return 0f;
        }
        var num2 = 0.3f;
        num = num2 / 6.283185f * Mathf.Asin(0f);
        return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num) * 6.283185f / num2);
    }

    public static void PunchPosition(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "punch";
        args["method"] = "position";
        args["easetype"] = EaseType.punch;
        Launch(target, args);
    }

    public static void PunchPosition(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        PunchPosition(target, Hash(args));
    }

    public static void PunchRotation(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "punch";
        args["method"] = "rotation";
        args["easetype"] = EaseType.punch;
        Launch(target, args);
    }

    public static void PunchRotation(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        PunchRotation(target, Hash(args));
    }

    public static void PunchScale(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "punch";
        args["method"] = "scale";
        args["easetype"] = EaseType.punch;
        Launch(target, args);
    }

    public static void PunchScale(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        PunchScale(target, Hash(args));
    }

    public static void PutOnPath(GameObject target, Transform[] path, float percent)
    {
        var vectorArray = new Vector3[path.Length];
        for (var i = 0; i < path.Length; i++)
        {
            vectorArray[i] = path[i].position;
        }
        target.transform.position = Interp(PathControlPointGenerator(vectorArray), percent);
    }

    public static void PutOnPath(GameObject target, Vector3[] path, float percent)
    {
        target.transform.position = Interp(PathControlPointGenerator(path), percent);
    }

    public static void PutOnPath(Transform target, Transform[] path, float percent)
    {
        var vectorArray = new Vector3[path.Length];
        for (var i = 0; i < path.Length; i++)
        {
            vectorArray[i] = path[i].position;
        }
        target.position = Interp(PathControlPointGenerator(vectorArray), percent);
    }

    public static void PutOnPath(Transform target, Vector3[] path, float percent)
    {
        target.position = Interp(PathControlPointGenerator(path), percent);
    }

    public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
    {
        return new Rect(FloatUpdate(currentValue.x, targetValue.x, speed), FloatUpdate(currentValue.y, targetValue.y, speed), FloatUpdate(currentValue.width, targetValue.width, speed), FloatUpdate(currentValue.height, targetValue.height, speed));
    }

    public static void Resume()
    {
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var target = (GameObject) hashtable["target"];
            Resume(target);
        }
    }

    public static void Resume(string type)
    {
        var list = new ArrayList();
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var obj2 = (GameObject) hashtable["target"];
            list.Insert(list.Count, obj2);
        }
        for (var j = 0; j < list.Count; j++)
        {
            Resume((GameObject) list[j], type);
        }
    }

    public static void Resume(GameObject target)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            tween.enabled = true;
        }
    }

    public static void Resume(GameObject target, bool includechildren)
    {
        Resume(target);
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Resume(current.gameObject, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    public static void Resume(GameObject target, string type)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                tween.enabled = true;
            }
        }
    }

    public static void Resume(GameObject target, string type, bool includechildren)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                tween.enabled = true;
            }
        }
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Resume(current.gameObject, type, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    private void ResumeDelay()
    {
        StartCoroutine("TweenDelay");
    }

    private void RetrieveArgs()
    {
        foreach (var hashtable in tweens)
        {
            if ((GameObject) hashtable["target"] == gameObject)
            {
                tweenArguments = hashtable;
                break;
            }
        }
        id = (string) tweenArguments["id"];
        type = (string) tweenArguments["type"];
        _name = (string) tweenArguments["name"];
        method = (string) tweenArguments["method"];
        if (tweenArguments.Contains("time"))
        {
            time = (float) tweenArguments["time"];
        }
        else
        {
            time = Defaults.time;
        }
        if (rigidbody != null)
        {
            physics = true;
        }
        if (tweenArguments.Contains("delay"))
        {
            delay = (float) tweenArguments["delay"];
        }
        else
        {
            delay = Defaults.delay;
        }
        if (tweenArguments.Contains("namedcolorvalue"))
        {
            if (tweenArguments["namedcolorvalue"].GetType() == typeof(NamedValueColor))
            {
                namedcolorvalue = (NamedValueColor) (int) tweenArguments["namedcolorvalue"];
            }
            else
            {
                try
                {
                    namedcolorvalue = (NamedValueColor) (int) Enum.Parse(typeof(NamedValueColor), (string) tweenArguments["namedcolorvalue"], true);
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("iTween: Unsupported namedcolorvalue supplied! Default will be used.");
                    namedcolorvalue = NamedValueColor._Color;
                }
            }
        }
        else
        {
            namedcolorvalue = Defaults.namedColorValue;
        }
        if (tweenArguments.Contains("looptype"))
        {
            if (tweenArguments["looptype"].GetType() == typeof(LoopType))
            {
                loopType = (LoopType) (int) tweenArguments["looptype"];
            }
            else
            {
                try
                {
                    loopType = (LoopType) (int) Enum.Parse(typeof(LoopType), (string) tweenArguments["looptype"], true);
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("iTween: Unsupported loopType supplied! Default will be used.");
                    loopType = LoopType.none;
                }
            }
        }
        else
        {
            loopType = LoopType.none;
        }
        if (tweenArguments.Contains("easetype"))
        {
            if (tweenArguments["easetype"].GetType() == typeof(EaseType))
            {
                easeType = (EaseType) (int) tweenArguments["easetype"];
            }
            else
            {
                try
                {
                    easeType = (EaseType) (int) Enum.Parse(typeof(EaseType), (string) tweenArguments["easetype"], true);
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("iTween: Unsupported easeType supplied! Default will be used.");
                    easeType = Defaults.easeType;
                }
            }
        }
        else
        {
            easeType = Defaults.easeType;
        }
        if (tweenArguments.Contains("space"))
        {
            if (tweenArguments["space"].GetType() == typeof(Space))
            {
                space = (Space) (int) tweenArguments["space"];
            }
            else
            {
                try
                {
                    space = (Space) (int) Enum.Parse(typeof(Space), (string) tweenArguments["space"], true);
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("iTween: Unsupported space supplied! Default will be used.");
                    space = Defaults.space;
                }
            }
        }
        else
        {
            space = Defaults.space;
        }
        if (tweenArguments.Contains("islocal"))
        {
            isLocal = (bool) tweenArguments["islocal"];
        }
        else
        {
            isLocal = Defaults.isLocal;
        }
        if (tweenArguments.Contains("ignoretimescale"))
        {
            useRealTime = (bool) tweenArguments["ignoretimescale"];
        }
        else
        {
            useRealTime = Defaults.useRealTime;
        }
        GetEasingFunction();
    }

    public static void RotateAdd(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "rotate";
        args["method"] = "add";
        Launch(target, args);
    }

    public static void RotateAdd(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        RotateAdd(target, Hash(args));
    }

    public static void RotateBy(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "rotate";
        args["method"] = "by";
        Launch(target, args);
    }

    public static void RotateBy(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        RotateBy(target, Hash(args));
    }

    public static void RotateFrom(GameObject target, Hashtable args)
    {
        bool isLocal;
        Vector3 eulerAngles;
        Vector3 vector2;
        args = CleanArgs(args);
        if (args.Contains("islocal"))
        {
            isLocal = (bool) args["islocal"];
        }
        else
        {
            isLocal = Defaults.isLocal;
        }
        if (isLocal)
        {
            vector2 = eulerAngles = target.transform.localEulerAngles;
        }
        else
        {
            vector2 = eulerAngles = target.transform.eulerAngles;
        }
        if (args.Contains("rotation"))
        {
            if (args["rotation"].GetType() == typeof(Transform))
            {
                var transform = (Transform) args["rotation"];
                eulerAngles = transform.eulerAngles;
            }
            else if (args["rotation"].GetType() == typeof(Vector3))
            {
                eulerAngles = (Vector3) args["rotation"];
            }
        }
        else
        {
            if (args.Contains("x"))
            {
                eulerAngles.x = (float) args["x"];
            }
            if (args.Contains("y"))
            {
                eulerAngles.y = (float) args["y"];
            }
            if (args.Contains("z"))
            {
                eulerAngles.z = (float) args["z"];
            }
        }
        if (isLocal)
        {
            target.transform.localEulerAngles = eulerAngles;
        }
        else
        {
            target.transform.eulerAngles = eulerAngles;
        }
        args["rotation"] = vector2;
        args["type"] = "rotate";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void RotateFrom(GameObject target, Vector3 rotation, float time)
    {
        var args = new object[] { "rotation", rotation, "time", time };
        RotateFrom(target, Hash(args));
    }

    public static void RotateTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
        {
            var transform = (Transform) args["rotation"];
            args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        args["type"] = "rotate";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void RotateTo(GameObject target, Vector3 rotation, float time)
    {
        var args = new object[] { "rotation", rotation, "time", time };
        RotateTo(target, Hash(args));
    }

    public static void RotateUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        bool isLocal;
        CleanArgs(args);
        var vectorArray = new Vector3[4];
        var eulerAngles = target.transform.eulerAngles;
        if (args.Contains("time"))
        {
            updateTime = (float) args["time"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        if (args.Contains("islocal"))
        {
            isLocal = (bool) args["islocal"];
        }
        else
        {
            isLocal = Defaults.isLocal;
        }
        if (isLocal)
        {
            vectorArray[0] = target.transform.localEulerAngles;
        }
        else
        {
            vectorArray[0] = target.transform.eulerAngles;
        }
        if (args.Contains("rotation"))
        {
            if (args["rotation"].GetType() == typeof(Transform))
            {
                var transform = (Transform) args["rotation"];
                vectorArray[1] = transform.eulerAngles;
            }
            else if (args["rotation"].GetType() == typeof(Vector3))
            {
                vectorArray[1] = (Vector3) args["rotation"];
            }
        }
        vectorArray[3].x = Mathf.SmoothDampAngle(vectorArray[0].x, vectorArray[1].x, ref vectorArray[2].x, updateTime);
        vectorArray[3].y = Mathf.SmoothDampAngle(vectorArray[0].y, vectorArray[1].y, ref vectorArray[2].y, updateTime);
        vectorArray[3].z = Mathf.SmoothDampAngle(vectorArray[0].z, vectorArray[1].z, ref vectorArray[2].z, updateTime);
        if (isLocal)
        {
            target.transform.localEulerAngles = vectorArray[3];
        }
        else
        {
            target.transform.eulerAngles = vectorArray[3];
        }
        if (target.rigidbody != null)
        {
            var euler = target.transform.eulerAngles;
            target.transform.eulerAngles = eulerAngles;
            target.rigidbody.MoveRotation(Quaternion.Euler(euler));
        }
    }

    public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
    {
        var args = new object[] { "rotation", rotation, "time", time };
        RotateUpdate(target, Hash(args));
    }

    public static void ScaleAdd(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "scale";
        args["method"] = "add";
        Launch(target, args);
    }

    public static void ScaleAdd(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        ScaleAdd(target, Hash(args));
    }

    public static void ScaleBy(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "scale";
        args["method"] = "by";
        Launch(target, args);
    }

    public static void ScaleBy(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        ScaleBy(target, Hash(args));
    }

    public static void ScaleFrom(GameObject target, Hashtable args)
    {
        Vector3 localScale;
        args = CleanArgs(args);
        var vector2 = localScale = target.transform.localScale;
        if (args.Contains("scale"))
        {
            if (args["scale"].GetType() == typeof(Transform))
            {
                var transform = (Transform) args["scale"];
                localScale = transform.localScale;
            }
            else if (args["scale"].GetType() == typeof(Vector3))
            {
                localScale = (Vector3) args["scale"];
            }
        }
        else
        {
            if (args.Contains("x"))
            {
                localScale.x = (float) args["x"];
            }
            if (args.Contains("y"))
            {
                localScale.y = (float) args["y"];
            }
            if (args.Contains("z"))
            {
                localScale.z = (float) args["z"];
            }
        }
        target.transform.localScale = localScale;
        args["scale"] = vector2;
        args["type"] = "scale";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void ScaleFrom(GameObject target, Vector3 scale, float time)
    {
        var args = new object[] { "scale", scale, "time", time };
        ScaleFrom(target, Hash(args));
    }

    public static void ScaleTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
        {
            var transform = (Transform) args["scale"];
            args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        args["type"] = "scale";
        args["method"] = "to";
        Launch(target, args);
    }

    public static void ScaleTo(GameObject target, Vector3 scale, float time)
    {
        var args = new object[] { "scale", scale, "time", time };
        ScaleTo(target, Hash(args));
    }

    public static void ScaleUpdate(GameObject target, Hashtable args)
    {
        float updateTime;
        CleanArgs(args);
        var vectorArray = new Vector3[4];
        if (args.Contains("time"))
        {
            updateTime = (float) args["time"];
            updateTime *= Defaults.updateTimePercentage;
        }
        else
        {
            updateTime = Defaults.updateTime;
        }
        vectorArray[0] = vectorArray[1] = target.transform.localScale;
        if (args.Contains("scale"))
        {
            if (args["scale"].GetType() == typeof(Transform))
            {
                var transform = (Transform) args["scale"];
                vectorArray[1] = transform.localScale;
            }
            else if (args["scale"].GetType() == typeof(Vector3))
            {
                vectorArray[1] = (Vector3) args["scale"];
            }
        }
        else
        {
            if (args.Contains("x"))
            {
                vectorArray[1].x = (float) args["x"];
            }
            if (args.Contains("y"))
            {
                vectorArray[1].y = (float) args["y"];
            }
            if (args.Contains("z"))
            {
                vectorArray[1].z = (float) args["z"];
            }
        }
        vectorArray[3].x = Mathf.SmoothDamp(vectorArray[0].x, vectorArray[1].x, ref vectorArray[2].x, updateTime);
        vectorArray[3].y = Mathf.SmoothDamp(vectorArray[0].y, vectorArray[1].y, ref vectorArray[2].y, updateTime);
        vectorArray[3].z = Mathf.SmoothDamp(vectorArray[0].z, vectorArray[1].z, ref vectorArray[2].z, updateTime);
        target.transform.localScale = vectorArray[3];
    }

    public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
    {
        var args = new object[] { "scale", scale, "time", time };
        ScaleUpdate(target, Hash(args));
    }

    public static void ShakePosition(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "shake";
        args["method"] = "position";
        Launch(target, args);
    }

    public static void ShakePosition(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        ShakePosition(target, Hash(args));
    }

    public static void ShakeRotation(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "shake";
        args["method"] = "rotation";
        Launch(target, args);
    }

    public static void ShakeRotation(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        ShakeRotation(target, Hash(args));
    }

    public static void ShakeScale(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "shake";
        args["method"] = "scale";
        Launch(target, args);
    }

    public static void ShakeScale(GameObject target, Vector3 amount, float time)
    {
        var args = new object[] { "amount", amount, "time", time };
        ShakeScale(target, Hash(args));
    }

    private float spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * 3.141593f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
        return start + (end - start) * value;
    }

    public static void Stab(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        args["type"] = "stab";
        Launch(target, args);
    }

    public static void Stab(GameObject target, AudioClip audioclip, float delay)
    {
        var args = new object[] { "audioclip", audioclip, "delay", delay };
        Stab(target, Hash(args));
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
        return new StartcIteratorE { fthis = this };
    }

    public static void Stop()
    {
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var target = (GameObject) hashtable["target"];
            Stop(target);
        }
        tweens.Clear();
    }

    public static void Stop(string type)
    {
        var list = new ArrayList();
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var obj2 = (GameObject) hashtable["target"];
            list.Insert(list.Count, obj2);
        }
        for (var j = 0; j < list.Count; j++)
        {
            Stop((GameObject) list[j], type);
        }
    }

    public static void Stop(GameObject target)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            tween.Dispose();
        }
    }

    public static void Stop(GameObject target, bool includechildren)
    {
        Stop(target);
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Stop(current.gameObject, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    public static void Stop(GameObject target, string type)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                tween.Dispose();
            }
        }
    }

    public static void Stop(GameObject target, string type, bool includechildren)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if ((tween.type + tween.method).Substring(0, type.Length).ToLower() == type.ToLower())
            {
                tween.Dispose();
            }
        }
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    Stop(current.gameObject, type, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    public static void StopByName(string name)
    {
        var list = new ArrayList();
        for (var i = 0; i < tweens.Count; i++)
        {
            var hashtable = tweens[i];
            var obj2 = (GameObject) hashtable["target"];
            list.Insert(list.Count, obj2);
        }
        for (var j = 0; j < list.Count; j++)
        {
            StopByName((GameObject) list[j], name);
        }
    }

    public static void StopByName(GameObject target, string name)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if (tween._name == name)
            {
                tween.Dispose();
            }
        }
    }

    public static void StopByName(GameObject target, string name, bool includechildren)
    {
        foreach (var tween in target.GetComponents<iTween>())
        {
            if (tween._name == name)
            {
                tween.Dispose();
            }
        }
        if (includechildren)
        {
            var enumerator = target.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (Transform) enumerator.Current;
                    StopByName(current.gameObject, name, true);
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    private void TweenComplete()
    {
        isRunning = false;
        if (percentage > 0.5f)
        {
            percentage = 1f;
        }
        else
        {
            percentage = 0f;
        }
        apply();
        if (type == "value")
        {
            CallBack("onupdate");
        }
        if (loopType == LoopType.none)
        {
            Dispose();
        }
        else
        {
            TweenLoop();
        }
        CallBack("oncomplete");
    }

    [DebuggerHidden]
    private IEnumerator TweenDelay()
    {
        return new TweenDelaycIteratorC { fthis = this };
    }

    private void TweenLoop()
    {
        DisableKinematic();
        switch (loopType)
        {
            case LoopType.loop:
                percentage = 0f;
                runningTime = 0f;
                apply();
                StartCoroutine("TweenRestart");
                break;

            case LoopType.pingPong:
                reverse = !reverse;
                runningTime = 0f;
                StartCoroutine("TweenRestart");
                break;
        }
    }

    [DebuggerHidden]
    private IEnumerator TweenRestart()
    {
        return new TweenRestartcIteratorD { fthis = this };
    }

    private void TweenStart()
    {
        CallBack("onstart");
        if (!loop)
        {
            ConflictCheck();
            GenerateTargets();
        }
        if (type == "stab")
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        if (type == "move" || type == "scale" || type == "rotate" || type == "punch" || type == "shake" || type == "curve" || type == "look")
        {
            EnableKinematic();
        }
        isRunning = true;
    }

    private void TweenUpdate()
    {
        apply();
        CallBack("onupdate");
        UpdatePercentage();
    }

    private void Update()
    {
        if (isRunning && !physics)
        {
            if (!reverse)
            {
                if (percentage < 1f)
                {
                    TweenUpdate();
                }
                else
                {
                    TweenComplete();
                }
            }
            else if (percentage > 0f)
            {
                TweenUpdate();
            }
            else
            {
                TweenComplete();
            }
        }
    }

    private void UpdatePercentage()
    {
        if (useRealTime)
        {
            runningTime += Time.realtimeSinceStartup - lastRealTime;
        }
        else
        {
            runningTime += Time.deltaTime;
        }
        if (reverse)
        {
            percentage = 1f - runningTime / time;
        }
        else
        {
            percentage = runningTime / time;
        }
        lastRealTime = Time.realtimeSinceStartup;
    }

    public static void ValueTo(GameObject target, Hashtable args)
    {
        args = CleanArgs(args);
        if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
        {
            UnityEngine.Debug.LogError("iTween Error: ValueTo() requires an 'onupdate' callback function and a 'from' and 'to' property.  The supplied 'onupdate' callback must accept a single argument that is the same type as the supplied 'from' and 'to' properties!");
        }
        else
        {
            args["type"] = "value";
            if (args["from"].GetType() == typeof(Vector2))
            {
                args["method"] = "vector2";
            }
            else if (args["from"].GetType() == typeof(Vector3))
            {
                args["method"] = "vector3";
            }
            else if (args["from"].GetType() == typeof(Rect))
            {
                args["method"] = "rect";
            }
            else if (args["from"].GetType() == typeof(float))
            {
                args["method"] = "float";
            }
            else if (args["from"].GetType() == typeof(Color))
            {
                args["method"] = "color";
            }
            else
            {
                UnityEngine.Debug.LogError("iTween Error: ValueTo() only works with interpolating Vector3s, Vector2s, floats, ints, Rects and Colors!");
                return;
            }
            if (!args.Contains("easetype"))
            {
                args.Add("easetype", EaseType.linear);
            }
            Launch(target, args);
        }
    }

    public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
    {
        var vector = targetValue - currentValue;
        currentValue += vector * speed * Time.deltaTime;
        return currentValue;
    }

    public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
    {
        var vector = targetValue - currentValue;
        currentValue += vector * speed * Time.deltaTime;
        return currentValue;
    }

    [CompilerGenerated]
    private sealed class StartcIteratorE : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal iTween fthis;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    if (fthis.delay <= 0f)
                    {
                        break;
                    }
                    Scurrent = fthis.StartCoroutine("TweenDelay");
                    SPC = 1;
                    return true;

                case 1:
                    break;

                default:
                    goto Label_006A;
            }
            fthis.TweenStart();
            SPC = -1;
        Label_006A:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class TweenDelaycIteratorC : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal iTween fthis;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    fthis.delayStarted = Time.time;
                    Scurrent = new WaitForSeconds(fthis.delay);
                    SPC = 1;
                    return true;

                case 1:
                    if (fthis.wasPaused)
                    {
                        fthis.wasPaused = false;
                        fthis.TweenStart();
                    }
                    SPC = -1;
                    break;
            }
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class TweenRestartcIteratorD : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal iTween fthis;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    if (fthis.delay <= 0f)
                    {
                        break;
                    }
                    fthis.delayStarted = Time.time;
                    Scurrent = new WaitForSeconds(fthis.delay);
                    SPC = 1;
                    return true;

                case 1:
                    break;

                default:
                    goto Label_0086;
            }
            fthis.loop = true;
            fthis.TweenStart();
            SPC = -1;
        Label_0086:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    private delegate void ApplyTween();

    private class CRSpline
    {
        public Vector3[] pts;

        public CRSpline(params Vector3[] pts)
        {
            this.pts = new Vector3[pts.Length];
            Array.Copy(pts, this.pts, pts.Length);
        }

        public Vector3 Interp(float t)
        {
            var num = pts.Length - 3;
            var index = Mathf.Min(Mathf.FloorToInt(t * num), num - 1);
            var num3 = t * num - index;
            var vector = pts[index];
            var vector2 = pts[index + 1];
            var vector3 = pts[index + 2];
            var vector4 = pts[index + 3];
            return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
        }
    }

    public static class Defaults
    {
        public static int cameraFadeDepth = 999999;
        public static Color color = Color.white;
        public static float delay = 0f;
        public static EaseType easeType = EaseType.easeOutExpo;
        public static bool isLocal = false;
        public static float lookAhead = 0.05f;
        public static float lookSpeed = 3f;
        public static LoopType loopType = LoopType.none;
        public static NamedValueColor namedColorValue = NamedValueColor._Color;
        public static bool orientToPath = false;
        public static Space space = Space.Self;
        public static float time = 1f;
        public static Vector3 up = Vector3.up;
        public static float updateTime = 1f * updateTimePercentage;
        public static float updateTimePercentage = 0.05f;
        public static bool useRealTime = false;
    }

    public enum EaseType
    {
        easeInQuad,
        easeOutQuad,
        easeInOutQuad,
        easeInCubic,
        easeOutCubic,
        easeInOutCubic,
        easeInQuart,
        easeOutQuart,
        easeInOutQuart,
        easeInQuint,
        easeOutQuint,
        easeInOutQuint,
        easeInSine,
        easeOutSine,
        easeInOutSine,
        easeInExpo,
        easeOutExpo,
        easeInOutExpo,
        easeInCirc,
        easeOutCirc,
        easeInOutCirc,
        linear,
        spring,
        easeInBounce,
        easeOutBounce,
        easeInOutBounce,
        easeInBack,
        easeOutBack,
        easeInOutBack,
        easeInElastic,
        easeOutElastic,
        easeInOutElastic,
        punch
    }

    private delegate float EasingFunction(float start, float end, float Value);

    public enum LoopType
    {
        none,
        loop,
        pingPong
    }

    public enum NamedValueColor
    {
        _Color,
        _SpecColor,
        _Emission,
        _ReflectColor
    }
}

