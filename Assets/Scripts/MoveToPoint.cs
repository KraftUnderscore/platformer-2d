using System.Collections;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float holdTime;

    private Vector2 endPoint;
    private Vector2 startPoint;
    private Transform objToMove;

    private bool goingToEndPoint;
    private bool onHold;

    private void Awake()
    {
        objToMove = transform.GetChild(0);
        endPoint = transform.GetChild(1).position;
        startPoint = objToMove.transform.position;
        goingToEndPoint = true;
        onHold = false;
    }

    void FixedUpdate()
    {
        if (onHold) return;

        if ((Vector2)objToMove.position == endPoint || (Vector2)objToMove.position == startPoint)
        {
            goingToEndPoint = !goingToEndPoint;
            StartCoroutine(holdTimer());
        }

        objToMove.position = Vector3.MoveTowards(objToMove.position, goingToEndPoint ? endPoint : startPoint, speed * Time.deltaTime);
    }

    private IEnumerator holdTimer()
    {
        onHold = true;
        yield return new WaitForSeconds(holdTime);
        onHold = false;
    }
}
