using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayM2Spoon : MonoBehaviour
{

    [SerializeField] GameObject m1Canvas;
    bool hasShown = false;

    public bool show = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && show)
        {

            m1Canvas.SetActive(true);
            hasShown = true;
            Invoke(nameof(DeactivateM2), 5f);

        }
    }

    public void DeactivateM2()
    {
        m1Canvas.SetActive(false);

    }
}
