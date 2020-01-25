using UnityEngine;

public class OnAnimationEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject startPanel;

    public void AnimationEnd()
    {
        this.gameObject.SetActive(false);
        startPanel.SetActive(true);
    }
}
