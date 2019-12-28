using MountainSlide.GameManager;
using MountainSlide.Level.Block;
using UnityEngine;

public class FinishZone : Block
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            GameManager.Instance.FinishLevel();
    }
}
