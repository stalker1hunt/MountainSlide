using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    private static UiController instance;
    public static UiController Instance { get { return instance ? instance : instance = FindObjectOfType<UiController>(); } }

    [SerializeField]
    private GameObject chooseLevel;
    [SerializeField]
    private GameObject startPanel;

    public void Start()
    {
        Application.targetFrameRate = 30;
    }

    public void StartGame()
    {
        chooseLevel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CloseLevelPanel()
    {
        chooseLevel.GetComponent<Animator>().SetTrigger("Hide");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
