using System;
using System.Collections.Generic;
using System.Text;

///referencias
using AutoMapper;
using Conecta2.DTO;
using Conecta2.Model;

namespace Conecta2.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region user
            CreateMap<User, UserDTO>().ReverseMap();
            #endregion user

            #region post
            CreateMap<Post, PostDTO>()
                .ForMember(destino =>
                    destino.NameCategory,
                    opt => opt.MapFrom(origen => origen.IdCategoryNavigation.NameCategory)
                )
                .ForMember(destino =>
                    destino.CompleteNameUser,
                    opt => opt.MapFrom(origen => $"{origen.IdUserNavigation.NameUser} {origen.IdUserNavigation.LastnameUser}")
                );

            //Mapeo inverso
            CreateMap<PostDTO, Post>()
                .ForMember(destino => destino.IdCategoryNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.IdUserNavigation, opt => opt.Ignore());
            #endregion post

            #region category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion category

            #region session

            CreateMap<User, SessionDTO>();

            #endregion session

            #region login

            CreateMap<LoginDTO, User>();

            #endregion login


        }
    }
}
