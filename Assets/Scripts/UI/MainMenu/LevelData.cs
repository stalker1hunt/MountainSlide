using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelName;
    public string LevelName { get { return levelName; }  }
}
