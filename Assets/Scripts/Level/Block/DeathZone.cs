using UnityEngine;
using MountainSlide.Level.Block;
using MountainSlide.GameManager;

public class DeathZone : Block
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            GameManager.Instance.Respawn();

        if (collision.collider.tag == "Bot")
            Debug.Log("Logic for bot");
    }
}
