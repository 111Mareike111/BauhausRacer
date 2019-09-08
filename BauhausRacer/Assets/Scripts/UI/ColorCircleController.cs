using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCircleController : MonoBehaviour {


    [SerializeField] Animator redAnim;
    [SerializeField] Animator yellowAnim;
    [SerializeField] Animator blueAnim;
    [SerializeField] Animator greenAnim;
    [SerializeField] Animator purpleAnim;
    [SerializeField] Animator orangeAnim;
    private string up = "Up", down = "Down";

    public void ChangeColorCircle(string colorName)
    {
        if (colorName == "NoColor")
        {
            ChangeToNoColor();
        }
        else if (colorName == "Red")
        {
            ChangeToRed();
        }
        else if (colorName == "Blue")
        {
            ChangeToBlue();
        }
        else if (colorName == "Yellow")
        {
            ChangeToYellow();
        }
        else if (colorName == "Violet")
        {
            ChangeToPurple();
        }
        else if (colorName == "Green")
        {
            ChangeToGreen();
        }
        else if (colorName == "Orange")
        {
            ChangeToOrange();
        }
    }

    private void ChangeToRed()
    {
        redAnim.SetTrigger(up);
        yellowAnim.SetTrigger(down);
        blueAnim.SetTrigger(down);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(down);
    }
    private void ChangeToYellow()
    {
        redAnim.SetTrigger(down);
        yellowAnim.SetTrigger(up);
        blueAnim.SetTrigger(down);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(down);
    }
    private void ChangeToBlue()
    {
        redAnim.SetTrigger(down);
        yellowAnim.SetTrigger(down);
        blueAnim.SetTrigger(up);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(down);
    }

    private void ChangeToGreen()
    {
        redAnim.SetTrigger(down);
        yellowAnim.SetTrigger(up);
        blueAnim.SetTrigger(up);
        greenAnim.SetTrigger(up);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(down);
    }

    private void ChangeToPurple()
    {
        redAnim.SetTrigger(up);
        yellowAnim.SetTrigger(down);
        blueAnim.SetTrigger(up);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(up);
        orangeAnim.SetTrigger(down);
    }
    private void ChangeToOrange()
    {
        redAnim.SetTrigger(up);
        yellowAnim.SetTrigger(up);
        blueAnim.SetTrigger(down);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(up);
    }

    private void ChangeToNoColor()
    {
        redAnim.SetTrigger(down);
        yellowAnim.SetTrigger(down);
        blueAnim.SetTrigger(down);
        greenAnim.SetTrigger(down);
        purpleAnim.SetTrigger(down);
        orangeAnim.SetTrigger(down);
    }

}
