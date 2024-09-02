namespace BaşarSoftDeneme.Models
{
    public class Point
    {
        public long Id { get; set; }
        public string WKT { get; set; } // WKT formatında geometrik veriyi tutacak
        public string Name { get; set; }

        // Parametresiz yapıcı metot (Entity Framework tarafından kullanılacak)
        public Point() { }

        // Opsiyonel olarak, manuel olarak bir WKT değeri atamak için kullanılabilecek bir yapıcı metot
        public Point(string wkt)
        {
            WKT = wkt;
        }

    }
}
