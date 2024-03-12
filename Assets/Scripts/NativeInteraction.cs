using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class NativeInteraction : MonoBehaviour
{
    [SerializeField] [Tooltip("오브젝트 생성을 처리하는 컴포넌트입니다.")]
    private ObjectSpawner m_ObjectSpawner;

    public void RandomizeSelection(string message)
    {
        Debug.Log($"RandomizeSelection {message}");
        m_ObjectSpawner.EnableSpawn();
        m_ObjectSpawner.RandomizeSpawnOption();
    }
}