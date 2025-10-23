using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    [SerializeField] private ScrollRect leftScrollView;
    [SerializeField] private ScrollRect RightScrollView;
    [SerializeField] private TextMeshProUGUI serverRangeLable;
    [SerializeField] private TextMeshProUGUI lastLable;
    [SerializeField] private Image lastServerState;

    private List<GameObject> leftItems = new List<GameObject>();
    public override void Init()
    {
        List<ServerInfo> infoList = LoginManager.Instacne.serverData;
        int num = infoList.Count / 5+1;
        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("Item/ServerLeftItem"));
            item.transform.SetParent(leftScrollView.content,false);

            ServerLeftItem itemInfo = item.GetComponent<ServerLeftItem>();
            int startIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            if(endIndex > infoList.Count)
                endIndex = infoList.Count;
            itemInfo.InitInfo(startIndex, endIndex);
        }
    }

    public override void Show()
    {
        base.Show();
        int id = LoginManager.Instacne.loginData.frontServerID;
        if (id <= 0)
        {
            lastLable.text = "暂无信息";
            lastLable.alignment = TextAlignmentOptions.Center;
            lastLable.rectTransform.localPosition = new Vector3(0,6,0);
            lastServerState.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginManager.Instacne.serverData[id - 1];
            lastLable.text = info.id + "区   " + info.name;
            lastLable.alignment = TextAlignmentOptions.Left;
            lastServerState.gameObject.SetActive(true);
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Atlas/Login");
            switch (info.state)
            {
                case 0:
                    lastServerState.gameObject.SetActive(false);
                    break;
                case 1:
                    lastServerState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2:
                    lastServerState.sprite = sa.GetSprite("ui_DL_huobao_01");
                    break;
                case 3:
                    lastServerState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                    break;
                case 4:
                    lastServerState.sprite = sa.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        UpdataPanel(1, 5 < LoginManager.Instacne.serverData.Count ? 5 : LoginManager.Instacne.serverData.Count);
    }

    public void UpdataPanel(int startIndex,int endIndex)
    {
        serverRangeLable.text = "服务器 " + startIndex + " - " + endIndex;
        for (int i = 0; i < leftItems.Count; i++)
        {
            Destroy(leftItems[i]);
        }
        leftItems.Clear();
        for(int i = startIndex; i <= endIndex; i++)
        {
            ServerInfo info = LoginManager.Instacne.serverData[i-1];
            GameObject item = Instantiate(Resources.Load<GameObject>("Item/ServerRightItem"));
            item.transform.SetParent(RightScrollView.content, false);
            ServerRightItem itemInfo = item.GetComponent<ServerRightItem>();
            itemInfo.InitInfo(info);
            leftItems.Add(item);
        }
    }
}
