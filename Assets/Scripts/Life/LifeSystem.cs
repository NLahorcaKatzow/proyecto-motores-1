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
        if (life <= 0) Die();
    }

    public void AddLife()
    {
        if (life == 3) return;
          
        life++;
    }
    public int GetLife()
    {
       return life;
    }
    public void Die()
    {
        //To do Scene Manager Muerte
      
    }

}
