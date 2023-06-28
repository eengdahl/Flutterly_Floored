using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{

    [SerializeField] GameStats gameStats;
    [SerializeField] Button endButton;
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject fadeToBlackImage;
    bool hasShown = false;
    SwitchControls controlsSwitch;

    private void Start()
    {
        controlsSwitch = GameObject.Find("Player").GetComponent<SwitchControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !hasShown)
        {
            //End cutscene
            controlsSwitch.SwitchToNoInput();
            StartCoroutine(FadeToBlack());
            Invoke(nameof(Startcutscene),2);
            Invoke(nameof(Endcutscene),11f);

            //End of game stats menu

            //hasShown = true;
            //Invoke(nameof(ChangeToMenu), 5);
        }
    }

    public IEnumerator FadeToBlack(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Color objectColor = fadeToBlackImage.GetComponent<Image>().color;
        float fadeAmount;

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

    private void Startcutscene()
    {
        playerModel.SetActive(false);
        StartCoroutine(FadeToBlack(false));
        cutscene.SetActive(true);
    }

    private void Endcutscene()
    {
        StartCoroutine(FadeToBlack(true, 2));
        gameStats.ShowStats();
    }

    public void ChangeToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
