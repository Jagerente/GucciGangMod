using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class Horse : MonoBehaviour
{
    private float awayTimer;
    private TITAN_CONTROLLER controller;
    public GameObject dust;
    public GameObject myHero;
    private Vector3 setPoint;
    private float speed = 45f;
    private string State = "idle";
    private float timeElapsed;

    private void crossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = {aniName, time};
            photonView.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    private void followed()
    {
        if (myHero != null)
        {
            State = "follow";
            setPoint = myHero.transform.position + Vector3.right * Random.Range(-6, 6) + Vector3.forward * Random.Range(-6, 6);
            setPoint.y = getHeight(setPoint + Vector3.up * 5f);
            awayTimer = 0f;
        }
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

    public bool IsGrounded()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        LayerMask mask2 = 1 << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask3 = mask2 | mask;
        return Physics.Raycast(gameObject.transform.position + Vector3.up * 0.1f, -Vector3.up, 0.3f, mask3.value);
    }

    private void LateUpdate()
    {
        if (myHero == null && photonView.isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        if (State == "mounted")
        {
            if (myHero == null)
            {
                unmounted();
                return;
            }

            myHero.transform.position = transform.position + Vector3.up * 1.68f;
            myHero.transform.rotation = transform.rotation;
            myHero.rigidbody.velocity = rigidbody.velocity;
            if (controller.targetDirection != -874f)
            {
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, controller.targetDirection, 0f), 100f * Time.deltaTime / (rigidbody.velocity.magnitude + 20f));
                if (controller.isWALKDown)
                {
                    rigidbody.AddForce(transform.forward * speed * 0.6f, ForceMode.Acceleration);
                    if (rigidbody.velocity.magnitude >= speed * 0.6f)
                    {
                        rigidbody.AddForce(-speed * 0.6f * rigidbody.velocity.normalized, ForceMode.Acceleration);
                    }
                }
                else
                {
                    rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
                    if (rigidbody.velocity.magnitude >= speed)
                    {
                        rigidbody.AddForce(-speed * rigidbody.velocity.normalized, ForceMode.Acceleration);
                    }
                }

                if (rigidbody.velocity.magnitude > 8f)
                {
                    if (!animation.IsPlaying("horse_Run"))
                    {
                        crossFade("horse_Run", 0.1f);
                    }

                    if (!myHero.animation.IsPlaying("horse_Run"))
                    {
                        myHero.GetComponent<HERO>().crossFade("horse_run", 0.1f);
                    }

                    if (!dust.GetComponent<ParticleSystem>().enableEmission)
                    {
                        dust.GetComponent<ParticleSystem>().enableEmission = true;
                        object[] parameters = {true};
                        photonView.RPC("setDust", PhotonTargets.Others, parameters);
                    }
                }
                else
                {
                    if (!animation.IsPlaying("horse_WALK"))
                    {
                        crossFade("horse_WALK", 0.1f);
                    }

                    if (!myHero.animation.IsPlaying("horse_idle"))
                    {
                        myHero.GetComponent<HERO>().crossFade("horse_idle", 0.1f);
                    }

                    if (dust.GetComponent<ParticleSystem>().enableEmission)
                    {
                        dust.GetComponent<ParticleSystem>().enableEmission = false;
                        object[] objArray2 = {false};
                        photonView.RPC("setDust", PhotonTargets.Others, objArray2);
                    }
                }
            }
            else
            {
                toIdleAnimation();
                if (rigidbody.velocity.magnitude > 15f)
                {
                    if (!myHero.animation.IsPlaying("horse_Run"))
                    {
                        myHero.GetComponent<HERO>().crossFade("horse_run", 0.1f);
                    }
                }
                else if (!myHero.animation.IsPlaying("horse_idle"))
                {
                    myHero.GetComponent<HERO>().crossFade("horse_idle", 0.1f);
                }
            }

            if ((controller.isAttackDown || controller.isAttackIIDown) && IsGrounded())
            {
                rigidbody.AddForce(Vector3.up * 25f, ForceMode.VelocityChange);
            }
        }
        else if (State == "follow")
        {
            if (myHero == null)
            {
                unmounted();
                return;
            }

            if (rigidbody.velocity.magnitude > 8f)
            {
                if (!animation.IsPlaying("horse_Run"))
                {
                    crossFade("horse_Run", 0.1f);
                }

                if (!dust.GetComponent<ParticleSystem>().enableEmission)
                {
                    dust.GetComponent<ParticleSystem>().enableEmission = true;
                    object[] objArray3 = {true};
                    photonView.RPC("setDust", PhotonTargets.Others, objArray3);
                }
            }
            else
            {
                if (!animation.IsPlaying("horse_WALK"))
                {
                    crossFade("horse_WALK", 0.1f);
                }

                if (dust.GetComponent<ParticleSystem>().enableEmission)
                {
                    dust.GetComponent<ParticleSystem>().enableEmission = false;
                    object[] objArray4 = {false};
                    photonView.RPC("setDust", PhotonTargets.Others, objArray4);
                }
            }

            var num = -Mathf.DeltaAngle(FengMath.getHorizontalAngle(transform.position, setPoint), gameObject.transform.rotation.eulerAngles.y - 90f);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0f, gameObject.transform.rotation.eulerAngles.y + num, 0f), 200f * Time.deltaTime / (rigidbody.velocity.magnitude + 20f));
            if (Vector3.Distance(setPoint, transform.position) < 20f)
            {
                rigidbody.AddForce(transform.forward * speed * 0.7f, ForceMode.Acceleration);
                if (rigidbody.velocity.magnitude >= speed)
                {
                    rigidbody.AddForce(-speed * 0.7f * rigidbody.velocity.normalized, ForceMode.Acceleration);
                }
            }
            else
            {
                rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
                if (rigidbody.velocity.magnitude >= speed)
                {
                    rigidbody.AddForce(-speed * rigidbody.velocity.normalized, ForceMode.Acceleration);
                }
            }

            timeElapsed += Time.deltaTime;
            if (timeElapsed > 0.6f)
            {
                timeElapsed = 0f;
                if (Vector3.Distance(myHero.transform.position, setPoint) > 20f)
                {
                    followed();
                }
            }

            if (Vector3.Distance(myHero.transform.position, transform.position) < 5f)
            {
                unmounted();
            }

            if (Vector3.Distance(setPoint, transform.position) < 5f)
            {
                unmounted();
            }

            awayTimer += Time.deltaTime;
            if (awayTimer > 6f)
            {
                awayTimer = 0f;
                LayerMask mask2 = 1 << LayerMask.NameToLayer("Ground");
                if (Physics.Linecast(transform.position + Vector3.up, myHero.transform.position + Vector3.up, mask2.value))
                {
                    transform.position = new Vector3(myHero.transform.position.x, getHeight(myHero.transform.position + Vector3.up * 5f), myHero.transform.position.z);
                }
            }
        }
        else if (State == "idle")
        {
            toIdleAnimation();
            if (myHero != null && Vector3.Distance(myHero.transform.position, transform.position) > 20f)
            {
                followed();
            }
        }

        rigidbody.AddForce(new Vector3(0f, -50f * rigidbody.mass, 0f));
    }

    public void mounted()
    {
        State = "mounted";
        gameObject.GetComponent<TITAN_CONTROLLER>().enabled = true;
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        animation.CrossFade(aniName, time);
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        animation.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
    }

    public void playAnimation(string aniName)
    {
        animation.Play(aniName);
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = {aniName};
            photonView.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        animation.Play(aniName);
        animation[aniName].normalizedTime = normalizedTime;
        if (PhotonNetwork.connected && photonView.isMine)
        {
            object[] parameters = {aniName, normalizedTime};
            photonView.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void setDust(bool enable)
    {
        if (dust.GetComponent<ParticleSystem>().enableEmission)
        {
            dust.GetComponent<ParticleSystem>().enableEmission = enable;
        }
    }

    private void Start()
    {
        controller = gameObject.GetComponent<TITAN_CONTROLLER>();
    }

    private void toIdleAnimation()
    {
        if (rigidbody.velocity.magnitude > 0.1f)
        {
            if (rigidbody.velocity.magnitude > 15f)
            {
                if (!animation.IsPlaying("horse_Run"))
                {
                    crossFade("horse_Run", 0.1f);
                }

                if (!dust.GetComponent<ParticleSystem>().enableEmission)
                {
                    dust.GetComponent<ParticleSystem>().enableEmission = true;
                    object[] parameters = {true};
                    photonView.RPC("setDust", PhotonTargets.Others, parameters);
                }
            }
            else
            {
                if (!animation.IsPlaying("horse_WALK"))
                {
                    crossFade("horse_WALK", 0.1f);
                }

                if (dust.GetComponent<ParticleSystem>().enableEmission)
                {
                    dust.GetComponent<ParticleSystem>().enableEmission = false;
                    object[] objArray2 = {false};
                    photonView.RPC("setDust", PhotonTargets.Others, objArray2);
                }
            }
        }
        else
        {
            if (animation.IsPlaying("horse_idle1") && animation["horse_idle1"].normalizedTime >= 1f)
            {
                crossFade("horse_idle0", 0.1f);
            }

            if (animation.IsPlaying("horse_idle2") && animation["horse_idle2"].normalizedTime >= 1f)
            {
                crossFade("horse_idle0", 0.1f);
            }

            if (animation.IsPlaying("horse_idle3") && animation["horse_idle3"].normalizedTime >= 1f)
            {
                crossFade("horse_idle0", 0.1f);
            }

            if (!animation.IsPlaying("horse_idle0") && !animation.IsPlaying("horse_idle1") && !animation.IsPlaying("horse_idle2") && !animation.IsPlaying("horse_idle3"))
            {
                crossFade("horse_idle0", 0.1f);
            }

            if (animation.IsPlaying("horse_idle0"))
            {
                var num = Random.Range(0, 10000);
                if (num < 10)
                {
                    crossFade("horse_idle1", 0.1f);
                }
                else if (num < 20)
                {
                    crossFade("horse_idle2", 0.1f);
                }
                else if (num < 30)
                {
                    crossFade("horse_idle3", 0.1f);
                }
            }

            if (dust.GetComponent<ParticleSystem>().enableEmission)
            {
                dust.GetComponent<ParticleSystem>().enableEmission = false;
                object[] objArray3 = {false};
                photonView.RPC("setDust", PhotonTargets.Others, objArray3);
            }

            rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
        }
    }

    public void unmounted()
    {
        State = "idle";
        gameObject.GetComponent<TITAN_CONTROLLER>().enabled = false;
    }
}