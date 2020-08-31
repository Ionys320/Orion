using System.Collections;
using System.IO;
using UnityEngine;

public class OmegaBloc1 : MonoBehaviour
{
    public GameObject wallToOpen, pont, player;

    public bool enableGravityManipulation = false;

    public bool levelInEnd = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        iTween.Defaults.easeType = iTween.EaseType.linear;

        using (StreamWriter writer = new StreamWriter("omega-settings.orion", false))
        {
            writer.Write("[GRAVITY]\nENABLE_GRAVITY_MANIPULATION=False");

            InvokeRepeating("CheckSettingsFile", 1f, 2f);
        }
    }

    async void CheckSettingsFile()
    {
        using (StreamReader sr = new StreamReader("omega-settings.orion"))
        {
            string line = await sr.ReadToEndAsync();
            enableGravityManipulation = line.Contains("ENABLE_GRAVITY_MANIPULATION=True");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("g") && enableGravityManipulation && !levelInEnd)
        {
            if (transform.rotation.z == 1f)
                StartCoroutine(Rotate(0f));
            else if (transform.rotation.z == 0f)
                StartCoroutine(Rotate(180f));
        }
        else if (Input.GetKeyDown("n"))
        {
            StartCoroutine(EndingBloc1());
        }
    }

    IEnumerator Rotate(float zRotation)
    {
        //Set the player to a default position to avoid wall problems
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        iTween.MoveTo(player, new Vector3(0f, 16f, 0f), 2f);
        //Rotate the bloc 1
        iTween.RotateTo(this.gameObject, new Vector3(0, 0, zRotation), 8f);
        //Wait the end of the rotation
        yield return new WaitForSeconds(8f);
        //Allow player to move
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public IEnumerator EndingBloc1()
    {
        StartCoroutine(Rotate(0f));
        yield return new WaitForSeconds(8f);
        iTween.MoveTo(wallToOpen, new Vector3(wallToOpen.transform.position.x, 48f, wallToOpen.transform.position.z), 8f);
        yield return new WaitForSeconds(3f);
        iTween.MoveTo(pont, new Vector3(pont.transform.position.x, pont.transform.position.y, -32), 8f);

        levelInEnd = true;
    }
}
