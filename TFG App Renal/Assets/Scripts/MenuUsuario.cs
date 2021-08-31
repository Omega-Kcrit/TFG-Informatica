﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class MenuUsuario : MonoBehaviour
{
    public Controlador controlador;
    public FireBaseManager auth;

    [Header("Menus internos")]
    public GameObject toLogin;
    public GameObject loggin;
    public GameObject alredyLog;
    public GameObject Registro;
    public GameObject register1;
    public GameObject register2;
    public GameObject testInicial;


    //Logged
    [Space]
    [Header("UI LOGGED")]
    public Text uiLogUsuarioA;
    public Text uiLogCorreoA;
    public Text uiLogCuidadorA;
    public InputField uiLogContraseñaN;
    public InputField uiLogUsuarioN;
    public InputField uiLogCorreoN;
    public Dropdown uiLogEstadoN;
    public Text uiLogPesoPlace, uiLogAlturaPlace;
    public InputField uiLogPesoN;
    public InputField uiLogAlturaN;
    public Toggle uiLogHiper;
    public Toggle uiLogDiabetes;
    public Toggle uiLogActividadLog;
    public TextMeshProUGUI punt;

    //Loggin
    [Space]
    [Header("UI Loggin")]
    public TMP_InputField uiCorreoInicio;
    public TMP_InputField uiPswInicio;


    // REGISTRO
    [Space]
    [Header("UI Datos Personales")]
    //UI datos personales
    public InputField uiNombre, uiCorreo, uiPasword, uiPasswordVerif, uiDia, uiMes, uiAno;
    public TMP_Text warningLoginTextREG;

   [Space]
    [Header("UI Datos Medicos")]
    //UI Datos Medicos
    public Toggle uiHiper;
    public Toggle uiDiabetes;
    public Toggle uiActividad;
    public Dropdown Dialisis;
    public InputField uiPeso, uiAltura;
    public TMP_Dropdown ui_EstadoInicial;

    //variables sobre datos personales
    string userName, correo, psw,pswV, dia,mes,año, fechaNacimiento;

    //Variabes sobre datos medicos
    bool Hipertension, Diabetes, Actividad;
    string Peso, Altura;
    int estadoDialisis;




    void Update()
    {
        
    }

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

        this.auth.warningLoginText.text = "";
        uiCorreoInicio.text = "";
        uiPswInicio.text = "";
        uiLogUsuarioA.text = "";
        uiLogCorreoA.text = "";
        uiLogCuidadorA.text = "";
        uiLogPesoPlace.text = ""; 
        uiLogAlturaPlace.text = "";
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
        
        this.auth.cambioUsuario(this.uiLogUsuarioN.text);
    }
    public void cambioCorreo()
    {
        //falta implementacion de firebase
        controlador.aUser.setCorreo(this.uiLogCorreoN.text);
        this.auth.cambioCorreo(this.uiLogCorreoN.text);

    }

    public void cambioContraseña()
    {
        //falta implementacion de firebase
        auth.cambioContraseña(this.uiLogContraseñaN.text);

    }
    public void cambioPeso()
    {
        //falta implementacion de firebase
        this.auth.cambioPeso(this.uiLogPesoN.text);
        controlador.aUser.setPeso(this.uiLogPesoN.text);

    }
    public void cambioAltura()
    {
        //falta implementacion de firebase
        this.auth.cambioAltura(this.uiLogAlturaN.text);
        controlador.aUser.setAltura(this.uiLogAlturaN.text);

    }
  
    public void cambioEstado()
    {
        this.auth.cambioEstado(this.uiLogEstadoN.value);
        controlador.aUser.cambioEstado(this.uiLogEstadoN.value);
    }
    public void cambioHiper()
    {
        this.auth.cambioHiper(this.uiLogHiper.isOn);
        controlador.aUser.cambioHiper(this.uiLogHiper.isOn);
    }
    public void cambioDiabetes()
    {
        this.auth.cambioDiabetes(this.uiLogDiabetes.isOn);
        controlador.aUser.cambioDiabetes(this.uiLogDiabetes.isOn);
    }
    public void cambioActividad()
    {
        this.auth.cambioActividad(this.uiLogActividadLog.isOn);
        controlador.aUser.cambioActividad(this.uiLogActividadLog.isOn);
    }


    public void setCuenta()
    { 

        this.uiLogUsuarioA.text = controlador.aUser.userName;
        this.uiLogCorreoA.text = controlador.aUser.correo;
        this.uiLogCuidadorA.text = "Nueva contraseña";
        this.uiLogPesoPlace.text = controlador.aUser.Peso;
        this.uiLogEstadoN.value = controlador.aUser.dialisis;
        this.uiLogAlturaPlace.text = controlador.aUser.Altura;
        this.uiLogHiper.isOn = controlador.aUser.Hipertension;
        this.uiLogActividadLog.isOn = controlador.aUser.Actividad;
        this.uiLogDiabetes.isOn = controlador.aUser.Diabetes;
        this.punt.text = controlador.aUser.puntuacion.ToString();


    }

    public void cerrarSesion()
    {
        this.auth.LogOut();
        alredyLog.SetActive(false);
        toLogin.SetActive(true);
        this.loggin.SetActive(true);
        auth.emailLoginField.text = "";
        auth.passwordLoginField.text = "";
        controlador.UssertoMain();
    }


    //Iniciar Sesion
    public void Loggin()
    {
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
        this.register2.SetActive(false);


    }

    public void BackRegst()
    {
        this.loggin.SetActive(true);
        this.Registro.SetActive(false);
        uiNombre.text ="";
        uiCorreo.text = "";
        uiPasword.text = "";
        uiPasswordVerif.text = "";
        uiDia.text = "";
        uiMes.text = "";
        uiAno.text = "";
    }

    public void NextRegister()
    {
        userName=uiNombre.text;
        correo = uiCorreo.text;
        psw = uiPasword.text;
        pswV = uiPasswordVerif.text;
        dia = uiDia.text;
        mes = uiMes.text;
        año = uiAno.text;
        fechaNacimiento = dia + "/" + mes + "/" + año;
        bool next = true;
        if (uiNombre.text == "")
        {
            next = false;
            this.warningLoginTextREG.text = "Falta nombre";
            return;
        }
        else if (uiCorreo.text == "")
        {
            next = false;
            this.warningLoginTextREG.text = "Falta correo";
            return;
        }
        else if(uiPasword.text == "")
        {
            next = false;
            this.warningLoginTextREG.text = "Falta contraseña";
            return;
        }
        else if(uiPasswordVerif.text == "")
        {
            next = false;
            this.warningLoginTextREG.text = "Falta repite la contraseña";
            return;
        }
        else if (uiPasswordVerif.text.CompareTo(uiPasword.text) !=0)
        {
            next = false;
            this.warningLoginTextREG.text = "Las contrseñas no coincden";
            return;
        }
        if (uiPasswordVerif.text.Length<5)
        {
            next = false;
            this.warningLoginTextREG.text = "La contrseña tiene que ser mayor a 6 letras";
            return;
        }

        dia = uiDia.text;
        mes = uiMes.text;
        año = uiAno.text;
        int d =-1;
        int m =-1;
        int a =-1;
        try {
            d = int.Parse(dia);
            m = int.Parse(mes);
            a = int.Parse(año);
        }
        catch (FormatException e){       }
       
        
        if ((d<0||d>32) || (m<1||m>12) || (a<1970||a>2020))
        {
            next = false;
            this.warningLoginTextREG.text = "La fecha esta mal puesta";
            return;
        }


        if (next)
        {
            this.register1.SetActive(false);
            this.register2.SetActive(true);
            this.warningLoginTextREG.text = "";
        }
    }

    public void BackRegst2()
    {
        this.register1.SetActive(true);
        this.register2.SetActive(false);
    }

    public void BackRegst3()
    {
        this.register2.SetActive(true);
        this.testInicial.SetActive(false);
    }

    public void Next2Register()
    {
        this.Peso = uiPeso.text;
        this.Altura = uiAltura.text;
        this.Hipertension = uiHiper.isOn;
        this.Diabetes = uiDiabetes.isOn;
        this.Actividad = uiActividad.isOn;
        this.estadoDialisis = this.Dialisis.value;
        bool next = true;

        int p = -1;
        int a = -1;
        try
        {
            p = int.Parse(Peso);
            a = int.Parse(Altura);
        }
        catch (IOException e) { }


        if (p < 0 || p > 200)
        {
            next = false;
            this.warningLoginTextREG.text = "El peso es incorrecto";
        }
        if (a < 0 || a > 250)
        {
            next = false;
            this.warningLoginTextREG.text = "La altura es incorrecta";
        }

        if (next)
        {
            this.register2.SetActive(false);
            this.testInicial.SetActive(true);
        }
    }
    public void Register()
    {

        auth.usernameRegisterField = this.userName;
        auth.emailRegisterField = this.correo;
        auth.passwordRegisterField = this.psw;
        auth.passwordRegisterVerifyField = this.pswV;
        auth.RegisterButton();

    }

    public void RegisterDone()
    {
        
        controlador.aUser = new ActualUser(userName, correo, fechaNacimiento, Hipertension,
                                 Diabetes, Actividad, Peso, Altura, estadoDialisis,0);
        int e=this.ui_EstadoInicial.value;
        switch (e)
        {
            case 0: e = 0; break;
            case 1: e = 2; break;
            case 2: e = 4; break;
            case 3: e = 6; break;
            case 4: e = 8; break;
            case 5: e = 10; break;
        }
        this.Registro.SetActive(false);
        this.testInicial.SetActive(false);
        this.setCuenta();
        alredyLog.SetActive(true);

        auth.RegisterDataButton(userName,correo, Peso,Altura,estadoDialisis,
            Hipertension,Diabetes,Actividad,fechaNacimiento, e);
        controlador.MostrarUsuario();
    }
    




}
