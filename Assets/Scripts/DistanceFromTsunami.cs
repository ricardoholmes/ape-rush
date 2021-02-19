using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceFromTsunami : MonoBehaviour
{
    TMP_Text textComponent;
    public int redTrigger = 2;
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (int.Parse(textComponent.text.Split('m')[0]) <= redTrigger)
        {
            textComponent.color = new Color32(210, 0, 0, 255);

        } else
        {
            textComponent.color = new Color32(0, 65, 101, 255);
        }
    }
}
