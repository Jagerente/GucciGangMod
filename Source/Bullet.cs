//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour
{
    private Vector3 heightOffSet = Vector3.up * 0.48f;
    private bool isdestroying;
    private float killTime;
    private float killTime2;
    private Vector3 launchOffSet = Vector3.zero;
    private bool left = true;
    public bool leviMode;
    public float leviShootTime;
    private LineRenderer lineRenderer;
    private GameObject master;
    private GameObject myRef;
    public TITAN myTitan;
    private ArrayList nodes = new ArrayList();
    private int phase;
    private GameObject rope;
    private int spiralcount;
    private ArrayList spiralNodes;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;

    public void checkTitan()
    {
        var obj2 = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().main_object;
        if (((obj2 != null) && (master != null)) && (master == obj2))
        {
            RaycastHit hit;
            LayerMask mask = 1 << LayerMask.NameToLayer("PlayerAttackBox");
            if (Physics.Raycast(transform.position, velocity, out hit, 10f, mask.value))
            {
                var collider = hit.collider;
                if (collider.name.Contains("PlayerDetectorRC"))
                {
                    var component = collider.transform.root.gameObject.GetComponent<TITAN>();
                    if (component != null)
                    {
                        if (myTitan == null)
                        {
                            myTitan = component;
                            myTitan.isHooked = true;
                        }
                        else if (myTitan != component)
                        {
                            myTitan.isHooked = false;
                            myTitan = component;
                            myTitan.isHooked = true;
                        }
                    }
                }
            }
        }
    }

    public void disable()
    {
        phase = 2;
        killTime = 0f;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            var parameters = new object[] { 2 };
            photonView.RPC("setPhase", PhotonTargets.Others, parameters);
        }
    }

    private void FixedUpdate()
    {
        if (((phase == 2) || (phase == 1)) && leviMode)
        {
            spiralcount++;
            if (spiralcount >= 60)
            {
                isdestroying = true;
                removeMe();
                return;
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !photonView.isMine)
        {
            if (phase == 0)
            {
                var transform = gameObject.transform;
                transform.position += ((velocity * Time.deltaTime) * 50f) + (velocity2 * Time.deltaTime);
                nodes.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
            }
        }
        else if (phase == 0)
        {
            RaycastHit hit;
            checkTitan();
            var transform3 = gameObject.transform;
            transform3.position += ((velocity * Time.deltaTime) * 50f) + (velocity2 * Time.deltaTime);
            LayerMask mask = 1 << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask2 = 1 << LayerMask.NameToLayer("Ground");
            LayerMask mask3 = 1 << LayerMask.NameToLayer("NetworkObject");
            LayerMask mask4 = (mask | mask2) | mask3;
            var flag2 = false;
            var flag3 = false;
            if (nodes.Count > 1)
            {
                flag3 = Physics.Linecast((Vector3) nodes[nodes.Count - 2], gameObject.transform.position, out hit, mask4.value);
            }
            else
            {
                flag3 = Physics.Linecast((Vector3) nodes[nodes.Count - 1], gameObject.transform.position, out hit, mask4.value);
            }
            if (flag3)
            {
                var flag4 = true;
                if (hit.collider.transform.gameObject.layer == LayerMask.NameToLayer("EnemyBox"))
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        var parameters = new object[] { hit.collider.transform.root.gameObject.GetPhotonView().viewID };
                        photonView.RPC("tieMeToOBJ", PhotonTargets.Others, parameters);
                    }
                    master.GetComponent<HERO>().lastHook = hit.collider.transform.root;
                    transform.parent = hit.collider.transform;
                }
                else if (hit.collider.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    master.GetComponent<HERO>().lastHook = null;
                }
                else if (((hit.collider.transform.gameObject.layer == LayerMask.NameToLayer("NetworkObject")) && (hit.collider.transform.gameObject.tag == "Player")) && !leviMode)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        var objArray3 = new object[] { hit.collider.transform.root.gameObject.GetPhotonView().viewID };
                        photonView.RPC("tieMeToOBJ", PhotonTargets.Others, objArray3);
                    }
                    master.GetComponent<HERO>().hookToHuman(hit.collider.transform.root.gameObject, transform.position);
                    transform.parent = hit.collider.transform;
                    master.GetComponent<HERO>().lastHook = null;
                }
                else
                {
                    flag4 = false;
                }
                if (phase == 2)
                {
                    flag4 = false;
                }
                if (flag4)
                {
                    master.GetComponent<HERO>().launch(hit.point, left, leviMode);
                    transform.position = hit.point;
                    if (phase != 2)
                    {
                        phase = 1;
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            var objArray4 = new object[] { 1 };
                            photonView.RPC("setPhase", PhotonTargets.Others, objArray4);
                            var objArray5 = new object[] { transform.position };
                            photonView.RPC("tieMeTo", PhotonTargets.Others, objArray5);
                        }
                        if (leviMode)
                        {
                            getSpiral(master.transform.position, master.transform.rotation.eulerAngles);
                        }
                        flag2 = true;
                    }
                }
            }
            nodes.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
            if (!flag2)
            {
                killTime2 += Time.deltaTime;
                if (killTime2 > 0.8f)
                {
                    phase = 4;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        var objArray6 = new object[] { 4 };
                        photonView.RPC("setPhase", PhotonTargets.Others, objArray6);
                    }
                }
            }
        }
    }

    private void getSpiral(Vector3 masterposition, Vector3 masterrotation)
    {
        var num = 1.2f;
        var num2 = 30f;
        var num3 = 2f;
        var num4 = 0.5f;
        num = 30f;
        num3 = 0.05f + (spiralcount * 0.03f);
        if (spiralcount < 5)
        {
            num = Vector2.Distance(new Vector2(masterposition.x, masterposition.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
        }
        else
        {
            num = 1.2f + ((60 - spiralcount) * 0.1f);
        }
        num4 -= spiralcount * 0.06f;
        var num6 = num / num2;
        var num7 = num3 / num2;
        var num8 = (num7 * 2f) * 3.141593f;
        num4 *= 6.283185f;
        spiralNodes = new ArrayList();
        for (var i = 1; i <= num2; i++)
        {
            var num10 = (i * num6) * (1f + (0.05f * i));
            var f = (((i * num8) + num4) + 1.256637f) + (masterrotation.y * 0.0173f);
            var x = Mathf.Cos(f) * num10;
            var z = -Mathf.Sin(f) * num10;
            spiralNodes.Add(new Vector3(x, 0f, z));
        }
    }

    public bool isHooked()
    {
        return (phase == 1);
    }

    [RPC]
    private void killObject()
    {
        Destroy(rope);
        Destroy(gameObject);
    }

    public void launch(Vector3 v, Vector3 v2, string launcher_ref, bool isLeft, GameObject hero, bool leviMode = false)
    {
        if (phase != 2)
        {
            master = hero;
            velocity = v;
            var f = Mathf.Acos(Vector3.Dot(v.normalized, v2.normalized)) * 57.29578f;
            if (Mathf.Abs(f) > 90f)
            {
                velocity2 = Vector3.zero;
            }
            else
            {
                velocity2 = Vector3.Project(v2, v);
            }
            if (launcher_ref == "hookRefL1")
            {
                myRef = hero.GetComponent<HERO>().hookRefL1;
            }
            if (launcher_ref == "hookRefL2")
            {
                myRef = hero.GetComponent<HERO>().hookRefL2;
            }
            if (launcher_ref == "hookRefR1")
            {
                myRef = hero.GetComponent<HERO>().hookRefR1;
            }
            if (launcher_ref == "hookRefR2")
            {
                myRef = hero.GetComponent<HERO>().hookRefR2;
            }
            nodes = new ArrayList();
            nodes.Add(myRef.transform.position);
            phase = 0;
            this.leviMode = leviMode;
            left = isLeft;
            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && photonView.isMine)
            {
                var parameters = new object[] { hero.GetComponent<HERO>().photonView.viewID, launcher_ref };
                photonView.RPC("myMasterIs", PhotonTargets.Others, parameters);
                var objArray2 = new object[] { v, velocity2, left };
                photonView.RPC("setVelocityAndLeft", PhotonTargets.Others, objArray2);
            }
            transform.position = myRef.transform.position;
            transform.rotation = Quaternion.LookRotation(v.normalized);
        }
    }

    [RPC]
    private void myMasterIs(int id, string launcherRef)
    {
        master = PhotonView.Find(id).gameObject;
        if (launcherRef == "hookRefL1")
        {
            myRef = master.GetComponent<HERO>().hookRefL1;
        }
        if (launcherRef == "hookRefL2")
        {
            myRef = master.GetComponent<HERO>().hookRefL2;
        }
        if (launcherRef == "hookRefR1")
        {
            myRef = master.GetComponent<HERO>().hookRefR1;
        }
        if (launcherRef == "hookRefR2")
        {
            myRef = master.GetComponent<HERO>().hookRefR2;
        }
    }

    [RPC]
    private void netLaunch(Vector3 newPosition)
    {
        nodes = new ArrayList();
        nodes.Add(newPosition);
    }

    [RPC]
    private void netUpdateLeviSpiral(Vector3 newPosition, Vector3 masterPosition, Vector3 masterrotation)
    {
        phase = 2;
        leviMode = true;
        getSpiral(masterPosition, masterrotation);
        var vector = masterPosition - ((Vector3) spiralNodes[0]);
        lineRenderer.SetVertexCount(spiralNodes.Count - ((int) (spiralcount * 0.5f)));
        for (var i = 0; i <= ((spiralNodes.Count - 1) - (spiralcount * 0.5f)); i++)
        {
            if (spiralcount < 5)
            {
                var position = ((Vector3) spiralNodes[i]) + vector;
                var num2 = (spiralNodes.Count - 1) - (spiralcount * 0.5f);
                position = new Vector3(position.x, (position.y * ((num2 - i) / num2)) + (newPosition.y * (i / num2)), position.z);
                lineRenderer.SetPosition(i, position);
            }
            else
            {
                lineRenderer.SetPosition(i, ((Vector3) spiralNodes[i]) + vector);
            }
        }
    }

    [RPC]
    private void netUpdatePhase1(Vector3 newPosition, Vector3 masterPosition)
    {
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, newPosition);
        lineRenderer.SetPosition(1, masterPosition);
        transform.position = newPosition;
    }

    private void OnDestroy()
    {
        if (FengGameManagerMKII.instance != null)
        {
            FengGameManagerMKII.instance.removeHook(this);
        }
        if (myTitan != null)
        {
            myTitan.isHooked = false;
        }
        Destroy(rope);
    }

    public void removeMe()
    {
        isdestroying = true;
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && photonView.isMine)
        {
            PhotonNetwork.Destroy(photonView);
            PhotonNetwork.RemoveRPCs(photonView);
        }
        else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Destroy(rope);
            Destroy(gameObject);
        }
    }

    private void setLinePhase0()
    {
        if (master == null)
        {
            Destroy(rope);
            Destroy(gameObject);
        }
        else if (nodes.Count > 0)
        {
            var vector = myRef.transform.position - ((Vector3) nodes[0]);
            lineRenderer.SetVertexCount(nodes.Count);
            for (var i = 0; i <= (nodes.Count - 1); i++)
            {
                lineRenderer.SetPosition(i, ((Vector3) nodes[i]) + vector * Mathf.Pow(0.75f, i));
            }
            if (nodes.Count > 1)
            {
                lineRenderer.SetPosition(1, myRef.transform.position);
            }
        }
    }

    [RPC]
    private void setPhase(int value)
    {
        phase = value;
    }

    [RPC]
    private void setVelocityAndLeft(Vector3 value, Vector3 v2, bool l)
    {
        velocity = value;
        velocity2 = v2;
        left = l;
        transform.rotation = Quaternion.LookRotation(value.normalized);
    }

    private void Start()
    {
        rope = (GameObject) Instantiate(Resources.Load("rope"));
        lineRenderer = rope.GetComponent<LineRenderer>();
        GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().addHook(this);
    }

    [RPC]
    private void tieMeTo(Vector3 p)
    {
        transform.position = p;
    }

    [RPC]
    private void tieMeToOBJ(int id)
    {
        transform.parent = PhotonView.Find(id).gameObject.transform;
    }

    public void update()
    {
        if (master == null)
        {
            removeMe();
        }
        else if (!isdestroying)
        {
            if (leviMode)
            {
                leviShootTime += Time.deltaTime;
                if (leviShootTime > 0.4f)
                {
                    phase = 2;
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (phase == 0)
            {
                setLinePhase0();
            }
            else if (phase == 1)
            {
                var vector = transform.position - myRef.transform.position;
                var vector2 = transform.position + myRef.transform.position;
                var velocity = master.rigidbody.velocity;
                var magnitude = velocity.magnitude;
                var f = vector.magnitude;
                var num3 = (int) ((f + magnitude) / 5f);
                num3 = Mathf.Clamp(num3, 2, 6);
                lineRenderer.SetVertexCount(num3);
                lineRenderer.SetPosition(0, myRef.transform.position);
                var index = 1;
                var num5 = Mathf.Pow(f, 0.3f);
                while (index < num3)
                {
                    var num6 = num3 / 2;
                    float num7 = Mathf.Abs(index - num6);
                    var num8 = (num6 - num7) / num6;
                    num8 = Mathf.Pow(num8, 0.5f);
                    var max = ((num5 + magnitude) * 0.0015f) * num8;
                    lineRenderer.SetPosition(index, (((new Vector3(Random.Range(-max, max), Random.Range(-max, max), Random.Range(-max, max)) + myRef.transform.position) + (vector * (index / ((float) num3)))) - (((Vector3.up * num5) * 0.05f) * num8)) - (((velocity * 0.001f) * num8) * num5));
                    index++;
                }
                lineRenderer.SetPosition(num3 - 1, transform.position);
            }
            else if (phase == 2)
            {
                if (!leviMode)
                {
                    lineRenderer.SetVertexCount(2);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, myRef.transform.position);
                    killTime += Time.deltaTime * 0.2f;
                    lineRenderer.SetWidth(0.1f - killTime, 0.1f - killTime);
                    if (killTime > 0.1f)
                    {
                        removeMe();
                    }
                }
                else
                {
                    getSpiral(master.transform.position, master.transform.rotation.eulerAngles);
                    var vector4 = myRef.transform.position - ((Vector3) spiralNodes[0]);
                    lineRenderer.SetVertexCount(spiralNodes.Count - ((int) (spiralcount * 0.5f)));
                    for (var i = 0; i <= ((spiralNodes.Count - 1) - (spiralcount * 0.5f)); i++)
                    {
                        if (spiralcount < 5)
                        {
                            var position = ((Vector3) spiralNodes[i]) + vector4;
                            var num11 = (spiralNodes.Count - 1) - (spiralcount * 0.5f);
                            position = new Vector3(position.x, (position.y * ((num11 - i) / num11)) + (gameObject.transform.position.y * (i / num11)), position.z);
                            lineRenderer.SetPosition(i, position);
                        }
                        else
                        {
                            lineRenderer.SetPosition(i, ((Vector3) spiralNodes[i]) + vector4);
                        }
                    }
                }
            }
            else if (phase == 4)
            {
                var transform = gameObject.transform;
                transform.position += velocity + velocity2 * Time.deltaTime;
                nodes.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
                var vector10 = myRef.transform.position - ((Vector3) nodes[0]);
                for (var j = 0; j <= (nodes.Count - 1); j++)
                {
                    lineRenderer.SetVertexCount(nodes.Count);
                    lineRenderer.SetPosition(j, ((Vector3) nodes[j]) + vector10 * Mathf.Pow(0.5f, j));
                }
                killTime2 += Time.deltaTime;
                if (killTime2 > 0.8f)
                {
                    killTime += Time.deltaTime * 0.2f;
                    lineRenderer.SetWidth(0.1f - killTime, 0.1f - killTime);
                    if (killTime > 0.1f)
                    {
                        removeMe();
                    }
                }
            }
        }
    }
}

