using UnityEngine;

[CreateAssetMenu(fileName = "New Wheel", menuName = "My Wheel/Wheel", order = 51)]
public class WheelData : ScriptableObject
{
    [SerializeField]
    private GameObject meshObject;
    public GameObject MeshObject { get { return meshObject; } }
}
