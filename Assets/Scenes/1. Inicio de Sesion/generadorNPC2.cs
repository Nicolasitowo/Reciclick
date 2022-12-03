using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generadorNPC2 : MonoBehaviour
{
    public float timeRemainingGenerate;
    public GameObject NPCinfo;
    public GameObject ObjetosInfo;
    public int tiempoEntero;

    public bool isGenerated;
    void Start()
    {
        isGenerated = false;
        timeRemainingGenerate = Random.Range(30, 50);
    }
    
    void Update()
    {
        tiempoEntero = (int) timeRemainingGenerate;

        if (tiempoEntero > 0)
        {
            timeRemainingGenerate -= Time.deltaTime;
        }
        else{
            tiempoEntero = 100;

            if (generarNPC()) {
                isGenerated = true;
                this.gameObject.GetComponent<generadorNPC2>().enabled = false;
            }
            
            
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool generarNPC()
    {
        int npcSeed = Random.Range(1, 6);


        TextAsset infonpc = Resources.Load<TextAsset>("NPCPrefabs");
        Sprite foto = Resources.Load<Sprite>("IMG/ImagenesTT/NPC/Npc" + npcSeed + "/1");
        string[] linea = infonpc.text.Split(new char[] { '\n' });
        string[] cosa = linea[npcSeed].Split(new char[] { ';' });

        

        string nombre = cosa[1];
        string dialogo = "";

        switch (npcSeed)
        {
            case 1:
                dialogo = "Llevaré estos juguetes, mi hija esta de cumpleaños hoy:";
                break;
            case 2:
                nombre = "Niño";
                dialogo = "Mi mamá me dio dinero, quiero llevar estos juguetes:";
                break;
            case 3:
                dialogo = "Estaba buscando un regalo para mi nieto, deseo llevar estos juguetes: ";
                break;
            case 4:
                nombre = "Niña";
                dialogo = "Deseo llevar este juguete por favor:";
                break;
            case 5:
                dialogo = "Mi hermano adoraba estos juguetes cuando era más joven: ";
                break;
            case 6:
                dialogo = "Estaba buscando un regalo para mis sobrinos, llevaré esto: ";
                break;
        }

        Instantiate(NPCinfo);
        Instantiate(ObjetosInfo);
        GameObject.Find("NPCInfo(Clone)").GetComponent<NPCInfo>().personaje = nombre;
        GameObject.Find("NPCInfo(Clone)").GetComponent<NPCInfo>().dialogo = dialogo;
        GameObject.Find("NPCInfo(Clone)").GetComponent<NPCInfo>().foto = foto;

        return true;

    }
}
