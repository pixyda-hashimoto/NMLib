using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace NMLib
{
    public static class LoadSceneManager
    {
        public static bool IsLoading { get; private set; } = false;
        
        public static void LoadScene(string nextScene)
        {
            IsLoading = true;
            Observable.FromCoroutine(() => LoadCoroutine(nextScene))
                .Subscribe(_ => IsLoading = false );
        }

        public static IEnumerator LoadCoroutine(string nextScene)
        {
            var async = SceneManager.LoadSceneAsync(nextScene);
            async.allowSceneActivation = false;

            while(async.progress < 0.9f) {
                Logger.Log(nextScene+" : progress = "+async.progress);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            async.allowSceneActivation = true;


    /*
    // こんなのがあるとして
    IEnumerator CoroutineA()
    {
        Debug.Log("a start");
        yield return new WaitForSeconds(1);
        Debug.Log("a end");
    }
    
    // こんなふうに使える
    Observable.FromCoroutine(CoroutineA)
        .Subscribe(_ => Debug.Log("complete"));
    
    // 戻り値のあるバージョンがあるとして
    IEnumerator CoroutineB(IObserver<int> observer)
    {
        observer.OnNext(100);
        yield return new WaitForSeconds(2);
        observer.OnNext(200);
        observer.OnCompleted();
    }
    
    // こんなふうに合成もできる
    var coroutineA = Observable.FromCoroutine(CoroutineA);
    var coroutineB = Observable.FromCoroutine<int>(observer => CoroutineB(observer));
    
    // Aが終わった後にBの起動、Subscribeには100, 200が送られてくる
    var subscription = coroutineA.SelectMany(coroutineB).Subscribe(x => Debug.Log(x));
    
    // Subscribeの戻り値からDisposeを呼ぶとキャンセル可能
    // subscription.Dispose();

    */

        }
    }
}