using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
