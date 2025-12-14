public static class FloatUtil
{
    public static float Normalize(this float input, float min, float max) => (input - min) / (max - min);
}