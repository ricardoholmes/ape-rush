using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceFromTsunami : MonoBehaviour
{
    TMP_Text textComponent;
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (int.Parse(textComponent.text.Split('m')[0]) <= 3)
        {
            textComponent.color = new Color32(255, 0, 0, 255);

        } else
        {
            textComponent.color = new Color32(0, 0, 0, 255);
        }
    }
}
