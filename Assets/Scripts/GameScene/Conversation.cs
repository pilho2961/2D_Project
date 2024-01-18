using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public TextMeshProUGUI lucidScript;
    private string script1 = "대충 여긴 어디지 하는 대사.";
    private bool script1Finished;
    [SerializeField] private GameObject tutorialUI;

    private void Start()
    {
        for (int i = 0; i < DataManager.Instance.data.isUnlock.Length; i++)
        {
            if (DataManager.Instance.data.isUnlock[i])
            {
                gameObject.SetActive(false);
            }
        }

        if (gameObject.activeSelf)
        {
            StartCoroutine(typingScript());
        }
    }

    private void Update()
    {
        EndConversation();
    }

    IEnumerator typingScript()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= script1.Length; i++)
        {
            lucidScript.text = script1.Substring(0, i);

            yield return new WaitForSeconds(0.15f);

            if (i == script1.Length)
            {
                script1Finished = true;
            }
        }
    }

    private void EndConversation()
    {
        if (script1Finished && Input.GetKey(KeyCode.Space))
        {
            tutorialUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
