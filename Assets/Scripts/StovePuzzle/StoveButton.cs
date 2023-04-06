using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedNodes;

    private void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(connectedNodes);
            gameObject.transform.parent.GetComponent<StoveComponents>().ActivateNode(connectedNodes);
        }
    }
}
