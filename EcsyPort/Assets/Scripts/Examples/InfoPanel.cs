using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcsyPort;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    EcsTest test;

    [SerializeField]
    Text frameRateText;

    [SerializeField]
    Text statsText;

    void Update()
    {
        frameRateText.text = "Frame Rate: " + (1.0f/Time.deltaTime).ToString("F2");
        statsText.text = test.world.stats();
    }
}
