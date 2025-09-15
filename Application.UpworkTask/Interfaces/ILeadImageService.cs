namespace Application.UpworkTask.Interfaces
{
    public interface ILeadImageService
    {
        Task<string> UploadImagesAsync(int leadId, List<string> base64Images);
        Task<List<(int Id, string Base64Image)>> GetImagesAsync(int leadId);
        Task<string> DeleteImageAsync(int imageId);
    }
}
