using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuadrito : MonoBehaviour
{
 
    public bool collisionAbajo = false;
    public int contador = 0;
    public GameObject objeto;
    public GameObject basura;

    void Update() {
        if (collisionAbajo) {
            objeto.GetComponent<scriptsBasura>().reducir();
            basura.GetComponent<Animator>().SetTrigger("hit");
            Debug.Log("reduce");

            collisionAbajo = false;
            contador = 0;
            GameObject.Find("respuestaActividad").GetComponent<respuestaActividadScripts2>().setDatos("10001;290761;1");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        string hola = other.name;
        Debug.Log(hola);
        if (hola == "Bottom") {
            Debug.Log('b');
            collisionAbajo = true;
        }
    }


}
