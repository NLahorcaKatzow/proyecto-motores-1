using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Dictionary <int, string> dialogos = new Dictionary <int, string>();
    private int dialogoActual = 1;
    public TextMeshProUGUI Texto1;
    public GameObject Tutorial; 
    public Button Continuar;

    private void Start()
    {
        Texto1.text = dialogos[dialogoActual];
    }
    private void Awake()
    {
        dialogos.Add(1, "¡Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misión Personal, o AMP. Voy a estar guiándote en esta misión.");
        dialogos.Add(2, "Dado el estado actual del planeta Tierra, nuestro equipo se dedica a estudiar potenciales planetas donde la humanidad pueda vivir.");
        dialogos.Add(3, "Para ello enviamos al High Operating Pathfinder Entity, un satélite especializado en recolectar información. Lamentablemente no volvió en el tiempo previsto.");
        dialogos.Add(4, "Tu misión es encontrar al satélite perdido H.O.P.E. para que podamos acceder a toda la información que estuvo recolectando.");
        dialogos.Add(5, "Un par de consejos antes de que empieces con tu búsqueda. Usa las flechas (o las letras AWSD) de tu panel de control para moverte por el espacio.");
        dialogos.Add(6, "Usa la barra espaciadora para saltar o esquivar.");
        dialogos.Add(7, "Puede que encuentres criaturas nativas del planeta. En el caso de que te ataquen, hemos instalado un equipo de defensa que puedes activar con el click izquierdo.");
        dialogos.Add(8, "Pero ten cuidado, solo tienes 3 vidas. Solo recolectando la energía de las criaturas que derrotes podrás recuperar las vidas perdidas.");
        dialogos.Add(9, "Espero haber sido de ayuda. Ya puedes empezar con tu misión. El destino de la humanidad está en tus manos.");

    }
   public void LeerDialogo()
    {
        Texto1.text = dialogos[dialogoActual];
        if (dialogoActual==9)
        {
            Debug.Log("Fin del diálogo");
            return;
            
        }
        dialogoActual++;

    }
     
   
    
}
