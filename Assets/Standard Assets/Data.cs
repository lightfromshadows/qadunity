using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuickAndDirty
{
	/*
	 * Helpers for cramming in-game data (ie inspector-exposed structs/classes) into PlayerPrefs and getting it back out again.
	 */
    public class Data
    {
		/*
		 * Fetch a string from PlayerPrefs and try to deserialize it.
		 */
        public static T Read<T>(string key) where T : class
        {
            string s = PlayerPrefs.GetString(key, "");

            if (s.Length < 1) return null;

            StringReader reader = new StringReader(s);
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            return serializer.Deserialize(reader) as T;
        }

		/*
		 * Serialize and cram an object into player prefs. 
		 * 
		 * PlayerPrefs strings have a limit of 1MB. If you're going over that you should look elsewhere for your data storage needs.
		 */
        public static void Write<T>(string key, T data) where T : class
        {
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(writer, data);

            string s = writer.ToString();

            PlayerPrefs.SetString(key, s);
        }

		/*
		 * It's a little silly to the normal C# programmer, but this is a dependable and fast way of deep copying serializable objects.
		 */
        public static T Clone<T>(T original)
        {
            // GET OFF MY LAWN~!
            if (!original.GetType().IsSerializable)
            {
                throw new ArgumentException("Type must be serializable.", "original");
            }
            // null is serializable so if someone tries to clone a null reference return the default (which is usually null)
            if (System.Object.ReferenceEquals(original, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream memStream = new MemoryStream();
            using (memStream) // Hint to GC that this can be released ASAP
            {
                // Serialize it into a chunk of memory, rewind to the head of that stream, and deserialize it.
                // BPDS
                formatter.Serialize(memStream, original); 
                memStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(memStream);
            }
        }
    }
}
