using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableAssetLoadUtility
{
    public static T LoadAssetAsync<T>(string _address) where T : Object
    {
        var handle = Addressables.LoadAssetAsync<T>(_address);
        var asset = handle.WaitForCompletion();
        return (T)asset;
    }

    public static List<T> LoadAssetsAsync<T>(string _labelName) where T : Object
    {
        var handle = Addressables.LoadAssetsAsync<T>(_labelName, null);
        var assets = handle.WaitForCompletion();
        return (List<T>)assets;
    }
}
