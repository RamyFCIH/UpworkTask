namespace Application.UpworkTask.Interfaces
{
    public interface ICustomerImageService
    {
        Task<string> UploadImagesAsync(int customerId, List<string> base64Images);
        Task<List<(int Id, string Base64Image)>> GetImagesAsync(int customerId);
        Task<string> DeleteImageAsync(int imageId);
    }
}
