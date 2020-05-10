using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeGenerator : MonoBehaviour
{
    [SerializeField] Transform targetPointParent;
    [HideInInspector]public string code = "";

    private void Awake()
    {
        RandomizeTransform();
        GenerateCode();
    }

    void RandomizeTransform()
    {
        Transform [] targetPoints = targetPointParent.GetComponentsInChildren<Transform>();

        //No usamos el padre como targetPoint
        int randomIndex = Random.Range(1, targetPoints.Length);

        transform.position = targetPoints[randomIndex].position;
        transform.rotation = targetPoints[randomIndex].rotation;
    }

    void GenerateCode()
    {
        TextMeshProUGUI codeText = GetComponentInChildren<TextMeshProUGUI>();

        
        List<char> aux = new List<char> { 'A', 'B', 'C', 'D' };

        int codeLength = aux.Count;

        for (int i = 0; i < codeLength; i++)
        {
            int randomIndex = Random.Range(0, aux.Count);

            char c = aux[randomIndex];
            aux.RemoveAt(randomIndex);

            code += c;
        }

        codeText.text = code;
    }
}
