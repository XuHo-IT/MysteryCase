using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseDomain
{
    public class Constants
    {
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        public enum Resolutions
        {
            Active,
            Solved,
            Archived
        }
        public enum Cost
        {
            Zero = 0,
            Five = 5,
            Ten = 10
        }
        public enum RoleUser
        {
            Player,
            Admin,
        }
        public enum Unlock
        {
            Purchased,
            Found,
            Granted
        }

    }
}
