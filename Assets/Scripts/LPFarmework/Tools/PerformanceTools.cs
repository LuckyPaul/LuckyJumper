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
	public  class PerformanceTools : UnitySingleton<PerformanceTools>
	{
        private FPSinDicator fPSinDicator;
        private VertsinDicator vertsinDicator;
        private TrisDicator trisDicator;
        private GUIStyle style;

        private bool IsShow = false;
        private  void Awake()
        {
            fPSinDicator = new FPSinDicator();
            vertsinDicator = new VertsinDicator();
            trisDicator = new TrisDicator();

            //GUI样式设置
            style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.red;
        }

        private void OnGUI()
        {
            if (!IsShow) return;

            GUI.Label(new Rect(20, Screen.height - 40, 200, 200), "FPS: " + this.fPSinDicator.Info.ToString("f2"), style);
            GUI.Label(new Rect(20, Screen.height - 80, 200, 200), "FPS: " + this.vertsinDicator.Info.ToString("f2"), style);
            GUI.Label(new Rect(20, Screen.height - 120, 200, 200), "FPS: " + this.trisDicator.Info.ToString("f2"), style);



        }

        public void OpenPerformanceTools()
        {
            IsShow = true;
            fPSinDicator.Show(true);
            vertsinDicator.Show(true);
            trisDicator.Show(true);
        }

        void Update()
        {
            if (!IsShow) return;

            fPSinDicator.GetInfo();
            vertsinDicator.GetInfo();
            trisDicator.GetInfo();
        }

    }

    public abstract class DicatorBase : MonoBehaviour
    {
        public bool IsShow;
        public float Info;



        public DicatorBase()
        {
            IsShow = false;
            Info = 0.0f;
        }


        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="isShow"></param>
        public  void Show(bool isShow)
        {
            IsShow = isShow;
        }


        public abstract void GetInfo();

    }

   /// <summary>
   /// FPS
   /// </summary>
    public class FPSinDicator : DicatorBase
    {
        public float fpsMeasuringDelta = 2.0f;

        private float timePassed=0.0f;
        private int m_FrameCount = 0;
       
        public override void GetInfo()
        {
            if (!IsShow) return;

            m_FrameCount = m_FrameCount + 1;
            timePassed = timePassed + Time.deltaTime;

            if (timePassed > fpsMeasuringDelta)
            {
                Info = m_FrameCount / timePassed;

                timePassed = 0.0f;
                m_FrameCount = 0;
            }
        }

    }

    /// <summary>
    /// Verts
    /// </summary>
    public class VertsinDicator : DicatorBase
    {
        public float fpsMeasuringDelta = 0.5f;
        private float timePassed = 0.0f;


        public VertsinDicator()
        {
            IsShow = false;
            Info = 0.0f;
            timePassed= Time.realtimeSinceStartup;

        }

        public override void GetInfo()
        {
            if (!IsShow) return;

            if (Time.realtimeSinceStartup > fpsMeasuringDelta + timePassed)
            {
                Info = 0;
                timePassed = Time.realtimeSinceStartup;
                GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject obj in ob)
                {
                    Component[] filters;
                    filters = obj.GetComponentsInChildren<MeshFilter>();
                    foreach (MeshFilter f in filters)
                    {
                        // tris += f.sharedMesh.triangles.Length / 3;
                        Info += f.sharedMesh.vertexCount;
                    }

                }

            }
        }

    }

    /// <summary>
    /// Tris
    /// </summary>
    public class TrisDicator : DicatorBase
    {
        public float fpsMeasuringDelta = 0.5f;
        private float timePassed = 0.0f;


        public TrisDicator()
        {
            IsShow = false;
            Info = 0.0f;
            timePassed = Time.realtimeSinceStartup;

        }

        public override void GetInfo()
        {
            if (!IsShow) return;

            if (Time.realtimeSinceStartup > fpsMeasuringDelta + timePassed)
            {
                Info = 0;
                timePassed = Time.realtimeSinceStartup;
                GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject obj in ob)
                {
                    Component[] filters;
                    filters = obj.GetComponentsInChildren<MeshFilter>();
                    foreach (MeshFilter f in filters)
                    {
                        Info += f.sharedMesh.triangles.Length / 3;
                    }

                }

            }
        }

    }

}

