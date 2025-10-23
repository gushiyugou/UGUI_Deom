using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    public bool isShow = true;
    public string info;
    TipPanel tipPanel;
    LoginPanel loginPanel;
    // Start is called before the first frame update
    void Start()
    {
        //tipPanel =  UIManager.Instance.ShowPanel<TipPanel>();
        UIManager.Instance.ShowPanel<LoginBackgroundPanel>();
        loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
        //UIManager.Instance.ShowPanel<RegisterPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (tipPanel != null)
        //{
        //    tipPanel.gameObject.SetActive(isShow);
        //    if (info.Length != 0)
        //    {
        //        tipPanel.ChangeInfo(info);
        //    }
        //}
    }
}
