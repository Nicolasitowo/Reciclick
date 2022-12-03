using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarNivel : MonoBehaviour
{

    public static string siguienteNivel;

    public void Cargando(string nivel) {
        siguienteNivel = nivel;
        SceneManager.LoadScene("Cargando");
    }
}
