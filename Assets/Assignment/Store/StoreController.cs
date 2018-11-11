using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradeCell;

    private List<GameObject> m_UpgradeButtons = new List<GameObject>();

    [SerializeField] private RectTransform contentPanel;

    [SerializeField]
    private Text m_GoldText;

    private float x = 0.0f;
    private float count = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStore(Unit i_Unit)
    {
        AbilityController m_AC = (i_Unit.GetComponent<AbilityController>() ? i_Unit.GetComponent<AbilityController>() : null);
        if (m_AC)
        {
            if (m_UpgradeButtons.Count != 0)
            {
                foreach (var mUpgradeButton in m_UpgradeButtons)
                {
                    Destroy(mUpgradeButton);
                }
            }

            m_UpgradeButtons.Clear();
            List<Abilities.AbilitiesEnum> m_AbitiesList = m_AC.GetAbilities();
            if (m_AbitiesList.Count != 0)
            {
                foreach (var ability in m_AbitiesList)
                {
                    GameObject go = Instantiate(upgradeCell);
                    go.transform.SetParent(contentPanel);
                    go.transform.localScale = new Vector3(1,1,1);
                    if (count <= 5)
                    {
                        go.GetComponent<RectTransform>().anchorMin = new Vector2(x, 0.55f);
                        go.GetComponent<RectTransform>().anchorMax = new Vector2(x = x + 0.2f, 1.0f);
                        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;

                        count++;
                    }
                    else if (count > 5 && count <= 10)
                    {
                        go.GetComponent<RectTransform>().anchorMin = new Vector2(x = x-0.2f, 0.0f);
                        go.GetComponent<RectTransform>().anchorMax = new Vector2(x, 0.45f);
                        go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                        go.GetComponent<RectTransform>().offsetMin = Vector2.zero;

                        count++;
                    }

                    m_UpgradeButtons.Add(go);
                    UpgradeCell u = go.GetComponent<UpgradeCell>();
                    go.GetComponentInChildren<Button>().onClick.AddListener(delegate {u.OnUpgradeClicked(ability, m_AC);});
                    GameObject costGo = go.transform.Find("Upgrade Button/Cost").gameObject;
                    GameObject nameGo = go.transform.Find("Icon/Name").gameObject;
                    nameGo.GetComponent<Text>().text = ability.ToString();
                    if (ability == Abilities.AbilitiesEnum.Blast)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerBlastDetails.m_CostToUpgrade.ToString();
                    }
                    else if (ability == Abilities.AbilitiesEnum.Fireball)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerFireballDetails.m_CostToUpgrade.ToString();
                    }
                    else if (ability == Abilities.AbilitiesEnum.Teleport)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerTeleportDetails.m_CostToUpgrade.ToString();
                    }
                    else if (ability == Abilities.AbilitiesEnum.Gravity)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerGravityDetails.m_CostToUpgrade.ToString();
                    }
                    else if (ability == Abilities.AbilitiesEnum.Meteor)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerMeteorDetails.m_CostToUpgrade.ToString();
                    }
                    else if (ability == Abilities.AbilitiesEnum.Stealth)
                    {
                        costGo.GetComponent<Text>().text = m_AC.s_PlayerStealthDetails.m_CostToUpgrade.ToString();
                    }
                    else
                    {
                        costGo.GetComponent<Text>().text = 0.ToString();
                    }
                }
            }
        }

        m_GoldText.text = i_Unit.GetComponent<Player>().Gold.ToString();
    }
}
