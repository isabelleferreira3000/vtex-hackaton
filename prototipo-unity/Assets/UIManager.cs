using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject content;
    public GameObject listItem;

    public static UIManager instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    public void GenerateVendorListItens()
    {
        foreach (Seller s in Database.sellers)
        {
            GameObject newSellerItem = Instantiate(listItem, content.transform);
            newSellerItem.transform.Find("Name").GetComponent<Text>().text = s.name;

            double nota = s.note;
            if (nota >= 0.0)
            {
                newSellerItem.transform.Find("Note").GetComponent<Text>().text = s.note.ToString() + "/10";
            }
            else
            {
                newSellerItem.transform.Find("Note").GetComponent<Text>().text = "S/A";
            }
        }
    }


}
