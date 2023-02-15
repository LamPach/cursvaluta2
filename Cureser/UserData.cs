using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cureser.Forms
{
    public class UserData
    {
        public UserData(UserModel user, UserContext userContext) 
        {
            if (user.FavouriteCurrencies == null)
            {
                user.FavouriteCurrencies = "";
                userContext.SaveChanges();
            }

            this.user = user;
            this.userContext = userContext;
            string fav = user.FavouriteCurrencies;
            FavCur = fav.Split(',').ToHashSet();
        }

        public void SaveChanges()
        {
            this.user.FavouriteCurrencies = string.Join(",", FavCur);
            this.userContext.SaveChanges();
        }

        public void AddCurrencyToFavourite(string code)
        {
            this.FavCur.Add(code);
            SaveChanges();
        }

        public void DeleteCurrenctFromFavourite(string code)
        {
            this.FavCur.Remove(code);
            SaveChanges();
        }

        private UserModel user;
        private UserContext userContext;
        public HashSet<string> FavCur { get; private set; }
    }
}
