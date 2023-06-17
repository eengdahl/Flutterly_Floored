using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{

    [SerializeField] GameStats gameStats;
    [SerializeField] Button endButton;
    bool hasShown = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !hasShown)
        {
            gameStats.ShowStats();
            hasShown = true;
            //Invoke(nameof(ChangeToMenu), 5);
        }
    }

    
   public void ChangeToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
