/***
 * 
 *    Title: LPFamework
 *           主题：  Unity单例--全局单例     
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/11/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using UnityEngine;

namespace LuckyPual.Tools {
	public abstract class GlobeSingleton<T> : MonoBehaviour where T:Component
	{
        static private Object _mutex = new Object();
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                    if (instance == null)
                    {
                        lock (_mutex)
                        {
                            if (instance == null)
                            {
                                instance = GameObject.FindObjectOfType<T>();
                                if (instance == null)
                                {
                                    GameObject obj = new GameObject(typeof(T).Name);
                                    instance = obj.AddComponent<T>();
                                }

                            }
                        }
                    }

                }
                return instance;
            }
        }

        /// <summary>
        /// 获取挂载unity单例脚本的节点
        /// </summary>
        private static GameObject GetOrCreateUnitySingletonTag()
        {
            GameObject _Manager = GameObject.FindGameObjectWithTag("UnitySingleton");
            if (null == _Manager)
            {
                _Manager = new GameObject("UnitySingletonObj");
                _Manager.tag = "UnitySingleton";
            }

            return _Manager;
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(GetOrCreateUnitySingletonTag());
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                DestroyImmediate((this as T));
            }
        }

    }
}

