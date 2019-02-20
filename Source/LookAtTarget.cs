//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
    public int level;
    private Transform mTrans;
    public float speed = 8f;
    public Transform target;

    private void LateUpdate()
    {
        if (target != null)
        {
            var forward = target.position - mTrans.position;
            if (forward.magnitude > 0.001f)
            {
                var b = Quaternion.LookRotation(forward);
                mTrans.rotation = Quaternion.Slerp(mTrans.rotation, b, Mathf.Clamp01(speed * Time.deltaTime));
            }
        }
    }

    private void Start()
    {
        mTrans = transform;
    }
}

