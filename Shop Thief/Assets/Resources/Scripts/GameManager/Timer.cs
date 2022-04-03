using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : Functionalities
{
    #region variables
    [SerializeField] private TMP_Text timeText;
    [HideInInspector] public float time;
    #endregion

    public IEnumerator NextSecond()
    {
        SubstractSecond();
        yield return new WaitForSecondsRealtime(1f);

        if (!GameManager.Instance.infinityTime)
        {
            timeText.text = ConvertTimeSecondsToString(time);
            if (time > 0)
                StartCoroutine(NextSecond());
            else
            {
                var game = GameManager.Instance;
                if (game.overallPrice >= game.requireScore)
                    game.GameWin();
                else
                    game.GameOver();
            }
        }
        else
            timeText.text = "∞";
    }

    private void SubstractSecond()
    {
        if(time > 0)
            time = SubstractSecond(time);
    }

    private string ConvertTimeSecondsToString(float seconds)
    {
        int m = (int)seconds / 60;
        int s = (int)seconds - (m * 60);

        string min = m < 10 ? $"0{m}" : m.ToString();
        string sec = s < 10 ? $"0{s}" : s.ToString();
        return $"{min}:{sec}";
    }
}