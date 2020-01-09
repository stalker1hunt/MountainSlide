using UnityEngine;
using MountainSlide.Level.Block;
using MountainSlide.GameManager;
public class BlockDynamic : Block
{
    private void Start()
    {
        DynamicChange();
    }

    private void DynamicChange()
    {
        int xScale = Random.Range(1, 15);
        int xPos = Random.Range(-72, 70);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, xScale);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.position.y, xPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            GameManager.Instance.Respawn();
    }
}

