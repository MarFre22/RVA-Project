// Project RVA

using UnityEngine;
using System.IO;


public class ReadData : MonoBehaviour
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

    



    void DrawCube(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color colour)
    {
        GameObject go;
        MeshFilter mf;
        MeshRenderer mr;
        Mesh m;
        Vector3[] vertices = {
            new Vector3 ((p2.x - 345), 0.1f, (p2.y - 227)),
            new Vector3 ((p1.x - 345), 0.1f, (p1.y - 227)),
            new Vector3 ((p1.x - 345), 0.1f + p1.z, (p1.y - 227)),
            new Vector3 ((p2.x - 345), 0.1f + p2.z, (p2.y - 227)),
            new Vector3 ((p3.x - 345), 0.1f + p3.z, (p3.y - 227)),
            new Vector3 ((p4.x - 345), 0.1f + p4.z, (p4.y - 227)),
            new Vector3 ((p4.x - 345), 0.1f, (p4.y - 227)),
            new Vector3 ((p3.x - 345), 0.1f, (p3.y - 227)),
        };
        Vector3[] verticesAveiro = {
            new Vector3 ((p2.x - 405), 0.1f, (p2.y - 515)),
            new Vector3 ((p1.x - 405), 0.1f, (p1.y - 515)),
            new Vector3 ((p1.x - 405), 0.1f + p1.z, (p1.y - 515)),
            new Vector3 ((p2.x - 405), 0.1f + p2.z, (p2.y - 515)),
            new Vector3 ((p3.x - 405), 0.1f + p3.z, (p3.y - 515)),
            new Vector3 ((p4.x - 405), 0.1f + p4.z, (p4.y - 515)),
            new Vector3 ((p4.x - 405), 0.1f, (p4.y - 515)),
            new Vector3 ((p3.x - 405), 0.1f, (p3.y - 515)),
        };
        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        go = new GameObject("Cube");
        mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        mr.material.color = colour;
        m = new Mesh();
        if (RuaAveiro)
            m.vertices = verticesAveiro;
        else
            m.vertices = vertices;
        m.triangles = triangles;
        mf.mesh = m;
        m.RecalculateNormals();
        if (RuaAveiro)
        {
            go.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.79f));
        }
        else
            go.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.75f));
    }

    void DrawPlane(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color colour)
    {
        MeshFilter mf;
        MeshRenderer mr;
        Mesh m;

        if (RuaAveiro)
        {
            PollutionGO_Planes_Aveiro[countPlanes] = new GameObject("Plane");
            mf = PollutionGO_Planes_Aveiro[countPlanes].AddComponent(typeof(MeshFilter)) as MeshFilter;
            mr = PollutionGO_Planes_Aveiro[countPlanes].AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        }
        else
        {
            PollutionGO_Planes[countPlanes] = new GameObject("Plane");
            mf = PollutionGO_Planes[countPlanes].AddComponent(typeof(MeshFilter)) as MeshFilter;
            mr = PollutionGO_Planes[countPlanes].AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        }
        mr.material.color = colour;
        m = new Mesh();
        if (RuaAveiro)
        {
            m.vertices = new Vector3[] {
                new Vector3 ((p1.x - 405), p1.z + 0.1f, (p1.y - 515)),
                new Vector3 ((p2.x - 405), p2.z + 0.1f, (p2.y - 515)),
                new Vector3 ((p3.x - 405), p3.z + 0.1f, (p3.y - 515)),
                new Vector3 ((p4.x - 405), p4.z + 0.1f, (p4.y - 515)),
            };
        }
        else
        {
            m.vertices = new Vector3[] {
                new Vector3 ((p1.x - 345), p1.z + 0.1f, (p1.y - 227)),
                new Vector3 ((p2.x - 345), p2.z + 0.1f, (p2.y - 227)),
                new Vector3 ((p3.x - 345), p3.z + 0.1f, (p3.y - 227)),
                new Vector3 ((p4.x - 345), p4.z + 0.1f, (p4.y - 227)),
            };
        }
        m.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };
        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mf.mesh = m;
        m.RecalculateBounds();
        m.RecalculateNormals();
        if (RuaAveiro)
        {
            PollutionGO_Planes_Aveiro[countPlanes].transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.79f));
        }
        else
        {
            PollutionGO_Planes[countPlanes].transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.75f));
        }
        countPlanes++;
    }

    void DrawLine(Vector3 p1, Vector3 p2)
    {
        GameObject myLine;
        LineRenderer lr;

        myLine = new GameObject();
        myLine.transform.position = Vector3.zero;
        myLine.AddComponent<LineRenderer>();
        lr = myLine.GetComponent<LineRenderer>();
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);
        if (RuaAveiro)
        {
            myLine.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.79f));
        }
        else
            myLine.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.77f, 1, 0.75f));
    }

    void ReadDataFile(string path, Color c, bool threeD, int type)
    {
        StreamReader reader = new StreamReader(path);
        string line, str;
        int i, j, countL = 0, countP = 0;
        double[] array = new double[4768];

        while ((line = reader.ReadLine()) != null)
        {
            str = string.Empty;
            j = countL * 4;
            for (i = 0; i < line.Length; i++)
            {
                if (System.Char.IsDigit(line[i]) || line[i] == '.')
                    str += line[i];
                else
                {
                    double.TryParse(str, out array[j]);
                    if (str.Length > 0)
                        j++;
                    str = string.Empty;
                }
            }
            double.TryParse(str, out array[j]);
            if (threeD == false)
            {
                j++;
                array[j] = 0.0;
            }
            countL++;
            countP++;
            if (countP == 4)
            {
                countP = 0;
                switch (type)
                {
                    case 0:
                        DrawPlane(new Vector3((float)array[j - 14], (float)array[j - 13], (float)array[j - 12]), new Vector3((float)array[j - 10], (float)array[j - 9], (float)array[j - 8]), new Vector3((float)array[j - 6], (float)array[j - 5], (float)array[j - 4]), new Vector3((float)array[j - 2], (float)array[j - 1], (float)array[j]), c);
                        break;
                    case 1:
                        DrawCube(new Vector3((float)array[j - 14], (float)array[j - 13], (float)array[j - 12]), new Vector3((float)array[j - 10], (float)array[j - 9], (float)array[j - 8]), new Vector3((float)array[j - 6], (float)array[j - 5], (float)array[j - 4]), new Vector3((float)array[j - 2], (float)array[j - 1], (float)array[j]), c);
                        break;
                }
            }
        }
        reader.Close();
    }

    void Start()
    {
        StreamReader reader;
        Renderer renderer;
        string line, str;
        int k, l;
        float j;
        float RaioEsfera = 3, RaioEsfera_Aveiro = 2;

        if (RuaAveiro)
        {
            //MapsService.MoveFloatingOrigin(new LatLng(40.637584, -8.648416));
//            ReadDataFile("Assets/PollutionData/25AbrilRoads.txt", Color.black, false, 0);
//            ReadDataFile("Assets/PollutionData/25AbrilTrees.txt", Color.green, false, 1);
//            ReadDataFile("Assets/PollutionData/25AbrilBuildings.txt", Color.red, false, 1);
//            for (j = -370; j <= 270; j = j + 2)
//                DrawLine(new Vector3(-250, 100, j), new Vector3(390, 100, j));
//            for (j = -250; j <= 390; j = j + 2)
//                DrawLine(new Vector3(j, 100, -370), new Vector3(j, 100, 270));
            reader = new StreamReader("Assets/PollutionData/25AbrilCvxyk1_9.dat");
            l = 0;
            while ((line = reader.ReadLine()) != null)
            {
                str = string.Empty;
                for (k = 0; k < line.Length; k++)
                {
                    if (System.Char.IsDigit(line[k]) || line[k] == '.')
                        str += line[k];
                    else
                    {
                        float.TryParse(str, out PollutionData_Aveiro[l]);
                        if (str.Length > 0)
                        {
                            l++;
                            if (l % 3 == 0)
                                k = line.Length;
                        }
                        str = string.Empty;
                    }
                }
                float.TryParse(str, out PollutionData_Aveiro[l]);
            }
            for (k = 0; k < l; k = k + 3)
            {
                if (PollutionData_Aveiro[k + 2] < 25)
                {
                    if (PollutionData_Aveiro[k + 2] == 0)
                        DrawPlane(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), Color.green);
                    else
                        DrawPlane(new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), Color.green);
                }
                else
                {
                    if (PollutionData_Aveiro[k + 2] < 200)
                        DrawPlane(new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), Color.yellow);
                    else
                        DrawPlane(new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionData_Aveiro[k] - 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionData_Aveiro[k] + 1, PollutionData_Aveiro[k + 1] - 1, 1.5f), Color.red);
                }
                PollutionGO_Planes_Aveiro[countPlanes - 1].SetActive(false);
                PollutionGO_Spheres_Aveiro[count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                PollutionGO_Spheres_Aveiro[count].name = count.ToString();
                renderer = PollutionGO_Spheres_Aveiro[count].GetComponent<Renderer>();
                if (PollutionData_Aveiro[k + 2] < 10)
                {
                    renderer.material.color = Color.green;
                    if (PollutionData_Aveiro[k + 2] > 0)
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(RaioEsfera_Aveiro / 5, RaioEsfera_Aveiro / 5, RaioEsfera_Aveiro / 5);
                    else
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    if (PollutionData_Aveiro[k + 2] < 25)
                    {
                        renderer.material.color = Color.green;
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(2 * RaioEsfera_Aveiro / 5, 2 * RaioEsfera_Aveiro / 5, 2 * RaioEsfera_Aveiro / 5);
                    }
                    else
                    {
                        if (PollutionData_Aveiro[k + 2] < 100)
                        {
                            renderer.material.color = Color.yellow;
                            PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(3 * RaioEsfera_Aveiro / 5, 3 * RaioEsfera_Aveiro / 5, 3 * RaioEsfera_Aveiro / 5);
                        }
                        else
                        {
                            if (PollutionData_Aveiro[k + 2] < 200)
                            {
                                renderer.material.color = Color.yellow;
                                PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(4 * RaioEsfera_Aveiro / 5, 4 * RaioEsfera_Aveiro / 5, 4 * RaioEsfera_Aveiro / 5);
                            }
                            else
                            {
                                renderer.material.color = Color.red;
                                PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(RaioEsfera_Aveiro, RaioEsfera_Aveiro, RaioEsfera_Aveiro);
                            }
                        }
                    }
                }
                PollutionGO_Spheres_Aveiro[count].transform.position = new Vector3(0.77f * (PollutionData_Aveiro[k] - 405), 1.5f, 0.79f * (PollutionData_Aveiro[k + 1] - 515));
                PollutionGO_Spheres_Aveiro[count].SetActive(false);
                count++;
            }
            reader = new StreamReader("Assets/PollutionData/25AbrilCvxyk1_9.dat");
            l = 0;
            while ((line = reader.ReadLine()) != null)
            {
                str = string.Empty;
                for (k = 0; k < line.Length; k++)
                {
                    if (System.Char.IsDigit(line[k]) || line[k] == '.')
                        str += line[k];
                    else
                    {
                        float.TryParse(str, out PollutionDataExtra_Aveiro[l]);
                        if (str.Length > 0)
                        {
                            l++;
                            if (l % 3 == 0)
                                k = line.Length;
                        }
                        str = string.Empty;
                    }
                }
                float.TryParse(str, out PollutionDataExtra_Aveiro[l]);
            }
            for (k = 0; k < l; k = k + 3)
            {
                if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 25)
                {
                    if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] == 0)
                        DrawPlane(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), Color.green);
                    else
                        DrawPlane(new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), Color.green);
                }
                else
                {
                    if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 200)
                        DrawPlane(new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), Color.yellow);
                    else
                        DrawPlane(new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] - 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] + 1, 1.5f), new Vector3(PollutionDataExtra_Aveiro[k] + 1, PollutionDataExtra_Aveiro[k + 1] - 1, 1.5f), Color.red);
                }
                PollutionGO_Planes_Aveiro[countPlanes - 1].SetActive(false);
                PollutionGO_Spheres_Aveiro[count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                PollutionGO_Spheres_Aveiro[count].name = count.ToString();
                renderer = PollutionGO_Spheres_Aveiro[count].GetComponent<Renderer>();
                if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 10)
                {
                    renderer.material.color = Color.green;
                    if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] > 0)
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(RaioEsfera / 5, RaioEsfera / 5, RaioEsfera / 5);
                    else
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 25)
                    {
                        renderer.material.color = Color.green;
                        PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(2 * RaioEsfera / 5, 2 * RaioEsfera / 5, 2 * RaioEsfera / 5);
                    }
                    else
                    {
                        if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 100)
                        {
                            renderer.material.color = Color.yellow;
                            PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(3 * RaioEsfera / 5, 3 * RaioEsfera / 5, 3 * RaioEsfera / 5);
                        }
                        else
                        {
                            if (fatorEscala * PollutionDataExtra_Aveiro[k + 2] < 200)
                            {
                                renderer.material.color = Color.yellow;
                                PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(4 * RaioEsfera / 5, 4 * RaioEsfera / 5, 4 * RaioEsfera / 5);
                            }
                            else
                            {
                                renderer.material.color = Color.red;
                                PollutionGO_Spheres_Aveiro[count].transform.localScale = new Vector3(RaioEsfera, RaioEsfera, RaioEsfera);
                            }
                        }
                    }
                }
                PollutionGO_Spheres_Aveiro[count].transform.position = new Vector3(0.77f * (PollutionData_Aveiro[k] - 405), 1.5f, 0.79f * (PollutionData_Aveiro[k + 1] - 515));
                PollutionGO_Spheres_Aveiro[count].SetActive(false);
                count++;
            }
        }
        else
        {
            ReadDataFile("Assets/PollutionData/ConstituicaoRoads.txt", Color.black, false, 0);
            //ReadDataFile("Assets/PollutionData/ConstituicaoTrees.txt", Color.green, true, 1);
            //ReadDataFile("Assets/PollutionData/ConstituicaoBuildings.txt", Color.red, true, 1);
            
            reader = new StreamReader("Assets/PollutionData/ConstituicaoB_NOx_9.dat");





            l = 0;
            while ((line = reader.ReadLine()) != null)
            {
                str = string.Empty;
                for (k = 0; k < line.Length; k++)
                {
                    if (System.Char.IsDigit(line[k]) || line[k] == '.')
                        str += line[k];
                    else
                    {
                        float.TryParse(str, out PollutionData[l]);
                        if (str.Length > 0)
                        {
                            l++;
                            if (l % 3 == 0)
                                k = line.Length;
                        }
                        str = string.Empty;
                    }
                }
                float.TryParse(str, out PollutionData[l]);
            }
            for (k = 0; k < l; k = k + 3)
            {
                if (PollutionData[k + 2] < 25)
                {
                    if (PollutionData[k + 2] == 0)
                        DrawPlane(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), Color.green);
                    else
                        DrawPlane(new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), Color.green);
                }
                else
                {
                    if (PollutionData[k + 2] < 200)
                        DrawPlane(new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), Color.yellow);
                    else
                        DrawPlane(new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), new Vector3(PollutionData[k] - 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] + 1.5f, 1.5f), new Vector3(PollutionData[k] + 1.5f, PollutionData[k + 1] - 1.5f, 1.5f), Color.red);
                }
                PollutionGO_Planes[countPlanes - 1].SetActive(false);
                PollutionGO_Spheres[count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                PollutionGO_Spheres[count].name = count.ToString();
                renderer = PollutionGO_Spheres[count].GetComponent<Renderer>();
                if (PollutionData[k + 2] < 10)
                {
                    renderer.material.color = Color.green;
                    if (PollutionData[k + 2] > 0)
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(RaioEsfera / 5, RaioEsfera / 5, RaioEsfera / 5);
                    else
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    if (PollutionData[k + 2] < 25)
                    {
                        renderer.material.color = Color.green;
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(2 * RaioEsfera / 5, 2 * RaioEsfera / 5, 2 * RaioEsfera / 5);
                    }
                    else
                    {
                        if (PollutionData[k + 2] < 100)
                        {
                            renderer.material.color = Color.yellow;
                            PollutionGO_Spheres[count].transform.localScale = new Vector3(3 * RaioEsfera / 5, 3 * RaioEsfera / 5, 3 * RaioEsfera / 5);
                        }
                        else
                        {
                            if (PollutionData[k + 2] < 200)
                            {
                                renderer.material.color = Color.yellow;
                                PollutionGO_Spheres[count].transform.localScale = new Vector3(4 * RaioEsfera / 5, 4 * RaioEsfera / 5, 4 * RaioEsfera / 5);
                            }
                            else
                            {
                                renderer.material.color = Color.red;
                                PollutionGO_Spheres[count].transform.localScale = new Vector3(RaioEsfera, RaioEsfera, RaioEsfera);
                            }
                        }
                    }
                }
                PollutionGO_Spheres[count].transform.position = new Vector3(0.77f * (PollutionData[k] - 345), 1.5f, 0.75f * (PollutionData[k + 1] - 227));
                PollutionGO_Spheres[count].SetActive(false);
                count++;
            }
            reader = new StreamReader("Assets/PollutionData/ConstituicaoB_Media.dat");
            l = 0;
            while ((line = reader.ReadLine()) != null)
            {
                str = string.Empty;
                for (k = 0; k < line.Length; k++)
                {
                    if (System.Char.IsDigit(line[k]) || line[k] == '.')
                        str += line[k];
                    else
                    {
                        float.TryParse(str, out PollutionDataExtra[l]);
                        if (str.Length > 0)
                        {
                            l++;
                            if (l % 3 == 0)
                                k = line.Length;
                        }
                        str = string.Empty;
                    }
                }
                float.TryParse(str, out PollutionDataExtra[l]);
            }
            for (k = 0; k < l; k = k + 3)
            {
                if (fatorEscala * PollutionDataExtra[k + 2] < 25)
                {
                    if (fatorEscala * PollutionDataExtra[k + 2] == 0)
                        DrawPlane(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), Color.green);
                    else
                        DrawPlane(new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), Color.green);
                }
                else
                {
                    if (fatorEscala * PollutionDataExtra[k + 2] < 200)
                        DrawPlane(new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), Color.yellow);
                    else
                        DrawPlane(new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] - 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] + 1.5f, 1.5f), new Vector3(PollutionDataExtra[k] + 1.5f, PollutionDataExtra[k + 1] - 1.5f, 1.5f), Color.red);
                }
                PollutionGO_Planes[countPlanes - 1].SetActive(false);
                PollutionGO_Spheres[count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                PollutionGO_Spheres[count].name = count.ToString();
                renderer = PollutionGO_Spheres[count].GetComponent<Renderer>();
                if (fatorEscala * PollutionDataExtra[k + 2] < 10)
                {
                    renderer.material.color = Color.green;
                    if (fatorEscala * PollutionDataExtra[k + 2] > 0)
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(RaioEsfera / 5, RaioEsfera / 5, RaioEsfera / 5);
                    else
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    if (fatorEscala * PollutionDataExtra[k + 2] < 25)
                    {
                        renderer.material.color = Color.green;
                        PollutionGO_Spheres[count].transform.localScale = new Vector3(2 * RaioEsfera / 5, 2 * RaioEsfera / 5, 2 * RaioEsfera / 5);
                    }
                    else
                    {
                        if (fatorEscala * PollutionDataExtra[k + 2] < 100)
                        {
                            renderer.material.color = Color.yellow;
                            PollutionGO_Spheres[count].transform.localScale = new Vector3(3 * RaioEsfera / 5, 3 * RaioEsfera / 5, 3 * RaioEsfera / 5);
                        }
                        else
                        {
                            if (fatorEscala * PollutionDataExtra[k + 2] < 200)
                            {
                                renderer.material.color = Color.yellow;
                                PollutionGO_Spheres[count].transform.localScale = new Vector3(4 * RaioEsfera / 5, 4 * RaioEsfera / 5, 4 * RaioEsfera / 5);
                            }
                            else
                            {
                                renderer.material.color = Color.red;
                                PollutionGO_Spheres[count].transform.localScale = new Vector3(RaioEsfera, RaioEsfera, RaioEsfera);
                            }
                        }
                    }
                }
                PollutionGO_Spheres[count].transform.position = new Vector3(0.77f * (PollutionDataExtra[k] - 345), 1.5f, 0.75f * (PollutionDataExtra[k + 1] - 227));
                PollutionGO_Spheres[count].SetActive(false);
                count++;
            }
        }
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            press = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            press = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            press = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            press = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            press = 4;
        }
        if (press == 0)
        {
            if (RuaAveiro)
            {
                for (count = 0; count < 2 * 229443; count++)
                {
                    if (PollutionGO_Spheres_Aveiro[count] != null)
                        PollutionGO_Spheres_Aveiro[count].SetActive(false);
                    if (PollutionGO_Planes_Aveiro[count] != null)
                        PollutionGO_Planes_Aveiro[count].SetActive(false);
                }
            }
            else
            {
                for (count = 0; count < 2 * 63003; count++)
                {
                    PollutionGO_Spheres[count].SetActive(false);
                    PollutionGO_Planes[count].SetActive(false);
                }
            }
            press = -1;
        }
        if (press == 1)
        {
            if (RuaAveiro)
            {
                for (count = 0; count < 229443; count++)
                    PollutionGO_Spheres_Aveiro[count].SetActive(true);
            }
            else
            {
                for (count = 0; count < 63003; count++)
                    PollutionGO_Spheres[count].SetActive(true);
            }
            press = -1;
        }
        if (press == 2)
        {
            if (RuaAveiro)
            {
                for (count = 0; count < 229443; count++)
                    PollutionGO_Planes_Aveiro[count].SetActive(true);
            }
            else
            {
                for (count = 0; count < 63003; count++)
                    PollutionGO_Planes[count].SetActive(true);
            }
            press = -1;
        }
        if (press == 3)
        {
            if (RuaAveiro)
            {
                for (count = 229443; count < 2 * 229443; count++)
                    PollutionGO_Spheres_Aveiro[count].SetActive(true);
            }
            else
            {
                for (count = 63003; count < 2 * 63003; count++)
                    PollutionGO_Spheres[count].SetActive(true);
            }
            press = -1;
        }
        if (press == 4)
        {
            if (RuaAveiro)
            {
                for (count = 229443; count < 2 * 229443; count++)
                    PollutionGO_Planes_Aveiro[count].SetActive(true);
            }
            else
            {
                for (count = 63003; count < 2 * 63003; count++)
                    PollutionGO_Planes[count].SetActive(true);
            }
            press = -1;
        }
    }
}