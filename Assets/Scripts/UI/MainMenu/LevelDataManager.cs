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

    /// <summary>
    /// Пишем индекс уровеня в массиве, если хотите взять 1 уровень пишем 0
    /// </summary>
    /// <param name="numberLevel"></param>
    /// <returns></returns>
    public LevelData GetLevel(int numberLevel)
    {
        if (numberLevel > levelDatas.Count - 1)
            return null;

        return levelDatas[numberLevel];
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
