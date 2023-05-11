using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SerializeTest : MonoBehaviour
{
    /// <summary>
    /// 序列化二进制
    /// </summary>
    /// <param name="outputPath"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool BinarySerialize(string outputPath, object obj)
    {
        
        try
        {
            string dirPath = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, obj);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"序列化二进制失败，{e}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 反序列化二进制文件
    /// </summary>
    /// <param name="binaryFilePath"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T BinaryDeserialize<T>(string binaryFilePath)
    {
        if (!File.Exists(binaryFilePath))
        {
            Debug.LogError($"反序列化二进制失败，找不到二进制文件：{binaryFilePath}");
            return default;
        }

        T obj = default;
        try
        {
            using (FileStream fs = File.OpenRead(binaryFilePath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                obj = (T)binaryFormatter.Deserialize(fs);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"反序列化二进制失败：{e}");
            return obj;
        }

        return obj;
    }

    [MenuItem("SerializeTools/JsonSerialize")]
    private static void JsonSerialize()
    {
        People p = new People();
        Name name = new Name();
        name.name1 = "l";
        name.name2 = "hw";
        p.name = name;
        p.age = 120;

        string jsonString = JsonUtility.ToJson(p);
        string path = Application.dataPath + "/Data/PeopleData.json";
        File.WriteAllText(path, jsonString);
        
        AssetDatabase.Refresh();
    }

    [MenuItem("SerializeTools/JsonDeserialize")]
    private static void JsonDeserialize()
    {
        string path = Application.dataPath + "/Data/PeopleData.json";
        StreamReader sr = File.OpenText(path);
        string jsonString = sr.ReadToEnd();
        sr.Close();

        People p = JsonUtility.FromJson<People>(jsonString);
        Debug.Log(p.name.name1);
        Debug.Log(p.name.name2);
        Debug.Log(p.age);
    }

    public class People : ScriptableObject
    {
        public Name name;
        public int age;
    }

    [Serializable]
    public class Name
    {
        public string name1;
        public string name2;
    }

    [MenuItem("SerializeTools/ScriptableObjectSerialize")]
    private static void ScriptableObjectSerialize()
    {
        string path = "Assets/Data/PeopleData.asset";
        People p = ScriptableObject.CreateInstance<People>();
        Name name = new Name();
        name.name1 = "l";
        name.name2 = "hw";
        p.name = name;
        p.age = 120;
        
        AssetDatabase.CreateAsset(p, path);
        AssetDatabase.Refresh();
    }

    [MenuItem("SerializeTools/ScriptableObjectDeserialize")]
    private static void ScriptableObjectDeserialize()
    {
        People p = AssetDatabase.LoadAssetAtPath<People>("Assets/Data/PeopleData.asset");
        
        Debug.Log(p.name.name1);
        Debug.Log(p.name.name2);
        Debug.Log(p.age);
    }
    
}
