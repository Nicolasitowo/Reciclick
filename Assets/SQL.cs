
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SQL
{
    private string message;
    private MySqlConnection con;

    public SQL()
    {
        this.message = "";
        this.con = new MySqlConnection();
    }

    public void conectar()
    {
        string infocon = "Server= ulearnet-db.cmfamk37bb89.sa-east-1.rds.amazonaws.com; Database= ulearnet_reim_pilotaje; Uid= masterulearnet ; Pwd= Ulearnet2021.";
        this.con.ConnectionString = infocon;

        try{
            con.Open();
            message = "Conexion exitosa con base de datos";
            Debug.Log(message);
        }
        catch (Exception ex)
        {
            message = "No se ha podido establecer conexion con la base de datos";
            Debug.Log(message);
            Debug.Log(ex.ToString());
        }
    }

    public MySqlConnection getCon(){
        return con;
    }
}
