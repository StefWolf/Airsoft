using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time = 60f;
    [SerializeField] private Text timerText;

    void Start() {
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer() {
        if (time > 0) {
            time--;

            if (timerText != null) {
                timerText.text = time < 10 ? "0"+time.ToString() : time.ToString();
            }
        } else {
            CancelInvoke("UpdateTimer");
            SceneManager.LoadScene("GameOver");
        }
    }
}
