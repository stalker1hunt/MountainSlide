using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelManager", menuName = "My Level/Level Manager", order = 51)]
public class LevelDataManager : ScriptableObject
{
    [SerializeField]
    private List<LevelData> levelDatas = new List<LevelData>();

    public LevelData GetLevel(string name)
    {
        return levelDatas.Find(x => x.LevelName == name);
    }

    public LevelData GetNextLevel(string currentLevel)
    {
        int curentIndexLevel = levelDatas.IndexOf(levelDatas.Find(x => x.LevelName == currentLevel));
        curentIndexLevel++;
        if (curentIndexLevel > levelDatas.Count - 1)
            return null;

        return levelDatas[curentIndexLevel];
    }
}
