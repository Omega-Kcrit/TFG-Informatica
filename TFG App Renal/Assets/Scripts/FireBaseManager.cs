using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;

public class FireBaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
     [Header("Register")]
    public string usernameRegisterField, emailRegisterField, passwordRegisterField, passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public MenuUsuario menuUser;

    //User Data variables
    [Header("UserData")]
    public string username, peso, altura, fecha;
    public int dialisis;
    public bool hiper, Diabetes, Actividad;
    public TMP_InputField uiLogCuidadorN;
    public TMP_InputField uiLogUsuarioN;
    public TMP_InputField uiLogCorreoN;
    public Toggle uiLogHiper;
    public Toggle uiLogDiabetes;
    public Toggle uiLogActividadLog;

    //LOGICA INTERNA
    [Header("Logica Interna")]
    public Controlador CON;
    public MenuAlimentos mA;


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        
    }
    void Start()
    {
       // if (CON.aUser == null) LogOut();

    }


    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
            }
            User = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + User.UserId);
            }
        }
    }

    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        //emailLoginField.text
        
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField, passwordRegisterField, usernameRegisterField));
    }


   

    // LOGIN & REGISTER
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Fallo al inicar!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Falta el Correo";
                    break;
                case AuthError.MissingPassword:
                    message = "Falta la contraseña";
                    break;
                case AuthError.WrongPassword:
                    message = "Contraseña incorrecta";
                    break;
                case AuthError.InvalidEmail:
                    message = "Correo invalido";
                    break;
                case AuthError.UserNotFound:
                    message = "La cuenta no existe";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged n
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            StartCoroutine(LoadUserData());
            warningLoginText.text = "";
            //confirmLoginText.text = "Logged In";
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Falta el nombre";
        }
        else if (passwordRegisterField != passwordRegisterVerifyField)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Las contrseñas no coincden!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Registero Erroneo!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Falta el Correo";
                        break;
                    case AuthError.MissingPassword:
                        message = "Falta la contraseña";
                        break;
                    case AuthError.WeakPassword:
                        message = "Contraseña debil";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "El correo ya esta en uso";
                        break;
                    case AuthError.InvalidEmail:
                        message = "El correo no es valido";
                        break;
                }
                warningRegisterText.text = message;
                menuUser.BackRegst3();
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                        menuUser.NowRegister();
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        menuUser.RegisterDone();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    public void LogOut()
    {
        this.CON.aUser = null;
        auth.SignOut();
        User.DeleteAsync();
    }



    // COMUNICACION DEL EXTERIOR DATABASE

    public void RegisterDataButton(string username,string correo, string peso, string altura, int dialisis, bool Hiper,
                                    bool Diabetes, bool Actividad, string fecha,int estadoInicial)
    {
        StartCoroutine(ActualizarUsernameDatabase(username));
        StartCoroutine(ActualizarPesoDatabase(peso));
        StartCoroutine(ActualizarAlturaDatabase(altura));
        StartCoroutine(ActualizarDialisisDatabase(dialisis));
        StartCoroutine(ActualizarHiperDatabase(Hiper));
        StartCoroutine(ActualizarDiabetesDatabase(Diabetes));
        StartCoroutine(ActualizarActividadFDatabase(Actividad));
        StartCoroutine(ActualizarFechaNDatabase(fecha));
        StartCoroutine(ActualizarPuntuacionFDatabase(0));
        StartCoroutine(ActualizarCorreoDatabase(correo));
        StartCoroutine(ActualizarEstadoIDatabase(estadoInicial));
        StartCoroutine(ActualizarlAct(0));
    }


    public void cambioUsuario(string username)
    {
        //falta implementacion firebase
        StartCoroutine(ActualizarUsernameDatabase(username));
    }
    public void cambioPeso(string peso)
    {
        //falta implementacion de firebase
        StartCoroutine(ActualizarPesoDatabase(peso));

    }
    public void cambioAltura(string altura)
    {
        //falta implementacion de firebase
        StartCoroutine(ActualizarAlturaDatabase(altura));

    }
    public void cambioEstado(int dialisis)
    {

        StartCoroutine(ActualizarDialisisDatabase(dialisis));
    }
    public void cambioHiper(bool hiper)
    {
        StartCoroutine(ActualizarHiperDatabase(hiper));
    }
    public void cambioDiabetes(bool diabetes)
    {
        StartCoroutine(ActualizarDiabetesDatabase(diabetes));
    }
    public void cambioActividad(bool actividad)
    {

        StartCoroutine(ActualizarActividadFDatabase(actividad));
    }
    public void cambioPuntuacion(int punt)
    {

        StartCoroutine(ActualizarPuntuacionFDatabase(punt));
    }

    public void cambioCorreo(string _correo)
    {
        StartCoroutine(ActualizarCorreoDatabase( _correo));
    }

    public void cambioContraseña(string _contraseña)
    {
        StartCoroutine(ActualizarContraseñaDatabase(_contraseña));
    }
    public void loadComida(int tipo)
    {
        StartCoroutine(LoadFoodData(tipo));
    }

    public void AddListasA(string tipo)
    {
        Debug.Log("AÑDENDO ACT");
        StartCoroutine(AddAlimentoAct(tipo));
        this.CON.aUser.lAct++;
        StartCoroutine(ActualizarlAct(this.CON.aUser.lAct));

    }

    public void LoadListasAct()
    {
       
        StartCoroutine(LoadListasA());
    }

    public void RevLista(string Alimetno)
    {
        StartCoroutine(RemoveAlimetoAtc(Alimetno));
        cambioPuntuacion(this.CON.aUser.puntuacion+50);
        StartCoroutine(ActualizarlAct(this.CON.aUser.lAct));
    }

    public void LoadPerfil(string p)
    {
        StartCoroutine(LoadFoodPerfilesData(p));
    }


    // DATA BASE interacton

    public IEnumerator LoadUserData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to load task with {DBTask.Exception}");

            string message = "Login Failed!";
            
            warningLoginText.text = message;
        }
        else if (DBTask.Result.Value == null)
        {
            //NODATA
            Debug.LogWarning(message: $"Failed find data");

            string message = "Login Failed!";
            this.LogOut();
            warningLoginText.text = message;
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            string username = snapshot.Child("username").Value.ToString();
            string fechaNac = snapshot.Child("Fecha de Nacimiento").Value.ToString();
            bool diabeteS = (bool)snapshot.Child("Diabetes").Value;
            string Peso = snapshot.Child("peso").Value.ToString();
            string Altura = snapshot.Child("Altura").Value.ToString();
            string Sdialisis = snapshot.Child("Dialisis").Value.ToString();
            bool SHipertension = (bool)snapshot.Child("Hipertension").Value;
            bool SActividad = (bool)snapshot.Child("ActividadFisica").Value;
            string SCorreo = snapshot.Child("correo").Value.ToString();
            string SPunt = snapshot.Child("Puntuacion").Value.ToString();
            int lact = int.Parse( snapshot.Child("ListaA").Value.ToString());

            bool diabetes=false;
            int dialisis=0;
            bool Hipertension = false;
            bool Actividad = false;

            if (diabeteS) diabetes = true;
            if (SHipertension) Hipertension = true;
            if (SActividad) Actividad = true;
            switch (Sdialisis)
            {
                case "1":
                    dialisis = 1;
                    break;
                case "2":
                    dialisis = 2;
                    break;
                case "3":
                    dialisis = 3;
                    break;
                default:
                    dialisis = 0;
                    break;

            }
            CON.aUser = 
                new ActualUser(username,SCorreo, fechaNac, Hipertension, diabetes, Actividad, Peso, Altura, dialisis,lact);
            CON.aUser.puntuacion = int.Parse(SPunt);
            CON.aUser.ToString();
            menuUser.Loggin();

        }

    }
    public IEnumerator LoadFoodData(int tipo)
    {

        string Atipo = "";
        switch (tipo)
        {
            case 0: Atipo = "Carne"; break;
            case 1: Atipo = "Pez"; break;
            case 2: Atipo = "Frutas"; break;
            case 3: Atipo = "LegumbresyVerduras"; break;
            case 4: Atipo = "Grasas"; break;
            case 5:  Atipo = "Lácteos"; break;
            case 6:  Atipo = "Pastelería"; break;
        }
        var DBTask = DBreference.Child("Alimentos").Child(Atipo).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to load task with {DBTask.Exception}");

        }
        else if (DBTask.Result.Value == null)
        {
            //NODATA
            Debug.LogWarning(message: $"Failed find data");

            string message = "NO DATA!";
            warningLoginText.text = message;
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            long i = snapshot.ChildrenCount;
            switch (Atipo)
            {
                case "Carne":
                                        
                    string []AlimentosC = new string[i];
                    int a = 1;
                    for(int s = 0; s<i; s++)
                    {
                        AlimentosC[s]= snapshot.Child(a.ToString()).Value.ToString();
                        a++;
                    }
                    this.mA.AlimentosC = AlimentosC;
                    this.mA.PreparacionTipos();
                    break;
                case "Pez":
                    
                    string[] AlimentosP = new string[i];
                    int p = 1;
                    for (int s = 0; s < i; s++)
                    {
                        AlimentosP[s] = snapshot.Child(p.ToString()).Value.ToString();
                        p++;
                    }
                    this.mA.AlimentosC = AlimentosP;
                    this.mA.PreparacionTipos();
                    break;

                case "Frutas":
                    string[] AlimentosF = new string[i];
                    int f = 1;
                    for (int s = 0; s < i; s++)
                    {
                        AlimentosF[s] = snapshot.Child(f.ToString()).Value.ToString();
                        f++;
                    }
                    this.mA.AlimentosC = AlimentosF;
                    this.mA.PreparacionTipos();
                    break;
                case "LegumbresyVerduras":
                    string[] AlimentosV = new string[i];
                    int lv = 1;
                    
                    for (int s = 0; s < i; s++)
                    {
                        Debug.Log(lv);
                        AlimentosV[s] = snapshot.Child(lv.ToString()).Value.ToString();
                        lv++;
                    }
                    this.mA.AlimentosC = AlimentosV;
                    this.mA.PreparacionTipos();
                    break;
                case "Grasas":
                    string[] AlimentosG = new string[i];
                    int g = 1;
                    for (int s = 0; s < i; s++)
                    {
                        AlimentosG[s] = snapshot.Child(g.ToString()).Value.ToString();
                        g++;
                    }
                    this.mA.AlimentosC = AlimentosG;
                    this.mA.PreparacionTipos();
                    break;
                case "Lácteos":
                    string[] AlimentosL = new string[i];
                    int l = 1;
                    for (int s = 0; s < i; s++)
                    {
                        AlimentosL[s] = snapshot.Child(l.ToString()).Value.ToString();
                        l++;
                    }
                    this.mA.AlimentosC = AlimentosL;
                    this.mA.PreparacionTipos();
                    break;
                case "Pastelería":
                    string[] AlimentosPa = new string[i];
                    int pa = 1;
                    for (int s = 0; s < i; s++)
                    {
                        AlimentosPa[s] = snapshot.Child(pa.ToString()).Value.ToString();
                        pa++;
                    }
                    this.mA.AlimentosC = AlimentosPa;
                    this.mA.PreparacionTipos();
                    break;
            }
        }
    }

    public IEnumerator LoadFoodPerfilesData(string perfil)
    {

        var DBTask = DBreference.Child("Perfiles").Child(perfil).GetValueAsync();
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to load task with {DBTask.Exception}");

            string message = "Login Failed!";
            Debug.Log("FALLO DAABASE");
            warningLoginText.text = message;
        }
        else if (DBTask.Result.Value == null)
        {
            //NODATA
            Debug.LogWarning(message: $"Failed find data");
            Debug.Log("NO EXISTE");
            string message = "Login Failed!";
            this.LogOut();
            warningLoginText.text = message;
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            long i = snapshot.ChildrenCount;
            Debug.Log("Esta dentro");
            Debug.Log("CUANTOS HIJOS"+i);
            
            string[] PerfilesC =snapshot.Child("Carne").Value.ToString().Split(',');
            Debug.Log("tamaño Carne" + PerfilesC.Length);
            this.mA.PerfilAC = PerfilesC;

            
            string[] PerfilesF = snapshot.Child("Frutas").Value.ToString().Split(',');
            this.mA.PerfilAF = PerfilesF;

            string[] PerfilesG = snapshot.Child("Grasas").Value.ToString().Split(',');
            this.mA.PerfilAG = PerfilesG;

            
            string[] PerfilesV = snapshot.Child("LegumbresyVerduras").Value.ToString().Split(',');
            this.mA.PerfilAV = PerfilesV;

            
            string[] PerfilesL = snapshot.Child("Lácteos").Value.ToString().Split(',');
            this.mA.PerfilAL = PerfilesL;

            
            string[] PerfilesP = snapshot.Child("Pastelería").Value.ToString().Split(',');
            this.mA.PerfilAP = PerfilesP;

            
            string[] PerfilesPe = snapshot.Child("Pez").Value.ToString().Split(',');
            this.mA.PerfilAPe = PerfilesPe;


        }
    }

    

    private IEnumerator LoadListasA()
    {
        var DBTask = DBreference.Child("Listas").Child(User.UserId).Child("ListaA").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to load task with {DBTask.Exception}");

            string message = "Login Failed!";
            Debug.Log("FALLO DAABASE");
            warningLoginText.text = message;
        }
        else if (DBTask.Result.Value == null)
        {
            //NODATA
            Debug.LogWarning(message: $"Failed find data");
            Debug.Log("NO EXISTE");
            string message = "Login Failed!";
            this.LogOut();
            warningLoginText.text = message;

        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            long i = snapshot.ChildrenCount;

            string[] ListAlimentosAC = new string[i];
            int p = 0;
            for (int s = 0; s < i; s++)
            {
                ListAlimentosAC[s] = snapshot.Child(p.ToString()).Value.ToString();
                p++;
            }
            this.mA.ListaAct = ListAlimentosAC;
            this.mA.PreparacionListasAct();
        }

        
    }

    // CAMBIO DE DATOS DE INICIO SESION
 

    //ACTUALizar DATOS
    private IEnumerator ActualizarUsernameDatabase( string _username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
            
        }
    }

    private IEnumerator ActualizarPesoDatabase(string _peso)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("peso").SetValueAsync(_peso);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarAlturaDatabase(string _altura)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Altura").SetValueAsync(_altura);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarDialisisDatabase(int _Dialis)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Dialisis").SetValueAsync(_Dialis);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }
    
    private IEnumerator ActualizarHiperDatabase(bool _hiper)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Hipertension").SetValueAsync(_hiper);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarDiabetesDatabase(bool _diabetes)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Diabetes").SetValueAsync(_diabetes);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarActividadFDatabase(bool _Actividad)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("ActividadFisica").SetValueAsync(_Actividad);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarPuntuacionFDatabase(int _Punt)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Puntuacion").SetValueAsync(_Punt);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarFechaNDatabase(string _FechaN)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Fecha de Nacimiento").SetValueAsync(_FechaN);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarCorreoDatabase(string _correo)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("correo").SetValueAsync(_correo);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
            this.User.UpdateEmailAsync(_correo);
        }
    }

    private IEnumerator ActualizarContraseñaDatabase(string _costraseña)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("correo").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
            this.User.UpdatePasswordAsync(_costraseña);
        }
    }

    private IEnumerator ActualizarEstadoIDatabase(int _estadoI)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("EstadoInicial").SetValueAsync(_estadoI);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator ActualizarlAct(int _lact)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("ListaA").SetValueAsync(_lact);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator AddAlimentoAct(string Alimento)
    {

        var DBTask = DBreference.Child("Listas").Child(User.UserId).Child("ListaA")
            .Child(this.CON.aUser.lAct.ToString()).SetValueAsync(Alimento);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database is now updated
        }
    }

    private IEnumerator RemoveAlimetoAtc(string Alimento)
    {
        
        Debug.Log(Alimento);
        
        var DBTask = DBreference.Child("Listas").Child(User.UserId).Child("ListaA")
           .Child(Alimento).SetValueAsync(null);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database is now updated
            this.CON.aUser.lAct--;
        }
    }
   

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
