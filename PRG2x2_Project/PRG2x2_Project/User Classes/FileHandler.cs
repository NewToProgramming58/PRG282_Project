using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PRG2x2_Project.User_Classes
{
    internal class FileHandler
    {
        string fileName = "Users.txt";
        public List<User> GetUsers() 
        {
            List<User> users = new List<User>();
            if (File.Exists(fileName)) 
            {
                string fileText = File.ReadAllText(fileName);

                if (fileText.Length > 0)
                {
                    fileText = fileText.Replace("\r\n", "\n");

                    List<string> usersList = fileText.Split('\n').ToList();

                    foreach (string data in usersList)
                    {
                        List<string> userInfo = data.Split(',').ToList();
                        // 0 = username
                        // 1 = password
                        users.Add(new User(userInfo[0], userInfo[1]));
                    }
                }
            }
            else
            {
                File.Create(fileName);
            }            

            return users;
        }

        public void addUser(User user)
        {
            File.AppendAllText(fileName, user.ToString());
        }
    }
}
