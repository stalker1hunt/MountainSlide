using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostUi : MonoBehaviour
{
    [SerializeField]
    private Image iconBoost;
    [SerializeField]
    private TextMeshProUGUI timeBoost; 

    public float BoostTime { private get { return float.Parse(timeBoost.text); } set { timeBoost.text = value.ToString(); } }


    public void SetupBoostUi(TypeBoost typeBoost)
    {
        switch (typeBoost)
        {
            case TypeBoost.Default:
                break;
            case TypeBoost.SpeedUp:
                iconBoost.color = Color.blue;
                break;
        }
    }
}
