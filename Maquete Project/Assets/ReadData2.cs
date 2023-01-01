using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using Newtonsoft.Json.Linq;
using UnityEngine.UIElements;
using System.Globalization;
using Unity.VisualScripting;
using Vuforia;
using UnityEditor;
//using UnityEditor.VersionControl;
//using AndroidSettings;


struct DataImported
{
    public float[] x_DataArray;
    public float[] y_DataArray;
    public float[] vel_DataArray;
    public float[] dir_DataArray;
    public GameObject[] arrows_ObjectsArray;
};


public class ReadData2 : MonoBehaviour  
{
   
    // Arrays to save the imported file values
    float[] x_DataArray, y_DataArray, vel_DataArray, dir_DataArray;


    // Arrows - Objects
    public GameObject arrow_0;
    public GameObject arrow_1;
    public GameObject arrow_2;
    public GameObject arrow_3;
    public GameObject arrow_4;
    public GameObject arrow_5;
    public GameObject arrow_6;
    public GameObject arrow_7;
    public GameObject arrow_8;
    public GameObject arrow_9;
    
    //      Object - Red
    public GameObject arrow_pink;
    



    //      Dimention
    float dim_Arrows = 0.15f;

    // Initialize Objects
    GameObject scripted_objects_object, ar_target_object, light_object, arrow_object;

    // Initialize aray of Objects
    GameObject[] arrows_ObjectsArray;

    // AR image target - input
    public ObserverBehaviour mTarget;

    private bool all_data_objects_zoom = false;

    private Vector3 scaleChange, positionChange;

    string fileToRead1;

    



