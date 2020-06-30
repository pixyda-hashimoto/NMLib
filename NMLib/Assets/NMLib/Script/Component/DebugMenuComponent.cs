using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    [AddComponentMenu("")]
    public class DebugMenuComponent : MonoBehaviour
    {
        private static Dictionary<string,Action> menuItems;
    //    private static Action menuItems = null;
        public static bool IsMenu { get; private set; } = false;

        void Start() {
    /*
            menuItems += ()=>{
                if (GUI.Button(new Rect(10, 10, 100, 60), "scene1")) {
                    Debug.Log("load begin");
                    LoadSceneManager.LoadScene("scene1");
                    Debug.Log("load end");
                }
            };
            menuItems += ()=>{
                if (GUI.Button(new Rect(10, 80, 100, 60), "scene2")) {
                    Debug.Log("load begin");
                    LoadSceneManager.LoadScene("scene2");
                    Debug.Log("load end");
                }
            };
    */
            menuItems = new Dictionary<string,Action>();
            menuItems.Add("scene1",()=>{
                if (GUI.Button(new Rect(10, 10, 100, 60), "scene1")) {
                    Debug.Log("load begin");
                    LoadSceneManager.LoadScene("scene1");
                    Debug.Log("load end");
                }
            });
            menuItems.Add("scene2",()=>{
                if (GUI.Button(new Rect(10, 80, 100, 60), "scene2")) {
                    Debug.Log("load begin");
                    LoadSceneManager.LoadScene("scene2");
                    Debug.Log("load end");
                }
            });
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                IsMenu = !IsMenu;
            }
        }

        void OnGUI()
        {
            if(IsMenu) {

                // GUI用の解像度設定
                //Vector2 guiScreenSize = new Vector2(1280, 720);	// 基準とする解像度
                float scale = (Screen.width > Screen.height) ? Screen.width  / 720f : Screen.height / 720f;
                GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), Vector2.zero);

                if(!LoadSceneManager.IsLoading) {
    //                menuItems?.Invoke();
                    
                    foreach(var item in menuItems) {
                        item.Value();
                    }

                    /*
                    if (GUI.Button(new Rect(10, 10, 100, 60), "scene1")) {
                        Debug.Log("load begin");
                        LoadSceneManager.LoadScene("scene1");
                        Debug.Log("load end");
                    }
                    if (GUI.Button(new Rect(10, 80, 100, 60), "scene2")) {
                        Debug.Log("load begin");
                        LoadSceneManager.LoadScene("scene2");
                        Debug.Log("load end");
                    }
                    if (GUI.Button(new Rect(10, 150, 100, 60), "scene3")) {
                        Debug.Log("load begin");
                        LoadSceneManager.LoadScene("scene3");
                        Debug.Log("load end");
                    }
                    if (GUI.Button(new Rect(10, 220, 100, 60), "scene4")) {
                        Debug.Log("load begin");
                        LoadSceneManager.LoadScene("scene4");
                        Debug.Log("load end");
                    }
                    */
                }

                // GUIの解像度をリセット
                GUI.matrix = Matrix4x4.identity; 
            }
        }

        /// <summary>
        /// デバッグメニューを開く
        /// </summary>
        public void Open() { IsMenu = true; }
        /// <summary>
        /// デバッグメニューを閉じる
        /// </summary> 
        public void Close() { IsMenu = false; }
    }
}