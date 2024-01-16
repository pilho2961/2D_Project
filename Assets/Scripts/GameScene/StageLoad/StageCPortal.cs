using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StageCPortal : MonoBehaviour
{
    public GameObject interactionUIPrefab;
    public GameObject createdUI;
    TextMeshProUGUI text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            createdUI = Instantiate(interactionUIPrefab, transform);
            SetUIPosition(createdUI, new Vector3(1000f, 1000f, 0f));

            text = createdUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "²Þ¿¡¼­ ±ú½Ã°Ú½À´Ï±î?";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.G))
        {
            SceneLoader.Instance.StageCSceneLoad();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(createdUI);
        }
    }

    private void SetUIPosition(GameObject uiObject, Vector3 position)
    {
        RectTransform uiRectTransform = uiObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<RectTransform>();
        uiRectTransform.position = position;
    }
}
