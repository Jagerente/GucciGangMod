//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using UnityEngine;

public class CheckHitGround : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.isGrounded = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAABB"))
        {
            this.isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.isGrounded = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAABB"))
        {
            this.isGrounded = true;
        }
    }
}

