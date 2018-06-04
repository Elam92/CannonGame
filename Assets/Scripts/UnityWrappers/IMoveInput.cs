namespace CannonGame
{
    public interface IMoveInput
    {
        // Reads in inputs to move objects.
        void ReadInput();

        // Horiztonal movement input value.
        float HorizontalInput { get; }

        // Vertical movement input value.
        float VerticalInput { get; }
    }
}