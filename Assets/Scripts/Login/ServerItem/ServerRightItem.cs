using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    [SerializeField] private Button itemBtn;
    [SerializeField] private TextMeshProUGUI BtnLable;
    [SerializeField] private Image state;
    [SerializeField] private Image newImage;

    private ServerInfo newServerInfo;
    public ServerInfo NewServerInfo => newServerInfo;

    private void Start()
    {
        itemBtn.onClick.AddListener(() =>
        {
            //记录当前选择的服务器
            LoginManager.Instacne.loginData.frontServerID = newServerInfo.id;
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }

    public void InitInfo(ServerInfo info)
    {
        newServerInfo = info;
        BtnLable.text = info.id+"区  "+ info.name;
        newImage.gameObject.SetActive(info.isNew);
        state.gameObject.SetActive(true);
        //加载图集
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Atlas/Login");

        switch (info.state)
        {
            case 0:
                state.gameObject.SetActive(false);
                break;
            case 1:
                state.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2:
                state.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 3:
                state.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 4:
                state.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
