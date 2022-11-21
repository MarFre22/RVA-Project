using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.UIElements;

public class ReadData2 : MonoBehaviour
{
    //public MapsService MapsService;
    int count = 0, countPlanes = 0, press = -1, fatorEscala = 20;
    bool RuaAveiro = false;
    float[] PollutionData = new float[3 * 63003 + 1];
    float[] PollutionDataExtra = new float[3 * 63003 + 1];
    GameObject[] PollutionGO_Spheres = new GameObject[2 * 63003];
    GameObject[] PollutionGO_Planes = new GameObject[2 * 3 * 3 * 63003];
    float[] PollutionData_Aveiro = new float[3 * 229443 + 1];
    float[] PollutionDataExtra_Aveiro = new float[3 * 229443 + 1];
    GameObject[] PollutionGO_Spheres_Aveiro = new GameObject[2 * 229443];
    GameObject[] PollutionGO_Planes_Aveiro = new GameObject[2 * 3 * 3 * 229443];

    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader;
        Renderer renderer;
        string line, str;
        int k, l;
        float j;
        float RaioEsfera = 3, RaioEsfera_Aveiro = 2;

        reader = new StreamReader("Assets/PollutionData/ConstituicaoB_NOx_9_test.dat");

        while ((line = reader.ReadLine()) != null)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (! System.Char.IsDigit(words[0][0]) || words.Length < 5 )
            {
                continue;
            }
           
            
            string log = "x: " + words[0] + " y: " + words[1] + " vel: " + words[3] + " dir: " + words[4];
            Debug.Log(log);
            
            


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
