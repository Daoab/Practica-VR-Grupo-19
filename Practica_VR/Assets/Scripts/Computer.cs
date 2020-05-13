using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Computer : MonoBehaviour
{
    [SerializeField] CodeGenerator codeGenerator;
    [SerializeField] TextMeshProUGUI codeText;
    [SerializeField] TextMeshProUGUI passwordText;

    [SerializeField] UnityEvent onCodeComplete;
    [SerializeField] UnityEvent onCodeFail;

    [SerializeField] Material redLightMat;
    [SerializeField] Color codeDefaultColor;
    [SerializeField] Color codeSuccessColor;

    string localCode = "";
    int localCodeIndex = 0;
    string defaultLocalCode = "";

    private void Start()
    {
        for (int i = 0; i < codeGenerator.code.Length; i++) defaultLocalCode += "_";
        codeText.text = defaultLocalCode;

        redLightMat.color = codeDefaultColor;
    }

    //Esta función cambia el color de las luces que tengan como material redLightMat
    public void ChangeLights()
    {
        redLightMat.color = codeSuccessColor;
    }

    //Esta función inserta un nuevo caracter al código y comprueba si es correcto o no en caso de que esté completo
    public void InsertCode(string c)
    {
        char aux = c[0];

        localCode += aux;

        string codeString = localCode;
        for (int i = localCode.Length; i < codeGenerator.code.Length; i++) codeString += "_";
        codeText.text = codeString;

        if(codeGenerator.code.Length == localCode.Length)
        {
            if (codeGenerator.code.Equals(localCode))
                onCodeComplete.Invoke();

            else
            {
                localCode = "";
                codeText.text = defaultLocalCode;
                passwordText.text = "Contraseña incorrecta";

                onCodeFail.Invoke();
            }
        }
    }
}
