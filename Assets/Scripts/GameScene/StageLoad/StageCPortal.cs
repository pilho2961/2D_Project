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
    AudioSource audioSource;
    bool audioPlayed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && DataManager.Instance.data.isUnlock[0] && DataManager.Instance.data.isUnlock[1] &&
            DataManager.Instance.data.isUnlock[3] && DataManager.Instance.data.isUnlock[4])
        {
            createdUI = Instantiate(interactionUIPrefab, transform);
            SetUIPosition(createdUI, new Vector3(1000f, 1000f, 0f));

            text = createdUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "꿈에서 깨시겠습니까?";
        }
        else if (other.CompareTag("Player"))
        {
            createdUI = Instantiate(interactionUIPrefab, transform);
            SetUIPosition(createdUI, new Vector3(1000f, 1000f, 0f));

            text = createdUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "꿈의 기억을 모두 모아 오세요.";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (DataManager.Instance.data.isUnlock[0] && DataManager.Instance.data.isUnlock[1] &&
            DataManager.Instance.data.isUnlock[3] && DataManager.Instance.data.isUnlock[4])
        {
            if (Input.GetKey(KeyCode.G) && !audioSource.isPlaying)
            {
                audioSource.Play();
                audioPlayed = true;
            }

            if (audioPlayed && !audioSource.isPlaying)
            {
                audioPlayed = false;
                SceneLoader.Instance.StageCSceneLoad();
            }
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
