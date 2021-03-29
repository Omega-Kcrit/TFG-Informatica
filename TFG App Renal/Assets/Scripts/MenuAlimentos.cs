using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAlimentos : MonoBehaviour
{
    private const string V = "";
    public GameObject menuPrincipal;
    [Header("Menus internos Dietas")]
    
    public GameObject Dietas;
    public GameObject DiaDieta;
    public GameObject PlatosDieta; 
    public GameObject ElaboracionDieta;

    [Header("Menus internos Alimentos")]
    public GameObject Alimentos;
    public GameObject MenuTiposAlimento;
    public GameObject AlimentoSeleccionado;

    [Header("Logica")]
    public Controlador controlador;
    private string[,] AlimentosCarne= new string[24,11] {//24,11
                                      {"1.Pollastre","2.Gall d’indi","3.Conill","4.Llom de porc","5.Vedella","6.Xai",
                                        "7.Bou","8.Vísceres","9.Carns elaborades en conserva","10.Canelons",
                                        "11.Carns empanades i arrebossades"
                                      },

                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"} };
    private string[,] AlimentosPescado = new string[24, 11] {//24,11
                                      {"Pollastre","Gall d’indi","Conill","Llom de porc","Vedella","Xai",
                                        "Bou","Vísceres","Carns elaborades en conserva","Canelons","Carns empanades i arrebossades"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"} };
    private string[,] AlimentosVerduras = new string[24, 11]{//24,11
                                      {"Pollastre","Gall d’indi","Conill","Llom de porc","Vedella","Xai",
                                        "Bou","Vísceres","Carns elaborades en conserva","Canelons","Carns empanades i arrebossades"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"} };
    private string[,] AlimentosFrutas = new string[24, 11]{//24,11
                                      {"Pollastre","Gall d’indi","Conill","Llom de porc","Vedella","Xai",
                                        "Bou","Vísceres","Carns elaborades en conserva","Canelons","Carns empanades i arrebossades"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"},
                                      {"1","2","3","4","5","6","7","8","9","10","11"} };
    


    [Header("Prefabs")]
    public GameObject PrefabBotonAlimento;
    public GameObject botonColocacionA;

    [Header("Almacenes de datos Alimentos")]
    public Text Texto;
    public Image ImagenTex;
    public Image[] Imagenecarne;
    public string[] textoCarne;
    public List<GameObject> Botones;




    // MENUS
    public void MainToAlimentos()
    {
        menuPrincipal.SetActive(false);
        Alimentos.SetActive(true);
    }
    public void AlimentosToTipos(int i)
    {
        Alimentos.SetActive(false);
        switch (i) {
            case 0:
                // TODO LOAD ALIMENTOS CARNE
               
                MenuTiposAlimento.SetActive(true);

                int P = 0;
                int length = AlimentosCarne.GetLength(1);
                if (controlador.aUser != null)
                {
                    string []p = controlador.aUser.Perfil.Split('C');
                    
                    P= int.Parse(p[1]);

                    //Debug.Log();

                }

                Debug.Log("TIpo de usuarip: "+P);
                for (int a = 0; a < length; a++)
                {
                    GameObject myButton = Instantiate(PrefabBotonAlimento,
                        botonColocacionA.transform.position, botonColocacionA.transform.rotation) as GameObject;
                    myButton.transform.SetParent(botonColocacionA.transform);

                    myButton.GetComponentInChildren<Text>().text = AlimentosCarne[P,a];
                    myButton.GetComponent<Button>().onClick.AddListener(delegate() 
                        { TiposToSeleccion(myButton.GetComponentInChildren<Text>()); });

                    Botones.Add(myButton);
                }
                break;
            case 1:
                // TODO LOAD ALIMENTOS Pesaco
                break;
            case 2:
                // TODO LOAD ALIMENTOS Frutas
                break;
            case 3:
                // TODO LOAD ALIMENTOS Verduras
                break;

        }
    }
    public void TiposToSeleccion(Text nombre )
    {
        MenuTiposAlimento.SetActive(false);
        /*


        */
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
        menuPrincipal.SetActive(true);
        Alimentos.SetActive(false);
    }

}
