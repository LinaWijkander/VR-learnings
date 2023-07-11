
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.ProceduralImage;

public class TrainingManager : MonoBehaviour
    {
        [SerializeField] private ProceduralImage progressBar;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text bestTimeText;
        [SerializeField] private int boxesToCompleteTraining = 10;
        [Serializable] public class ProgramStateEvent : UnityEvent<float> { }
        
        private int boxesCompleted = 0;
        private PlayerPrefsController playerPrefsController;
        private float trainingTimer;
        private float bestTime;
        private bool timerStarted;
        
        
        private void OnEnable()
        {
            boxesCompleted = playerPrefsController.GetInt("boxesCompleted");
            bestTime = playerPrefsController.GetFloat("bestTime");
            UpdateBestTimeUI();
            UpdateProgressBarUI();
        }
        
        private void Awake()
        {
            playerPrefsController = FindObjectOfType<PlayerPrefsController>();
        }

        // Update eller coroutine
        private void Update()
        {
            if (timerStarted)
            {
                trainingTimer += Time.deltaTime;
                timerText.text = $"Time: {trainingTimer}";
            }
        }

        private void OnApplicationQuit()
        {
            // Save the Current training progress
            playerPrefsController.SetInt("boxesCompleted", boxesCompleted);
            playerPrefsController.SetFloat("bestTimer", trainingTimer);
           // OnProgramQuit.Invoke(boxesCompleted);
        }

        public void StartTrainingTimer()
        {
            timerStarted = true;
            trainingTimer = 0;
        }
        
        public void StopTrainingTimer()
        {
            timerStarted = false;
            
            if (bestTime == 0 || trainingTimer < bestTime)
                bestTime = trainingTimer;
            
            UpdateBestTimeUI();
            trainingTimer = 0;
        }

        public void ResetProgress()
        {
            boxesCompleted = 0;
            UpdateProgressBarUI();
        }

        public void AddBoxCompleted()
        {
            if (boxesCompleted == boxesToCompleteTraining)
            {
                StopTrainingTimer();
                return;
            }
            boxesCompleted++;
            Debug.Log(("Boxes placed: " + boxesCompleted));
            UpdateProgressBarUI();
        }

        private void UpdateProgressBarUI()
        {
            progressBar.fillAmount = (float)boxesCompleted / boxesToCompleteTraining;
        }
        
        private void UpdateBestTimeUI()
        {
            bestTimeText.text = $"Best: {bestTime.ToString()}";
        }
    }