    /*
    // Function to count the max number of lines of the file
    int TotalLines(string[] textDataArray)
    {
        int valret = 0;
        
        if (type == 0)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                int i = 0;
                while (r.ReadLine() != null)
                {
                    i++;
                }
                valret = i;
                //return i;
            }
        }
        else if (type == 1)
        {
            valret = textDataArray.Length;
        }

        return valret;
        
    }
    */

    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 120, 40), "Model Scale Decrease"))
        {
            all_data_objects_zoom = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
        StreamReader reader;
        Renderer renderer;
        //string line;
        float j;

        //      Height of Arrows Objects
        float height_YArrows = 0f;

        //      Arrows - Classification
        float arrow_class_0 = 0.3f;
        float arrow_class_1 = 1.5f;
        float arrow_class_2 = 3.3f;
        float arrow_class_3 = 5.4f;
        float arrow_class_4 = 7.9f;
        float arrow_class_5 = 10.7f;
        float arrow_class_6 = 13.8f;
        float arrow_class_7 = 17.1f;
        float arrow_class_8 = 20.7f;
        float arrow_class_9 = 24.4f;

        

        // Values modifiers
        float offset_xData = 0f, offset_yData = 0f;
        float scale_xData = 0.5f, scale_yData = 0.5f, scale_velData = 1f;

        //string[] textData = null;

        //int Android = 0;

        // Path Imported File
        //TextAsset textFile = Resources.Load<TextAsset>(Path.Combine("NCM2-Edit", "02edit.dat"));
        //string path = Path.Combine("ncme2", "02edit");
        TextAsset textFile = Resources.Load("ncme2/02edit") as TextAsset;
        //string texto = textFile.text;
        string[] textData = textFile.text.Split('\n');
        /*
        if (Application.platform == RuntimePlatform.Android)
        {
            Android = 1;
            //fileToRead1 = Path.Combine(Application.streamingAssetsPath, "StreamingAssets", "DataEscoamento", "NCM2-Edit", "02edit.dat");


            fileToRead1 = null;

            TextAsset textFile = Resources.Load<TextAsset>(Path.Combine("NCM2-Edit", "02edit.dat"));


            textData = textFile.text.Split(new char[] { '\n' });

            //UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(fileToRead1);

            //www.SendWebRequest();
            //while (!www.isDone)
            //{
            //}

        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            fileToRead1 = Path.Combine(Application.streamingAssetsPath, "DataEscoamento", "NCM2-Edit", "02edit.dat");
        }
        */


        // AR Camera Object
        ar_target_object = GameObject.FindWithTag("ARImageTarget");
        scripted_objects_object = GameObject.FindWithTag("ScriptedObjects");
        //scripted_objects_object.SetActive(false);

        //Transform ar_target_transform = ar_target_object.Transform();

        // Light object
        light_object = GameObject.FindWithTag("Light");

        // Light object - Modify parent
        light_object.transform.parent = ar_target_object.transform;

        // Arrow Objec
        arrow_object = GameObject.FindWithTag("ArrowObj");



        // Count lines of the file
        //int fileLenght = TotalLines(Android, fileToRead1, textData);
        int fileLenght = textData.Length;


        DataImported DataImp;

        // Initializate Arrays with max lenght
        DataImp.x_DataArray = new float[fileLenght];
        DataImp.y_DataArray = new float[fileLenght];
        DataImp.vel_DataArray = new float[fileLenght];
        DataImp.dir_DataArray = new float[fileLenght];
        DataImp.arrows_ObjectsArray = new GameObject[fileLenght];


        //reader = new StreamReader(fileToRead1);

        int countDataIndex = 0;

        int resoltuionArrows = 300;

        int countResolution = 0;

        // Loop to analyse each line of the file
        //while ((line = reader.ReadLine()) != null)
        foreach (string line in textData)
        {
          
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (! System.Char.IsDigit(words[0][0]) || words.Length < 4 )
            {
                continue;
            }

            if ((float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) != 2f && float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) != 2f) && countResolution == resoltuionArrows)
            {
                countResolution = 0;
                continue;
            }

            // Save each parameter of the file to the arrays
            DataImp.x_DataArray[countDataIndex] = offset_xData + scale_xData * float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat);
            DataImp.y_DataArray[countDataIndex] = offset_yData + scale_yData *  float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat);
            DataImp.vel_DataArray[countDataIndex] = float.Parse(words[2], CultureInfo.InvariantCulture.NumberFormat);
            DataImp.dir_DataArray[countDataIndex] = float.Parse(words[3], CultureInfo.InvariantCulture.NumberFormat);

            //string log = "x: " + words[0] + " y: " + words[1] + " vel: " + words[2] + " dir: " + words[3];
            //string log = "y: " + y_DataArray[countDataIndex];
            //Debug.Log(log);

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) == 2f && float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) == 2f)
            {
                DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_pink);

                string log = "o ponto [0,0] Existe!!!";
                Debug.Log(log);
            }
            else
            {
                // Import object with classification
                if (DataImp.vel_DataArray[countDataIndex] < arrow_class_0)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_0);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_1 && DataImp.vel_DataArray[countDataIndex] >= arrow_class_0)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_1);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_2 && DataImp.vel_DataArray[countDataIndex] > arrow_class_1)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_2);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_3 && DataImp.vel_DataArray[countDataIndex] > arrow_class_2)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_3);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_4 && DataImp.vel_DataArray[countDataIndex] > arrow_class_3)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_4);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_5 && DataImp.vel_DataArray[countDataIndex] > arrow_class_4)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_5);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_6 && DataImp.vel_DataArray[countDataIndex] > arrow_class_5)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_6);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_7 && DataImp.vel_DataArray[countDataIndex] > arrow_class_6)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_7);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_8 && DataImp.vel_DataArray[countDataIndex] > arrow_class_7)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_8);
                }
                else if (DataImp.vel_DataArray[countDataIndex] <= arrow_class_9 && DataImp.vel_DataArray[countDataIndex] > arrow_class_8)
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }
                else
                {
                    DataImp.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }

                GL.Flush();
            }


            

            //arrows_ObjectsArray[countDataIndex] = arrow_object
            DataImp.arrows_ObjectsArray[countDataIndex].name = "Arrow_" + countDataIndex.ToString();
            //renderer = arrows_ObjectsArray[countDataIndex].GetComponent<Renderer>();

            //renderer.material.color = Color.green;
            DataImp.arrows_ObjectsArray[countDataIndex].transform.localScale = new Vector3(dim_Arrows, dim_Arrows, dim_Arrows);
            DataImp.arrows_ObjectsArray[countDataIndex].transform.position = new Vector3(DataImp.x_DataArray[countDataIndex], height_YArrows , DataImp.y_DataArray[countDataIndex]  );




            DataImp.arrows_ObjectsArray[countDataIndex].transform.SetParent(scripted_objects_object.transform, true);

            //arrows_ObjectsArray[countDataIndex].transform.parent = scripted_objects_object.transform;

            //      Activate 
            //arrows_ObjectsArray[countDataIndex].SetActive(false);

            countDataIndex++;
            countResolution++;

        }

        //scripted_objects_object.transform.parent = ar_target_object.transform;

        scripted_objects_object.SetActive(false);
        scripted_objects_object.transform.localScale = new Vector3(0.018f, 0.05f, 0.03f);
        scripted_objects_object.transform.localPosition = new Vector3(0.4f, 0.05f, -0.4f);

        //foreach (Transform child in scripted_objects_object.transform)
        //    child.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        

        // if AR image target is visible
        //if (Vuforia.TargetStatus.NotObserved)
        if (mTarget != null)
        {
            //      Activate 
            //foreach (Transform child in scripted_objects_object.transform)
            //    child.gameObject.SetActive(true);
            scripted_objects_object.SetActive(true);
            //ar_target_object.SetActive(true);

            if (all_data_objects_zoom == true)
            {
                scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
                scripted_objects_object.transform.localScale += scaleChange;
                all_data_objects_zoom = false;
            }
        }
        else
        {
            if (scripted_objects_object.activeInHierarchy)
            {
                scripted_objects_object.SetActive(false);
            }
        }
        

        

    }
}
