namespace lab1.debug
{
    public static class ObjectExt
    {
        public static void DebugPrint(this object obj)
        {
            var opts = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(obj, opts));
        }
    }
}