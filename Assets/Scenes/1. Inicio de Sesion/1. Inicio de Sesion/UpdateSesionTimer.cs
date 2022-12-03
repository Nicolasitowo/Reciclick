using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateSesionTimer : MonoBehaviour
{

    public float timeRemainingGenerate;
    public int tiempoEntero;

    void Start()
    {
        timeRemainingGenerate = 10f;
    }

    void Update()
    {
        tiempoEntero = (int)timeRemainingGenerate;

        if (tiempoEntero > 0)
        {
            timeRemainingGenerate -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(ActualizarSesion());
            timeRemainingGenerate = 10f;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public IEnumerator ActualizarSesion()
    {
        if (GameObject.Find("sesionInfo").GetComponent<InicioSesion>().idSesion == "")
        {
            yield return new WaitForSeconds(0f);
            Debug.Log("no hay sesion aun");
        }
        else
        {
            string URL = InicioSesion.api + "/asigna_reim_alumno/termino";

            Sesion sesion = new Sesion();
            sesion.datetime_termino = tiempoActual();
            sesion.sesion_id = GameObject.Find("sesionInfo").GetComponent<InicioSesion>().idSesion;

            var json = JsonUtility.ToJson(sesion);

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
                        Debug.Log("Tiempo Sesion Actualizado");
                    }
                }
            }
        }
    }


    public string tiempoActual()
    {
        DateTime ahora = DateTime.Now;
        string fechaActual = ahora.ToString("yyyy-MM-dd HH:mm:ss");
        return fechaActual;
    }
}
