using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    [AddComponentMenu("")]
    public class NMLibComponent : MonoBehaviour
    {
        [SerializeField] 
        private bool dontDestroyOnLoad = false;
        
        void Awake() {

            #if UNITY_EDITOR
            Logger.Log("defined #UNITY_EDITOR");
            #endif
            #if DEVELOPMENT_BUILD
            Logger.Log("defined #DEVELOPMENT_BUILD");
            #endif
            #if DEBUG
            Logger.Log("defined #DEBUG");
            #endif

            Logger.Log("dummy1:"+Environment.Dummy1);
            Logger.Log("dummy2:"+Environment.Instance.dummy2);
            Logger.Log("dummy3:"+Environment.Instance.dummy3);
            Logger.Log("dummy4:"+Environment.Instance.dummy4);
            Logger.Log("dummy5:"+Environment.Instance.dummy5);

            InputManager.Initialize();
            if(dontDestroyOnLoad) {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        void Update()
        {
            InputManager.Update();
        }
    }
}