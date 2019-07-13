using UnityEngine;

public class InputToEvent : MonoBehaviour
{
    public bool DetectPointedAtGameObject;
    public static Vector3 inputHitPos;
    private GameObject lastGo;

    private void Press(Vector2 screenPos)
    {
        lastGo = RaycastObject(screenPos);
        if (lastGo != null)
        {
            lastGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
        }
    }

    private GameObject RaycastObject(Vector2 screenPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(screenPos), out hit, 200f))
        {
            inputHitPos = hit.point;
            return hit.collider.gameObject;
        }
        return null;
    }

    private void Release(Vector2 screenPos)
    {
        if (lastGo != null)
        {
            if (RaycastObject(screenPos) == lastGo)
            {
                lastGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
            }
            lastGo.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
            lastGo = null;
        }
    }

    private void Update()
    {
        if (DetectPointedAtGameObject)
        {
            goPointedAt = RaycastObject(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Press(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Release(touch.position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Press(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Release(Input.mousePosition);
            }
        }
    }

    public static GameObject goPointedAt { get; private set; }
}