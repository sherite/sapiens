using AutoMapper;
using Entities;
using Entities.DTOs;
using Entities.Helpers;

namespace GenericDataLayer.Helpers
{
    /// <summary>
    /// Mapping de DTO a Modelos y de Modelos a TO
    /// </summary>
    public static class ModelMapping
    {
        private static IMapper mapper;

        static ModelMapping()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Generics
                config.CreateMap<User, Entities.DTOs.UserDTO>(MemberList.None).ReverseMap();

                config.CreateMap(typeof(MgrResponse<>), typeof(MgrResponse<>), MemberList.None);

            });

            mapper = mapperConfig.CreateMapper();
        }

        /// <summary>
        /// Mapea un DTO a un modelo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static BluePrint ToModel(this Entities.DTOs.PlanoDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return mapper.Map<Entities.DTOs.PlanoDTO, BluePrint>(dto);

        }

        /// <summary>
        /// Mapea un DTO a un modelo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Rol ToModel(this Entities.DTOs.RolDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return mapper.Map<Entities.DTOs.RolDTO, Rol>(dto);

        }

        /// <summary>
        /// Mapea un DTO a un modelo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Permiso ToModel(this Entities.DTOs.RightDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return mapper.Map<Entities.DTOs.RightDTO, Permiso>(dto);
        }

        /// <summary>
        /// Mapea un DTO a un modelo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static User ToModel(this Entities.DTOs.UserDTO dto)
        {
            if(dto == null)
            {
                return null;
            }

            return mapper.Map<Entities.DTOs.UserDTO, User>(dto);
        }

        /// <summary>
        /// Mapea un DTO a un modelo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Group ToModel(this GroupDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return mapper.Map<GroupDTO, Group>(dto);
        }

        /// <summary>
        /// Mapea un modelo a DTO
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        public static Entities.DTOs.RightDTO ToDTO(this Permiso permiso)
        {
            if (permiso == null)
            {
                return null;
            }

            var retVal = mapper.Map<Permiso, Entities.DTOs.RightDTO>(permiso);

            return retVal;
        }

        /// <summary>
        /// Mapea un modelo a DTO
        /// </summary>
        /// <param name="plano"></param>
        /// <returns></returns>
        public static Entities.DTOs.PlanoDTO ToDTO(this BluePrint plano)
        {
            if (plano == null)
            {
                return null;
            }

            var retVal = mapper.Map<BluePrint, Entities.DTOs.PlanoDTO>(plano);

            return retVal;
        }

        /// <summary>
        /// Mapea un modelo a DTO
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static Entities.DTOs.RolDTO ToDTO(this Rol rol)
        {
            var retVal = new RolDTO();

            try
            {
                if (rol == null)
                {
                    return null;
                }

                retVal = mapper.Map<Rol, Entities.DTOs.RolDTO>(rol);

            }
            catch (System.Exception)
            {

                throw;
            }

            return retVal;
        }

        /// <summary>
        /// Converts to dto.
        /// </summary>
        /// <param name="grupo">The grupo.</param>
        /// <returns></returns>
        public static GroupDTO ToDTO(this Group grupo)
        {
            if (grupo == null)
            {
                return null;
            }

            var retVal = mapper.Map<Group, GroupDTO>(grupo);

            return retVal;
        }

        /// <summary>
        /// Mapea un modelo a DTO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static Entities.DTOs.UserDTO ToDTO(this User usuario)
        {
            if (usuario == null)
            {
                return null;
            }

            return mapper.Map<User, Entities.DTOs.UserDTO>(usuario);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOne"></typeparam>
        /// <typeparam name="TTwo"></typeparam>
        /// <param name="mgrResponse"></param>
        /// <returns></returns>
        public static MgrResponse<TTwo> ToDTO<TOne, TTwo>(
            this MgrResponse<TOne> mgrResponse)
        {
            if (mgrResponse == null)
            {
                return null;
            }

            return mapper.Map<MgrResponse<TOne>, MgrResponse<TTwo>>(mgrResponse);
        }
    }
}