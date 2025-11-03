using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Dictionary<int, string> dialogos = new Dictionary<int, string>();
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
        dialogos.Add(1, "�Hola! Bienvenido al planeta SR1078. Yo soy tu Asistente de Misi�n Personal, o AMP. Voy a estar gui�ndote en esta misi�n.");
        dialogos.Add(2, "Dado el estado actual del planeta Tierra, nuestro equipo se dedica a estudiar potenciales planetas donde la humanidad pueda vivir.");
        dialogos.Add(3, "Para ello enviamos al High Operating Pathfinder Entity, un sat�lite especializado en recolectar informaci�n. Lamentablemente no volvi� en el tiempo previsto.");
        dialogos.Add(4, "Tu misi�n es encontrar al sat�lite perdido H.O.P.E. para que podamos acceder a toda la informaci�n que estuvo recolectando.");
        dialogos.Add(5, "Un par de consejos antes de que empieces con tu b�squeda. Usa las flechas (o las letras AWSD) de tu panel de control para moverte por el espacio.");
        dialogos.Add(6, "Usa la barra espaciadora para saltar o esquivar.");
        dialogos.Add(7, "Puede que encuentres criaturas nativas del planeta. En el caso de que te ataquen, hemos instalado un equipo de defensa que puedes activar con el click izquierdo.");
        dialogos.Add(8, "Pero ten cuidado, solo tienes 3 vidas. Solo recolectando la energ�a de las criaturas que derrotes podr�s recuperar las vidas perdidas.");
        dialogos.Add(9, "Espero haber sido de ayuda. Ya puedes empezar con tu misi�n. El destino de la humanidad est� en tus manos.");

    }
    public void LeerDialogo()
    {
        Texto1.text = dialogos[dialogoActual];
        if (dialogoActual == dialogos.Count)
        {
            Debug.Log("Fin del diálogo");
            SceneManager.Instance.LoadNextLevel();
            return;

        }
        dialogoActual++;

    }
    
    private void OnDisable()
    {
        SceneManager.Instance.LoadNextLevel();
    }
}
