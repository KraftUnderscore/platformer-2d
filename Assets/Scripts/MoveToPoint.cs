using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 endPoint;
    private Vector2 startPoint;
    private Transform objToMove;

    private bool goingToEndPoint;

    private void Awake()
    {
        objToMove = transform.GetChild(0);
        endPoint = transform.GetChild(1).position;
        startPoint = objToMove.transform.position;
        goingToEndPoint = true;
    }

    void FixedUpdate()
    {
        if ((Vector2)objToMove.position == endPoint || (Vector2)objToMove.position == startPoint) goingToEndPoint = !goingToEndPoint;
        objToMove.position = Vector3.MoveTowards(objToMove.position, goingToEndPoint ? endPoint : startPoint, speed * Time.deltaTime);
    }
}
