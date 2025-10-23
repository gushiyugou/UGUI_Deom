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
                tipPanel.ChangeInfo("������ע���˺ţ�");
                return;
            }
            else if(passwordInput.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("������ע�����룡");
                return;
            }
            else if (accountInput.text.Length <= 6 || passwordInput.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("�˺ź�����������6Ϊ����");
                return;
            }

            //ִ�����ݱ�����߼����ж�ע����˺ź������Ƿ�Ϸ���
            //����������壬���������
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
                tipPanel.ChangeInfo("���˺��ѱ�ע�ᣬ������˺ţ�");
                accountInput.text = "";
                passwordInput.text = "";
            }

        });
    }

    public override void Show()
    {
        base.Show();
        //����UI��ʾ����
    }

    
}
