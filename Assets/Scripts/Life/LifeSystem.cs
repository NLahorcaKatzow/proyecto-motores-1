using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private int life;

    void Start()
    {
        life = 3;
    }

    
    public void LoseLife()
    {
        life--;
    }

    public void WinLife()
    {
        life++;
    }

}
