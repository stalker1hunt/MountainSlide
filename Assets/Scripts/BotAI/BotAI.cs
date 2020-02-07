using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RVP;

[RequireComponent(typeof(FollowAI))]
public class BotAI : MonoBehaviour
{
    private FollowAI moveBotAI;

    private void Awake()
    {
        moveBotAI = GetComponent<FollowAI>();
    }

    public void FindWay(Action onSucses = null, Action onFailed = null)
    {
        var _path = TravelPathManager.Instance.GetRandomTravelPath();
        moveBotAI.target = _path.StartPoint;
    }
}
