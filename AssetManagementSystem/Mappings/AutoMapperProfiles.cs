﻿using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AutoMapper;

namespace AssetManagementSystem.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<AssetType, AssetTypeDto>().ReverseMap();
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<DisposalAssets,DisposalAssetsDto>().ReverseMap(); 
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Contract, ContractDto>().ReverseMap();
            CreateMap<OrderedAssetType, OrderedAssetTypeDto>().ReverseMap();
        }
    }
}
