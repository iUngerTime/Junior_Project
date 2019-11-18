using System;

using JP_PantryAid.Models;

namespace JP_PantryAid.ViewModels
{
   public class ItemDetailViewModel : BaseViewModel
   {
      public Item Item { get; set; }
      public ItemDetailViewModel(Item item = null)
      {
         Title = item?.Text;
         Item = item;
      }
   }
}
