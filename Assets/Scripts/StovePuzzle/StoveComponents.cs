using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class StoveComponents : MonoBehaviour
{
    public Material nodeOn;
    public Material nodeOff;
    //[SerializeField] private float delayT = 0.1f;
    [SerializeField] private List<GameObject> allNodes;
    AudioSource aS;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        foreach (GameObject node in allNodes)
            StartCoroutine(InitateStove(node.GetComponent<StoveNode>(), 0));
    }
    private IEnumerator ChangeMaterial(MeshRenderer nodeMeshRenderer, Material newMaterial, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
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
        {
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOn, 0));
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOff, 5));
        }
        else
        {
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOff, 0));
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOn, 5));
        }
    }

    public void ToggleAllNodes(List<GameObject> nodes)
    {
        foreach (GameObject node in nodes)
            StartCoroutine(ToggleOn(node.GetComponent<StoveNode>(), 0));

        //GetComponent<StoveNode>().SetAllActive();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aS.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aS.Stop();
        }
    }

        public IEnumerator InitateStove(StoveNode node, float delayTime)
    {
        //Add delay variable to delay toggles
        yield return new WaitForSeconds(delayTime);
        node.ToggleActive();

        //Sets to active or inactive sprite (On/Off)
        if (node.litNode)
        {
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOn, 0));
        }
        else
        {
            StartCoroutine(ChangeMaterial(node.GetComponent<MeshRenderer>(), nodeOff, 0));
        }
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
