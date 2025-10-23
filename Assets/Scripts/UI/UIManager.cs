using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //private static UIManager instance = new UIManager();
    //public static UIManager Instance => instance;

    //private Dictionary<string,BasePanel> panelDictionary = new Dictionary<string,BasePanel>();
    //private Transform UIRoot;
    //private UIManager()
    //{
    //    UIRoot = GameObject.Find("UIRoot").transform;
    //    GameObject.DontDestroyOnLoad(UIRoot.parent.gameObject);
    //}

    ////显示面板
    //public T ShowPanel<T>() where T : BasePanel
    //{
    //    //显示面板就是动态的创建面板
    //    string panelName = typeof(T).Name;

    //    if(panelDictionary.ContainsKey(panelName))
    //        return panelDictionary[panelName] as T;

    //    GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
    //    panelObj.transform.SetParent(UIRoot,false);

    //    T panel = panelObj.GetComponent<T>();
    //    panelDictionary.Add(panelName, panel);
    //    panel.Show();

    //    return panel;
    //}

    ////隐藏面板

    //public void HidePanel<T>(bool isFade = true) where T:BasePanel 
    //{
    //    string panelName = typeof(T).Name;
    //    if (panelDictionary.ContainsKey(panelName))
    //    {
    //        if (isFade)
    //        {
    //            panelDictionary[panelName].Hide(() =>
    //            {
    //                GameObject.Destroy(panelDictionary[panelName].gameObject);
    //                panelDictionary.Remove(panelName);
    //            });
    //        }
    //        else
    //        {
    //            GameObject.Destroy(panelDictionary[panelName].gameObject);
    //            panelDictionary.Remove(panelName);
    //        }
    //    }
    //}

    ////获取面板
    //public T GetPanel<T>() where T : BasePanel
    //{
    //    string panelName = typeof (T).Name;
    //    if (panelDictionary.ContainsKey(panelName))
    //    {
    //        return panelDictionary[panelName] as T;
    //    }
    //    return null;
    //}
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;
    private Dictionary<Type,BasePanel> panelDic = new Dictionary<Type,BasePanel>();
    private Transform UICanvas;

    private UIManager()
    {
        UICanvas = GameObject.Find("UICanvas").transform;
        GameObject.DontDestroyOnLoad(UICanvas.parent);
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        Debug.Log(typeof(T));
        if (panelDic.ContainsKey(typeof(T)))
            return panelDic[typeof(T)] as T;
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + typeof(T).Name));
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(typeof(T), panel);
        panelObj.transform.SetParent(UICanvas, false);
        panel.Show();
        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        if (panelDic.ContainsKey(typeof(T)))
        {
            if (isFade)
            {
                panelDic[typeof(T)].Hide(() =>
                {
                    GameObject.Destroy(panelDic[typeof(T)].gameObject);
                    panelDic.Remove(typeof(T));
                });
            }
            else
            {
                GameObject.Destroy(panelDic[typeof(T)].gameObject);
                panelDic.Remove(typeof(T));
            }
        }
    }
    
    public T GetPanel<T>() where T : BasePanel
    {
        if (panelDic.ContainsKey(typeof(T)))
        {
            return panelDic[typeof(T)] as T;
        }
        return null;
    }
}
