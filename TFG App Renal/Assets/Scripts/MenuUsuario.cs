using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUsuario : MonoBehaviour
{
    public Controlador controlador;

    [Header("Menus internos")]
    public GameObject toLogin;
    public GameObject alredyLog;
    public GameObject Registro;
    public GameObject register1;
    public GameObject register2;


    //Logged
    [Space]
    [Header("UI LOGGED")]
    public Text usuario, correo;
    public Text Cuidador;
    public Dropdown Estado;






    //Loggin
    [Space]
    [Header("UI Loggin")]
    public InputField uiCorreoInicio;
    public InputField uiPswInicio;


    // REGISTRO
    [Space]
    [Header("UI Datos Personales")]
    //UI datos personales
    public GameObject uiPaciente;
    public InputField uiNombre, uiCorreo, uiPasword, uiDia, uiMes, uiAno;

    [Space]
    [Header("UI Datos Medicos")]
    //UI Datos Medicos
    public Toggle uiHiper;
    public Toggle uiDiabetes;
    public Toggle uiPot;
    public Toggle uiSodiodo;
    public Toggle uiFosforo;
    public Toggle uiActividad;
    public Dropdown Dialisis;
    public InputField uiPeso, uiAltura;
    [Space]

    //variables sobre datos personales
    string userName, correo, psw, dia,mes,año, fechaNacimiento;
    bool paciente;

    //Variabes sobre datos medicos
    bool Hipertension, Diabetes, Potasion, Sodio, Fosforo, Actividad;
    string Peso, Altura;
    int estadoDialisis;




    public void stard(bool logged)
    {
        if (logged)
        {
            alredyLog.SetActive(true);
            userName = controlador.aUser.userName;


        }
        else
        {
            toLogin.SetActive(true);
        }
    }
    public void Exit()
    {

        controlador.UssertoMain();
        
    }
    public void RegToLog()
    {
        this.Registro.SetActive(false);
        this.toLogin.SetActive(true);
    }

    //Iniciar Sesion
    public void Loggin()
    {

        controlador.InicioSesion(uiCorreoInicio.text, uiPswInicio.text);

    }


    //Registro
    public void NowRegister()
    {

        this.toLogin.SetActive(false);
        this.register1.SetActive(true);


    }

    public void NextRegister()
    {
        userName=uiNombre.text;
        correo = uiCorreo.text;
        psw = uiPasword.text;
        dia = uiDia.text;
        mes = uiMes.text;
        año = uiAno.text;
        fechaNacimiento = dia + "/" + mes + "/" + año;
        this.register1.SetActive(false);
        this.register2.SetActive(true);
    }
    public void Register()
    {
        this.Peso = uiPeso.text;
        this.Altura = uiAltura.text;
    
        this.Hipertension = uiHiper.isOn;
        this.Sodio = uiSodiodo.isOn;
        this.Fosforo = uiFosforo.isOn;
        this.Diabetes = uiDiabetes.isOn;
        this.Potasion = uiPot.isOn;
        this.Actividad = uiActividad.isOn;
        this.estadoDialisis = this.Dialisis.value;

        controlador.aUser = new ActualUser(userName, correo, psw, fechaNacimiento, paciente, Hipertension, 
                                 Diabetes, Potasion, Sodio, Fosforo, Actividad, Peso, Altura, estadoDialisis);



    }





}
