using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int defencePoints = 10;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private TMP_Text defencePointsText;

    // Atm GameManager and UI (Menu) is mixed
    private void Start()
    {
        defencePointsText.text = defencePoints.ToString();
    }

    private void OnEnable()
    {
        Enemy.EnemyInFountain += EnemyInFountain;
        EnemyManager.GameOver += OnGameOver;
    }
    void OnDisable() 
    { 
        Enemy.EnemyInFountain -= EnemyInFountain; 
        EnemyManager.GameOver -= OnGameOver;
    }


    private void EnemyInFountain(Enemy enemy)
    {
        defencePoints--;
        UpdateDefencePointsText();

        if (defencePoints <= 0)
        {
            FadeInText(loseText);
            EnemyManager.Instance.DestroyThyself();
        }
    }

    private void FadeInText(GameObject text)
    {
        text.SetActive(true);
        StartCoroutine(IncreaseAlphaOverTime(text));
    }
    
    private void UpdateDefencePointsText()
    {
        defencePointsText.text = defencePoints.ToString();
    }

    private IEnumerator IncreaseAlphaOverTime(GameObject textObject)
    {
        CanvasGroup cg = textObject.GetComponent<CanvasGroup>();
        while (cg.alpha < 1)
        {
            cg.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGameOver()
    {
        winText.SetActive(true);
    }
}
