namespace app
{
    internal class Employers : Company
    {
        public string Name { get; private set; }
        public Guid ID { get; private set; }
    
        Employers(string name, Guid id)
        {
            Name = name;
            ID = id;
        }
    }
}