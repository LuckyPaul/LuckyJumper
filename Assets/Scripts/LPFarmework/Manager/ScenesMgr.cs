﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.Tools;

public class ScenesMgr : GlobeSingleton<ScenesMgr> {

    public AsyncOperation asyncOperation;
    public delegate void OnLoadSceneAsyncOverCallBack(AsyncOperation asyncOperation);   //场景异步加载完成回调

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void Load(string sceneName)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.Log("Error:场景-" + sceneName + "不存在");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 加载场景_累加（之前关卡不销毁）
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadAdditive(string sceneName)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.Log("Error:场景-" + sceneName + "不存在");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

    }








    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="asyncOperation"></param>
    /// <param name="onLoadSceneAsyncOver"></param>
    /// <returns></returns>
    /// 
    public void LoadAsync(string sceneName, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
    {
        StartCoroutine(IELoadAsync(sceneName, onLoadSceneAsyncOverCallBack));
    }


    IEnumerator IELoadAsync(string sceneName, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
    {
        //if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
        //{
        //    Debug.Log("Error:场景-" + sceneName + "不存在");
        //    yield break;
        //}
        yield return new WaitForEndOfFrame();
        asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
        onLoadSceneAsyncOverCallBack(asyncOperation);
    }



    /// <summary>
    /// 异步加载场景_叠加（之前关卡不销毁）
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="asyncOperation"></param>
    /// <param name="onLoadSceneAsyncOverCallBack"></param>
    /// <returns></returns>
    /// 
    public void LoadAdditiveAsync(string sceneName, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
    {
        StartCoroutine(IELoadAdditiveAsync(sceneName, onLoadSceneAsyncOverCallBack));
    }



    IEnumerator IELoadAdditiveAsync(string sceneName, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.Log("Error:场景-" + sceneName + "不存在");
            yield break;
        }
        yield return new WaitForEndOfFrame();
        asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
        onLoadSceneAsyncOverCallBack(asyncOperation);

    }
}
