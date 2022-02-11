using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public int timeValue;
    public TextMeshProUGUI time;

    public int scoreValue;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        if (!time)
            return;

        if (!score)
            return;

        time.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (time == null)
            return;

        if (timeValue < 0)
        {
            time.text = "0";
            return;
        }

        var minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60);
        var secondes = Mathf.Floor(Time.timeSinceLevelLoad % 60);
        var secondesText = secondes.ToString();
        
        if (secondes < 10)
            secondesText = $"0{secondesText}";

        time.text = $"{minutes} : {secondesText}";
    }
}
