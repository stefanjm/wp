using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoodPeckerAngular.Core.Interfaces;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Data
{
    public class CupRepository : ICupRepository
    {
        private readonly WoodPeckerAngularContext _context;

        public CupRepository(WoodPeckerAngularContext context)
        {
            _context = context;
        }

        public async Task<CupModel> Delete(CupModel cup)
        {
            var cupModel = await _context.CupModel.FindAsync(cup.Id);
            if (cupModel == null)
            {
                return cupModel;
            }

            _context.CupModel.Remove(cupModel);
            await _context.SaveChangesAsync();

            return cupModel;
        }

        public async Task<IEnumerable<CupModel>> GetAll()
        {
            return await _context.CupModel.ToListAsync();
        }

        public async Task<CupModel> GetById(int id)
        {
            return await _context.CupModel.FindAsync(id);
        }

        public async Task<bool> Insert(CupModel cup)
        {
            _context.CupModel.Add(cup);
            await _context.SaveChangesAsync();

            if (_context.CupModel.Any(e => e.Id == cup.Id))
                return true;
            else
                return false;
        }
    }
}
