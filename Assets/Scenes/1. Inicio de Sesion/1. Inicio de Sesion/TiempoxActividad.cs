using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TiempoxActividad : MonoBehaviour
{
    public string inicio;
    public string final;
    public int causa;
    public int usuario_id;
    public int reim_id;
    public int actividad_id;

    private void Start()
    {
        inicio = tiempoActual();
    }

    public void salirActividad(int actividad) {
        this.actividad_id = actividad;
        this.final = tiempoActual();
        this.reim_id = 204;
        this.usuario_id = GameObject.Find("sesionInfo").GetComponent<InicioSesion>().id_usuario;
        this.causa = 0;
        StartCoroutine(add());
    }

    public void salirActividad()
    {
        int actividad = 1;

        switch (SceneManager.GetActiveScene().name) {

            case "Menu Principal":
                actividad = 1;
                break;
            case "Reducir Basura":
                actividad = 10001;
                break;
            case "Punto Limpio":
                actividad = 10006;
                break;
            case "Feria de las Pulgas(New)":
                actividad = 10004;
                break;
            case "Tu Tienda":
                actividad = 10003;
                break;
            case "BLOG":
                actividad = 10005;
                break;
        }

        this.actividad_id = actividad;
        this.final = tiempoActual();
        this.reim_id = 204;
        this.usuario_id = GameObject.Find("sesionInfo").GetComponent<InicioSesion>().id_usuario;
        this.causa = 0;
        StartCoroutine(add());
    }

    public string tiempoActual()
    {
        DateTime ahora = DateTime.Now;
        string fechaActual = ahora.ToString("yyyy-MM-dd HH:mm:ss");
        return fechaActual;
    }

    public IEnumerator add()
    {
        string URL = InicioSesion.api + "/tiempoxactividad/add";

        TiempoxActividadClass elemento = new TiempoxActividadClass();
        elemento.inicio = inicio;
        elemento.final = final;
        elemento.actividad_id = actividad_id;
        elemento.reim_id = reim_id;
        elemento.usuario_id = usuario_id;

        var json = JsonUtility.ToJson(elemento);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                if (www.isDone)
                {
                    Debug.Log(result);
                }
            }
        }
    }
}

[Serializable]
public class TiempoxActividadClass {

    public string inicio;
    public string final;
    public int causa;
    public int usuario_id;
    public int reim_id;
    public int actividad_id; 

}
