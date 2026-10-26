using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class ColorChangeManager : MonoBehaviour
{
    public AudioSourceManager[] changeColorScripts; 

    private void Start()
    {
        // Subscribe to key presses for numbers 1 to 6
        for (int i = 1; i <= 6; i++)
        {
            int index = i;
            Observable.EveryUpdate()
                .Where(_ => Keyboard.current[Key.Digit1 + index - 1].wasPressedThisFrame)
                .Subscribe(_ => ActivateColorChange(index))
                .AddTo(this);
        }
    }

    private void ActivateColorChange(int index)
    {
        // Calculate array indices (2 objects per key press)
        int firstIndex = (index - 1) * 2;
        int secondIndex = firstIndex + 1;

        // Check array bounds and call NextColor if valid
        if (firstIndex < changeColorScripts.Length)
            changeColorScripts[firstIndex]?.NextColor();
        if (secondIndex < changeColorScripts.Length)
            changeColorScripts[secondIndex]?.NextColor();
    }
}