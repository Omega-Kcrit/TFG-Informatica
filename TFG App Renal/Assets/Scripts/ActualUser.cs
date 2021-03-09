using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualUser
{

    //variables sobre datos personales
    public string userName, correo, psw, fechaNacimiento;
    public bool paciente;

    //Variabes sobre datos medicos
    public bool Hipertension, Diabetes, Potasion, Sodio, Fosforo, Actividad;
    public string Peso, Altura;
    int dialisis;


    public ActualUser(string userName, string correo, string psw, string fechaNacimiento,
        bool paciente, bool hipertension, bool diabetes, bool potasion, bool sodio, bool fosforo,
        bool actividad, string peso, string altura, int dialisis)
    {
        this.userName = userName;
        this.correo = correo;
        this.psw = psw;
        this.fechaNacimiento = fechaNacimiento;
        this.paciente = paciente;
        this.dialisis = dialisis;
        Hipertension = hipertension;
        Diabetes = diabetes;
        Potasion = potasion;
        Sodio = sodio;
        Fosforo = fosforo;
        Actividad = actividad;
        Peso = peso;
        Altura = altura;
    }





}
