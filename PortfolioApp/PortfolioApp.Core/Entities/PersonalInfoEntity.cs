﻿namespace PortfolioApp.Core.Entities;
public class PersonalInfoEntity : BaseEntity<int>
{
    public string About { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Adress { get; set; }
}
