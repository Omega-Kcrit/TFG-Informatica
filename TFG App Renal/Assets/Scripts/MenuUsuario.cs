using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUsuario : MonoBehaviour
{
    public Controlador controlador;

    [Header("Menus internos")]
    public GameObject toLogin;
    public GameObject loggin;
    public GameObject alredyLog;
    public GameObject Registro;
    public GameObject register1;
    public GameObject register2;


    //Logged
    [Space]
    [Header("UI LOGGED")]
    public Text uiLogUsuarioA, uiLogCorreoA;
    public Text uiLogCuidadorA;
    public InputField uiLogCuidadorN, uiLogUsuarioN, uiLogCorreoN;
    public Dropdown uiLogEstadoN;
    public Text uiLogPesoPlace, uiLogAlturaPlace;
    public InputField uiLogPesoN, uiLogAlturaN;
    public Toggle uiLogHiper;
    public Toggle uiLogDiabetes, uiLogActividadLog;







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



    //Cambios de menu
    public void stard(bool logged)
    {
        if (logged)
        {
            alredyLog.SetActive(true);
            this.setCuenta();
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


    //Cuenta ya iniciada
    public void cambioUsuario()
    {
        //falta implementacion firebase
        controlador.aUser.setNombre(this.uiLogUsuarioN.text);
    }
    public void cambioCorreo()
    {
        //falta implementacion de firebase
        controlador.aUser.setCorreo(this.uiLogCorreoN.text);

    }
    public void cambioPeso()
    {
        //falta implementacion de firebase
        controlador.aUser.setPeso(this.uiLogPesoN.text);

    }
    public void cambioAltura()
    {
        //falta implementacion de firebase
        controlador.aUser.setAltura(this.uiLogAlturaN.text);

    }
    public void cambioCuidador()
    {
        //falta implementacion de firebase
        controlador.aUser.setCuidador(this.uiLogCuidadorN.text);

    }
    public void cambioEstado()
    {
        controlador.aUser.cambioEstado(this.uiLogEstadoN.value);
    }
    public void cambioHiper()
    {
        controlador.aUser.cambioHiper(this.uiLogHiper.isOn);
    }
    public void cambioDiabetes()
    {
        controlador.aUser.cambioDiabetes(this.uiLogDiabetes.isOn);
    }
    public void cambioActividad()
    {
        controlador.aUser.cambioActividad(this.uiLogActividadLog.isOn);
    }

    public void setCuenta()
    { 

        this.uiLogUsuarioA.text = controlador.aUser.userName;
        this.uiLogCorreoA.text = controlador.aUser.correo;
        if (controlador.aUser.paciente) this.uiLogCuidadorA.text = controlador.aUser.cuidador;
        this.uiLogPesoPlace.text = controlador.aUser.Peso;
        this.uiLogAlturaPlace.text = controlador.aUser.Altura;
        this.uiLogHiper.isOn = controlador.aUser.Hipertension;

    }

    public void cerrarSesion()
    {
        controlador.aUser = null;
        alredyLog.SetActive(false);
        toLogin.SetActive(true);
    }


    //Iniciar Sesion
    public void Loggin()
    {

        controlador.InicioSesion(uiCorreoInicio.text, uiPswInicio.text);
        alredyLog.SetActive(true);
        toLogin.SetActive(false);
        this.setCuenta();

    }


    //Registro
    public void NowRegister()
    {

        this.loggin.SetActive(false);
        this.Registro.SetActive(true);
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

        this.Registro.SetActive(false);
        this.register2.SetActive(false);
        this.setCuenta();
        alredyLog.SetActive(true);

    }





}
