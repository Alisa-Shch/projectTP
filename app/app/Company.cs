namespace app
{
    internal class Company
    {
        //public Guid CID { get; set; }
        public string Name { get; private set; }
    
        Company(string name)
        {
            Name = name;
        }
    }
}