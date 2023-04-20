using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    CinemachineImpulseSource impuls;
    // Start is called before the first frame update
    void Start()
    {
        impuls = transform.GetComponent<CinemachineImpulseSource>();
        //Invoke("shakeCatMoves", 5f);
    }

    public void shakeObjectFalls()
    {
        impuls.GenerateImpulse(0.1f);
    }
    public void shakeCatMoves()
    {
        impuls.GenerateImpulse(0.01f);
    }
}
