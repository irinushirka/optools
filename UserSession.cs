﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrynaSkurkoOptoolProgram
{
    class UserSession
    {
        private static volatile User currentUser;
        private static object syncRoot = new Object();

        private UserSession() { }

        public static User GetUser()
        {
            if (currentUser == null) throw new Exception("Not logged in.");
            return currentUser;
        }

        public static void Login(User user)
        {
            if (currentUser != null) throw new Exception("Already logged in");
            lock (syncRoot)
            {
                currentUser = user;
            }
        }

        public static void Logout()
        {
            lock (syncRoot)
            {
                currentUser = null;
            }
        }
    }
}
