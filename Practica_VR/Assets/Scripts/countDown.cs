using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countDown : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TimeManager timeManager;
    bool countDownActive = false;

    private void Start()
    {
        time = timeManager.maxGameTime;

        int minInt = (int)(time) / 60;
        string min = minInt.ToString();
        string sec = ((int)(time - minInt * 60)).ToString();
        if (sec.Length == 1) sec = "0" + sec;

        countText.text = min.ToString() + ':' + sec.ToString();
    }

    public void startCount()
    {
        countDownActive = true;
    }

    //Función que actualiza la cuenta atrás en cada frame
    void Update()
    {
        if (countDownActive)
        {
            time -= Time.deltaTime;
            string min, sec;
            if (time > 0f)
            {
                int minInt = (int)(time) / 60;
                min = minInt.ToString();
                sec = ((int)(time - minInt * 60)).ToString();
                if (sec.Length == 1) sec = "0" + sec;
            }
            else
            {
                min = "0";
                sec = "00";
                countDownActive = false;
            }
            countText.text = min.ToString() + ':' + sec.ToString();
        }
    }
}