using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuHandler : MonoBehaviour
{
    public Animator birdAnimator;
    public Animator paperAnimator;
    string startGamepaper = "Plane_004|Plane_004Action_001";


    string startGame = "crash in to window";
    public AudioClip pang;
    AudioSource aS;


    SwitchControls switchControls;



    void Start()
    {
        aS = GetComponent<AudioSource>();
        switchControls = FindAnyObjectByType<SwitchControls>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    public void StartGameButton()
    {
        paperAnimator.CrossFade(startGamepaper, 0, 0);
        birdAnimator.CrossFade(startGame, 0,0);

        Invoke(nameof(StartGame), 2);
       // Invoke(nameof(playPang), 1.3f);

    }
    public void ExitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }


    void playPang()
    {
        aS.PlayOneShot(pang);
    }
    private void StartGame()
    {

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
