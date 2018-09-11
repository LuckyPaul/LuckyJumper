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























    }
}

