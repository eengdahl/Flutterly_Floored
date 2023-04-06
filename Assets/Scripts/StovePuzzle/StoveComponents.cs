using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class StoveComponents : MonoBehaviour
{
    public Material nodeOn;
    public Material nodeOff;
    [SerializeField] private float delayT = 0.1f;

    private void ChangeMaterial(MeshRenderer nodeMeshRenderer, Material newMaterial)
    {
        nodeMeshRenderer.material = newMaterial;
    }

    public void ActivateNode(List<GameObject> nodes)
    {
        Debug.Log(nodes);
        //Toggles clicked star and connected ones
        foreach (GameObject node in nodes)
            StartCoroutine(ToggleOn(node.GetComponent<StoveNode>(), 0));
    }

    public IEnumerator ToggleOn(StoveNode node, float delayTime)
    {
        //Add delay variable to delay toggles
        yield return new WaitForSeconds(delayTime);
        node.ToggleActive();

        //Sets to active or inactive sprite (On/Off)
        if (node.litNode)
            ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOn);
        else
            ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOff);
    }

    //public IEnumerator checkWin(float delayTime)
    //{
    //    yield return new WaitForSeconds(delayTime);

    //    bool allOn = lines.All(x => x.GetComponent<StarLine>().litLine);

    //    if (allOn)
    //    {
    //        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
    //        {
    //            col.enabled = false;
    //        }
    //        ringApature.checkRing(name);
    //        NarratorHandler.Instance.PlayFromKeyWord(keyword);
    //    }
    //}
}
