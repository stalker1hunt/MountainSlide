using MountainSlide.GameManager;
using MountainSlide.Level.Boost;
using UnityEngine;

public class BoostObject : Boost
{
    [SerializeField]
    private GameObject takeBoosteEffect;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(takeBoosteEffect, this.gameObject.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
        GameManager.Instance.BoostTake(TypeBoost);
    }

    private void Update()
    {
        transform.Rotate(0, 2.0f, 0);
    }
}
