namespace DesafioDev.Domain.Entities;

public class Owner
{
    public Guid Id { get; private set; }

    public string Cpf { get; private set; }

    public string Name { get; private set; }

    public virtual Establishment Establishment { get; private set; }
     
    public Owner() { }

    public Owner(string cpf, string name)
    {
        Id = Guid.NewGuid();
        SetCpf(cpf);   
        SetName(name);
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetCpf(string cpf)
    {
        Cpf = cpf;
    }
}
