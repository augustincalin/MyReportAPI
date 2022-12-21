namespace ReportApi.Services.Interfaces
{
    public interface IMimeMapperService
    {
        public (string Format, string MimeType) GetFormat(string acceptHeader);
    }
}
