using UnityEngine;

public class WheelObject : MonoBehaviour
{
    [SerializeField]
    private Transform wheelMeshContent;

    void Start()
    {
        var _wheelData = Resources.Load<WheelDataManager>("Data/WheelManager");
        var _wheel = _wheelData.GetRandomWheel();
        Instantiate(_wheel.MeshObject, wheelMeshContent, false);
    }
}
