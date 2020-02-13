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

        if (other.tag == "Player")
            GameManager.Instance.BoostTake(TypeBoost);

        if (other.tag == "Bot")
            Debug.Log("Logic for boost bot");
    }
}
