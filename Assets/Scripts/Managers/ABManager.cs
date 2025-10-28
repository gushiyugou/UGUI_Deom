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
    /*AB������������Ҫ�����ǣ�
     * 1.���ⲿ������Ľ�����Դ����
    */

    //���ֵ�洢AB��������Ϊ���ļ���
    private Dictionary<string, AssetBundle> abDictionary = new Dictionary<string, AssetBundle>();
    private AssetBundle mainAB = null;
    //��������ȡ�õ������ļ�
    private AssetBundleManifest manifest = null;    

    private string BasePathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// ������
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
        //��������
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(BasePathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle tempAb = null;
        //�õ�ָ���İ���������������Ϣ
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

    #region ͬ������
    //1.ͬ������
    /// <summary>
    /// ������Դ����
    /// </summary>
    /// <typeparam name="T">������Դ������</typeparam>
    /// <param name="abName">AB��������</param>
    /// <param name="resName">���صľ�����Դ������</param>
    /// <returns></returns>
    public T LoadABResources<T>(string abName, string resName) where T : Object
    {
        //���ذ�
        LoadAB(abName);

        //��ȡָ���������ظð��еľ�����Դ
        T resources = abDictionary[abName].LoadAsset(resName,typeof(T)) as T;
        if(typeof(T) == typeof(GameObject))
        {
            return Instantiate(resources);
        }
        return resources;
    }
    /// <summary>
    /// ������Դ���أ�����Object���͵�ֵ����Ҫ��Object����asת��
    /// </summary>
    /// <param name="abName">AB������</param>
    /// <param name="resName">Ҫ���ص�AB���о�������</param>
    /// <returns></returns>
    public Object LoadABResources(string abName, string resName)
    {
        LoadAB(abName);


        //��ȡָ���������ظð��еľ�����Դ
        Object resources = abDictionary[abName].LoadAsset(resName) as Object;
        if (resources is GameObject)
            return Instantiate(resources);
        return resources;
    }
    /// <summary>
    /// ������Դ���أ�����Object���͵�ֵ����Ҫ��Object����asת������Ҫ�����ǹ�ܵ���Դͬ��ʱ�����ͻ�ȡ��ȷ����Դ
    /// </summary>
    /// <param name="abName">AB������</param>
    /// <param name="resName">Ҫ���ص�AB���о������Դ����</param>
    /// <param name="type">��Դ������</param>
    /// <returns></returns>
    public Object LoadABResources(string abName, string resName,System.Type type)
    {
        LoadAB(abName);

        //��ȡָ���������ظð��еľ�����Դ
        Object resources = abDictionary[abName].LoadAsset(resName,type);
        if (resources is GameObject)
            return Instantiate(resources);
        return resources;
    }
    #endregion

    #region �첽����

    //2.�첽����
    /// <summary>
    /// ���������첽����
    /// </summary>
    /// <param name="abName">������</param>
    /// <param name="resName">��Դ����</param>
    /// <param name="callBack">�ص�ί��</param>
    public void LoadResAsync(string abName,string resName,UnityAction<Object> callBack)
    {
        //����������Э��
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        LoadAB(abName);

        //��ȡָ���������ظð��еľ�����Դ
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync(resName);
            yield return resources;

        //��ִ�е�����ʱ���Ὣ���ݴ��ݸ��ⲿ�ĺ���
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset));
        else
            callBack(resources.asset);
    }

    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
    {
        //����������Э��
        StartCoroutine(ReallyLoadResAsync(abName, resName,type, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadAB(abName);

        //��ȡָ���������ظð��еľ�����Դ
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync(resName,type);
        yield return resources;

        //��ִ�е�����ʱ���Ὣ���ݴ��ݸ��ⲿ�ĺ���
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset));
        else
            callBack(resources.asset);
    }


    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        //����������Э��
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName,callBack));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        LoadAB(abName);

        //��ȡָ���������ظð��еľ�����Դ
        AssetBundleRequest resources = abDictionary[abName].LoadAssetAsync<T>(resName);
        yield return resources;

        //��ִ�е�����ʱ���Ὣ���ݴ��ݸ��ⲿ�ĺ���
        if (resources.asset is GameObject)
            callBack(Instantiate(resources.asset) as T);
        else
            callBack(resources.asset as T);
    }


    #endregion
    //3.������ж��
    public void UnLoadAssignAssetBundle(string abName)
    {
        if (abDictionary.ContainsKey(abName))
        {
            abDictionary[abName].Unload(false);
            abDictionary.Remove(abName);
        }
    }
    //4.���а���ж��
    public void UnLoadAllAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDictionary.Clear();
        mainAB = null;
        manifest = null;
    }
}
