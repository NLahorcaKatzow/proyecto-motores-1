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
    public int GetLife()
    {
       return life;
    }
    public void Die()
    {
        if (life <=0)
        {
           
        }
    }

}
