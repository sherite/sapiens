namespace Sherite.ERP.Common
{
    public class Enums
    {
        public enum OrdinationSense
        {
            Ascending = 1,
            Descending = 2
        }

        public enum StatusObject
        {
            Ok = 0,
            Error = 1
        }

        public enum Modules
        {
            Settings = 0,
            Administration = 1,
            Security = 2
        }

        public enum Operations
        {

            /* OPERATION'S USERS MODULE */
            UsersInsert = 100,
            UsersDelete = 101,
            UsersUpdate = 102

            /* OPERATION'S ANOTHER MODULE */
        }
    }
}