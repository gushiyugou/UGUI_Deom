using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel 
{
    [SerializeField] private TMP_InputField account;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Toggle remeberToggle;
    [SerializeField] private Toggle autoLoginToggle;
    [SerializeField] private Button registerBtn;
    [SerializeField] private Button LoginBtn;

    public override void Init()
    {
        remeberToggle.onValueChanged.AddListener((isOn) =>
        {
            if (!isOn)
            {
                autoLoginToggle.isOn = false;
            }
        });

        autoLoginToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                remeberToggle.isOn = true;
            }
        });

        registerBtn.onClick.AddListener(() =>
        {
            //隐藏当前登录面板
            UIManager.Instance.HidePanel<LoginPanel>();
            //显示注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
        });
        LoginBtn.onClick.AddListener(() =>
        {
            //登录逻辑判断
            if (account.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("请输入注册账号！");
                return;
            }
            else if (password.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("请输入注册密码！");
                return;
            }
            else if (account.text.Length <= 6 || password.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号和密码必须大于6为数！");
                return;
            }
            //登录成功，关闭当前面板，打开服务器面板
            if(LoginManager.Instacne.AccountLogin(account.text, password.text))
            {
                LoginManager.Instacne.loginData.userName = account.text;
                LoginManager.Instacne.loginData.passWord = password.text;
                LoginManager.Instacne.loginData.isRemeberPassrword = remeberToggle.isOn;
                LoginManager.Instacne.loginData.isAutoLogin = autoLoginToggle.isOn;
                LoginManager.Instacne.SaveLoginData();
                UIManager.Instance.HidePanel<LoginPanel>();

                //判断是否进入过服务器面板，没有则进入选服面板，有则进入服务器面板
                //UIManager.Instance.ShowPanel<ServerPanel>();
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误，请重新输入！");
                //登录失败，为提示面板添加提示信息，显示提示面板
            }

        });

        account.onValueChanged.AddListener((userNamme) =>
        {
            //更新登录数据中的账号信息
        });
        password.onValueChanged.AddListener((passWord) =>
        {
            //更新登录数据中的密码信息
        });
    }

    public override void Show()
    {
        base.Show();
        //更新数据
        LoginData loginData = LoginManager.Instacne.loginData;
        remeberToggle.isOn = loginData.isRemeberPassrword;
        autoLoginToggle.isOn = loginData.isAutoLogin;
        account.text = loginData.userName;
        password.text = remeberToggle.isOn ? loginData.passWord: "";

        if (autoLoginToggle.isOn)
        {
            //自动登录逻辑
        }
    }
    public void UpdataPanelData(string account,string password)
    {
        this.account.text = account;
        this.password.text = password;  
    }

    //public override void Init()
    //{
    //    //面板控件事件注册
    //    registerBtn.onClick.AddListener(() =>
    //    {
    //        //显示注册面板

    //        //隐藏当前登录面板
    //        UIManager.Instance.HidePanel<LoginPanel>();
    //    });

    //    LoginBtn.onClick.AddListener(() =>
    //    {
    //        //判断是否数据是否正确，正确则登录成功，成功则关闭当前界面，显示服务器面板

    //        //数据不正确则登录不成功，显示提示框，并更新提示框中的提示信息
    //    });

    //    remeberToggle.onValueChanged.AddListener((isOn) =>
    //    {
    //        if (!isOn)
    //        {
    //            autoLoginToggle.isOn = false;
    //        }
    //    });
    //    autoLoginToggle.onValueChanged.AddListener((isOn) =>
    //    {
    //        //更新自动登录选项
    //        if (isOn)
    //        {
    //            remeberToggle.isOn = true;
    //        }
    //    });

    //    account.onValueChanged.AddListener((value) =>
    //    {
    //        //更新账号数据
    //    });
    //    password.onValueChanged.AddListener((value) =>
    //    {
    //        //更新密码数据
    //    });
    //}

    //public override void Show()
    //{
    //    base.Show();
    //    //更新数据
    //    LoginData loginData = LoginManager.Instacne.loginData;
    //    remeberToggle.isOn = loginData.isRememberPassWord;
    //    autoLoginToggle.isOn = loginData.isAutoLogin;

    //    account.text = loginData.userName;
    //    password.text = remeberToggle.isOn ? loginData.passWord : "";

    //    if (autoLoginToggle.isOn)
    //    {
    //        //自动登录判断
    //    }
    //}

}
