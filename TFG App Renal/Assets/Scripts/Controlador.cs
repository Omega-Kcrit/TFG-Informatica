using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{

    public GameObject mainMenu, userMenuObj,Alimentos;
    public MenuUsuario userMenu;

    public ActualUser aUser;



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

    public void AlimentoToMain()
    {
        mainMenu.SetActive(true);
        Alimentos.SetActive(false);
    }

    public void MostrarUsuario()
    {
        Debug.Log("IMC: "+aUser.IMC);
    }

}
