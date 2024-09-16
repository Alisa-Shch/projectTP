namespace app
{
    internal class Role : Employers
    {
        public Guid NameID { get; private set; }
        public string RoleName { get; private set; }

        Role(Guid nameID, string roleName)
        {
            NameID = nameID;
            RoleName = roleName;
        }
    }
}