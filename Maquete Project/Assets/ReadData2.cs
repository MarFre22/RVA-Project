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

    int buttonAltura = 0;
    
    public void ChangeAltura(string altura="")
    {
        
        if (altura == "2"){
            buttonAltura = 2;
        }
        else if(altura == "24")
        {
            buttonAltura = 24;
        }
        else if (altura == "48")
        {
            buttonAltura = 48;
        }
        
        
    }

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


    public Button button_1;
    public Button button_2;
    public Button button_3;



    //      Dimention
    float dim_Arrows = 0.19f;
    float dim_Arrowsz = 0.70f;

    // Initialize Objects
    GameObject scripted_objects_object, ar_target_object, light_object, scripted_objects_object2, scripted_objects_object3;

    // Initialize aray of Objects
    GameObject[] arrows_ObjectsArray;

    // AR image target - input
    public ObserverBehaviour mTarget;



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

        //int Android = 0;

        // Path Imported File
        TextAsset textFile = Resources.Load("ncme2/02edit") as TextAsset;
        TextAsset textFile2 = Resources.Load("ncme2/24edit") as TextAsset;
        TextAsset textFile3 = Resources.Load("ncme2/48edit") as TextAsset;
        

        string[] textData = textFile.text.Split('\n');
        string[] textData2 = textFile2.text.Split('\n');
        string[] textData3 = textFile3.text.Split('\n');



        // AR Camera Object
        ar_target_object = GameObject.FindWithTag("ARImageTarget");
        scripted_objects_object = GameObject.FindWithTag("ScriptedObjects");
        scripted_objects_object2 = GameObject.FindWithTag("ScriptedObjects2");
        scripted_objects_object3 = GameObject.FindWithTag("ScriptedObjects3");
        //scripted_objects_object.SetActive(false);

        //Transform ar_target_transform = ar_target_object.Transform();

        // Light object
        light_object = GameObject.FindWithTag("Light");

        // Light object - Modify parent
        light_object.transform.parent = ar_target_object.transform;



        // Count lines of the file
        int fileLenght = textData.Length;
        int fileLenght2 = textData2.Length;
        int fileLenght3 = textData3.Length;


        DataImported DataImp;
        DataImported DataImp2;
        DataImported DataImp3;

        // Initializate Arrays with max lenght
        DataImp.x_DataArray = new float[fileLenght];
        DataImp.y_DataArray = new float[fileLenght];
        DataImp.vel_DataArray = new float[fileLenght];
        DataImp.dir_DataArray = new float[fileLenght];
        DataImp.arrows_ObjectsArray = new GameObject[fileLenght];

        DataImp2.x_DataArray = new float[fileLenght2];
        DataImp2.y_DataArray = new float[fileLenght2];
        DataImp2.vel_DataArray = new float[fileLenght2];
        DataImp2.dir_DataArray = new float[fileLenght2];
        DataImp2.arrows_ObjectsArray = new GameObject[fileLenght2];

        DataImp3.x_DataArray = new float[fileLenght3];
        DataImp3.y_DataArray = new float[fileLenght3];
        DataImp3.vel_DataArray = new float[fileLenght3];
        DataImp3.dir_DataArray = new float[fileLenght3];
        DataImp3.arrows_ObjectsArray = new GameObject[fileLenght3];

        // -----------------------------------------------
        int countDataIndex = 0;
        int resoltuionArrows = 1;
        int countResolution = 0;
        int flush = 0;

        // Loop to analyse each line of the file
        foreach (string line in textData)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (! System.Char.IsDigit(words[0][0]) || words.Length < 4 )
            {
                continue;
            }

            if (float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
                continue;
            }

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
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

                flush +=1 ;

                if (flush == 250)
                {
                    GL.Flush();
                    flush = 0;
                }
                   
            }

            DataImp.arrows_ObjectsArray[countDataIndex].name = "Arrow_" + countDataIndex.ToString();
            DataImp.arrows_ObjectsArray[countDataIndex].transform.localScale = new Vector3(dim_Arrows, dim_Arrows, dim_Arrowsz);
            DataImp.arrows_ObjectsArray[countDataIndex].transform.position = new Vector3(DataImp.x_DataArray[countDataIndex], height_YArrows , DataImp.y_DataArray[countDataIndex]  );
            DataImp.arrows_ObjectsArray[countDataIndex].transform.SetParent(scripted_objects_object.transform, true);

            countDataIndex++;
        }

        // -----------------------------------------------
        countDataIndex = 0;
        resoltuionArrows = 1;
        countResolution = 0;
        flush = 0;

        // Loop to analyse each line of the file
        foreach (string line in textData2)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!System.Char.IsDigit(words[0][0]) || words.Length < 4)
            {
                continue;
            }

            if (float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
                continue;
            }

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
                continue;
            }

            // Save each parameter of the file to the arrays
            DataImp2.x_DataArray[countDataIndex] = offset_xData + scale_xData * float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat);
            DataImp2.y_DataArray[countDataIndex] = offset_yData + scale_yData * float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat);
            DataImp2.vel_DataArray[countDataIndex] = float.Parse(words[2], CultureInfo.InvariantCulture.NumberFormat);
            DataImp2.dir_DataArray[countDataIndex] = float.Parse(words[3], CultureInfo.InvariantCulture.NumberFormat);

            //string log = "x: " + words[0] + " y: " + words[1] + " vel: " + words[2] + " dir: " + words[3];
            //string log = "y: " + y_DataArray[countDataIndex];
            //Debug.Log(log);

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) == 2f && float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) == 2f)
            {
                DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_pink);

                string log = "o ponto [0,0] Existe!!!";
                Debug.Log(log);
            }
            else
            {
                // Import object with classification
                if (DataImp2.vel_DataArray[countDataIndex] < arrow_class_0)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_0);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_1 && DataImp2.vel_DataArray[countDataIndex] >= arrow_class_0)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_1);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_2 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_1)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_2);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_3 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_2)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_3);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_4 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_3)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_4);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_5 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_4)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_5);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_6 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_5)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_6);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_7 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_6)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_7);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_8 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_7)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_8);
                }
                else if (DataImp2.vel_DataArray[countDataIndex] <= arrow_class_9 && DataImp2.vel_DataArray[countDataIndex] > arrow_class_8)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }
                else
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }

                flush += 1;

                if (flush == 250)
                {
                    GL.Flush();
                    flush = 0;
                }

            }

            DataImp2.arrows_ObjectsArray[countDataIndex].name = "Arrow_" + countDataIndex.ToString();
            DataImp2.arrows_ObjectsArray[countDataIndex].transform.localScale = new Vector3(dim_Arrows, dim_Arrows, dim_Arrowsz);
            DataImp2.arrows_ObjectsArray[countDataIndex].transform.position = new Vector3(DataImp2.x_DataArray[countDataIndex], height_YArrows, DataImp2.y_DataArray[countDataIndex]);
            DataImp2.arrows_ObjectsArray[countDataIndex].transform.SetParent(scripted_objects_object2.transform, true);

            countDataIndex++;
        }

        // -----------------------------------------------
        countDataIndex = 0;
        resoltuionArrows = 1;
        countResolution = 0;
        flush = 0;

        // Loop to analyse each line of the file
        foreach (string line in textData3)
        {
            string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!System.Char.IsDigit(words[0][0]) || words.Length < 4)
            {
                continue;
            }

            if (float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
                continue;
            }

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) % 2 == 0)
            {
                continue;
            }

            // Save each parameter of the file to the arrays
            DataImp3.x_DataArray[countDataIndex] = offset_xData + scale_xData * float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat);
            DataImp3.y_DataArray[countDataIndex] = offset_yData + scale_yData * float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat);
            DataImp3.vel_DataArray[countDataIndex] = float.Parse(words[2], CultureInfo.InvariantCulture.NumberFormat);
            DataImp3.dir_DataArray[countDataIndex] = float.Parse(words[3], CultureInfo.InvariantCulture.NumberFormat);

            //string log = "x: " + words[0] + " y: " + words[1] + " vel: " + words[2] + " dir: " + words[3];
            //string log = "y: " + y_DataArray[countDataIndex];
            //Debug.Log(log);

            if (float.Parse(words[0], CultureInfo.InvariantCulture.NumberFormat) == 2f && float.Parse(words[1], CultureInfo.InvariantCulture.NumberFormat) == 2f)
            {
                //DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_pink);

                //string log = "o ponto [0,0] Existe!!!";
                //Debug.Log(log);
            }
            else
            {
                // Import object with classification
                if (DataImp3.vel_DataArray[countDataIndex] < arrow_class_0)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_0);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_1 && DataImp3.vel_DataArray[countDataIndex] >= arrow_class_0)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_1);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_2 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_1)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_2);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_3 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_2)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_3);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_4 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_3)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_4);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_5 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_4)
                {
                    DataImp2.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_5);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_6 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_5)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_6);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_7 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_6)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_7);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_8 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_7)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_8);
                }
                else if (DataImp3.vel_DataArray[countDataIndex] <= arrow_class_9 && DataImp3.vel_DataArray[countDataIndex] > arrow_class_8)
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }
                else
                {
                    DataImp3.arrows_ObjectsArray[countDataIndex] = GameObject.Instantiate(arrow_9);
                }

                flush += 1;

                if (flush == 250)
                {
                    GL.Flush();
                    flush = 0;
                }

            }

            DataImp3.arrows_ObjectsArray[countDataIndex].name = "Arrow_" + countDataIndex.ToString();
            DataImp3.arrows_ObjectsArray[countDataIndex].transform.localScale = new Vector3(dim_Arrows, dim_Arrows, dim_Arrowsz);
            DataImp3.arrows_ObjectsArray[countDataIndex].transform.position = new Vector3(DataImp3.x_DataArray[countDataIndex], height_YArrows, DataImp3.y_DataArray[countDataIndex]);
            DataImp3.arrows_ObjectsArray[countDataIndex].transform.SetParent(scripted_objects_object3.transform, true);

            countDataIndex++;
        }

        scripted_objects_object.SetActive(false);
        scripted_objects_object2.SetActive(false);
        scripted_objects_object3.SetActive(false);
        float scale_x = 0.018f * 2.0f;
        float scale_y = 0.05f;
        float scale_z = 0.03f * 2.0f;
        scripted_objects_object.transform.localScale = new Vector3(scale_x, scale_y, scale_z);
        scripted_objects_object.transform.localPosition = new Vector3(0.4f, 0.05f, -0.4f);
        scripted_objects_object2.transform.localScale = new Vector3(scale_x, scale_y, scale_z);
        scripted_objects_object2.transform.localPosition = new Vector3(0.4f, 0.50f, -0.4f);
        scripted_objects_object3.transform.localScale = new Vector3(scale_x, scale_y, scale_z);
        scripted_objects_object3.transform.localPosition = new Vector3(0.4f, 1f, -0.4f);

    }

    // Update is called once per frame
    void Update()
    {

        

        // if AR image target is visible
        //if (Vuforia.TargetStatus.NotObserved)
        if (mTarget != null)
        {
            //      Activate 
            if (buttonAltura == 2)
            {
                scripted_objects_object.SetActive(true);
                scripted_objects_object2.SetActive(false);
                scripted_objects_object3.SetActive(false);
            }
            else if (buttonAltura == 24)
            {
                scripted_objects_object.SetActive(false);
                scripted_objects_object2.SetActive(true);
                scripted_objects_object3.SetActive(false);
            }
            else if (buttonAltura == 48)
            {
                scripted_objects_object.SetActive(false);
                scripted_objects_object2.SetActive(false);
                scripted_objects_object3.SetActive(true);
            }
            
        }
        else
        {
            if (scripted_objects_object.activeInHierarchy)
            {
                scripted_objects_object.SetActive(false);
            }
            else if (scripted_objects_object2.activeInHierarchy)
            {
                scripted_objects_object2.SetActive(false);
            }
            else if (scripted_objects_object.activeInHierarchy)
            {
                scripted_objects_object2.SetActive(false);
            }
        }
        

        

    }
}
