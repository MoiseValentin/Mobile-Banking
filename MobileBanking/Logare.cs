using System;

public class Logare
{
    private String userName;
    private String password;
	public Logare()
	{
	}
    public Logare (String username, String password)
    {
        this.userName = username;
        this.password = password;
    }
    public String getUserName()
    {
        String auxiliarUserName = userName;
        return auxiliarUserName;
    }
    public String getPassword()
    {
        // ? putem sa gasim un algoritm de criptare a parolei si sa verificam cu parola criptata din baza de date
        String auxiliarPassword = password;
        return auxiliarPassword;
    }
    
    /// se va returna userul, daca este posibil 
    /// => void = USER !!!!!
    public void openUserAccount()
    {
        //am nevoie de "block" din baza de date
        ///daca exista userul in baza de date si nu este blocat => deschid contul
        ///daca exista userul in baza de date si contul este blocat => nu deschid contul + mesaj ("Cont blocat")
        ///daca nu exista userul in baza de date => mesaj de eroare 

    }
    public void newUserAccount(String name, String cnp, String iban, String ibanPassword, String password)
    {

    }

}
