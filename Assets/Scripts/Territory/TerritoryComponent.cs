using System;
using UnityEngine;

//----------------------------------------------
//----------------------------------------------
// Card
//----------------------------------------------
//----------------------------------------------
public class TerritoryComponent : MonoBehaviour
{
    //----------------------------------------------
    // Variables
    private Territory m_territory;
    private bool m_isHovered = false;
    private bool m_isSelected = false;
    private Vector3 m_initialPosition = new Vector3();

    //----------------------------------------------
    // Properties

    public bool Hovered
    {
        get { return m_isHovered; }
    }

    public bool Selected
    {
        get { return m_isSelected; }
    }

    public Territory Territory
    {
        get { return m_territory; }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    public void Init(Territory territory)
    {
        m_territory = territory;
        m_isHovered = false;
        m_isSelected = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
           // spriteRenderer.sprite = CardStaticData.Instance.GetSprite(m_card.Value, m_card.Family);
        }

        gameObject.name = territory.ToString();
    }

    public void SetInitialPosition(Vector3 initialPosition)
    {
        transform.localPosition = initialPosition;
        m_initialPosition = initialPosition;
    }

    bool CanBeSelected()
    {
        return true;   
    }
    

    // Update is called once per frame
    void Update()
    {
        if(CanBeSelected())
        {
            GameObject underMouse = Picker.Instance.UnderMouse;

            if (underMouse != null && underMouse == gameObject)
            {
                SetHovered(true);
            }
            else
            {
                SetHovered(false);
            }

            if(Hovered && !Selected)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetSelected(true);
                }
            }

            if (Selected && Input.GetMouseButtonUp(0))
            {
                SetSelected(false);
            }

            if(m_isSelected)
            {
                Vector3 newPos = new Vector3();
                newPos = Picker.Instance.MouseWorldPos;
                newPos.z -= 0.1f;
                transform.localPosition = newPos;
            }
            else
            {
                transform.localPosition = m_initialPosition;
            }
        }
        else
        {
            if(Selected)
            {
                SetSelected(false);
            }
        }
    }

    static float scaleFactor = 1.2f;
    static float invScaleFactor = 1.0f / scaleFactor;
    protected void SetHovered(bool under)
    {
        if (under != m_isHovered)
        {
            if (m_isHovered)
            {
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= invScaleFactor;
                scale.y *= invScaleFactor;
                gameObject.transform.localScale = scale;
            }
            m_isHovered = under;
            if (m_isHovered)
            {
                Vector3 scale = gameObject.transform.localScale;
                scale.x *= scaleFactor;
                scale.y *= scaleFactor;
                gameObject.transform.localScale = scale;
            }
        }
    }

    protected void SetSelected(bool selected)
    {
        if(selected != m_isSelected)
        {
            bool isInHandArea = IsMouseInHandArea();

            m_isSelected = selected;

            EyepatchCard.Selected evt = Pools.Claim<EyepatchCard.Selected>();
            evt.Init(m_card, m_isSelected, isInHandArea);
            EventManager.SendEvent(evt);
        }
    }

    protected bool IsMouseInHandArea()
    {
        Vector3 mouseToInitial = Input.mousePosition - gameObject.transform.position;
        return mouseToInitial.magnitude >= 1.0f;
    }
}