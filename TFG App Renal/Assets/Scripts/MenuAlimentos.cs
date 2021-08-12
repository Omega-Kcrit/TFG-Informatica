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
    public GameObject MenuListas;
    public GameObject MenuFav;
    public GameObject MenuActual;
    public TMP_Text warningListText; 

    [Header("Menus internos Alimentos")]
    public GameObject Alimentos;
    public GameObject MenuTiposAlimento;
    public GameObject AlimentoSeleccionado;

    [Header("Logica")]
    public Controlador controlador;
    public string[] AlimentosC;
    /*public string[] AlimentosF;
    public string[] AlimentosP;
    public string[] AlimentosV;
    public string[] AlimentosG;
    public string[] AlimentosL;*/
    public string[] PerfilAC;
    public string[] PerfilAF;
    public string[] PerfilAP;
    public string[] PerfilAV;
    public string[] PerfilAG;
    public string[] PerfilAL;
    public string[] PerfilAPe;



    [Header("Prefabs")]
    public GameObject PrefabBotonAlimento;
    public GameObject botonColocacionA;

    [Header("Almacenes de datos Alimentos")]
    public Text Texto;
    public string Categoria="";
    public Image ImagenTex;
    public Image[] Imagenecarne;
    public string[] textoCarne;
    public List<GameObject> Botones;




    // MENUS
    public void MainToAlimentos()
    {
        menuPrincipal.SetActive(false);
        Alimentos.SetActive(true);
        if (controlador.aUser != null)
        {
            this.auth.loadPerfil(controlador.aUser.Perfil);
            
        }
        else this.auth.loadPerfil("C1");
    }

    public void MainToLista()
    {
        if (controlador.aUser != null)
        {
            this.auth.loadPerfil(controlador.aUser.Perfil);
            menuPrincipal.SetActive(false);
            Alimentos.SetActive(true);

        }
        else this.warningListText.text = "Tienes que iniciar sesion";

    }

    public void ListaToActual()
    {
        if (controlador.aUser != null)
        {
            this.auth.loadPerfil(controlador.aUser.Perfil);
            menuPrincipal.SetActive(false);
            Alimentos.SetActive(true);

        }
        else this.warningListText.text = "Tienes que iniciar sesion";

    }

    public void ListaToFavs()
    {
        if (controlador.aUser != null)
        {
            this.auth.loadPerfil(controlador.aUser.Perfil);
            menuPrincipal.SetActive(false);
            Alimentos.SetActive(true);

        }
        else this.warningListText.text = "Tienes que iniciar sesion";

    }

    public void FavsActToListas()
    {
        
        MenuFav.SetActive(false);
        MenuActual.SetActive(false);
        MenuListas.SetActive(true);
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
                {this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }else if (Rec.Equals("1"))
                {this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }else
                {this.Texto.text = "Se recomienda evitar comer este alimento.";}

                break;
            case "Frutas":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAF[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
            case "Grasas":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAG[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
            case "LegumbresyVerduras":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAV[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
            case "Lácteos":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAL[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
            case "Pez":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(this.PerfilAPe.Length);
                Debug.Log(i);
                Rec = this.PerfilAPe[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
            case "Pastelería":
                p = nombre.text.Split('.');
                i = int.Parse(p[0]);
                Debug.Log(i);
                Rec = this.PerfilAP[i-1];

                if (Rec.Equals("0"))
                {
                    this.Texto.text = "Este alimento es recomendable para su estado actual.";
                }
                else if (Rec.Equals("1"))
                {
                    this.Texto.text = "Se recomienda comer este alimento con moderacion y en cantidades controladas. ";
                }
                else
                { this.Texto.text = "Se recomeinda evitar comer este alimento."; }

                break;
        }



        AlimentoSeleccionado.SetActive(true);
    }
    public void SeleccionToTipos()
    {
        MenuTiposAlimento.SetActive(true);
        AlimentoSeleccionado.SetActive(false);
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
        controlador.AlimentoToMain();
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

    public void PreparacionListas()
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
            myButton.GetComponentInChildren<Text>().text = p + "." + sep[0];
            myButton.GetComponent<Button>().onClick.AddListener(delegate ()
            { TiposToSeleccion(myButton.GetComponentInChildren<Text>()); });

            Botones.Add(myButton);
            p++;
        }
    }

}
