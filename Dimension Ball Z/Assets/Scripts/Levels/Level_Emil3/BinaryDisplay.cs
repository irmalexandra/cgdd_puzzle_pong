using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinaryDisplay : MonoBehaviour
{

    public static BinaryDisplay Instance;

    public TMPro.TextMeshProUGUI currentBinary;
    public TMPro.TextMeshProUGUI currentInt;
    public TMPro.TextMeshProUGUI requiredInt;
    public GameObject winPortal;
    // Start is called before the first frame update
    void Start()
    {
        requiredInt.text = Random.Range(3, 8).ToString();
        Instance = this;
    }

    public void Signal(int input)
    {

        if (currentBinary.text.Length == 0 && input == 0)
        {
            return;
        }
        
        currentBinary.text += input;
        int required = int.Parse(requiredInt.text);
        int binaryToInt = Convert.ToInt32(currentBinary.text, 2);
        currentInt.text = binaryToInt.ToString();

        if (binaryToInt == required)
        {
            winPortal.SetActive(true);
        }
        else if (binaryToInt > required)
        {
            currentBinary.text = "";
        }
        


    }
    
}
