using RVP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPath : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    public Transform StartPoint { get { return startPoint; } private set { } }

    private List<Transform> generationPoints = new List<Transform>();
    public void AddPoint(Transform _waypoint)
    {
        generationPoints.Add(_waypoint);
    }
}
