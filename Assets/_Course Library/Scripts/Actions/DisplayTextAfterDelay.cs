using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// TV remote toggles coroutine on and off
// coroutine a while loop

public class DisplayTextAfterDelay : MonoBehaviour
{
    [Tooltip("Text to display after x delay")] 
    [SerializeField] private List<TextStruct> textToDisplay;
    
    [SerializeField] private TMP_Text displayText;
    private bool captionsRunning;
    private bool coroutineRunning;
    
    [Serializable]
    public class TextStruct {
        public string text;
        public float delay;
    }

    /*public void ShowCaptions()
    {
        StartCoroutine(nameof(DisplayText));
    }*/

    /*private IEnumerator DisplayText()
    {
        int index = 0;
        yield return new WaitForSeconds(textToDisplay[index].delay);
        displayText.text = textToDisplay[index].text;
        index++;
    }*/

    public IEnumerator DisplayText ()
    {
        // Enable text gameObject when clip is running on the TV
        displayText.transform.parent.gameObject.SetActive(true);
        coroutineRunning = true;
        
        while (captionsRunning)
        {
            int index = 0;
            yield return new WaitForSeconds(textToDisplay[index].delay);
            displayText.text = textToDisplay[index].text;
            index++;
        }
        coroutineRunning = false;
        displayText.transform.parent.gameObject.SetActive(false);
    }

    public void ToggleCaptions()
    {
        captionsRunning = !captionsRunning;
        
        if(!coroutineRunning)
            StartCoroutine(nameof(DisplayText));
    }
}
