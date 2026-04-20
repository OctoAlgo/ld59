using System.Collections;
using UnityEngine;
using Random = System.Random;

public class SatelliteMove : MonoBehaviour
{
    public AudioClip startMove;
    public AudioClip move;
    public AudioClip endMove;
    
    public float timeDistanceMultiplier = 1.0f;
    
    public float moveRangeX = 360.0f;
    public float moveRangeY = 90.0f;
    
    private GameObject dishHolder;
    private GameObject dish;
    private AudioSource audioSource;
    
    private bool busy;
    private bool moving;
    private float distance;
    private Vector3 originalRotationDishHolder;
    private Vector3 originalRotationDish;
    private Vector3 target;
    private float timeStartMove;
    
    void Start()
    {
        dishHolder = gameObject.transform.Find("DishHolder").gameObject;
        dish = dishHolder.transform.Find("Dish").gameObject;
        audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        if (moving)
        {
            float timeToMove = timeDistanceMultiplier * distance;
            
            Vector3 desiredDishHolderRot = Vector3.Lerp(originalRotationDishHolder, target,
                (Time.time - timeStartMove) / timeToMove);
            Vector3 desiredDishRot = Vector3.Lerp(originalRotationDish, target,
                (Time.time - timeStartMove) / timeToMove);

            Vector3 targetDishHolderRot = new Vector3(0.0f, desiredDishHolderRot.y, 90.0f);
            Vector3 targetDishRot = new Vector3(desiredDishRot.x, 90.0f, 0.0f);
            
            dishHolder.transform.localRotation = Quaternion.Euler(targetDishHolderRot);
            dish.transform.localRotation = Quaternion.Euler(targetDishRot);
        }
    }

    public IEnumerator MoveTowards(string hashOne, string hashTwo)
    {
        /*
        if (busy)
        {
            yield return false;
        }
        */
        
        busy = true;
        originalRotationDishHolder = dishHolder.transform.localRotation.eulerAngles;
        originalRotationDish = dish.transform.localRotation.eulerAngles;
        
        var hashOneValue = StringHash(hashOne);
        var hashTwoValue = StringHash(hashTwo);

        Debug.Log(hashOne + " " + hashTwo);
        Debug.Log(hashOneValue + " " + hashTwoValue);
        
        target = new Vector3(hashOneValue * moveRangeY, hashTwoValue * moveRangeX, hashTwoValue);
        
        float distanceOne = Mathf.Abs(originalRotationDishHolder.y - target.y);
        float distanceTwo = Mathf.Abs(originalRotationDish.x - target.x);
        
        distance = distanceOne + distanceTwo;
        
        audioSource.PlayOneShot(startMove);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        moving = true;
        timeStartMove = Time.time;
        audioSource.clip = move;
        audioSource.loop = true;
        audioSource.Play();
        
        float timeToMove = timeDistanceMultiplier * distance;
        
        Debug.Log(hashOne + " " + hashTwo);
        Debug.Log(hashOneValue + " " + hashTwoValue);
        Debug.Log("Distance: " + distance + " Time to move (seconds): " + timeToMove);
        
        yield return new WaitUntil(() => timeStartMove + timeToMove < Time.time);
        
        audioSource.loop = false;
        audioSource.Stop();
        moving = false;
        audioSource.PlayOneShot(endMove);
        
        yield return new WaitUntil(() => !audioSource.isPlaying);
        
        busy = false;
    }
    
    private float StringHash(string hash)
    {
        string hashingString = "123456789ABCDEF";
        int total = 0;
        foreach (var c in hash)
        {
            var index = hashingString.IndexOf(c);
            total += index;
        }
        float divsor = hashingString.Length * hash.Length;
        float numericHash = total / divsor;
        return numericHash;
    }
}