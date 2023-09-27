using System.Collections;
using UnityEngine;
public class Bookblock : MonoBehaviour
{
    public Transform targetPosition;
    public Vector3 targetRotation;
    public float duration = 2f;
    public AnimationCurve easeCurve;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool isMoving = false;
    [SerializeField] ScreenShake screenShake;

    [SerializeField]GameObject vitrincollider;
    [SerializeField] GameObject targetFloorPosition;
    string layer = "Ground";
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void MoveAndRotate()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveAndRotateCoroutine());
        }
    }

   public void BookBlockRemove()
    {
        vitrincollider.SetActive(false);
        transform.position = targetFloorPosition.transform.position;
        transform.rotation = targetFloorPosition.transform.rotation;
        gameObject.tag = "Ground";
        int layerInt = LayerMask.NameToLayer(layer);
        gameObject.layer = layerInt;
    }

    private IEnumerator MoveAndRotateCoroutine()
    {
        isMoving = true;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float easeValue = easeCurve.Evaluate(t);

            transform.position = Vector3.Lerp(initialPosition, targetPosition.position, easeValue);
            transform.rotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(targetRotation), easeValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        screenShake.shakeObjectFalls();

        transform.position = targetPosition.position;
        transform.rotation = Quaternion.Euler(targetRotation);
        isMoving = false;
    }
}

