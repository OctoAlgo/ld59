using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Alien Image Pair", menuName = "Alien/Alien Image Pair", order = 1)]
public class AlienImagePair : ScriptableObject
{
    public Sprite curiousImage;
    public Sprite happyImage;
    public Sprite sadImage;
}
