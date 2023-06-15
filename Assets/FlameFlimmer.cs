using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FlameFlimmer : MonoBehaviour
{
    public float minValue = 2f;
    public float maxValue = 2.5f;
    public float speed = 1f;
    Light lightComponent;
    private HDAdditionalLightData additionalLightData;
    private bool goingUp = true;
    private float currentValue;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
        currentValue = Random.Range(minValue, maxValue);
        additionalLightData = lightComponent.GetComponent<HDAdditionalLightData>();
    }

    private void Update()
    {
        if (goingUp)
        {
            currentValue += speed * Time.deltaTime;
            additionalLightData.SetRange(currentValue);
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                goingUp = false;
            }
        }
        else
        {
            currentValue -= speed * Time.deltaTime;
            additionalLightData.SetRange(currentValue);
            if (currentValue <= minValue)
            {
                currentValue = minValue;
                goingUp = true;
            }
        }

        // Use the current value for whatever you need
      //  Debug.Log(currentValue);
    }
}