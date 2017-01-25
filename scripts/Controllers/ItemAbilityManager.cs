using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemAbilityManager : MonoBehaviour
{
    public float pickUpCooldownTime = 1.0f;
    public Dictionary<string, AbilityTemplate> abilities;
    public Dictionary<string, ItemTemplate> items;

    public bool dash = false;
    public bool grab = false;
    public bool blink = false;

    // Update is called once per frame
    void Update()
    {
        //Update isn't called inside a dictionary
        //This foreach loop calls it for every element in the dictionary
        if (abilities.Count > 0)
        {
            foreach (KeyValuePair<string, AbilityTemplate> pair in abilities)
            {
                abilities[pair.Key].Update();
            }
        }

        if (items.Count > 0)
        {
            foreach (KeyValuePair<string, ItemTemplate> pair in items)
            {
                items[pair.Key].Update();
            }
        }
    }

    void FixedUpdate()
    {
        //FixedUpdate isn't called inside a dictionary
        //This foreach loop calls it for every element in the dictionary
        if (abilities.Count > 0)
        {
            foreach (KeyValuePair<string, AbilityTemplate> pair in abilities)
            {
                abilities[pair.Key].FixedUpdate();
            }
        }

        if (items.Count > 0)
        {
            foreach (KeyValuePair<string, ItemTemplate> pair in items)
            {
                items[pair.Key].FixedUpdate();
            }
        }
    }


    //
    // Ability functions
    //
    public void addAbility(string _key, AbilityTemplate _ability)
    {
        //Check if there already is an ability assigned to that key
        if (abilities.ContainsKey(_key))
        {
            replaceAbility(_key, _ability);
        }
        else //If not, then add ability
        {
            abilities.Add(_key, _ability);
        }

        // Get needed components
        abilities[_key].animator = transform.FindChild("PlayerGraphics").GetComponent<Animator>();
        abilities[_key].controller = transform.GetComponent<PlayerController>();

        // Assign this player
        abilities[_key].thisPlayer = transform;

        // Assign that player
        if (Network.connections.Length > 0)
        {
            if (abilities[_key].thisPlayer.tag == "Deceit")
            {
                abilities[_key].thatPlayer = GameObject.FindGameObjectWithTag("Will").transform;
            }
            else if (abilities[_key].thisPlayer.tag == "Will")
            {
                abilities[_key].thatPlayer = GameObject.FindGameObjectWithTag("Deceit").transform;
            }
        }

        // Updates button if possible
        ItemAbilityButtonController.AbilityButtonsUpdate();
    }

    public void dropAbility(string _key)
    {
        abilities.Remove(_key);
    }

    public void replaceAbility(string _key, AbilityTemplate _ability)
    {
        dropAbility(_key);
        addAbility(_key, _ability);
    }

    //
    // Item functions
    //
    public void addItem(string _key, ItemTemplate _item)
    {
        //Check if there already is an ability assigned to that key
        if (items.ContainsKey(_key))
        {
            Debug.Log("replace item" + _key);
            replaceItem(_key, _item);
        }
        else //If not, then add ability
        {
            Debug.Log("Add item: " + _key);
            items.Add(_key, _item);
        }
        //Debug.Log("Add item: " + _key);

        // Get needed components
        items[_key].animator = transform.FindChild("PlayerGraphics").GetComponent<Animator>();
        items[_key].controller = transform.GetComponent<PlayerController>();
        items[_key].currentKey = _key;

        // Assign this player
        items[_key].thisPlayer = transform;

        // Assign that player
        if (Network.connections.Length > 0)
        {
            if (items[_key].thisPlayer.tag == "Deceit")
            {
                items[_key].thatPlayer = GameObject.FindGameObjectWithTag("Will").transform;
            }
            else if (items[_key].thisPlayer.tag == "Will")
            {
                items[_key].thatPlayer = GameObject.FindGameObjectWithTag("Deceit").transform;
            }
        }

        // Updates button if possible
        ItemAbilityButtonController.ItemButtonsUpdate();
    }

    public void dropItem(string _key)
    {
        items.Remove(_key);
    }

    public void replaceItem(string _key, ItemTemplate _item)
    {
        dropItem(_key);
        addItem(_key, _item);
    }


    // Sets every ability and item on a small cooldown after
    // pickup choice is made to not trigger effect while making a choice
    public void pickUpCooldown()
    {
        foreach (KeyValuePair<string, AbilityTemplate> pair in abilities)
        {
            abilities[pair.Key].cooldownTimer = pickUpCooldownTime;
            abilities[pair.Key].pickupCooldown = true;
            abilities[pair.Key].available = false;
        }
        foreach (KeyValuePair<string, ItemTemplate> pair in items)
        {
            items[pair.Key].cooldownTimer = pickUpCooldownTime;
            items[pair.Key].pickupCooldown = true;
            items[pair.Key].available = false;
        }
    }

    public void deceitAddAbilitiesAndItems()
    {
        abilities = new Dictionary<string, AbilityTemplate>();
        items = new Dictionary<string, ItemTemplate>();

        // Add Standard abilities for deceit.
        addAbility("Dash", transform.GetComponent<Dash>());
        addAbility("X", transform.GetComponent<Confusion>());
        addAbility("B", transform.GetComponent<Push>());
    }

    public void willAddAbilitiesAndItems()
    {
        abilities = new Dictionary<string, AbilityTemplate>();
        items = new Dictionary<string, ItemTemplate>();

        // Add Standard abilities for will.
        addAbility("Dash", transform.GetComponent<Dash>());
        addAbility("X", transform.GetComponent<WillOverTime>());
        addAbility("Y", transform.GetComponent<Tag>());
        addAbility("B", transform.GetComponent<Pull>());
    }

    public void deceitConnectAddThatPlayer()
    {
        if (abilities.Count > 0)
        {
            foreach (KeyValuePair<string, AbilityTemplate> pair in abilities)
            {
                abilities[pair.Key].thatPlayer = GameObject.FindWithTag("Deceit").transform;
            }
        }
        if (items.Count > 0)
        {
            foreach (KeyValuePair<string, ItemTemplate> pair in items)
            {
                {
                    items[pair.Key].thatPlayer = GameObject.FindWithTag("Deceit").transform;
                }
            }
        }
    }
}