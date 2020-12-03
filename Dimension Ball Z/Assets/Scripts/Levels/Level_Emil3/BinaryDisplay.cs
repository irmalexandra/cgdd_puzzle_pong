using UnityEngine;

public class BinaryDisplay : MonoBehaviour
{

    public static BinaryDisplay Instance;

    public TMPro.TextMeshProUGUI currentBinary;
    public TMPro.TextMeshProUGUI currentInt;
    public TMPro.TextMeshProUGUI requiredInt;
    // Start is called before the first frame update
    void Start()
    {
        requiredInt.text = Random.Range(3, 8).ToString();
        Instance = this;
    }

    public void Signal(int input)
    {
        currentBinary.text += input;
        
    }
}
