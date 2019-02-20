//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
    private GameObject mChild;
    private GameObject mPrefab;
    public InvBaseItem.Slot slot;

    public GameObject Attach(GameObject prefab)
    {
        if (mPrefab != prefab)
        {
            mPrefab = prefab;
            if (mChild != null)
            {
                Destroy(mChild);
            }
            if (mPrefab != null)
            {
                var transform = this.transform;
                mChild = Instantiate(mPrefab, transform.position, transform.rotation) as GameObject;
                var transform2 = mChild.transform;
                transform2.parent = transform;
                transform2.localPosition = Vector3.zero;
                transform2.localRotation = Quaternion.identity;
                transform2.localScale = Vector3.one;
            }
        }
        return mChild;
    }
}

