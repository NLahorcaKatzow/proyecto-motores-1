using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _oldposition;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_animator != null)
            Debug.LogError("No se encuentra");
        _oldposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _newposition = transform.position;
        if (_oldposition != _newposition)
        {
            _animator.SetFloat("Speed", 1);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
        _oldposition = _newposition;
        _animator.gameObject.transform.localPosition = Vector3.zero ;
    }
}
