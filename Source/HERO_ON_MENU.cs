using UnityEngine;

public class HERO_ON_MENU : MonoBehaviour
{
    private Vector3 cameraOffset;
    private Transform cameraPref;
    public int costumeId;
    private Transform head;
    public float headRotationX;
    public float headRotationY;

    private void LateUpdate()
    {
        head.rotation = Quaternion.Euler(head.rotation.eulerAngles.x + headRotationX, head.rotation.eulerAngles.y + headRotationY, head.rotation.eulerAngles.z);
        if (costumeId == 9)
        {
            GameObject.Find("MainCamera_Mono").transform.position = cameraPref.position + cameraOffset;
        }
    }

    private void Start()
    {
        var component = gameObject.GetComponent<HERO_SETUP>();
        HeroCostume.init2();
        component.init();
        component.myCostume = HeroCostume.costume[costumeId];
        component.setCharacterComponent();
        head = transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
        cameraPref = transform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
        if (costumeId == 9)
        {
            cameraOffset = GameObject.Find("MainCamera_Mono").transform.position - cameraPref.position;
        }
        if (component.myCostume.sex == SEX.FEMALE)
        {
            animation.Play("stand");
            animation["stand"].normalizedTime = Random.Range(0f, 1f);
        }
        else
        {
            animation.Play("stand_levi");
            animation["stand_levi"].normalizedTime = Random.Range(0f, 1f);
        }
        var num = 0.5f;
        animation["stand"].speed = num;
        animation["stand_levi"].speed = num;
    }
}

