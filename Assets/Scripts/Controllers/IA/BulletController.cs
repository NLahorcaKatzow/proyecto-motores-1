using DG.Tweening;
using UnityEngine;
public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 10f;
    public Ease ease = Ease.Linear;
    void Start()
    {
        transform.DOMove(transform.position + transform.forward * speed, lifetime).SetEase(ease).OnComplete(() => Dst());
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthController.Instance.TakeDamage(damage);
            Dst();
        }
    }
    
    private void Dst()
    {
    transform.DOScale(0, 0.5f).SetEase(Ease.InOutSine);
        transform.DOPunchScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => Destroy(gameObject));
        
    }
}