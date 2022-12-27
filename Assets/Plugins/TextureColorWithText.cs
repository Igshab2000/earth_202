using UnityEngine;
using System.Collections.Generic;
using System;

public class TextureColorWithText : MonoBehaviour
{   
    public Dictionary<System.Drawing.Color, string> dictionary = new Dictionary<System.Drawing.Color, string>();
    
    public Camera cam;

    int screenWidth = Screen.width;
    int screenHeight = Screen.height;

    private bool showText = false, someRandomCondition = true;
    private float currentTime = 0.0f, executeTime = 0.0f, timeToWait = 5.0f;
    private int counter = 0, timeout = 0, timelevel = 0, timelimit = 80;

    int tolerance = 10;
    // bool locationFound = false;

    string toDisplay = "Давайте начнем!";

    public bool IsInValidRange(Color32 guess, System.Drawing.Color check, int threshold)
    {
        if( check.R-threshold <= guess.r && check.R+threshold >= guess.r && 
            check.G-threshold <= guess.g && check.G+threshold >= guess.g &&
            check.B-threshold <= guess.b && check.B+threshold >= guess.b ) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnGUI()
    {   
        GUI.skin.label.fontSize = 70;

        if (showText)
        {
            // (Screen.width / 2) - (toDisplay.Length / 2)
            GUI.Label(new Rect(40, 40, 2000, 500), toDisplay);

            timeout++;
            if (timeout == timelimit)
            {
                timeout = 0;
                if (timelevel == 1)
                {
                    showText = false;
                    timelevel = 0;
                    counter++;

                    //if(locationFound) {
                        // Goto next question
                    //}
                    
                }
                else timelevel++;
            }
        }
    }

    void Start()
    {
        cam = Camera.main;
        
        dictionary.Add(System.Drawing.Color.FromArgb(252,227,88), "Германия");
		dictionary.Add(System.Drawing.Color.FromArgb(249,207,219), "Россия");
        dictionary.Add(System.Drawing.Color.FromArgb(185, 255, 55), "Аргентина");
        dictionary.Add(System.Drawing.Color.FromArgb(0, 198, 188), "Бразилия");
        dictionary.Add(System.Drawing.Color.FromArgb(181, 91, 181), "Испания");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 138, 60), "Япония");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 243, 15), "Южная Корея");
        dictionary.Add(System.Drawing.Color.FromArgb(34, 177, 76), "Италия");
        dictionary.Add(System.Drawing.Color.FromArgb(91, 64, 247), "Афганистан");
        dictionary.Add(System.Drawing.Color.FromArgb(64, 171, 242), "Египет");
        dictionary.Add(System.Drawing.Color.FromArgb(247, 88, 49), "Турция");
        dictionary.Add(System.Drawing.Color.FromArgb(196, 243, 95), "Монголия");
        dictionary.Add(System.Drawing.Color.FromArgb(254, 90, 119), "Индия");
        dictionary.Add(System.Drawing.Color.FromArgb(203, 182, 141), "Конго");
        dictionary.Add(System.Drawing.Color.FromArgb(136, 189, 157), "апуа - Новая Гвинея");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 9, 9), "Тайвань");
        dictionary.Add(System.Drawing.Color.FromArgb(166, 191, 106), "Нидерланды");
        dictionary.Add(System.Drawing.Color.FromArgb(143, 35, 35), "Дания");
        dictionary.Add(System.Drawing.Color.FromArgb(131, 165, 245), "Казахстан");
        dictionary.Add(System.Drawing.Color.FromArgb(255, 220, 113), "Китай");
        dictionary.Add(System.Drawing.Color.FromArgb(211, 184, 199), "США");
        dictionary.Add(System.Drawing.Color.FromArgb(243, 148, 176), "Канада");
        dictionary.Add(System.Drawing.Color.FromArgb(204, 141, 123), "Саудовская Аравия");
        dictionary.Add(System.Drawing.Color.FromArgb(130, 154, 129), "Мексика");
        dictionary.Add(System.Drawing.Color.FromArgb(188, 241, 139), "Нигер");
        dictionary.Add(System.Drawing.Color.FromArgb(222, 250, 197), "Нигерия");
        dictionary.Add(System.Drawing.Color.FromArgb(77, 234, 199), "Франция");
        dictionary.Add(System.Drawing.Color.FromArgb(85, 60, 200), "Украина(Россия)");
        dictionary.Add(System.Drawing.Color.FromArgb(159, 130, 247), "Оман");
        dictionary.Add(System.Drawing.Color.FromArgb(150, 20, 230), "Зимбабве");
        dictionary.Add(System.Drawing.Color.FromArgb(10, 150, 20), "Новая Зеландия");
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        // Debug.Log("XY::"+pixelUV);
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        // Debug.Log("X::"+pixelUV.x +" Y::"+pixelUV.y);
        Color32 c;

        c = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

        // Debug.Log("Color::"+ c);

        // if (dictionary.ContainsKey(System.Drawing.Color.FromArgb(c.r, c.g, c.b)))
        // {
        //     Debug.Log(dictionary[System.Drawing.Color.FromArgb(c.r, c.g, c.b)]);
        // }
        // else
        // {
        //     Debug.Log("Better luck next time");
        // }

        if(Input.GetMouseButtonDown(0))
        {
            // showText = true;

            foreach (KeyValuePair<System.Drawing.Color, string> entry in dictionary)
            {
                // do something with entry.Value or entry.Key
                System.Drawing.Color checkColor = entry.Key;
    
                if (IsInValidRange(c, checkColor, tolerance)) 
                {
                    Debug.Log("Эта страна - " + entry.Value);
                    toDisplay = String.Concat("Вы нашли ", entry.Value);
                    showText = true;
                    // locationFound = true;
                    break;
                }
                // else
                // {
                //     Debug.Log("Not Found");
                //     continue;
                // }
            }
        }

    }

}