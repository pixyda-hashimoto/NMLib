#if DEBUG
#define DEBUG_SINGLETON
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    public class Singleton<T> where T : class, new()
    {
        static class SingletonCreator {
            static SingletonCreator() { 
#if DEBUG_SINGLETON
                Logger.Log("SingletonCreator constructor"); 
#endif
            }
            internal static readonly T instance = new T();
        }

        protected Singleton() {
#if DEBUG_SINGLETON
            Logger.Log("Singleton constructor"); 
            Logger.Assert( SingletonCreator.instance==null, "singleton duplicate" );
#endif
        }
        public static T Instance {
            get { return SingletonCreator.instance; }
        }
    }

}
