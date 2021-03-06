﻿using System;
using System.IO;
using System.Runtime.Serialization;

namespace GraZaDuzoZaMalo
{
    public class DataContractSerialization
    {
        private static string _filePath = Path.GetFullPath("save.xml");

        public static bool SaveExists()
        {
            return new FileInfo(_filePath).Exists;
        }
        public static void SerializeToFile<T>(T obj)
        {
            try
            {
                using var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fileStream, obj);
            }
            catch(Exception)
            {
                throw new SaveException("Wystąpił błąd z zapisaniem pliku.");
            }

        }

        public static T DeserializeFromFile<T>()
        {
            try
            {
                using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                var serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(fileStream);
            }
            catch(Exception)
            {
                throw new SaveException("Wystąpił błąd z odczytem zapisu gry. Gra zostanie odpalona od nowa.");
            }

        }

        public static void DeleteSave()
        {
            if(SaveExists())
            {
                File.Delete(_filePath);
            }
        }
    }
}
