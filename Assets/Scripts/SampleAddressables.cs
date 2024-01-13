using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SampleAddressables : MonoBehaviour
{
    //方法１：アドレスの直接指定。Tは型。
    //Addressables.LoadAssetAsync<T>("HogeHoge");
    //基本的には非同期（Task系、コルーチン）なロード、同期的なロードも可能。

    [SerializeField] private string addressName;
    [SerializeField] private string labelName;

    //Taskの使用
    private async Task SetObject01(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var prefab01 = handle.Result;
            Instantiate(prefab01);
        }

        //ロードだけでなくインスタンス化まで行いたい場合以下のように記述。
        //所属するシーンが破棄されたタイミングでReleaseされる。
        var prefab02 = Addressables.InstantiateAsync(_address);
    }

    //UniTaskの使用
    private async UniTask SetObject02(string _address)
    {
        var prefab01 = await Addressables.LoadAssetAsync<GameObject>(_address).Task.AsUniTask();
        Instantiate(prefab01);
    }

    //コルーチン
    private IEnumerator SetObject03(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
        }
    }

    //同期的なロード
    private void SetObject04(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        //同期的にロード完了を待機
        var prefab = handle.WaitForCompletion();
        Instantiate(prefab);
    }

    //ラベルの使用
    private async UniTask SetObjects(string _label)
    {
        var prefabs = await Addressables.LoadAssetsAsync<GameObject>(_label, null).Task.AsUniTask();
        foreach (var prefab in prefabs)
        {
            Instantiate(prefab);
        }

    }

    //リリース
    private void ReleaseObject(AsyncOperationHandle _handle)
    {
        Addressables.Release(_handle);
    }

    //方法２：シリアライズしたAssetReferenceを使用

    [SerializeField] private AssetReferenceGameObject referenceObj;

    private async UniTask SetObject05()
    {
        var prefab = await referenceObj.LoadAssetAsync<GameObject>().Task.AsUniTask();
        Instantiate(prefab);
    }

    //シーンのロード
    private void ChangeScene(string _address)
    {
        Addressables.LoadSceneAsync(_address, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    private void Start()
    {
        //await SetObject01(addressName);
        //SetObject02(addressName).Forget();
        //StartCoroutine(SetObject03(addressName));
        //SetObject04(addressName);
        //SetObject05().Forget();

        //SetObjects(labelName).Forget();

        //ChangeScene(addressName);
    }
}
