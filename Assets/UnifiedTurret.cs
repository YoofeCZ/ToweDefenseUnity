using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnifiedTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject upgradeUI;
    [SerializeField] protected Button upgradeButton;

    [Header("Attributes")]
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float rotationSpeed = 150f;

    [SerializeField] protected float bps = 1f; //Bullets per second
    [SerializeField] protected int baseUpgradeCost = 100;

    [SerializeField] protected float sps = 4f; //Slowness per second
    [SerializeField] protected float freezeTime = 1f;

    private float bpsBase;
    private float TargetingRangeBase;
    private float spsBase;
    private float freezetimeBase;

    private int level = 1;
    private void Start()
    {
        spsBase = sps;
        freezetimeBase = freezeTime;
        bpsBase = bps;
        TargetingRangeBase = targetingRange;


        upgradeButton.onClick.AddListener(Upgrade);
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bps = CalculateBPS();
        targetingRange = CalculateRange();
        sps = CalculateSPS();
        freezeTime = CalculateFreezeTime();

        CloseUpgradeUI();
        Debug.Log("New Attack is: " + bps + " or New Freze is: " + sps);
        Debug.Log("New Range is: " + targetingRange);
        Debug.Log("Next Cost is: " + CalculateCost());
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateFreezeTime()
    {
        return freezetimeBase * Mathf.Pow(level, 0.6f);
    }
    private float CalculateSPS()
    {
        return bpsBase * Mathf.Pow(level, 1f);
    }
    private float CalculateBPS()
    {
        return spsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange()
    {
        return TargetingRangeBase * Mathf.Pow(level, 0.4f);
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);


    }
}
