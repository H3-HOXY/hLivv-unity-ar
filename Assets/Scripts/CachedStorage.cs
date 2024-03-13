using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using SFile = System.IO.File;

/**
 * @author 이호연
 */
public class CachedStorage : MonoBehaviour
{
    private ObjectSpawner _objectSpawner;
    private HashSet<AssetBundle> _loadedBundles;

    private void Awake()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        _loadedBundles = new HashSet<AssetBundle>();
    }

    void Start()
    {
        Debug.LogFormat(this, "path is : {0}", Application.persistentDataPath);

        Debug.LogFormat(
            $"PersistentPath {0} is {1}",
            Application.persistentDataPath,
            Directory.Exists(Application.persistentDataPath) ? "exists" : "not exists");

        var prefabs = _objectSpawner.objectPrefabs;
        prefabs.Clear();
    }

    public void LoadFromLocalStorage(string productId)
    {
        Debug.Log("LoadFromLocalStorage");
        if (!_loadedBundles.Select(bundle => bundle.name).ToArray().Contains(productId))
        {
            string[] bundles =
                Directory.GetFiles(Application.persistentDataPath)
                    .Where(fileName => fileName.Contains(productId))
                    .ToArray();

            foreach (var prefab in bundles)
            {
                var asset = AssetBundle.LoadFromFile(prefab);
                if (asset == null)
                {
                    Debug.Log($"{productId}에 대한 bundle을 로딩할 수 없습니다.");
                }

                _loadedBundles.Add(asset);

                var prefabName = asset.name;

                var furniturePrefab = asset.LoadAsset<GameObject>($"{prefabName}");

                if (furniturePrefab == null)
                {
                    Debug.Log($"file : {prefab} {prefabName} not exists ");
                    continue;
                }

                Debug.Log($"file : {prefabName} exists");
                _objectSpawner.objectPrefabs.Add(furniturePrefab);
            }
        }

        int idx = _objectSpawner.objectPrefabs
            .Select((o, i) => new Tuple<string, int>(o.name, i))
            .Where((o, i) => string.Equals(o.Item1, productId, StringComparison.CurrentCultureIgnoreCase))
            .First().Item2;
        Debug.LogFormat("selected index is {0}", idx);
        _objectSpawner.spawnOptionIndex = idx;
        _objectSpawner.EnableSpawn();
    }
}