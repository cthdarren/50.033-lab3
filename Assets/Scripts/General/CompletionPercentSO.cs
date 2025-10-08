using UnityEngine;

[CreateAssetMenu(fileName = "CompletionPercentSO", menuName = "CompletionPercentSO")]
public class CompletionPercentSO : ScriptableObject
{
    public int maxCollectibles = 3;
    public int numCollectibles = 0;
    public bool bossDefeated = false;

    public void collect()
    {
        numCollectibles++;
        if (numCollectibles > maxCollectibles)
            numCollectibles = 3;
    }
    public float calcPercent()
    {
        return (((float)numCollectibles/maxCollectibles)*50f) + ((bossDefeated ? 1 : 0) * 50f); 
    }
}
