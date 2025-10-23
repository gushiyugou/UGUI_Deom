using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    [SerializeField] private Button itemBtn;
    [SerializeField] private TextMeshProUGUI itemLable;

    private int startIndex;
    private int endIndex;

    private void Start()
    {
        itemBtn.onClick.AddListener(() =>
        {
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdataPanel(this.startIndex, this.endIndex);
        });
    }

    public void InitInfo(int startIndex,int endIndex)
    {
        this.startIndex = startIndex;
        this.endIndex = endIndex;

        itemLable.text = startIndex + " - " + endIndex + "Çø";
    }
}
