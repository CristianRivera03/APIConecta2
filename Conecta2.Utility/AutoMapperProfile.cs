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

            #region module

            CreateMap<Module, ModuleDTO>();

            #endregion module

            #region user
            CreateMap<User, UserDTO>()
                .ForMember(destino =>
                    destino.RoleName,
                    opt => opt.MapFrom(origen => origen.IdRoleNavigation.NameRol));


            CreateMap<UserCreateDTO, User>();

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


            CreateMap<Post, PostCreateDTO>().ReverseMap();
            #endregion post

            #region category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion category

            #region session

            CreateMap<User, SessionDTO>()
                .ForMember(destino =>
                    destino.RoleName,
                    opt => opt.MapFrom(origen => origen.IdRoleNavigation.NameRol))


                .ForMember(destino =>
                    destino.AllowedModules,
                    //saca el modulo de cada registro rm module
                    opt => opt.MapFrom(origen => origen.IdRoleNavigation.Rolemodules.Select(rm => rm.Module))
                );

            #endregion session

            #region login

            CreateMap<LoginDTO, User>();

            #endregion login

            #region role

            CreateMap<CreateRoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            #endregion role


            #region ChangeRoleUser
            CreateMap<ChangeUserRoleDTO, User>();

            #endregion ChangeRoleUser



        }
    }
}
