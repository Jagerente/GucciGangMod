//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using GGM;
using System.Collections;
using UnityEngine;

public class TITAN_SETUP : Photon.MonoBehaviour
{
    public GameObject eye;
    private CostumeHair hair;
    private GameObject hair_go_ref;
    private int hairType;
    public bool haseye;
    private GameObject part_hair;
    public int skin;

    private void Awake()
    {
        CostumeHair.init();
        CharacterMaterials.init();
        HeroCostume.init2();
        hair_go_ref = new GameObject();
        eye.transform.parent = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").transform;
        hair_go_ref.transform.position = (eye.transform.position + (Vector3.up * 3.5f)) + (transform.forward * 5.2f);
        hair_go_ref.transform.rotation = eye.transform.rotation;
        hair_go_ref.transform.RotateAround(eye.transform.position, transform.right, -20f);
        hair_go_ref.transform.localScale = new Vector3(210f, 210f, 210f);
        hair_go_ref.transform.parent = transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head").transform;
    }

    [RPC]
    public IEnumerator loadskinE(int hair, int eye, string hairlink)
    {
        var iteratorVariable0 = false;
        Destroy(part_hair);
        this.hair = CostumeHair.hairsM[hair];
        hairType = hair;
        if (this.hair.hair != string.Empty)
        {
            var iteratorVariable1 = (GameObject) Instantiate(Resources.Load("Character/" + this.hair.hair));
            iteratorVariable1.transform.parent = hair_go_ref.transform.parent;
            iteratorVariable1.transform.position = hair_go_ref.transform.position;
            iteratorVariable1.transform.rotation = hair_go_ref.transform.rotation;
            iteratorVariable1.transform.localScale = hair_go_ref.transform.localScale;
            iteratorVariable1.renderer.material = CharacterMaterials.materials[this.hair.texture];
            var mipmap = true;
            if (Settings.MipMapping == 1)
            {
                mipmap = false;
            }
            if ((!hairlink.EndsWith(".jpg") && !hairlink.EndsWith(".png")) && !hairlink.EndsWith(".jpeg"))
            {
                if (hairlink.ToLower() == "transparent")
                {
                    iteratorVariable1.renderer.enabled = false;
                }
            }
            else if (FengGameManagerMKII.linkHash[0].ContainsKey(hairlink))
            {
                iteratorVariable1.renderer.material = (Material) FengGameManagerMKII.linkHash[0][hairlink];
            }
            else
            {
                var link = new WWW(hairlink);
                yield return link;
                var iteratorVariable4 = RCextensions.loadimage(link, mipmap, 0x30d40);
                link.Dispose();
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(hairlink))
                {
                    iteratorVariable0 = true;
                    iteratorVariable1.renderer.material.mainTexture = iteratorVariable4;
                    FengGameManagerMKII.linkHash[0].Add(hairlink, iteratorVariable1.renderer.material);
                    iteratorVariable1.renderer.material = (Material) FengGameManagerMKII.linkHash[0][hairlink];
                }
                else
                {
                    iteratorVariable1.renderer.material = (Material) FengGameManagerMKII.linkHash[0][hairlink];
                }
            }
            part_hair = iteratorVariable1;
        }
        if (eye >= 0)
        {
            setFacialTexture(this.eye, eye);
        }
        if (iteratorVariable0)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
    }

    public void setFacialTexture(GameObject go, int id)
    {
        if (id >= 0)
        {
            var num = 0.25f;
            var num2 = 0.125f;
            var x = num2 * ((int) (id / 8f));
            var y = -num * (id % 4);
            go.renderer.material.mainTextureOffset = new Vector2(x, y);
        }
    }

    public void setHair()
    {
        Destroy(part_hair);
        var index = Random.Range(0, CostumeHair.hairsM.Length);
        if (index == 3)
        {
            index = 9;
        }
        hairType = index;
        hair = CostumeHair.hairsM[index];
        if (hair.hair == string.Empty)
        {
            hair = CostumeHair.hairsM[9];
            hairType = 9;
        }
        part_hair = (GameObject) Instantiate(Resources.Load("Character/" + hair.hair));
        part_hair.transform.parent = hair_go_ref.transform.parent;
        part_hair.transform.position = hair_go_ref.transform.position;
        part_hair.transform.rotation = hair_go_ref.transform.rotation;
        part_hair.transform.localScale = hair_go_ref.transform.localScale;
        part_hair.renderer.material = CharacterMaterials.materials[hair.texture];
        part_hair.renderer.material.color = HeroCostume.costume[Random.Range(0, HeroCostume.costume.Length - 5)].hair_color;
        var id = Random.Range(1, 8);
        setFacialTexture(eye, id);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && photonView.isMine)
        {
            var parameters = new object[] { hairType, id, part_hair.renderer.material.color.r, part_hair.renderer.material.color.g, part_hair.renderer.material.color.b };
            photonView.RPC("setHairPRC", PhotonTargets.OthersBuffered, parameters);
        }
    }

    public void setHair2()
    {
        int num;
        object[] objArray2;
        if ((Settings.TitanSkins == 1) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || photonView.isMine))
        {
            Color color;
            num = Random.Range(0, 9);
            if (num == 3)
            {
                num = 9;
            }
            var index = skin - 70;
            if (((int) FengGameManagerMKII.settings[0x20]) == 1)
            {
                index = Random.Range(0x10, 20);
            }
            if (((int) FengGameManagerMKII.settings[index]) >= 0)
            {
                num = (int) FengGameManagerMKII.settings[index];
            }
            var hairlink = (string) FengGameManagerMKII.settings[index + 5];
            var eye = Random.Range(1, 8);
            if (haseye)
            {
                eye = 0;
            }
            var flag2 = false;
            if ((hairlink.EndsWith(".jpg") || hairlink.EndsWith(".png")) || hairlink.EndsWith(".jpeg"))
            {
                flag2 = true;
            }
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && photonView.isMine)
            {
                if (flag2)
                {
                    objArray2 = new object[] { num, eye, hairlink };
                    photonView.RPC("setHairRPC2", PhotonTargets.AllBuffered, objArray2);
                }
                else
                {
                    color = HeroCostume.costume[Random.Range(0, HeroCostume.costume.Length - 5)].hair_color;
                    objArray2 = new object[] { num, eye, color.r, color.g, color.b };
                    photonView.RPC("setHairPRC", PhotonTargets.AllBuffered, objArray2);
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (flag2)
                {
                    StartCoroutine(loadskinE(num, eye, hairlink));
                }
                else
                {
                    color = HeroCostume.costume[Random.Range(0, HeroCostume.costume.Length - 5)].hair_color;
                    setHairPRC(num, eye, color.r, color.g, color.b);
                }
            }
        }
        else
        {
            num = Random.Range(0, CostumeHair.hairsM.Length);
            if (num == 3)
            {
                num = 9;
            }
            Destroy(part_hair);
            hairType = num;
            hair = CostumeHair.hairsM[num];
            if (hair.hair == string.Empty)
            {
                hair = CostumeHair.hairsM[9];
                hairType = 9;
            }
            part_hair = (GameObject) Instantiate(Resources.Load("Character/" + hair.hair));
            part_hair.transform.parent = hair_go_ref.transform.parent;
            part_hair.transform.position = hair_go_ref.transform.position;
            part_hair.transform.rotation = hair_go_ref.transform.rotation;
            part_hair.transform.localScale = hair_go_ref.transform.localScale;
            part_hair.renderer.material = CharacterMaterials.materials[hair.texture];
            part_hair.renderer.material.color = HeroCostume.costume[Random.Range(0, HeroCostume.costume.Length - 5)].hair_color;
            var id = Random.Range(1, 8);
            setFacialTexture(eye, id);
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && photonView.isMine)
            {
                objArray2 = new object[] { hairType, id, part_hair.renderer.material.color.r, part_hair.renderer.material.color.g, part_hair.renderer.material.color.b };
                photonView.RPC("setHairPRC", PhotonTargets.OthersBuffered, objArray2);
            }
        }
    }

    [RPC]
    private void setHairPRC(int type, int eye_type, float c1, float c2, float c3)
    {
        Destroy(part_hair);
        hair = CostumeHair.hairsM[type];
        hairType = type;
        if (hair.hair != string.Empty)
        {
            var obj2 = (GameObject) Instantiate(Resources.Load("Character/" + hair.hair));
            obj2.transform.parent = hair_go_ref.transform.parent;
            obj2.transform.position = hair_go_ref.transform.position;
            obj2.transform.rotation = hair_go_ref.transform.rotation;
            obj2.transform.localScale = hair_go_ref.transform.localScale;
            obj2.renderer.material = CharacterMaterials.materials[hair.texture];
            obj2.renderer.material.color = new Color(c1, c2, c3);
            part_hair = obj2;
        }
        setFacialTexture(eye, eye_type);
    }

    [RPC]
    public void setHairRPC2(int hair, int eye, string hairlink)
    {
        if (Settings.TitanSkins == 1)
        {
            StartCoroutine(loadskinE(hair, eye, hairlink));
        }
    }

    public void setPunkHair()
    {
        Destroy(part_hair);
        hair = CostumeHair.hairsM[3];
        hairType = 3;
        var obj2 = (GameObject) Instantiate(Resources.Load("Character/" + hair.hair));
        obj2.transform.parent = hair_go_ref.transform.parent;
        obj2.transform.position = hair_go_ref.transform.position;
        obj2.transform.rotation = hair_go_ref.transform.rotation;
        obj2.transform.localScale = hair_go_ref.transform.localScale;
        obj2.renderer.material = CharacterMaterials.materials[hair.texture];
        switch (Random.Range(1, 4))
        {
            case 1:
                obj2.renderer.material.color = FengColor.hairPunk1;
                break;

            case 2:
                obj2.renderer.material.color = FengColor.hairPunk2;
                break;

            case 3:
                obj2.renderer.material.color = FengColor.hairPunk3;
                break;
        }
        part_hair = obj2;
        setFacialTexture(eye, 0);
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && photonView.isMine)
        {
            var parameters = new object[] { hairType, 0, part_hair.renderer.material.color.r, part_hair.renderer.material.color.g, part_hair.renderer.material.color.b };
            photonView.RPC("setHairPRC", PhotonTargets.OthersBuffered, parameters);
        }
    }

    public void setVar(int skin, bool haseye)
    {
        this.skin = skin;
        this.haseye = haseye;
    }

}

