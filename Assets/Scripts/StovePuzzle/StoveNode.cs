using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveNode : MonoBehaviour
{
    [SerializeField] private List<GameObject> connectedNodes;

    public bool litNode;
    public Material materialOn;
    public Material materialOff;

    private void Start()
    {
        
    }
    public void ToggleActive()
    {
        litNode = !litNode;
    }

    public void ChangeMaterial(MeshRenderer meshRenderer, Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }
}
