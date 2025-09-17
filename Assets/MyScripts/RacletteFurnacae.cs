using System.Collections;
using UnityEngine;

public class RacletteFurnacae : MonoBehaviour
{
    public Transform cheeseSnapPoint;
    public float cookTime = 5f;
    public Material cookedMat;

    private CheeseWheel currentCheese;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cheese"))
        {

            CheeseWheel newCheese = other.GetComponent<CheeseWheel>();
            if (currentCheese == null)
            {

                currentCheese = newCheese;
                currentCheese.ClipToFurnace(cheeseSnapPoint);
                StartCoroutine(CookCheese(currentCheese));
            }
        }
    }

    IEnumerator CookCheese(CheeseWheel cheese)
    {
        yield return new WaitForSeconds(cookTime);
        cheese.isReady = true;
        cheese.SetMaterial(cookedMat);
    }

    
}
