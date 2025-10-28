using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Dictionary <int, string> dialogos = new Dictionary <int, string>();
    private int dialogoActual = 1;
    TextMeshProUGUI Texto1;

    private void Awake()
    {
        dialogos.Add(1, "¡Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misión Personal, o AMP. Voy a estar guiándote en esta misión.");
        dialogos.Add(2, "Dado el estado actual del planeta Tierra, nuestro equipo se dedica a estudiar potenciales planetas donde la humanidad pueda vivir.");
        dialogos.Add(1, "Para ello enviamos al High Operating Pathfinder Entity, un satélite especializado en recolectar información. Lamentablemente no volvió en el tiempo previsto.");
        dialogos.Add(1, "Tu misión es encontrar al satélite perdido H.O.P.E. para que podamos acceder a toda la información que estuvo recolectando.");
        dialogos.Add(1, "Un par de consejos antes de que empieces con tu búsqueda. ");
        dialogos.Add(1, "¡Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misión Personal, o AMP. Voy a estar guiándote en esta misión.");
        dialogos.Add(1, "¡Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misión Personal, o AMP. Voy a estar guiándote en esta misión.");
        dialogos.Add(1, "¡Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misión Personal, o AMP. Voy a estar guiándote en esta misión.");

    }

}
