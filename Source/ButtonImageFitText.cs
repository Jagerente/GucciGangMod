using UnityEngine;
using UnityEngine.UI;

public class ButtonImageFitText : MonoBehaviour
{
    public Image image;
    public Text text;

    private void Start()
    {
        print(string.Concat(text.flexibleWidth, " ", text.minWidth, " ", text.preferredWidth));
    }

    private void Update()
    {
    }
}