using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OmegaPlayer : MonoBehaviour
{
    public GameObject wallOfBloc1, wallOfBloc2, topOfBloc2, pont, activationCubesElement;
    public Material deactivated, activated;
    private List<Transform> activationCubes = new List<Transform>();
    private int lastActivatedCube = -1;
    private Vector3 originalPosition;
    public OmegaBloc1 omegaBloc1;

    void Start()
    {
        originalPosition = transform.position;
        if (activationCubesElement != null)
        {
            for (int n = 0; n < activationCubesElement.transform.childCount; n++)
            {
                activationCubes.Add(activationCubesElement.transform.GetChild(n));
            }
        }
    }

    void Update()
    {
        if (transform.position.y > 30 || transform.position.y < -6)
            transform.position = originalPosition;

        if (topOfBloc2.transform.position.y <= 2f)
            SceneManager.LoadScene(3);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "EmptySpace")
            transform.position = originalPosition;
        else if (collider.name == "GoodbyeBloc1")
            StartCoroutine(GoodbyeBloc1());
        else if (collider.name == "WelcomeBloc2")
            StartCoroutine(StartingBloc2());
        else if (collider.tag == "ActivationCube")
        {
            if (activationCubes[lastActivatedCube + 1].name == collider.name)
            {
                lastActivatedCube++;
                activationCubes[lastActivatedCube].GetComponent<Renderer>().material = activated;
                for (int i = 0; i < activationCubes[lastActivatedCube].transform.childCount; i++)
                {
                    activationCubes[lastActivatedCube].transform.GetChild(i).GetComponent<Renderer>().material = activated;
                }
                
                if (lastActivatedCube + 1 == activationCubes.Count)
                    StartCoroutine(omegaBloc1.EndingBloc1());
            }
            else if (activationCubes[lastActivatedCube].name != collider.name)
            {
                lastActivatedCube++;
                foreach (var item in activationCubes)
                {
                    item.GetComponent<Renderer>().material = deactivated;
                    for (int i = 0; i < item.transform.childCount; i++)
                    {
                        item.transform.GetChild(i).GetComponent<Renderer>().material = deactivated;
                    }
                }
                lastActivatedCube = -1;
            }

        }
    }

    IEnumerator GoodbyeBloc1()
    {
        iTween.MoveTo(wallOfBloc2, new Vector3(wallOfBloc2.transform.position.x, 48f, wallOfBloc2.transform.position.z), 8f);
        yield return new WaitForSeconds(2f);
        iTween.MoveTo(wallOfBloc1, new Vector3(wallOfBloc1.transform.position.x, 16f, wallOfBloc1.transform.position.z), 8f);
    }

    IEnumerator StartingBloc2()
    {
        iTween.MoveTo(pont, new Vector3(pont.transform.position.x, pont.transform.position.y, 0), 8f);
        yield return new WaitForSeconds(2f);
        iTween.MoveTo(wallOfBloc2, new Vector3(wallOfBloc2.transform.position.x, 16f, wallOfBloc2.transform.position.z), 8f);
        yield return new WaitForSeconds(10f);
        iTween.MoveTo(topOfBloc2, new Vector3(topOfBloc2.transform.position.x, 0f, topOfBloc2.transform.position.z), 16f);

    }
}
