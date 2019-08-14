using System.Collections;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class PVPcheckPoint : MonoBehaviour
{
    private bool annie;
    public GameObject[] chkPtNextArr;
    public GameObject[] chkPtPreviousArr;
    public static ArrayList chkPts;
    private float getPtsInterval = 20f;
    private float getPtsTimer;
    public bool hasAnnie;
    private float hitTestR = 15f;
    public GameObject humanCyc;
    public float humanPt;
    public float humanPtMax = 40f;
    public int id;
    public bool isBase;
    public int normalTitanRate = 70;
    private bool playerOn;
    public float size = 1f;
    private float spawnTitanTimer;
    public CheckPointState state;
    private GameObject supply;
    private float syncInterval = 0.6f;
    private float syncTimer;
    public GameObject titanCyc;
    public float titanInterval = 30f;
    private bool titanOn;
    public float titanPt;
    public float titanPtMax = 40f;

    [RPC]
    private void changeHumanPt(float pt)
    {
        humanPt = pt;
    }

    [RPC]
    private void changeState(int num)
    {
        if (num == 0)
        {
            state = CheckPointState.Non;
        }

        if (num == 1)
        {
            state = CheckPointState.Human;
        }

        if (num == 2)
        {
            state = CheckPointState.Titan;
        }
    }

    [RPC]
    private void changeTitanPt(float pt)
    {
        titanPt = pt;
    }

    private void checkIfBeingCapture()
    {
        int num;
        playerOn = false;
        titanOn = false;
        var objArray = GameObject.FindGameObjectsWithTag("Player");
        var objArray2 = GameObject.FindGameObjectsWithTag("titan");
        for (num = 0; num < objArray.Length; num++)
        {
            if (Vector3.Distance(objArray[num].transform.position, transform.position) < hitTestR)
            {
                playerOn = true;
                if (state == CheckPointState.Human && objArray[num].GetPhotonView().isMine)
                {
                    if (GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint != gameObject)
                    {
                        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint = gameObject;
                        string[] msg = {"Respawn point changed to point ", id.ToString(), "."};
                        InRoomChat.SystemMessageLocal(msg);
                    }

                    break;
                }
            }
        }

        for (num = 0; num < objArray2.Length; num++)
        {
            if (Vector3.Distance(objArray2[num].transform.position, transform.position) < hitTestR + 5f && (objArray2[num].GetComponent<TITAN>() == null || !objArray2[num].GetComponent<TITAN>().hasDie))
            {
                titanOn = true;
                if (state == CheckPointState.Titan && objArray2[num].GetPhotonView().isMine && objArray2[num].GetComponent<TITAN>() != null && objArray2[num].GetComponent<TITAN>().nonAI)
                {
                    if (GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint != gameObject)
                    {
                        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint = gameObject;
                        string[] msg = {"Respawn point changed to point ", id.ToString(), "."};
                        InRoomChat.SystemMessageLocal(msg);
                    }

                    break;
                }
            }
        }
    }

    private bool checkIfHumanWins()
    {
        for (var i = 0; i < chkPts.Count; i++)
        {
            if ((chkPts[i] as PVPcheckPoint).state != CheckPointState.Human)
            {
                return false;
            }
        }

        return true;
    }

    private bool checkIfTitanWins()
    {
        for (var i = 0; i < chkPts.Count; i++)
        {
            if ((chkPts[i] as PVPcheckPoint).state != CheckPointState.Titan)
            {
                return false;
            }
        }

        return true;
    }

    private float getHeight(Vector3 pt)
    {
        RaycastHit hit;
        LayerMask mask2 = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(pt, -Vector3.up, out hit, 1000f, mask2.value))
        {
            return hit.point.y;
        }

        return 0f;
    }

    public string getStateString()
    {
        if (state == CheckPointState.Human)
        {
            return "[" + ColorSet.color_human + "]H[-]";
        }

        if (state == CheckPointState.Titan)
        {
            return "[" + ColorSet.color_titan_player + "]T[-]";
        }

        return "[" + ColorSet.color_D + "]_[-]";
    }

    private void humanGetsPoint()
    {
        if (humanPt >= humanPtMax)
        {
            humanPt = humanPtMax;
            titanPt = 0f;
            syncPts();
            state = CheckPointState.Human;
            object[] parameters = {1};
            photonView.RPC("changeState", PhotonTargets.All, parameters);
            if (LevelInfo.getInfo(FengGameManagerMKII.level).mapName != "The City I")
            {
                supply = PhotonNetwork.Instantiate("aot_supply", transform.position - Vector3.up * (transform.position.y - getHeight(transform.position)), transform.rotation, 0);
            }

            var component = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>();
            component.PVPhumanScore += 2;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkPVPpts();
            if (checkIfHumanWins())
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameWin();
            }
        }
        else
        {
            humanPt += Time.deltaTime;
        }
    }

    private void humanLosePoint()
    {
        if (humanPt > 0f)
        {
            humanPt -= Time.deltaTime * 3f;
            if (humanPt <= 0f)
            {
                humanPt = 0f;
                syncPts();
                if (state != CheckPointState.Titan)
                {
                    state = CheckPointState.Non;
                    object[] parameters = {0};
                    photonView.RPC("changeState", PhotonTargets.Others, parameters);
                }
            }
        }
    }

    private void newTitan()
    {
        var obj2 = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().spawnTitan(normalTitanRate, transform.position - Vector3.up * (transform.position.y - getHeight(transform.position)), transform.rotation, false);
        if (LevelInfo.getInfo(FengGameManagerMKII.level).mapName == "The City I")
        {
            obj2.GetComponent<TITAN>().chaseDistance = 120f;
        }
        else
        {
            obj2.GetComponent<TITAN>().chaseDistance = 200f;
        }

        obj2.GetComponent<TITAN>().PVPfromCheckPt = this;
    }

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Destroy(gameObject);
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.PVP_CAPTURE)
        {
            if (photonView.isMine)
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
        else
        {
            chkPts.Add(this);
            IComparer comparer = new IComparerPVPchkPtID();
            chkPts.Sort(comparer);
            if (humanPt == humanPtMax)
            {
                state = CheckPointState.Human;
                if (photonView.isMine && LevelInfo.getInfo(FengGameManagerMKII.level).mapName != "The City I")
                {
                    supply = PhotonNetwork.Instantiate("aot_supply", transform.position - Vector3.up * (transform.position.y - getHeight(transform.position)), transform.rotation, 0);
                }
            }
            else if (photonView.isMine && !hasAnnie)
            {
                if (Random.Range(0, 100) < 50)
                {
                    var num = Random.Range(1, 2);
                    for (var i = 0; i < num; i++)
                    {
                        newTitan();
                    }
                }

                if (isBase)
                {
                    newTitan();
                }
            }

            if (titanPt == titanPtMax)
            {
                state = CheckPointState.Titan;
            }

            hitTestR = 15f * size;
            transform.localScale = new Vector3(size, size, size);
        }
    }

    private void syncPts()
    {
        object[] parameters = {titanPt};
        photonView.RPC("changeTitanPt", PhotonTargets.Others, parameters);
        object[] objArray2 = {humanPt};
        photonView.RPC("changeHumanPt", PhotonTargets.Others, objArray2);
    }

    private void titanGetsPoint()
    {
        if (titanPt >= titanPtMax)
        {
            titanPt = titanPtMax;
            humanPt = 0f;
            syncPts();
            if (state == CheckPointState.Human && supply != null)
            {
                PhotonNetwork.Destroy(supply);
            }

            state = CheckPointState.Titan;
            object[] parameters = {2};
            photonView.RPC("changeState", PhotonTargets.All, parameters);
            var component = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>();
            component.PVPtitanScore += 2;
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkPVPpts();
            if (checkIfTitanWins())
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameLose();
            }

            if (hasAnnie)
            {
                if (!annie)
                {
                    annie = true;
                    PhotonNetwork.Instantiate("FEMALE_TITAN", transform.position - Vector3.up * (transform.position.y - getHeight(transform.position)), transform.rotation, 0);
                }
                else
                {
                    newTitan();
                }
            }
            else
            {
                newTitan();
            }
        }
        else
        {
            titanPt += Time.deltaTime;
        }
    }

    private void titanLosePoint()
    {
        if (titanPt > 0f)
        {
            titanPt -= Time.deltaTime * 3f;
            if (titanPt <= 0f)
            {
                titanPt = 0f;
                syncPts();
                if (state != CheckPointState.Human)
                {
                    state = CheckPointState.Non;
                    object[] parameters = {0};
                    photonView.RPC("changeState", PhotonTargets.All, parameters);
                }
            }
        }
    }

    private void Update()
    {
        var x = humanPt / humanPtMax;
        var num2 = titanPt / titanPtMax;
        if (!photonView.isMine)
        {
            x = humanPt / humanPtMax;
            num2 = titanPt / titanPtMax;
            humanCyc.transform.localScale = new Vector3(x, x, 1f);
            titanCyc.transform.localScale = new Vector3(num2, num2, 1f);
            syncTimer += Time.deltaTime;
            if (syncTimer > syncInterval)
            {
                syncTimer = 0f;
                checkIfBeingCapture();
            }
        }
        else
        {
            if (state == CheckPointState.Non)
            {
                if (playerOn && !titanOn)
                {
                    humanGetsPoint();
                    titanLosePoint();
                }
                else if (titanOn && !playerOn)
                {
                    titanGetsPoint();
                    humanLosePoint();
                }
                else
                {
                    humanLosePoint();
                    titanLosePoint();
                }
            }
            else if (state == CheckPointState.Human)
            {
                if (titanOn && !playerOn)
                {
                    titanGetsPoint();
                }
                else
                {
                    titanLosePoint();
                }

                getPtsTimer += Time.deltaTime;
                if (getPtsTimer > getPtsInterval)
                {
                    getPtsTimer = 0f;
                    if (!isBase)
                    {
                        var component = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>();
                        component.PVPhumanScore++;
                    }

                    GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkPVPpts();
                }
            }
            else if (state == CheckPointState.Titan)
            {
                if (playerOn && !titanOn)
                {
                    humanGetsPoint();
                }
                else
                {
                    humanLosePoint();
                }

                getPtsTimer += Time.deltaTime;
                if (getPtsTimer > getPtsInterval)
                {
                    getPtsTimer = 0f;
                    if (!isBase)
                    {
                        var local2 = GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>();
                        local2.PVPtitanScore++;
                    }

                    GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkPVPpts();
                }

                spawnTitanTimer += Time.deltaTime;
                if (spawnTitanTimer > titanInterval)
                {
                    spawnTitanTimer = 0f;
                    if (LevelInfo.getInfo(FengGameManagerMKII.level).mapName == "The City I")
                    {
                        if (GameObject.FindGameObjectsWithTag("titan").Length < 12)
                        {
                            newTitan();
                        }
                    }
                    else if (GameObject.FindGameObjectsWithTag("titan").Length < 20)
                    {
                        newTitan();
                    }
                }
            }

            syncTimer += Time.deltaTime;
            if (syncTimer > syncInterval)
            {
                syncTimer = 0f;
                checkIfBeingCapture();
                syncPts();
            }

            x = humanPt / humanPtMax;
            num2 = titanPt / titanPtMax;
            humanCyc.transform.localScale = new Vector3(x, x, 1f);
            titanCyc.transform.localScale = new Vector3(num2, num2, 1f);
        }
    }

    public GameObject chkPtNext
    {
        get
        {
            if (chkPtNextArr.Length <= 0)
            {
                return null;
            }

            return chkPtNextArr[Random.Range(0, chkPtNextArr.Length)];
        }
    }

    public GameObject chkPtPrevious
    {
        get
        {
            if (chkPtPreviousArr.Length <= 0)
            {
                return null;
            }

            return chkPtPreviousArr[Random.Range(0, chkPtPreviousArr.Length)];
        }
    }
}