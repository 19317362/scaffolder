﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Scaffolder.Core.Data;

namespace Scaffolder.Core.Meta
{
    public enum StorageType
    {
        FileSystem = 0,
        FTP = 1,
        AzureStorage = 2,
        SSH = 3,
        AmazonS3 = 4
    }

    public class StorageConfiguration
    {
        public StorageType Type { get; set; }

        public String Url { get; set; }

        public dynamic Connection { get; set; }
    }

    public class User
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public bool Administrator { get; set; }
    }

    public class Configuration
    {
	    public String Name { get; set; }

        public String ConnectionString { get; set; }

        public String Logo { get; set; }

        public StorageConfiguration StorageConfiguration { get; set; }

        public String ApplicationRestartCommand { get; set; }

        public List<User> Users { get; set; }

	    public DatabaseEngine Engine { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public bool Save(String path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
            return true;
        }

        public static Configuration Load(String path)
        {
            var json = File.ReadAllText(path);
            var configuration = JsonConvert.DeserializeObject<Configuration>(json);
	        var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
	        configuration.Name = directoryInfo.Name;
	        return configuration;
        }

        public static Configuration Create()
        {
            return new Configuration
            {
                Title = "",
                Description = "",
                ConnectionString = "Server=server.address;Database=dbname;User Id=login;Password=password",
                StorageConfiguration = new StorageConfiguration
                {
                    Type = StorageType.FileSystem,
                    Url = "http://example.com/storage/",
                    Connection = new
                    {
                        Path = "/var/www/example.com/storage/"
                    }
                },
                Users = new List<User>
                {
                    new User {Login = "admin", Password = "admin", Administrator = true},
                    new User {Login = "manager", Password = "manager", Administrator = false},
                },
                Logo = "http://example.com/logo.jpg",
                ApplicationRestartCommand = ""
            };
        }
    }
}
