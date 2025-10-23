using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    [SerializeField]private Button confirmBtn;
    [SerializeField]private TextMeshProUGUI textInfo;
    public override void Init()
    {
        confirmBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeInfo(string info)
    {
        textInfo.text = info;
    }
}
