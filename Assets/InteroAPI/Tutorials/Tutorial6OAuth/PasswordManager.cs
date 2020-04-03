using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    string path;
    public string username;
    public string password;
    public void Start()
    {
        path = Application.dataPath + "/pass.txt";
    }

    public bool AreCredentialsSaved()
    {
        return File.Exists(path);
    }
    public void ReadCredentials()
    {
        IEnumerable<string> ls = File.ReadLines(path);
        IEnumerator<string> e = ls.GetEnumerator();
        e.MoveNext();
        username = e.Current;
        e.MoveNext();
        password = e.Current;
    }

    public void SaveCredentials(string username, string password)
    {
        print("SaveCredentials success " + username);

        File.WriteAllText(path, username + "\n" + password + "\n");
        this.username = username;
        this.password = password;
        print("SaveCredentials success " + username);

    }
}
