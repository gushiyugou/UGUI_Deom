using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    private static LoginManager _instance = new LoginManager();
    public static LoginManager Instacne => _instance;

    private LoginData _loginData;
    public LoginData loginData => _loginData;

    private RegisterData _registerData;
    public RegisterData registerData => _registerData;

    //所有服务器数据
    private List<ServerInfo> _serverData;
    public List<ServerInfo> serverData => _serverData;


    private LoginManager()
    {
        _loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        _registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        _serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region 登录数据的存取
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(_loginData, "LoginData");
    }

    public void ClearLoginData()
    {
        _loginData.frontServerID = 0;
        _loginData.isAutoLogin = false;
        _loginData.isRemeberPassrword = false;
    }
    #endregion

    #region 注册数据的存取
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(_registerData, "RegisterData");
    }

    

    public bool AccountRegister(string account,string password)
    {
        if (registerData.registerInfo.ContainsKey(account))
        {
            return false;
        }
        
        registerData.registerInfo.Add(account, password);
        SaveRegisterData();
        return true;

    }

    public bool AccountLogin(string account,string password)
    {
        if(registerData.registerInfo.ContainsKey(account) && registerData.registerInfo[account] == password)
        {
            return true;
        }
        return false;
        //if (registerData.registerInfo.ContainsKey(account))
        //{
        //    if (registerData.registerInfo[account] == password)
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }
    #endregion
}
