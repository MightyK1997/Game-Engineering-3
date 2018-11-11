using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpdateHealthBars : MonoBehaviour {

    [SerializeField]
    private List<Unit> m_UnitList;

    [SerializeField]
    private List<Slider> m_HealthBarList;

    private Dictionary<Unit, Slider> m_HealthBars = new Dictionary<Unit, Slider>();

	private void Start () {
        m_HealthBars = m_UnitList.Select((i, j) => new { i, v = m_HealthBarList[j] }).ToDictionary(x => x.i, x => x.v);
    }
	
	private void Update () {
        foreach (var healthBar in m_HealthBars)
        {
            healthBar.Value.value = healthBar.Key.Health.Percent;
        }
	}
}
