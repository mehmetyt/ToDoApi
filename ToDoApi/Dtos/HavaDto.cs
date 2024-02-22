namespace ToDoApi.Dtos
{
    public class HavaDto
    {
        public DateOnly Tarih { get; set; }
        public int Sicaklik { get; set; }
        public string Aciklama { get; set; }=null!;
    }
}
