namespace app
{
    internal class Candidate
    {
        public string Name { get; private set; }
        public string Mail { get; private set; }
    
        Candidate(string name, string mail)
        {
            Name = name;
            Mail = mail;
        }
    }
}