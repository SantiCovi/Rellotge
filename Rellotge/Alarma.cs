using System;
using System.ComponentModel;

[Serializable()]
public class Alarma
    { 

    public string hora { get; set; }

    public string minuto { get; set; }

    public bool alarmaActiva { get; set; }

	public Alarma()
	{
	}
}
