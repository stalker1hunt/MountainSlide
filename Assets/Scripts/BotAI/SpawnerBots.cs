using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField]
    private BotAI objectBot;
    [SerializeField]
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField]
    [Range(0, 4)]
    private int countEnemy;
    [HideInInspector]
    public List<GameObject> Pull = new List<GameObject>();

    private void Start()
    {
        SetupSpawner();
    }

    private void SetupSpawner()
    {
        for (int i = 0; i < countEnemy; i++)
        {
            var _bot = Instantiate(objectBot, GetPos(), true);
            _bot.FindWay();
            Pull.Add(_bot.gameObject);
        }
    }

    private Transform GetPos()
    {
        var _point = spawnPoints.Find(x => x.PositionTake == false);
     
        if(_point == null)
            throw new Exception("Cant find spawnPoint for Bot!");

        _point.PositionTake = true;
        return _point.gameObject.transform;
    }
}
