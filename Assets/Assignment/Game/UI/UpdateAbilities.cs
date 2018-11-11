using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAbilities : MonoBehaviour
{

    //This script controls the ability UI part of the UI.
    [SerializeField]
    private Text m_UnitNameTextBox;
    [SerializeField]
    private Text m_UnitMaxText;
    [SerializeField]
    private Slider m_HealthSlider;
    [SerializeField]
    private GameObject m_InstatiationGameObject;

    [SerializeField] private Sprite m_TeleportImage;
    [SerializeField] private Sprite m_FireBallImage;
    [SerializeField] private Sprite m_BlastImage;
    [SerializeField] private Sprite m_DefaultAbiliityImage;

    [SerializeField]
    private RectTransform m_ParentPanel;
    [SerializeField]
    private Transform m_ButtonPrefab;

    private Unit m_Unit = null;

    private Transform m_InitializedTransform;

    private List<Button> m_CreatedButtons;

    private float m_Timer;

    private void Start()
    {
        m_CreatedButtons = new List<Button>();
    }

	void Update () {
	    if (m_Unit != null)
	    {
            UpdateAbilityUI(m_Unit);
	    }
	}

    //Gets called when some one clicks on an Unit.
    public void UpdateAbilityUI(Unit i_Unit)
    {
        if (m_Unit != i_Unit)
        {
            UpdatePlayerIcon(i_Unit);
            CreateAbilityButtons(i_Unit);
            SetActive();
        }
        m_Unit = i_Unit;
        m_UnitNameTextBox.text = i_Unit.Name;
        m_UnitMaxText.text = i_Unit.Health.Max.ToString();
        m_HealthSlider.value = i_Unit.Health.Percent;
    }

    //Creates a new Instance of the player at a location far below the play space.
    //Shows 3D version of the current unit.
    private void UpdatePlayerIcon(Unit i_Unit)
    {
        if(m_InitializedTransform) GameObject.Destroy(m_InitializedTransform.gameObject);
        Transform m_NewTransform = GameObject.Instantiate(i_Unit.transform, m_InstatiationGameObject.transform.position,
            Quaternion.Inverse(Quaternion.identity));
        m_NewTransform.Rotate(Vector3.up, 180.0f);
        m_NewTransform.GetComponent<Rigidbody>().isKinematic = true;
        m_NewTransform.GetComponent<Rigidbody>().useGravity = false;
        m_NewTransform.GetComponent<UnitController>().enabled = false;
        m_InitializedTransform = m_NewTransform;
    }

    //Dynamically create buttons for the abilities for a particular Unit.
    private void CreateAbilityButtons(Unit i_Unit)
    {
        AbilityController m_AC = (i_Unit.GetComponent<AbilityController>() ? i_Unit.GetComponent<AbilityController>() : null) ;
        if (m_AC)
        {
            //Removes Previously Created Buttons
            if (m_CreatedButtons.Count != 0)
            {
                foreach (var button in m_CreatedButtons)
                {
                    GameObject.Destroy(button.gameObject);
                }
            }

            m_CreatedButtons.Clear();
            List<Abilities.AbilitiesEnum> m_AbitiesList = m_AC.GetAbilities();
            //Create new Buttons
            if (m_AbitiesList.Count != 0)
            {
                foreach (var ability in m_AbitiesList)
                {
                    GameObject go = Instantiate(m_ButtonPrefab).gameObject;
                    go.transform.SetParent(m_ParentPanel, false);
                    go.transform.localScale = new Vector3(1, 1, 1);

                    Button b = go.GetComponent<Button>();
                    m_CreatedButtons.Add(b);
                    AbilityButtonController m_Ab = b.gameObject.AddComponent<AbilityButtonController>();
                    if (ability == Abilities.AbilitiesEnum.Blast)
                    {
                        b.GetComponent<Image>().sprite = m_BlastImage;
                        m_Ab.coolDown = m_AC.s_PlayerBlastDetails.CooldownTime;
                    }
                    else if (ability == Abilities.AbilitiesEnum.Fireball)
                    {
                        b.GetComponent<Image>().sprite = m_FireBallImage;
                        m_Ab.coolDown = m_AC.s_PlayerFireballDetails.CooldownTime;
                    }
                    else if (ability == Abilities.AbilitiesEnum.Teleport)
                    {
                        b.GetComponent<Image>().sprite = m_TeleportImage;
                        m_Ab.coolDown = m_AC.s_PlayerTeleportDetails.CooldownTime;
                    }
                    else if (ability == Abilities.AbilitiesEnum.Gravity)
                    {
                        m_Ab.coolDown = m_AC.s_PlayerGravityDetails.CooldownTime;
                    }
                    else if (ability == Abilities.AbilitiesEnum.Meteor)
                    {
                        m_Ab.coolDown = m_AC.s_PlayerMeteorDetails.CooldownTime;
                    }
                    else if (ability == Abilities.AbilitiesEnum.Stealth)
                    {
                        m_Ab.coolDown = m_AC.s_PlayerStealthDetails.CooldownTime;
                    }
                    else
                    {
                        b.GetComponent<Image>().sprite = m_DefaultAbiliityImage;
                        m_Ab.coolDown = 10;
                    }
                    b.GetComponentInChildren<Text>().text = ability.ToString();
                    m_Ab.abilityController = m_AC;
                    m_Ab.AbilityUnit = i_Unit;
                    b.onClick.AddListener(delegate {m_Ab.OnClick(ability);});
                }
            }
        }
    }

    private void SetActive()
    {
        m_UnitNameTextBox.enabled = true;
        m_UnitMaxText.transform.parent.GetChild(2).GetComponent<Text>().enabled = true;
        m_UnitMaxText.transform.parent.GetComponentInChildren<Text>().enabled = true;
        m_HealthSlider.gameObject.SetActive(true);
    }
}
