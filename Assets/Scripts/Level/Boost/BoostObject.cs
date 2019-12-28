using UnityEngine;

public class BoostObject : Boost
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        MountainSlide.GameManager.GameManager.Instance.BoostTake(TypeBoost);
    }
}
