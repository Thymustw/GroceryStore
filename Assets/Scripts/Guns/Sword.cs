using UnityEngine;

public class Sword : Gun
{
    public float maxDistance = 1f;
    private Transform handlePos;
    protected override void Awake()
    {
        base.Awake();
        handlePos = transform.Find("SwordHandle");
    }


    private void DrawArc(Vector3 origin, Vector3 direction, float radius, float arcLength, Color color)
    {
        var matrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = color;

        Vector3 beginPoint = new Vector3(radius + origin.x, origin.y, origin.z);
        Vector3 firstPoint = new Vector3(radius + origin.x, origin.y, origin.z);
        var rad = Mathf.Deg2Rad * 90f;

        for (float t = 0; t < rad / 2; t += 0.01f)
        {
            float x = Mathf.Cos(t) * radius;
            float y = Mathf.Sin(t) * radius;

            Vector3 endPoint = new Vector3(x + origin.x, y + origin.y, origin.z);

            Gizmos.DrawLine(beginPoint, endPoint);
            Gizmos.DrawLine(beginPoint, endPoint);

            beginPoint = endPoint;

        }

        Gizmos.color = Color.black;
        Gizmos.DrawLine(firstPoint, origin);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(beginPoint, origin);
        Gizmos.matrix = matrix;
    }


    private void OnDrawGizmos() 
    {
        //Gizmos.DrawRay(transform.position, transform.right * maxDistance);
        DrawArc(handlePos.localPosition, Vector3.zero, maxDistance, 0, Color.blue);
    }
}