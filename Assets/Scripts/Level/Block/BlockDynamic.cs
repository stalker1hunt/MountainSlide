using UnityEngine;
using MountainSlide.Level.Block;
using MountainSlide.GameManager;
public class BlockDynamic : Block
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Bot")
            this.gameObject.SetActive(false);

        if (collision.collider.tag == "Bot")
            Debug.Log("Logic for red Wall bot");
    }
}

