using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovilPlataform : MonoBehaviour
{
    Vector3 start;
    public Vector3 end;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(start, end + start, (Mathf.Sin(speed * Time.time) + 1f) / 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (Application.isPlaying) Gizmos.DrawLine(start, end + start);
        else Gizmos.DrawLine(transform.position, end + transform.position);
    }
}
