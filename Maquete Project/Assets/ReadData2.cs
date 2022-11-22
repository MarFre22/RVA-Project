using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.UIElements;
using System.Globalization;
using Unity.VisualScripting;

public class ReadData2 : MonoBehaviour
{
    // Arrays to save the imported file values
    float[] x_DataArray, y_DataArray, vel_DataArray, dir_DataArray;

    // Values modifiers
    float offset_xData = 0f, offset_yData = 0f;
    float scale_xData = 1f, scale_yData = 1f, scale_velData = 1f;

    // Arrows
    //      Height of Arrows Objects
    float height_YArrows = 0f;
    //      Dimention
    float dim_Arrows = 2f;

    
    //int count = 0, countPlanes = 0, press = -1, fatorEscala = 20;



    
    //bool RuaAveiro = false;

    GameObject[] arrows_ObjectsArray;

    //GameObject[] PollutionGO_Planes = new GameObject[2 * 3 * 3 * 63003];
    //float[] PollutionData_Aveiro = new float[3 * 229443 + 1];
    //float[] PollutionDataExtra_Aveiro = new float[3 * 229443 + 1];
    //GameObject[] PollutionGO_Spheres_Aveiro = new GameObject[2 * 229443];
    //GameObject[] PollutionGO_Planes_Aveiro = new GameObject[2 * 3 * 3 * 229443];
    string fileToRead1 = "Assets/PollutionData/ConstituicaoB_NOx_9_test.dat";

    // Function to count the max number of lines of the file
    int TotalLines(string filePath)
    {
        using (StreamReader r = new StreamReader(filePath))
        {
            int i = 0;
            while (r.ReadLine() != null) 
            { 
                i++; 
            }
            return i;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader;
        Renderer renderer;
        string line, str;
        int k, l;
        float j;
        //float RaioEsfera = 3, RaioEsfera_Aveiro = 2;

        // Count lines of the file
        int fileLenght = TotalLines(fileToRead1);
        
        // Initializate Arrays with max lenght
        x_DataArray = new float[fileLenght];
        y_DataArray = new float[fileLenght];
        vel_DataArray = new float[fileLenght];
        dir_DataArray = new float[fileLenght];
        arrows_ObjectsArray = new GameObject[fileLenght];


        reader = new StreamReader(fileToRead1);

        int countDataIndex = 0;

        // Loop to analyse each line of the file
        while ((line = reader.ReadLine()) != null)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (! System.Char.IsDigit(words[0][0]) || words.Length < 5 )
            {
                continue;
            }

            // Save each parameter of the file to the arrays
            x_DataArray[countDataIndex] = offset_xData + scale_xData * float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat);
            y_DataArray[countDataIndex] = offset_yData + scale_yData *  float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat);
            vel_DataArray[countDataIndex] = scale_velData * float.Parse(words[3], CultureInfo.InvariantCulture.NumberFormat);
            dir_DataArray[countDataIndex] = float.Parse(words[4], CultureInfo.InvariantCulture.NumberFormat);

            //string log = "x: " + words[0] + " y: " + words[1] + " vel: " + words[3] + " dir: " + words[4];
            string log = "y: " + y_DataArray[countDataIndex];
            Debug.Log(log);

            // Create and edit arrows
            arrows_ObjectsArray[countDataIndex] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            arrows_ObjectsArray[countDataIndex].name = "Arrow_" + countDataIndex.ToString();
            renderer = arrows_ObjectsArray[countDataIndex].GetComponent<Renderer>();
            renderer.material.color = Color.green;
            arrows_ObjectsArray[countDataIndex].transform.localScale = new Vector3(dim_Arrows, dim_Arrows, dim_Arrows);
            arrows_ObjectsArray[countDataIndex].transform.position = new Vector3(x_DataArray[countDataIndex], height_YArrows , y_DataArray[countDataIndex]  );

            
            //      Activate 
            arrows_ObjectsArray[countDataIndex].SetActive(true);

            countDataIndex++;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
