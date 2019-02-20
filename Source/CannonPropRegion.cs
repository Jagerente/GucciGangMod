//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using UnityEngine;

public class CannonPropRegion : Photon.MonoBehaviour
{
    public bool destroyed;
    public bool disabled;
    public string settings;
    public HERO storedHero;

    public void OnDestroy()
    {
        if (storedHero != null)
        {
            storedHero.myCannonRegion = null;
            storedHero.ClearPopup();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        var gameObject = collider.transform.root.gameObject;
        if (gameObject.layer == 8 && gameObject.GetPhotonView().isMine)
        {
            var component = gameObject.GetComponent<HERO>();
            if (component != null && !component.isCannon)
            {
                if (component.myCannonRegion != null)
                {
                    component.myCannonRegion.storedHero = null;
                }
                component.myCannonRegion = this;
                storedHero = component;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        var gameObject = collider.transform.root.gameObject;
        if (gameObject.layer == 8 && gameObject.GetPhotonView().isMine)
        {
            var component = gameObject.GetComponent<HERO>();
            if (component != null && storedHero != null && component == storedHero)
            {
                component.myCannonRegion = null;
                component.ClearPopup();
                storedHero = null;
            }
        }
    }

    [RPC]
    public void RequestControlRPC(int viewID, PhotonMessageInfo info)
    {
        if (photonView.isMine && PhotonNetwork.isMasterClient && !disabled)
        {
            var component = PhotonView.Find(viewID).gameObject.GetComponent<HERO>();
            if (component != null && component.photonView.owner == info.sender && !FengGameManagerMKII.instance.allowedToCannon.ContainsKey(info.sender.ID))
            {
                disabled = true;
                StartCoroutine(WaitAndEnable());
                FengGameManagerMKII.instance.allowedToCannon.Add(info.sender.ID, new CannonValues(photonView.viewID, settings));
                component.photonView.RPC("SpawnCannonRPC", info.sender, settings);
            }
        }
    }

    [RPC]
    public void SetSize(string settings, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            var strArray = settings.Split(',');
            if (strArray.Length > 15)
            {
                var a = 1f;
                GameObject gameObject = null;
                gameObject = this.gameObject;
                if (strArray[2] != "default")
                {
                    if (strArray[2].StartsWith("transparent"))
                    {
                        float num2;
                        if (float.TryParse(strArray[2].Substring(11), out num2))
                        {
                            a = num2;
                        }
                        foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
                        {
                            renderer.material = (Material) FengGameManagerMKII.RCassets.Load("transparent");
                            if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                            {
                                renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                            }
                        }
                    }
                    else
                    {
                        foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
                        {
                            renderer.material = (Material) FengGameManagerMKII.RCassets.Load(strArray[2]);
                            if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                            {
                                renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                            }
                        }
                    }
                }
                var x = gameObject.transform.localScale.x * Convert.ToSingle(strArray[3]);
                x -= 0.001f;
                var y = gameObject.transform.localScale.y * Convert.ToSingle(strArray[4]);
                var z = gameObject.transform.localScale.z * Convert.ToSingle(strArray[5]);
                gameObject.transform.localScale = new Vector3(x, y, z);
                if (strArray[6] != "0")
                {
                    var color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), a);
                    foreach (var filter in gameObject.GetComponentsInChildren<MeshFilter>())
                    {
                        var mesh = filter.mesh;
                        var colorArray = new Color[mesh.vertexCount];
                        for (var i = 0; i < mesh.vertexCount; i++)
                        {
                            colorArray[i] = color;
                        }
                        mesh.colors = colorArray;
                    }
                }
            }
        }
    }

    public void Start()
    {
        if ((int) FengGameManagerMKII.settings[64] >= 100)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    public IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(5f);
        if (!destroyed)
        {
            disabled = false;
        }
    }

}

