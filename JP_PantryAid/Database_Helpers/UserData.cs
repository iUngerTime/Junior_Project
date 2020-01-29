using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Models;

namespace Database_Helpers
{
    public class UserData : iUserDataRepo
    {
        public int AddAlergy(User currUser, Ingredient newAlergy)
        {
            throw new NotImplementedException();
        }

        public int AddDislikedRecipe(User currUser, Recipe_Short newDisliked)
        {
            throw new NotImplementedException();
        }

        public int AddFavoriteRecipe(User currUser, Recipe_Short newFavorite)
        {
            throw new NotImplementedException();
        }

        public int AddUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(User delUser)
        {
            throw new NotImplementedException();
        }

        public int EditUserInfo(User currentInfo, User newInfo)
        {
            throw new NotImplementedException();
        }

        public int RemoveAlergy(User currUser, Ingredient alergy)
        {
            throw new NotImplementedException();
        }

        public int RemoveDislikedRecipe(User currUser, Recipe_Short nonDisliked)
        {
            throw new NotImplementedException();
        }

        public int RemoveFavoriteRecipe(User currUser, Recipe_Short nonFavorite)
        {
            throw new NotImplementedException();
        }
    }
}
