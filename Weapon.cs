using UnityEngine;
using System;

public abstract class Weapon
{

    private int damage;
    private int range;
    private float delay;
    private string name; 
    private string description; 
    /*Note: intentionally did not include too many required fields in the abstract class. Can add more later if necessary */ 
    
	public Weapon(string name, string description,int damage, int range, int delay) /* initiates a basic weapon object. Can override in a 
                                                                                    subclass to construct a weapon with more fields */                                        
	{
        this.name = name;
        this.description = description;
        this.damage = damage;
        this.range = range;
        this.delay = delay;
    }

    public int prepareToFire() 
    {

    }

    public int getDamage()
    {
        return this.damage; 
    }

    public int getRange()
    {
        return this.range; 
    }

    public float getDelay()
    {
        return this.delay;
    }

    public string getName()
    {
        return this.name;
    }

    public string description()
    {
        return this.description; 
    }
}
