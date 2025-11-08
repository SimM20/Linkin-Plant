public class Weed : CustomBehaviour
{
    private Weeds weedController;

    public void InitializeWeed(Weeds controller) => weedController = controller;

    public void Cut()
    {
        weedController.OnWeedCut(this);
        Destroy(gameObject);
    }
}
