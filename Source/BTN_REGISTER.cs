//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class BTN_REGISTER : MonoBehaviour
{
    public GameObject email;
    private bool invalid;
    public GameObject logincomponent;
    public GameObject nameGO;
    public GameObject output;
    public GameObject password;
    public GameObject password2;

    private string DomainMapper(Match match)
    {
        var mapping = new IdnMapping();
        var unicode = match.Groups[2].Value;
        try
        {
            unicode = mapping.GetAscii(unicode);
        }
        catch (ArgumentException)
        {
            invalid = true;
        }
        return match.Groups[1].Value + unicode;
    }

    public bool IsValidEmail(string strIn)
    {
        invalid = false;
        if (string.IsNullOrEmpty(strIn))
        {
            return false;
        }
        strIn = Regex.Replace(strIn, "(@)(.+)S", new MatchEvaluator(DomainMapper));
        if (invalid)
        {
            return false;
        }
        return Regex.IsMatch(strIn, "^(?(\")(\"[^\"]+?\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\S%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9]{2,17}))S", RegexOptions.IgnoreCase);
    }

    private void OnClick()
    {
        if (nameGO.GetComponent<UIInput>().text.Length < 3)
        {
            output.GetComponent<UILabel>().text = "User name too short.";
        }
        else if (password.GetComponent<UIInput>().text.Length < 3)
        {
            output.GetComponent<UILabel>().text = "Password too short.";
        }
        else if (password.GetComponent<UIInput>().text != password2.GetComponent<UIInput>().text)
        {
            output.GetComponent<UILabel>().text = "Password does not match the confirm password.";
        }
        else if (!IsValidEmail(email.GetComponent<UIInput>().text))
        {
            output.GetComponent<UILabel>().text = "This e-mail address is not valid.";
        }
        else
        {
            logincomponent.GetComponent<LoginFengKAI>().signup(nameGO.GetComponent<UIInput>().text, password.GetComponent<UIInput>().text, password2.GetComponent<UIInput>().text, email.GetComponent<UIInput>().text);
            output.GetComponent<UILabel>().text = "please wait...";
        }
    }
}

