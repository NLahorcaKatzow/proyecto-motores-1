using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int numeroEscena;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator LoadLevel(int level)
    {
        animator.SetTrigger("End");

        // Espera hasta que SceneOut termine
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("SceneOut") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        );

        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel(numeroEscena));
        }
    }
}