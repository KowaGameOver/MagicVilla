using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public class VillaRepository : Repository<Villa>,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public bool FindByNameAsync(VillaCreateDTO entityCreateDTO)
        {
            if (GetAsync(x => x.Name.ToLower() == entityCreateDTO.Name.ToLower()) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
