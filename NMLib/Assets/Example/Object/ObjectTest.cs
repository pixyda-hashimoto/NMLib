using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NMLib;


public class ObjectTest : MonoBehaviour
{
    // シンプルなクラス
    public class TestObject {
        public TestObject(){
            Logger.Log("TestObject.Constructor");
        }
        ~TestObject(){
            Logger.Log("TestObject.Destructor");
        }
        public void test(){
            Logger.Log("TestObject.test");
        }
    }
    // TestObjectをプールするクラス
    public class TestPool : ObjectPool<TestObject> {
        public TestPool() {
        }
        protected override TestObject CreateInstance(){
            Logger.Log("TestPool.CreateInstance");
            return new TestObject();
        }
        protected override void OnPop(TestObject instance){
            Logger.Log("TestPool.OnPop");
        }
        protected override void OnPush(TestObject instance){
            Logger.Log("TestPool.OnPush");
        }
        protected override void OnClear(TestObject instance){
            Logger.Log("TestPool.OnClear");
        }
    }

    // TestPrefabをプールするクラス
    public class TestPool2 : ObjectPool<TestPrefab> {
        private TestPrefab prefab;
        private Transform parent;
        public TestPool2(TestPrefab _prefab, Transform _parent) {
            prefab = _prefab;
            parent = _parent;
        }
        protected override TestPrefab CreateInstance(){
            Logger.Log("TestPool2.CreateInstance");
            return GameObject.Instantiate(prefab,parent);
        }
        protected override void OnPop(TestPrefab instance){
            Logger.Log("TestPool2.OnPop");
            instance.gameObject.SetActive(true);
        }
        protected override void OnPush(TestPrefab instance){
            Logger.Log("TestPool2.OnPush");
            instance.gameObject.SetActive(false);
        }
        protected override void OnClear(TestPrefab instance){
            Logger.Log("TestPool2.OnClear");
            UnityEngine.Object.Destroy(instance.gameObject);
        }
    }

    [SerializeField] private TestPrefab prefab = null;
    TestPool  pool1;
    TestPool2 pool2;
    List<TestObject> testObjects;
    List<TestPrefab> testPrefabs;

    void Start()
    {
        // オブジェクトプールテスト
        testObjects = new List<TestObject>();
        testPrefabs = new List<TestPrefab>();
        pool1 = new TestPool();
        pool2 = new TestPool2(prefab,this.transform);
    }

    void OnDestroy()
    {
        //オブジェクトプール開放
        pool1.Dispose();
        pool2.Dispose();
    }

    void Update()
    {
    }

    void OnGUI()
    {
        // GUI用の解像度設定
        //Vector2 guiScreenSize = new Vector2(1280, 720);	// 基準とする解像度
        float scale = (Screen.width > Screen.height) ? Screen.width  / 720f : Screen.height / 720f;
        GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), Vector2.zero);

        if(pool1!=null&&pool2!=null) 
        {
            GUILayout.BeginArea (new Rect(0, 0, 200, 512));
            GUILayout.Label("pool1 count [ object:"+testObjects.Count+" / pool:"+pool1.PoolCount+" ]");
            if (GUILayout.Button("pool1 Clean")) {
                pool1.Clear();
            }
            if (GUILayout.Button("pool1 Sweep 50%")) {
                pool1.Sweep(0.5f,1);
            }
            if (GUILayout.Button("pool1 pop")) {
                testObjects.Add(pool1.Pop());
            }
            if(testObjects.Count>0) {
                if (GUILayout.Button("pool1 push")) {
                    pool1.Push(testObjects[0]);
                    testObjects.RemoveAt(0);
                }
            }
            GUILayout.EndArea();

            GUILayout.BeginArea (new Rect(220, 0, 200, 512));
            GUILayout.Label("pool2 count [ object:"+testPrefabs.Count+" / pool:"+pool2.PoolCount+" ]");
            if (GUILayout.Button("pool2 Clean")) {
                pool2.Clear();
            }
            if (GUILayout.Button("pool2 Sweep 50%")) {
                pool2.Sweep(0.5f,1);
            }
            if (GUILayout.Button("pool2 pop")) {
                testPrefabs.Add(pool2.Pop());
            }
            if(testPrefabs.Count>0) {
                if (GUILayout.Button("pool2 push")) {
                    pool2.Push(testPrefabs[0]);
                    testPrefabs.RemoveAt(0);
                }
            }
            GUILayout.EndArea();



        }

        // GUIの解像度をリセット
        GUI.matrix = Matrix4x4.identity; 
    }

}
