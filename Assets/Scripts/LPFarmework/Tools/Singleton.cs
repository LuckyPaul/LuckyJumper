/***
 * 
 *    Title: LPFamework
 *           主题：   C#单例    
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/11/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using System;

namespace LuckyPual.Tools {
	public class Singleton<T>  where T:new() 
	{
        private static  object _Mutex = new object();
        private static T instance = default(T);
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_Mutex)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }

                }

                return instance;
            }

        }

        protected Singleton()
        {
            Init();
            if (instance != null)
            {
               // throw new SingletonException(GetType() + "This Singleton is already exist ! Please not new again !!!");
            }
        }

        protected virtual void Init() { }
    }

}


