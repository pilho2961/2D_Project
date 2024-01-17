using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StageEPortal : MonoBehaviour
{
    public GameObject interactionUIPrefab;
    public GameObject createdUI;
    TextMeshProUGUI text;
    private bool isActive = true;
    AudioSource audioSource;
    bool audioPlayed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (DataManager.Instance.data.isUnlock[4] && isActive)
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            createdUI = Instantiate(interactionUIPrefab, transform);
            SetUIPosition(createdUI, new Vector3(1600f, 900f, 0f));

            text = createdUI.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Stage E로 이동하시겠습니까?";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.G) && !audioSource.isPlaying)
        {
            audioSource.Play();
            audioPlayed = true;
        }

        if (audioPlayed && !audioSource.isPlaying)
        {
            audioPlayed = false;
            SceneLoader.Instance.StageESceneLoad();
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
