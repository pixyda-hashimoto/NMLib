using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefab : MonoBehaviour
{
    public TestPrefab(){
        Logger.Log("TestPrefab constructor");
    }
    ~TestPrefab(){
        Logger.Log("TestPrefab destructor");
    }
    public void test(){
        Logger.Log("TestPrefab test");
    }
}
