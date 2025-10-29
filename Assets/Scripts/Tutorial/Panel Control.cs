using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PanelControl : MonoBehaviour
{
    public GameObject tutorial; 
    public Button skipTutorial; 
    

    void CerrarTutorial()
    {
        tutorial.SetActive(false);
    }

   
  

}
