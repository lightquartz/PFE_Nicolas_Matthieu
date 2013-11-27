using eDriven.Extensions.Login;
using UnityEngine;
using Event=eDriven.Core.Events.Event;

public class CustomControlsDemoHandlers : MonoBehaviour
{
    void LoginHandler(Event e)
    {
        LoginEvent loginEvent = (LoginEvent)e;
        Debug.Log(
            string.Format(@"User ""{0}"" logged in [password: ""{1})""]",
            loginEvent.Username,
            loginEvent.Password
        ));
    }

    public void Miki(Event e)
    {
        Debug.Log("Miki: " + e);
    }
}