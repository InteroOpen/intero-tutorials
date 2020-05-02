using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace InteroAPI.OAuth
{
    public class PasswordManager
    {
        string path;
        public string username;
        public string password;
        public PasswordManager()
        {
            path = Application.persistentDataPath + "/pass.txt";
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
            // print("1 SaveCredentials started " + username);

            File.WriteAllText(path, username + "\n" + password + "\n");
            this.username = username;
            this.password = password;
            UnityEngine.Debug.Log("2 SaveCredentials success " + username);

        }
    }
}