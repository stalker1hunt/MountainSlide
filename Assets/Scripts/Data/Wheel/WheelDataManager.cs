using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wheel Manager", menuName = "My Wheel/Wheel Manager", order = 51)]
public class WheelDataManager : ScriptableObject
{
    [SerializeField]
    private List<WheelData> wheelDatas = new List<WheelData>();

    public WheelData GetRandomWheel()
    {
        return wheelDatas[UnityEngine.Random.Range(0, wheelDatas.Count - 1)];
    }
}
