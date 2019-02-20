//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class ParentFollow : MonoBehaviour
{
    private Transform bTransform;
    public bool isActiveInScene;
    private Transform parent;

    private void Awake()
    {
        bTransform = transform;
        isActiveInScene = true;
    }

    public void RemoveParent()
    {
        parent = null;
    }

    public void SetParent(Transform transform)
    {
        parent = transform;
        bTransform.rotation = transform.rotation;
    }

    private void Update()
    {
        if (isActiveInScene && parent != null)
        {
            bTransform.position = parent.position;
        }
    }
}

