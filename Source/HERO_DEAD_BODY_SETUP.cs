using UnityEngine;

public class HERO_DEAD_BODY_SETUP : MonoBehaviour
{
    public GameObject blood_arm_l;
    public GameObject blood_arm_r;
    public GameObject blood_lower;
    public GameObject blood_upper;
    public GameObject blood_upper1;
    public GameObject blood_upper2;
    public GameObject chest;
    public GameObject col_chest;
    public GameObject col_head;
    public GameObject col_lower_arm_l;
    public GameObject col_lower_arm_r;
    public GameObject col_shin_l;
    public GameObject col_shin_r;
    public GameObject col_thigh_l;
    public GameObject col_thigh_r;
    public GameObject col_upper_arm_l;
    public GameObject col_upper_arm_r;
    public GameObject hand_l;
    public GameObject hand_r;
    public GameObject head;
    public GameObject leg;
    private float lifetime = 15f;

    public void init(string aniname, float time, BODY_PARTS part)
    {
        animation.Play(aniname);
        animation[aniname].normalizedTime = time;
        animation[aniname].speed = 0f;
        switch (part)
        {
            case BODY_PARTS.UPPER:
                col_upper_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_upper_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_l.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_l.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_r.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_r.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(leg);
                Destroy(hand_l);
                Destroy(hand_r);
                Destroy(blood_lower);
                Destroy(blood_arm_l);
                Destroy(blood_arm_r);
                gameObject.GetComponent<HERO_SETUP>().createHead2();
                gameObject.GetComponent<HERO_SETUP>().createUpperBody2();
                break;

            case BODY_PARTS.ARM_L:
                col_upper_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_l.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_l.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_r.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_r.GetComponent<CapsuleCollider>().enabled = false;
                col_head.GetComponent<CapsuleCollider>().enabled = false;
                col_chest.GetComponent<BoxCollider>().enabled = false;
                Destroy(head);
                Destroy(chest);
                Destroy(leg);
                Destroy(hand_r);
                Destroy(blood_lower);
                Destroy(blood_upper);
                Destroy(blood_upper1);
                Destroy(blood_upper2);
                Destroy(blood_arm_r);
                gameObject.GetComponent<HERO_SETUP>().createLeftArm();
                break;

            case BODY_PARTS.ARM_R:
                col_upper_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_l.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_l.GetComponent<CapsuleCollider>().enabled = false;
                col_thigh_r.GetComponent<CapsuleCollider>().enabled = false;
                col_shin_r.GetComponent<CapsuleCollider>().enabled = false;
                col_head.GetComponent<CapsuleCollider>().enabled = false;
                col_chest.GetComponent<BoxCollider>().enabled = false;
                Destroy(head);
                Destroy(chest);
                Destroy(leg);
                Destroy(hand_l);
                Destroy(blood_lower);
                Destroy(blood_upper);
                Destroy(blood_upper1);
                Destroy(blood_upper2);
                Destroy(blood_arm_l);
                gameObject.GetComponent<HERO_SETUP>().createRightArm();
                break;

            case BODY_PARTS.LOWER:
                col_upper_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_l.GetComponent<CapsuleCollider>().enabled = false;
                col_upper_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_lower_arm_r.GetComponent<CapsuleCollider>().enabled = false;
                col_head.GetComponent<CapsuleCollider>().enabled = false;
                col_chest.GetComponent<BoxCollider>().enabled = false;
                Destroy(head);
                Destroy(chest);
                Destroy(hand_l);
                Destroy(hand_r);
                Destroy(blood_upper);
                Destroy(blood_upper1);
                Destroy(blood_upper2);
                Destroy(blood_arm_l);
                Destroy(blood_arm_r);
                gameObject.GetComponent<HERO_SETUP>().createLowerBody();
                break;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            gameObject.GetComponent<HERO_SETUP>().deleteCharacterComponent2();
            Destroy(gameObject);
        }
    }
}