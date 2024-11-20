using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.DTO
{
    public class PostMenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }


        public static Menu BuildFromDTO(PostMenuDTO DTO)
        {
            return new Menu()
            {
                MenuId = DTO.MenuId,
                MenuName = DTO.MenuName

            };
        }
    }
}
