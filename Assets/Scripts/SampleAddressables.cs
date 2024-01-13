using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SampleAddressables : MonoBehaviour
{
    //���@�P�F�A�h���X�̒��ڎw��BT�͌^�B
    //Addressables.LoadAssetAsync<T>("HogeHoge");
    //��{�I�ɂ͔񓯊��iTask�n�A�R���[�`���j�ȃ��[�h�A�����I�ȃ��[�h���\�B

    [SerializeField] private string addressName;
    [SerializeField] private string labelName;

    //Task�̎g�p
    private async Task SetObject01(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var prefab01 = handle.Result;
            Instantiate(prefab01);
        }

        //���[�h�����łȂ��C���X�^���X���܂ōs�������ꍇ�ȉ��̂悤�ɋL�q�B
        //��������V�[�����j�����ꂽ�^�C�~���O��Release�����B
        var prefab02 = Addressables.InstantiateAsync(_address);
    }

    //UniTask�̎g�p
    private async UniTask SetObject02(string _address)
    {
        var prefab01 = await Addressables.LoadAssetAsync<GameObject>(_address).Task.AsUniTask();
        Instantiate(prefab01);
    }

    //�R���[�`��
    private IEnumerator SetObject03(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
        }
    }

    //�����I�ȃ��[�h
    private void SetObject04(string _address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_address);
        //�����I�Ƀ��[�h������ҋ@
        var prefab = handle.WaitForCompletion();
        Instantiate(prefab);
    }

    //���x���̎g�p
    private async UniTask SetObjects(string _label)
    {
        var prefabs = await Addressables.LoadAssetsAsync<GameObject>(_label, null).Task.AsUniTask();
        foreach (var prefab in prefabs)
        {
            Instantiate(prefab);
        }

    }

    //�����[�X
    private void ReleaseObject(AsyncOperationHandle _handle)
    {
        Addressables.Release(_handle);
    }

    //���@�Q�F�V���A���C�Y����AssetReference���g�p

    [SerializeField] private AssetReferenceGameObject referenceObj;

    private async UniTask SetObject05()
    {
        var prefab = await referenceObj.LoadAssetAsync<GameObject>().Task.AsUniTask();
        Instantiate(prefab);
    }

    //�V�[���̃��[�h
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
