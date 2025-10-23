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
            //���ص�ǰ��¼���
            UIManager.Instance.HidePanel<LoginPanel>();
            //��ʾע�����
            UIManager.Instance.ShowPanel<RegisterPanel>();
        });
        LoginBtn.onClick.AddListener(() =>
        {
            //��¼�߼��ж�
            if (account.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("������ע���˺ţ�");
                return;
            }
            else if (password.text.Length == 0)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("������ע�����룡");
                return;
            }
            else if (account.text.Length <= 6 || password.text.Length <= 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("�˺ź�����������6Ϊ����");
                return;
            }
            //��¼�ɹ����رյ�ǰ��壬�򿪷��������
            if(LoginManager.Instacne.AccountLogin(account.text, password.text))
            {
                LoginManager.Instacne.loginData.userName = account.text;
                LoginManager.Instacne.loginData.passWord = password.text;
                LoginManager.Instacne.loginData.isRemeberPassrword = remeberToggle.isOn;
                LoginManager.Instacne.loginData.isAutoLogin = autoLoginToggle.isOn;
                LoginManager.Instacne.SaveLoginData();
                UIManager.Instance.HidePanel<LoginPanel>();

                //�ж��Ƿ�������������壬û�������ѡ����壬���������������
                //UIManager.Instance.ShowPanel<ServerPanel>();
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�˺Ż�����������������룡");
                //��¼ʧ�ܣ�Ϊ��ʾ��������ʾ��Ϣ����ʾ��ʾ���
            }

        });

        account.onValueChanged.AddListener((userNamme) =>
        {
            //���µ�¼�����е��˺���Ϣ
        });
        password.onValueChanged.AddListener((passWord) =>
        {
            //���µ�¼�����е�������Ϣ
        });
    }

    public override void Show()
    {
        base.Show();
        //��������
        LoginData loginData = LoginManager.Instacne.loginData;
        remeberToggle.isOn = loginData.isRemeberPassrword;
        autoLoginToggle.isOn = loginData.isAutoLogin;
        account.text = loginData.userName;
        password.text = remeberToggle.isOn ? loginData.passWord: "";

        if (autoLoginToggle.isOn)
        {
            //�Զ���¼�߼�
        }
    }
    public void UpdataPanelData(string account,string password)
    {
        this.account.text = account;
        this.password.text = password;  
    }

    //public override void Init()
    //{
    //    //���ؼ��¼�ע��
    //    registerBtn.onClick.AddListener(() =>
    //    {
    //        //��ʾע�����

    //        //���ص�ǰ��¼���
    //        UIManager.Instance.HidePanel<LoginPanel>();
    //    });

    //    LoginBtn.onClick.AddListener(() =>
    //    {
    //        //�ж��Ƿ������Ƿ���ȷ����ȷ���¼�ɹ����ɹ���رյ�ǰ���棬��ʾ���������

    //        //���ݲ���ȷ���¼���ɹ�����ʾ��ʾ�򣬲�������ʾ���е���ʾ��Ϣ
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
    //        //�����Զ���¼ѡ��
    //        if (isOn)
    //        {
    //            remeberToggle.isOn = true;
    //        }
    //    });

    //    account.onValueChanged.AddListener((value) =>
    //    {
    //        //�����˺�����
    //    });
    //    password.onValueChanged.AddListener((value) =>
    //    {
    //        //������������
    //    });
    //}

    //public override void Show()
    //{
    //    base.Show();
    //    //��������
    //    LoginData loginData = LoginManager.Instacne.loginData;
    //    remeberToggle.isOn = loginData.isRememberPassWord;
    //    autoLoginToggle.isOn = loginData.isAutoLogin;

    //    account.text = loginData.userName;
    //    password.text = remeberToggle.isOn ? loginData.passWord : "";

    //    if (autoLoginToggle.isOn)
    //    {
    //        //�Զ���¼�ж�
    //    }
    //}

}
