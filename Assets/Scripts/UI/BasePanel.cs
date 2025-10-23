using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BasePanel : MonoBehaviour
{
    //protected CanvasGroup canvasGroup;

    //[SerializeField, Header("淡入淡出时间")]
    //protected float alphaSpeed = 10f;

    //protected bool isShow = false;

    //protected UnityAction hideCallBack;

    //protected virtual void Awake()
    //{
    //    canvasGroup = GetComponent<CanvasGroup>();
    //    canvasGroup.alpha = 0f;
    //}
    //protected virtual void Start()
    //{
    //    Init();
    //}

    ///// <summary>
    ///// 主要用于 初始化 按钮事件监听等内容的初始处理
    ///// </summary>
    //public abstract void Init();

    ///// <summary>
    ///// 显示自己时的处理逻辑
    ///// </summary>
    //public virtual void ShowPanel()
    //{
    //    isShow = true;
    //    canvasGroup.alpha = 0f;

    //}

    ///// <summary>
    ///// 隐藏自己时的处理逻辑
    ///// </summary>
    //public virtual void HidePanel(UnityAction callBack)
    //{
    //    hideCallBack = callBack;
    //    isShow = false;
    //    canvasGroup.alpha = 1f;
    //}

    //protected virtual void Update()
    //{
    //    if(isShow && canvasGroup.alpha != 1)
    //    {
    //        canvasGroup.alpha += alphaSpeed * Time.deltaTime;
    //        if(canvasGroup.alpha >= 1)
    //            canvasGroup.alpha = 1;
    //    }
    //    else if(!isShow && canvasGroup.alpha != 0)
    //    {
    //        canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
    //        if (canvasGroup.alpha <= 0)
    //        {
    //            canvasGroup.alpha = 0;
    //            //淡出成功时UI管理器动态删除面板
    //            hideCallBack?.Invoke();
    //        }
    //    }
    //}

    protected CanvasGroup _canvasGroup;
    [SerializeField]protected float fadeSpeed = 10.0f;
    protected bool isShow = false;
    protected UnityAction callBack;
    protected virtual void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }
    protected virtual void Update()
    {
        if (isShow)
        {
            _canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            if (_canvasGroup.alpha >= 1)
            {
                _canvasGroup.alpha = 1;
            }
        }
        else if(!isShow)
        {
            _canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0;
                callBack?.Invoke();
            }   
        }
    }

    public abstract void Init();
    public virtual void Show()
    {
        isShow = true;
        _canvasGroup.alpha = 0f;
    }

    public virtual void Hide(UnityAction action)
    {
        isShow = false;
        _canvasGroup.alpha = 1f;
        callBack = action;
    }
}
