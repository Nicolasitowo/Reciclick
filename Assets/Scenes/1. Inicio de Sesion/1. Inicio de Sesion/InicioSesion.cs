using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InicioSesion : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static string api = "https://7tv5uzrpoj.execute-api.sa-east-1.amazonaws.com/prod/api";
    //public static string api = "http://localhost:3002/api";
    public string id;
    public int id_reim;
    public int id_usuario;
    public string avatar;

    public string idSesion;
    
    public int puntos;

    public Text error;
    public InputField userInput;
    public InputField passwordInput;
    

    public void ingresar() {
        UsuarioClass user = new UsuarioClass();
        user.loginame = userInput.text;
        user.password = passwordInput.text;
        if (user.loginame == "" || user.password == "")
        {
            Debug.Log("Porfavor rellene todos los campos");
            error.text = "Porfavor rellene todos los campos";
        }
        else {
            StartCoroutine(BuscaUsuario(user));
        }
        
    }
    
    IEnumerator BuscaUsuario(UsuarioClass user) {
        string URL = InicioSesion.api + "/login/204";
        var json = JsonUtility.ToJson(user);
        using (UnityWebRequest www = UnityWebRequest.Post(URL,json)) {
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
                    
                    if (result == "null")
                    {
                        Debug.Log("Error 404 Datos incorrectos");
                        error.text = "Datos incorrectos";
                    }
                    else {

                        Debug.Log("Usuario encontrado");
                        Debug.Log(result);
                        var usuarioJson = JsonUtility.FromJson<UsuarioClass>(result);
                        Debug.Log(usuarioJson.id);
                        Sesion sesion = new Sesion(usuarioJson.id);
                        idSesion = sesion.sesion_id;
                        StartCoroutine(CreaSesion(sesion));
                    }
                }

            }
        }
    }
    
    IEnumerator CreaSesion(Sesion sesion)
    {
        string URL = InicioSesion.api + "/asigna_reim_alumno/add";
        var json = JsonUtility.ToJson(sesion);
        using (UnityWebRequest www = UnityWebRequest.Post(URL,json)) {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                if (www.isDone)
                {
                    DetalleUsuario detalle = new DetalleUsuario();
                    detalle.id_reim = sesion.reim_id;
                    detalle.id_usuario = sesion.usuario_id;
                    StartCoroutine(CargaDetalles(detalle));
                }
            }
        }
    }

    IEnumerator CreaDetalles(DetalleUsuario info)
    {
        string URL = InicioSesion.api + "/detalle_usuario/add";
        DetalleUsuario detalle = new DetalleUsuario();

        detalle.opciones_inicio = crearVacio();
        detalle.id_reim = 204;
        detalle.identificador_personal = ""+info.id_usuario;
        detalle.id_usuario = info.id_usuario;

        var json = JsonUtility.ToJson(detalle);

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
                    Debug.Log("Crea Detalle: "+result);
                    StartCoroutine(CargaDetalles(info));
                    cargarMenuPrincipal();
                }
            }
        }
    }


    IEnumerator CargaDetalles(DetalleUsuario detalle)
    {
        string URL = InicioSesion.api + "/detalle_usuario/get";
        var json = JsonUtility.ToJson(detalle);
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
                    
                    if (result == "false")
                    {
                        StartCoroutine(CreaDetalles(detalle));
                        Debug.Log(result);
                    }
                    else {
                        
                        Debug.Log("holi");
                        Debug.Log(result);
                        var detallenotJson = JsonUtility.FromJson<DetalleUsuario>(result);
                        cargarDetalles(detallenotJson);
                    }
                    
                }
            }
        }
    }
    
    void cargarMenuPrincipal() {
         CargarNivel cargar = new CargarNivel();
         cargar.Cargando("Menu Principal");
    }

    void cargarDetalles(DetalleUsuario detalle) {

        
        id_reim = detalle.id_reim;
        id_usuario = detalle.id_usuario;
        avatar = detalle.identificador_personal;

        string[] splitguion = detalle.opciones_inicio.Split(new char[] { '-' });
        puntos = int.Parse(splitguion[0].Remove(0, 3));

        string[] materiales = splitguion[1].Split(new char[] { ';' });
        GameObject.Find("sesionInfo").GetComponent<materialesInfo>().plastico = int.Parse(materiales[0].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<materialesInfo>().latas = int.Parse(materiales[1].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<materialesInfo>().papel = int.Parse(materiales[2].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<materialesInfo>().carton = int.Parse(materiales[3].Remove(0, 3));

        string[] objetos = splitguion[2].Split(new char[] { ';' });

        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto1 = int.Parse(objetos[0].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto2 = int.Parse(objetos[1].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto3 = int.Parse(objetos[2].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto4 = int.Parse(objetos[3].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto5 = int.Parse(objetos[4].Remove(0, 3));
        GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto6 = int.Parse(objetos[5].Remove(0, 3));

        cargarMenuPrincipal();
    }

    public void guardarDetalles() {

        DetalleUsuario detalle = new DetalleUsuario();

        int plastico = GameObject.Find("sesionInfo").GetComponent<materialesInfo>().plastico;
        int latas = GameObject.Find("sesionInfo").GetComponent<materialesInfo>().latas;
        int papel = GameObject.Find("sesionInfo").GetComponent<materialesInfo>().papel;
        int carton = GameObject.Find("sesionInfo").GetComponent<materialesInfo>().carton;


        int objeto1 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto1;
        int objeto2 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto2;
        int objeto3 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto3;
        int objeto4 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto4;
        int objeto5 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto5;
        int objeto6 = GameObject.Find("sesionInfo").GetComponent<ObjetosInfo>().Objeto6;


        detalle.opciones_inicio = "DI="+puntos+"-PL="+plastico+";LA="+latas+";PA="+papel+";CA="+carton+"-O1="+objeto1+";O2="+objeto2+";O3="+objeto3+";O4="+objeto4+";O5="+objeto5+";O6="+objeto6;

        detalle.id_reim = id_reim;
        detalle.id_usuario = id_usuario;
        detalle.identificador_personal = avatar;
        StartCoroutine(guardaDetalles(detalle));

    }

    public string crearVacio() {
        
        string opciones_inicio = "DI=0-PL=0;LA=0;PA=0;CA=0-O1=0;O2=0;O3=0;O4=0;O5=0;O6=0";
        return opciones_inicio;
    }

    public IEnumerator obtenerUltimo() {
        ElementoCatalogo elemento = new ElementoCatalogo();
        string URL = InicioSesion.api + "/detalle_usuario/getLast";
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

    IEnumerator guardaDetalles(DetalleUsuario detalle) {
        string URL = InicioSesion.api + "/detalle_usuario/update";
        var json = JsonUtility.ToJson(detalle);
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
            else {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                if (www.isDone)
                {
                    Debug.Log("Detalle Usuario: "+result);
                }
            }
        }
    }

    public void sumarPuntos(int cant) {
        puntos += cant;
    }
}
