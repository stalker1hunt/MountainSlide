using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "My Level/Level", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelName;
    public string LevelName { get { return levelName; }  }
}
