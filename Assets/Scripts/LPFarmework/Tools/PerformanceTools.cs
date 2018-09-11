/***
 * 
 *    Title: LPFamework
 *           主题： 性能监测器    
 *    Description: 
 *           功能： 监测相关性能FPS DrawCall Verts Meshs
 *                  
 *    Date: 9/11/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using UnityEngine;

namespace LuckyPual.Tools {
	public class PerformanceTools : UnitySingleton<PerformanceTools>
	{
        private bool IsShowFPS = false;
        private bool IsShowDrawCall = false;
        private bool IsShowVerts = false;
        private bool IsShowMeshs = false;


        public float f_UpdateInterval = 0.5F;  //刷新间隔
        private float f_LastInterval;      //上一次刷新的时间间隔
        public static int verts;
        public static int tris;

        /// <summary>
        /// FPS
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowFPS(bool isShow)
        {
            IsShowFPS = isShow;
        }

        /// <summary>
        /// DrawCall
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowDrawCall(bool isShow)
        {
            IsShowDrawCall = isShow;
        }

        /// <summary>
        /// Verts
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowVerts(bool isShow)
        {
            IsShowVerts = isShow;
        }

        /// <summary>
        /// Meshs
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowMeshs(bool isShow)
        {
            IsShowMeshs = isShow;
        }


        private void Start()
        {
            f_LastInterval = Time.realtimeSinceStartup;
        }


        /// <summary>
        /// 得到场景中所有的GameObject
        /// </summary>
        void GetAllObjects()
        {
            verts = 0;
            tris = 0;
            GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject obj in ob)
            {
                GetAllVertsAndTris(obj);
            }
        }
        //得到三角面和顶点数
        void GetAllVertsAndTris(GameObject obj)
        {
            Component[] filters;
            filters = obj.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter f in filters)
            {
                tris += f.sharedMesh.triangles.Length / 3;
                verts += f.sharedMesh.vertexCount;
            }
        }


        void Update()
        {

            if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
            {
                f_LastInterval = Time.realtimeSinceStartup;
                GetAllObjects();
            }
        }














    }
}

