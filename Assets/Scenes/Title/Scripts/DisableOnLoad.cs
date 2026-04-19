using System.Threading.Tasks;
using UnityEngine;

public class DisableOnLoad : MonoBehaviour
{
    [SerializeField] private Behaviour elementToDisable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async Task Start()
    {
        await Task.Yield();
        elementToDisable.enabled = false;
    }
}
