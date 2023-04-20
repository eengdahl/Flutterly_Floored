using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedNodes;
    private readonly float buttonCooldown = 5f;
    public bool buttonReady = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && buttonReady)
        {
            buttonReady = false;
            gameObject.transform.parent.GetComponent<StoveComponents>().ActivateNode(connectedNodes);
        }
        Invoke(nameof(ButtonCooldownReset), buttonCooldown);
    }

    private void ButtonCooldownReset()
    {
        buttonReady = true;
    }
}
