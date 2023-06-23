using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    [Header("Health Display")]
    [SerializeField] Image[] heartImages;
    [SerializeField] Image healthBarImage;

    #region Demo Area
    [Header("Demo Display")]
    [SerializeField] float health = 100;
    /// <summary>
    /// reduce health by this amount every waitTime tick
    /// </summary>
    [SerializeField] float healthLost = 0.5f;
    /// <summary>
    /// in seconds
    /// </summary>
    [Range(0,1)]
    [SerializeField] float waitTime = 0.1f;

    /// <summary>
    /// The number of pieces in a heart
    /// </summary>
    [SerializeField] int heartPieces = 4;

    /// <summary>
    /// The maximum value that health can be
    /// </summary>
    float maxHealth = 100;

    /// <summary>
    /// The percentage that each heart takes
    /// </summary>
    float HeartDivisionValue
    {
        get
        {
            return maxHealth / heartImages.Length;
        }
    }

    private void Start()
    {
        StartCoroutine(ReduceHealth());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) StartCoroutine(ReduceHealth());
    }

    IEnumerator ReduceHealth()
    {
        while(health > 0)
        {
            yield return new WaitForSeconds(waitTime);

            health -= healthLost;
            DisplayHealth();
        }
    }
    #endregion Demo Area

    void DisplayHealth()
    {
        int heartsFilled = Mathf.FloorToInt(health / HeartDivisionValue);
        float percentLeft = (health - (heartsFilled * HeartDivisionValue)) / HeartDivisionValue;

        healthBarImage.fillAmount = percentLeft;

        for(int i = 0; i < heartImages.Length; i++)
        {
            if (i < heartsFilled) heartImages[i].fillAmount = 1;
            else if (i == heartsFilled) heartImages[i].fillAmount = Mathf.Round(percentLeft*heartPieces)/heartPieces;
        }
    }
}
