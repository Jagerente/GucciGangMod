using UnityEngine;

public class TITAN_CONTROLLER : MonoBehaviour
{
    public bool bite;
    public bool bitel;
    public bool biter;
    public bool chopl;
    public bool chopr;
    public bool choptl;
    public bool choptr;
    public bool cover;
    public Camera currentCamera;
    public float currentDirection;
    public bool grabbackl;
    public bool grabbackr;
    public bool grabfrontl;
    public bool grabfrontr;
    public bool grabnapel;
    public bool grabnaper;
    public FengCustomInputs inputManager;
    public bool isAttackDown;
    public bool isAttackIIDown;
    public bool isHorse;
    public bool isJumpDown;
    public bool isSuicide;
    public bool isWALKDown;
    public bool sit;
    public float targetDirection;

    private void Start()
    {
        inputManager = GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>();
        currentCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        int num;
        int num2;
        float y;
        float num4;
        float num5;
        float num6;
        if (isHorse)
        {
            if (FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseForward))
            {
                num = 1;
            }
            else if (FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseBack))
            {
                num = -1;
            }
            else
            {
                num = 0;
            }

            if (FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseLeft))
            {
                num2 = -1;
            }
            else if (FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseRight))
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }

            if (num2 != 0 || num != 0)
            {
                y = currentCamera.transform.rotation.eulerAngles.y;
                num4 = Mathf.Atan2(num, num2) * 57.29578f;
                num4 = -num4 + 90f;
                num5 = y + num4;
                targetDirection = num5;
            }
            else
            {
                targetDirection = -874f;
            }

            isAttackDown = false;
            isAttackIIDown = false;
            if (targetDirection != -874f)
            {
                currentDirection = targetDirection;
            }

            num6 = currentCamera.transform.rotation.eulerAngles.y - currentDirection;
            if (num6 >= 180f)
            {
                num6 -= 360f;
            }

            if (FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseJump))
            {
                isAttackDown = true;
            }

            isWALKDown = FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseWalk);
        }
        else
        {
            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanForward))
            {
                num = 1;
            }
            else if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanBack))
            {
                num = -1;
            }
            else
            {
                num = 0;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanLeft))
            {
                num2 = -1;
            }
            else if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanRight))
            {
                num2 = 1;
            }
            else
            {
                num2 = 0;
            }

            if (num2 != 0 || num != 0)
            {
                y = currentCamera.transform.rotation.eulerAngles.y;
                num4 = Mathf.Atan2(num, num2) * 57.29578f;
                num4 = -num4 + 90f;
                num5 = y + num4;
                targetDirection = num5;
            }
            else
            {
                targetDirection = -874f;
            }

            isAttackDown = false;
            isJumpDown = false;
            isAttackIIDown = false;
            isSuicide = false;
            grabbackl = false;
            grabbackr = false;
            grabfrontl = false;
            grabfrontr = false;
            grabnapel = false;
            grabnaper = false;
            choptl = false;
            chopr = false;
            chopl = false;
            choptr = false;
            bite = false;
            bitel = false;
            biter = false;
            cover = false;
            sit = false;
            if (targetDirection != -874f)
            {
                currentDirection = targetDirection;
            }

            num6 = currentCamera.transform.rotation.eulerAngles.y - currentDirection;
            if (num6 >= 180f)
            {
                num6 -= 360f;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanPunch))
            {
                isAttackDown = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanSlam))
            {
                isAttackIIDown = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanJump))
            {
                isJumpDown = true;
            }

            if (inputManager.GetComponent<FengCustomInputs>().isInputDown[InputCode.restart])
            {
                isSuicide = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanCover))
            {
                cover = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanSit))
            {
                sit = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabFront) && num6 >= 0f)
            {
                grabfrontr = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabFront) && num6 < 0f)
            {
                grabfrontl = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabBack) && num6 >= 0f)
            {
                grabbackr = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabBack) && num6 < 0f)
            {
                grabbackl = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabNape) && num6 >= 0f)
            {
                grabnaper = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanGrabNape) && num6 < 0f)
            {
                grabnapel = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanAntiAE) && num6 >= 0f)
            {
                choptr = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanAntiAE) && num6 < 0f)
            {
                choptl = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanBite) && num6 > 7.5f)
            {
                biter = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanBite) && num6 < -7.5f)
            {
                bitel = true;
            }

            if (FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanBite) && num6 >= -7.5f && num6 <= 7.5f)
            {
                bite = true;
            }

            isWALKDown = FengGameManagerMKII.inputRC.isInputTitan(InputCodeRC.titanWalk);
        }
    }
}