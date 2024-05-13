using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField]
    private Text text_Preview;
    [SerializeField]
    private Text text_Input;
    [SerializeField]
    private InputField if_text;

    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private PickupController player;

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OK();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }
        }
    }

    public void Call()
    {

    }

    public void Cancel()
    {

    }

    public void OK()
    {

    }

    IEnumerator DropItemCoroutine(int _num)
    {
        yield return null;
    }

    public bool CheckNumber(string _string)
    {
        bool isNumber = true;

        return isNumber;
    }
}
