using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DragDropSurface : MonoBehaviour
{
    public bool rotatePlacedObject;

    private void OnDrop(GameObject go)
    {
        var component = go.GetComponent<DragDropItem>();
        if (component != null)
        {
            var transform = NGUITools.AddChild(gameObject, component.prefab).transform;
            transform.position = UICamera.lastHit.point;
            if (rotatePlacedObject)
            {
                transform.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
            }
            Destroy(go);
        }
    }
}

