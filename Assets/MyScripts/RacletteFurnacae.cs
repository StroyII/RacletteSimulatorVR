using System.Collections;
using UnityEngine;

public class RacletteFurnacae : MonoBehaviour
{
    public Transform cheeseSnapPoint;
    public float cookTime = 5f;
    public Material cookedMat;

    public int maxUses = 5;

    private CheeseWheel currentCheese;

    void Update()
    {
        if (currentCheese != null && currentCheese.currentUse >= maxUses)
        {
            Destroy(currentCheese.gameObject);
            currentCheese = null;
        }    
    }

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
