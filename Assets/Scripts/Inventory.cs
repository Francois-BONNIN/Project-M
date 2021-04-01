using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance;

    private void Awake() //lu avant tous les autres fonction lors du démarage
    {
        if(instance != null) // pour que l'inventaire soit unique
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        instance = this; // pour avoir accés de n'importe ou
    }
    public void AddCoins(int count)
    {
        coinsCount += count;
    }
}
