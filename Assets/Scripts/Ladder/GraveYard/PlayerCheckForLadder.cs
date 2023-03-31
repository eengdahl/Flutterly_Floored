using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckForLadder : MonoBehaviour
{
    Vector3 direction = new Vector3(0,0,-1);
    float grabDistance = 1f;
    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, direction);
        if(Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, grabDistance))
        {
            //raycastHit.transform.TryGetComponent(out LadderScript ladder);
        }
    }
}
