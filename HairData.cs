namespace Lab3
{
    public struct HairData
    {
        public string Color { get; set; }
        public float Lenght { get; set; }
        public HairData(string color, float lenght)
        {
            Color = color;
            Lenght = lenght;
        }

        public override string ToString()
        {
            return string.Format($"Color:{Color},Lenght:{Lenght}cm");
        }
    }
}