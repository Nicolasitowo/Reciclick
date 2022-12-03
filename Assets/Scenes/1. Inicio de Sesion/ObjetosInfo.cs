using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjetosInfo : MonoBehaviour
{
    public int Objeto1;
    public int Objeto2;
    public int Objeto3;
    public int Objeto4;
    public int Objeto5;
    public int Objeto6;


    public int obtenerCant(int id)
    {
        switch (id) {
            case 1:
                return Objeto1;
            case 2:
                return Objeto2;
            case 3:
                return Objeto3;
            case 4:
                return Objeto4;
            case 5:
                return Objeto5;
            case 6:
                return Objeto6;
            case 290616:
                return Objeto1;
            case 290617:
                return Objeto2;
            case 290618:
                return Objeto3;
            case 290621:
                return Objeto4;
            case 290622:
                return Objeto5;
            case 290623:
                return Objeto6;
        }
        return 0;
    }
    public void modificarCant(int id, int nuevacantidad)
    {
        switch (id)
        {
            case 1:
                Objeto1 = nuevacantidad;
                break;
            case 2:
                Objeto2 = nuevacantidad;
                break;
            case 3:
                Objeto3 = nuevacantidad;
                break;
            case 4:
                Objeto4 = nuevacantidad;
                break;
            case 5:
                Objeto5 = nuevacantidad;
                break;
            case 6:
                Objeto6 = nuevacantidad;
                break;

            case 290616:
                Objeto1 = nuevacantidad;
                break;
            case 290617:
                Objeto2= nuevacantidad;
                break;
            case 290618:
                Objeto3 = nuevacantidad;
                break;
            case 290621:
                Objeto4 = nuevacantidad;
                break;
            case 290622:
                Objeto5 = nuevacantidad;
                break;
            case 290623:
                Objeto6 = nuevacantidad;
                break;
        }
    }
    public void sumar(int id, int nuevacantidad)
    {
        switch (id)
        {
            case 1:
                Objeto1 += nuevacantidad;
                break;
            case 2:
                Objeto2 += nuevacantidad;
                break;
            case 3:
                Objeto3 += nuevacantidad;
                break;
            case 4:
                Objeto4 += nuevacantidad;
                break;
            case 5:
                Objeto5 += nuevacantidad;
                break;
            case 6:
                Objeto6 += nuevacantidad;
                break;

            case 290616:
                Objeto1 += nuevacantidad;
                break;
            case 290617:
                Objeto2 += nuevacantidad;
                break;
            case 290618:
                Objeto3 += nuevacantidad;
                break;
            case 290621:
                Objeto4 += nuevacantidad;
                break;
            case 290622:
                Objeto5 += nuevacantidad;
                break;
            case 290623:
                Objeto6 += nuevacantidad;
                break;
        }
    }
}
