using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button ChangeServerBtn;
    [SerializeField] private Button StartBtn;
    [SerializeField]private TextMeshProUGUI serverInfo;
    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            if(LoginManager.Instacne.loginData.isAutoLogin)
                LoginManager.Instacne.loginData.isAutoLogin = false;
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        ChangeServerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
        });
        StartBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ServerPanel>();
            //UIManager.Instance.ShowPanel<CharacterPanel>();
            LoginManager.Instacne.SaveLoginData();
            SceneManager.LoadScene("MainScene");
            UIManager.Instance.HidePanel<LoginBackgroundPanel>();
        });
    }

    public override void Show()
    {
        base.Show();
        //serverInfo.text = 
        int id = LoginManager.Instacne.loginData.frontServerID;
        if(id <= 0)
        {
            serverInfo.text = "未选择服务器";
            return;
        }
        else
        {
            ServerInfo info = LoginManager.Instacne.serverData[id - 1];
            serverInfo.text = info.id + "区   " + info.name;
        }
            
    }
}
