﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace OpenIM
{
    public class InitInfo{
            public int platform;
            public string api_addr;
            public string ws_addr;
            public string data_dir;
            public int log_level;
            public string object_storage;
            public string encryption_key;
    }

    public partial class OpenIMClient
    {
        static AndroidJavaClass _Instance = null;

        public static void Init()
        {
            _Instance = new AndroidJavaClass("open_im_sdk.Open_im_sdk");
            if (_Instance == null)
                Debug.LogError("not find open_im_sdk.Open_im_sdk");
        }
        #region Tools
        public static string GetTimeStamp(){
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        }
        #endregion

        public static void InitSDK(OnConnListenerCallBack callback,InitInfo info)
        {
            if (_Instance == null) 
                Init();

            var args = JsonUtility.ToJson(info);
            Debug.Log("args = " + args);
            _Instance.CallStatic<bool>("initSDK",callback,GetTimeStamp(),args);
        }
        
        public static void Login(BaseCallBack callback,string uid,string token){
            Debug.Log("Login UID = " + uid);
            Debug.Log("Login TOKEN = " + token);
            _Instance.CallStatic("login",callback,GetTimeStamp(),uid,token);
        }


    }
    
}
