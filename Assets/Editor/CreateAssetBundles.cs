using UnityEditor;

public class CreateAssetBundles
{
    private const string BundlePath = "./AssetBundles";

    [MenuItem("Assets/Build AssetBundles/Build Android AssetBundles")]
    static void BuildAllAndroidAssetBundles()
    {
        string assetBundleDirectory = $"{BundlePath}/Android";
        if (!System.IO.Directory.Exists(assetBundleDirectory))
        {
            System.IO.Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            BuildAssetBundleOptions.None,
            BuildTarget.Android);
    }

    [MenuItem("Assets/Build AssetBundles/Build Windows AssetBundles")]
    static void BuildAllWindowsAssetBundles()
    {
        string assetBundleDirectory = $"{BundlePath}/Windows";
        if (!System.IO.Directory.Exists(assetBundleDirectory))
        {
            System.IO.Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
}