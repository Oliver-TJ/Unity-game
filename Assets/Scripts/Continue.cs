using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
