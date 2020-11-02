using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public class Element{
        string name;
        int cost { get; set; }
        string info { get; set; }
        string ImagePath { get; set; }
        bool usable { get; set; }
        bool bought { get; set; }
        public Element(string Info_, string name_, int cost_, string ImagePath)
        {
            this.Info = Info_;
            this.name = name_;
            this.cost = cost_;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public string Info
        {
            get { return info; }
            set { info = value; }
        }
    }

	void Start () {
        Element SE = new Element("Samle text", "Sword", 100, "sampletext");
	}
	

	void Update () {
	    
	}
}
