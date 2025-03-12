
class Room
{
    // field to store the room descriptions and the item in it.
    private string description;
    private string item;

    // Initializing the room with a description and an item in it
    public Room(string description, string item)
    {
        this.description = description;
        this.item = item;
    }

    // to get the description of the room
    public string GetDescription()
    {
        return description; // Returns the room's description.
    }

    // to return and get the item inside the room
    public string GetItem()
    {
        return item;
    }

    // to remove the item from the room when player picks it up
    public void RemoveItem()
    {
        item = null;
    }
}
