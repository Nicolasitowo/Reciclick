using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cargando : MonoBehaviour
{
    
    void Start()
    {
        string nivel = CargarNivel.siguienteNivel;
        StartCoroutine(cargarNivel(nivel));
    }

    IEnumerator cargarNivel(string nivel) {

        yield return new WaitForSeconds(0.7f);

        SceneManager.LoadScene(nivel);
        
       
    }

    
}
