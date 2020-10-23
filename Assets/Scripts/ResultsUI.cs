using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
    public Text diamondsText;

    private void Start()
    {
        diamondsText.text = PlayerPrefs.GetInt("Last Diamonds").ToString() + " diamonds";
    }
}
