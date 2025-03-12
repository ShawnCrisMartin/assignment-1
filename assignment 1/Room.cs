   class Room
   {
       private string description;
       private string item;

       public Room(string description, string item)
       {
           this.description = description;
           this.item = item;
       }

       public string GetDescription()
       {
           return description;
       }

       public string GetItem()
       {
           return item;
       }

       public void RemoveItem()
       {
           item = null;
       }
   }
