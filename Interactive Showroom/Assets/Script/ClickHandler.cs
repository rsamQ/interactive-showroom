using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickHandler : MonoBehaviour
{
    public Button yourButton;
    public VideoControll Video;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        Video.SetFrame(100);
    }
}