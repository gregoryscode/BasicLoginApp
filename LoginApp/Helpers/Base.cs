﻿using System;
using System.Collections.Generic;
using System.IO;
using LoginApp.Models;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;

namespace LoginApp.Helpers
{
    public class Base
    {
        public static readonly string APP_FOLDER_PATH = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "LoginApp");
        public static readonly string USER_DATABASE_FILE = "userdatabase.json";

        private static Base _instance;

        public static Base Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Base();
                }

                return _instance;

            }
        }

        public static void TrackEvent(string eventName, Dictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(eventName, properties);
        }

        public static void TrackError(Exception ex, Dictionary<string, string> properties = null)
        {
            Crashes.TrackError(ex, properties);
        }

        public static void CreateSetupFolders()
        {
            try
            {
                Directory.CreateDirectory(APP_FOLDER_PATH);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool RegisterUser(List<User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users);
                string filename = Path.Combine(APP_FOLDER_PATH, USER_DATABASE_FILE);

                using (var streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.WriteLine(json);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<User> GetUsers()
        {
            try
            {
                string file = Path.Combine(APP_FOLDER_PATH, USER_DATABASE_FILE);
                string json = null;

                if (!File.Exists(file))
                {
                    return null;
                }

                using (var streamReader = new StreamReader(file))
                {
                    json = streamReader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
