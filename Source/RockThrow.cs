//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RockThrow : Photon.MonoBehaviour
{
    private bool launched;
    private Vector3 oldP;
    private Vector3 r;
    private Vector3 v;

    private void explore()
    {
        GameObject obj2;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            obj2 = PhotonNetwork.Instantiate("FX/boom6", transform.position, transform.rotation, 0);
            if (transform.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null)
            {
                obj2.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID;
                obj2.GetComponent<EnemyfxIDcontainer>().titanName = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().titanName;
            }
        }
        else
        {
            obj2 = (GameObject) Instantiate(Resources.Load("FX/boom6"), transform.position, transform.rotation);
        }
        obj2.transform.localScale = transform.localScale;
        var b = 1f - (Vector3.Distance(GameObject.Find("MainCamera").transform.position, obj2.transform.position) * 0.05f);
        b = Mathf.Min(1f, b);
        GameObject.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().startShake(b, b, 0.95f);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Destroy(gameObject);
        }
        else
        {
            PhotonNetwork.Destroy(photonView);
        }
    }

    private void hitPlayer(GameObject hero)
    {
        if (((hero != null) && !hero.GetComponent<HERO>().HasDied()) && !hero.GetComponent<HERO>().isInvincible())
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!hero.GetComponent<HERO>().isGrabbed)
                {
                    hero.GetComponent<HERO>().die((v.normalized * 1000f) + (Vector3.up * 50f), false);
                }
            }
            else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !hero.GetComponent<HERO>().HasDied()) && !hero.GetComponent<HERO>().isGrabbed)
            {
                hero.GetComponent<HERO>().markDie();
                var myOwnerViewID = -1;
                var titanName = string.Empty;
                if (transform.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null)
                {
                    myOwnerViewID = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID;
                    titanName = transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().titanName;
                }
                Debug.Log("rock hit player " + titanName);
                var parameters = new object[] { (v.normalized * 1000f) + (Vector3.up * 50f), false, myOwnerViewID, titanName, true };
                hero.GetComponent<HERO>().photonView.RPC("netDie", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void initRPC(int viewID, Vector3 scale, Vector3 pos, float level)
    {
        var gameObject = PhotonView.Find(viewID).gameObject;
        var transform = gameObject.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
        this.transform.localScale = gameObject.transform.localScale;
        this.transform.parent = transform;
        this.transform.localPosition = pos;
    }

    public void launch(Vector3 v1)
    {
        launched = true;
        oldP = transform.position;
        v = v1;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            var parameters = new object[] { v, oldP };
            photonView.RPC("launchRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void launchRPC(Vector3 v, Vector3 p)
    {
        launched = true;
        var vector = p;
        transform.position = vector;
        oldP = vector;
        transform.parent = null;
        launch(v);
    }

    private void Start()
    {
        r = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    private void Update()
    {
        if (launched)
        {
            this.transform.Rotate(r);
            v -= (20f * Vector3.up) * Time.deltaTime;
            var transform = this.transform;
            transform.position += v * Time.deltaTime;
            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER) || PhotonNetwork.isMasterClient)
            {
                LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = 1 << LayerMask.NameToLayer("Players");
                LayerMask mask3 = 1 << LayerMask.NameToLayer("EnemyAABB");
                LayerMask mask4 = (mask2 | mask) | mask3;
                foreach (var hit in Physics.SphereCastAll(this.transform.position, 2.5f * this.transform.lossyScale.x, this.transform.position - oldP, Vector3.Distance(this.transform.position, oldP), mask4))
                {
                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "EnemyAABB")
                    {
                        var gameObject = hit.collider.gameObject.transform.root.gameObject;
                        if ((gameObject.GetComponent<TITAN>() != null) && !gameObject.GetComponent<TITAN>().hasDie)
                        {
                            gameObject.GetComponent<TITAN>().hitAnkle();
                            var position = this.transform.position;
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                gameObject.GetComponent<TITAN>().hitAnkle();
                            }
                            else
                            {
                                if ((this.transform.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null) && (PhotonView.Find(this.transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID) != null))
                                {
                                    position = PhotonView.Find(this.transform.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID).transform.position;
                                }
                                gameObject.GetComponent<HERO>().photonView.RPC("hitAnkleRPC", PhotonTargets.All, new object[0]);
                            }
                        }
                        explore();
                    }
                    else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Players")
                    {
                        var hero = hit.collider.gameObject.transform.root.gameObject;
                        if (hero.GetComponent<TITAN_EREN>() != null)
                        {
                            if (!hero.GetComponent<TITAN_EREN>().isHit)
                            {
                                hero.GetComponent<TITAN_EREN>().hitByTitan();
                            }
                        }
                        else if ((hero.GetComponent<HERO>() != null) && !hero.GetComponent<HERO>().isInvincible())
                        {
                            hitPlayer(hero);
                        }
                    }
                    else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Ground")
                    {
                        explore();
                    }
                }
                oldP = this.transform.position;
            }
        }
    }
}

