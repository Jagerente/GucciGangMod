//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class SpectatorMovement : MonoBehaviour
{
    public bool disable;
    public FengCustomInputs inputManager;
    private float speed = 100f;

    private void Start()
    {
        inputManager = GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>();
    }

    private void Update()
    {
        if (!disable)
        {
            float num2;
            float num3;
            var speed = this.speed;
            if (inputManager.isInput[InputCode.jump])
            {
                speed *= 3f;
            }
            if (inputManager.isInput[InputCode.up])
            {
                num2 = 1f;
            }
            else if (inputManager.isInput[InputCode.down])
            {
                num2 = -1f;
            }
            else
            {
                num2 = 0f;
            }
            if (inputManager.isInput[InputCode.left])
            {
                num3 = -1f;
            }
            else if (inputManager.isInput[InputCode.right])
            {
                num3 = 1f;
            }
            else
            {
                num3 = 0f;
            }
            var transform = this.transform;
            if (num2 > 0f)
            {
                transform.position += this.transform.forward * speed * Time.deltaTime;
            }
            else if (num2 < 0f)
            {
                transform.position -= this.transform.forward * speed * Time.deltaTime;
            }
            if (num3 > 0f)
            {
                transform.position += this.transform.right * speed * Time.deltaTime;
            }
            else if (num3 < 0f)
            {
                transform.position -= this.transform.right * speed * Time.deltaTime;
            }
            if (inputManager.isInput[InputCode.leftRope])
            {
                transform.position -= this.transform.up * speed * Time.deltaTime;
            }
            else if (inputManager.isInput[InputCode.rightRope])
            {
                transform.position += this.transform.up * speed * Time.deltaTime;
            }
        }
    }
}

