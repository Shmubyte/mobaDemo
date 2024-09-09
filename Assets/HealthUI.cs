using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSLider3D;
    public Slider healthSLider2D;

    public void Start3DSlider(float maxValue)
    {
        healthSLider3D.maxValue = maxValue;
        healthSLider3D.value = maxValue;

    }

    public void update3DSlider(float value)
    {
        healthSLider3D.value = value;
    }

    public void update2DSlider(float maxValue, float value) 
    {
        if (gameObject.CompareTag("Player")) 
        {
            healthSLider2D.maxValue = maxValue;
            healthSLider2D.value = value;
        }
    }

}
