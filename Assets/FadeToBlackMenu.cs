using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackMenu : MonoBehaviour
{
    public GameObject fadeToBlackImage;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator MenuFade(bool fadeToBlack = true, float fadeSpeed = 1, float delay = 1)
    {
        Color objectColor = fadeToBlackImage.GetComponent<Image>().color;
        float fadeAmount;

        yield return new WaitForSeconds(delay);
        if (fadeToBlack)
        {
            while (fadeToBlackImage.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (fadeToBlackImage.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }

    }
}
