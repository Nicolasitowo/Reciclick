using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class materialesInfo : MonoBehaviour
{
    public int plastico;
    public int carton;
    public int papel;
    public int latas;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Reducir Basura") {
            GameObject.Find("PanelMateriales").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = latas + "";
            GameObject.Find("PanelMateriales").transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = plastico + "";
            GameObject.Find("PanelMateriales").transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = papel + "";
            GameObject.Find("PanelMateriales").transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = carton + "";
        }

        else if (SceneManager.GetActiveScene().name == "Punto Limpio")
        {
            GameObject.Find("MaterialesInventario").transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = carton + "";
            GameObject.Find("MaterialesInventario").transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = latas + "";
            GameObject.Find("MaterialesInventario").transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = plastico + "";
            GameObject.Find("MaterialesInventario").transform.GetChild(2).transform.GetChild(3).GetComponent<Text>().text = papel + "";
        }
    }
}
