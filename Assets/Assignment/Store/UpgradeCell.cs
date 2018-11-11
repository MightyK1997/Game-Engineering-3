using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCell : MonoBehaviour {

    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private Text nameLabel = null;

    [SerializeField]
    private List<Image> upgradeIcons = new List<Image>();

    [SerializeField]
    private Text costLabel = null;

    public void OnUpgradeClicked(Abilities.AbilitiesEnum i_Ability, AbilityController i_AC) {
        i_AC.UpdatePlayerLevel(i_Ability);
    }
	
}
