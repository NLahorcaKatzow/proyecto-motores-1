using UnityEngine;
using DG.Tweening;
public class CharacterRotationController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public LayerMask floorMask;
    public Vector3 target;

    void Update()
    {
        Turn();
    }
    
    void Turn()
    {
        //target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, floorMask))
        {
            target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        
        
        transform.LookAt(target);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.1f);
    }
    
}