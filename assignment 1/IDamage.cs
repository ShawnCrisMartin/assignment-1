

namespace DungeonExplorer
{
    //idamage is an interface that that we use in classes where we have to implement taking damage, so any class that implements this interface should have a damagetaken method.
    public interface IDamage
    {
        // method that will reduce the health of the character by the amount of damage taken. and in the calss where all this is used is where we will implement the real logic of subtracting hp 
        void DamageTaken(int damage);
    }
}
