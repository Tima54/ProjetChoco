using Models;
using System;
using System.Security.Cryptography.X509Certificates;

public class Administrator: BaseModel
{
    public string Login { get; set; }
	public string Password { get; set; }
	
	public Administrator()
	{
	
	}

}
