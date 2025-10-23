using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    [SerializeField] private TMP_InputField accountInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button cancelBtn;
    [SerializeField] private Button registerBtn;

    public override void Init()
    {
        accountInput.onValueChanged.AddListener((account) =>
        {

        });
        passwordInput.onValueChanged.AddListener((password) =>
        {

        });
        cancelBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<RegisterPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();
        });

        registerBtn.onClick.AddListener(() =>
        {
            
            if(accountInput.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("请输入注册账号！");
                return;
            }
            else if(passwordInput.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("请输入注册密码！");
                return;
            }
            else if (accountInput.text.Length <= 6 || passwordInput.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号和密码必须大于6为数！");
                return;
            }

            //执行数据保存的逻辑（判断注册的账号和密码是否合法）
            //隐藏自身面板，打开其他面板
            if (LoginManager.Instacne.AccountRegister(accountInput.text, passwordInput.text))
            {
                LoginManager.Instacne.ClearLoginData();
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                loginPanel.UpdataPanelData(accountInput.text, passwordInput.text);
                UIManager.Instance.HidePanel<RegisterPanel>();
                
            }
            else
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("该账号已被注册，请更换账号！");
                accountInput.text = "";
                passwordInput.text = "";
            }

        });
    }

    public override void Show()
    {
        base.Show();
        //更新UI显示内容
    }

    
}
