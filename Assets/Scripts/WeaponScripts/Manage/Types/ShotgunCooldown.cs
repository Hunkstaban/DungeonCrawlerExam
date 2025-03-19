using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunCooldown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coolDownText;

    public IEnumerator CoolDownDisplay(float coolDownTime)
    {
        float remainingTime = coolDownTime;
        
        while (remainingTime > 0)
        {
            coolDownText.SetText(remainingTime.ToString());
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }
}