using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelElement : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;

    public void LoadLevel()
    {
        UiController.Instance.LoadLevel(levelData.LevelName);
    }
}
