using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Locked : MonoBehaviour
{
    void Start() =>
        StartCoroutine(WaitForUnlock());

    IEnumerator WaitForUnlock()
    {
        while (File.Exists("orion.lock"))
            yield return null;

        SceneManager.LoadScene(1);
    }
}
