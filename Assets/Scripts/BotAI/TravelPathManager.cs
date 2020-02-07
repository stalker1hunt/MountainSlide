using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPathManager : MonoBehaviour
{
    private static TravelPathManager instance;
    public static TravelPathManager Instance { get { return instance ? instance : instance = FindObjectOfType<TravelPathManager>(); } }

    [SerializeField]
    private List<TravelPath> travelPaths = new List<TravelPath>();

    public TravelPath GetRandomTravelPath()
    {
       return travelPaths[Random.Range(0, travelPaths.Count - 1)];
    }

    //todo Дописать
    public TravelPath GenerateTravelPath()
    {
        return null;
        /*
        TravelPath path = new TravelPath();


        for (int i = 0; i < length; i++)
        {

        }
        */
    }
}
