/***
 * 
 *    Title: LPFamework
 *           主题：  网络-socket通讯工具  
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/12/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using LuckyPual.Tools;
using System;

namespace LuckyPual.Net
{
    public class LPSocketHelper:Singleton<LPSocketHelper>
    {
        public ProtocolType protocolType= ProtocolType.Tcp;
        private Socket socket;
        public delegate void ConnectCallback();

        ConnectCallback connectSuccessedDelegate = null;
        ConnectCallback connectFailedDelegate = null;

        private bool isStopReceive = true;


        /// <summary>
        /// 连接状态
        /// </summary>  
        /// <see cref="Socket.Connected"/>
        public bool Connected
        {
             get { return socket != null && socket.Connected; }
        }


        /// <summary>
        /// 初始化
        /// </summary>
        public new void Init()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType);
        }

        /// <summary>
        /// 连接远程端口
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_port"></param>
        public void Connect(string _ip, int _port)
        {
            IAsyncResult result =socket.BeginConnect(_ip, _port, OnConnectedCallBack, this);

            bool success = result.AsyncWaitHandle.WaitOne(5000, true);
            if (!success)
            {
                Closed();
                if (connectFailedDelegate != null) connectFailedDelegate();
            }
            else
            {
                isStopReceive = false;
            }
        }


        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="ar"></param>
        public void  OnConnectedCallBack(IAsyncResult ar)
        {
            if (!socket.Connected)
            {
                if (connectFailedDelegate != null) connectFailedDelegate();
                return;
            }

            if (connectSuccessedDelegate != null)
            {
                connectSuccessedDelegate();
            }
        }

        /// <summary>
        /// 消息接收
        /// </summary>
        private void ReceiveMessage()
        {
            while (!isStopReceive)
            {
                if (!socket.Connected)
                {
                    //断开连接  结束线程
                    socket.Close();
                    break;
                }

                try
                {
                    //接受数据保存至bytes当中  
                    byte[] bytes = new byte[4096];
                    //Receive方法中会一直等待服务端回发消息  
                    //如果没有回发会一直在这里等着。  

                    int i = socket.Receive(bytes);

                    if (i <= 0)
                    {
                        socket.Close();
                        break;
                    }



                }
                catch (Exception ee)
                {
                    Debug.Log("数据接收异常:" + ee);
                    socket.Close();
                    break;
                }




            }
        }



        public void AsyReceiveMessage()
        {

        }




        /// <summary>
        /// 发送消息
        /// </summary>
        private void SendMessage(byte[] data)
        {
            if (socket == null) return;
            if (!socket.Connected)
            {
                Closed();
                return;
            }
            try
            {

             IAsyncResult asyncSend = socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
             bool success = asyncSend.AsyncWaitHandle.WaitOne(5000, true);
             if (!success)
             {
                Closed();
             }

            }
            catch (Exception ee)
            {
                Debug.Log("发送异常 : " + ee.ToString());
            }
        }

        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="asyncConnect"></param>
        private void SendCallback(IAsyncResult asyncConnect)
        {
            Debug.Log("send success");
        }


        /// <summary>
        /// 关闭socket
        /// </summary>
        public void Closed()
        {
            isStopReceive = true;
            if (socket != null && socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            socket = null;
        }





    }


}