using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class SaveLoad : MonoBehaviour
{

    private void Awake()
    {
        //Test();
    }

    void Test()
    {
        //MyArrayObject mainObject = new MyArrayObject();
        //mainObject.xSize = 2;
        //mainObject.obj = new MyObject[2, 2];
        //mainObject.obj[0, 0] = new MyObject(0, "zero");
        //mainObject.obj[0, 1] = new MyObject(1, "one");
        //mainObject.obj[1, 0] = new MyObject(2, "two");
        //mainObject.obj[1, 1] = new MyObject(3, "three");

        //MyArrayObjectSerializable seriaObject = mainObject.ToMyArrayObjectSerializable();


        //String generatedString = MapToJSON(seriaObject); // Save object to JSON string

        //String ressourcePath = Path.Combine(Application.dataPath, "Resources"); // Get Path to game resources folder
        //String filePath = Path.Combine("StreamingAssets", "Test.json"); // Get Path to file in resources folder
        //String realPath = Path.Combine(ressourcePath, filePath); // Get Real Path

        //SaveJSONToFile(generatedString, realPath); // Write Json string to file

        //String newPath = Path.Combine("StreamingAssets", "Test"); // Get Path to file in resources folder without .json !
        //String savedString = LoadJSONFromFile(newPath); // Load Json from file

        //MyArrayObjectSerializable secondSeriaObject = JSONToObject(savedString);  // Create Object from Json

        //MyArrayObject secondObject = secondSeriaObject.ToMyArrayObject();
        //Debug.Log(secondObject.obj[0, 1].ToString());

    }

    string MapToJSON(MapSaveStateSerializable obj)
    { // Save object to JSON string
        return JsonUtility.ToJson(obj);
    }

    void SaveJSONToFile(string JSON, string path)
    { // Write Json string to file
        File.WriteAllText(path, JSON);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }


    string LoadJSONFromFile(string path)
    { // Load Json string from file
        TextAsset asset = Resources.Load(path) as TextAsset;
        return asset.text;
    }

    MapSaveStateSerializable JSONToObject(string JSON)
    { // Create Object from Json
        return JsonUtility.FromJson<MapSaveStateSerializable>(JSON);
    }

    [System.Serializable]
    public class MyArrayObjectSerializable
    {

        public MyObject[] objSeria;
        public int objSize;

        public MyArrayObject ToMyArrayObject()
        {
            MyArrayObject mao = new MyArrayObject();
            int sizeObj = (int)Mathf.Sqrt(objSize);
            mao.xSize = sizeObj;
            mao.obj = new MyObject[sizeObj, sizeObj];
            for (int i = 0; i < sizeObj; i++)
            {
                for (int j = 0; j < sizeObj; j++)
                {
                    mao.obj[i, j] = objSeria[i * sizeObj + j];
                }
            }

            return mao;
        }

    }

    [System.Serializable]
    public class MyArrayObject
    {

        public MyObject[,] obj;
        public int xSize;
        public int ySize;

        public MyArrayObjectSerializable ToMyArrayObjectSerializable()
        {
            MyArrayObjectSerializable maos = new MyArrayObjectSerializable();
            int sizeObjSeria = xSize * ySize;
            maos.objSize = sizeObjSeria;

            maos.objSeria = new MyObject[sizeObjSeria];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    maos.objSeria[i * xSize + j] = obj[i, j];
                }
            }

            return maos;
        }

    }

    [System.Serializable]
    public class MyObject
    {
        public int testInt;
        public string testString;

        public MyObject(int i, string s)
        {
            testInt = i;
            testString = s;
        }

        public override string ToString()
        {
            return ("testInt = " + testInt + "testString = " + testString);
        }
    }

}
