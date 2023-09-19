using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using System;
using System.Linq;

public class AuthManager : MonoBehaviour
{
    [Header("General")]
    public TextMeshProUGUI username;
    public UserData userdata;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBref;

    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("Leaderboard")]
    public GameObject scoreElement;
    public Transform scoreboardContent;



    void Awake()
    {
        //Make sure that all of the required dependencies exist on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible, then Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Unable to resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }


    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        Debug.Log("Set up Successful");
        DBref = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void LoginButton()
    {
        //Attached to the onClick on Login button
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        Debug.Log("routine started");

    }
    public void RegisterButton()
    {
        //Attached to the onClick on Register button
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    public void SignOut()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    private IEnumerator Login(string _email, string _password)
    {
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User has succesfully logged in
            User = new FirebaseUser(LoginTask.Result.User);
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            StartCoroutine(LoadUserData());


            yield return new WaitForSeconds(2);

            username.text = User.DisplayName;
            UIManager.instance.MainScreen();
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();

        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username is missing
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password do not match
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors run switch case
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
              
                User = new FirebaseUser(RegisterTask.Result.User);

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now in database
                        //Return to login screen so they can put in their details
                        UIManager.instance.LoginScreen();
                        ClearLoginFeilds();
                        ClearRegisterFeilds();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    public void SaveDataButton()
    {

        StartCoroutine(UpdateUsernameAuth(username.text));
        StartCoroutine(UpdateUsernameDatabase(username.text));

        StartCoroutine(UpdateLevel(userdata.Level, "Level"));
        StartCoroutine(UpdateLevel(userdata.LevelBarProg, "Level Progress"));

        StartCoroutine(UpdateGems(userdata.Gems));

        StartCoroutine(UpdateMission(userdata.Rprog, "Recycle", "Recycleprog"));
        StartCoroutine(UpdateMission(userdata.Rgoal, "Recycle", "Recyclegoal"));
        StartCoroutine(UpdateMission(userdata.Wprog, "Water", "Waterprog"));
        StartCoroutine(UpdateMission(userdata.Wgoal, "Water", "Watergoal"));
        StartCoroutine(UpdateMission(userdata.Mprog, "Meat", "Meatprog"));
        StartCoroutine(UpdateMission(userdata.Mgoal, "Meat", "Meatgoal"));

        StartCoroutine(UpdateItems(userdata.WorldProg, "WorldProg"));
        StartCoroutine(UpdateItems(userdata.ChickenProg, "ChickenProg"));
        StartCoroutine(UpdateItems(userdata.BuffProg, "BuffaloProg"));
        StartCoroutine(UpdateItems(userdata.LizardProg, "LizardProg"));
        StartCoroutine(UpdateItems(userdata.MonkeyProg, "MonkeyProg"));

        Debug.Log("Data Saved");

    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        Task ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    public string GetUsername()
    {
        return User.UserId;
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        Task DBTask = DBref.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Success
        }
    }

    public IEnumerator UpdateLevel(int i, string child)
    {
        Task DBTask = DBref.Child("users").Child(User.UserId).Child("Level and Progress").Child(child).SetValueAsync(i);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //level is now updated
        }
    }

    public IEnumerator UpdateGems(int gems)
    {
        Task DBTask = DBref.Child("users").Child(User.UserId).Child("gems").SetValueAsync(gems);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Gems is now updated
        }
    }

  

    public IEnumerator UpdateItems(int value, string child)
    {
        Task DBTask = DBref.Child("users").Child(User.UserId).Child("Items").Child(child).SetValueAsync(value);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //item is now updated
        }
    }

    public IEnumerator UpdateMission(int value, string parent, string child)
    {
        Task DBTask = DBref.Child("users").Child(User.UserId).Child("mission").Child(parent).Child(child).SetValueAsync(value);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //mission is now updated
        }
    }


    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        Task<DataSnapshot> DBTask = DBref.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {

            //Default Data
            userdata.SetLevel(0);
            userdata.SetLevelBarProg(0);
            userdata.SetGems(0);
           

            userdata.SetWprog(0);
            userdata.SetWgoal(1);
            userdata.SetRprog(0);
            userdata.SetRgoal(1);
            userdata.SetMprog(0);
            userdata.SetMgoal(1);

            userdata.SetMonkeyProg(0);
            userdata.SetChickenProg(0);
            userdata.SetBuffProg(0);
            userdata.SetLizardProg(0);

            userdata.SetWorldProg(0);

            userdata.flag = true;


        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            Debug.Log("Snapshot started");
            

            userdata.SetLevel((int)((long) snapshot.Child("Level and Progress").Child("Level").Value));
            userdata.SetLevelBarProg((int)((long)snapshot.Child("Level and Progress").Child("Level Progress").Value));
            userdata.SetGems((int)((long) snapshot.Child("gems").Value));
         

            userdata.SetWprog((int)((long)snapshot.Child("mission").Child("Water").Child("Waterprog").Value));
            userdata.SetWgoal((int)((long)snapshot.Child("mission").Child("Water").Child("Watergoal").Value));

            userdata.SetRprog((int)((long)snapshot.Child("mission").Child("Recycle").Child("Recycleprog").Value));
            userdata.SetRgoal((int)((long)snapshot.Child("mission").Child("Recycle").Child("Recyclegoal").Value));

            userdata.SetMprog((int)((long)snapshot.Child("mission").Child("Meat").Child("Meatprog").Value));
            userdata.SetMgoal((int)((long)snapshot.Child("mission").Child("Meat").Child("Meatgoal").Value));

            userdata.SetWorldProg((int)(long)snapshot.Child("Items").Child("WorldProg").Value);
            userdata.SetChickenProg((int)(long)snapshot.Child("Items").Child("ChickenProg").Value);
            userdata.SetBuffProg((int)(long)snapshot.Child("Items").Child("BuffaloProg").Value);
            userdata.SetLizardProg((int)(long)snapshot.Child("Items").Child("LizardProg").Value);
            userdata.SetMonkeyProg((int)(long)snapshot.Child("Items").Child("MonkeyProg").Value);


            Debug.Log("Snapshot Complete");

            userdata.flag = true;


        }

    }
    public void LeaderboardClick()
    {
        StartCoroutine(LoadLeaderboardData());
    }

    private IEnumerator LoadLeaderboardData()
    {
        //Get all the users data ordered by level
        Task<DataSnapshot> DBTask = DBref.Child("users").Child("Level and Progress").Child("Level").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            Debug.Log("snapshot recieved");


            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UserID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int level = int.Parse(childSnapshot.Child("Level and Progress").Child("Level").Value.ToString());
                Debug.Log(username);
                Debug.Log(level.ToString());
                

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, level);
            }

            //Go to scoareboard screen
            UIManager.instance.BoardScreen();
        }
    }

}