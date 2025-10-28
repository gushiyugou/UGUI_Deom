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
        //异步加载
        //ABManager.GetInstance().LoadResAsync<GameObject>("playermodle", "player", (obj) =>
        //{
        //    //obj就是LoadResAsync函数中返回过来的资源resources.asset
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



        /*AB包的依赖：一个包中的资源用到了另一个包中的资源，如A包中的模型用到了B包中的材质球，此时
         * A包和B包之间就形成了一种依赖，A包依赖于B包，当只加载了自己的包，而没有加载依赖包，此时就会出现
         * 资源缺失的情况。所以需要在资源引用之前先加载依赖包，才不会出现资源缺失的情况。
         * 
         * 但是当依赖包太过混乱时，加载指定的依赖包会比较麻烦，所以需要利用主包 获取依赖信息 
         */
        /*通过主包来获取依赖信息的步骤
         * 1.加载主包
         * 2.加载主包中的固定文件
         * 3.从固定文件中得到依赖信息
         */
        //AssetBundle ABmain =  AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/PC");
        //AssetBundleManifest abMFest = ABmain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //string[] strs = abMFest.GetAllDependencies("playermodle");


        //1.加载AB包
        //LoadFromFile:从本地文件夹加载
        //LoadFromFileDynic,返回值类型是AssetBundleCreateRequest,传入的参数是URL路径
        //AssetBundle bundle =  AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/playermodle");
        //AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/playermodle");
        //2.加载AB包中的资源,一共三个重载，推荐使用第二种，因为Lua中使用这种方式
        //GameObject obj =  bundle.LoadAsset<GameObject>("player");
        //GameObject obj = bundle.LoadAsset("player",typeof(GameObject)) as GameObject;
        //GameObject obj = bundle.LoadAsset("player") as GameObject;

        /*注意：AB包不能不能重复加载，一个AB包只能加载一次，超过一次就会报错
         * 报错信息为
         * The AssetBundle 'playermodle' can't be loaded because another AssetBundle with the same files is already loaded.
        */
        //Instantiate(obj);
        /*卸载AB包*/
        //卸载单独的AB包
        //参数的含义是一致的
        //bundle.Unload(true);


        /*异步加载 ——> 协程*/
        //StartCoroutine(LoadABRes("button", "10"));

    }

    //IEnumerator LoadABRes(string AbName,string name)
    //{
    //    //1.加载AB包
    //    AssetBundleCreateRequest ab2 =  AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + $"/{AbName}");
    //    //2.加载AB包中的资源
    //    yield return ab2;
    //    AssetBundleRequest image =  ab2.assetBundle.LoadAssetAsync(name,typeof(Sprite));
    //    yield return image;
    //    //异步加载时，得到的AB包中的具体资源需要.出asset之后才能只用，如果类型不对，还需要as成对应类型
    //    img.sprite = image.asset as Sprite;
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //卸载所有已经加载过的AB包，参数为true，则会卸载已经引用过的AB包资源，false则不会卸载已引用过的资源
    //        //AssetBundle.UnloadAllAssetBundles(true);
            

    //    }
    //}
}
