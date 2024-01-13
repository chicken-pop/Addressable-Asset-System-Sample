using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class EnemyFactorytSample : MonoBehaviour
{
    [SerializeField] private bool isAddressables;

    //ステージ 1：強参照、Addressables なし
    
    [SerializeField] private GameObject[] enemys;

    private void NoAddressablesCreateEnemy()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            var enemy = Instantiate(enemys[i]);
            enemy.transform.position = new Vector3(-3 + i * 3, 0);

        }
    }
    
    //ステージ 2：Addressables を実装する
    [SerializeField] private string labelName;

    private void AddressablesCreateEnemy(string _labelName)
    {
        var handle = Addressables.LoadAssetsAsync<GameObject>(_labelName, null);
        var prefabs = handle.WaitForCompletion();
        for (int i = 0; i < prefabs.Count; i++)
        {
            var enemy = Instantiate(prefabs[i]);
            enemy.transform.position = new Vector3(-3 + i * 3, 0);

        }
    }

    private void Start()
    {
        if (!isAddressables)
        {
            //NoAddressablesCreateEnemy();
        }
        else
        {
            //AddressablesCreateEnemy(labelName);
        }
    }
}
