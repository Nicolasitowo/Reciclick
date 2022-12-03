using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class respuestaActividadScripts2 : MonoBehaviour
{

    public void setDatos(string info)
    { 
            alumnoRespuestaActividad accion = new alumnoRespuestaActividad();
            accion.Eje_X = GameObject.Find("respuestaActividad").GetComponent<touchPosition>().x;
            accion.Eje_Y = GameObject.Find("respuestaActividad").GetComponent<touchPosition>().y;
            accion.Eje_Z = 0;
            string[] linea = info.Split(new char[] { ';' });
            accion.id_actividad = int.Parse(linea[0]);
            accion.id_elemento = int.Parse(linea[1]);

            accion.correcta = int.Parse(linea[2]);

            if (accion.correcta == 1) {
                accion.resultado = "Es un Boton";
            }
            else if (accion.correcta == 2)
            {
                accion.resultado = "Es un Toque Falso";
            }
            else if (accion.correcta == 0)
            {
                accion.resultado = "No se pudo realizar";
            }

            if (linea.Length == 4){
                accion.resultado = linea[3];
            }


            accion.id_per = 202102;
            accion.id_user = GameObject.Find("sesionInfo").GetComponent<InicioSesion>().id_usuario;
            accion.id_reim = GameObject.Find("sesionInfo").GetComponent<InicioSesion>().id_reim;
            accion.datetime_touch = tiempoActual();
            accion.Tipo_Registro = "0";
            

            StartCoroutine(putDatos(accion));
        
    }

    public IEnumerator putDatos(alumnoRespuestaActividad accion)
    {
        string URL = InicioSesion.api + "/alumno_respuesta/add2";

        var json = JsonUtility.ToJson(accion);

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
                    Debug.Log("Accion Registrada");
                }
            }
        }
    }

    public string tiempoActual()
    {
        DateTime ahora = DateTime.Now;
        string fechaActual = ahora.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        return fechaActual;
    }

}
