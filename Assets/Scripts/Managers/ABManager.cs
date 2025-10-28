using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public enum ABType
{
    Synchronize,
    Asynchronous
}
public class ABManager : SingletonAutoMono<ABManager>
{
    /*AB包管理器的主要作用是：
     * 1.让外部更方便的进行资源加载
    */

    //用字典存储AB包，名字为包的键名
    private Dictionary<string, AssetBundle> abDictionary = new Dictionary<string, AssetBundle>();
    private AssetBundle mainAB = null;
    //依赖包获取用的配置文件
    private AssetBundleManifest manifest = null;    

    private string BasePathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// 主包名
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }


    public void LoadAB(string abName)
    {
        //加载主包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(BasePathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle tempAb = null;
        //得到指定的包的所有依赖包信息
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDictionary.ContainsKey(strs[i]))
            {
                tempAb = AssetBundle.LoadFromFile(BasePathUrl + strs[i]);
                abDictionary.Add(strs[i], tempAb);
            }
        }

        if (!abDictionary.ContainsKey(abName))
        {
            tempAb = AssetBundle.LoadFromFile(BasePathUrl + abName);
            abDictionary.Add(abName, tempAb);
        }
    }

    #region 同步加载
    //1.同步加载
    /// <summary>
    /// 泛型资源加载
    /// </summary>
    /// <typeparam name="T">加载资源的类型</typeparam>
    /// <param name="abName">AB包的名字</param>
    /// <param name="resName">加载的具体资源的名字</param>
    /// <returns></returns>
    public T LoadABResources<T>(string abName, string resName) where T : Object
    {
        //加载包
        LoadAB(abName);

        //获取指定包并加载该包中的具体资源
        T resources = abDictionary[abName].LoadAsset(resName,typeof(T)) as T;
        if(typeof(T) == typeof(GameObject))
        {
            return Instantiate(resources);
        }
        return resources;
    }
    /// <summary>
    /// 常规资源加载，返回Object类型的值，需要对Object进行as转换
    /// </summary>
    /// <param name="abName">AB包名字</param>
    /// <param name="resName">要加载的AB包中具体名字</param>
    /// <returns></returns>
    public Object LoadABResources(string abName, string resName)
    {
        LoadAB(abName);


        //获取指定包并加载该包中的具体资源
        Object resources = abDictionary[abName].LoadAsset(resName) as Object;
        if (resources is GameObject)
            return Instantiate(resources);
        return resources;
    }
    /// <summary>
    /// 反射资源加载，返回Object类型的值，需要对Object进行as转换，主要作用是规避当资源同名时按类型获取正确的资源
    /// </summary>
    /// <param name="abName">AB包名字</param>
    /// <param name="resName">要加载的AB包中具体的资源名字</param>
    /// <param name="type">资源的类型</param>
    /// <returns></returns>
    public Object LoadABResources(string abName, string resName,System.Type type)
    {
        LoadAB(abName);

        //获取指定包并加载该包中的具体资源
        Object resources = abDictionary[abName].LoadAsset(resName,type);
        if (resources is GameObject)
            return Instantiate(resources);
        return resources;
    }
    #endregion

    #region 异步加载

    //2.异步加载
    /// <summary>
    /// 根据名字异步加载
    /// </summary>
    /// <param name="abName">包名字</param>
    /// <param name="resName">资源名字</param>
    /// <param name="callBack">回调委托</param>
    public void LoadResAsync(string abName,string resName,UnityAction<Object> callBack)
    {
        //作用是启动协程
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        LoadAB(abName);

        //获取指定包并加载该包中的具体资源
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync(resName);
            yield return resources;

        //当执行到这里时，会将内容传递给外部的函数
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset));
        else
            callBack(resources.asset);
    }

    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
    {
        //作用是启动协程
        StartCoroutine(ReallyLoadResAsync(abName, resName,type, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadAB(abName);

        //获取指定包并加载该包中的具体资源
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync(resName,type);
        yield return resources;

        //当执行到这里时，会将内容传递给外部的函数
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset));
        else
            callBack(resources.asset);
    }


    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        //作用是启动协程
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName,callBack));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        LoadAB(abName);

        //获取指定包并加载该包中的具体资源
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync<T>(resName);
        yield return resources;

        //当执行到这里时，会将内容传递给外部的函数
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset) as T);
        else
            callBack(resources.asset as T);
    }


    #endregion
    //3.单个包卸载
    public void UnLoadAssignAssetBundle(string abName)
    {
        if (abDictionary.ContainsKey(abName))
        {
            abDictionary[abName].Unload(false);
            abDictionary.Remove(abName);
        }
    }
    //4.所有包的卸载
    public void UnLoadAllAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDictionary.Clear();
        mainAB = null;
        manifest = null;
    }
}
