using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABTest : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {

        //img.sprite = ABManager.GetInstance().LoadBagResources<Sprite>("button", "10");
        //GameObject obj = ABManager.GetInstance().LoadABResources("playermodle", "player",typeof(GameObject)) as GameObject;
        //Instantiate(obj);
        //�첽����
        //ABManager.GetInstance().LoadResAsync<GameObject>("playermodle", "player", (obj) =>
        //{
        //    //obj����LoadResAsync�����з��ع�������Դresources.asset
        //    /*  if (resources.asset is GameObject)
        //            allBack(Instantiate(resources.asset));
        //        else
        //            callBack(resources.asset);
        //     */
        //    Instantiate(obj);
        //});
        //ABManager.GetInstance().LoadResAsync<Sprite>("button", "10", (obj) =>
        //{
        //    img.sprite = obj;
        //});

        //ABManager.GetInstance().LoadResAsync<GameObject>("playermodle", "player", (obj) =>
        //{

        //    Instantiate(obj).transform.localScale = Vector3.one * 2;
        //});



        /*AB����������һ�����е���Դ�õ�����һ�����е���Դ����A���е�ģ���õ���B���еĲ����򣬴�ʱ
         * A����B��֮����γ���һ��������A��������B������ֻ�������Լ��İ�����û�м�������������ʱ�ͻ����
         * ��Դȱʧ�������������Ҫ����Դ����֮ǰ�ȼ������������Ų��������Դȱʧ�������
         * 
         * ���ǵ�������̫������ʱ������ָ������������Ƚ��鷳��������Ҫ�������� ��ȡ������Ϣ 
         */
        /*ͨ����������ȡ������Ϣ�Ĳ���
         * 1.��������
         * 2.���������еĹ̶��ļ�
         * 3.�ӹ̶��ļ��еõ�������Ϣ
         */
        //AssetBundle ABmain =  AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/PC");
        //AssetBundleManifest abMFest = ABmain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //string[] strs = abMFest.GetAllDependencies("playermodle");


        //1.����AB��
        //LoadFromFile:�ӱ����ļ��м���
        //LoadFromFileDynic,����ֵ������AssetBundleCreateRequest,����Ĳ�����URL·��
        //AssetBundle bundle =  AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/playermodle");
        //AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/playermodle");
        //2.����AB���е���Դ,һ���������أ��Ƽ�ʹ�õڶ��֣���ΪLua��ʹ�����ַ�ʽ
        //GameObject obj =  bundle.LoadAsset<GameObject>("player");
        //GameObject obj = bundle.LoadAsset("player",typeof(GameObject)) as GameObject;
        //GameObject obj = bundle.LoadAsset("player") as GameObject;

        /*ע�⣺AB�����ܲ����ظ����أ�һ��AB��ֻ�ܼ���һ�Σ�����һ�ξͻᱨ��
         * ������ϢΪ
         * The AssetBundle 'playermodle' can't be loaded because another AssetBundle with the same files is already loaded.
        */
        //Instantiate(obj);
        /*ж��AB��*/
        //ж�ص�����AB��
        //�����ĺ�����һ�µ�
        //bundle.Unload(true);


        /*�첽���� ����> Э��*/
        //StartCoroutine(LoadABRes("button", "10"));

    }

    //IEnumerator LoadABRes(string AbName,string name)
    //{
    //    //1.����AB��
    //    AssetBundleCreateRequest ab2 =  AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + $"/{AbName}");
    //    //2.����AB���е���Դ
    //    yield return ab2;
    //    AssetBundleRequest image =  ab2.assetBundle.LoadAssetAsync(name,typeof(Sprite));
    //    yield return image;
    //    //�첽����ʱ���õ���AB���еľ�����Դ��Ҫ.��asset֮�����ֻ�ã�������Ͳ��ԣ�����Ҫas�ɶ�Ӧ����
    //    img.sprite = image.asset as Sprite;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //ж�������Ѿ����ع���AB��������Ϊtrue�����ж���Ѿ����ù���AB����Դ��false�򲻻�ж�������ù�����Դ
    //        //AssetBundle.UnloadAllAssetBundles(true);
            

    //    }
    //}
}
