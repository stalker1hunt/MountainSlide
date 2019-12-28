using UnityEngine;
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
        int xPos = Random.Range(395, 433);
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(xPos, transform.position.y, transform.localPosition.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            GameManager.Instance.Respawn();
    }
}
