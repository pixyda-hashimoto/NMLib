using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    [CreateAssetMenu(menuName="ScriptableObject/Environment")]
    public class Environment : ScriptableObject {

        private static Environment instance = null;
        public static Environment Instance {
            get {
                if( instance == null ) {
                    instance = ScriptableObject.Instantiate(Resources.Load("Environment")) as Environment;
                    if( instance == null ) {
                        Logger.Assert(false, "Missing Environment.");
                        instance = CreateInstance<Environment>();
                    }
                }
                return instance;
            }
        }


        [SerializeField]
        private string dummy1 = "hehehe";
        public  static string Dummy1 {get{return Instance.dummy1;}} //面倒だけどプロパティ用意する？

        public int dummy2 = 444;
        public bool dummy3 = true;
        public float dummy4 = 8f;
        public bool dummy5 = false;

    }
}
