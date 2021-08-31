using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuAlimentos : MonoBehaviour
{
    private const string V = "";
    public GameObject menuPrincipal;
    public FireBaseManager auth;

    [Header("Menus internos Listas")]    
    public GameObject MenuActual;
    public GameObject AlimentoSeleccionadoListas;
    public TMP_Text warningListText;
    public bool actR = false;
    public int selecc;

    [Header("Menus internos Alimentos")]
    public GameObject Alimentos;
    public GameObject MenuTiposAlimento;
    public GameObject AlimentoSeleccionado;
    public TMP_Text warngA;

    [Header("Logica")]
    public Controlador controlador;
    public string[] AlimentosC;
    public string[] PerfilAC;
    public string[] PerfilAF;
    public string[] PerfilAP;
    public string[] PerfilAV;
    public string[] PerfilAG;
    public string[] PerfilAL;
    public string[] PerfilAPe;  
    public string[] ListaAct;  
    public string[] ListaFav;  




    [Header("Prefabs")]
    public GameObject PrefabBotonAlimento;
    public GameObject botonColocacionA;
    public GameObject botonColocacionAL;

    [Header("Almacenes de datos Alimentos")]
    public Text Texto,TextoLA,TextoLF;
    public string Categoria="";
    public Image ImagenTex;
    public List<GameObject> Botones;




    // MENUS
    public void MainToAlimentos()
    {
        menuPrincipal.SetActive(false);
        Alimentos.SetActive(true);
        if (controlador.aUser != null)
        {
            this.auth.LoadPerfil(controlador.aUser.Perfil);
            
        }
        else this.auth.LoadPerfil("C1");
        this.warningListText.text = "";
    }

    public void MainToLista()
    {
        if (controlador.aUser != null)
        {
            menuPrincipal.SetActive(false);
            this.MenuActual.SetActive(false);
            this.warningListText.text = "";
            auth.LoadListasAct();
        }
        else this.warningListText.text = "Tienes que iniciar sesion";

    }
  
   public void ListatoMain()
    {
        this.menuPrincipal.SetActive(true);
        foreach (GameObject B in Botones)
        {
            Destroy(B);
        }
        Botones.Clear();
        this.MenuActual.SetActive(false);

    }

    public void AlimentoActL()
    {
        AlimentoSeleccionadoListas.SetActive(false);
        MenuActual.SetActive(true);
        if (actR)
        {
            foreach (GameObject B in Botones)
            {
                Destroy(B);
            }
            Botones.Clear();
            auth.LoadListasAct();
        }
        this.actR = false;
    }
    public void FavsActToSeleccion(Text nombre)
    {
        string[] p;
        p = nombre.text.Split('.');
        int i;
        i = int.Parse(p[0]); Debug.Log(i);
        this.TextoLA.text = this.ListaAct[i-1];
        this.selecc = i;
        MenuActual.SetActive(false);
        AlimentoSeleccionadoListas.SetActive(true);
    }
    
    public void AlimentosToTipos(int i)
    {
        Alimentos.SetActive(false);
        switch (i) {
            case 0:
                auth.loadComida(i);
                this.Categoria = "Carne";
                break;
            case 1:
                auth.loadComida(i);
                this.Categoria = "Pez";
                break;
            case 2:
                auth.loadComida(i);
                this.Categoria = "Frutas";
                break;
            case 3:
                auth.loadComida(i);
                this.Categoria = "LegumbresyVerduras";
                break;

            case 4:
                auth.loadComida(i);
                this.Categoria = "Grasas";
                break;

            case 5: 
                auth.loadComida(i);
                this.Categoria = "Lácteos";
                break;

            case 6:
                auth.loadComida(i);
                this.Categoria = "Pastelería";
                break;

        }
    }
    public void TiposToSeleccion(Text nombre )
    {
        MenuTiposAlimento.SetActive(false);
        Debug.Log(nombre.text);
        string[] p;
        int i;
        string Rec;
        switch (Categoria)
        {
            case "Carne":
                p = nombre.text.Split('.');
                i =  int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAC[i-1];

                if (Rec.Equals("0"))
                {this.Texto.text = p[1] + "."+"\nEste alimento es recomendable para su estado actual.";
                }else if (Rec.Equals("1"))
                {this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }else
                {this.Texto.text = p[1] + "." + "\nSe recomienda evitar comer este alimento."; }

                break;
            case "Frutas":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAF[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
            case "Grasas":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAG[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
            case "LegumbresyVerduras":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAV[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
            case "Lácteos":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAL[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
            case "Pez":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(this.PerfilAPe.Length);
                Debug.Log(i);
                Rec = this.PerfilAPe[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
            case "Pastelería":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAP[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = p[1] + "." + "\nEste alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = p[1] + "." + "\nSe recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = p[1] + "." + "\nSe recomeinda evitar comer este alimento."; }

                break;
        }


        Debug.Log(this.Texto.text);
        AlimentoSeleccionado.SetActive(true);
    }
    public void SeleccionToTipos()
    {
        MenuTiposAlimento.SetActive(true);
        AlimentoSeleccionado.SetActive(false);
        this.warngA.text = "";
    }
    public void TiposToAlimentos()
    {
        Alimentos.SetActive(true);
        foreach (GameObject B in Botones)
        {
            Destroy(B);
        }
        Botones.Clear();
        MenuTiposAlimento.SetActive(false);
    }
    public void AlimentosTOMain()
    {
        this.warningListText.text = "";
        controlador.AlimentoToMain();
    }
    public void AlimentosTOAlmMain()
    {
        menuPrincipal.SetActive(true);
        Alimentos.SetActive(false);
        this.warningListText.text = "";
    }

    public void BotonAct()
    {
        if (this.controlador.aUser == null)
        {
            warngA.text = "Tienes que estar logeado";
        }
        else
        {

            this.auth.AddListasA(this.Texto.text);
        }
    }
    public void BotonRev()
    {
        if (!this.actR)
        {
            int A = this.selecc - 1;
            this.auth.RevLista(A.ToString());
            this.actR = true;
        }
        
    }

    public void AlmentosLista()
    {
        this.AlimentoSeleccionado.SetActive(false);

    }


    //DATOSLOAD
    public void PreparacionTipos()
    {
        MenuTiposAlimento.SetActive(true);
        int length = this.AlimentosC.Length;
        int p = 1;
        for (int a = 0; a < length; a++)
        {
            GameObject myButton = Instantiate(PrefabBotonAlimento,
                botonColocacionA.transform.position, botonColocacionA.transform.rotation) as GameObject;
            myButton.transform.SetParent(botonColocacionA.transform);
            string[] sep = AlimentosC[a].Split(',');
            myButton.GetComponentInChildren<Text>().text = p+"."+sep[0];
            myButton.GetComponent<Button>().onClick.AddListener(delegate ()
            { TiposToSeleccion(myButton.GetComponentInChildren<Text>()); });

            Botones.Add(myButton);
            p++;
        }
    }

    public void PreparacionListasAct()
    {
        MenuActual.SetActive(true);
        int length = this.ListaAct.Length;
        int p = 1;
        for (int a = 0; a < length; a++)
        {
            GameObject myButton = Instantiate(PrefabBotonAlimento,
                botonColocacionAL.transform.position, botonColocacionAL.transform.rotation) as GameObject;
            myButton.transform.SetParent(botonColocacionAL.transform);
            string[] sep = ListaAct[a].Split('.');
            myButton.GetComponentInChildren<Text>().text = p + "." + sep[0];
            myButton.GetComponent<Button>().onClick.AddListener(delegate ()
            { FavsActToSeleccion(myButton.GetComponentInChildren<Text>()); });

            Botones.Add(myButton);
            p++;
        }
    }


}
