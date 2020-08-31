using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCards : MonoBehaviour
{
    private GameObject card;
    public UnityEngine.UI.Text text;

    private void Start() =>
        card = transform.parent.gameObject;

    private void OnMouseUp() =>
        StartCoroutine(Treatment());

    IEnumerator Treatment()
    {
        if (!FirstGlobalScript.cardSelected)
        {
            FirstGlobalScript.cardSelected = true;
            iTween.RotateTo(card, new Vector3(0f, 180f, 0f), 2f);

            yield return new WaitForSeconds(0.6f);

            if (FirstGlobalScript.goodCard == "All" || FirstGlobalScript.goodCard == card.name)
            {
                text.text = "Bravo !";

                yield return new WaitForSeconds(2f);

                SceneManager.LoadScene(2);
            }
            else
            {
                text.text = "Dommage ! Mais retenez bien : le hasard n'existe pas.";

                yield return new WaitForSeconds(5f);

                Application.Quit();
            }
        }
    }
}
