using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{

    public GameObject mainMenu, userMenuObj,Alimentos;
    public MenuUsuario userMenu;

    public ActualUser aUser;
    // Start is called before the first frame update
    void Start()
    {
        
        //FiraBase verifica si hay usuario y coloca el usuario ya


    }

    //INICIO SESION FIREBASE;
    public void InicioSesion(string correo, string psw)
    {
        //FIREBASE TODO
    }


    //CAMBIO DE MENUS
    public void MainToUser()
    {
        mainMenu.SetActive(false);
        userMenuObj.SetActive(true);
        bool logg;
        if (aUser != null) logg = true;
        else logg = false;

        userMenu.stard(logg);

    }

    public void UssertoMain()
    {

        userMenuObj.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void MenuToAlimento()
    {

        mainMenu.SetActive(false);
        Alimentos.SetActive(true);
    }


}
