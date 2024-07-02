using AssetManagementSystem.Models.Dtos;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace AuthService.Services.AssetService
{
    public class AssetService : IAssetService
    {
        private readonly HttpClient _httpClient;

        public AssetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AssetDto> GetAssetByIdAsync(int assetId)
        {
            var response = await _httpClient.GetAsync($"Assets/{assetId}");
            response.EnsureSuccessStatusCode();

            var assetJson = await response.Content.ReadAsStringAsync();
            var asset = JsonSerializer.Deserialize<AssetDto>(assetJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return asset;
        }

        public async Task<AssetDto> UpdateAssetAsync(int id, AssetDto assetDto)
        {
            var assetJson = JsonSerializer.Serialize(assetDto);
            var content =  new StringContent(assetJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Assets/{id}", content);
            response.EnsureSuccessStatusCode();

            var updatedAssetJson = await response.Content.ReadAsStringAsync();
            var updatedAsset = JsonSerializer.Deserialize<AssetDto>(updatedAssetJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            return updatedAsset;

        }


    }
}
