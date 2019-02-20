//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LoginFengKAI : MonoBehaviour
{
    private string ChangeGuildURL = "http://aotskins.com/version/guild.php";
    private string ChangePasswordURL = "http://fenglee.com/game/aog/change_password.php";
    private string CheckUserURL = "http://aotskins.com/version/login.php";
    private string ForgetPasswordURL = "http://fenglee.com/game/aog/forget_password.php";
    public string formText = string.Empty;
    private string GetInfoURL = "http://aotskins.com/version/getinfo.php";
    public PanelLoginGroupManager loginGroup;
    public GameObject output;
    public GameObject output2;
    public GameObject panelChangeGUILDNAME;
    public GameObject panelChangePassword;
    public GameObject panelForget;
    public GameObject panelLogin;
    public GameObject panelRegister;
    public GameObject panelStatus;
    public static PlayerInfoPHOTON player;
    private static string playerGUILDName = string.Empty;
    private static string playerName = string.Empty;
    private static string playerPassword = string.Empty;
    private string RegisterURL = "http://fenglee.com/game/aog/signup_check.php";

    public void cGuild(string name)
    {
        if (playerName == string.Empty)
        {
            logout();
            NGUITools.SetActive(panelChangeGUILDNAME, false);
            NGUITools.SetActive(panelLogin, true);
            output.GetComponent<UILabel>().text = "Please sign in.";
        }
        else
        {
            StartCoroutine(changeGuild(name));
        }
    }

    [DebuggerHidden]
    private IEnumerator changeGuild(string name)
    {
        return new changeGuildcIterator5 { name = name, Sname = name, fthis = this };
    }

    [DebuggerHidden]
    private IEnumerator changePassword(string oldpassword, string password, string password2)
    {
        return new changePasswordcIterator4 { oldpassword = oldpassword, password = password, password2 = password2, Soldpassword = oldpassword, Spassword = password, Spassword2 = password2, fthis = this };
    }

    private void clearCOOKIE()
    {
        playerName = string.Empty;
        playerPassword = string.Empty;
    }

    public void cpassword(string oldpassword, string password, string password2)
    {
        if (playerName == string.Empty)
        {
            logout();
            NGUITools.SetActive(panelChangePassword, false);
            NGUITools.SetActive(panelLogin, true);
            output.GetComponent<UILabel>().text = "Please sign in.";
        }
        else
        {
            StartCoroutine(changePassword(oldpassword, password, password2));
        }
    }

    [DebuggerHidden]
    private IEnumerator ForgetPassword(string email)
    {
        return new ForgetPasswordcIterator6 { email = email, Semail = email, fthis = this };
    }

    [DebuggerHidden]
    private IEnumerator getInfo()
    {
        return new getInfocIterator2 { fthis = this };
    }

    public void login(string name, string password)
    {
        StartCoroutine(Login(name, password));
    }

    [DebuggerHidden]
    private IEnumerator Login(string name, string password)
    {
        return new LogincIterator1 { name = name, password = password, Sname = name, Spassword = password, fthis = this };
    }

    public void logout()
    {
        clearCOOKIE();
        player = new PlayerInfoPHOTON();
        player.initAsGuest();
        output.GetComponent<UILabel>().text = "Welcome," + player.name;
    }

    [DebuggerHidden]
    private IEnumerator Register(string name, string password, string password2, string email)
    {
        return new RegistercIterator3 { name = name, password = password, password2 = password2, email = email, Sname = name, Spassword = password, Spassword2 = password2, Semail = email, fthis = this };
    }

    public void resetPassword(string email)
    {
        StartCoroutine(ForgetPassword(email));
    }

    public void signup(string name, string password, string password2, string email)
    {
        StartCoroutine(Register(name, password, password2, email));
    }

    private void Start()
    {
        if (player == null)
        {
            player = new PlayerInfoPHOTON();
            player.initAsGuest();
        }
        if (playerName != string.Empty)
        {
            NGUITools.SetActive(panelLogin, false);
            NGUITools.SetActive(panelStatus, true);
            StartCoroutine(getInfo());
        }
        else
        {
            //this.output.GetComponent<UILabel>().text = "Welcome," + player.name;
        }
    }

    [CompilerGenerated]
    private sealed class changeGuildcIterator5 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal string Sname;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal WWW w1;
        internal string name;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("name", playerName);
                    form0.AddField("guildname", name);
                    w1 = new WWW(fthis.ChangeGuildURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    if (w1.error == null)
                    {
                        fthis.output.GetComponent<UILabel>().text = w1.text;
                        if (w1.text.Contains("Guild name set."))
                        {
                            NGUITools.SetActive(fthis.panelChangeGUILDNAME, false);
                            NGUITools.SetActive(fthis.panelStatus, true);
                            fthis.StartCoroutine(fthis.getInfo());
                        }
                        w1.Dispose();
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_0135;
            }
            SPC = -1;
        Label_0135:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class changePasswordcIterator4 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal string Soldpassword;
        internal string Spassword;
        internal string Spassword2;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal WWW w1;
        internal string oldpassword;
        internal string password;
        internal string password2;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("userid", playerName);
                    form0.AddField("old_password", oldpassword);
                    form0.AddField("password", password);
                    form0.AddField("password2", password2);
                    w1 = new WWW(fthis.ChangePasswordURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    if (w1.error == null)
                    {
                        fthis.output.GetComponent<UILabel>().text = w1.text;
                        if (w1.text.Contains("Thanks, Your password changed successfully"))
                        {
                            NGUITools.SetActive(fthis.panelChangePassword, false);
                            NGUITools.SetActive(fthis.panelLogin, true);
                        }
                        w1.Dispose();
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_014A;
            }
            SPC = -1;
        Label_014A:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class ForgetPasswordcIterator6 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal string Semail;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal WWW w1;
        internal string email;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("email", email);
                    w1 = new WWW(fthis.ForgetPasswordURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    if (w1.error == null)
                    {
                        fthis.output.GetComponent<UILabel>().text = w1.text;
                        w1.Dispose();
                        NGUITools.SetActive(fthis.panelForget, false);
                        NGUITools.SetActive(fthis.panelLogin, true);
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_00FA;
            }
            fthis.clearCOOKIE();
            SPC = -1;
        Label_00FA:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class getInfocIterator2 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal string[] result2;
        internal WWW w1;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("userid", playerName);
                    form0.AddField("password", playerPassword);
                    w1 = new WWW(fthis.GetInfoURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    if (w1.error == null)
                    {
                        if (w1.text.Contains("Error,please sign in again."))
                        {
                            NGUITools.SetActive(fthis.panelLogin, true);
                            NGUITools.SetActive(fthis.panelStatus, false);
                            fthis.output.GetComponent<UILabel>().text = w1.text;
                            playerName = string.Empty;
                            playerPassword = string.Empty;
                        }
                        else
                        {
                            var separator = new char[] { '|' };
                            result2 = w1.text.Split(separator);
                            playerGUILDName = result2[0];
                            fthis.output2.GetComponent<UILabel>().text = result2[1];
                            player.name = playerName;
                            player.guildname = playerGUILDName;
                        }
                        w1.Dispose();
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_01A7;
            }
            SPC = -1;
        Label_01A7:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class LogincIterator1 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal string Sname;
        internal string Spassword;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal WWW w1;
        internal string name;
        internal string password;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("userid", name);
                    form0.AddField("password", password);
                    form0.AddField("version", UIMainReferences.version);
                    w1 = new WWW(fthis.CheckUserURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    fthis.clearCOOKIE();
                    if (w1.error == null)
                    {
                        fthis.output.GetComponent<UILabel>().text = w1.text;
                        fthis.formText = w1.text;
                        w1.Dispose();
                        if (fthis.formText.Contains("Welcome back") && fthis.formText.Contains("(^o^)/~"))
                        {
                            NGUITools.SetActive(fthis.panelLogin, false);
                            NGUITools.SetActive(fthis.panelStatus, true);
                            playerName = name;
                            playerPassword = password;
                            fthis.StartCoroutine(fthis.getInfo());
                        }
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_019C;
            }
            SPC = -1;
        Label_019C:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }

    [CompilerGenerated]
    private sealed class RegistercIterator3 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal string Semail;
        internal string Sname;
        internal string Spassword;
        internal string Spassword2;
        internal LoginFengKAI fthis;
        internal WWWForm form0;
        internal WWW w1;
        internal string email;
        internal string name;
        internal string password;
        internal string password2;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    form0 = new WWWForm();
                    form0.AddField("userid", name);
                    form0.AddField("password", password);
                    form0.AddField("password2", password2);
                    form0.AddField("email", email);
                    w1 = new WWW(fthis.RegisterURL, form0);
                    Scurrent = w1;
                    SPC = 1;
                    return true;

                case 1:
                    if (w1.error == null)
                    {
                        fthis.output.GetComponent<UILabel>().text = w1.text;
                        if (w1.text.Contains("Final step,to activate your account, please click the link in the activation email"))
                        {
                            NGUITools.SetActive(fthis.panelRegister, false);
                            NGUITools.SetActive(fthis.panelLogin, true);
                        }
                        w1.Dispose();
                        break;
                    }
                    print(w1.error);
                    break;

                default:
                    goto Label_0156;
            }
            fthis.clearCOOKIE();
            SPC = -1;
        Label_0156:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }
}

