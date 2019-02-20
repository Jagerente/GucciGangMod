//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterCreateAnimationControl : MonoBehaviour
{
    [CompilerGenerated]
    private static Dictionary<string, int> fswitchSmap0;
    private string currentAnimation;
    private float interval = 10f;
    private HERO_SETUP setup;
    private float timeElapsed;

    private void play(string id)
    {
        currentAnimation = id;
        animation.Play(id);
    }

    public void playAttack(string id)
    {
        var key = id;
        if (key != null)
        {
            int num;
            if (fswitchSmap0 == null)
            {
                var dictionary = new Dictionary<string, int>(7);
                dictionary.Add("mikasa", 0);
                dictionary.Add("levi", 1);
                dictionary.Add("sasha", 2);
                dictionary.Add("jean", 3);
                dictionary.Add("marco", 4);
                dictionary.Add("armin", 5);
                dictionary.Add("petra", 6);
                fswitchSmap0 = dictionary;
            }
            if (fswitchSmap0.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                        currentAnimation = "attack3_1";
                        break;

                    case 1:
                        currentAnimation = "attack5";
                        break;

                    case 2:
                        currentAnimation = "special_sasha";
                        break;

                    case 3:
                        currentAnimation = "grabbed_jean";
                        break;

                    case 4:
                        currentAnimation = "special_marco_0";
                        break;

                    case 5:
                        currentAnimation = "special_armin";
                        break;

                    case 6:
                        currentAnimation = "special_petra";
                        break;
                }
            }
        }
        animation.Play(currentAnimation);
    }

    private void Start()
    {
        setup = gameObject.GetComponent<HERO_SETUP>();
        currentAnimation = "stand_levi";
        play(currentAnimation);
    }

    public void toStand()
    {
        if (setup.myCostume.sex == SEX.FEMALE)
        {
            currentAnimation = "stand";
        }
        else
        {
            currentAnimation = "stand_levi";
        }
        animation.CrossFade(currentAnimation, 0.1f);
        timeElapsed = 0f;
    }

    private void Update()
    {
        if ((currentAnimation == "stand") || (currentAnimation == "stand_levi"))
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > interval)
            {
                timeElapsed = 0f;
                if (Random.Range(1, 0x3e8) < 350)
                {
                    play("salute");
                }
                else if (Random.Range(1, 0x3e8) < 350)
                {
                    play("supply");
                }
                else
                {
                    play("dodge");
                }
            }
        }
        else if (animation[currentAnimation].normalizedTime >= 1f)
        {
            if (currentAnimation == "attack3_1")
            {
                play("attack3_2");
            }
            else if (currentAnimation == "special_sasha")
            {
                play("run_sasha");
            }
            else
            {
                toStand();
            }
        }
    }
}

