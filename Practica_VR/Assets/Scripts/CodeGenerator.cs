using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeGenerator : MonoBehaviour
{
    [SerializeField] Transform targetPointParent; //Objeto padre de los posibles lugares donde se posicionará el código
    [HideInInspector]public string code = "";

    private void Awake()
    {
        RandomizeTransform();
        GenerateCode();
    }

    //Función que posiciona el código en un target point aleatorio
    void RandomizeTransform()
    {
        Transform [] targetPoints = targetPointParent.GetComponentsInChildren<Transform>();

        //No usamos el padre como targetPoint (por eso el índice mínimo es 1)
        int randomIndex = Random.Range(1, targetPoints.Length);

        transform.position = targetPoints[randomIndex].position;
        transform.rotation = targetPoints[randomIndex].rotation;
    }

    //Función que genera un código aleatorio sin repeticiones de cuatro letras
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
