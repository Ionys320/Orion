using System.Collections;
using System.IO;
using UnityEngine;

public class FirstGlobalScript : MonoBehaviour
{
    public static bool cardSelected = false;
    public Texture O, X;
    public static string goodCard = "None";


    void Start()
    {
        using (StreamWriter writer = new StreamWriter("first-settings.orion", false))
        {
            writer.Write("[VICTORY]\nVICTORY_CARD=None");

            InvokeRepeating("CheckSettingsFile", 1f, 2f);
        }
    }

    async void CheckSettingsFile()
    {
        using (StreamReader sr = new StreamReader("first-settings.orion"))
        {
            string line = await sr.ReadToEndAsync();
            if (line.Contains("VICTORY_CARD=Left"))
                SetCards("Left", O, X, X);
            else if (line.Contains("VICTORY_CARD=Middle"))
                SetCards("Middle", X, O, X);
            else if (line.Contains("VICTORY_CARD=Right"))
                SetCards("Right", X, X, O);
            else if (line.Contains("VICTORY_CARD=All"))
                SetCards("All", O, O, O);
            else if (line.Contains("VICTORY_CARD=None"))
                SetCards("None", X, X, X);
        }
    }

    void SetCards(string card, Texture left, Texture middle, Texture right)
    {
        goodCard = card;
        GameObject.FindGameObjectWithTag("Left-Verso").GetComponent<Renderer>().material.SetTexture("_MainTex", left);
        GameObject.FindGameObjectWithTag("Middle-Verso").GetComponent<Renderer>().material.SetTexture("_MainTex", middle);
        GameObject.FindGameObjectWithTag("Right-Verso").GetComponent<Renderer>().material.SetTexture("_MainTex", right);
    }

}