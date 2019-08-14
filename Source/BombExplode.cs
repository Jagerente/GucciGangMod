using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class BombExplode : MonoBehaviour
{
    public GameObject myExplosion;

    public void Start()
    {
        if (photonView != null)
        {
            float num2;
            float num3;
            float num4;
            float num5;
            var owner = photonView.owner;
            if (RCSettings.teamMode > 0)
            {
                var num = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
                if (num == 1)
                {
                    GetComponent<ParticleSystem>().startColor = Color.cyan;
                }
                else if (num == 2)
                {
                    GetComponent<ParticleSystem>().startColor = Color.magenta;
                }
                else
                {
                    num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
                    num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
                    num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
                    num5 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombA]);
                    num5 = Mathf.Max(0.5f, num5);
                    GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, num5);
                }
            }
            else
            {
                num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
                num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
                num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
                num5 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombA]);
                num5 = Mathf.Max(0.5f, num5);
                GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, num5);
            }

            var num6 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombRadius]) * 2f;
            num6 = Mathf.Clamp(num6, 40f, 120f);
            GetComponent<ParticleSystem>().startSize = num6;
        }
    }
}