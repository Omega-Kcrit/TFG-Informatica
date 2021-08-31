using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualUser
{

    //variables sobre datos personales
    public string userName,correo, fechaNacimiento, cuidador;
    public bool paciente;

    //Variabes sobre datos medicos
    public bool Hipertension, Diabetes, Actividad;
    public string Peso, Altura,Perfil;
    public int dialisis, puntuacion,estadoInc, lAct;
    public float IMC;



    public ActualUser(string userName,string correo, string fechaNacimiento,
        bool hipertension, bool diabetes, bool actividad, string peso,
        string altura, int dialisis, int lact)
    {
        this.userName = userName;
        this.fechaNacimiento = fechaNacimiento;
        this.dialisis = dialisis;
        this.correo = correo;
        this.lAct = lact;
        Hipertension = hipertension;
        Diabetes = diabetes;
        Actividad = actividad;
        Peso = peso;
        Altura = altura;

        this.IMC = float.Parse(peso) / Mathf.Pow(float.Parse(altura)/100, 2);
        this.puntuacion = 0;
        this.TipoPerfil();
    }

    public void TipoPerfil()
    {
        switch (this.Actividad)
        {
            case true:

                switch (this.Diabetes)
                {

                    case true:

                        switch (this.dialisis)
                        {
                            case 3:
                                
                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C12";//22
                                }else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C4";//2
                                }else
                                {
                                    this.Perfil = "C20";//2
                                }
                                break;
                            default:
                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C10";//2
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C2";//2
                                }
                                else
                                {
                                    this.Perfil = "C18";//2
                                }

                                break;
                        }


                        break;

                    case false:
                        // NO DIABETICO SI AF 
                        switch (this.dialisis)
                        {
                            case 3:

                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C11";//2
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C3";//2
                                }
                                else
                                {
                                    this.Perfil = "C19";//
                                }
                                break;
                            default: // NO DIABETICO SI AF 
                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C9";//2
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C1";//2
                                }
                                else
                                {
                                    this.Perfil = "C17";//2
                                }

                                break;
                        }

                        break;
                }

                break;
            case false:
                switch (this.Diabetes)
                {

                    case true:

                        switch (this.dialisis)
                        {
                            case 3:

                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C16";//22
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C8";//22
                                }
                                else
                                {
                                    this.Perfil = "C24";//22
                                }
                                break;
                            default:
                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C14";//22
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C6";//2
                                }
                                else
                                {
                                    this.Perfil = "C22";//2
                                }

                                break;
                        }


                        break;

                    case false:
                        // NO DIABETICO NO AF 
                        switch (this.dialisis)
                        {
                            case 3:

                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C15";//2
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C7";//2
                                }
                                else
                                {
                                    this.Perfil = "C23";//2
                                }
                                break;
                            default: // NO DIABETICO NO AF 
                                if (this.IMC < 18.5)
                                {
                                    this.Perfil = "C13";//2
                                }
                                else if (this.IMC > 18.5 && this.IMC < 24.9)
                                {
                                    this.Perfil = "C5";//2
                                }
                                else
                                {
                                    this.Perfil = "C21";//2
                                }

                                break;
                        }

                        break;
                }


                break;
        }
    }

    public void setNombre(string nNombre)
    {
        this.userName = nNombre;
    }

    public void setCuidador(string nCuidador)
    {
        this.cuidador = nCuidador;
    }

    public void setPeso(string nPeso)
    {
        this.Peso = nPeso;
    }

    public void setAltura(string nAltura)
    {
        this.Altura = nAltura;
    }

    public void setCorreo(string Activi)
    {
        this.correo = Activi;
    }

    public void cambioEstado(int estadoNuevo)
    {
        this.dialisis = estadoNuevo;
    }

    public void cambioHiper(bool hiper)
    {
        this.Hipertension = hiper;
    }

    public void cambioDiabetes(bool Diabe)
    {
        this.Diabetes = Diabe;
    }

    public void cambioActividad(bool Activi)
    {
        this.Actividad = Activi;
    }
    
    override
    public string ToString()
    {
        Debug.Log("Nombre: "+this.userName);
        Debug.Log("Altura: "+this.Altura);
        Debug.Log("Peso: "+this.Peso);
        Debug.Log("Diabetes: "+this.Diabetes);
        Debug.Log("Dialisis: "+this.dialisis);
        Debug.Log("Hiperten: "+this.Hipertension);
        Debug.Log("Actvdad: "+this.Actividad);
        return "";
    }


}
