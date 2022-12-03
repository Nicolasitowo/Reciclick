using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sesion
{
    public string sesion_id;
    public int usuario_id;
    public string periodo_id;
    public int reim_id;
    public string datetime_inicio;
    public string datetime_termino;

    public Sesion (int usuario_id){
        this.reim_id = 204;
        this.periodo_id = "202201";
        this.usuario_id = usuario_id;
        generarCodigoSesion();

    }

    public Sesion()
    {
    }
    public void generarCodigoSesion(){
        datetime_inicio = tiempoActual();
        datetime_termino = tiempoActual();
        sesion_id = usuario_id+"-"+reim_id + "-" + datetime_inicio;

    }
    public string tiempoActual() {
        DateTime ahora = DateTime.Now;
        string fechaActual = ahora.ToString("yyyy-MM-dd HH:mm:ss");
        return fechaActual;
    }
}
